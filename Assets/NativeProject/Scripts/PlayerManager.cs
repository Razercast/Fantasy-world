using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 10;
    public int damage = 2;
    private PlayerInputSys _input;
    private Animator _anim;
    public Transform weapon;
    private bool is_alive = true;

    public GameObject inventoryUI;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _input = new PlayerInputSys();

        //inventory = new Inventory();
        // uiInventory.SetInventory(inventory);

        //ItemWorld.SpawnItemWorld(new Vector3(0, 0, 0), new Item {itemType = Item.ItemType.Sword,amount=1 });

    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Триггер атаки
        if(_input.Player.Attack.triggered)
        {
            _anim.SetTrigger("MeleeAttack");
        }


        if(_input.Player.OpenInventory.triggered)
        {
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
            } else
            {
                inventoryUI.SetActive(true);
            }
        }
    }
    //Обработчик на событие удара
    void Hit()
    {
        float radius = 0.9f;
        Collider[] hitColliders = Physics.OverlapSphere(weapon.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                hitCollider.SendMessage("getHit", damage);
            }

        }
    }
    //Обработчик получения удара
    void getHit(int damage)
    {
        if(is_alive==false) { return; }
        _anim.SetTrigger("hit");
        health = health - damage;
        //Проверка здоровья
        if (health <= 0 && is_alive == true)
        {
            is_alive = false;
            die();
        }
    }
    //Обработчик если здоровье упало ниже 0
    void die()
    {
        _anim.SetTrigger("die");
        //Деактивиация контроля хотя каким боком SendMessage работает хз
        gameObject.GetComponent<StarterAssets.ThirdPersonController>().enabled = false;
        gameObject.GetComponent<PlayerManager>().enabled = false;

    }

    private void OnDrawGizmos()
    {
        //Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        //Gizmos.DrawSphere(weapon.position,0.9f);
    }


}
