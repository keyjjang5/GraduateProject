using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        int num = gameObject.name[gameObject.name.Length-1] - 48;
        string name = "Waypoint End " + num;
        target = GameObject.Find(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("tlfgod");
            other.transform.position = target.transform.position + new Vector3(0.0f, 1.0f);

        }
    }
}
