using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnAttackers : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 2f;
    public float waveDelay = 5f;
    public List<List<Vector3>> riverPaths = new List<List<Vector3>>();

    public int enemiesPerWave = 5;
    public bool spawning = true;

    private int currentWave = 0;
    private int currentPath = 0;

    public TMP_Text waveText;

    public void Start()
    {
        UpdateWaveText();
        StartCoroutine(WaveManager());
    }

    public void SetRiverPaths(List<Vector3> path1, List<Vector3> path2, List<Vector3> path3)
    {
        AdjustYpos(path1);
        AdjustYpos(path2);
        AdjustYpos(path3);

        riverPaths.Add(path1);
        riverPaths.Add(path2);
        riverPaths.Add(path3);
    }

    public void AdjustYpos(List<Vector3> path)
    {
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 adjustedPosition = path[i];
            adjustedPosition.y = 1f;
            path[i] = adjustedPosition;
        }
    }

    IEnumerator WaveManager()
    {
        while (spawning && currentWave < 3)
        {
            yield return StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(waveDelay);
            currentWave++;
            UpdateWaveText();
        }
    }

    IEnumerator SpawnWave()
    {
        int enemiesToSpawn = enemiesPerWave + (currentWave * 2);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (riverPaths.Count > 0)
        {
            GameObject enemyPrefab = enemyPrefabs[currentWave];

            Vector3 spawnPoint = riverPaths[currentPath][0];
            spawnPoint.y = 1f;

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

            Attackers enemyScript = enemy.GetComponent<Attackers>();
            enemyScript.Path(riverPaths[currentPath]);

            currentPath = (currentPath + 1) % riverPaths.Count;
        }
    }

    void UpdateWaveText()
    {
        waveText.text = "Wave: " + (currentWave + 1).ToString();
    }
}