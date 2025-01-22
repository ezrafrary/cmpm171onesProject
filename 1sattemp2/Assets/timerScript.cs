using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerScript : MonoBehaviour
{
    public float targetTime = 60.0f;
    public TextMeshProUGUI timerText;

    public RoomManager roomManager;

    void Update(){
        targetTime -= Time.deltaTime;
        timerText.text = ((int)targetTime).ToString();

        if (targetTime <= 0.0f){
            timerEnded();
        }

    }

    public void timerEnded(){
        roomManager.timerEnded();
    }
}
