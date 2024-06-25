using UnityEngine;

// Basic input 
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private TankController controller;
    [SerializeField] private TurretGun turretGun;
    [SerializeField] private AbilityManager abilityManager;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<TankController>();
        if (turretGun == null)
            turretGun = GetComponent<TurretGun>();
    }

    void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            turretGun.Shoot();
        
        if (Input.GetKeyDown(KeyCode.Y))
            abilityManager.UseAbilitySlot(0);
        
        controller.MoveHorizontal(Input.GetAxis(GlobalString.Horizontal));
    }
}
