using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;
using System.Net;
using UnityEngine.SceneManagement;  

public class HomeScreenManager : MonoBehaviour
{
    public TextMeshProUGUI cityName;
    public TextMeshProUGUI temp;
    public TextMeshProUGUI weatherType;
    public TextMeshProUGUI humidity;
    public TextMeshProUGUI wind;
    public TextMeshProUGUI tempture;
    public TextMeshProUGUI sunset;
    public TextMeshProUGUI sunrise;
    public Image imageToDisplay;
    public GameObject mainPanel;
    public GameObject progressPanel;
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
    public TMP_InputField weatherCityText;

   
   
    // Start is called before the first frame update
    void Start()
    {
        int type=PlayerPrefs.GetInt("Type");
        if(type==1){
        string cityName=PlayerPrefs.GetString("CityName");
        weatherCityText.text=cityName;
        loadFromLocal();
        }
        else if(type==0){
        loadFromLocal();
        }
        else{
        mainPanel.SetActive(false);
        progressPanel.SetActive(true);
        StartCoroutine(LocationCoroutine());   
        }
        
    }

    public void changeToArCamera(){
        SceneManager.LoadScene("ARCamera");  
    }

    public void changeToSearchCity(){
        GetWeatherByCityName();
    }
    

 public void loadFromLocal()
{
   string jsonData=PlayerPrefs.GetString("CurrentData");
   WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonData);
        if(info!=null){
            cityName.text= info.name+", "+info.sys.country;
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
    
            string jsonDataDays=PlayerPrefs.GetString("DaysData");
            WeatherInfoDayResponse infoDays = JsonUtility.FromJson<WeatherInfoDayResponse>(jsonDataDays);
  
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
            tempDay2.text=Math.Round((infoDays.daily[2].temp.day-273.15))+" °C";
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
            tempDay5.text=Math.Round((infoDays.daily[5].temp.day-273.15))+" °C";
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
    public void GetWeatherByCityName()
{
   WeatherInfo info = APIManager.GetWeatherByCityName(weatherCityText.text);
        if(info!=null){
            PlayerPrefs.SetInt("Type",1);
            PlayerPrefs.SetString("CityName",weatherCityText.text);
            cityName.text= info.name+", "+info.sys.country;
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
            tempDay2.text=Math.Round((infoDays.daily[2].temp.day-273.15))+" °C";
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
            tempDay5.text=Math.Round((infoDays.daily[5].temp.day-273.15))+" °C";
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

    IEnumerator LocationCoroutine() {
    // Uncomment if you want to test with Unity Remotesss

#if UNITY_EDITOR
        yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
        yield return new WaitForSecondsRealtime(5f);
#endif
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            yield break;
        }

#elif UNITY_IOS
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            yield break;
        }
#endif
        // Start service before querying location
        UnityEngine.Input.location.Start(500f, 500f);
                
        // Wait until service initializes
        int maxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0) {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1) {
            // TODO Failure
            Debug.LogFormat("Timed out");
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running) {
            // TODO Failure
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            yield break;
        } else {
            Debug.LogFormat("Location service live. status {0}", UnityEngine.Input.location.status);
            // Access granted and location value could be retrieved
            Debug.LogFormat("Location: " 
                + UnityEngine.Input.location.lastData.latitude + " " 
                + UnityEngine.Input.location.lastData.longitude + " " 
                + UnityEngine.Input.location.lastData.altitude + " " 
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " " 
                + UnityEngine.Input.location.lastData.timestamp);

            double _latitude = UnityEngine.Input.location.lastData.latitude;
            double _longitude = UnityEngine.Input.location.lastData.longitude;
            // TODO success do something with location
            WeatherInfo info=APIManager.GetWeather(_latitude,_longitude);
            WeatherInfoDayResponse infoDays=APIManager.GetWeatherByDays(_latitude,_longitude);

            if(info!=null){
            PlayerPrefs.SetInt("Type",0);
            cityName.text= info.name+", "+info.sys.country;
            temp.text=Math.Round((info.main.temp-273.15))+" °C";
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
            progressPanel.SetActive(false);
            }
            else
            cityName.text="No record found";
            try{

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
            tempDay2.text=Math.Round((infoDays.daily[2].temp.day-273.15))+" °C";
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
            tempDay5.text=Math.Round((infoDays.daily[5].temp.day-273.15))+" °C";
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

        // Stop service if there is no need to query location updates continuously
        UnityEngine.Input.location.Stop();
    }

public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                PlayerPrefs.SetInt("Type",-1);
                Application.Quit();
            }
        }    
    }


}
