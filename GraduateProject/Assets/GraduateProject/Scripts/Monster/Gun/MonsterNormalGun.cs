using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterNormalGun : Gun
{
    public MonsterNormalGun() : base()
    {
        maxMagazine = 12;
        currentMagazine = 12;
        ammo = Mathf.Infinity;
        pellets = 1;
        reloadTime = 5.0f;
        range = 20.0f;
        speed = 7.0f;
        rpm = 30;
        damage = 1.0f;
    }

    public override void interaction()
    {
        Debug.Log("MonsterNormalGun");
    }

    public override void action()
    {
        Debug.Log("MonsterNormalGunAction");

    }

    public override void shot(GameObject bullet)
    {
        if (currentMagazine < 0)
            return;

        MonsterNormalGunBullet bulletComp = bullet.GetComponent<MonsterNormalGunBullet>();
        //bulletComp.lookAtPos = bullet.transform.forward;
        //bulletComp.lookAtPos.y = bullet.transform.position.y;
        //bulletComp.Direction = bulletComp.lookAtPos - bullet.transform.position;
        bulletComp.speed = this.speed;
        bulletComp.range = this.range;
        bulletComp.IsPlay = true;
        bulletComp.Damage = damage;
        bullet.transform.LookAt(bulletComp.target.transform.position);


        currentMagazine -= 1;
    }

    public override void reload()
    {
        Debug.Log("NormalGunReload");

        ammo -= maxMagazine - currentMagazine;
        currentMagazine = maxMagazine;
    }
}
