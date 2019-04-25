using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace What_To_Eat
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool read = true;


            //List<string> maybeList = new List<string> { "2","44","13","78","4","45","46","1","73","72","41" };

            List<string> rinaList = new List<string> {
                "Pizza",
                "Mexican",
                "Vegan",
                "Thai",
                "Sushi",
                "Subway",
                "Chinese",
                "Peruvian",
                "East Side Sandwich",
                "Japanese",
                "Burgerville",
                "Little Big Burger",
                "Olive Garden",
                "Tin Shed",
                "Ramen",
                "New Seasons",
                "Burrito"

            };

            List<string> nathanList = new List<string> {
                "Pizza",
                "Mexican",
                "Vegan",
                "Thai",
                "Sushi",
                "Subway",
                "Chinese",
                "Peruvian",
                "East Side Sandwich",
                "Japanese",
                "Burgerville",
                "Little Big Burger",
                "Olive Garden",
                "Tin Shed",
                "Ramen",
                "New Seasons",
                "Burrito"

            };

            if (read)
            {
                string json = File.ReadAllText(@"c:../../rinaList.json");
                rinaList = JsonConvert.DeserializeObject<List<string>>(json);
                json = File.ReadAllText(@"c:../../nathanList.json");
                nathanList = JsonConvert.DeserializeObject<List<string>>(json);
            }

            /*
            string currentTime = DateTime.Now.TimeOfDay.ToString();
            currentTime = currentTime.Substring(0, currentTime.Length - 11);
            Console.WriteLine(currentTime);
            Console.WriteLine("minutes: " + TimeToMinutes(currentTime));

            AddNewOption();
            */



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(rinaList, nathanList));





            //Console.Read();
        }



        //Converts a time of day to minutes since midnight
        static int TimeToMinutes(string time)
        {

            int minutes = 60 * Int32.Parse(time.Substring(0, 2)) + Int32.Parse(time.Substring(3, 2));

            return minutes;
        }
    }
}
