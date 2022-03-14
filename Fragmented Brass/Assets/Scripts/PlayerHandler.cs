using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IHealthSys
{
    public Animator animator;
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
        Debug.Log("Health: " + currentHealth);
        healthBar.SetMaxValue(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !Input.GetKeyDown(KeyCode.Mouse1) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|GunIdle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|IdleRadio")))
        {
            isBuilding = !isBuilding;
            if (isBuilding)
            {
                animator.SetTrigger("EquipRadio");
            }
            else
            {
                animator.SetTrigger("EquipGun");
            }
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
