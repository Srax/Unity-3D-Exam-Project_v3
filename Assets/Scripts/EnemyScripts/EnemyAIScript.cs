using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAIScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    Animator anim;
    EnemySkeletonStats enemSkeleStat;
    private Vector3 startPos;

    [Header("Enemy Stats")]
    public float detectRange = 15f;
    public float attackRange = 3f;
    public float attackSpeed = 4f;


    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemSkeleStat = GetComponent<EnemySkeletonStats>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemSkeleStat.canControl == true)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist <= attackRange) //If player is in attack range, attack
            {
                agent.SetDestination(transform.position);
                if (!isAttacking)
                {
                    StartCoroutine(attack(attackSpeed));
                }
            } else if(dist <= detectRange) //Else if the player is outside of attack range but inside of the detection range, move towards the player
            {
                agent.SetDestination(player.transform.position);
            } else //If the player is no longer inside of the attack -or detection range, move back to the start position.
            {
                agent.SetDestination(startPos);
            }

        } else
        {
            //The enemy is dead
        }
    }

    public IEnumerator attack(float attSpeed)
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        player.GetComponent<CharacterStats>().TakeDamage(enemSkeleStat.attackDamage);
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }
}
