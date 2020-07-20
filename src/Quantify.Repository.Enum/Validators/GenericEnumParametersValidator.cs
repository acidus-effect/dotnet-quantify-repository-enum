using System;

namespace Quantify.Repository.Enum.Validators
{
    internal class GenericEnumParametersValidator
    {
        public bool GenericParameterIsEnumType<TUnit>() where TUnit : struct, IConvertible
        {
            return typeof(TUnit).IsEnum;
        }
    }
}