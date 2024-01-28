using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
public class Point : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int max;
    [SerializeField] int min;
    [SerializeField] int y_val;
    public GameObject player;
    [SerializeField] float speed;
    [SerializeField] PhotonView pv;
    public int value;
    float delay = 0;


    private void Start()
    {
        int x = Random.Range(min, max);
        int z = Random.Range(min, max);
        rb.AddForce(new Vector3(x, y_val, z));
        
    }

    void Update()
    {
        if (delay < 2)
        {
            delay += Time.deltaTime;
            Debug.Log(delay);
        }
        transform.Rotate(Vector3.forward * speed * 15 * Time.deltaTime);
        if (player != null)
        {

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
       
    }
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Player" && delay >= 2)
        {
            if (player != null)
            {
                Debug.Log(value);
                collider.gameObject.GetComponent<PhotonView>().Owner.AddScore(value);
                GetComponent<CapsuleCollider>().enabled = false;
                PhotonNetwork.Destroy(this.gameObject);


            }



        }

    }
    

}
