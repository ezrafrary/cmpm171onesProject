using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;


public class Explosion : MonoBehaviour
{
    public int damage = 1;
    private bool AlreadyDamaged = false;
    

    void OnTriggerEnter(Collider other){
        if (other.transform.gameObject.GetComponent<Health>() && AlreadyDamaged == false){
            AlreadyDamaged = true;
            Debug.Log("damaged");
            //PhotonNetwork.LocalPlayer.AddScore(damage); add score for damage
            if (damage >= other.transform.gameObject.GetComponent<Health>().health){
                //kill

                RoomManager.instance.kills++;
                RoomManager.instance.SetHashes();
                PhotonNetwork.LocalPlayer.AddScore(1);
            }
            other.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            
        }
    }
}
