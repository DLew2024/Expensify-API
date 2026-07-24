using Expensify.DataAccessLayer.Entities.Models.ReferenceDataSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Expensify.DataAccessLayer.Utility.Constants;

namespace Expensify.DataAccessLayer.Configurations.ReferenceDataSchema;

public class CurrencyCodeConfiguration : IEntityTypeConfiguration<CurrencyCode>
{
    public void Configure(EntityTypeBuilder<CurrencyCode> builder)
    {
        // Maps currency codes to the reference_data schema.
        builder.ToTable("currency_codes", "reference_data");

        // Configures the primary key.
        builder.HasKey(currency => currency.Id).HasName("pk_currency_codes");

        // Configures the display name of the currency.
        builder
            .Property(currency => currency.Name)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.Name);

        // Configures the ISO-style currency code.
        // Example: USD, EUR, GBP.
        builder
            .Property(currency => currency.Code)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.CurrencyCode);

        // Configures the currency display symbol.
        // Example: $, €, £.
        builder
            .Property(currency => currency.Symbol)
            .IsRequired()
            .HasMaxLength(DatabaseLengths.CurrencySymbol);

        // Configures the number of decimal places normally used by the currency.
        // Example: USD uses 2 while JPY uses 0.
        builder.Property(currency => currency.DecimalPlaces).IsRequired();

        // Configures whether the currency is available for use.
        builder.Property(currency => currency.IsActive).IsRequired().HasDefaultValue(true);

        // Ensures each currency code is unique.
        builder
            .HasIndex(currency => currency.Code)
            .IsUnique()
            .HasDatabaseName("uq_currency_codes_code");

        // Seeds supported system currencies.
        builder.HasData(
            new CurrencyCode
            {
                Id = Guid.Parse("16628839-2150-475b-9108-8c223e60c0a3"),
                Name = "United States Dollar",
                Code = "USD",
                Symbol = "$",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("f0dbd1c0-4663-437b-a4e9-6098712c5ede"),
                Name = "Euro",
                Code = "EUR",
                Symbol = "€",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("4c5021eb-a30f-4446-a759-e2a30d814fb3"),
                Name = "British Pound Sterling",
                Code = "GBP",
                Symbol = "£",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("f654313c-d52f-4574-8ebd-beb68f18d4cb"),
                Name = "Canadian Dollar",
                Code = "CAD",
                Symbol = "$",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("87ee8fa5-2031-4730-bdbf-0a0998ac6ab3"),
                Name = "Australian Dollar",
                Code = "AUD",
                Symbol = "$",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("24144803-4020-4dc1-9765-f433bc4dd7c9"),
                Name = "Japanese Yen",
                Code = "JPY",
                Symbol = "¥",
                DecimalPlaces = 0,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("c03746aa-8504-4425-b16e-181de3400c3e"),
                Name = "Swiss Franc",
                Code = "CHF",
                Symbol = "CHF",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("7ec564e1-4de2-4cfb-9d42-d4cc5b743cb8"),
                Name = "Mexican Peso",
                Code = "MXN",
                Symbol = "$",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("1f78aa2a-b6a4-4d43-91bf-31b2a977b842"),
                Name = "Indian Rupee",
                Code = "INR",
                Symbol = "₹",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            },
            new CurrencyCode
            {
                Id = Guid.Parse("b7ce746f-f54c-4517-9bf4-ddaa76b363ef"),
                Name = "Chinese Yuan Renminbi",
                Code = "CNY",
                Symbol = "¥",
                DecimalPlaces = 2,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                LastUpdatedBy = Guid.Empty,
                CreateDate = 0,
                UpdatedDate = 0,
            }
        );
    }
}
