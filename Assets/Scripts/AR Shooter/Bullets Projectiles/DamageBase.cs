using System;
using TriInspector;
using UltraPool;
using UnityEngine;

namespace _Scripts_.Bullets_Projectiles
{
    public struct DamageStruct
    {
        public float damage;
        public float velocity;
        public LayerMask hitLayer;
        
        public Transform sender;
        public Transform currentTarget;
        public Transform receiver;

        public Vector3 hitPosition;
        public Vector3 startPos;

        public float rotSpeed;
        public float flyTime;
    }
    
    [DeclareBoxGroup("Base", Title = "Base")]
    [DeclareBoxGroup("Technical", Title = "Technical")]
    public class DamageBase : MonoBehaviour
    {
        protected bool inited;
        protected float timetemp = 0;
  
        [GroupNext("Base")]
        [SerializeField] protected LayerMask hitLayer = -1;
        [SerializeField] protected int damage = 10;
        [GroupNext("Technical")]
        //public bool 
        public bool rayChecker = true;
        public float lifeTime = 1.5f;
        public float raycastDist = 0.2f;

        protected Vector3 previousPosition;
        protected DamageStruct damageStruct;
        
        //public DamageData damageData;
        [HideInInspector] public GameObject Owner;
        
        public virtual void Init(DamageStruct _damageStruct, bool overrideData = false)
        {
            this.damageStruct = _damageStruct;
            this.damageStruct.hitLayer = hitLayer;
            inited = true;
        }

        protected virtual void OnEnable()
        {
            previousPosition = transform.position;
        }

        protected virtual void OnDisable()
        {
            inited = false;
            timetemp = 0;
        }
        
        public void IgnoreSelf(GameObject owner)
        {
            if (GetComponent<Collider>() && owner)
            {
                if (owner.GetComponent<Collider>())
                    Physics.IgnoreCollision(GetComponent<Collider>(), owner.GetComponent<Collider>());

                if (Owner.transform.root)
                {
                    foreach (Collider col in Owner.transform.root.GetComponentsInChildren<Collider>())
                    {
                        Physics.IgnoreCollision(GetComponent<Collider>(), col);
                    }
                }
            }
        }
        
        protected virtual void RayCheckerSingle()
        {
            RaycastHit hitInfo;
            
            Debug.DrawLine(transform.position, transform.position + transform.forward * raycastDist);
            if (Physics.Linecast(transform.position, transform.position + transform.forward * raycastDist, out hitInfo, hitLayer))
            {
                if (!hitInfo.collider)
                    return;

                damageStruct.hitPosition = hitInfo.point;
                damageStruct.receiver = hitInfo.collider.transform;
                
                DealDamage(hitInfo.transform);
                Despawn();
            }

            previousPosition = transform.position;
        }
        
    
        protected virtual void DealDamage(Transform objHitted)
        {
             objHitted.GetComponentInParent<Character>()?.TakeDamage(damage);
        }
    
        protected virtual void Despawn()
        {
            Debug.Log("AR: Every script must have own Despawn() " + name);
        }
    }
}