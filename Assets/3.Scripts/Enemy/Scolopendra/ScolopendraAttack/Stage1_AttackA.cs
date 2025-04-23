using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_AttackA : EnemyAttack
{
    public override IEnumerator Pattern(Enemy enemy)
    {
        enemy.isPattern = true;
        yield return new WaitForSeconds(1.3f);
        enemy.attackColliders[0].SetActive(true);
        yield return new WaitForSeconds(0.7f);
        enemy.attackColliders[0].SetActive(false);
        enemy.isPattern = false;
    }
}
