using System;
using System.Collections.Generic;
using System.Text;

namespace JPWeatherApp.Models
{
    class WeatherInfo
    {
        public string name { get; set; }
        public Main main { get; set; }

    }

    class Main
    {
        public float temp { get; set; }
    }

    class List
    {
        public Main main { get; set; }
        public string dt_txt { get; set; }
    }

    class ForecastInfo
    {
        public List[] list { get; set; }
    }
}
