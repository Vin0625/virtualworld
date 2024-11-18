using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed=10.0f;
    public float xRange=20.0f;
    public float zRange=30.0f;

    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput=Input.GetAxis("Horizontal");
        verticalInput=Input.GetAxis("Vertical");//상하 키보드 입력받기
        transform.Translate(Vector3.right*horizontalInput*Time.deltaTime*speed);
        transform.Translate(Vector3.forward*verticalInput*Time.deltaTime*speed);//이동 동작

        if(transform.position.x<-xRange){
            transform.position=new Vector3(-xRange,transform.position.y,transform.position.z);
        }
        if(transform.position.x>xRange){
            transform.position=new Vector3(xRange,transform.position.y,transform.position.z);
        }
        if(transform.position.z<-zRange/3){//아래쪽으로 최대로 갈 수 있는 영역지정
            transform.position=new Vector3(transform.position.x,transform.position.y,-zRange/3);
        }
        if(transform.position.z>zRange){//위쪽으로 최대로 갈 수 있는 영역지정
            transform.position=new Vector3(transform.position.x,transform.position.y,zRange);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            //Launch a projectile from the player
            Instantiate(projectilePrefab,transform.position,projectilePrefab.transform.rotation);
        }
    }
}
