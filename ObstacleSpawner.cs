using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] obstacles;
    public GameObject cam;

    public float randHigh = 8;
    public float randLow = 5;
    float restart = 30;
    

    private List<GameObject> obstaclesForSpawning = new List<GameObject>();

    void Awake()
    {
        InitializeObstacles();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnRandomObstacle());

    }

    // Update is called once per frame
    void Update()
    {

        //Updating the spawning rate each time the camera moves a certain amount
        if(cam.transform.position.x > restart)
        {
            randHigh -= 0.35f;
            randLow -= 0.35f;           
            restart += 25;
        }
        //Setting limits for the spawning rate
        if (randHigh < 2)
        {
            randHigh = 2f;
        }
        if(randLow < 0.5)
        {
            randLow = 0.5f;
        }
    }

    void InitializeObstacles()
    {
        int index = 0;
    //Adding obstacles to spawn
        for (int i = 0; i < obstacles.Length * 3; ++i)
        {
            GameObject obj = Instantiate(obstacles[index], new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity) as GameObject;
            obstaclesForSpawning.Add(obj);
            obstaclesForSpawning[i].SetActive(false);
            index++;
            if (index == obstacles.Length)
                index = 0;
        }
    }

    void Shuffle()
    {
        //Randomizing the obstacles to be spawned

        for (int i = 0; i < obstaclesForSpawning.Count; ++i)
        {
            GameObject temp = obstaclesForSpawning[i];
            int random = Random.Range(i, obstaclesForSpawning.Count);
            obstaclesForSpawning[i] = obstaclesForSpawning[random];
            obstaclesForSpawning[random] = temp;
        }
    }

    IEnumerator SpawnRandomObstacle()
    {
        //Spawning obstacles at a random time witihin range
        yield return new WaitForSeconds(Random.Range(randLow, randHigh));
        int index = Random.Range(0, obstaclesForSpawning.Count);
        while (true)
        {
            if (!obstaclesForSpawning[index].activeInHierarchy)
            {
                obstaclesForSpawning[index].SetActive(true);
                obstaclesForSpawning[index].transform.position = new Vector3(transform.position.x, transform.position.y, -2);
                break;
            }
            else
            {
                index = Random.Range(0, obstaclesForSpawning.Count);
            }

        }
        StartCoroutine(SpawnRandomObstacle());
    }
}
