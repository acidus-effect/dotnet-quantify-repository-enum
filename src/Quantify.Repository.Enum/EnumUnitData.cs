using System;

namespace Quantify.Repository.Enum
{
    internal class EnumUnitData<TValue, TUnit> : UnitData<TValue, TUnit>
    {
        public TUnit Unit { get; }

        public TValue Value { get; }

        public EnumUnitData(TUnit unit, TValue value)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Unit = unit;
            Value = value;
        }
    }
}