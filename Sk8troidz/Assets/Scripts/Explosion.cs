using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
public class Explosion : MonoBehaviour
{

    public GameObject explosion;
    public GameObject smoke;
    public float damage;
    public float power;
    public float radius;
    public float speed;
    public PhotonView pv;
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {

            Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.GetComponent<Player_Health>() != null)
                {
                hit.gameObject.GetComponent<Player_Health>().Add_Explosion(power, radius, this.transform.position.x, this.transform.position.y, this.transform.position.z);
                if (hit.gameObject.GetComponent<PhotonView>().Owner.GetPhotonTeam() != PhotonNetwork.LocalPlayer.GetPhotonTeam())
                {
                    hit.gameObject.GetComponent<Player_Health>().Remove_Health(damage);
                                    }
                }
            }
            
        GameObject explosion_clone = PhotonNetwork.Instantiate(explosion.name, this.transform.position, this.transform.rotation);
        GameObject smoke_clone = PhotonNetwork.Instantiate(smoke.name, this.transform.position, this.transform.rotation);
        PhotonNetwork.Destroy(this.gameObject);
    }

}
