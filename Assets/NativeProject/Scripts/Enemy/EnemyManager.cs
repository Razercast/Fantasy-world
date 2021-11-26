using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int health = 4;
    public int damage = 1;
    private Animator _anim;
    public float attackRadius = 0.7f;
    private bool is_alive = true;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public Animator getAnim()
    {
        return _anim;
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
        Debug.Log("Test damage get");
        _anim.SetTrigger("hit");
        health = health - damage;
        //Получает урон
    }

    public virtual void attack ()
    {
        //do something in other class
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
