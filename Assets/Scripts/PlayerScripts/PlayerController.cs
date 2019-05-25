using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject spell_Spawner;
    public bool canMove;

    [Header("Attributes")]
    public float range = 15f;
    private Transform target;
    private string enemyTag = "Enemy";

    void Update()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        //Cast a ray to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //If shift and left Click is pressed at he same time, stand still and rotate towards mouse position
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
        {
            //If the raycast  hits
            if (Physics.Raycast(ray, out hit))
            {
                //Get mouse position and rotate the player towards that position on the Y-axis
                Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.LookAt(targetPos);
                //set agent's destionation to players current destionation to make it stop moving
                agent.SetDestination(transform.position);
            }
        }

        //If left click is down, move towards mouse position
        else if (Input.GetMouseButton(0))
        {
            //If the raycast  hits
            if (Physics.Raycast(ray, out hit))
            {
                //set agent's destination to mouse's destionation
                agent.SetDestination(hit.point);
            }

        }
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
