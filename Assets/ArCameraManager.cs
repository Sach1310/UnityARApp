using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net;
using System;
using System.IO;
using UnityEngine.SceneManagement;  

public class ArCameraManager : MonoBehaviour
{
    public TextMeshProUGUI temp;
    public TextMeshProUGUI cityName;
    public TextMeshProUGUI weatherType;
    public Image imageToDisplay;
    public GameObject rainObj;
    public GameObject iceObj;
    public GameObject lightObj;
    public GameObject planObj;
    public int type=0;



    // Start is called before the first frame update
    void Start()
    {
        string jsonData=PlayerPrefs.GetString("CurrentData");
        WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonData);

        if(info!=null){
        cityName.text= info.name+", "+info.sys.country;
        temp.text=(info.main.temp-273.15)+" °C";
        weatherType.text=info.weather[0].main;
        Davinci.get()
            .load(String.Format("https://openweathermap.org/img/wn/{0}@4x.png", info.weather[0].icon))
            .into(imageToDisplay)
            .setCached(true)
            .setFadeTime(3)
            .start();     
        }
        type=0;
        testType();
    }
    public void testType(){
        type++;
        if(type==1){
            rainObj.SetActive(true);
            iceObj.SetActive(false);
            lightObj.SetActive(false);
            planObj.SetActive(false);
        }else if(type==2){
            rainObj.SetActive(false);
            iceObj.SetActive(true);
            lightObj.SetActive(false);
            planObj.SetActive(true);
      
        }else if(type==3){
            rainObj.SetActive(false);
            iceObj.SetActive(false);
            lightObj.SetActive(true);
            planObj.SetActive(false);
        }
        if(type==3){
            type=0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                                                
                SceneManager.LoadScene("HomeScene");  
            }
        }    
    }
}
