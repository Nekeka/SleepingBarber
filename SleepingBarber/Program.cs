using System.Threading;
namespace SleepingBarber

{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            BarberShop bs = new BarberShop(10);
            Thread barber = new Thread(bs.BarberWork);
            barber.Start();
            for (int i = 1; i < bs.maxSeats; i++)
            {
                Thread customer = new Thread(bs.CustomerWait);
                customer.Name = "Customer " + i;
                customer.Start();
                Thread.Sleep(new Random().Next(1000, 5000));
            }
              
        }
    }

    internal class BarberShop
    {
        public int maxSeats = 0; //колво мест 
        int waiting = 0; //колво ожидающих

        Semaphore customers = new Semaphore(0,100);
        Semaphore barbers = new Semaphore(0,1);
        
        Mutex seat = new Mutex();

        public BarberShop(int maxSeats)
        {
            this.maxSeats = maxSeats;
        }

        public void BarberWork()
        {
            while (true)
            {
                customers.WaitOne();
                seat.WaitOne();
                waiting--;


                barbers.Release();
                seat.ReleaseMutex();

                Thread.Sleep(new Random().Next(1000, 3000));
                Console.WriteLine("Барбер закінчив із клієнтом");
            }
        }

        public void CustomerWait()
        {
            seat.WaitOne();
            if (waiting < maxSeats)
            {
                waiting++;
                Console.WriteLine($"клієнт {Thread.CurrentThread.Name} зайшов. Очікують: {waiting}");

                customers.Release();
                seat.ReleaseMutex();
                barbers.WaitOne();

                Console.WriteLine($"{Thread.CurrentThread.Name} стріжеться.");

            }
        }
    }
}
