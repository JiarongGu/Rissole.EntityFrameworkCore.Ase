﻿using EntityFrameworkCore.Ase.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Linq.Expressions;

namespace EntityFrameworkCore.Ase.Query.Internal
{
    public class AseQuerySqlGenerator : QuerySqlGenerator
    {
        private readonly IAseOptions _aseOptions;

        public AseQuerySqlGenerator(QuerySqlGeneratorDependencies dependencies, IAseOptions aseOptions)
            : base(dependencies)
        {
            _aseOptions = aseOptions;
        }


        // Sybase does not support TOP in subquery
        // https://stackoverflow.com/questions/47790152/top-selection-in-sybase-subquery
        protected override void GenerateTop(SelectExpression selectExpression)
        {
            if (selectExpression.Limit != null)
            {
                Sql.Append("TOP ");
                Visit(selectExpression.Limit);
                Sql.Append(" ");
            }

            if (selectExpression.Offset != null)
            {
                Sql.Append("START AT ");
                Visit(selectExpression.Offset);
                Sql.Append(" ");
            }
        }

        protected override void GenerateLimitOffset(SelectExpression selectExpression)
        {
            // START AT comes after TOP, where as this is called after the main query.
            // See GenerateTop
        }

        protected override void GenerateOrderings(SelectExpression selectExpression)
        {
            // base implementation will generate "ORDER BY (SELECT 1)" when there is a Skip value, which isn't supported.
            if (selectExpression.Orderings.Any())
                base.GenerateOrderings(selectExpression);
        }

        public override IRelationalCommand GetCommand(SelectExpression selectExpression)
        {
            var cmd = base.GetCommand(selectExpression);
            return cmd;
        }

        protected override Expression VisitSqlFunction(SqlFunctionExpression sqlFunctionExpression)
        {
            if (!sqlFunctionExpression.IsBuiltIn
                && string.IsNullOrEmpty(sqlFunctionExpression.Schema))
            {
                sqlFunctionExpression = SqlFunctionExpression.Create(
                    schema: "dbo",
                    sqlFunctionExpression.Name,
                    sqlFunctionExpression.Arguments,
                    sqlFunctionExpression.Type,
                    sqlFunctionExpression.TypeMapping);
            }

            return base.VisitSqlFunction(sqlFunctionExpression);
        }
    }
}
