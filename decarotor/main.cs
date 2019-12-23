using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecoratorPattern
{
    class Client
    {
        public static void Main(String []args)
        {
            SadePizza safPizza = new SadePizza();
            Console.WriteLine(safPizza.ToString());
            Console.WriteLine(safPizza.PizzaFiyati());
            Console.WriteLine();

            MantarliPizza mp = new MantarliPizza(safPizza);
            Console.WriteLine(mp.ToString());
            Console.WriteLine(mp.PizzaFiyati());
            Console.WriteLine();

            KiymaliPizza kp = new KiymaliPizza(safPizza);
            Console.WriteLine(kp.ToString());
            Console.WriteLine(kp.PizzaFiyati());
            Console.WriteLine();

            KusBasiEtliPizza kbep = new KusBasiEtliPizza(safPizza);
            Console.WriteLine(kbep.ToString());
            Console.WriteLine(kbep.PizzaFiyati());
            Console.WriteLine();

            KiymaliPizza kiymaliMantarliPizza = new KiymaliPizza(mp);
            Console.WriteLine(kiymaliMantarliPizza.ToString());
            Console.WriteLine(kiymaliMantarliPizza.PizzaFiyati());
            Console.ReadKey();
        }
    }
}