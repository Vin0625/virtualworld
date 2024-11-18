using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float movespeed;
    private PlayerControler playerControlerScript;

    private float leftBound=-15;
    // Start is called before the first frame update
    void Start()
    {
        playerControlerScript=GameObject.Find("player").GetComponent<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControlerScript.gameOver == false){
        transform.Translate(Vector3.left*movespeed*Time.deltaTime);
        }

        if(transform.position.x<leftBound&&gameObject.tag=="Obstacle"){
            Destroy(gameObject);
        }
    }
}
