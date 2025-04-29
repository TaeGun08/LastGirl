using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_AttackB : EnemyAttack
{
    public override IEnumerator Pattern(Enemy enemy)
    {
        enemy.isPattern = true;
        yield return new WaitForSeconds(3.5f);
        playerCam.ShakeCamera(1.5f);
        yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(0.5f);
        enemy.attackColliders[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        enemy.attackColliders[0].SetActive(false);
        yield return new WaitForSeconds(3.5f);
        enemy.isPattern = false;
    }
}
