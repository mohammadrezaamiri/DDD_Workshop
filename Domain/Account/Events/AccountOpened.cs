namespace Domain.Account.Events;

public record AccountOpened(string AccountId, decimal Amount) : IDomainEvent;