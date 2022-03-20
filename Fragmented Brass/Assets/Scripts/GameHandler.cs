using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnerPoint;
    public GridBuildingSystem gridBuildingSystem;

    public float timeBetweenWaves = 15f;
    private float countdown = 30f;
    private int waveNumber = 4;
    private int CurrentWave = 1;
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            CurrentWave++;
            gridBuildingSystem.ReceiveWall(2);
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        Debug.Log("Wave Incoming!");

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnerPoint.position, spawnerPoint.rotation);
    }

}
