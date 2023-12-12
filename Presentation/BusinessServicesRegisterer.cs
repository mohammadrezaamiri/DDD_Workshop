using Domain;
using MessageBus;
using Persistence.InMemory;
using Services.AccountStories;
using Services.AccountStories.OpenAccount;
using Services.TransactionStories;
using Services.TransactionStories.CommitTransfer;
using Services.TransactionStories.DraftTransfer;

namespace Presentation;

public static class BusinessServicesRegisterer
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IAccounts, Accounts>();
        services.AddSingleton<ITransactions, Transactions>();
    }

    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddTransient
            <ICommandHandler<OpenAccountCommand>,
                OpenAccountCommandHandler>();
        services.AddTransient
        <ICommandHandler<DraftTransferCommand>,
            DraftTransferCommandHandler>();
        services.AddTransient
        <ICommandHandler<CommitTransferCommand>,
            CommitTransferCommandHandler>();
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
    }

    public static void RegisterDomainServices(this IServiceCollection services)
    {
        services.AddTransient<ITransferService, TransferService>();
    }
}