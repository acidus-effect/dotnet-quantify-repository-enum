using Quantify.Repository.Enum.DataAnnotation;

namespace Quantify.Repository.Enum.Test.Assets
{
    public enum TestUnit_MultipleBaseUnits
    {
        [Unit("0.001")]
        Millimetre = 17,
        [Unit("0.01")]
        Centimetre = 18,
        [Unit("0.1")]
        Decimetre = 19,
        [BaseUnit]
        Metre = 20,
        [Unit("10")]
        Decametre = 21,
        [BaseUnit]
        Hectometre = 22,
        [Unit("1000")]
        Kilometre = 23
    }
}