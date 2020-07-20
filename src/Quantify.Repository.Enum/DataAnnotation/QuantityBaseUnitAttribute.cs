using System;

namespace Quantify.Repository.Enum.DataAnnotation
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class QuantityBaseUnitAttribute : Attribute
    {
        public QuantityBaseUnitAttribute()
        {
        }
    }
}