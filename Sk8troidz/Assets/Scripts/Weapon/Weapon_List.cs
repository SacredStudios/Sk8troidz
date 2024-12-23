using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_List : MonoBehaviour
{
    public List<Weapon> my_weapon_list;
    public List<Weapon> all_weapon_list;
    public List<Weapon> ai_weapon_list;
    public Weapon curr_weapon; //the current weapon
    public GameObject sk8troid_menu;

    public GameObject weapon_name;
    public GameObject description;
    public GameObject damage_slider;
    public GameObject range_slider;
    public GameObject speed_slider;
    public GameObject attackcost_slider;

    public void AddWeapon(Weapon weapon)
    {
        my_weapon_list.Add(weapon);
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        sk8troid_menu = GameObject.Find("Sk8troid(MENUVERSION)");

        curr_weapon = my_weapon_list[0];
        if(PlayerPrefs.HasKey("weapon"))
        {
            LoadWeapon(PlayerPrefs.GetString("weapon"));
        }
        
        //curr_weapon = all_weapon_list[Random.Range(0,2)]; //be sure to comment this out
        ChangeWeapon(curr_weapon);
    }
    public void LoadWeapon(string name)
    {
        foreach (Weapon weapon in my_weapon_list) //would be better if I used a hashmap
        {

            if (weapon.name == name)
            {
                ChangeWeapon(weapon);
            }
        }

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
        weapon_name.GetComponent<Text>().text = weapon.weapon_name;
        description.GetComponent<Text>().text = weapon.weapon_description;
        damage_slider.GetComponent<Slider>().value = weapon.damage;
        range_slider.GetComponent<Slider>().value = weapon.range;
        speed_slider.GetComponent<Slider>().value = Mathf.Round((1 / weapon.weapon_delay)*10.0f)*0.1f; ; //this displays value to 10th decimal point
        attackcost_slider.GetComponent<Slider>().value = weapon.attack_cost;
        PlayerPrefs.SetString("weapon", weapon.name);
        PlayerPrefs.Save();
    }
}
