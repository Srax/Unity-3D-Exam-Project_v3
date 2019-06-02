using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossStats : MonoBehaviour
{

    public GameObject gm;

    private float startEnemyHealth;
    public float currentEnemyHealth = 500;
    public float attackDamage = 30f;
    public float amountExp;
    public Image healthBar;
    public GameObject healthBarCanvas;

    public bool isDead = false;
    public bool canControl = true;
    Animator anim;


    void Start()
    {
        startEnemyHealth = currentEnemyHealth;
        anim = GetComponent<Animator>();
        canControl = true;
        gm = GameObject.FindGameObjectWithTag("GameMaster");
    }

    public void TakeDamage(float amount)
    {
        currentEnemyHealth -= amount; //Reduce current health by damage taken
        healthBar.fillAmount = currentEnemyHealth / startEnemyHealth;
        if (currentEnemyHealth <= 0 && !isDead) { Die(); } //Die
    }


    void Die()
    {
        isDead = true; //The enemy is dead
        canControl = false; //Enemy can no longer control
        healthBarCanvas.SetActive(false); //Deactivate HealthCanvas
        anim.SetTrigger("Die"); //Play death animation.
        gm.GetComponent<GameMasterScript>().quest.goal.BossKilled(); //Complete BossKill quest
        Destroy(gameObject, 5f); //Destroy Phanos after 15 seconds.
    }
}
