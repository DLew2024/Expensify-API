namespace Expensify.DataAccessLayer.Enums;

/// <summary>
/// Represents the supported currencies within the application.
/// Values use the ISO 4217 currency codes.
/// </summary>
public enum CurrencyCode
{
    /// <summary>
    /// United States Dollar (USD).
    /// </summary>
    USD = 0,

    /// <summary>
    /// Euro (EUR).
    /// </summary>
    EUR = 1,

    /// <summary>
    /// British Pound Sterling (GBP).
    /// </summary>
    GBP = 2,

    /// <summary>
    /// Canadian Dollar (CAD).
    /// </summary>
    CAD = 3,

    /// <summary>
    /// Australian Dollar (AUD).
    /// </summary>
    AUD = 4,

    /// <summary>
    /// Japanese Yen (JPY).
    /// </summary>
    JPY = 5,

    /// <summary>
    /// Swiss Franc (CHF).
    /// </summary>
    CHF = 6,

    /// <summary>
    /// Mexican Peso (MXN).
    /// </summary>
    MXN = 7,

    /// <summary>
    /// Indian Rupee (INR).
    /// </summary>
    INR = 8,

    /// <summary>
    /// Chinese Yuan Renminbi (CNY).
    /// </summary>
    CNY = 9,
}
