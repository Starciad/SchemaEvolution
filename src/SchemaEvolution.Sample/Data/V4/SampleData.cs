using MessagePack;

using SchemaEvolution.Core.Data;

using System;
using System.Text;

namespace SchemaEvolution.Sample.Data.V4
{
    [Serializable]
    [MessagePackObject]
    public sealed class SampleData : IData
    {
        [Key(0)]
        public string FirstName { get; init; }

        [Key(1)]
        public string LastName { get; init; }

        [Key(2)]
        public int Level { get; init; }

        [Key(3)]
        public Vitality Health { get; init; }

        public override string ToString()
        {
            StringBuilder sb = new();

            _ = sb.AppendLine("Sample Data V4");
            _ = sb.AppendLine($"FirstName: {this.FirstName}");
            _ = sb.AppendLine($"LastName: {this.LastName}");
            _ = sb.AppendLine($"Level: {this.Level}");
            _ = sb.AppendLine($"Health: {this.Health}");

            return sb.ToString();
        }
    }
}
