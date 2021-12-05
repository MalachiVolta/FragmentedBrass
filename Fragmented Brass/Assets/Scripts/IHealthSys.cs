using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHealthSys
{
    void Damage(int dmgAm);
    void Heal(int healAm);

    void Die();
}
