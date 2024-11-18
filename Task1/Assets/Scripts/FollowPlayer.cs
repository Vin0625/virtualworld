using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private int changecam=0;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {   
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            changecam++;//카메라 시점을 설정하는 int형 변수
        }
        if((changecam%4)==0){ //첫번째 시점
            transform.position=player.transform.position+new Vector3(0,5,-8);
            transform.rotation = Quaternion.Euler(10, 0, 0);
        }else if((changecam%4)==1){ //두번째 시점
            transform.position=player.transform.position+new Vector3(-10,5,0);
            transform.rotation = Quaternion.Euler(10, 90, 0);
        }else if((changecam%4)==2){ //세번째 시점
            transform.position=player.transform.position+new Vector3(7.5f,5,0);
            transform.rotation = Quaternion.Euler(10, -90, 0);
        }else{//네번째 시점
            transform.position=player.transform.position+new Vector3(0,5,10);
            transform.rotation = Quaternion.Euler(10, 180, 0);
        }
    }
}
