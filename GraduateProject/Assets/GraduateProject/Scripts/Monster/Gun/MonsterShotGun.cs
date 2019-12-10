using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class MonsterShotGun : Gun
{
    public MonsterShotGun() : base()
    {
        maxMagazine = 3;
        currentMagazine = 3;
        ammo = 40;
        pellets = 5;
        reloadTime = 4.0f;
        range = 20.0f;
        speed = 10.0f;
        rpm = 15;
        damage = 1.0f;
    }

    public override void interaction()
    {
        Debug.Log("MonsterShotGun");
    }

    public override void action()
    {
        Debug.Log("MonsterShotGunAction");

    }

    public override void shot(GameObject bullet)
    {
        if (currentMagazine < 0)
            return;

        MonsterShotGunBullet bulletComp = bullet.GetComponent<MonsterShotGunBullet>();
        //bulletComp.lookAtPos = bullet.transform.forward;
        //bulletComp.lookAtPos.y = bullet.transform.position.y;
        //bulletComp.Direction = bulletComp.lookAtPos - bullet.transform.position;
        bulletComp.speed = this.speed;
        bulletComp.range = this.range;
        bulletComp.IsPlay = true;
        bulletComp.Damage = damage;
        bulletComp.Pellets = pellets;
        bulletComp.shot();
        //bullet.transform.LookAt(bulletComp.target.transform.position);


        currentMagazine -= 1;
    }

    public override void reload()
    {
        Debug.Log("MonsterShotGunReload");

        ammo -= maxMagazine - currentMagazine;
        currentMagazine = maxMagazine;
    }
}
