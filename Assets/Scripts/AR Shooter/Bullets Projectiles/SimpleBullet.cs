using TriInspector;
using UnityEngine;
using UltraPool;
using UnityEngine.PlayerLoop;

namespace _Scripts_.Bullets_Projectiles
{
    
    [DeclareBoxGroup("Base", Title = "Base")]
    [DeclareBoxGroup("Effects", Title = "Effects")]

    public class SimpleBullet : DamageBase
    {
        #region Variables
       
        [GroupNext("Base")]
        [SerializeField] private float velocity = 380;
        
        [GroupNext("Effects")]
        [SerializeField] 
        private Transform destroyEffect;

        #endregion

        #region Set
        #endregion

        private void UpdateDamageInfo()
        {}
        
        public override void Init(DamageStruct _damageStruct, bool overrideData = false)
        {
             this.damageStruct = _damageStruct;
            
            // Sender don't override data so take main data
            if (overrideData == false)
            {
                this.damageStruct.hitLayer = hitLayer;
                this.damageStruct.damage = damage;
                this.damageStruct.velocity = velocity;
            }
            
            inited = true;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            timetemp = 0;
        }
        
        public void Update()
        {
            if (inited == false)
                return;

            if (rayChecker)
                RayCheckerSingle();

            // if (lifeTime > 0)
            //     UpdateLifeTime();

            //Move
            transform.Translate(damageStruct.velocity * Time.deltaTime * Vector3.forward, Space.Self);
        }

        private void UpdateLifeTime()
        {
            timetemp += Time.deltaTime;
            
            if (timetemp >= lifeTime)
                Despawn();
        }
        
        protected override void Despawn()
        {
            gameObject.SetActive(false);
            ObjectPoolManager.Instance.ReturnObject(this);
        }

//        private void ExplosionDamage()
//        {
//            Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
//            for (int i = 0; i < hitColliders.Length; i++)
//            {
//                Collider hit = hitColliders[i];
//                if (!hit)
//                    continue;
//
//                if (hit.GetComponent<Rigidbody>())
//                    hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0f);
//
//            }
//
//            Collider[] dmhitColliders = Physics.OverlapSphere(transform.position, DamageRadius);
//
//            for (int i = 0; i < dmhitColliders.Length; i++)
//            {
//                Collider hit = dmhitColliders[i];
//
//                if (!hit)
//                    continue;
//
//                if (DoDamageCheck(hit.gameObject) && (Owner == null || (Owner != null && hit.gameObject != Owner.gameObject)))
//                {
//                    DamagePack damagePack = new DamagePack();
//                    damagePack.Damage = Damage;
//                    damagePack.Owner = Owner;
//                    hit.gameObject.SendMessage("ApplyDamage", damagePack, SendMessageOptions.DontRequireReceiver);
//                }
//            }
//
//        }
//
//        private void NormalDamage(Collision collision)
//        {
//            DamagePack damagePack = new DamagePack();
//            damagePack.Damage = Damage;
//            damagePack.Owner = Owner;
//            collision.gameObject.SendMessage("ApplyDamage", damagePack, SendMessageOptions.DontRequireReceiver);
//        }
    }
}