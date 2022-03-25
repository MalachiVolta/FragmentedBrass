using UnityEngine;
using TMPro;

public class Target : MonoBehaviour, ITargetable
{
    [SerializeField] private int currentHealth = 50;
    public TextMeshProUGUI healthText;
    public GameHandler handler;
    public HealthBar healthBar;
    public void Hit(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        handler.Lose();
    }

    public void SetHealth(int amount)
    {
        currentHealth = amount;
    }

    void Update()
    {
        healthText.text = currentHealth.ToString();
        healthBar.SetValue(currentHealth);
    }

    void Start()
    {
        healthBar.SetMaxValue(currentHealth);
        healthBar.SetValue(currentHealth);
        healthText.text = currentHealth.ToString();
    }
}
