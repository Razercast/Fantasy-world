using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttacker : EnemyManager
{
    public Transform arrowSpawn;
    public override void attack()
    {
        Animator anim = getAnim();
        anim.SetTrigger("Attack");
    }

    void Shoot()
    {

        Debug.Log("Shot");
    }

}
