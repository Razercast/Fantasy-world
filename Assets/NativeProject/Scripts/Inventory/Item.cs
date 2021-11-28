using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public string Identifier = "new";
    new public string name = "New Item Name";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        //Something
        Debug.Log("Using" + name);
    }
}
