using UnityEngine;
using Photon.Pun;

public class PhotonTimer : MonoBehaviourPunCallbacks
{
    private float startTime; // The time when the timer starts
    public float timer = 0; // The timer value (in seconds)
    public bool isTimerRunning = true; // Whether the timer is running or not

    public float timerDuration = 60f; // The total duration for the timer (in seconds)

    void Start()
    {
        //StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // Update the timer for the MasterClient only
                timer = timerDuration - (Time.time - startTime);
                if (timer <= 0f)
                {
                    isTimerRunning = false;
                    timer = 0f; // Prevent timer from going negative
                    OnTimerEnd(); // Call timer end action (e.g., end game, etc.)
                }
                
                // Synchronize the timer value across the network
                photonView.RPC("SyncTimer", RpcTarget.Others, timer);
            }
        }
    }

    // This RPC is called on all clients to sync the timer
    [PunRPC]
    void SyncTimer(float syncedTime)
    {
        if (!PhotonNetwork.IsMasterClient) // Only update the timer for non-MasterClients
        {
            timer = syncedTime;
        }
    }

    // Action to perform when the timer reaches 0
    private void OnTimerEnd()
    {
        Debug.Log("Timer has ended!");

        // Add logic for what happens when the timer ends, such as ending the game
        // or triggering a new event in the game (like spawning a new level).
    }

    // Function to start the timer manually
    public void StartTimer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startTime = Time.time;
            isTimerRunning = true;
        }
    }
}
