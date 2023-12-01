using Domain;
using Domain.Account;
using Domain.Exceptions;
using Domain.SharedValueObject;

namespace Services;

public class TransferService : ITransferService
{
    readonly Accounts _accounts;

    public TransferService(Accounts accounts)
        => _accounts = accounts;

    public void Transfer(
        AccountId creditAccountId, 
        AccountId debitAccountId,
        Money amount)
    {
        var creditAccount = _accounts.FindById(creditAccountId);
        var debitAccount = _accounts.FindById(debitAccountId);

        if (debitAccount is null)
        {
            debitAccount = new Account(debitAccountId, 0);
            _accounts.Add(debitAccount);
        }

        if(creditAccount is null) 
            throw new CreditAccountNotFoundException();

        creditAccount.Credit(amount);
        debitAccount.Debit(amount);

        _accounts.Update(creditAccount);
        _accounts.Update(debitAccount);
    }
}