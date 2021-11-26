using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 10f;

    private Transform target;
    private Transform targetRoot;
    private NavMeshAgent agent;
    private Animator _anim;
    private EnemyManager _enemyManager;
    public Transform unitHead;
    //public LayerMask layerMask;

    public enum AttackMode
    {
        melee = 0,
        range = 1,
    }
    public AttackMode attackMode;


    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerHandler.instance.player.transform;
        targetRoot = PlayerHandler.instance.playerCameraRoot.transform;
        agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _enemyManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode==AttackMode.melee)
        {
            meleeLogic();
        } else
        {
            rangeLogic();
        }
    }

    //AI для милишника
    void meleeLogic()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            _anim.SetFloat("Speed", 3f);
            _anim.SetFloat("MotionSpeed", 1);
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                Attack();
                _anim.SetFloat("Speed", 0f);
            }
        }
        else
        {
            _anim.SetFloat("Speed", 0f);
            _anim.SetFloat("MotionSpeed", 0);
        }
    }
    //AI для дальника
    void rangeLogic()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            if (Physics.Linecast(unitHead.position, targetRoot.position))
            {
                _anim.SetFloat("Speed", 3f);
                _anim.SetFloat("MotionSpeed", 1);
                agent.SetDestination(target.position);
            } else
            {
                agent.SetDestination(transform.position);
                FaceTarget();
                Attack();
                _anim.SetFloat("Speed", 0f);
            }
            


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
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
