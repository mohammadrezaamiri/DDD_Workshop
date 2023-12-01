using Services.Domain.SharedValueObject;

namespace Services.Domain;

public interface ITransferService
{
    void Transfer(string creditAccountId, string debitAccountId, Money amount);
}