using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


//WARNING: if you call TakeDamage() on a trigger field (say with OnTriggerEnter()) it may get called twice, use special handling.

public class Health : MonoBehaviour
{
    public int health;
    public bool IsLocalPlayer;

    [Header("UI")]
    public TextMeshProUGUI healthText;

    [PunRPC]
    public void TakeDamage(int _damage){
        health -= _damage;

        healthText.text = health.ToString();
        if(health <= 0){

            if(IsLocalPlayer){
                
                RoomManager.instance.SpawnPlayer();
                RoomManager.instance.deaths++;
                RoomManager.instance.SetHashes();

            }
            Destroy(gameObject);
        }
    }

    [PunRPC]
    public void KillPlayer(){
        if (health > 0){ //This seems pointless, but if you use OnTriggerEnter as a damage field, it gets called twice in one frame, duplicating a client 
            TakeDamage(health);
        }

    }
}
