using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Maui.Storage;

namespace MinMaxApp
{
    internal class LocalDatabase
    {
        private const int COMPATMENT_COUNT = 8;

        private const string PREF_MED_NAME = $"comp_medName_";
        private const string PREF_MED_AMOUNT = $"comp_amount_";
        private const string PREF_REMINDER_AMOUNT = $"comp_reminderAmount_";

        public LocalDatabase()
        {
            for (int i = 0; i < COMPATMENT_COUNT; i++)
            {

            }

        }

        public Compartment GetCompartment(int id)
        {

            string medName = Preferences.Default.Get($"{PREF_MED_NAME}{id}", "Nėra");
            int amount = Preferences.Default.Get($"{PREF_MED_AMOUNT}{id}", 0);
            int reminderAmount = Preferences.Default.Get($"{PREF_REMINDER_AMOUNT}{id}", 0);

            return new Compartment(medName, amount, reminderAmount);
        }

        public void SetCompartment(int id, string sMedName, int amount, int reminderAmount)
        {
            Preferences.Default.Set($"{PREF_MED_NAME}{id}", sMedName);
            Preferences.Default.Set($"{PREF_MED_AMOUNT}{id}", amount);
            Preferences.Default.Set($"{PREF_REMINDER_AMOUNT}{id}", reminderAmount);
        }

    }
}
