using MessageBus;

namespace Services.TransactionStories.CommitTransfer;

public record CommitTransferCommand(string TransactionId) : ICommand;