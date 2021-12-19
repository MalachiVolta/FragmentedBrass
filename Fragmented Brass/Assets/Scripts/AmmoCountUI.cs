using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCountUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI tmp;
    public Weapon wpn;

    void Update()
    {
        int ammo = wpn.currentAmmo;
        
        if(ammo<10)
        {
            tmp.text = "0" + ammo.ToString();
        }
        else tmp.text = ammo.ToString();
    }
}
