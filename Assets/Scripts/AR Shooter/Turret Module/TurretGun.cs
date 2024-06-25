using _AR_.Extensions;
using _Scripts_.Bullets_Projectiles;
using _Scripts_.Bullets_Projectiles;
using JoostenProductions;
using TriInspector;
using UltraPool;
using UnityEngine;

#region DeclareFoldoutGroup

[DeclareBoxGroup("References", Title = "References")]
[DeclareBoxGroup("Base", Title = "Base")]
[DeclareBoxGroup("Bullet", Title = "Bullet data")]
[DeclareBoxGroup("Debug", Title = "Debug")]

[DeclareToggleGroup("overrideBulletData", Title = "Override Bullet Data")]
#endregion

public class TurretGun : OverridableMonoBehaviour
{
    #region Variables

    
    [InlineEditor]
    [SerializeField] private TurretGunConfigSO config; 

    [GroupNext("References")]
    [SerializeField] private Transform spawnPoint;
    
  
    [Group("overrideBulletData")] public bool overrideBulletData;
    [Group("overrideBulletData")] public float damage;
    [Group("overrideBulletData")] public float velocity;
    [Group("overrideBulletData")] public LayerMask hitLayer;

    [GroupNext("Debug")]
    [SerializeField] private int currLevel = 1;
    [SerializeField] private bool drawDebug;
    [SerializeField] private float cooldownTimer;

    // Cache
    private bool inited;
    private SimpleBullet currBullet;
    private DamageStruct _damageStruct;
  
    private TurretGunLevel Data => config.GetData(currLevel);

    #endregion

    private void Awake()
    {
        if (Data.simpleBulletPrefab == null)
        {
            Debug.LogError($"AR: No bullet prefab at {transform.name}" );
            enabled = false;
        }
        
        if (spawnPoint == null)
        {
            Debug.LogError($"AR: No spawnPoint at {transform.name}" );
            enabled = false;
        }

        Init();
    }

    private void Init()
    {
        if (inited)
            return;

        _damageStruct = new DamageStruct();
        UpdateDamageInfo();
        
        inited = true;
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        cooldownTimer = Data.cooldown;
    }

    /// <summary>
    /// Update inherited from UpdateManager
    /// </summary>
    public override void UpdateMe()
    {
        if (inited == false)
            return;

        // Cooldown
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        if (drawDebug)
            DrawDebugRays();
    }

    private bool CanShoot()
    {
        if (cooldownTimer > 0)
            return false;

        return true;
    }

    [Button]
    public void Shoot()
    {
        if (CanShoot() == false)
            return;

        cooldownTimer = config.GetData(currLevel).cooldown;
        
        currBullet = ObjectPoolManager.Instance.GetObjectAuto(Data.simpleBulletPrefab);
        currBullet.transform.SetNewPosRot(spawnPoint);
        
        currBullet.Init(_damageStruct, overrideBulletData);
        currBullet.gameObject.SetActive(true);
    }


    private void UpdateDamageInfo()
    {
        _damageStruct.sender = transform;
        
        if (overrideBulletData)
        {
            _damageStruct.sender = transform;
            _damageStruct.damage = damage;
            _damageStruct.velocity = velocity;
            _damageStruct.hitLayer = hitLayer;
        }
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
    
    private void DrawDebugRays()
    {
        if (spawnPoint != null)
            Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 100.0f);
    }
}