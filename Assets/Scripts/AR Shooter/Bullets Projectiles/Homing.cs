using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Homing : MonoBehaviour
{
    public Transform target;
    public float drivespeed = 40;
    public float damping = 5.0f;

    public bool hasSplit = false;

    public bool splitMissile = false;
    public int amountToSplit = 2;
    public float retargetRadius = 200.0f;
    public List<Collider> listcolliders;


    Collider[] colliders;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Work out the direction you need to be facing to be moving towards the target
        if (target != null)
        {
            //Actually turn in that direction
            Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            if (splitMissile == true)
            {
                //This is the range form the target at which to split
                if (Vector3.Distance(target.position, transform.position) <= 150.0f && hasSplit == false)
                {
                    //the amount of little rockets you want to spawn
                    for (int x = 0; x < amountToSplit; x++)
                    {
                        //This gives the rockets a little random-ness in the direction they spawn
                        GameObject rocketclone;
                        Quaternion randRot = transform.rotation;
                        float randY = UnityEngine.Random.Range(-4.0f, 4.0f);
                        randRot.y += randY;


                        Vector3 randomPos = transform.position;

                        float randX = UnityEngine.Random.Range(-4.0f, 4.0f);
                        randRot.x += randX;
                        randomPos.x += randX;
                        randomPos.y += randY;


                        //this instantiates the the little rockets and activates their homing script giving it a target of whatever the ray cast hit.
                        rocketclone = Instantiate(gameObject, randomPos, randRot) as GameObject;
                        rocketclone.GetComponent<Homing>().target = target;
                        rocketclone.GetComponent<Homing>().hasSplit = true;
                        //The smaller rockets go faster
                        rocketclone.GetComponent<Homing>().drivespeed = drivespeed * 2;
                    }

                    //Set has split to true and destroy the "parent" rocket 
                    hasSplit = true;
                    Destroy(gameObject);
                }
            }
        }

        if (target == null)
        {
            RaycastHit hitInfo;
            colliders = Physics.OverlapSphere(transform.position, retargetRadius);
            listcolliders = colliders.ToList();


            for (int x = 0; x < listcolliders.Count; x++)
            {
                //int lastTarget = colliders.Length;
                //Array.Sort(colliders);
                if (listcolliders[x].gameObject.tag != "Enemy")
                {
                    listcolliders.RemoveAt(x);
                }

                //if(listcolliders[x].gameObject.tag == "Enemy")
                //{
                //Debug.Log(listcolliders[x].name);


                //}
            }

            target = listcolliders[0].transform;
        }

        //Drives the rocket foward
        transform.Translate(Vector3.forward * Time.deltaTime * drivespeed);
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, retargetRadius);
    }

    void OnTriggerEnter(Collider c)
    {
        //If you come in contact with an enemy, start the explosions and junk.
        if (c.tag == "Enemy")
        {
            StartCoroutine(death());
            if (c.gameObject != null)
            {
                target = null;
                Destroy(c.gameObject);
            }
            else
            {
            }
        }
    }

    //Explosions and junk
    IEnumerator death()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}