using MessageBus;

namespace Services.TransactionStories.DraftTransfer;

public record DraftTransferCommand(
    string TransactionId,
    string CreditAccountId,
    string DebitAccountId,
    decimal Amount,
    DateTime TransactionDate,
    string Description) : ICommand;