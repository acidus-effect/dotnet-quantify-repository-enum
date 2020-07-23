using Quantify.Repository.Enum.DataAnnotation;

namespace Quantify.Repository.Enum.Test.Assets
{
    public enum TestUnit_MissingBaseUnit
    {
        [QuantityUnit("0.001")]
        Millimetre = 17,
        [QuantityUnit("0.01")]
        Centimetre = 18,
        [QuantityUnit("0.1")]
        Decimetre = 19,
        [QuantityUnit("1")]
        Metre = 20,
        [QuantityUnit("10")]
        Decametre = 21,
        [QuantityUnit("100")]
        Hectometre = 22,
        [QuantityUnit("1000")]
        Kilometre = 23
    }
}