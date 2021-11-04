using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
   HealthSys healthSystem = new HealthSys(100);

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Health: "+healthSystem.getHealth());
        healthBar.SetMaxValue(healthSystem.getHealth());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
           healthSystem.Damage(10);
           Debug.Log("Health: "+healthSystem.getHealth());
           healthBar.SetValue(healthSystem.getHealth());
        }
    }
}
