using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisSlamTakeDown : EnemyAttack
{
    public override IEnumerator Pattern(Enemy enemy)
    {
        float attackTimer = 0f;
        enemy.isPattern = true;
        while (attackTimer > 5f)
        {
            enemy.attackColliders[0].SetActive(true);
            enemy.attackColliders[1].SetActive(true);
            attackTimer += Time.deltaTime;
            yield return false;
        }
        enemy.isPattern = false;
    }
}
