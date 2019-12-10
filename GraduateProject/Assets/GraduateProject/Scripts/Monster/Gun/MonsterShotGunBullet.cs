using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShotGunBullet : MonsterNormalGunBullet
{
    protected int pellets;
    public int Pellets
    {
        get { return pellets; }
        set { pellets = value; }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        timer = 0.0f;

        magazinePos = transform.position;


    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!isPlay)
            return;

        timer += Time.deltaTime;
        if (timer >= range / speed)
        {
            returnMagazine();
            return;
        }
    }

    private void returnMagazine()
    {
        isPlay = false;
        transform.position = magazinePos;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        timer = 0.0f;
    }

    public void shot()
    {
        for (int i = 0; i < pellets; i++)
        {
            MonsterNormalGunBullet bulletComp = transform.GetChild(i).GetComponent<MonsterNormalGunBullet>();
            bulletComp.lookAtPos = lookAtPos;
            bulletComp.Direction = dir;
            bulletComp.speed = this.speed;
            bulletComp.range = this.range;
            bulletComp.IsPlay = true;
            bulletComp.Damage = damage;

            bulletComp.Direction = Quaternion.Euler(0, -10, 0) * bulletComp.Direction;
            bulletComp.Direction = Quaternion.Euler(0, 5 * i, 0) * bulletComp.Direction;
        }
    }
}
