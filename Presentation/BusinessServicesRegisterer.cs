using Domain;
using Domain.Account.Events;
using MessageBus;
using MessageBus.DomainEventsBus;
using Persistence;
using Persistence.Accounts;
using Persistence.Transactions;
using Queries;
using Queries.Accounts;
using Queries.Transactions;
using Services;
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
        services.AddScoped<EFWriteDataContext>();
        services.AddScoped<EFReadDataContext>();
        services.AddScoped<IAccounts, AccountRepository>();
        services.AddScoped<ITransactions, TransactionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddScoped
            <ICommandHandler<OpenAccountCommand>,
                OpenAccountCommandHandler>();
        services.AddScoped
        <ICommandHandler<DraftTransferCommand>,
            DraftTransferCommandHandler>();
        services.AddScoped
        <ICommandHandler<CommitTransferCommand>,
            CommitTransferCommandHandler>();
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped
        <IDomainEventHandler<AccountOpened>, AuditorService>();
        services.AddScoped
            <IDomainEventHandler<AccountOpened>, AnotherAuditorService>();
    }

    public static void RegisterQueries(this IServiceCollection services)
    {
        services.AddScoped<AccountQueries>();
        services.AddScoped<TransactionQueries>();
    }

    public static void RegisterDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ITransferService, TransferService>();
    }
}