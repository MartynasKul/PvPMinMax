using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace MinMaxApp
{
    internal class LocalDatabase
    {
        private const int COMPATMENT_COUNT = 8;

        private const string PREF_MED_NAME = $"comp_medName_";
        private const string PREF_MED_AMOUNT = $"comp_amount_";
        private const string PREF_REMINDER_AMOUNT = $"comp_reminderAmount_";
        private const string PREF_REMINDER_TIMES = $"comp_reminderTimes_";
        private const string PREF_REMINDER_DAYS = $"comp_reminderDays_";

        public LocalDatabase()
        {
            for (int i = 0; i < COMPATMENT_COUNT; i++)
            {

            }

        }

        public Compartment GetCompartment(int id)
        {
            string medName = Preferences.Get($"{PREF_MED_NAME}{id}", "Nėra");
            int amount = Preferences.Get($"{PREF_MED_AMOUNT}{id}", 0);
            int reminderAmount = Preferences.Get($"{PREF_REMINDER_AMOUNT}{id}", 0);

            // Retrieve times JSON string from Preferences
            string timesJson = Preferences.Get($"{PREF_REMINDER_TIMES}{id}", string.Empty);
            List<(int hour, int minute)> times = string.IsNullOrEmpty(timesJson) ? new List<(int hour, int minute)>() : JsonConvert.DeserializeObject<List<(int hour, int minute)>>(timesJson);

            // Retrieve days JSON string from Preferences
            string daysJson = Preferences.Get($"{PREF_REMINDER_DAYS}{id}", string.Empty);
            List<int> days = string.IsNullOrEmpty(daysJson) ? new List<int>() : JsonConvert.DeserializeObject<List<int>>(daysJson);

            return new Compartment(medName, amount, reminderAmount, times, days);
        }


        public void SetCompartment(int id, string sMedName, int amount, int reminderAmount, List<(int hour, int minute)> times, List<int> days)
        {
            Preferences.Default.Set($"{PREF_MED_NAME}{id}", sMedName);
            Preferences.Default.Set($"{PREF_MED_AMOUNT}{id}", amount);
            Preferences.Default.Set($"{PREF_REMINDER_AMOUNT}{id}", reminderAmount);

            Preferences.Default.Set($"{PREF_REMINDER_TIMES}{id}", JsonConvert.SerializeObject(times));
            Preferences.Default.Set($"{PREF_REMINDER_DAYS}{id}", JsonConvert.SerializeObject(days));
        }

    }
}
