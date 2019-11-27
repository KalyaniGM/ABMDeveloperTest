using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Parse_EDIFACT_Text
    {
        static void Main(string[] args)
        {
            string edifact = @"UNA:+.? '
                             UNB+UNOC:3+2021000969+4441963198+180525:1225+3VAL2MJV6EH9IX+KMSV7HMD+CUSDECU-IE++1++1'
                             UNH+EDIFACT+CUSDEC:D:96B:UN:145050'
                             BGM+ZEM:::EX+09SEE7JPUV5HC06IC6+Z'
                             LOC+17+IT044100'
                             LOC+18+SOL'
                             LOC+35+SE'
                             LOC+36+TZ'
                             LOC+116+SE003033'
                             DTM+9:20090527:102'
                             DTM+268:20090626:102'
                             DTM+182:20090527:102'";

            var results = new List<Tuple<string, string>>();
            
            string[] lines = edifact.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(line => line.Trim().StartsWith("LOC+")).ToArray();

            foreach (string line in lines)
            {
                //remove trailing ' 
                string trimmedLine = line.Substring(0, line.IndexOf('\''));

                //split on the + 
                string[] items = trimmedLine.Split('+').ToArray();

                results.Add(new Tuple<string, string>(items[1], items[2]));
            }

            foreach (var result in results)
            {
                Console.WriteLine(result);                
            }
        }
    }
}
