using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{ 
    public Item()
    {
    }

    // E 키를 통해서 상호작용(습득, 사용 등)
    public virtual void interaction()
    {
        Debug.Log("Item");
    }
}
