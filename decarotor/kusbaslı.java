class KusBasiEtliPizza : Decorator
    {
        public KusBasiEtliPizza(Pizza pz)
            : base(pz)
        {
        }
        public override decimal PizzaFiyati()
        {
            return base.PizzaFiyati() + 3.50m;
        }
        public override string ToString()
        {
            return "Kuþ Baþý Etli " + base.pizza.ToString();
        }
    }
}