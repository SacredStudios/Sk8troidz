using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_List : MonoBehaviour
{
    public List<Weapon> my_weapon_list;
    public List<Weapon> all_weapon_list;
    public Weapon curr_weapon; //the current weapon
    [SerializeField] GameObject sk8troid_menu;

    public void AddWeapon(Weapon weapon)
    {
        my_weapon_list.Add(weapon);
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        sk8troid_menu = GameObject.Find("Sk8troid(MENUVERSION)");
        curr_weapon = my_weapon_list[0];
        //curr_weapon = all_weapon_list[Random.Range(0,2)]; //be sure to comment this out
        ChangeWeapon(curr_weapon);
    }
    Weapon GetCurrentWeapon() //will be used on startup to get player's last used weapon
    {
        //will finish this with PlayerPrefs
        return curr_weapon;
        


    }
    public void ChangeWeapon(Weapon weapon)
    {
        curr_weapon = weapon;
        sk8troid_menu.GetComponent<Clothes_Dummy>().ChangeWeapon(weapon);
        Debug.Log(curr_weapon.weapon_description);
    }
}
