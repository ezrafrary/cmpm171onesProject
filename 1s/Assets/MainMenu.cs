using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject RoomManagerObject;
    public GameObject mainMenuObject;


    public void PlayButtonClicked(){
        RoomManagerObject.SetActive(true);
        mainMenuObject.SetActive(false);
    }
}
