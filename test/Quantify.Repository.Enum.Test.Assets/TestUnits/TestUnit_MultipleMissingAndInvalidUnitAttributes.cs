namespace Quantify.Repository.Enum.Test.Assets.TestUnits
{
    [UnitEnum(TestUnit_MultipleMissingAndInvalidUnitAttributes.Metre)]
    public enum TestUnit_MultipleMissingAndInvalidUnitAttributes
    {
        Millimetre = 17,
        [Unit("0.01")]
        Centimetre = 18,
        [Unit("Hello, World!")]
        Decimetre = 19,
        Metre = 20,
        [Unit("10")]
        Decametre = 21,
        [Unit("Horse")]
        Hectometre = 22,
        Kilometre = 23
    }
}