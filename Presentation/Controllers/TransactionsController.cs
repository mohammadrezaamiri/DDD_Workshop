using MessageBus;
using Microsoft.AspNetCore.Mvc;
using Queries.Transactions;
using Services.TransactionStories.CommitTransfer;
using Services.TransactionStories.DraftTransfer;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;
    private readonly TransactionQueries _transactions;

    public TransactionsController(
        ICommandDispatcher dispatcher,
        TransactionQueries transactions)
    {
        _dispatcher = dispatcher;
        _transactions = transactions;
    }

    [HttpPost("draft")]
    public void Draft([FromBody] DraftTransferCommand command)
        => _dispatcher.Dispatch(command);

    [HttpPost("commit")]
    public void Commit([FromBody] CommitTransferCommand command)
        => _dispatcher.Dispatch(command);

    [HttpGet]
    public List<TransferDraftViewModel> GetAll()
        => _transactions.AllDrafts();
}