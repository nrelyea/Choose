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

            List<string> maybeList = new List<string> {
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
                string json = File.ReadAllText(@"c:../../maybeList.json");
                maybeList = JsonConvert.DeserializeObject<List<string>>(json);
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(maybeList));







            //Console.Read();
        }
    }
}
