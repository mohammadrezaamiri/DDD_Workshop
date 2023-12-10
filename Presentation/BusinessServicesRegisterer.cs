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
            <IMessageHandler<OpenAccountCommand>,
                OpenAccountCommandHandler>();
        services.AddTransient
        <IMessageHandler<DraftTransferCommand>,
            DraftTransferCommandHandler>();
        services.AddTransient
        <IMessageHandler<CommitTransferCommand>,
            CommitTransferCommandHandler>();
        services.AddSingleton<IDispatcher, Dispatcher>();
    }

    public static void RegisterDomainServices(this IServiceCollection services)
    {
        services.AddTransient<ITransferService, TransferService>();
    }
}