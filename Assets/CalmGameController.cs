using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalmGameController : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject particle;
    public GameObject holdBreathText;
    public GameObject outBreathText;

    private MusicController musicController = new MusicController();
    private GameObject sphere;
    public float maxScale = 0.2f;
    public float minScale = 0.05f;
    public float scaleSpeed = 0.1f;
    public float scale = 0.1f;
    private int scaleDirection = 1;
    private int axisScaleDirection = 1;
    private int counter = 1;

    // Start is called before the first frame update
    void Start()
    {
        musicController.PlayCalmGameMusic();
        sphere = Instantiate(spherePrefab, new Vector3(0, 0, 1), Quaternion.identity);
        sphere.GetComponent<CubeSphere>().minRadius = 0.5f;
        sphere.GetComponent<CubeSphere>().maxRadius = 1f;
        sphere.GetComponent<CubeSphere>().axisScale = 10f;
        sphere.GetComponent<CubeSphere>().speed = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (scale > maxScale || scale < minScale)
        {
            if (scale < minScale)
            {
                counter++;
            }
            scaleDirection = -scaleDirection;
        }
        if (scale > (maxScale + minScale) / 2 + (maxScale - minScale) / 4 || scale < (maxScale + minScale) / 2 - (maxScale - minScale) / 4)
        {
            scale += 0.001f * scaleSpeed * scaleDirection / 2;
        }
        else
        {
            scale += 0.001f * scaleSpeed * scaleDirection;
        }
        sphere.transform.localScale = new Vector3(scale, scale, scale);
        particle.transform.localScale = new Vector3(scale, scale, scale);
        if (sphere.GetComponent<CubeSphere>().axisScale > 15f || sphere.GetComponent<CubeSphere>().axisScale < 5f)
        {
            axisScaleDirection = -axisScaleDirection;
        }
        sphere.GetComponent<CubeSphere>().axisScale += 0.001f * axisScaleDirection;
        sphere.transform.Rotate(0.1f, 0.2f, 0.1f, Space.Self);

        if (counter == 3)
        {
            SceneManager.LoadScene("MainExperience", LoadSceneMode.Single);
        }
    }
}
