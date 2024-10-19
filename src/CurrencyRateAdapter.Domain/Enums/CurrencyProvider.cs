using System.Runtime.Serialization;

namespace CurrencyRateAdapter.Domain.Enums
{
    public enum CurrencyProvider
    {
        [EnumMember(Value = nameof(NationalBankOfMoldova))]
        NationalBankOfMoldova,
    }
}
