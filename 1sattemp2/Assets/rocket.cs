using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class rocket : MonoBehaviour
{
    public int damage = 1;
    public GameObject ignoreHitbox;
    private float projectilelifetime = 500f;

    public void setIgnoreHitbox(GameObject _ignoreHitbox){
        ignoreHitbox = _ignoreHitbox;
    }

    private PhotonView pv;

    void Start(){
        pv = GetComponent<PhotonView>();
    }
    void FixedUpdate(){
        projectilelifetime--;
        if (projectilelifetime < 0){
            if(pv){
            if (pv.IsMine){
                PhotonNetwork.Destroy(gameObject);
            }
        }
        }
        
    }
    
    void OnCollisionEnter(Collision other){
        

        if (other.transform.gameObject.GetComponent<Health>() && other.transform.gameObject != ignoreHitbox){
            //PhotonNetwork.LocalPlayer.AddScore(damage); add score for damage
            if (damage >= other.transform.gameObject.GetComponent<Health>().health){
                //kill

                RoomManager.instance.kills++;
                RoomManager.instance.SetHashes();
                PhotonNetwork.LocalPlayer.AddScore(1);
            }
            other.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            
        }
            
            
        if(pv){
            if (pv.IsMine){
                PhotonNetwork.Destroy(gameObject);
            }
        }

        
        

    }

}
