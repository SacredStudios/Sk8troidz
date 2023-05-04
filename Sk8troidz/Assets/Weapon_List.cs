using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_List : MonoBehaviour
{
    public List<Weapon> my_weapon_list;
    public List<Weapon> all_weapon_list;
    [SerializeField] Weapon_Selector ws;
    public Weapon weapon; //the current weapon

    public void AddWeapon(Weapon weapon)
    {
        my_weapon_list.Add(weapon);
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        weapon = GetCurrentWeapon();
        weapon = all_weapon_list[Random.Range(0,2)]; //be sure to comment this out
        ws.ChangeWeapon(weapon);
    }
    Weapon GetCurrentWeapon() //will be used on startup to get player's last used weapon
    {
        //will finish this with PlayerPrefs
        return null;
    }
}
