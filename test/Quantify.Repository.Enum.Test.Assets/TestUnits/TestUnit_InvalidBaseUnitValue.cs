using Quantify.Repository.Enum.DataAnnotation;

namespace Quantify.Repository.Enum.Test.Assets
{
    [BaseUnit(WrongUnitEnum.TestValue)]
    public enum TestUnit_InvalidBaseUnitValue
    {
        [Unit("0.000001")]
        Micrometre = 16,
        [Unit("0.001")]
        Millimetre = 17,
        [Unit("0.01")]
        Centimetre = 18,
        [Unit("0.1")]
        Decimetre = 19,
        [Unit("1")]
        Metre = 20,
        [Unit("10")]
        Decametre = 21,
        [Unit("100")]
        Hectometre = 22,
        [Unit("1000")]
        Kilometre = 23
    }

    public enum WrongUnitEnum
    {
        TestValue
    }
}