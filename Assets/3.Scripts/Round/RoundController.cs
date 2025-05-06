using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundController : MonoBehaviour
{
    private GameManager gameManager;
    
    private RoundSystem roundSystem;
    
    [Header("RoundController Settings")] 
    [SerializeField] private RoundObject roundObject;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] roundEndPrefabs;
    private int curSpawnEnemy;
    [SerializeField] private int curRound;
    
    private Queue<Enemy> minionQueue = new Queue<Enemy>();
    private Queue<Enemy> eliteQueue = new Queue<Enemy>();
    private Queue<Enemy> subBossQueue = new Queue<Enemy>();
    private Queue<Enemy> lastBossQueue = new Queue<Enemy>();
    
    
    private void Start()
    {
        gameManager = GameManager.Instance;
        roundSystem =  RoundSystem.Instance;
    }

    private void Update()
    {
        if (gameManager.IsGameEnd) return;

        if (roundObject.Data.Length < curRound + 1 && roundSystem.SpawnEnemyCount <= 0)
        {
            gameManager.GameEnd(true);
            return;
        }
        
        if (gameManager.IsGameStart == false) return;
        if (roundSystem.IsRoundStart == false) return;
        roundSystem.IsRoundStart = false;
        roundEndPrefabs[0].gameObject.SetActive(false);
        roundEndPrefabs[1].gameObject.SetActive(false);
        roundEndPrefabs[2].gameObject.SetActive(false);
        
        roundSystem.RoundCanvas.RoundText.text = roundObject.Data.Length > curRound + 1 ? 
            $"현재 라운드 : {curRound + 1}" : $"현재 라운드 : 마지막 라운드";

        StartCoroutine(SpawnEnemiesCoroutine(roundObject.Data[curRound].Minion, 
            roundObject.Data[curRound].MaxSpawnMinion, minionQueue));
        
        StartCoroutine(SpawnEnemiesCoroutine(roundObject.Data[curRound].Elite,
            roundObject.Data[curRound].MaxSpawnElite, eliteQueue));
        
        StartCoroutine(SpawnEnemiesCoroutine(roundObject.Data[curRound].SubBoss, 
            roundObject.Data[curRound].MaxSpawnSubBoss, subBossQueue));
        
        StartCoroutine(SpawnEnemiesCoroutine(roundObject.Data[curRound].LastBoss, 
            roundObject.Data[curRound].MaxSpawnLastBoss, lastBossQueue));
        
        curRound++;
    }

    private void LateUpdate()
    {
        if (roundEndPrefabs[0].activeInHierarchy
            || roundSystem.SpawnEnemyCount > 0
            || curRound <= 0) return;
        roundEndPrefabs[0].gameObject.SetActive(true);
        //roundEndPrefabs[1].gameObject.SetActive(true);
        roundEndPrefabs[2].gameObject.SetActive(true);
    }
    
    private IEnumerator SpawnEnemiesCoroutine(Enemy[] enemyArr, int count, Queue<Enemy> enemyQueue)
    {
        if (enemyArr == null) yield break;
        Enemy[] enemies = enemyArr;
        Enemy temp = null;
        int spawnEnemyCount = 0;
        int maxSpawnEnemyCount = count;
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (spawnEnemyCount < maxSpawnEnemyCount)
        {
            bool check = false;
            
            if (enemyQueue.Count > 0)
            {
                for (int i = 0; i < enemyQueue.Count; i++)
                {
                    temp = enemyQueue.Dequeue();
                    if (temp.gameObject.activeInHierarchy == false)
                    {
                        temp.gameObject.SetActive(true);
                        temp.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        temp.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        enemyQueue.Enqueue(temp);
                        check = true;
                        break;
                    }
                    enemyQueue.Enqueue(temp);
                }
            }

            if (check == false)
            {
                enemyQueue.Enqueue(Instantiate(enemies[Random.Range(0, enemies.Length)].gameObject, 
                    spawnPoints[Random.Range(0, spawnPoints.Length)].position, 
                    Quaternion.identity, transform).GetComponent<Enemy>());
            }

            spawnEnemyCount++;
            roundSystem.SpawnEnemyCount++;
            yield return wait;
        }
    }
}
