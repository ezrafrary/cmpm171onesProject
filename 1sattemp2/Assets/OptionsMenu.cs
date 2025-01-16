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
    public TMP_InputField fovInputField;
    public Slider fovSlider;



    //it is awkward to change settings on player, go from OptionsMenu.cs -> RoomManager.cs -> PlayerSetup.cs -> MouseLook.cs

    public void sensSliderChanged(){
        sensInputField.text = sensSlider.value.ToString();
        roomManager.GetComponent<RoomManager>().changeSens(sensSlider.value, sensSlider.value); //xsens and ysens are the same
    }

    public void sensInputFieldChanged(){
        try{
            sensSlider.value = float.Parse(sensInputField.text);
        }catch{
            sensInputField.text = "";
            sensSlider.value = 2f;
        }
    }

    public void fovSliderChanged(){
        fovInputField.text = fovSlider.value.ToString();
        roomManager.GetComponent<RoomManager>().changeFov((int)fovSlider.value);
    }

    public void fovinputFieldChanged(){
        try{
            fovSlider.value = float.Parse(fovInputField.text);
        }catch{
            fovInputField.text = "";
            fovSlider.value = 60f;
        }
    }
}
