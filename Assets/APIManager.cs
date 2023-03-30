using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;


public class APIManager
{
        public static string API_KEY="c447d7f226f4567b7f190fbec0e6e483";


public static WeatherInfo GetWeather(double _latitude, double _longitude)
{
    try{
      HttpWebRequest request = 
          (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&APPID={2}", 
           _latitude,_longitude, API_KEY));
          HttpWebResponse response = (HttpWebResponse)request.GetResponse();
          StreamReader reader = new StreamReader(response.GetResponseStream());
          string jsonResponse = reader.ReadToEnd();
          PlayerPrefs.SetString("CurrentData",jsonResponse);
          WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
          return info;

          }catch(WebException webException)
          {
            Debug.LogFormat("API Error {0}", webException);
            return null;
          }
    }


    public static WeatherInfoDayResponse GetWeatherByDays(double _latitude, double _longitude)
{
    try{
      HttpWebRequest request = 
          (HttpWebRequest)WebRequest.Create(String.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&APPID={2}", 
           _latitude,_longitude, API_KEY));
          HttpWebResponse response = (HttpWebResponse)request.GetResponse();
          StreamReader reader = new StreamReader(response.GetResponseStream());
          string jsonResponse = reader.ReadToEnd();
          PlayerPrefs.SetString("DaysData",jsonResponse);

          WeatherInfoDayResponse info = JsonUtility.FromJson<WeatherInfoDayResponse>(jsonResponse);
          return info;

          }catch(WebException webException)
          {
            Debug.LogFormat("API Error {0}", webException);
            return null;
          }
    }

public static WeatherInfo GetWeatherByCityName(String cityName)
{
    try{
      HttpWebRequest request = 
          (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}", 
           cityName, API_KEY));
          HttpWebResponse response = (HttpWebResponse)request.GetResponse();
          StreamReader reader = new StreamReader(response.GetResponseStream());
          string jsonResponse = reader.ReadToEnd();
          PlayerPrefs.SetString("CurrentData",jsonResponse);
          WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
          return info;

          }catch(WebException webException)
          {
            Debug.LogFormat("API Error {0}", webException);
            return null;
          }
    }


}
