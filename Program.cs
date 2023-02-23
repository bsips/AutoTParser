using System;
using System.Collections.Generic;
using System.IO;

namespace AutoTParser
{
    class Program
    {
        static void Main()
        {
            const string file = "auto_translates.sql";
            AutoTranslate.Build();

            foreach (KeyValuePair<uint, string> item in AutoTranslate.list)
            {
                File.AppendAllText(file, $"INSERT INTO 'auto_translates' VALUES ({item.Key}, \"{item.Value}\") \n");
            }
            Console.WriteLine($"Completed exporting to {file}");
            Console.ReadKey();
        }
    }
}
