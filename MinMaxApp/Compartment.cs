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
        public List<(int hour, int minute)> TimeAmounts { get; set; } // laiko pridejimas

        // Constructor
        public Compartment(string name, int amount, int reminderAmount)
        {
            this.medName = name;
            this.amount = amount;
            this.reminderAmount = reminderAmount;
            this.TimeAmounts = new List<(int hour, int minute)>();
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

        public override string ToString() // isvedimui i faila
        {
            string line = "";
            line += medName + ";" + amount + ";" + reminderAmount + ";";
            foreach (var timeAmount in TimeAmounts) 
            {
                line += timeAmount.hour + ";" + timeAmount.minute +";";
            }
            return line;
        }
    }
}
