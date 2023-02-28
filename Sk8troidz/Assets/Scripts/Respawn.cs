using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float respawn_time;
    [SerializeField] GameObject death_anim;
    [SerializeField] GameObject death_head;
    [SerializeField] GameObject point;
    [SerializeField] PointTally pt;

    public void Death()
{
        
        player.SetActive(false);
        
        GameObject death_anim_clone = Instantiate(death_anim, player.transform.position, Quaternion.identity);
        death_anim_clone.SetActive(true);
        
            GameObject point_clone = Instantiate(point, death_anim_clone.transform.position, Quaternion.identity);
            point_clone.SetActive(true);
            pt.ChangePoints(-1);

        for (int i = 0; i < pt.points/2; i ++) {
            GameObject point_clone2 = Instantiate(point, death_anim_clone.transform.position, Quaternion.identity);
            point_clone2.SetActive(true);
            pt.ChangePoints(-1);
        }
        

        GameObject death_head_clone = Instantiate(death_head, player.transform.position, Quaternion.identity);
        death_head_clone.SetActive(true);
       
        Invoke("Player_Active",  respawn_time);
}
    void Player_Active()
    {
        player.SetActive(true);
    }

}