using System;
using System.IO;
using Task_4.BiryuzovayaZhemchuzina;

namespace Task_4
{
    internal class Program
    {
        readonly static string path = @"D:\Системные значки\Новая папка — копия\Task_4.txt";
        readonly static string url =  "https://2.ac-biryuzovaya-zhemchuzhina.ru/flats/all?floor=&type=&status=&minArea=&maxArea=&minPrice=&maxPrice=";


    static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            ParserWorker<string[]> parser = new ParserWorker<string[]>(new BZParser(), url);
            parser.OnCompleted += Parser_OnCompleted;
            parser.Worker();
            Console.ReadLine();
        }

        private static void Parser_OnCompleted(object arg1, string[] arg2)
        {
            WriteData(arg2);
            Console.WriteLine("Успешно");
        }

        private static void WriteData(string[] arg)
        {
            StreamWriter sw = new StreamWriter(path, false);

            foreach (string item in arg)
            {
                sw.WriteLine(item);
                //Console.WriteLine(item);
            }
            sw.Close();
        }
    }
}
