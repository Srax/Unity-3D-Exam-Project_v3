using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestTarget : MonoBehaviour
{
    public Collider[] hitObjects;
    public float radius = 5f;

    public void Update()
    {
       /* hitObjects = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                EnemySkeletonStats enemScript = collider.GetComponent<EnemySkeletonStats>();
                if (enemScript != null) //If the enemy have the script
                {
                    enemScript.TakeDamage(10f);
                }
            }
        }*/
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
