using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyBossStats))]
public class BossAIScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    Animator anim;
    EnemyBossStats enemBossStats;
    private Vector3 startPos;

    [Header("Enemy Stats")]
    public float detectRange = 15f;
    public float attackRange = 3f;
    public float attackSpeed = 4f;


    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemBossStats = GetComponent<EnemyBossStats>();
        anim = GetComponent<Animator>();
        startPos = transform.position;

        InvokeRepeating("DetectAndChasePlayer", 0.5f, 1f);  //Run "DetectAndChasePlayer" function every 1 second.
    }

    // Update is called once per frame
    void Update()
    {
        {
            if(enemBossStats.isDead == true)
            {
                MoveToPoint(transform.position);
            }
        }
    }

    void DetectAndChasePlayer()
    {
        if (enemBossStats.canControl == true)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        anim.SetBool("Walk", false); //If the boss is standing still, stop "moving".
                    }
                }
                else
                {
                    anim.SetBool("Walk", true);
                }
            }


            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist <= attackRange) //If player is in attack range, attack
            {
                MoveToPoint(transform.position);
                if (!isAttacking)
                {
                    StartCoroutine(attack(attackSpeed));
                }
            }
            else if (dist <= detectRange) //Else if the player is outside of attack range but inside of the detection range, move towards the player
            {
                MoveToPoint(player.transform.position);
            }
            else //If the player is no longer inside of the attack -or detection range, move back to the start position.
            {
                MoveToPoint(startPos);
            }

        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public IEnumerator attack(float attSpeed)
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.75f);
        player.GetComponent<CharacterStats>().TakeDamage(enemBossStats.attackDamage);
        yield return new WaitForSeconds(10 / attackSpeed);
        isAttacking = false;
    }
}
