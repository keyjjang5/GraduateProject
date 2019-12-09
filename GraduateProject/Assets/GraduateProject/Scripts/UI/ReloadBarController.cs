using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBarController : MonoBehaviour
{

    float reloadTime;
    public float ReloadTime
    {
        set { reloadTime = value; }
    }
    Scrollbar scrollbar;

    bool isReload = false;
    Gun gun;


    // Start is called before the first frame update
    void Start()
    {
        //reloadTime = 0.0f;
        scrollbar = gameObject.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReload == true)
        {
            scrollbar.value += Time.deltaTime / reloadTime;
            if (scrollbar.value >= 1.0f)
            {
                reloadEnd();
            }
        }
    }

    public void reload(Gun otherGun, float reloadTime)
    {
        isReload = true;
        gun = otherGun;
        this.reloadTime = reloadTime;
    }

    private void reloadEnd()
    {
        gun.reload();
        reloadTime = 0.0f;
        scrollbar.value = 0.0f;
        isReload = false;
        gameObject.SetActive(false);
    }
}
