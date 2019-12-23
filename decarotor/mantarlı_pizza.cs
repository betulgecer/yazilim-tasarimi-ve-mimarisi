class MantarliPizza : Decorator
   {
       public MantarliPizza(Pizza pz)
           : base(pz)
       {
       }
       public override decimal PizzaFiyati()
       {
           return base.PizzaFiyati() + 1.50m;
       }
       public override string ToString()
       {
           return "Mantarl� " + base.pizza.ToString();
       }
   }