namespace Moneybank.Models{
    public class Customer{
        public int Id{get; set;}

        public string Name {get; set;}

        public string Address {get; set;}

        public AccountDetails AccountDetails {get; set;}

        public Transactions Transactions {get; set;}

        
    }
}