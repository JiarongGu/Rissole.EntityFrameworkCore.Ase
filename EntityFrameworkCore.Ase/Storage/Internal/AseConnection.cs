using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace EntityFrameworkCore.Ase.Storage.Internal
{
    public interface IAseConnection : IRelationalConnection
    {
    }

    internal class AseConnection : RelationalConnection, IAseConnection
    {
        public AseConnection(RelationalConnectionDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override DbConnection CreateDbConnection() => new AdoNetCore.AseClient.AseConnection(base.ConnectionString);

        public virtual IAseConnection CreateMasterConnection()
        {
            var contextOptions = new DbContextOptionsBuilder()
                .UseAse(ConnectionString)
                .Options;

            return new AseConnection(Dependencies.With(contextOptions));
        }


        public override bool IsMultipleActiveResultSetsEnabled
            => false;

        /// <summary>
        ///     Indicates whether the store connection supports ambient transactions
        /// </summary>
        protected override bool SupportsAmbientTransactions
            => false;
    }
}
