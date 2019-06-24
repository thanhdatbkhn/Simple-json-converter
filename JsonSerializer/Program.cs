using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using JsonSerializer.Data;

namespace JsonSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            TestParse1();
            Console.ReadLine();
        }

        public static void TestParse1()
        {
            string json = " {'a':[1,2,3,4,'5']}";
            var obj = JToken.Parse(json);

            Console.WriteLine($"json: {obj.ToString()}");
        }
    }
}
