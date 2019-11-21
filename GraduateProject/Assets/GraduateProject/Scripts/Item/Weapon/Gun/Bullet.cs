using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    public Vector3 lookAtPos;
    public float speed;
    public float range;
    private float timer;

    private Vector3 dir;
    public Vector3 Direction
    {
        get { return dir; }
        set { dir = value.normalized; }
    }
    private Vector3 magazinePos;
    public Vector3 MagazinePos
    {
        get { return MagazinePos; }
        set { MagazinePos = value; }
    }

    private bool isPlay = false;
    public bool IsPlay
    {
        get { return isPlay; }
        set { isPlay = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        magazinePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlay)
            return;

        timer += Time.deltaTime;
        if (timer >= 5.0f)
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

    private void OnCollisionEnter(Collision collision)
    {
        returnMagazine();
    }

    private void returnMagazine()
    {
        isPlay = false;
        transform.position = magazinePos;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        timer = 0.0f;
    }
}
