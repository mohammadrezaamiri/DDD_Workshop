using Services.Domain.Account;
using Services.Domain.SharedValueObject;

namespace Services.Domain;

public interface ITransferService
{
    void Transfer(AccountId creditAccountId, AccountId debitAccountId, Money amount);
}