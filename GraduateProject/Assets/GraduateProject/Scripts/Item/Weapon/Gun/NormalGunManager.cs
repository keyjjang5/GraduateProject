using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGunManager : GunManager
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        item = new NormalGun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
