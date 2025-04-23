using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_AttackA : EnemyAttack
{
    public override IEnumerator Pattern(Enemy enemy)
    {
        enemy.isPattern = true;
        yield return new WaitForSeconds(5f);
        yield return new WaitForSeconds(0.5f);
        enemy.attackColliders[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemy.attackColliders[1].SetActive(false);
        yield return new WaitForSeconds(3.5f);
        enemy.isPattern = false;
    }
}
