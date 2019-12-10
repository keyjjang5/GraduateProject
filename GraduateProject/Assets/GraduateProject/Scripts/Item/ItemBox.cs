using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    GameObject shotgunDrop;
    readonly int m_HashOpen = Animator.StringToHash("open");
    protected Animator m_Animator;              

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();

        shotgunDrop = Resources.Load("ShotGunDrop") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Animator.SetTrigger(m_HashOpen);
            Invoke("createItem", 4.0f);
            
        }
    }

    void createItem()
    {
        GameObject newDrop = Instantiate(shotgunDrop);
        newDrop.transform.position = transform.position + new Vector3(0,-2.0f);

        Destroy(gameObject);
    }
}
