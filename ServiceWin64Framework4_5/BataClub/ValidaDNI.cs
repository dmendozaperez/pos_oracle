using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BataClub_DNI
{
    public class ValidaDNI
    {
        public  bool ValidateDocument(string identificationDocument)
        {
            if (identificationDocument.Trim().Length != 8) return true;

            string numString = identificationDocument; //"1287543.0" will return false for a long
            long number1 = 0;
            bool canConvert = long.TryParse(numString, out number1);
            if (canConvert == true)            
                return false;
            else
                return true;
            
        }
    }
}
