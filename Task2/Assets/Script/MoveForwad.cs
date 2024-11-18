using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwad : MonoBehaviour
{
    public float moveSpeed = 0.1f; // 매우 낮은 이동 속도
    public float rotationChangeInterval = 2f; // 회전 방향을 변경하는 시간 간격
    public float smoothMoveTime = 0.3f; // 위치 변경에 대한 부드러움 정도
    public float smoothRotateTime = 0.5f; // 회전 변경에 대한 부드러움 정도

    private Quaternion targetRotation; // 목표 회전
    private Vector3 targetPosition; // 목표 위치

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        // 랜덤한 회전을 시작합니다.
        StartCoroutine(ChangeRotationRoutine());
    }

    // Update is called once per frame
    void Update()
    {
       targetPosition += transform.forward * moveSpeed * Time.deltaTime;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -20f, 20f);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -10f, 30f);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothMoveTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotateTime * Time.deltaTime);
   
    }
    IEnumerator ChangeRotationRoutine()
    {
        while (true)
        {     
            targetRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            
            yield return new WaitForSeconds(rotationChangeInterval);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        targetRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
}
