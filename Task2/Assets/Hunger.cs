using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
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
