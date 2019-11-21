using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : ItemManager
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        item = new Weapon();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            base.OnTriggerStay(other);
            addWeapon(item as Weapon);
        }
    }

    protected virtual void addWeapon(Weapon weapon)
    {
        playerController.addWeapon(weapon);
    }
}
