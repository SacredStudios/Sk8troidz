using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Player_Health : MonoBehaviour

{
    [SerializeField] float max_health;
    public float current_health;
    [SerializeField] GameObject death_effect;
    [SerializeField] GameObject parent;
    [SerializeField] Slider health_bar;
    [SerializeField] Slider health_bar_other;
    [SerializeField] PhotonView pv;
    public GameObject last_hit;

    void Start()
    {
        current_health = max_health;    
    }

  
    public void Remove_Health(float amount, GameObject player)
    {
        last_hit = player;
        pv.RPC("ChangeHealth", RpcTarget.All, -1 * amount, player.GetComponent<PhotonView>().ViewID);

    }
    public void Add_Explosion(float power, float radius, float x, float y, float z)
    {
        pv.RPC("Explode", RpcTarget.All, power, radius, x ,y ,z);
    }
    [PunRPC] void Explode(float power, float radius, float x, float y, float z)
    {
        GetComponent<Rigidbody>().AddExplosionForce(power, new Vector3(x,y,z), radius, 1.12f);
    }
    [PunRPC] void ChangeHealth(float amount, int viewID) //the player that last hit you
    {
        if (viewID != 0)
        {
            last_hit = PhotonView.Find(viewID).gameObject;
        }
        
        Debug.Log(last_hit + "for health");
        current_health += amount;
        health_bar.value = current_health;
        health_bar_other.value = current_health;
        if (current_health <= 0 && pv.IsMine)
        {
            Death();
        }
        else if (current_health > max_health)
        {
            current_health = max_health;
        }

    }


    public void Add_Health(float amount)
    {
        pv.RPC("ChangeHealth", RpcTarget.All, amount, 0);
        

    }


    void Death()
    {
        Respawn rs = GetComponentInParent<Respawn>();
        rs.Death();

     }

    private void OnEnable()
    {
        current_health = 100;
        health_bar.value = current_health;
        health_bar_other.value = current_health;
    }
}
