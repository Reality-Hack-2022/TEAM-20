using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusGameController : MonoBehaviour
{
    public GameObject spherePrefab;
    private List<GameObject> sphereList = new List<GameObject>();
    private List<Rigidbody> rigidBodies = new List<Rigidbody>();

    private MusicController musicController = new MusicController();
    private int sphereCount = 8;
    private float radius = 0.5f;
    private int randIdxOne = 0;
    private int randIdxTwo = 0;

    // Start is called before the first frame update
    void Start()
    {
        //musicController.PlayFocusGameMusic();

        for (int i = 0; i < sphereCount; i++)
        {
            GameObject sphere = Instantiate(spherePrefab, new Vector3(Mathf.Cos(Mathf.PI * 2.0f / sphereCount * i) * radius, Mathf.Sin(Mathf.PI * 2.0f / sphereCount * i) * radius + .2f, 1), Quaternion.identity);
            sphere.SetActive(true);
            Rigidbody gameObjectsRigidBody = sphere.AddComponent<Rigidbody>(); // Add the rigidbody.
            gameObjectsRigidBody.useGravity = false;
            sphereList.Add(sphere);
            rigidBodies.Add(gameObjectsRigidBody);
        }

        // Highlight two of the spheres and add popup text that shows instructions
        randIdxOne = Random.Range(0, sphereCount);
        randIdxTwo = Random.Range(0, sphereCount);
        while (randIdxTwo == randIdxOne)
        {
            randIdxTwo = Random.Range(0, sphereCount);
        }

        // Change the scale for now instead of changing color
        sphereList[randIdxOne].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        sphereList[randIdxTwo].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public int START_SEQ_TIME = 5;
    private bool showStartSeq = true;

    public int MOVE_SEQ_TIME = 15;
    private bool showMoveSeq = false;

    private bool showSelectSeq = false;

    private float elapsedTime = 0;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // If the game is done with the start sequence
        if (showStartSeq && elapsedTime > START_SEQ_TIME)
        {
            showStartSeq = false;
            showMoveSeq = true;
            elapsedTime = 0;

            sphereList[randIdxOne].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            sphereList[randIdxTwo].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            // Send them off in random velocities
            for (int i = 0; i < sphereCount; i++)
            {
                rigidBodies[i].velocity = new Vector3(Random.Range(-.4f, .4f), Random.Range(-.4f, .4f), 0);
            }
        }

        // If the game is done with the move sequence
        if (showMoveSeq && elapsedTime > MOVE_SEQ_TIME)
        {
            showMoveSeq = false;
            showSelectSeq = true;
            elapsedTime = 0;

            // Stop the spheres
            for (int i = 0; i < sphereCount; i++)
            {
                rigidBodies[i].velocity = new Vector3(0, 0, 0);

                // Add a collider for the users hand
            }
        }

        
    }
}
