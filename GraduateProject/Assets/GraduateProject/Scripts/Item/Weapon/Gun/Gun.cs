using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun : Weapon
{
    protected int maxMagazine;      // 탄창
    public int MaxMagazine
    {
        get { return maxMagazine; }
    }
    protected int currentMagazine;  // 남은 탄창
    public int CurrentMagazine
    {
        get { return currentMagazine; }
    }
    protected float ammo;              // 탄약
    protected int pellets;      // 한발에 사출되는 총알의 수
    protected float reloadTime; // 재장전 시간
    public float ReloadTime
    {
        get { return reloadTime; }
    }
    protected float range;      // 사정거리
    protected float speed;      // 탄속
    protected float rpm;        // 연사속도(Round Per Minute)
    public float Rpm
    {
        get { return rpm; }
    }
    protected float damage;     // 공격력
    public float Damage
    {
        get { return damage; }
    }

    public Gun() : base()
    {

    }

    public override void interaction()
    {
        Debug.Log("Gun");
    }

    public override void action()
    {
        Debug.Log("GunAction");
    }

    public virtual void shot(GameObject bullet)
    {
        Debug.Log("GunShot");
    }

    public virtual void reload()
    {
        Debug.Log("GunReload");
    }
}
