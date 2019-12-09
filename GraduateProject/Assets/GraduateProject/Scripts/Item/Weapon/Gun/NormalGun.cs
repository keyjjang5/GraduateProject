using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NormalGun : Gun
{
    public NormalGun() : base()
    {
        maxMagazine = 12;
        currentMagazine = 12;
        ammo = Mathf.Infinity;
        pellets = 1;
        reloadTime = 2.0f;
        range = 20.0f;
        speed = 7.0f;
        rpm = 60;
        damage = 1.0f;
        GunUINum = (int)GunUI.NormalGunUI;
    }

    public override void interaction()
    {
        Debug.Log("NormalGun");
    }

    public override void action()
    {
        Debug.Log("NormalGunAction");
        
    }

    public override void shot(GameObject bullet)
    {
        if (currentMagazine < 0)
            return;

        Bullet bulletComp = bullet.GetComponent<Bullet>();
        bulletComp.lookAtPos = MousePointer.GetMousePos();
        bulletComp.lookAtPos.y = bullet.transform.position.y;
        bulletComp.Direction = bulletComp.lookAtPos - bullet.transform.position;
        bullet.transform.LookAt(bulletComp.lookAtPos);
        bulletComp.speed = this.speed;
        bulletComp.range = this.range;
        bulletComp.IsPlay = true;
        bulletComp.Damage = damage;

        currentMagazine -= 1;
    }

    public override void reload()
    {
        Debug.Log("NormalGunReload");

        ammo -= maxMagazine - currentMagazine;
        currentMagazine = maxMagazine;
    }
}
