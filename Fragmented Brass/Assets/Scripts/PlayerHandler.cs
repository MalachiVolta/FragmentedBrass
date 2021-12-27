using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour,IHealthSys
{
    int maxHealth = 100;
    int currentHealth;
    bool isAlive = true;
    public bool isBuilding = false;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth=maxHealth;
        Debug.Log("Health: "+currentHealth); 
        healthBar.SetMaxValue(maxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
           Damage(10);
           Debug.Log("Health: "+currentHealth);
           healthBar.SetValue(currentHealth);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            isBuilding = !isBuilding;
        }
    }

    public void Damage(int dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0) Die();
    }

    public void Heal(int heal)
    {
        if(isAlive)
        {
            currentHealth += heal;
            if(currentHealth>maxHealth) currentHealth=maxHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Player Died");
        isAlive=false;
    }
}
