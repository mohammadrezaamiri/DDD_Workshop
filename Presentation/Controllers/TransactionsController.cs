using Microsoft.AspNetCore.Mvc;
using Services;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly TransactionOrchestrator _transactionsOrchestrator;

    public TransactionsController(TransactionOrchestrator transactionsOrchestrator)
    {
        _transactionsOrchestrator = transactionsOrchestrator;
    }

    [HttpPost("draft")]
    public void Draft([FromBody] DraftTransactionDto dto)
    {
        _transactionsOrchestrator.DraftTransfer(
            dto.TransactionId,
            dto.CreditAccountId,
            dto.DebitAccountId,
            dto.Amount,
            dto.TransactionDate,
            dto.Description);
    }

    [HttpPost("commit")]
    public void Commit([FromBody] CommitTransactionDto dto)
    {
        _transactionsOrchestrator.CommitTransfer(dto.TransactionId);
    }
}

public record DraftTransactionDto(
    string TransactionId,
    string CreditAccountId,
    string DebitAccountId,
    decimal Amount,
    DateTime TransactionDate,
    string Description);

public record CommitTransactionDto(string TransactionId);