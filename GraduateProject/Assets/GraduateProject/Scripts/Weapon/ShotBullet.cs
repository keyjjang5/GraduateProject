using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    public GameObject gun;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        gun = gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.SetParent(gun.transform);
            newBullet.transform.position = gun.transform.position;
        }
    }
}
