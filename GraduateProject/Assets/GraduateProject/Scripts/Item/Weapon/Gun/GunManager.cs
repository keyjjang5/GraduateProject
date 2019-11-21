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
}
