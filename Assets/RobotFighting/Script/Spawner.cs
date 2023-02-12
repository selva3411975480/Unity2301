using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawner系统
/// </summary>
namespace RobotFighting
{
public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    private Wave currentWave;
    private int currentWaveNumber;

    private int enemiesRemainingToSpawn;
    private int enemiesRemainingAlive;
    private float nextSpawntime;

    private void Start()
    {
        NextWave();
    }
    private void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawntime)
        {
            enemiesRemainingToSpawn--;
            nextSpawntime = Time.time + currentWave.timeBetWeenSpawns;

            Enemy spwnedEnemy = Instantiate(enemy, Vector3.zero,Quaternion.identity) as Enemy;
            spwnedEnemy.OnDeath += OnEnableDeath;
        }
    }
    private void OnEnableDeath()
    {
        //print("Enemy died");检测事件系统是否在工作
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive==0)
        {
            NextWave();
        }
    }
    void NextWave()
    {
        currentWaveNumber++;
        print("Wave"+currentWaveNumber);//当前波数
        if (currentWaveNumber -1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;

        }
    }
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetWeenSpawns;
    }
}
}