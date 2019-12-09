using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunManager : GunManager
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        item = new ShotGun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void addWeapon(Weapon weapon)
    {
        base.addWeapon(weapon);

        //// 탄창만큼의 총알을 미리 생성해놓음
        //Gun gun = weapon as Gun;
        //for (int i = 0; i < gun.MaxMagazine; i++)
        //{
        //    GameObject newBullet = Instantiate(Resources.Load("ShotGunBullet") as GameObject);
        //    newBullet.transform.position = new Vector3(100.0f + i * 2.0f, 100.0f, 100.0f);
        //    bullets.Add(newBullet);
        //}
        //playerController.bullets = this.bullets;

        createGun("ShotGun");
    }

    // 총생성
    //protected override void createGun(string path)
    //{
    //    // 이미 장착중인 무기가 있으면 파괴하고 생성
    //    if (playerController.transform.GetChild(4).childCount > 0)
    //        Destroy(playerController.transform.GetChild(4).GetChild(0).gameObject);

    //    base.createGun(path);

    //    GameObject newWeapon = Instantiate(Resources.Load(path)) as GameObject;
    //    newWeapon.transform.SetParent(playerController.transform.GetChild(4));
    //    newWeapon.transform.localPosition = Vector3.zero;
    //    newWeapon.transform.localRotation = newWeapon.transform.parent.localRotation;
    //}
}
