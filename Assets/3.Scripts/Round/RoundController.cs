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
    private int curSpawnEnemy;
    private int curRound;
    
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
        if (gameManager.IsGameStart == false) return;
        if (roundSystem.IsRoundStart == false) return;
        roundSystem.IsRoundStart = false;
        roundSystem.RoundCanvas.RoundText.text = $"현재 라운드 : {curRound + 1}";
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

    private IEnumerator SpawnEnemiesCoroutine(Enemy[] enemyArr, int count, Queue<Enemy> enemyQueue)
    {
        if (enemyArr == null) yield break;
        Enemy[] enemies = enemyArr;
        Enemy temp = null;
        int spawnEnemyCount = 0;
        int maxSpawnEnemyCount = count;
        WaitForSeconds wait = new WaitForSeconds(0.25f);
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
                    new Vector3(Player.LocalPlayer.transform.position.x + Random.Range(-20f, 20f), 0f, 
                        Player.LocalPlayer.transform.position.z + Random.Range(-20f, 20f)), 
                    Quaternion.identity, transform).GetComponent<Enemy>());
            }

            spawnEnemyCount++;
            roundSystem.SpawnEnemyCount++;
            yield return wait;
        }
    }
}
