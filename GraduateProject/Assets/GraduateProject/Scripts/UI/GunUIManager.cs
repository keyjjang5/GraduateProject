using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum GunUI
{
    NormalGunUI,
    ShotGunUI
}


public class GunUIManager : MonoBehaviour
{
    List<GameObject> GunUI = new List<GameObject>();
    List<Slider> gunUISlider = new List<Slider>();
    int selectedUI;
    // Start is called before the first frame update
    void Start()
    {
        selectedUI = -1;
        GunUI.Add(Instantiate(Resources.Load("NormalGunUI") as GameObject));
        GunUI.Add(Instantiate(Resources.Load("ShotGunUI") as GameObject));

        for (int i = 0; i < GunUI.Count; i++)
        {
            GunUI[i].transform.SetParent(this.transform, false);
            gunUISlider.Add(GunUI[i].transform.GetChild(1).GetComponent<Slider>());
            offUI(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedUI == -1)
            return;
    }

    public void SelectGunUI(int num)
    {
        for (int i = 0; i < GunUI.Count; i++)
            offUI(i);

        onUI(num);
        selectedUI = num;
    }

    void offUI(int num)
    {
        GunUI[num].SetActive(false);
    }
    void onUI(int num)
    {
        GunUI[num].SetActive(true);
    }

    public void updateMagazine(float maxMagazine, float nowMagazine)
    {
        gunUISlider[selectedUI].value = nowMagazine / maxMagazine;
    }
}
