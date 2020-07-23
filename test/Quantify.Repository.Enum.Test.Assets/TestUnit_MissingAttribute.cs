using Quantify.Repository.Enum.DataAnnotation;

namespace Quantify.Repository.Enum.Test.Assets
{
    public enum TestUnit_MissingAttribute
    {
        [QuantityUnit("0.001")]
        Millimetre = 17,
        [QuantityUnit("0.01")]
        Centimetre = 18,
        Decimetre = 19,
        [QuantityBaseUnit]
        Metre = 20,
        [QuantityUnit("10")]
        Decametre = 21,
        [QuantityUnit("100")]
        Hectometre = 22,
        [QuantityUnit("1000")]
        Kilometre = 23
    }
}