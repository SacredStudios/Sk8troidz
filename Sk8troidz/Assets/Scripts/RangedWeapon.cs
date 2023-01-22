using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new_weapon", menuName = "Scripts/RangedWeapon", order = 1)]
public class RangedWeapon : Weapon
{
    public GameObject particle_trail;
    
    public float range;
    

   // MAKE GAME LIKE NSMBDS MULTIPLAYER

    public override void Shoot(GameObject parent)
    {
        Debug.DrawRay(parent.transform.position, spine.transform.forward * range, Color.green);
        Ray ray = new Ray(parent.transform.position, spine.transform.forward * range);
        RaycastHit hit = new RaycastHit();
        Instantiate(particle_trail, particle_pos.transform.position, particle_pos.transform.rotation, particle_pos.transform);
       // Instantiate(particle_trail2, particle_pos.transform.position, particle_pos.transform.rotation, particle_pos.transform);
        if (Physics.Raycast(ray, out hit)) //Target Acquired
        {
            Debug.Log(hit.collider.name);
            Debug.Log(hit.point);
        }
        else
        {
            Debug.Log("");
        }
    }
}
