using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Rissole.EntityFrameworkCore.Ase.Internal.TypeMappings
{
    public class CharBooleanConvertor : ValueConverter<bool, char>
    {
        public CharBooleanConvertor() : base(
            (bool value) => value ? 'Y' : 'N',
            (char value) => value == 'Y' ? true : false
        )
        { }
    }

    public class AseCharBooleanTypeMapping : RelationalTypeMapping
    {
        public AseCharBooleanTypeMapping() : base(new RelationalTypeMappingParameters(
                    new CoreTypeMappingParameters(
                        typeof(bool),
                        new CharBooleanConvertor(),
                        new ValueComparer<bool>(false),
                        new ValueComparer<bool>(false)),
                        "char",
                        StoreTypePostfix.Size,
                        System.Data.DbType.String,
                        false,
                        1,
                        true,
                        null,
                        null))
        {
        }

        protected AseCharBooleanTypeMapping(RelationalTypeMappingParameters parameters) : base(parameters)
        {
        }


        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new AseCharBooleanTypeMapping(parameters);
        }
    }
}
