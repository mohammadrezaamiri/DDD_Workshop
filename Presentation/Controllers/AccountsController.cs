using Domain.Account;
using MessageBus;
using Microsoft.AspNetCore.Mvc;
using Services.AccountStories;
using Services.AccountStories.OpenAccount;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IAccounts _accounts;

    public AccountsController(
        ICommandDispatcher commandDispatcher,
        IAccounts accounts)
    {
        _commandDispatcher = commandDispatcher;
        _accounts = accounts;
    }

    [HttpPost] public void OpenAccount(OpenAccountCommand command)
        => _commandDispatcher.Dispatch(command);

    [HttpGet]
    public List<Account> GetAll()
    {
        return _accounts.All();
    }
}