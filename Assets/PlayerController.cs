using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct PlayerState
{
    public int focus;
    public int happiness;
    public int flow;
    public int heartrate;
}

public class PlayerController : MonoBehaviour
{
    public int TIME_TILL_FLOW_LOST = 20;
    public int QUERY_EEG_THRESH = 1;
    public int FLOW_THRESHOLD = 70;
    public int DANGER_THRESHOLD = 50;

    private PlayerState lastState = new PlayerState{ focus = 0, happiness = 0, flow = 0, heartrate = 0 };
    private float timeSinceFlow = 0;
    private float timeCounter = 0;
    private bool inFlow = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        // If user not in flow, start keeping track of time since flow
        if (!inFlow)
        {
            timeSinceFlow += Time.deltaTime;
        }

        // Only ask the EEG class what the player state is every QUERY_EEG_THRESH seconds (5)
        if (timeCounter > QUERY_EEG_THRESH)
        {
            timeCounter = 0;
            lastState = EEGController.GetAverageState(20);
            if (lastState.flow < 70)
            {
                inFlow = false;
            } 
            else
            {
                inFlow = true;
                timeSinceFlow = 0;
            }
        }

        // Check if timeSinceFlow has passed the threshold
        if (timeSinceFlow > TIME_TILL_FLOW_LOST)
        {
            // Evaluate whether the user is bored or whether the user is unhappy
            if (lastState.focus < DANGER_THRESHOLD && lastState.happiness < DANGER_THRESHOLD)
            {
                SceneManager.LoadScene("CalmGame", LoadSceneMode.Single);
            }
            else if (lastState.focus < DANGER_THRESHOLD)
            {
                SceneManager.LoadScene("FocusGame", LoadSceneMode.Single);
            }
            else if (lastState.happiness < DANGER_THRESHOLD)
            {
                SceneManager.LoadScene("CalmGame", LoadSceneMode.Single);
            }
        }
    }
}