using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace EntityFrameworkCore.Ase.Query.Internal
{
    public class AseQueryTranslationPostprocessor : RelationalQueryTranslationPostprocessor
    {
        public AseQueryTranslationPostprocessor(
            QueryTranslationPostprocessorDependencies dependencies,
            RelationalQueryTranslationPostprocessorDependencies relationalDependencies,
            QueryCompilationContext queryCompilationContext)
            : base(dependencies, relationalDependencies, queryCompilationContext)
        {
        }

        public override Expression Process(Expression query)
        {
            query = base.Process(query);
            query = new SearchConditionConvertingExpressionVisitor(SqlExpressionFactory).Visit(query);

            return query;
        }
    }
}
