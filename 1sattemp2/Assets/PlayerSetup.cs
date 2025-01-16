using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;

    [Header("next 2 should be the same object")]
    public GameObject camera;
    public Camera cameraObj;

    public string nickname;
    


    public TextMeshPro nicknameText;
    
    public void SetCameraFov(float _cameraFov){
        cameraObj.fieldOfView = _cameraFov;
    }

    public void SetPlayerSens(float _sensX, float _sensY){
        camera.GetComponent<MouseLook>().setSensitivity(_sensX,_sensY);
    }

    public void IsLocalPlayer(){
        movement.enabled = true;
        camera.SetActive(true);

    }

    [PunRPC]
    public void SetNickname(string _name){
        nickname = _name;

        nicknameText.text = nickname;
    }
}
