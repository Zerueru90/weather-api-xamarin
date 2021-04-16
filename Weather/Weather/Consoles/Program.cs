using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Json; //Requires nuget package System.Net.Http.Json
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

using Weather.Models;
using Weather.Services;

namespace Weather.Consoles
{
    //Your can move your Console application Main here. Rename Main to myMain and make it NOT static and async
    class Program
    {
        #region used by the Console
        Views.ConsolePage theConsole;
        StringBuilder theConsoleString;
        public Program(Views.ConsolePage myConsole)
        {
            //used for the Console
            theConsole = myConsole;
            theConsoleString = new StringBuilder();
        }
        #endregion

        //public async Task myMainNew()
        //{
        //    double latitude = 59.4033092;
        //    double longitude = 17.9442904;

        //    Task<Forecast> t1 = new OpenWeatherService().GetForecastAsync(latitude, longitude);

        //    bool uriReturn = true;
        //    try
        //    {
        //        t1.Wait();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        uriReturn = false;
        //    }
        //    //Your Code
        //    if (uriReturn)
        //    {
        //        Console.WriteLine($"Weather forecast for {t1.Result.City}");
        //        var groupList = t1.Result.Items.GroupBy(x => x.DateTime.Date);

        //        foreach (var item in groupList)
        //        {
        //            Console.WriteLine(item.Key.Date.ToShortDateString());
        //            foreach (var item2 in item)
        //            {
        //                Console.WriteLine($"    - {item2.DateTime:HH:mm}: {item2.Description}, temperature: {item2.Temperature} degC, wind: {item2.WindSpeed} m/S");
        //            }
        //        }
        //    }
        //}


        #region Console Demo program
        //This is the method you replace with your async method renamed and NON static Main
        public async Task myMain()
        {
            theConsole.WriteLine("Demo program output");

            //Write an output to the Console
            theConsole.Write("One ");
            theConsole.Write("Two ");
            theConsole.WriteLine("Three and end the line");

            //As theConsole.WriteLine return trips are quite slow in UWP, use instead of myConsoleString to build the the more complex output
            //string using several myConsoleString.AppendLine instead of several theConsole.WriteLine. 
            foreach (char c in "Hello World from my Console program")
            {
                theConsoleString.Append(c);
            }

            //Once the string is complete Write it to the Console
            theConsole.WriteLine(theConsoleString.ToString());

            theConsole.WriteLine("Wait for 2 seconds...");
            await Task.Delay(2000);

            //Finally, demonstrating getting some data async
            theConsole.WriteLine("Download from https://dotnet.microsoft.com/...");
            theConsoleString.Clear();
            using (var w = new WebClient())
            {
                string str = await w.DownloadStringTaskAsync("https://dotnet.microsoft.com/");
                theConsoleString.Append($"Nr of characters downloaded: {str.Length}");
            }
            theConsole.WriteLine(theConsoleString.ToString());
        }

        //If you have any event handlers, they could be placed here
        void myEventHandler(object sender, string message)
        {
            theConsole.WriteLine($"Event message: {message}"); //theConsole is a Captured Variable, don't use myConsoleString here
        }
        #endregion
    }
}
