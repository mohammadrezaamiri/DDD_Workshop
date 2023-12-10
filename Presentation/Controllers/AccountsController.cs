using Microsoft.AspNetCore.Mvc;
using Services.AccountStories.OpenAccount;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public AccountsController(IDispatcher dispatcher)
        => _dispatcher = dispatcher;

    [HttpPost] public void OpenAccount(OpenAccountCommand command)
        => _dispatcher.Dispatch(command);
}