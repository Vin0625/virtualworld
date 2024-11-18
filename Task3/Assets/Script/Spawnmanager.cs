using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos =new Vector3(25,1,0);
    private float startDelay =2;
    private float repeatRate = 2;
    private PlayerControler playerControlerScript;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle",startDelay,repeatRate);
        playerControlerScript=GameObject.Find("player").GetComponent<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle(){
        if(playerControlerScript.gameOver==false){
        Instantiate(obstaclePrefab,spawnPos,obstaclePrefab.transform.rotation);
        }
    }
}
