using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX= 5.0f;
    private float spawnRangeZ=5.0f;
    private float spawnPosZ=20.0f;
    private float startDelay =2;
    private float spawnInterval=10f;

    public static int animal1num=1;

    public static int animal2num=1;

    public static int animal3num=1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimal",startDelay,spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAnimal(){
        Vector3 spawnPos= new Vector3(Random.Range(-spawnRangeX,spawnRangeX),0,Random.Range(-spawnRangeZ,spawnRangeZ));

        if(animal1num>0){
            Instantiate(animalPrefabs[0],spawnPos,animalPrefabs[0].transform.rotation);
        }

        spawnPos= new Vector3(Random.Range(-spawnRangeX,spawnRangeX),0,spawnPosZ);
        if(animal2num>0){
            Instantiate(animalPrefabs[1],spawnPos,animalPrefabs[1].transform.rotation);
        }

        spawnPos= new Vector3(Random.Range(-spawnRangeX,spawnRangeX),0,spawnPosZ);
        if(animal3num>0){
            Instantiate(animalPrefabs[2],spawnPos,animalPrefabs[2].transform.rotation);
        }
        
    }
}
