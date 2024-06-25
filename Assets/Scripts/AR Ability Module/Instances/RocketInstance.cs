using System;
using System.Collections;
using System.Collections.Generic;
using _AR_.Extensions;
using TriInspector;
using UltraPool;
using UnityEngine;

public class RocketInstance : AbilityInstance
{
    #region Variable

    [Space] 
    [GroupNext("Base")]
    [SerializeField] private float velocity = 380;

    [GroupNext("Effects")]
    [SerializeField] 
    private Transform destroyEffect;

    public float raycastDist = 0.2f;
    [SerializeField] protected LayerMask hitLayer = -1;

    
    [Title("For debug")]
    [DisableInEditMode]
    public Transform owner;
    [DisableInEditMode]
    public Transform explodePrefab;
    [DisableInEditMode]
    public Transform explodeInstance;
    [DisableInEditMode]
    public float damageValue;
   
    [DisableInEditMode]
    public float explosionRadius;

    private bool exploded;
    
    private bool inited;
    private Collider[] results = new Collider[20];
    #endregion

    public override void Init()
    {
        exploded = false;
        inited = true;
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log("AR: Activate Rocket");
        Explode();
    }

    public void Update()
    {
        if (inited == false)
            return;

        RayCheckerSingle();

        //Move
        transform.Translate(velocity * Time.deltaTime * Vector3.forward, Space.Self);
    }

    
    protected virtual void RayCheckerSingle()
    {
        RaycastHit hitInfo;
            
        Debug.DrawLine(transform.position, transform.position + transform.forward * raycastDist);
        if (Physics.Linecast(transform.position, transform.position + transform.forward * raycastDist, out hitInfo, hitLayer))
        {
            if (!hitInfo.collider)
                return;

            Activate();
        }

    }
    
    private void Explode()
    {
        if (exploded)
            return;

        exploded = true;

        int num = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, results, LayerMask.GetMask("Enemy"));
        
        for (int i = 0; i < num; i++)
        {
            Collider hitCollider = results[i];
            if (hitCollider.transform.root.CompareTag(GlobalString.Enemy))
            {
                Character character = hitCollider.GetComponentInParent<Character>();
                if (character != null)
                {
                    character.TakeDamage(damageValue);
                }
            }
        }

        Despawn();
    }

    private void Despawn()
    {
        inited = false;
        ObjectPoolManager.Instance.ReturnObject( this.name, this);
    }
}
