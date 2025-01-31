using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AmmoPickup : MonoBehaviour
{
    public int magsGained;
    public GameObject itemAttachedTo;
    private bool hasBeenUsed;


    void OnTriggerEnter(Collider other){
        if(!hasBeenUsed){
            hasBeenUsed = true;
            if(other.transform.gameObject.GetComponent<Health>()){
                //add ammo to 
                //other.transform.gameObject.GetComponent<PhotonView>().RPC("Heal", RpcTarget.All, healthGained);
                other.transform.gameObject.GetComponent<PhotonView>().RPC("refillCurrentWeapon",RpcTarget.All, magsGained);
                itemHasBeenPickedUp();
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }


    public void itemHasBeenPickedUp(){
        if(itemAttachedTo){
            itemAttachedTo.GetComponent<ItemSpawnPedestal>().itemPickedUp();
        }
    }
}
