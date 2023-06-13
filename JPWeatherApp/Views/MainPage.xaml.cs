using JPWeatherApp.Helper;
using JPWeatherApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JPWeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            entry.Text = "Seattle";
            GetWeatherInfo();
        }

        public void city_button(object sender, EventArgs args)
        {
            cityTxt.Text = entry.Text.ToString();
            GetWeatherInfo();
        }

        private async void GetWeatherInfo()
        {
            cityTxt.Text = entry.Text.ToString();
            string city = cityTxt.Text;

            var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=8c1e240150949fb7bfe0bf0503c8a20e&units=imperial";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                cityTxt.Text = weatherInfo.name.ToUpper();
                mainTemp.Text = weatherInfo.main.temp.ToString("0");

                var todayDate = DateTime.Now;
                mainDayDate.Text = todayDate.ToString("dddd, MMM dd").ToUpper();

                GetForecast();
            }
            else
            {
                await DisplayAlert("Weather Info", "No weather information found", "OK");
            }

        }

        private async void GetForecast()
        {
            cityTxt.Text = entry.Text.ToString();
            string city = cityTxt.Text;

            var url = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid=8c1e240150949fb7bfe0bf0503c8a20e&units=imperial";
            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var forcastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    List<List> allList = new List<List>();

                    foreach (var list in forcastInfo.list)
                    {
                        var date = DateTime.Parse(list.dt_txt);

                        if (date > DateTime.Now && date.Hour == 21 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }

                    dayTxtDay1.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd");
                    dateDay1.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                    tempDay1.Text = allList[1].main.temp.ToString("0");

                    dayTxtDay2.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd");
                    dateDay2.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                    tempDay2.Text = allList[2].main.temp.ToString("0");

                    dayTxtDay3.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd");
                    dateDay3.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                    tempDay3.Text = allList[3].main.temp.ToString("0");

                    dayTxtDay4.Text = DateTime.Parse(allList[4].dt_txt).ToString("dddd");
                    dateDay4.Text = DateTime.Parse(allList[4].dt_txt).ToString("dd MMM");
                    tempDay4.Text = allList[4].main.temp.ToString("0");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No forecast information found", "OK");
            }
        }
    }
}