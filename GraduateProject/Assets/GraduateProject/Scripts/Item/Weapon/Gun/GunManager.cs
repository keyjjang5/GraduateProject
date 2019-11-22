using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : WeaponManager
{
    protected List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        item = new Gun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 총 생성
    protected virtual void createGun(string path)
    {
        Debug.Log("Create Gun");
        // 이미 장착중인 무기가 있으면 파괴하고 생성
        if (playerController.transform.GetChild(4).childCount > 0)
            Destroy(playerController.transform.GetChild(4).GetChild(0).gameObject);

        GameObject newWeapon = Instantiate(Resources.Load(path)) as GameObject;
        newWeapon.transform.SetParent(playerController.transform.GetChild(4));
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = newWeapon.transform.parent.localRotation;
    }

}
