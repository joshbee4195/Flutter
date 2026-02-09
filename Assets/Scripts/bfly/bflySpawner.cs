using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bflySpawner : MonoBehaviour
{

    public GameObject grass;

    public GameObject[] obstacles;


    public Transform grassSpawn;
    public Transform obstacleSpawn;

    public GameObject player;
    public int distLimit;
    public int distLimitObs;


    //make list of spawned so can clean up when player gone past

    public List<GameObject> grassList;
    public List<GameObject> obsList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GrassSpawning();


        ObstacleSpawning();

        CleanUp();
    }


    public void GrassSpawning()
    {
        if (grassSpawn.transform.position.z - player.transform.position.z < distLimit)
        {
           GameObject p1 = Instantiate(grass, grassSpawn.transform.position, Quaternion.identity);

            grassList.Add(p1);

            grassSpawn.position += new Vector3(0, 0, 50);
        }
    }

    public void ObstacleSpawning()
    {
        if (obstacleSpawn.transform.position.z - player.transform.position.z < distLimitObs)
        {
            GameObject p1 = Instantiate(obstacles[Random.Range(0, obstacles.Length)], obstacleSpawn.transform.position, Quaternion.identity);

            obsList.Add(p1);

            obstacleSpawn.position += new Vector3(0, 0, 25);
        }
    }

    public void CleanUp()
    {
        //for every object in list
        //if player beyond, destroy

        for (int i = 0; i < obsList.Count; i++)
        {
            if (obsList[i].transform.position.z < player.transform.position.z - 25)
            {
                Destroy(obsList[i]);
                obsList.RemoveAt(i);
            }
        }
    }
}
