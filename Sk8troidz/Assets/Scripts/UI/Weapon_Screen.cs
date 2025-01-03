using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Screen : MonoBehaviour
{
    [SerializeField] GameObject weapon_btn;
    [SerializeField] GameObject weapon_panel;
    public int j;
    void Start()
    {
       
        GameObject weapon_list = GameObject.Find("WeaponList");
        foreach (Weapon w in weapon_list.GetComponent<Weapon_List>().my_weapon_list)
        {
                GameObject btn = Instantiate(weapon_btn, weapon_panel.transform);
                btn.SetActive(true);
                btn.GetComponent<Weapon_Btn>().weapon = w;
                btn.GetComponent<Weapon_Btn>().list = weapon_list.GetComponent<Weapon_List>();
                btn.GetComponentInChildren<RawImage>().texture = w.icon;
                weapon_panel.transform.position = new Vector3(weapon_panel.transform.position.x, btn.transform.position.y, weapon_panel.transform.position.z);
            
        }
    }

    

}
