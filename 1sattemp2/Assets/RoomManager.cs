using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{

    /*
        this class handles most of te serverside things. watch all of the following videos before editing it:
        https://www.youtube.com/watch?v=xRXOnuFji-Q&list=PL0iUgXtqnG2gPaXE1hHYoBjqTSWTNwR-6&index=1&ab_channel=bananadev2 
        https://www.youtube.com/watch?v=mZP_X1ejj8s&list=PL0iUgXtqnG2gPaXE1hHYoBjqTSWTNwR-6&index=5&ab_channel=bananadev2 
        https://www.youtube.com/watch?v=x8dfgCoe53w&list=PL0iUgXtqnG2gPaXE1hHYoBjqTSWTNwR-6&index=10&ab_channel=bananadev2 
        https://www.youtube.com/watch?v=TOP-huJ9duY&list=PL0iUgXtqnG2gPaXE1hHYoBjqTSWTNwR-6&index=12&ab_channel=bananadev2 
        https://www.youtube.com/watch?v=_QilKZ1f5Vo&list=PL0iUgXtqnG2gPaXE1hHYoBjqTSWTNwR-6&index=13&ab_channel=bananadev2
    */

    public static RoomManager instance;

    public GameObject player;
    [Space]
    public Transform[] spawnPoints;
    [Space]
    public GameObject roomCam;

    [Space]
    public GameObject nameUI;
    public GameObject connectingUI;
    public GameObject mainMenuUI;

    private string nickname = "unnamed";

    [HideInInspector]
    public int kills = 0;
    [HideInInspector]
    public int deaths = 0;
    [HideInInspector]
    public float playerSensX = 2;
    [HideInInspector]
    public float playerSensY = 2;
    [HideInInspector]
    public float playerFov = 60;

    public string roomNameToJoin = "test";
    

    void Awake(){
        instance = this;
    }


    public void changeSens(float _sensX, float _sensY){
        playerSensX = _sensX;
        playerSensY = _sensY;
    }

    public void changeFov(int newFov){
        playerFov = newFov;
    }

    public void ChangeNicname(string _name){
        nickname = _name;
    }
    
    public void JoinRoomButtonPressed(){
        Debug.Log("Connecting");

        
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

   


    public override void OnJoinedRoom(){
        base.OnJoinedRoom();
        connectingUI.SetActive(false);
        Debug.Log("We're connected and in a room!");
        roomCam.SetActive(false);
        SpawnPlayer();
        
    }

    public override void OnLeftRoom(){
        base.OnLeftRoom();
        Debug.Log("left room");
        roomCam.SetActive(true);
        mainMenuUI.SetActive(true);

    }

    

    public void SpawnPlayer(){

        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer(); 
        _player.GetComponent<Health>().IsLocalPlayer = true; 
        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);


        //set local settings using PlayerSetup
        _player.GetComponent<PlayerSetup>().SetPlayerSens(playerSensX, playerSensY);
        _player.GetComponent<PlayerSetup>().SetCameraFov(playerFov);

        PhotonNetwork.LocalPlayer.NickName = nickname;
        
    }

    
    public void SetHashes(){
        try{
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash["kills"] = kills;
            hash["deaths"] = deaths;

            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        catch{
            //nothing
        }
    }
}
