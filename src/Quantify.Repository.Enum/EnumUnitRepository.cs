using Quantify.Repository.Enum.ValueParser;
using System;
using System.Collections.Generic;

namespace Quantify.Repository.Enum
{
    public class EnumUnitRepository<TValue, TUnit> : UnitRepository<TValue, TUnit> where TUnit : struct, IConvertible
    {
        private readonly IReadOnlyDictionary<TUnit, TValue> UnitDictionary;

        internal EnumUnitRepository(EnumUnitExtractor<TValue, TUnit> enumUnitExtractor)
        {
            if (enumUnitExtractor == null)
                throw new ArgumentNullException(nameof(enumUnitExtractor));

            UnitDictionary = enumUnitExtractor.Extract();
        }

        public static EnumUnitRepository<TValue, TUnit> CreateInstance()
        {
            var stringValueParserFactory = new StringValueParserFactory<TValue>().Build();
            var enumUnitExtractor = new EnumUnitExtractor<TValue, TUnit>(stringValueParserFactory);

            return new EnumUnitRepository<TValue, TUnit>(enumUnitExtractor);
        }

        public UnitData<TValue, TUnit> GetUnit(TUnit unit)
        {
            return UnitDictionary.TryGetValue(unit, out var value) ? new EnumUnitData<TValue, TUnit>(unit, value) : null;
        }
    }
}