using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    public float objectHealth = 10f;
    private bool isDead = false;
    public GameObject destroyedVersion;

    public void TakeDamage(float amount)
    {
        objectHealth -= amount;
        //healthBar.fillAmount = health / startHealth;
        if (objectHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Instantiate(destroyedVersion, transform.position, Quaternion.identity); //Spawn destroyed version on us
        Destroy(gameObject); //Remove current object
    }
}
