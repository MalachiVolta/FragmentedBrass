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
    public ChangeCrosshair changeCrosshair;

    public Transform spawnerPoint;
    public GridBuildingSystem gridBuildingSystem;
    [SerializeField] TextMeshProUGUI WaveText;
    [SerializeField] TextMeshProUGUI TimerText;
    private GameObject[] gameObjects;
    public float timeBetweenWaves = 45f;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float countdown = 60f;
    private int waveNumber = 5;
    private int CurrentWave = 1;
    private float victoryTime = 10f;
    private bool gameOver = false;
    public bool isMidWave = true;
    private int enemyCount = 0;
    private int currentEnemyCount = 0;
    private bool allHaveSpawned = false;
    public int killedEnemies = 0;
    private bool isReady = false;

    private void Start()
    {
        sceneHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
    }

    void Update()
    {

        if (!gameOver)
        {
            if (isMidWave)
                WaveText.text = "CURRENT WAVE: " + CurrentWave.ToString() + "\n" + "NEXT WAVE IN: " + (int)countdown;
            else
                WaveText.text = "CURRENT WAVE: " + CurrentWave.ToString() + "\n" + "ENEMIES LEFT: " + ((int)enemyCount - (int)killedEnemies).ToString();

            if (Input.GetKeyDown(KeyCode.F5) && isMidWave && !isReady)
            {
                countdown = 3f;
            }

            if ((timer % 60) < 10)
            {
                TimerText.text = "TIMER: \n" + (int)timer / 60 + ":0" + (int)timer % 60;
            }
            else
            {
                TimerText.text = "TIMER: \n" + (int)timer / 60 + ":" + (int)timer % 60;
            }

            if (currentEnemyCount == enemyCount)
                allHaveSpawned = true;

            if (countdown <= 0f)
            {
                allHaveSpawned = false;
                killedEnemies = 0;
                enemyCount = 0;
                currentEnemyCount = 0;
                isMidWave = false;
                StartCoroutine(SpawnWave());
                CurrentWave++;
                gridBuildingSystem.ReceiveWall(3);
                countdown = timeBetweenWaves;
                changeCrosshair.isFinished = false;
                return;
            }

            if (!isMidWave && allHaveSpawned && killedEnemies == enemyCount)
            {
                isMidWave = true;
                changeCrosshair.isFinished = false;
            }

            if (isMidWave)
            {
                countdown -= Time.deltaTime;
            }

            if (!isMidWave)
            {
                timer += Time.deltaTime;
            }


            if (CurrentWave == 9)
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
        enemyCount = waveNumber;
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
        currentEnemyCount++;
    }

    void Win()
    {
        gameOver = true;
        countdown = 1000f;
        target.SetHealth(100000);
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        StopAllCoroutines();

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
