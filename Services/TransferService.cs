using Domain;
using Domain.Account;
using Domain.Exceptions;
using Domain.SharedValueObject;

namespace Services;

public class TransferService : ITransferService
{
    readonly IAccounts _accounts;

    public TransferService(IAccounts accounts)
        => _accounts = accounts;

    public void Transfer(
        TransactionParties parties,
        Money amount)
    {
        var creditAccount = _accounts.FindById(parties.CreditAccountId);
        var debitAccount = _accounts.FindById(parties.DebitAccountId);

        if (debitAccount is null)
        {
            debitAccount = new Account(parties.DebitAccountId, 0);
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