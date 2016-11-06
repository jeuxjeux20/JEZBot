// Strikes system for EZBot
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZBotWPF
{
    class Strikes
    {
        //Strike storage formats are usually like:
        //{"user",strikes}
        public static Array[] striketable = {};
        public static string[] strikepunishments = {
            "warn",
            "no-embed",
            "core",
            "ban"
        };
        public static void addstrike(string user)
        {
            foreach (string[] strikedata in striketable)
            {
                if (user == strikedata[0])
                {
                    strikedata[1] = Convert.ToString(Convert.ToInt32(strikedata[1]) + 1);
                }
                else
                {
                    striketable[striketable.Length] = new string[2] { user, "1" };
                }
            }
        }
        public static void removestrike(string user)
        {
            foreach (string[] strikedata in striketable)
            {
                if (user == strikedata[0] && Convert.ToInt32(strikedata[1]) >0)
                {
                    strikedata[1] = Convert.ToString(Convert.ToInt32(strikedata[1]) - 1);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
