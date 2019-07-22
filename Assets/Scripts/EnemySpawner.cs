using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public Wave[] waves;
    public Enemy enemy;
    Wave CurrentWave;
    int CurrentWaveNumber;
    int enemiesRemainingToSpawn;
    float nextSpawnTIme;
    int enemiesRemainingAlive;
    private void Start()
    {
        NextWave();
    }
    private void Update()
    {
        if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTIme)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTIme = Time.time + CurrentWave.TimeBetweenSpawn;
            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawnedEnemy.onDeath += onEnemeyDeath;
        }
    }
    void onEnemeyDeath()
    {
        enemiesRemainingAlive--;
        if(enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }
    void NextWave()
    {
        CurrentWaveNumber++;
        if(CurrentWaveNumber -1 < waves.Length)
        {
            CurrentWave = waves[CurrentWaveNumber - 1];
            enemiesRemainingToSpawn = CurrentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }
    [System.Serializable]
	public class Wave
    {
        public int enemyCount;
        public float TimeBetweenSpawn;
    }
}
