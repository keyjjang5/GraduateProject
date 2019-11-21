using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Item
{
    public Weapon() : base()
    {

    }

    public override void interaction()
    {
        Debug.Log("Weapon");
    }

    public virtual void action()
    {
        Debug.Log("WeaponAction");
    }
}
