using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchExperience : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadExperience()
    {
        SceneManager.LoadScene("MainExperience", LoadSceneMode.Single);
    }

    public void LoadProfile()
    {
        SceneManager.LoadScene("ProfileSelection", LoadSceneMode.Single);
    }

    public void LoadAboutUs()
    {
        SceneManager.LoadScene("AboutUs", LoadSceneMode.Single);
    }

    public void LoadAboutFlow()
    {
        SceneManager.LoadScene("AboutFlow", LoadSceneMode.Single);
    }

    public void LoadFlowLibrary()
    {
        SceneManager.LoadScene("SwitchGame", LoadSceneMode.Single);
    }

    public void LoadTheme()
    {
        SceneManager.LoadScene("SwitchTheme", LoadSceneMode.Single);
    }

    public void LoadCalmGame()
    {
        SceneManager.LoadScene("CalmGame", LoadSceneMode.Single);
    }

    public void LoadFocusGame()
    {
        SceneManager.LoadScene("FocusGame", LoadSceneMode.Single);
    }
}
