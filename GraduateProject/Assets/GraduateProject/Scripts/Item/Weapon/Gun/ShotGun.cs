using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public ShotGun() : base()
    {
        maxMagazine = 2;
        currentMagazine = 2;
        ammo = 40;
        pellets = 5;
        reloadTime = 3.5f;
        range = 10.0f;
        speed = 20.0f;
        rpm = 10;
        damage = 1.0f;
    }

    public override void interaction()
    {
        Debug.Log("ShotGun");
    }

    public override void action()
    {
        Debug.Log("ShotGunAction");

    }

    public override void shot(GameObject bullet)
    {
        if (currentMagazine < 0)
            return;

        ShotGunBullet bulletComp = bullet.GetComponent<ShotGunBullet>();
        bulletComp.lookAtPos = MousePointer.GetMousePos();
        bulletComp.lookAtPos.y = bullet.transform.position.y;
        bulletComp.Direction = bulletComp.lookAtPos - bullet.transform.position;
        bulletComp.speed = this.speed;
        bulletComp.range = this.range;
        bulletComp.IsPlay = true;
        bulletComp.Damage = damage;
        bulletComp.Pellets = pellets;
        bulletComp.shot();

        currentMagazine -= 1;
    }

    public override void reload()
    {
        Debug.Log("ShotGunReload");

        ammo -= maxMagazine - currentMagazine;
        currentMagazine = maxMagazine;
    }
}
