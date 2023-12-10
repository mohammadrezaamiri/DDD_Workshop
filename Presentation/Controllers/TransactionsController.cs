using Microsoft.AspNetCore.Mvc;
using Services.TransactionStories.CommitTransfer;
using Services.TransactionStories.DraftTransfer;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public TransactionsController(IDispatcher dispatcher)
        => _dispatcher = dispatcher;

    [HttpPost("draft")]
    public void Draft([FromBody] DraftTransferCommand command)
        => _dispatcher.Dispatch(command);

    [HttpPost("commit")]
    public void Commit([FromBody] CommitTransferCommand command)
        => _dispatcher.Dispatch(command);
}