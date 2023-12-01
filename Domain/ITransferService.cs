using Domain.SharedValueObject;

namespace Domain;

public interface ITransferService
{
    void Transfer(TransactionParties parties, Money amount);
}