using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace program
{
    class Program
    {
        private DateTime firstDate, secondDate;

        private void CheckDatesOrder()
        {
            if (firstDate.Date > secondDate.Date)
            {
                DateTime tempDate;

                tempDate = firstDate;
                firstDate = secondDate;
                secondDate = tempDate;
            }
        }

        public string GetDateRange(string firstDateString, string secondDateString)
        {
            string datePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

            if (DateTime.TryParseExact(firstDateString, datePattern, CultureInfo.CurrentCulture, DateTimeStyles.None, out firstDate) &&
                DateTime.TryParseExact(secondDateString, datePattern, CultureInfo.CurrentCulture, DateTimeStyles.None, out secondDate))
            {
                CheckDatesOrder();

                if (firstDate.Year == secondDate.Year)
                {
                    if (firstDate.Month == secondDate.Month)
                    {
                        Match m = Regex.Match(datePattern, @"d+", RegexOptions.IgnoreCase);
                        return string.Format("{0} - {1}", firstDate.ToString(m.Value), secondDate.ToShortDateString());
                    }
                    else
                    {
                        Match m = Regex.Match(datePattern, @"[m|d]+\W+[m|d]+", RegexOptions.IgnoreCase);
                        return string.Format("{0} - {1}", firstDate.ToString(m.Value), secondDate.ToShortDateString());
                    }
                }
                else
                {
                    return string.Format("{0} - {1}", firstDate.ToShortDateString(), secondDate.ToShortDateString());
                }
            }
            else
            {
                return string.Format("Blad! Sprawdz podane daty.\nFormat obslugiwany na twoim komputerze to: {0} ", datePattern);
            }
        }

        static void Main(string[] args)
        {

            if(args.Length == 2)
            {
		Program program = new Program();
                Console.WriteLine(program.GetDateRange(args[0], args[1]));
            }
            else
            {
                Console.WriteLine("Podaj dwie daty!");
            }

            Console.ReadKey();
        }
    }
}