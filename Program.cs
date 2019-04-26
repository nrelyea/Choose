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


            ///*
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
                "Burrito",
                "Bagel",
                "Italian"

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
                "Burrito",
                "Bagel",
                "Italian"

            };
            //*/

            /*
            List<string> rinaList = new List<string> {
                "Thai",
                "Pizza",
                "Vegan",
                "Italian",
                "Mexican"              
            };

            List<string> nathanList = new List<string> {
                "Mexican",
                "Pizza",
                "Italian",
                "Thai",
                "Vegan"                             
            };
            */

            if (read)
            {
                string json = File.ReadAllText(@"c:../../rinaList.json");
                rinaList = JsonConvert.DeserializeObject<List<string>>(json);
                json = File.ReadAllText(@"c:../../nathanList.json");
                nathanList = JsonConvert.DeserializeObject<List<string>>(json);
            }





            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(rinaList, nathanList));


            //List<string> combinedList = CombineLists(rinaList, nathanList);


            //Console.Read();
        }

    }

    public class CombinedItem
    {
        public string Option { get; set; }
        public int Score { get; set; }
        public CombinedItem(string option, int score)
        {
            Option = option;
            Score = score;
        }
    }
}
