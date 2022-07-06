using System.Collections.Generic;
using Moneybank.Enum;

namespace Moneybank.Models{

    public class AccountDetails{
        public int AccountNumber{get; set;}
        public decimal Balance{get; set;}
        public decimal Deposit {get; set;}
        public List<Transactions> Transactions {get; set;} = new List<Transactions>();
        public AccountStatus AccountStatus {get; set;}
    }
    


}