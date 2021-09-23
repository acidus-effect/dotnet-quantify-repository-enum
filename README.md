# Quantify.Repository.Enum ![ci branch parameter](https://github.com/acidicsoftware/dotnet-quantify-repository-enum/workflows/Continuous%20Integration/badge.svg?branch=trunk)

<img src="assets/quantify-repository-enum-logo.png" height="180px" width="180px" align="center" />

If your unit values are static, universal constants, like length and weight units, and if a solution where unit values are represented by an enum suits your needs, then look no further.

This library contains a complete implementation of a Quantify unit repository based on enums. All you have to do is to make sure that your unit enum is annotated with the correct attributes, and you're good to go.

```csharp
var unitRepository = new EnumUnitRepository<Unit>();
```

For more information about the Quantify framework, please see [Quantify](https://github.com/acidicsoftware/dotnet-quantify).

## Unit Enum Structure
To make your unit enum work with the enum repository, your enum must be correctly annotated with [UnitEnumAttribute](src/Quantify.Repository.Enum/UnitEnumAttribute.cs) and [UnitAttribute](src/Quantify.Repository.Enum/DataAnnotation/UnitAttribute.cs).

```csharp
[UnitEnum(Unit.Metre)]
public enum Length {

    [Unit("0.001")]
    Millimetre = 0,
    
    [Unit("0.01")]
    Centimetre = 1,
    
    [Unit("0.1")]
    Decimetre = 2,
    
    Metre = 3,
    
    [Unit("10")]
    Decametre = 4,
    
    [Unit("100")]
    Hectometre = 5,
    
    [Unit("1000")]
    Kilometre = 6,
    
    [Unit("The Matrix Has You....")]
    Inch = 7,
    
    Foot = 8,
    
}
```

The enum itself **must** be annotated with [UnitEnumAttribute](src/Quantify.Repository.Enum/UnitEnumAttribute.cs) and the base unit argument must point to a value in the enum. If the enum is not annotated with the attribute or if the base unit argument is not a valid value from the annotated enum, then the instantiation of the repository will fail.

Enum values must be annotated with [UnitAttribute](src/Quantify.Repository.Enum/DataAnnotation/UnitAttribute.cs) to be available as usable units. The `valueInBaseUnits` argument describes how many base units the current unit represents. As an example, if the SI unit metre is the base unit, then the SI unit kilometre would have a `valueInBaseUnits` value of 1000, since 1 kilometre represents 1000 base units (that is 1000 metres).

The argument `valueInBaseUnits` is a string that represents the value as a decimal number with a period (.) as the decimal separator.

If an enum value is not annotated with the attribute or if the argument `valueInBaseUnits` is invalid, then the unit will be ignored and it won't be available for use.

If the enum value designated as the base unit, is also annotated with the [UnitAttribute](src/Quantify.Repository.Enum/DataAnnotation/UnitAttribute.cs), the attribute value will be ignored since the base unit always has a value of 1.

## Unit Enum Report

[UnitEnumReportGenerator](src/Quantify.Repository.Enum/Report/UnitEnumReportGenerator.cs) can be used to generate a report, containing errors and warnings related to the unit enum. This information can be used in unit tests, to validate the structure of your custom unit enum.

```csharp
var reportGenerator = new UnitEnumReportGenerator();
var report = reportGenerator.CreateReport<Unit>();

var hasErrors = report.HasErrors;
var hasWarnings = report.HasWarnings;
```

The generated report has the following properties:

| Property | Type | Description |
| :--- | :--- | :--- |
| HasWarnings | `bool` | `true` if one or more warnings were found; otherwise `false`. |
| Warnings | `IReadOnlyCollection<string>` | Warning descriptions. One for each warning. |
| HasErrors | `bool` | `true` if one or more errors were found; otherwise `false`. |
| Errors | `IReadOnlyCollection<string>` | Error descriptions. One for each error. |
| HasValueMissingUnitAttributeWarning | `bool` | Warning: One or more of the values in the enum are not annotated with [UnitAttribute](src/Quantify.Repository.Enum/DataAnnotation/UnitAttribute.cs). |
| HasValueWithInvalidUnitAttributeWarning | `bool` | Warning: One or more of the values in the enum have an invalid [UnitAttribute](src/Quantify.Repository.Enum/DataAnnotation/UnitAttribute.cs). |
| BaseUnitHasUnitAttributeWarning | `bool` | Warning: The value marked as the base unit is also annotated with [UnitAttribute](src/Quantify.Repository.Enum/DataAnnotation/UnitAttribute.cs). |
| IsMissingBaseUnitAttribute | `bool` | Error: The enum is not annotated with [BaseUnitAttribute](src/Quantify.Repository.Enum/DataAnnotation/BaseUnitAttribute.cs). |
| HasInvalidBaseUnitAttribute | `bool` | Error: The value referenced as the base unit in [BaseUnitAttribute](src/Quantify.Repository.Enum/DataAnnotation/BaseUnitAttribute.cs) is not valid. |

You can also create a summary containing all of the warnings and/or errors in a single formatted string:

```csharp
var reportGenerator = new UnitEnumReportGenerator();
var report = reportGenerator.CreateReport<Unit>();

var summary = report.CreateSummary();
```

*© Copyright 2021 Michel Gammelgaard. All rights reserved. Provided under the [MIT license](LICENSE).*
*Floppy disk icon by Michel Gammelgaard. © Copyright 2020 Michel Gammelgaard. All rights reserved.*