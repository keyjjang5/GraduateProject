using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    protected PlayerController playerController;
    protected Item item;
    // Start is called before the first frame update
    protected void Start()
    {
        playerController = GameObject.Find("unitychan").GetComponent<PlayerController>();
        item = new Item();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            item.interaction();
        }
    }
}
