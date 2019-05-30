using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameMasterScript gm;


    [Header("Attack1 CD")]
    public float attack1CoolDownTimer;
    public float attack1CoolDown = 2.5f;
    public float attack1Radius = 2f;
    public float attack1ManaCost = 20f;
    public Image attack1UIButton;
    [Header("Attack2 CD")]
    public float attack2CoolDownTimer;
    public float attack2CoolDown = 4f;
    public float attack2Radius = 4f;
    public float attack2ManaCost = 40f;
    public Image attack2UIButton;
    [Header("Attack3 CD")]
    public float attack3CoolDownTimer;
    public float attack3CoolDown = 8f;
    public float attack3ManaCost = 40f;
    public Image attack3UIButton;

    [Header("Health Potion CD")]
    public float healthPotionCoolDownTimer;
    public float healthPotionCoolDown = 2.5f;
    public float healAmount = 100f;
    public Image healthPotionUIButton;

    [Header("Mana Potion CD")]
    public float manaPotionCoolDownTimer;
    public float manaPotionCoolDown = 2.5f;
    public float manaAmount = 100f;
    public Image manaPotionUIButton;

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

    void FixedUpdate()
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
            if (Input.GetKey(KeyCode.Alpha1) && attack1CoolDownTimer == 0)
            {
                if(cs.currentPlayerMana > attack1ManaCost)
                {
                    Attack1();
                    attack1CoolDownTimer = attack1CoolDown;
                } else
                {
                    print("Not enough mana for attack 1!");
                }
            }

            //Attack 2
            if (Input.GetKey(KeyCode.Alpha2) && attack2CoolDownTimer == 0)
            {
                if (cs.currentPlayerMana > attack2ManaCost)
                {
                    Attack2();
                    attack2CoolDownTimer = attack2CoolDown;
                }
                else
                {
                    print("Not enough mana for attack 2!");
                }
            }

            //Attack 3
            if (Input.GetKey(KeyCode.Alpha3) && attack3CoolDownTimer == 0)
            {
                if(cs.currentPlayerMana > attack3ManaCost)
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        shootEnergyBolt(hit);
                        attack3CoolDownTimer = attack3CoolDown;
                    }
                } else
                {
                    print("Not enough mana for attack 3!");
                }
            }

            //Use Health Potion
            if (Input.GetKeyDown(KeyCode.Alpha4) && healthPotionCoolDownTimer == 0)
            {
                if (cs.currentPlayerHealth < cs.maxPlayerHealth)
                {
                    cs.AddHealth(healAmount);
                    healthPotionCoolDownTimer = healthPotionCoolDown;
                }
                else
                {
                    print("You are already full on health.");
                }
            }

            //Use Mana Potion
            if (Input.GetKeyDown(KeyCode.Alpha5) && manaPotionCoolDownTimer == 0)
            {
                if (cs.currentPlayerMana < cs.maxPlayerMana)
                {
                    cs.AddMana(manaAmount);
                    manaPotionCoolDownTimer = manaPotionCoolDown;
                }
                else
                {
                    print("You are already full on mana.");
                }
            }



            //Cooldown Timers :)
            if (attack1CoolDownTimer > 0){
                attack1UIButton.fillAmount = attack1CoolDownTimer / attack1CoolDown;
                attack1CoolDownTimer -= Time.deltaTime;
            }

            if (attack2CoolDownTimer > 0){
                attack2UIButton.fillAmount = attack2CoolDownTimer / attack2CoolDown;
                attack2CoolDownTimer -= Time.deltaTime;
            }

            if (attack3CoolDownTimer > 0){
                attack3UIButton.fillAmount = attack3CoolDownTimer / attack3CoolDown;
                attack3CoolDownTimer -= Time.deltaTime;
            }

            if (healthPotionCoolDownTimer > 0)
            {
                healthPotionUIButton.fillAmount = healthPotionCoolDownTimer / healthPotionCoolDown;
                healthPotionCoolDownTimer -= Time.deltaTime;
            }

            if (manaPotionCoolDownTimer > 0)
            {
                manaPotionUIButton.fillAmount = manaPotionCoolDownTimer / manaPotionCoolDown;
                manaPotionCoolDownTimer -= Time.deltaTime;
            }

            if (attack1CoolDownTimer <= 0) { attack1CoolDownTimer = 0f; }
            if (attack2CoolDownTimer <= 0) { attack2CoolDownTimer = 0f; }
            if (attack3CoolDownTimer <= 0) { attack3CoolDownTimer = 0f; }
            if (healthPotionCoolDownTimer <= 0) { healthPotionCoolDownTimer = 0f; }
            if (manaPotionCoolDownTimer <= 0) { manaPotionCoolDownTimer = 0f; }

        } else {
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
        if (cs != null)
        {
                cs.DecreaseMana(attack3ManaCost); //Remove X amount of mana from the player
                GameObject spellGo = (GameObject)Instantiate(spell_FireBolt, transform.position, transform.rotation);
                EnergyBallSpell fb = spellGo.GetComponent<EnergyBallSpell>();

                if (fb != null)
                {
                    fb.Seek(hit.point);
            }
        }
        else
        {
            print("MISSING PlayerStats script");
        }
    }

    public void Attack1()
    {
        if (cs != null)
        {
                anim.SetTrigger("attack1");
                cs.DecreaseMana(attack1ManaCost); //Remove X amount of mana from the player
                agent.SetDestination(gameObject.transform.position); //Set actors destination to it's current destination (to stop it)
                hitObjects = Physics.OverlapSphere(attack1Pos.transform.position, attack1Radius);
                StartCoroutine("att1");
        }
        else
        {
            print("MISSING PlayerStats script");
        }
    }

    public void Attack2()
    {
        if (cs != null)
        {
                anim.SetTrigger("attack2");
                cs.DecreaseMana(attack2ManaCost); //Remove X amount of mana from the player
                agent.SetDestination(gameObject.transform.position); //Set actors destination to it's current destination (to stop it)
                hitObjects = Physics.OverlapSphere(transform.position, attack2Radius);
                StartCoroutine("att2");
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
                        cs.AddExp(enemSkeletonStats.amountExp);
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
                        cs.AddExp(enemSkeletonStats.amountExp);
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
