using MessagePack;

using System;

namespace SchemaEvolution.Sample.Data.V4
{
    [Serializable]
    [MessagePackObject]
    public sealed class Vitality
    {
        [Key(0)]
        public float Current { get; init; }

        [Key(1)]
        public float Maximum { get; init; }

        public override string ToString()
        {
            return $"Vitality | Current: {this.Current}, Maximum: {this.Maximum}";
        }
    }
}
