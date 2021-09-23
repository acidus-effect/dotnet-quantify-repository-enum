namespace Quantify.Repository.Enum.Test.Assets
{
    [BaseUnit(TestUnit.Metre)]
    public enum TestUnit
    {
        [Unit("10000")]
        Micrometre = 16,
        [Unit("1000")]
        Millimetre = 17,
        [Unit("100")]
        Centimetre = 18,
        [Unit("10")]
        Decimetre = 19,
        Metre = 20,
        [Unit("0.1")]
        Decametre = 21,
        [Unit("0.01")]
        Hectometre = 22,
        [Unit("0.001")]
        Kilometre = 23
    }
}