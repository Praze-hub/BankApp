using System;
using System.Collections.Generic;
using Moneybank.Models;
using Moneybank.Enum;

namespace Moneybank
{
    class Program
    {
            internal static List<Customer> customers = new List<Customer>();
            internal static int customerId  = 0;
            internal static int accountNumberSeed = 1234567890;


        static void Main(string[] args)
        {
         while(true){
             Console.WriteLine("==>Pick 1 to create an account");
             Console.WriteLine("==>Pick 2 to close acccount");
             Console.WriteLine("==>Pick 3 to check balance");
             Console.WriteLine("==>Pick 4 to credit account");
             Console.WriteLine("==>Pick 5 to transfer");
       

             try
             {
                 int response = int.Parse(Console.ReadLine());
                 if(response == 1){
                     Console.WriteLine("==>What is your name?");
                     var nameFromUser = Console.ReadLine();
                     Console.WriteLine("==>What is your address?");
                     var addressFromUser = Console.ReadLine();
                     Console.WriteLine("==>How much do you want to start with:");
                     var depositFromUser = decimal.Parse(Console.ReadLine());

                     Console.WriteLine("==>Creating account.....");
                     CreateAccount(nameFromUser,addressFromUser,depositFromUser);
                     Console.WriteLine("**Account created succesfully");
                     Console.WriteLine("**Your account number is " + accountNumberSeed);

                     continue;


                 }else if(response == 2){
                    
                     Console.WriteLine("==>What is your account number:");
                     var accountNumberFromUser = int.Parse(Console.ReadLine());
                     Console.WriteLine("==>Are you sure you want to close this account?");
                     var areYouSure = Console.ReadLine().ToLower();
                     if(areYouSure == "yes"){
                         CloseAccount(accountNumberFromUser);
                          Console.WriteLine("**Closing account.....");
                     }

                     continue;
                 }else if(response == 3){
                     Console.WriteLine("==>Check balance");
                     Console.WriteLine("==>What  is your account number?");
                     var accountNumberFromUser = int.Parse(Console.ReadLine());
                     var balance = Checkbalance(accountNumberFromUser);
                     if(balance == null){
                         Console.WriteLine("**Account does not exist");
                     }else{
                         Console.WriteLine("**Retrieving balance");
                         Console.WriteLine("**Your balance is:"+balance);
                     }
                     
                     continue;
                 }else if(response == 4){
                     Console.WriteLine("==>What is your account number");
                     var accountNumberFromUser = int.Parse(Console.ReadLine());
                     Console.WriteLine("==>How much do your want to credit your account with");
                     var amountToCredit = decimal.Parse(Console.ReadLine());
                    //  if(amountToCredit > 1){
                         CreditAccount(accountNumberFromUser,amountToCredit);
                         Console.WriteLine("**Credit successful");
                    //  }else{
                    //      Console.WriteLine("Amount must be greater than 1");
                     
                    
                     continue;


                 }else if(response == 5){
                     Console.WriteLine("==>What is your account number");
                     var accountNumberFromUser = int.Parse(Console.ReadLine());
                     Console.WriteLine("==>Enter account number to transfer to");
                     var accountToTransferTo = int.Parse(Console.ReadLine());
                     Console.WriteLine("==>How much do you want to transfer");
                     var amountToTransfer = decimal.Parse(Console.ReadLine());
                     TransferFunds(accountNumberFromUser,accountToTransferTo,amountToTransfer);


                     Console.WriteLine("**Tranfer successful");
                     continue;

                 }


                 
             }
             catch (System.Exception ex)
             {
                  Console.WriteLine("Please check value,Message"+ ex.Message);
             }
            
         }

        


         }
          static void CreateAccount(string name,string address,decimal amount){
             customerId += 1;
             accountNumberSeed +=1;

             Console.WriteLine(">>Mapping Customer");
             Customer customer = new Customer();
             customer.Id = customerId;
             customer.Name = name;
             customer.Address = address;

             Console.WriteLine(">>Mapping Account details");
             AccountDetails accountDetails = new AccountDetails();
             accountDetails.AccountNumber = accountNumberSeed;
             accountDetails.Balance = amount;
             accountDetails.AccountStatus = AccountStatus.Active;
             customer.AccountDetails = accountDetails;

             Transactions transaction = new Transactions();
             transaction.Amount = amount;
             transaction.TransactionType = TransactionType.Credit;
             transaction.DateOfTransaction = DateTime.UtcNow;
             Console.WriteLine(">>Adding Transactions");
             customer.AccountDetails.Transactions.Add(transaction);
             Console.WriteLine(">>Adding Customer");
             customers.Add(customer);





        }

           static void CloseAccount(int accountNumber){
               foreach(var customer in customers){
                   if(customer.AccountDetails.AccountNumber == accountNumber){
                       Console.WriteLine("**Account found");
                       customer.AccountDetails.AccountStatus = AccountStatus.Disabled;
                       Console.WriteLine($"**Account of {accountNumber} disabled");
                       return;
                   } 
               }
               Console.WriteLine("**Account not found");
               return;
           }

           static decimal? Checkbalance(int accountNumber){
               foreach(var customer in customers){
                   if(customer.AccountDetails.AccountNumber == accountNumber){
                       Console.WriteLine("**Account found");
                       

                       return customer.AccountDetails.Balance;
                   }
               }
               Console.WriteLine("**Account not found");
               return null;
           }

           static decimal? CreditAccount(int accountNumber,decimal amount){
               foreach(var customer in customers){
                   Console.WriteLine(">>Finding user");
                   if(customer.AccountDetails.AccountNumber == accountNumber){
                        Console.WriteLine("**Account found");
                        Console.WriteLine(">>Matching transaction");
                        customer.AccountDetails.Deposit = amount;
                        // customer.Transactions.TransactionType = TransactionType.Credit;
                        Console.WriteLine("**Matched");
                        var balanceNow = customer.AccountDetails.Balance + amount; 
                        Console.WriteLine($"**Account of {accountNumber} has been credited with  {amount} your balance is {balanceNow}");
                    
                        return amount;
                   }
               }
               Console.WriteLine("**Account not found");
               return null;
           }
           static decimal? TransferFunds(int accountNumber,int accountTo,decimal amount){
               foreach(var customer in customers){
                   Console.WriteLine(">>Finding users");
                   
                   if(customer.AccountDetails.AccountNumber == accountNumber){
                    Console.WriteLine("**Account found");
                    foreach(var customerto in customers){
                        Console.WriteLine("**Findimg users");
                        if(customerto.AccountDetails.AccountNumber == accountTo){
                            Console.WriteLine("**User account found proceed with transfer");
                            customerto.AccountDetails.Deposit = amount;
                            Console.WriteLine(">>Matched");
                            var balanceNow = customerto.AccountDetails.Balance += amount; 
                            Console.WriteLine($"**Account of {accountNumber} has been credited with  {amount} your balance is {balanceNow}");
                    
                            return amount;


                        }
                    } 
                    Console.WriteLine("**Account not found");
                    return null;
                //    return amount;
                   }
               }
                  Console.WriteLine("**Account not found");
                    return null;

           }
    }
}
