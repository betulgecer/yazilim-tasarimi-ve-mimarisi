class KiymaliPizza : Decorator
   {
       public KiymaliPizza(Pizza pz)
           : base(pz)
       {
       }
       public override decimal PizzaFiyati()
       {
           return base.PizzaFiyati() + 2.00m;
       }
       public override string ToString()
       {
           return "K�ymal� " + base.pizza.ToString();
       }
   }