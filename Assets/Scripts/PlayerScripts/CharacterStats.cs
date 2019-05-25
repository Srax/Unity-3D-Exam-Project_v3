using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public float currentPlayerHealth;
    public float currentPlayerMana;

    private float maxPlayerHealth = 300;
    private float maxPlayerMana = 100f;
    private float maxPlayerExp = 100f;

    public float playerMeleeDamage = 25f;
    public float playerSpellDamage = 20f;

    [Header("STATS")]
    public float exp = 0;
    public float level = 1;
    public float vitality = 1;
    public float intellect = 1;
    public float strength = 1;
    public float manaRegenSpeed = 1f;

    [Header("MISC")]
    Animator anim;                      //Reference to our animator
    public Image healthBar;
    public Image expBar;
    public Image manaBar;
    public bool isDead = false;
    public bool isControllable = true;

    void Start()
    {
        InvokeRepeating("RegenerateMana", 0.0f, (1.0f/manaRegenSpeed)); //Regenerate mana all the time
        currentPlayerHealth = maxPlayerHealth;
        currentPlayerMana = maxPlayerMana;
        anim = GetComponent<Animator>();
        healthBar.fillAmount = currentPlayerHealth / maxPlayerHealth;
        manaBar.fillAmount = currentPlayerMana / maxPlayerMana;
        expBar.fillAmount = exp / maxPlayerExp;
        isControllable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("b")){TakeDamage(100); UpdateExp(20); DecreaseMana(50); }


        if (currentPlayerMana > maxPlayerMana) { currentPlayerMana = maxPlayerMana; } //Players mana can't be larger than the max allowed mana
        if (currentPlayerHealth > maxPlayerHealth) { currentPlayerHealth = maxPlayerHealth; } //Same as the above, but for health instead of mana
        if (currentPlayerMana < 0) { currentPlayerMana = 0f; } //Player mana can't get below 0
    }

    public void TakeDamage(float amount)
    {
        currentPlayerHealth -= amount; //Reduce current health by damage taken        
        healthBar.fillAmount = currentPlayerHealth / maxPlayerHealth; //Change the healthBar's fill amount
        if (currentPlayerHealth <= 0 && !isDead) {Die();} //Die
    }

    void Die()
    {
        isDead = true;
        isControllable = false;
        anim.SetTrigger("die");
    }

    void RegenerateMana()
    {
        if (currentPlayerMana < maxPlayerMana)
        {
            currentPlayerMana += 1;
            manaBar.fillAmount = currentPlayerMana / maxPlayerMana; //Change the manaBar's fill amount
        }
    }

    public void DecreaseMana(float amount)
    {
        currentPlayerMana -= amount;
        manaBar.fillAmount = currentPlayerMana / maxPlayerMana; //Change the manaBar's fill amount
    }


    public void UpdateExp(float amount)
    {
        exp += amount;
        expBar.fillAmount = exp / maxPlayerExp;
        if (exp >= maxPlayerExp){LevelUp();}
    }

    void LevelUp()
    {
        level += 1;
        vitality += 1;
        intellect += 1;
        strength += 1;
        manaRegenSpeed += 0.5f;
        exp = 0; //Reset EXP

        maxPlayerHealth += (vitality * 10);
        maxPlayerMana += (intellect * 10);
        maxPlayerExp += 50f;
        playerMeleeDamage += (strength * 4.5f);
        playerSpellDamage += (intellect * 3f);

        currentPlayerHealth = maxPlayerHealth; //Give the player full health
        currentPlayerMana = maxPlayerMana; //Give the player full mana

        //Reset the mana, health and exp bar
        expBar.fillAmount = 0f;
        healthBar.fillAmount = 1f;
        manaBar.fillAmount = 1f;
    }
}
