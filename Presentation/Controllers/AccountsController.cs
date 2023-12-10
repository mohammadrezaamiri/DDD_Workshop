using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AccountOrchestrator _accountOrchestration;

    public AccountsController(AccountOrchestrator accountOrchestration)
    {
        _accountOrchestration = accountOrchestration;
    }

    [HttpPost]
    public void OpenAccount(OpenAccountDto dto)
    {
        var (accountId, initialBalance) = dto;
        _accountOrchestration.OpenAccount(accountId, initialBalance);
    }
}

public record OpenAccountDto(string AccountId, decimal InitialBalance);