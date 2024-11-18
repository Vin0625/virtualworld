using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    float speed=20;
    float turnspeed=40;
    public float verticalInput;
    public float horizontalInput;
    private float timer=0;
    static Vector3 currentPosition;
    static Quaternion currentRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput=Input.GetAxis("Horizontal");
        verticalInput=Input.GetAxis("Vertical");
        // Move the vehocle forward
        transform.Translate(Vector3.forward*Time.deltaTime*speed*verticalInput);
        transform.Rotate(Vector3.up*Time.deltaTime*turnspeed*horizontalInput);

        timer+=Time.deltaTime;//현실시간으로 timer값 증가

        if(timer>4){//timer가 4이상일 때 ResetPosition함수 실행 후 타이머 초기화
            ResetPosition();
            timer=0;
        }

        if(transform.position.y<-2){// 객체의 y위치가 -2보다 떨어졌을 때 저장했던 위치로 이동
            transform.position = currentPosition;
            transform.rotation = currentRotation;
        }
        
    }

    void ResetPosition()//현재 오브젝트의 위치를 저장
    {
        currentPosition = transform.position;
        currentRotation = transform.rotation;
    }
}
