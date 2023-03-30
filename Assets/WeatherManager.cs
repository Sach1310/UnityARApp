using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;
using System.Net;
using UnityEngine.SceneManagement;


public class WeatherManager : MonoBehaviour
{
    public string API_KEY="c447d7f226f4567b7f190fbec0e6e483";
    public TMP_InputField weatherCityText;
    public TextMeshProUGUI temp;
    public TextMeshProUGUI weatherType;
    public TextMeshProUGUI humidity;
    public TextMeshProUGUI wind;
    public TextMeshProUGUI tempture;
    public TextMeshProUGUI sunset;
    public TextMeshProUGUI sunrise;
    public Image imageToDisplay;
    public GameObject mainPanel;
    public GameObject nextDaysPanel;
    public TextMeshProUGUI day1;
    public TextMeshProUGUI tempDay1;
    public Image imgDay1;
    public TextMeshProUGUI day2;
    public TextMeshProUGUI tempDay2;
    public Image imgDay2;
    public TextMeshProUGUI day3;
    public TextMeshProUGUI tempDay3;
    public Image imgDay3;
    public TextMeshProUGUI day4;
    public TextMeshProUGUI tempDay4;
    public Image imgDay4;
    public TextMeshProUGUI day5;
    public TextMeshProUGUI tempDay5;
    public Image imgDay5;

 void Start(){
    mainPanel.SetActive(false);

}

public void GetWeather()
{
   WeatherInfo info = APIManager.GetWeatherByCityName(weatherCityText.text);
        if(info!=null){
            temp.text=Math.Round((info.main.temp-273.15),2)+" °C";
            weatherType.text=info.weather[0].main;
            humidity.text=info.main.humidity.ToString()+"%";
            tempture.text=info.main.pressure.ToString()+" hpa";
            wind.text=info.wind.speed.ToString()+" m/s";
            sunset.text=UnixTimeStampToDateTime(info.sys.sunrise).ToString("hh:mm")+" PM\n(Sunset)";
            sunrise.text=UnixTimeStampToDateTime(info.sys.sunset).ToString("hh:mm")+" AM\n(sunrise)";

            Davinci.get()
            .load(String.Format("https://openweathermap.org/img/wn/{0}@4x.png", info.weather[0].icon))
            .into(imageToDisplay)
            .setCached(true)
            .setFadeTime(3)
            .start();  
            mainPanel.SetActive(true);

            try{
                        Debug.LogFormat("Location service live. status {0}", info.coord.lon);
    
            WeatherInfoDayResponse infoDays=APIManager.GetWeatherByDays(info.coord.lat,info.coord.lon);

            if(infoDays!=null){

            day1.text=  UnixTimeStampToDateTime(infoDays.daily[1].dt).ToString("ddd");
            tempDay1.text=Math.Round((infoDays.daily[1].temp.day-273.15))+" °C";
            Davinci.get()
            .load(String.Format("https://openweathermap.org/img/wn/{0}@4x.png",infoDays.daily[1].weather[0].icon))
            .into(imgDay1)
            .setCached(true)
            .setFadeTime(3)
            .start(); 

            day2.text=  UnixTimeStampToDateTime(infoDays.daily[2].dt).ToString("ddd");
            tempDay2.text=(infoDays.daily[2].temp.day-273.15)+" °C";
            Davinci.get()
            .load(String.Format("https://openweathermap.org/img/wn/{0}@4x.png",infoDays.daily[2].weather[0].icon))
            .into(imgDay2)
            .setCached(true)
            .setFadeTime(3)
            .start();  

            day3.text=  UnixTimeStampToDateTime(infoDays.daily[3].dt).ToString("ddd");
            tempDay3.text=Math.Round((infoDays.daily[3].temp.day-273.15))+" °C";
            Davinci.get()
            .load(String.Format("https://openweathermap.org/img/wn/{0}@4x.png",infoDays.daily[3].weather[0].icon))
            .into(imgDay3)
            .setCached(true)
            .setFadeTime(3)
            .start(); 

            day4.text=  UnixTimeStampToDateTime(infoDays.daily[4].dt).ToString("ddd");
            tempDay4.text=Math.Round((infoDays.daily[4].temp.day-273.15))+" °C";
            Davinci.get()
            .load(String.Format("https://openweathermap.org/img/wn/{0}@4x.png",infoDays.daily[4].weather[0].icon))
            .into(imgDay4)
            .setCached(true)
            .setFadeTime(3)
            .start(); 

            day5.text=  UnixTimeStampToDateTime(infoDays.daily[5].dt).ToString("ddd");
            tempDay5.text=(infoDays.daily[5].temp.day-273.15)+" °C";
            Davinci.get()
            .load(String.Format("https://openweathermap.org/img/wn/{0}@4x.png",infoDays.daily[5].weather[0].icon))
            .into(imgDay5)
            .setCached(true)
            .setFadeTime(3)
            .start(); 
            nextDaysPanel.SetActive(true);

            }
            }catch(Exception ae){
                                Debug.LogFormat("API Error {0}", ae);

            }
            }   

    }

    public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        
}
