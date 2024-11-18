using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToFood : MonoBehaviour
{
    public float detectionRadius = 10f; // 음식 탐지 반경
    public float speed = 5f; // 이동 속도
    public float rotationSpeed = 10f; // 회전 속도
    private float maxhunger =3;
    private float curhunger=3;

    private float behungry=10;

    public GameObject animal;
    public Slider hungerbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        Transform closestFood = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("food"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestFood = hitCollider.transform;
                }
            }
        }

        if (closestFood != null)
        {
            Vector3 direction = closestFood.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);


            Vector3 moveDirection = closestFood.position - transform.position;
            moveDirection.y = 0; 
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, closestFood.position) < 0.1f) 
            {
                Destroy(closestFood.gameObject);
                closestFood=null;
                if(curhunger<3){
                    curhunger++;
                }
            }
        }

        if(behungry>0){
            behungry-=Time.deltaTime;
        }else{
            curhunger--;
            behungry=10;
        }

        UdateHungerbar();

        if(curhunger==0){
            if(animal.tag=="animal1"){
                SpawnManager.animal1num--;
            }else if(animal.tag=="animal2"){
                SpawnManager.animal2num--;
            }else if(animal.tag=="animal3"){
                SpawnManager.animal3num--;
            }
            Destroy(animal);
        }
        
    }
    
     private void UdateHungerbar(){
        hungerbar.value=(float)curhunger / (float)maxhunger;
    }
}
