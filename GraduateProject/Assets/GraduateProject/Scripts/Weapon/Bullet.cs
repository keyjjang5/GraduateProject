using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    public Vector3 lookAtPos;
    public float speed;
    private float timer;

    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        lookAtPos.y = transform.position.y;
        speed = 5.0f;
        timer = 0.0f;
        dir = lookAtPos - transform.position;
        dir = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5.0f)
            Destroy(gameObject);

        //transform.Translate((lookAtPos - transform.position) * Time.deltaTime * speed);
        
        transform.GetComponent<Rigidbody>().velocity = dir * speed;


        //transform.position = Vector3.MoveTowards(transform.position,
        //                                      lookAtPos * 10,
        //                                    Time.deltaTime * speed);


    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
