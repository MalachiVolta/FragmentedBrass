using UnityEngine;

public class Enemy : MonoBehaviour, ITargetable
{
    public GridBuildingSystem gridBuildingSystem;
    public GameObject Grid;

    [SerializeField] private int currentHealth = 50;

    void Start()
    {
        Grid = GameObject.FindGameObjectWithTag("Grid");
        gridBuildingSystem = Grid.GetComponent<GridBuildingSystem>();
    }

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
        gridBuildingSystem.ReceiveTurret(50);
        Destroy(gameObject);
    }
}
