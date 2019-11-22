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
        // Destroy가 불리기 전에 작업을 끝내기 때문에 이렇게 써도 괜찮음
        playerController.addWeapon(weapon);
        Destroy(gameObject);
    }
}
