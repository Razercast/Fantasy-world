using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 10;
    public int damage = 2;
    private PlayerInputSys _input;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _input = new PlayerInputSys();
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
        if(_input.Player.Attack.triggered)
        {
            Debug.Log("attack");
            _anim.SetTrigger("MeleeAttack");
        }
    }
}
