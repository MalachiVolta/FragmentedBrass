using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IHealthSys
{
    public Animator animator;
    public GameHandler gameHandler;
    int maxHealth = 100;
    int currentHealth;
    bool isAlive = true;
    public bool isBuilding = false;
    public bool crosshair = false;


    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
    }

    void Update()
    {
        if (gameHandler.isMidWave == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|GunIdle"))
        {
            isBuilding = true;
        }
        else if (gameHandler.isMidWave == false && (animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|IdleRadio")))
        {
            isBuilding = false;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);
    }

    public void Damage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0) Die();
    }

    public void Heal(int heal)
    {
        if (isAlive)
        {
            currentHealth += heal;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Player Died");
        isAlive = false;
    }
}
