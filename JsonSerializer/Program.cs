using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using JsonSerializer.Data;
using System.Diagnostics;

namespace JsonSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var cmd = Console.ReadLine();
                //TestParse3(cmd);
                
                TestParse4(cmd);
                TestParse2(cmd);
            }
            Console.ReadLine();
        }

        public static void TestParse1()
        {
            string json = "{'a':[1,2,3,4,'5'],\n 'b': null}";
            var obj = JToken.Parse(json);

            Console.WriteLine($"json: {obj.ToString()}");
        }

        public static void TestParse2(string json)
        {
            //string json = "{'a':[1,2,3,4,'5'], 'b': null}";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //for (int i = 0; i < 1000; i++)
            //{
            json = json;
            var obj = JToken.Parse(json);
            //}
            stopwatch.Stop();
            Console.WriteLine($"1000 times json parse: {stopwatch.Elapsed}");
            Console.WriteLine($"json parse: {obj.ToString()}");
        }

        public static void TestParse3(string json)
        {
            //string json = "{'a':[1,2,3,4,'5'], 'b': null}";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //for (int i = 0; i < 1000; i++)
            //{
            json = json;
            var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
            //}
            stopwatch.Stop();
            Console.WriteLine($"1000 times Newtonsoft json parse: {stopwatch.Elapsed}");
        }

        public static void TestParse4(string json)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //for (int i = 0; i < 1000; i++)
            //{
            json = json;
            var obj = JToken.ParseNew(json);
            //}
            stopwatch.Stop();
            Console.WriteLine($"1000 times json parse new: {stopwatch.Elapsed}");
        }
    }
}
