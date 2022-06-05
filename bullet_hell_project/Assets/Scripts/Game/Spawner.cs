using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Spawnable))]
public class Spawner : MonoBehaviour
{

    int waveCurrent;

    public int waveCount;
    public float spawnDelay;

    [Space]
    public int[] enemyCountFodder;
    public EnemyTypes[] enemyTypeFodder;
    public int[] enemyCountMain;
    public EnemyTypes[] enemyTypeMain;
    public int[] enemyCountBoss;
    public EnemyTypes[] enemyTypeBoss;

    Spawnable spawnable;

    public enum EnemyTypes { 
        none,
        splitter,
        emitter,
        pulser,
        warper
    };

    private void Start()
    {
        spawnable = GetComponent<Spawnable>();

        StartCoroutine("SpawnSequence");

    }

    IEnumerator SpawnSequence()
    {

        for (waveCurrent = 0; waveCurrent < waveCount; waveCurrent++)
        {
            yield return StartCoroutine("SpawnWave");
        }

    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemyCountFodder[waveCurrent]; i++)
        {

            SpawnEnemy(enemyTypeFodder[waveCurrent]);

            yield return new WaitForSeconds(spawnDelay);
        }

        for (int i = 0; i < enemyCountMain[waveCurrent]; i++)
        {
            SpawnEnemy(enemyTypeMain[waveCurrent]);

            yield return new WaitForSeconds(spawnDelay);
        }

        for (int i = 0; i < enemyCountBoss[waveCurrent]; i++)
        {
            SpawnEnemy(enemyTypeBoss[waveCurrent]);

            yield return new WaitForSeconds(spawnDelay);
        }

        while (CurrentEnemyCount() > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(spawnDelay);
    }

    int CurrentEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void SpawnEnemy(EnemyTypes in_type)
    {
        switch (in_type)
        {
            case EnemyTypes.emitter:
                SpawnEmitter();
                break;

            case EnemyTypes.splitter:
                SpawnSplitter();
                break;

            case EnemyTypes.pulser:
                SpawnPulser();
                break;

            case EnemyTypes.warper:
                SpawnWarper();
                break;
        }
    }

    void SpawnEmitter()
    {
        int rnd = UnityEngine.Random.Range(0, 2);

        float xSpawn = rnd == 0 ? -GameRules.screenWidth : GameRules.screenWidth;
        Vector3 v = new Vector3(xSpawn, UnityEngine.Random.Range(GameRules.screenHeight / 2, GameRules.screenHeight - 1), 0);

        Instantiate(spawnable.emitter, v, Quaternion.identity);
    }

    void SpawnSplitter()
    {
        float xSpawn = UnityEngine.Random.Range(-GameRules.screenWidth, GameRules.screenWidth);
        Vector3 v = new Vector3(xSpawn, GameRules.screenHeight, 0);

        Instantiate(spawnable.splitter, v, Quaternion.identity);
    }

    void SpawnPulser()
    {
        float xSpawn = UnityEngine.Random.Range(-GameRules.screenWidth / 2, GameRules.screenWidth / 2);
        Vector3 v = new Vector3(xSpawn, GameRules.screenHeight, 0);

        Instantiate(spawnable.pulser, v, Quaternion.identity);
    }

    void SpawnWarper()
    {
        int rnd = UnityEngine.Random.Range(0, 2);

        float xSpawn = rnd == 0 ? -GameRules.screenWidth : GameRules.screenWidth;
        Vector3 v = new Vector3(xSpawn, UnityEngine.Random.Range(GameRules.screenHeight / 2, GameRules.screenHeight), 0);

        Instantiate(spawnable.warper, v, Quaternion.identity);
    }
}
