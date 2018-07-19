using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WeatherForecaste.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Predict()
        {
            string results = "";

            using (WebClient wc = new WebClient())
            {
                results = wc.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='chicago, il')&format=json");
            }

            dynamic jo = JObject.Parse(results);
            var items = jo.query.results.channel.item.condition.text;

            ViewData["weather"] = items;


            return View();
        }
    }
}