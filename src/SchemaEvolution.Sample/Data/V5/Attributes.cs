using MessagePack;

using System;

namespace SchemaEvolution.Sample.Data.V5
{
    [Serializable]
    [MessagePackObject]
    public sealed class Attributes
    {
        [Key(0)]
        public int Strength { get; init; }

        [Key(1)]
        public int Dexterity { get; init; }

        [Key(2)]
        public int Intelligence { get; init; }

        public override string ToString()
        {
            return $"Attributes | Strength: {this.Strength}, Dexterity: {this.Dexterity}, Intelligence: {this.Intelligence}";
        }
    }
}
