using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public int damage = 1;
    [SerializeField] private float bulletSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(gameObjectRemove());
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidbody.velocity = transform.forward * bulletSpeed;
    }

    IEnumerator gameObjectRemove()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BulletTarget>()!= null)
        {
            other.SendMessage("getHit",damage);
        } else
        {
            Debug.Log("MISS");
        }
        Destroy(gameObject);
    }
}
