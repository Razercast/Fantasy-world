using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : EnemyManager
{
    public Transform attackPos; //Точка откуда сферу для отлова создавать
    public override void attack()
    {
        Animator anim = getAnim();
        anim.SetTrigger("Attack");
    }

    public void Hit()
    {
        //Debug.Log("Twice test");
        Collider[] hitColliders = Physics.OverlapSphere(attackPos.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                hitCollider.SendMessage("getHit", damage);
            }
        }
        // Debug.Log("hit end");
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }
    public void FootR()
    {
        //Звук шагов
    }
    public void FootL()
    {
        //и тут тоже
    }
}
