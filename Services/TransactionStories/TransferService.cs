using Domain;
using Domain.Account;
using Domain.Exceptions;
using Domain.SharedValueObject;
using Services.AccountStories;

namespace Services.TransactionStories;

public class TransferService : ITransferService
{
    readonly IAccounts _accounts;

    public TransferService(IAccounts accounts)
        => _accounts = accounts;

    public void Transfer(
        TransferRequest request)
    {
        var creditAccount = _accounts.FindById(request.Parties.CreditAccountId);
        var debitAccount = _accounts.FindById(request.Parties.DebitAccountId);

        if (debitAccount is null)
        {
            debitAccount = new Account(request.Parties.DebitAccountId, 0);
            _accounts.Add(debitAccount);
        }

        if(creditAccount is null) 
            throw new CreditAccountNotFoundException();

        creditAccount.Credit(request.Amount);
        debitAccount.Debit(request.Amount);

        _accounts.Update(creditAccount);
        _accounts.Update(debitAccount);
    }
}