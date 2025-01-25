using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPhotonSoundManager : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstepSFX;

    public AudioSource gunShootSource;
    public AudioClip[] allGunShootSFX;

    public void PlayFootstepsSFX(){
        GetComponent<PhotonView>().RPC("PlayFootstepsSFX_RPC", RpcTarget.All);
    }



    [PunRPC]
    public void PlayFootstepsSFX_RPC(){
        footstepSource.clip = footstepSFX;

        //pitch/volume
        footstepSource.pitch = UnityEngine.Random.Range(0.7f, 1.2f);
        footstepSource.volume = UnityEngine.Random.Range(0.05f, 0.1f);
        footstepSource.Play();
    }

    public void PlayShootSFX(int index){
        GetComponent<PhotonView>().RPC("PlayShootSFX_RPC", RpcTarget.All, index);
    }

    [PunRPC]
    public void PlayShootSFX_RPC(int index){
        gunShootSource.clip = allGunShootSFX[index];

        //pitch/volume
        gunShootSource.pitch = UnityEngine.Random.Range(0.7f, 1.2f);
        gunShootSource.volume = UnityEngine.Random.Range(0.1f, 0.3f);

        gunShootSource.Play();
    }
}
