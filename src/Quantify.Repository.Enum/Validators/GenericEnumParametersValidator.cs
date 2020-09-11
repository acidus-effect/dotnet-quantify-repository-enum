using System.Reflection;

namespace Quantify.Repository.Enum.Validators
{
    internal class GenericEnumParametersValidator
    {
        public bool GenericParameterIsEnumType<TUnit>() where TUnit : struct
        {
            return typeof(TUnit).GetTypeInfo().IsEnum;
        }
    }
}