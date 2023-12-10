namespace Services.AccountStories.OpenAccount;

public record OpenAccountCommand(string AccountId, decimal InitialBalance);