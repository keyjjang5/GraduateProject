using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class MonsterNormalGunBullet : MonoBehaviour
{
    public Vector3 lookAtPos;
    public GameObject target;
    public float speed;
    public float range;
    protected float timer;

    protected float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    protected Vector3 dir;
    public Vector3 Direction
    {
        get { return dir; }
        set { dir = value.normalized; }
    }
    protected Vector3 magazinePos;
    public Vector3 MagazinePos
    {
        get { return MagazinePos; }
        set { MagazinePos = value; }
    }

    protected bool isPlay = false;
    public bool IsPlay
    {
        get { return isPlay; }
        set { isPlay = value; }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        timer = 0.0f;

        setMagazinePos(transform.position);

        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isPlay)
            return;

        timer += Time.deltaTime;
        if (timer >= range / speed)
        {
            returnMagazine();
            return;
        }

        //transform.Translate((lookAtPos - transform.position) * Time.deltaTime * speed);

        transform.GetComponent<Rigidbody>().velocity = dir * speed;


        //transform.position = Vector3.MoveTowards(transform.position,
        //                                      lookAtPos * 10,
        //                                    Time.deltaTime * speed);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bullet")
            return;
        if (other.transform.tag == "Monster")
            return;
        if (other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().hited(0.5f);
            Debug.Log("TriggerHit");
        }// 플레이어 체력을 데미지 만큼 깎음

        returnMagazine();
    }

    // 총알 저장소로 돌아감
    private void returnMagazine()
    {
        isPlay = false;
        transform.position = magazinePos;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        timer = 0.0f;
    }

    public void setMagazinePos(Vector3 position)
    {
        magazinePos = position;
    }
}
