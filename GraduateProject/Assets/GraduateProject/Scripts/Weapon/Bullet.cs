using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 lookAtPos;
    public float speed;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        lookAtPos = GameObject.Find("LookAtPos").transform.position;
        speed = 5.0f;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5.0f)
            Destroy(gameObject);

        transform.position = Vector3.MoveTowards(transform.position, 
                                                transform.position + lookAtPos * 10,
                                                Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
