using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
public class LookAtCamera : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject Player_Assets;
    [SerializeField] PhotonView pv;
    [SerializeField] Text player_name;
    [SerializeField] Text score;
    public static Camera cam;
    

    void Start()
    {
        if(pv.IsMine)
        {
            player_name.text = PhotonNetwork.NickName;
            canvas.SetActive(false);
            cam = Player_Assets.GetComponentInChildren<Camera>();
          
        }
        else
        {
            player_name.text = pv.Owner.NickName;
            if (pv.Owner.GetPhotonTeam().Code == 1)
            {
                score.color = new Color(120, 0, 0);
                player_name.color = new Color(255, 0, 0);
            }
            if (pv.Owner.GetPhotonTeam().Code == 2)
            {
                score.color = new Color(120, 50, 100);
                player_name.color = new Color(255, 50, 100);
            }

        }
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
       if (cam != null)
        {
            
            canvas.transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
           // Player_Name.transform.LookAt(Player_Name.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        } 
    }
}
