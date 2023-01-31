using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float respawn_time;
    [SerializeField] GameObject death_anim;
    
    public void Death()
{
        //Add death anim. I was thinking maybe everything explodes and a head by itself spawns with the eye pupil rotating around- Past Jessie

        player.SetActive(false);
        GameObject death_clone = Instantiate(death_anim, player.transform.position, Quaternion.identity);
        death_clone.SetActive(true);
        Invoke("Player_Active", respawn_time);
}
    void Player_Active()
    {
        player.SetActive(true);
    }

}
