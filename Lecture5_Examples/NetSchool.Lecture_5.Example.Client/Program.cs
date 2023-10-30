using NetSchool.Lecture_5.Example.RabbitMQ;
using System;
using System.Text;
using System.Threading;

namespace NetSchool.Lecture_5.Example.Client
{
    class Program
    {
        private static readonly string queueName = "LECTURE5_SAMPLE";

        static void Main(string[] args)
        {
            Console.WriteLine("CLIENT");

            var rmq = new RabbitMqService();
            rmq.RegisterListener(queueName, onReceive: (sender, arg) =>
            {
                var message = Encoding.UTF8.GetString(arg.Body.ToArray());
                Console.Write($"Data: {message}... ");
                Thread.Sleep(1000);
                Console.WriteLine($"done");
            });


            while (true)
            {
                Console.WriteLine("Enter text (\"exit\" for stop): ");
                var s = Console.ReadLine();
                if (s == "exit") break;
            }
        }
    }
}
