using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyHealth = 10f;
    public bool isDead = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("idle01", true);
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        //healthBar.fillAmount = health / startHealth;
        if (enemyHealth <= 0 && !isDead)
        {
            Die();
        } else
        {
            StartCoroutine(takeDamageAnim());
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetBool("dead", true);
        //PlayerControlScript.Money += enemyMoney;
        Destroy(gameObject, 5f);
    }


    IEnumerator takeDamageAnim()
    {
        anim.SetBool("damage", true);
        yield return new WaitForSeconds(0.5f); //Length of animaton
        anim.SetBool("damage", false);
    }
}
