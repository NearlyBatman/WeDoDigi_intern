using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeDoDigi_intern.Models
{
    public class CountHolder
    {
        // Objekt der holder base64 stringen fra viewet og en counter 
        // der bestemmer hvor mange iteration vi har været igennem
        public string imageString { get; set; }
        public int intCounter { get; set; }

        public string SetImgString(int counter, string imgString)
        {
            string message = "Please try again";

            switch (counter)
            {
                case 1:
                    break;
            }

            return message;
        }
    }
}
