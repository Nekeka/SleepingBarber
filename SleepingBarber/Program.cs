using System.Threading;
namespace SleepingBarber

{
    internal class Program
    {
        static void Main(string[] args)
        {


            //// создаем новые потоки
            Thread myThread1 = new Thread(new ParameterizedThreadStart(Print));
            Thread myThread2 = new Thread(Print);
            Thread myThread3 = new Thread(message => Console.WriteLine(message));

            // запускаем потоки
            myThread1.Start("Hello");
            myThread2.Start("Hi");
            myThread3.Start("Salut");


            void Print(object? message) => Console.WriteLine(message);
        }
    }
}
