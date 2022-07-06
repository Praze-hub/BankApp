using System;
using Moneybank.Enum;

namespace Moneybank.Models{

    public class Transactions{
        public decimal Amount {get; set;}

        public TransactionType TransactionType {get; set;}

        public DateTime DateOfTransaction {get; set;}
    }
}