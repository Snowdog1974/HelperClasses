using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClasses
{
    public class OrganisationNoHelper
    {
            /// <summary>
            /// Kontrollera att kontrollnumret i angivet identifikationsnummer är korrekt.
            /// </summary>
            /// <param name="idNumber">Id-nummer inklusive kontrollsiffra.</param>
            /// <returns>True/False</returns>
            public static bool IsValidIdNr(string idNumber)
            {
                bool result= false;
                if (string.IsNullOrEmpty(idNumber))
                {
                    //throw new FormatException("Not a valid Id Number");
                    result = false;
                }
                if (idNumber.IndexOf('-') != -1)
                    idNumber = idNumber.Replace("-", string.Empty);

                if (idNumber.IndexOf(' ') != -1)
                    idNumber = idNumber.Replace(" ", string.Empty);

                if (idNumber.Length == 12)
                    idNumber = idNumber.Remove(0, 2);

                else if (idNumber.Length != 10)
                {
                    //throw new FormatException("Not a valid Id Number");
                    result = false;
                }
                else
                {
                    result = (CalculateIdNrControlNumber(idNumber.Substring(0, 9)) == ToInt(idNumber[idNumber.Length - 1]));
                
                }

                return result;
            }


            /// <summary>
            /// Beräkna kontrollsiffran i ett identifikationsnummer.
            /// </summary>
            /// <param name="idNumber">Id-nummer exklusive kontrollsiffra.</param>
            /// <returns>Kontrollsiffran för id-numret.</returns>
            private static int CalculateIdNrControlNumber(string idNumber)
            {
                int sum = 0;
                for (int i = 0; i < idNumber.Length; i++)
                {
                    int number = ToInt(idNumber[i]) * (1 + ((i + 1) % 2));

                    foreach (char c in number.ToString())
                        sum += ToInt(c);
                }

                string szSum = sum.ToString();
                int lastNum = ToInt(szSum[szSum.Length - 1]);

                return (lastNum != 0) ? 10 - lastNum : 0;
            }


            public static int ToInt(char chr)
            {
                return Convert.ToInt32(char.GetNumericValue(chr));
            }

        
    }
}


