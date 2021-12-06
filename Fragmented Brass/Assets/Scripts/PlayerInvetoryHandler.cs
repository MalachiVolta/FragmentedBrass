using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvetoryHandler : MonoBehaviour
{
    public int primaryAmmo;
    public int secondaryAmmo;
    public int scrap;
    public int walls;
    // Start is called before the first frame update
    void Start()
    {
        setDefaultValues();
    }
    
    void setDefaultValues()
    {
        primaryAmmo=300;
        secondaryAmmo=150;
        scrap=500;
        walls=5;
    }
}
