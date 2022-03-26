using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class EEGController : MonoBehaviour
{
    public static int EEG_AVERAGE_TIME = 10;

    public GameObject focusSphere;
    public GameObject happinessSphere;
    public GameObject flowSphere;
    public GameObject heartrateSphere;

    // List of EEG average states over time ()
    private static List<PlayerState> averages = new List<PlayerState>();
    private static PlayerState currState = new PlayerState{focus = 0, happiness = 0, flow = 0, heartrate = 0};
    private static int numMeasurements = 0;
    private static float elapsedTime = 0;
    
    void Start()
    {
        //focusSphere.SetActive(true);
        //happinessSphere.SetActive(true);
        //flowSphere.SetActive(true);
        //heartrateSphere.SetActive(true);
        StartCoroutine(getRequest("http://192.168.33.57:9000/brain"));
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator getRequest(string uri)
    {
        while (true)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                string[] values = uwr.downloadHandler.text.Split(' ');
                int focusValue = int.Parse(values[0]);
                int happinessValue = int.Parse(values[1]);
                int zoneValue = int.Parse(values[2]);
                int heartrateValue = int.Parse(values[3]);

                //focusSphere.transform.position = new Vector3(
                //    focusSphere.transform.position.x,
                //    focusValue / 100.0f * 1,
                //    focusSphere.transform.position.z
                //);

                //happinessSphere.transform.position = new Vector3(
                //    happinessSphere.transform.position.x,
                //    happinessValue / 100.0f * 1,
                //    happinessSphere.transform.position.z
                //);

                //flowSphere.transform.position = new Vector3(
                //    flowSphere.transform.position.x,
                //    zoneValue / 100.0f * 1,
                //    flowSphere.transform.position.z
                //);

                //heartrateSphere.transform.position = new Vector3(
                //    heartrateSphere.transform.position.x,
                //    heartrateValue / 140.0f * 1,
                //    heartrateSphere.transform.position.z
                //);

                numMeasurements++;
                elapsedTime += Time.deltaTime;
                currState = new PlayerState
                {
                    focus = currState.focus + focusValue,
                    happiness = currState.happiness + happinessValue,
                    flow = currState.flow + zoneValue,
                    heartrate = currState.heartrate + heartrateValue
                };

                if (focusValue == -1 || happinessValue == -1 || zoneValue == -1 || heartrateValue == -1)
                {
                    yield return null;
                }

                if (elapsedTime > EEG_AVERAGE_TIME)
                {
                    currState = new PlayerState
                    {
                        focus = currState.focus / numMeasurements,
                        happiness = currState.happiness / numMeasurements,
                        flow = currState.flow / numMeasurements,
                        heartrate = currState.heartrate / numMeasurements
                    };

                    Debug.Log("NEW AVERAGE " + currState.focus + " " + currState.happiness + " " + currState.flow + " " + currState.heartrate);

                    averages.Add(currState);
                    currState = new PlayerState { focus = 0, happiness = 0, flow = 0, heartrate = 0 };
                    numMeasurements = 0;
                    elapsedTime = 0;
                }

                yield return new WaitForSeconds(1);
            }
        }
    }

    public static PlayerState GetAverageState(int seconds)
    {
        PlayerState avgState = new PlayerState { focus = 0, happiness = 0, flow = 0, heartrate = 0 };
        int numSelectMeasurements = seconds / EEG_AVERAGE_TIME;
        int count = 0;

        // If the program has just been started, we wont have any averages
        if (averages.Count == 0)
        {
            return avgState;
        }

        // Sum up all averages within a certain time frame
        for (int i = averages.Count-1; i > -1; i++)
        {
            avgState.focus = averages[i].focus;
            avgState.happiness += averages[i].happiness;
            avgState.flow += averages[i].flow;
            avgState.heartrate += averages[i].heartrate;
            count++;
        }

        avgState.focus /= count;
        avgState.happiness /= count;
        avgState.flow /= count;
        avgState.heartrate /= count;

        return avgState;
    }
}
