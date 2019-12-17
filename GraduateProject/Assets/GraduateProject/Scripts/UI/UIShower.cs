using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIShower : MonoBehaviour
{
    public Scrollbar elementalScrollbar;
    public Camera camera;
    private Transform target;


    // Use this for initialization

    void Start()
    {
        target = GetComponent<Transform>();
    }



    // Update is called once per frame

    void Update()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(target.position);
        screenPos.y += 100.0f;
        float x = screenPos.x;

        elementalScrollbar.transform.position = new Vector3(x, screenPos.y, elementalScrollbar.transform.position.z);
    }
}
