using FluentMigrator;

namespace App.Migration;

[Migration(202312292353)]
public class _202312292353_InitialTables : FluentMigrator.Migration 
{
    public override void Up()
    {
        Create.Table("Accounts")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey()
            .WithColumn("Balance").AsDecimal().NotNullable();

        Create.Table("Transactions")
            .WithColumn("Id").AsString().NotNullable().PrimaryKey()
            .WithColumn("Date").AsDateTime2().NotNullable()
            .WithColumn("Status").AsInt32().NotNullable()
            .WithColumn("Description").AsString().Nullable()
            .WithColumn("Amount").AsDecimal().NotNullable()
            .WithColumn("CreditAccountId").AsString().NotNullable()
            .WithColumn("DebitAccountId").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Transactions");
        Delete.Table("Accounts");
    }
}