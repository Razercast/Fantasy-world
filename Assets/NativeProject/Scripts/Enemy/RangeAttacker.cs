using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttacker : EnemyManager
{
    public Transform arrowSpawn;
    public GameObject arrow;
    public override void attack()
    {
        Animator anim = getAnim();
        anim.SetTrigger("Attack");
    }

    void Shoot()
    {
        Vector3 aimDir = (PlayerHandler.instance.playerCameraRoot.transform.position - arrowSpawn.position).normalized;
        GameObject bullet = Instantiate(arrow, arrowSpawn.position, Quaternion.LookRotation(aimDir, Vector3.up));
        bullet.GetComponent<BulletProjectile>().damage = damage;
    }

    void FootR()
    {
        //Для звуков в будущем
    }
    void FootL()
    {
        //Для звуков в будущем
    }
}
