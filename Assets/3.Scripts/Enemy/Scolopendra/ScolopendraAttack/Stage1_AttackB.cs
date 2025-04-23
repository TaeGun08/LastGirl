using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_AttackB : EnemyAttack
{
    public override IEnumerator Pattern(Enemy enemy)
    {
        enemy.isPattern = true;
        yield return new WaitForSeconds(1.5f);
        enemy.attackColliders[2].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        enemy.attackColliders[2].SetActive(false);
        enemy.isPattern = false;
    }
}
