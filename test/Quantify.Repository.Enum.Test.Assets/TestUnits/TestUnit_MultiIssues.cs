﻿using Quantify.Repository.Enum.DataAnnotation;

namespace Quantify.Repository.Enum.Test.Assets
{
    public enum TestUnit_MultiIssues
    {
        [Unit("0.001")]
        Millimetre = 17,
        Centimetre = 18,
        [Unit("0.1")]
        Decimetre = 19,
        [Unit("1")]
        [BaseUnit]
        Metre = 20,
        [Unit("10")]
        Decametre = 21,
        [Unit("100")]
        Hectometre = 22,
        [Unit("1000")]
        Kilometre = 23
    }
}