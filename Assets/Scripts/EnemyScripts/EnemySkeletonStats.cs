using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkeletonStats : MonoBehaviour
{
    private float startEnemyHealth;
    public float currentEnemyHealth = 100f;
    public float attackDamage = 10f;
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
    }

    private void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        currentEnemyHealth -= amount; //Reduce current health by damage taken
        healthBar.fillAmount = currentEnemyHealth / startEnemyHealth;
        if (currentEnemyHealth <= 0 && !isDead) { Die(); } //Die
    }


    void Die()
    {
        isDead = true;
        canControl = false;
        healthBarCanvas.SetActive(false);
        anim.SetTrigger("Die");
        Destroy(gameObject, 15f);
    }
}
