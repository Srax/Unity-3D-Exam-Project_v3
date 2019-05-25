using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;                   //Target to follow
    NavMeshAgent agent;                 //Reference to our navmesh agent
    public float rotationSpeed = 5f;    //How fast our player rotates
    Animator anim;                      //Reference to our animator


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }


        CheckForAnimations();
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;
        target = newTarget.InteractionTransform;    
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }


    void CheckForAnimations()
    {
        /* This code is the same as the one below, but with less steps
        if(agent.velocity.sqrMagnitude > 0.1f)
        {
            anim.SetBool("idle_normal", false); //Set idle to false
            anim.SetBool("move_forward", true); //Set moving to true
        } else
        {
            anim.SetBool("idle_normal", true); //Set idle to false
            anim.SetBool("move_forward", false); //Set moving to true
        }
        */

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    anim.Play("Idle"); //If the player is not moving, play the "idle" animation.
                }
            }
            else
            {
                anim.Play("Run"); //If the animator is moving, play the "run" animation
            }
        }
    }

}
