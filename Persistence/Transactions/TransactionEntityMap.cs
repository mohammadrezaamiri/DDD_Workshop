using Domain.Account;
using Domain.SharedValueObject;
using Domain.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Transactions;

public class TransactionEntityMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(_ => _.Id);
        
        builder.Property(_ => _.Id)
            .HasConversion(model => 
                model.Value, value => new TransactionId(value))
            .ValueGeneratedNever();

        builder.Property(_ => _.Date).IsRequired();
        builder.Property(_ => _.Status).IsRequired();
        builder.Property(_ => _.Description).IsRequired(false);
        builder.OwnsOne(_ => _.Request, _ =>
        {
            _.Property(a => a.Amount)
                .HasConversion(
                    m => m.Value, 
                    value => new Money(value))
                .HasColumnName("Amount");
            _.OwnsOne(a => a.Parties, a =>
            {
                a.Property(b => b.CreditAccountId)
                    .HasConversion(b => b.Value,
                        value => new AccountId(value))
                    .HasColumnName("CreditAccountId");
                a.Property(b => b.DebitAccountId)
                    .HasConversion(b => b.Value,
                        value => new AccountId(value))
                    .HasColumnName("DebitAccountId");
            });
        });
    }
}