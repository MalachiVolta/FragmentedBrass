using UnityEngine;

public class Enemy : MonoBehaviour, ITargetable
{
    public GridBuildingSystem gridBuildingSystem;
    public GameObject Grid;
    public GameHandler gameHandler;
    private AudioSource audioSource;
    public AudioClip hit;

    [SerializeField] private int currentHealth = 50;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Grid = GameObject.FindGameObjectWithTag("Grid");
        gridBuildingSystem = Grid.GetComponent<GridBuildingSystem>();
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }

    public void Hit(int amount)
    {
        AudioSource.PlayClipAtPoint(hit, transform.position);
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
        gameHandler.killedEnemies++;
    }

    public void SoundMech()
    {
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
    }
}
