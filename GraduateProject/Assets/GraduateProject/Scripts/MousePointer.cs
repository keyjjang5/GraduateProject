using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("unitychan");    
    }

    // Update is called once per frame
    void Update()
    {
        lookAtMouse();
    }

    void lookAtMouse()
    {
        Vector3 lookTarget = GetMousePos();
        lookTarget.y = transform.position.y;

        if (Vector3.Distance(player.transform.position, lookTarget) < 5.0f)
            transform.position = lookTarget;
        else
        {
            transform.position = player.transform.position + Vector3.Normalize(lookTarget - player.transform.position) * 5;
        }
    }

    public static Vector3 GetMousePos()
    {
        Vector3 lookTarget = new Vector3(0, 0, 0);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lookTarget = hit.point;
        }

        return lookTarget;
    }
}
