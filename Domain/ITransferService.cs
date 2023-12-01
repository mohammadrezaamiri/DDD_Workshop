using Domain.Account;
using Domain.SharedValueObject;

namespace Domain;

public interface ITransferService
{
    void Transfer(AccountId creditAccountId, AccountId debitAccountId, Money amount);
}