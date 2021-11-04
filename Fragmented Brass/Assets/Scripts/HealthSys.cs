using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSys
{
    private int health;
    private int maxHealth;

    public HealthSys(int health)
    {
        maxHealth = health;
        this.health = maxHealth;
    }

    public int getHealth(){
        return health;
    }

    public void Damage(int dmgAm)
    {
        health -= dmgAm;
        if(health < 0) health = 0;
    }

    public void Heal(int healAm)
    {
        health += healAm;
        if (health > maxHealth) health = maxHealth;
    }

}
