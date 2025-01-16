using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;   

public class OptionsMenu : MonoBehaviour
{
    public Slider sensSlider;
    public TMP_InputField sensInputField;
    public GameObject roomManager;



    //it is awkward to change settings on player, go from OptionsMenu.cs -> RoomManager.cs -> PlayerSetup.cs -> MouseLook.cs

    public void sensSliderChanged(){
        sensInputField.text = sensSlider.value.ToString();
        roomManager.GetComponent<RoomManager>().changeSens(sensSlider.value, sensSlider.value);
    }

    public void sensInputFieldChanged(){
        try{
            sensSlider.value = float.Parse(sensInputField.text);
        }catch{
            sensInputField.text = "";
            sensSlider.value = 2f;
        }
    }

    public void setSensitivity(){

    }
}
