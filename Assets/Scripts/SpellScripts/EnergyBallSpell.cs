﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallSpell : MonoBehaviour
{
    private Vector3 target;
    public float speed = 70f;
    public float explosionRadius = 5f;
    public float spellDamage = 5;
    public GameObject impactEffect;
    public GameObject player;

    public Collider[] hitObjects;
    public void Seek(Vector3 _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target) - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position + new Vector3(0,1,0), transform.rotation); //Spawn a bullet shatter effect
       Destroy(effectIns, 2f); //Destroy bullet shatter effect after 2 seconds (length of animation)

        if (explosionRadius >= 0)
        {
            Explode();
        }
        Destroy(gameObject); //Destroy spellObject
    }

    void Explode()
    {
        hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitObjects)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(500, transform.position, explosionRadius, 1);

            if (collider.tag == "Enemy")
            {
                EnemySkeletonStats enemSkeletonStats = collider.GetComponent<EnemySkeletonStats>();
                if(enemSkeletonStats != null) //If the enemy have the script
                {
                    enemSkeletonStats.TakeDamage(player.GetComponent<CharacterStats>().playerSpellDamage * 2);
                    if (enemSkeletonStats.isDead == true)
                    {
                        //player.GetComponent<CharacterStats>().UpdateExp(10f);
                    }
                }
            }

            if (collider.tag == "DestructableObject")
            {
                ObjectStats objScript = collider.GetComponent<ObjectStats>();
                if(objScript != false) //If the object have the script
                {
                    objScript.TakeDamage(spellDamage); //Damage the destructable object
                }
            }
        }
    }

    void Damage(Transform enemy)
    {
        EnemyStats es = enemy.GetComponent<EnemyStats>();

        if (es != null)
        {
            es.TakeDamage(spellDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
