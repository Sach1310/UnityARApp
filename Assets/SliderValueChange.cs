using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    public Slider slider;
    public float silderValue=0;

    void Start()
    {
        slider.maxValue=100;
    }

    void Update(){
        if(silderValue<=100){
        slider.value=silderValue;
        silderValue=silderValue+2;
        }
    }

    
}

