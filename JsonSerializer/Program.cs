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
            Console.WriteLine("Hello World!");
            string s = "123456";
            JToken j_token = (JToken)s;
            string s1 = j_token;
            float f = 124.5243423f;
            int i = (int)f; 
            Console.ReadLine();
        }
    }
}
