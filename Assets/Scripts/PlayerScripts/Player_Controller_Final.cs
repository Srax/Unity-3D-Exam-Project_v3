using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Player_Motor_Final))]
[RequireComponent(typeof(NavMeshAgent))]
public class Player_Controller_Final : MonoBehaviour
{
    public LayerMask movementMask;
    public Interactable focus;
    Player_Motor_Final motor;
    Animator anim;                      //Reference to our animator
    NavMeshAgent agent;                 //Reference to our navmesh agent

    CharacterStats cs;


    [Header("Attack1 CD")]
    private float attack1NextFire = 0.0f;
    public float attack1FireRate = 2f;
    public float attack1Radius = 2f;
    [Header("Attack2 CD")]
    private float attack2NextFire = 0.0f;
    public float attack2FireRate = 4f;
    public float attack2Radius = 4f;
    [Header("Attack3 CD")]
    private float attack3NextFire = 0.0f;
    public float attack3FireRate = 10f;

    [Header("Misc")]
    public GameObject spell_FireBolt;
    public GameObject attack1Pos;
    public Collider[] hitObjects;
    public Camera cam;

    private void Start()
    {
        motor = GetComponent<Player_Motor_Final>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        cs = gameObject.GetComponent<CharacterStats>();        
    }

    void Update()
    {

        if (cs.isControllable == true)
        {
            //If leftclick, move
            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }
            }

            //if rightclick, interact
            if (Input.GetMouseButton(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }
            }

            //Attack 1
            if (Input.GetKey(KeyCode.Alpha1) && Time.time > attack1NextFire)
            {
                attack1NextFire = Time.time + attack1FireRate;
                Attack1();
            }

            //Attack 2
            if (Input.GetKey(KeyCode.Alpha2) && Time.time > attack2NextFire)
            {
                attack2NextFire = Time.time + attack2FireRate;
                Attack2();
            }

            //Attack 3
            if (Input.GetKey(KeyCode.Alpha3) && Time.time > attack3NextFire)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    attack3NextFire = Time.time + attack3FireRate;
                    shootEnergyBolt(hit);
                }
            }
        } else
        {
            //Do nothing
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }



    void shootEnergyBolt(RaycastHit hit)
    {
        EnergyBallSpell ebs = spell_FireBolt.GetComponent<EnergyBallSpell>();
        float manaCost = 40;
        if (cs != null)
        {
            if (cs.currentPlayerMana >= manaCost)
            {
                cs.DecreaseMana(manaCost); //Remove X amount of mana from the player
                GameObject spellGo = (GameObject)Instantiate(spell_FireBolt, transform.position, transform.rotation);
                EnergyBallSpell fb = spellGo.GetComponent<EnergyBallSpell>();

                if (fb != null)
                {
                    fb.Seek(hit.point);
                }
            }
            else
            {
                print("Not enough mana");
            }
        }
        else
        {
            print("MISSING PlayerStats script");
        }
    }

    public void Attack1()
    {
        float manaCost = 10f;
        if (cs != null)
        {
            if (cs.currentPlayerMana >= manaCost)
            {
                anim.SetTrigger("attack1");
                cs.DecreaseMana(manaCost); //Remove X amount of mana from the player
                agent.SetDestination(gameObject.transform.position); //Set actors destination to it's current destination (to stop it)
                hitObjects = Physics.OverlapSphere(attack1Pos.transform.position, attack1Radius);
                StartCoroutine("att1");
            }
            else
            {
                print("Not enough mana");
            }
        }
        else
        {
            print("MISSING PlayerStats script");
        }
    }

    public void Attack2()
    {
        float manaCost = 20f;
        if (cs != null)
        {
            if (cs.currentPlayerMana >= manaCost)
            {
                anim.SetTrigger("attack2");
                cs.DecreaseMana(manaCost); //Remove X amount of mana from the player
                agent.SetDestination(gameObject.transform.position); //Set actors destination to it's current destination (to stop it)
                hitObjects = Physics.OverlapSphere(transform.position, attack2Radius);
                StartCoroutine("att2");
            }
            else
            {
                print("Not enough mana");
            }
        }
        else
        {
            print("MISSING PlayerStats script");
        }
    }


    public IEnumerator att1()
    {
        yield return new WaitForSeconds(1f);
        foreach (Collider enemy in hitObjects)
        {
            if (enemy.tag == "Enemy")
            {
                EnemySkeletonStats enemSkeletonStats = enemy.GetComponent<EnemySkeletonStats>();
                if (enemSkeletonStats != null) //If the enemy have the script
                {
                   enemSkeletonStats.TakeDamage(cs.playerMeleeDamage);

                    if (enemSkeletonStats.isDead == true)
                    {
                        cs.UpdateExp(enemSkeletonStats.amountExp);
                    }

                }
            }
        }
    }

    public IEnumerator att2()
    {
        yield return new WaitForSeconds(1f);
        foreach (Collider collider in hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                EnemySkeletonStats enemSkeletonStats = collider.GetComponent<EnemySkeletonStats>();
                if (enemSkeletonStats != null) //If the enemy have the script
                {
                    enemSkeletonStats.TakeDamage(cs.playerMeleeDamage * 1.5f);
                    if (enemSkeletonStats.isDead == true)
                    {
                        cs.UpdateExp(enemSkeletonStats.amountExp);
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attack2Radius);
        Gizmos.DrawWireSphere(attack1Pos.transform.position, attack1Radius);
    }
}
