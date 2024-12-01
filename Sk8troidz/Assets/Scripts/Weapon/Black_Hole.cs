using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class Black_Hole : MonoBehaviour
{
    [Header("Black Hole Settings")]
    public Weapon weapon;
    [SerializeField] float basePullForce = 1000f; // The base strength of the pull
    [SerializeField] float pullRadius = 10000f; // The radius within which objects are pulled
    [SerializeField] float updateInterval = 0.5f; // Time interval to check for new objects (in seconds)
    public static GameObject[] players;
    private List<GameObject> targets;
    private float nextUpdateTime = 0f; // Timer to control update frequency

    void Start()
    {
        targets = new List<GameObject>();
    }

    void Update()
    {
        // Only check for new objects at specified intervals
        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time + updateInterval;

            // Check for newly instantiated objects
            CheckForNewObjects();
        }
    }

    void FixedUpdate()
    {
        foreach (GameObject obj in targets)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (transform.position - rb.position).normalized;
                float distance = Vector3.Distance(transform.position, rb.position);

                if (distance <= pullRadius)
                {
                    // Calculate pull strength using inverse square law
                    float pullStrength = 2 * basePullForce / Mathf.Pow(distance, 2);
                    rb.AddForce(direction * pullStrength, ForceMode.Acceleration);
                }
            }
        }

    }

    private void CheckForNewObjects()
    {
        
        
            targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            targets.AddRange(GameObject.FindGameObjectsWithTag("AI_Player"));

            targets.RemoveAll(player =>
            {
                PhotonView photonView = player.GetComponent<PhotonView>();
                if (photonView == null) return true;

                Photon.Realtime.Player owner = photonView.Owner;
                Photon.Realtime.Player localPlayer = PhotonNetwork.LocalPlayer;

                if (owner == null || localPlayer == null) return false;

                return weapon.player.GetComponent<Team_Handler>().GetTeam() == player.GetComponent<Team_Handler>().GetTeam();
            });


        }


    }

   
