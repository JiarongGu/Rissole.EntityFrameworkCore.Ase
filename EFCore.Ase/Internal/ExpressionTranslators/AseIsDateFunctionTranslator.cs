﻿using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace EntityFrameworkCore.Ase.Internal.ExpressionTranslators
{
    class AseIsDateFunctionTranslator : IMethodCallTranslator
    {
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        private static readonly MethodInfo _methodInfo = typeof(AseDbFunctionsExtensions)
            .GetRuntimeMethod(nameof(AseDbFunctionsExtensions.IsDate), new[] { typeof(DbFunctions), typeof(string) });

        public AseIsDateFunctionTranslator(ISqlExpressionFactory sqlExpressionFactory)
            => _sqlExpressionFactory = sqlExpressionFactory;

        public virtual SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            return _methodInfo.Equals(method)
                ? _sqlExpressionFactory.Convert(
                    _sqlExpressionFactory.Function(
                        "ISDATE",
                        new[] { arguments[1] },
                        _methodInfo.ReturnType),
                    _methodInfo.ReturnType)
                : null;
        }
    }
}
