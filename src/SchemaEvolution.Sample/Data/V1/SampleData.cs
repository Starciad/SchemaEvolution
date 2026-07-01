using MessagePack;

using SchemaEvolution.Core.Data;

using System;
using System.Text;

namespace SchemaEvolution.Sample.Data.V1
{
    [Serializable]
    [MessagePackObject]
    public sealed class SampleData : IData
    {
        [Key(0)]
        public string Name { get; init; }

        [Key(1)]
        public int Level { get; init; }

        public override string ToString()
        {
            StringBuilder sb = new();

            _ = sb.AppendLine("Sample Data V1");
            _ = sb.AppendLine($"Name: {this.Name}");
            _ = sb.AppendLine($"Level: {this.Level}");

            return sb.ToString();
        }
    }
}
