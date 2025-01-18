using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;



public class DestoryAfter : MonoBehaviour
{
    public float projectilelifetime = 1f;


    private PhotonView pv;

    void Start(){
        pv = GetComponent<PhotonView>();
    }

    void FixedUpdate(){
        if(projectilelifetime <= 0){
            if(pv){
                if (pv.IsMine){
                    PhotonNetwork.Destroy(gameObject);
                }
            }
            
        }

        projectilelifetime--;
        
    }

}
