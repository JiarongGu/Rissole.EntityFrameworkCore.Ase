﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace EntityFrameworkCore.Ase.ValueGeneration.Internal
{
    public class AseSequenceValueGeneratorState : HiLoValueGeneratorState
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public AseSequenceValueGeneratorState(ISequence sequence)
            : base(sequence.IncrementBy)
        {
            Sequence = sequence;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual ISequence Sequence { get; }
    }
}