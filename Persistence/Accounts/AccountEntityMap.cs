using Domain.Account;
using Domain.SharedValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Accounts;

public class AccountEntityMap : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(_ => _.Id);
        
        builder.Property(_ => _.Id)
            .HasConversion(model => 
                model.Value, value => new AccountId(value))
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(_ => _.Balance)
            .HasConversion(
                m => m.Value,
                value => new Money(value)).IsRequired();
    }
}