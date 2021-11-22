using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public static PlayerHandler instance;
    #region
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;
}
