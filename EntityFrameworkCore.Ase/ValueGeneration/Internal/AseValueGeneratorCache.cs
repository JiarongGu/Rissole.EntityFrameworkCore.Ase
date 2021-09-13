using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace EntityFrameworkCore.Ase.ValueGeneration.Internal
{
    internal interface IAseValueGeneratorCache : IValueGeneratorCache
    {

    }

    internal class AseValueGeneratorCache : ValueGeneratorCache, IAseValueGeneratorCache
    {
        public AseValueGeneratorCache(ValueGeneratorCacheDependencies dependencies)
            : base(dependencies)
        {
        }
    }
}