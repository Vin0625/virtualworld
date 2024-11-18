using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automoving : MonoBehaviour
{
    public float speed =9;

    public float rotationSpeed = 1.0f; // 회전 속도 조절 변수
    private Quaternion targetRotation; // 목표 회전 각도
    private bool startRotation = false; // 회전 시작 여부

    int turn=0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*speed);


        if (startRotation)
        {
            // Quaternion.Slerp를 이용해 점진적으로 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // 현재 회전이 목표 회전에 충분히 가까운지 확인
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f)
            {
                // 회전 완료
                transform.rotation = targetRotation; // 정확한 목표 회전 각도로 설정
                startRotation = false; // 회전 중지
            }
        }

        if(transform.position.y<-10){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag=="turnleft"){
            turn=-45;
            StartRotation();
        }else if(other.tag=="turnright"){
            turn=45;
            StartRotation();
        }else if(other.tag=="set90"){
            transform.rotation = Quaternion.Euler(0, 90, 0);
            startRotation=false;
        }else if(other.tag=="set0"){
            transform.rotation=Quaternion.Euler(0,0,0);
            startRotation=false;
        }else if(other.tag=="set-90"){
            transform.rotation=Quaternion.Euler(0,-90,0);
            startRotation=false;
        }else if(other.tag=="set180"){
            transform.rotation=Quaternion.Euler(0,180,0);
            transform.position = new Vector3(-105, transform.position.y, transform.position.z);
            startRotation=false;
        }else if(other.tag=="set-90x"){
            transform.rotation=Quaternion.Euler(0,-90,0);
            transform.position = new Vector3(98, transform.position.y, 149.8f);
            startRotation=false;
        }
    }
    public void StartRotation()
    {
        // 현재 회전에서 90도 추가한 것을 목표 회전으로 설정
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + turn, transform.eulerAngles.z);
        startRotation = true; // 회전 시작
    }
}
