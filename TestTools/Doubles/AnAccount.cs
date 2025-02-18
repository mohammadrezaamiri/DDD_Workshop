using Domain.Account;
using Domain.SharedValueObject;

namespace TestTools.Doubles;

public class AnAccount
{
   private AccountId _id = new (Guid.NewGuid().ToString());
   private Money _balance = 0;
   
   public AnAccount WithId(AccountId id)
   {
      _id = id;
      return this;
   }

   public AnAccount WithBalance(Money balance)
   {
      _balance = balance;
      return this;
   }

   public Account Please()
      => new (_id, _balance);
     
}