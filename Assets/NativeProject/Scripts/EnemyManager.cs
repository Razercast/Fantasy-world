using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int health = 4;
    public int damage = 1;
    private Animator _anim;
    public Transform attackPos; //Точка откуда сферу для отлова создавать
    public float attackRadius = 0.7f;
    private bool is_alive = true;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && is_alive)
        {
            is_alive = false;
            die();
        }
    }

    void getHit(int damage)
    {
        health = health - damage;
        //Получает урон
    }

    public void attack()
    {
        _anim.SetTrigger("MeleeAttack");
    }

    public void Hit()
    {
        //Debug.Log("Twice test");
        Collider[] hitColliders = Physics.OverlapSphere(attackPos.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag=="Player")
            {
                hitCollider.SendMessage("getHit",damage);
            }
        }
        // Debug.Log("hit end");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPos.position, 0.7f);
    }

    void die()
    {
        gameObject.GetComponent<EnemyAI>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        _anim.SetTrigger("die");
        StartCoroutine(DestroyObject());
        //умереть
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }


}
