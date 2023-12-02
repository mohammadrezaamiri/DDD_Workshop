using Domain.SharedValueObject;

namespace Domain;

public interface ITransferService
{
    void Transfer(TransferRequest request);
}