using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecoratorPattern
{
    public abstract class Pizza
    {
        public abstract decimal PizzaFiyati();
    }
java 
    class SadePizza : Pizza
    {
        public override decimal PizzaFiyati()
        {
            return 20.00m;
        }
        public override string ToString()
        {
            return "Pizza";
        }
    }