using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public ProgressBar progressBar;
    public void SetMaxValue(int health)
    {
        progressBar.maximum=health;
    }

    public void SetValue(int health)
    {
        progressBar.current=health;
    }

} 
