using Domain.Transaction;
using MessageBus;
using Microsoft.AspNetCore.Mvc;
using Services.TransactionStories;
using Services.TransactionStories.CommitTransfer;
using Services.TransactionStories.DraftTransfer;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;
    private readonly ITransactions _transactions;

    public TransactionsController(
        ICommandDispatcher dispatcher,
        ITransactions transactions)
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
    public List<Transaction> GetAll()
        => _transactions.All().ToList();
}