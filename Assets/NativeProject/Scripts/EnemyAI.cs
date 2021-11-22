using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyManager))]
public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 10f;

    private Transform target;
    private NavMeshAgent agent;
    private Animator _anim;
    private EnemyManager _enemyManager;

    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerHandler.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _enemyManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            _anim.SetFloat("Speed",3f);
            _anim.SetFloat("MotionSpeed", 1);
            agent.SetDestination(target.position);
            if(distance<= agent.stoppingDistance)
            {
                Attack();
                FaceTarget();
                _anim.SetFloat("Speed", 0f);
            }
        } else
        {
            _anim.SetFloat("Speed", 0f);
            _anim.SetFloat("MotionSpeed", 0);
        }
    }

    void Attack()
    {
        if (canAttack)
        {
            _enemyManager.attack();
            canAttack = false;
            StartCoroutine(delayAttack());
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != new Vector3(0, 0, 0)) {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    IEnumerator delayAttack()
    {
        yield return new WaitForSeconds(2);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
