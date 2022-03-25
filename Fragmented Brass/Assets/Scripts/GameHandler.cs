using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Target target;
    public GameObject canvas;
    public GameObject victoryCanvas;
    public TextMeshProUGUI victoryText;
    public SceneHandler sceneHandler;

    public Transform spawnerPoint;
    public GridBuildingSystem gridBuildingSystem;
    [SerializeField] TextMeshProUGUI WaveText;
    [SerializeField] TextMeshProUGUI TimerText;
    private GameObject[] gameObjects;
    public float timeBetweenWaves = 30f;
    [SerializeField] private float timer = 660f;
    [SerializeField] private float countdown = 60f;
    private int waveNumber = 4;
    private int CurrentWave = 1;
    private float victoryTime = 10f;
    private bool gameOver = false;

    private void Start()
    {
        sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
    }
    void Update()
    {
        if (!gameOver)
        {
            WaveText.text = "CURRENT WAVE: " + CurrentWave.ToString() + "\n" + "NEXT WAVE IN: " + (int)countdown;
            if ((timer % 60) < 10)
            {
                TimerText.text = "TIMER: \n" + (int)timer / 60 + ":0" + (int)timer % 60;
            }
            else
            {
                TimerText.text = "TIMER: \n" + (int)timer / 60 + ":" + (int)timer % 60;
            }

            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                CurrentWave++;
                gridBuildingSystem.ReceiveWall(5);
                countdown = timeBetweenWaves;
                return;
            }

            countdown -= Time.deltaTime;
            timer -= Time.deltaTime;


            if (timer <= 0)
            {
                Win();
            }
        }
        else
        {
            victoryTime -= Time.deltaTime;

            if (victoryTime <= 0)
                sceneHandler.LoadMenu();
        }
    }

    IEnumerator SpawnWave()
    {
        waveNumber *= 2;
        Debug.Log("Wave Incoming!");

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnerPoint.position, spawnerPoint.rotation);
    }

    void Win()
    {
        gameOver = true;
        countdown = 1000f;
        target.SetHealth(100000);
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
        canvas.SetActive(false);
        victoryCanvas.SetActive(true);
    }

    public void Lose()
    {
        gameOver = true;
        canvas.SetActive(false);
        victoryCanvas.SetActive(true);
        victoryText.text = "YOU LOST!";
    }

}
