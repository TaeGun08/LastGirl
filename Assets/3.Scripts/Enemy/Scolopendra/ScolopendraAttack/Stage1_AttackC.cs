using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_AttackC : EnemyAttack
{
    public override IEnumerator Pattern(Enemy enemy)
    {
        enemy.isPattern = true;
        yield return new WaitForSeconds(0.9f);
        enemy.attackColliders[1].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        enemy.attackColliders[0].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        enemy.attackColliders[0].SetActive(false);
        enemy.attackColliders[1].SetActive(false);
        enemy.isPattern = false;
    }
}
