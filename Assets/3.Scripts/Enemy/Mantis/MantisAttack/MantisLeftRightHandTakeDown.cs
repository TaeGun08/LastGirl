using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisLeftRightHandTakeDown : EnemyAttack
{
    public override IEnumerator Pattern(Enemy enemy)
    {
        enemy.isPattern = true;
        yield return new WaitForSeconds(1.6f);
        enemy.attackColliders[0].SetActive(true);
        yield return new WaitForSeconds(0.8f);
        enemy.attackColliders[0].SetActive(false);
        enemy.isPattern = false;
    }
}