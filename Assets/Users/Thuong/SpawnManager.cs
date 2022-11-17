using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float Y = 4;
    private float StarRepeatingAtTime = 3f;
    private float Timerepeat = 1.3f; // repeating every 0.3 second;
    private int rateFastStar;
    public GameObject[] spawnObjectPrefab;
    private int RandomNum;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObj", StarRepeatingAtTime, Timerepeat);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnRandomObj()
    {
        RandomNum = Random.Range(0, 1);
  
        switch (RandomNum)
        {
            case 0:
                Y = -3;
                Vector3 Spawnpos1 = new Vector3(this.transform.position.x + 18, Y, 0);
                Instantiate(spawnObjectPrefab[RandomNum], Spawnpos1, spawnObjectPrefab[RandomNum].transform.rotation);
                Vector3 Spawnpos2 = new Vector3(this.transform.position.x + 27 , -5, 0);
                Instantiate(spawnObjectPrefab[RandomNum], Spawnpos2, spawnObjectPrefab[RandomNum].transform.rotation);
                break;
            case 1:
                Y = 1;
                Vector3 Spawnpos3 = new Vector3(this.transform.position.x + 27 , Y, 0);
                Instantiate(spawnObjectPrefab[RandomNum], Spawnpos3, spawnObjectPrefab[RandomNum].transform.rotation);

                Vector3 Spawnpos4 = new Vector3(this.transform.position.x + 18, 2, 0);
                Instantiate(spawnObjectPrefab[RandomNum], Spawnpos4, spawnObjectPrefab[RandomNum].transform.rotation);
                break;
        }
       
    }
}
