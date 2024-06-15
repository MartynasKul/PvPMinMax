using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MinMaxApp
{
    internal class Compartment
    {
        public string medName { get; set; } // vaisto pavadinimas
        public int amount { get; set; } // geriamu vaistu kiekis
        public int reminderAmount { get; set; }// priminimu kiekis
        public List<(int hour, int minute)> TimeAmounts = new List<(int hour, int minute)>(); // laiko pridejimas
        public List<int> Days = new List<int>();

        // Constructor
        public Compartment(string name, int amount, int reminderAmount, List<(int hour, int minute)> times, List<int> days)
        {
            this.medName = name;
            this.amount = amount;
            this.reminderAmount = reminderAmount;
            this.TimeAmounts = times;
            this.Days = days;
        }

        public Compartment(string name, int amount, int reminderAmount)
        {
            this.medName = name;
            this.amount = amount;
            this.reminderAmount = reminderAmount;
        }

        // Method to add time amount to the list
        public void AddTimeAmount(int hour, int minute)
        {
            TimeAmounts.Add((hour, minute));
        }

        // Method to display compartment information
        public void DisplayCompartmentInfo()
        {
            Console.WriteLine($"Name: {medName}");
            Console.WriteLine($"Amount: {amount}");
            Console.WriteLine($"Reminder Amount: {reminderAmount}");
            Console.WriteLine("Time Amounts:");
            foreach (var timeAmount in TimeAmounts)
            {
                Console.WriteLine($"- {timeAmount.hour}:{timeAmount.minute}");
            }
        }

        public override string ToString()
        {
            if (this.amount <= 0)
                return "+";

            string spelling = "kartą";

            if (this.reminderAmount > 1 && this.reminderAmount < 10)
                spelling = "kartus";
         

            return $"{this.medName}\nKiekis: {this.amount}\n{this.reminderAmount} {spelling} per dieną";
        }
    }
}
