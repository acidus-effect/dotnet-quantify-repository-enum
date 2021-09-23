namespace Quantify.Repository.Enum.Test.Assets
{
    [UnitEnum(TestUnit_InvalidUnitValue.Metre)]
    public enum TestUnit_InvalidUnitValue
    {
        [Unit("0.001")]
        Millimetre = 17,
        [Unit("0.01")]
        Centimetre = 18,
        [Unit("Hello, World!")]
        Decimetre = 19,
        Metre = 20,
        [Unit("10")]
        Decametre = 21,
        [Unit("100")]
        Hectometre = 22,
        [Unit("1000")]
        Kilometre = 23
    }
}