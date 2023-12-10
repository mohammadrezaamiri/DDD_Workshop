using Domain;
using Persistence.InMemory;
using Services;

namespace Presentation;

public static class BusinessServicesRegisterer
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccounts, Accounts>();
        services.AddScoped<ITransactions, Transactions>();
    }

    public static void RegisterOrchestrations(this IServiceCollection services)
    {
        services.AddScoped<AccountOrchestrator>();
        services.AddScoped<TransactionOrchestrator>();
    }
    
    public static void RegisterDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ITransferService, TransferService>();
    }
}