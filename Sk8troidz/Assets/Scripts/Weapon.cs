using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject
{
    public string weapon_name;
    public string weapon_description;
    public float weapon_Speed;
    public int damage;
    public int max_ammo;
    public float knockback;
    public float range;
    public GameObject instance;
    //Add var for sound

    
    public GameObject impact_effect;
    public GameObject body;

    // Update is called once per frame
   public virtual void Shoot(GameObject parent) { }

    //public virtual void Deactivate(GameObject parent) { }
}
