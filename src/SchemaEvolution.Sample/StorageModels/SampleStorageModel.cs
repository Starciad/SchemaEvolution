using SchemaEvolution.Core.StorageModels;

using System.Text;

namespace SchemaEvolution.Sample.StorageModels
{
    internal sealed class SampleStorageModel : IStorageModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Level { get; set; }
        public Vitality Health { get; set; } = new();
        public Attributes Attributes { get; set; } = new();

        public override string ToString()
        {
            StringBuilder sb = new();

            _ = sb.AppendLine("Sample Storage Model");
            _ = sb.AppendLine($"FirstName: {this.FirstName}");
            _ = sb.AppendLine($"LastName: {this.LastName}");
            _ = sb.AppendLine($"Level: {this.Level}");
            _ = sb.AppendLine($"Health: {this.Health}");
            _ = sb.AppendLine($"Attributes: {this.Attributes}");

            return sb.ToString();
        }
    }
}
