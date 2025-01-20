using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject RoomManagerObject;
    public GameObject mainMenuObject;
    public OptionsMenu optionsMenuObj;
    public TMP_InputField nameInputField;
    private string defaultname = "test1";


    void Start(){
        optionsMenuObj.loadSettings();
        loadName();
    }


    public void nameInputChanged(){

    }

    public void saveName(){
        PlayerPrefs.SetString("playerName", nameInputField.text);
    }

    public void loadName(){
        nameInputField.text = PlayerPrefs.GetString("playerName", defaultname);
    }

    public void PlayButtonClicked(){
        RoomManagerObject.SetActive(true);
        mainMenuObject.SetActive(false);
    }
}
