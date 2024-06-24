using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
[CreateAssetMenu(fileName = "new_weapon", menuName = "Scripts/RangedWeapon", order = 1)]
public class RangedWeapon : Weapon
{
    public GameObject particle_trail;
    public GameObject particle_explosion;
    public GameObject impact_explosion;

    public override void Shoot(GameObject parent, GameObject particle_pos, GameObject explosion_pos)
    {
        //  Debug.DrawRay(parent.transform.position, particle_pos.transform.up * range, Color.green); //chage this to capsulecast
        
        

        float radius = 1f;
        Ray ray = new Ray(parent.transform.position, particle_pos.transform.up); //-new Vector3(radius, 0, 0), 
        RaycastHit hit = new RaycastHit();
        PhotonNetwork.Instantiate(particle_trail.name, particle_pos.transform.position, particle_pos.transform.rotation);
        Instantiate(particle_explosion, explosion_pos.transform.position, particle_pos.transform.rotation);

        Ray distance = new Ray(player.transform.position, particle_pos.transform.up);

        if (Physics.Raycast(distance, min_distance))
        {
            Debug.Log("too short");
        }
        else
        {

            if (Physics.SphereCast(ray, radius, out hit, range)) //Target Acquired
            {


                if (hit.distance <= range)
                {
                    //Debug.Log(hit.collider.tag);
                    //  Debug.Log(hit.collider.GetComponent<PhotonView>().Owner.GetPhotonTeam() + " + " + PhotonNetwork.LocalPlayer.GetPhotonTeam());
                    PhotonNetwork.Instantiate(impact_explosion.name, hit.point, Quaternion.identity);
                    //"if()" is a good name of a book
                    if ((hit.collider.tag == "Player") && hit.collider.GetComponent<Team_Handler>().GetTeam() != player.GetComponent<Team_Handler>().GetTeam())
                    {
                        Player_Health ph = hit.collider.GetComponent<Player_Health>();
                        if (ph != null)
                        {
                            if (ph.current_health > 0)
                            {
                                Debug.Log("hit");
                                if (ph.current_health - damage <= 0)
                                {
                                    Transform oldpos = hit.collider.transform;
                                    hit.collider.transform.position = new Vector3(9999, 9999, 9999);
                                    SpawnCoin(hit.transform.parent.gameObject, hit.point);
                                    ph.PlayerLastHit(pv.ViewID); //base this off of the gameObject, not ID
                                    parent.GetComponentInParent<Super_Bar>().ChangeAmount(35);


                                }
                                ph.Remove_Health(damage);
                            }

                        }
                        else
                        {
                            Debug.Log("hit");
                        }
                        //call function from individual joints
                        // Debug.Log(hit.collider.gameObject.GetComponent<PhotonView>().Owner.GetPhotonTeam() + "+" + pv.Owner.GetPhotonTeam());

                    }
                    else if (hit.collider.tag == "Player_Head" && hit.collider.GetComponentInParent<PhotonView>().Owner.GetPhotonTeam() != PhotonNetwork.LocalPlayer.GetPhotonTeam())
                    {
                        Player_Health ph = hit.collider.GetComponentInParent<Player_Health>();
                        if (ph != null)
                        {
                            if (ph.current_health > 0)
                            {
                                Debug.Log("headshot");
                                if (ph.current_health - (damage * 1.5) <= 0)
                                {
                                    Transform oldpos = hit.collider.transform;
                                    hit.collider.transform.position = new Vector3(9999, 9999, 9999);
                                    SpawnCoin(hit.transform.parent.gameObject, hit.point);
                                    Debug.Log("it works");
                                    ph.PlayerLastHit(pv.ViewID);
                                    parent.GetComponentInParent<Super_Bar>().ChangeAmount(35);

                                }
                                ph.Remove_Health(damage * 1.5f);
                            }
                        }
                        else
                        {
                            Debug.Log("hit");
                        }
                        //Debug.Log("HeadShot");

                    }
                }
            }
        }

    }
    
}



        
       
