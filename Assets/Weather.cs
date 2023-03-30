using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Weather{
  public int id;
  public string main;
  public string icon;
}
[Serializable]
public class WeatherDetails{
  public double temp;
  public double pressure;
  public double humidity;
}
[Serializable]
public class Wind{
  public double speed;
}
[Serializable]
public class Coordinates{
  public double lat;
  public double lon;
}
[Serializable]
public class WeatherInfo
{
          public int cod;
          public int id;
          public string name;
          public List<Weather> weather;
          public WeatherDetails main;
          public Wind wind;
          public WeatherSys sys;
          public Coordinates coord;
}

[Serializable]
public class WeatherInfoDay
{
          public double dt;
          public List<Weather> weather;
          public WeatherInfoTemp temp;
}
[Serializable]
public class WeatherInfoTemp
{
  public double day;
}

[Serializable]
public class WeatherInfoDayResponse
{
    public List<WeatherInfoDay> daily;
    public WeatherInfoDay current;

}

[Serializable]
public class WeatherSys{
  public double sunrise;
  public double sunset;
  public string country;
}

