using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject RoomManagerObject;
    public GameObject mainMenuObject;
    public OptionsMenu optionsMenuObj;

    void Start(){
        optionsMenuObj.loadSettings();
    }

    public void PlayButtonClicked(){
        RoomManagerObject.SetActive(true);
        mainMenuObject.SetActive(false);
    }
}
