using EntityFrameworkCore.Ase.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Update;
using System.Linq;

namespace EntityFrameworkCore.Ase.Update.Internal
{
    internal class AseModificationCommandBatchFactory : IModificationCommandBatchFactory
    {
        private readonly ModificationCommandBatchFactoryDependencies _dependencies;
        private readonly IDbContextOptions _options;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public AseModificationCommandBatchFactory(
            ModificationCommandBatchFactoryDependencies dependencies,
            IDbContextOptions options)
        {
            _dependencies = dependencies;
            _options = options;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual ModificationCommandBatch Create()
        {
            var optionsExtension = _options.Extensions.OfType<AseOptionsExtension>().FirstOrDefault();

            return new AseModificationCommandBatch(_dependencies, optionsExtension?.MaxBatchSize);
        }
    }
}
