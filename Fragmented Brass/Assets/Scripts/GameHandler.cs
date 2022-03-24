using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform spawnerPoint;
    public GridBuildingSystem gridBuildingSystem;
    [SerializeField] TextMeshProUGUI WaveText;
    [SerializeField] TextMeshProUGUI TimerText;
    public float timeBetweenWaves = 30f;
    private float timer = 660f;
    private float countdown = 60f;
    private int waveNumber = 4;
    private int CurrentWave = 1;
    void Update()
    {
        WaveText.text = "CURRENT WAVE: " + CurrentWave.ToString() + "\n" + "NEXT WAVE IN: " + (int)countdown;
        TimerText.text = "TIMER: \n" + (int)timer / 60 + ":" + (int)timer % 60;
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            CurrentWave++;
            gridBuildingSystem.ReceiveWall(10);
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
        countdown = 1000f;
    }

}
