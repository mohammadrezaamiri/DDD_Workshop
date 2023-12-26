namespace Queries.Transactions;

public record TransferDraftViewModel(
    string CreditAccountId,
    string DebitAccountId,
    decimal Balance,
    DateTime Date);