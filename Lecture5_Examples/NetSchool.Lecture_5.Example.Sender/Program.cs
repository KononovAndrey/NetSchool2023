using NetSchool.Lecture_5.Example.RabbitMQ;
using System;

namespace NetSchool.Lecture_5.Example.Sender
{
    class Program
    {
        private static readonly string queueName = "LECTURE5_SAMPLE";

        static void Main(string[] args)
        {
            Console.WriteLine("SENDER");

            var rmq = new RabbitMqService();

            while (true)
            {
                Console.Write("Enter text (\"exit\" for stop): ");
                var s = Console.ReadLine();
                if (s == "exit") break;

                rmq.Publish(queueName, s);
            }
        }
    }
}
