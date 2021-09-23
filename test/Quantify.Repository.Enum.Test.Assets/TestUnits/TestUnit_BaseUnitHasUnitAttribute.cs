﻿namespace Quantify.Repository.Enum.Test.Assets
{
    [BaseUnit(TestUnit.Metre)]
    public enum TestUnit_BaseUnitHasUnitAttribute
    {
        [Unit("0.000001")]
        Micrometre = 16,
        [Unit("0.001")]
        Millimetre = 17,
        [Unit("0.01")]
        Centimetre = 18,
        [Unit("0.1")]
        Decimetre = 19,
        [Unit("1337")]
        Metre = 20,
        [Unit("10")]
        Decametre = 21,
        [Unit("100")]
        Hectometre = 22,
        [Unit("1000")]
        Kilometre = 23
    }
}