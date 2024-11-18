using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private int cameranum=0;
    public GameObject player;

    Camera cameraCon;
    // Start is called before the first frame update
    void Start()
    {
        cameraCon=GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //쉬프트를 누르면 카메라의 시점을 바꿈
        //캐릭터가 보는 정면값으로 
        //시점도 perspective로 바꿔야함
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            cameranum++;
        }

        if(cameranum%2==0){//원래 시점
            transform.position=new Vector3(0,25,10);
            transform.rotation=Quaternion.Euler(90,0,0);
            cameraCon.orthographic=true;

        }else{//1인칭 시점
            transform.position=player.transform.position+new Vector3(0,3,0);
            transform.rotation=player.transform.rotation;
            cameraCon.orthographic=false;
        }
    }
}
