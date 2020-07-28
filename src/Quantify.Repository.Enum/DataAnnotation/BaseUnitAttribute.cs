using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BaseUnitAttribute : Attribute
    {
        public BaseUnitAttribute()
        {
        }
    }
}