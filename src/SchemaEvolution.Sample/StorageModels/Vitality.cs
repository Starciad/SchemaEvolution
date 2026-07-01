namespace SchemaEvolution.Sample.StorageModels
{
    internal sealed class Vitality
    {
        public float Current { get; set; }
        public float Maximum { get; set; }

        public override string ToString()
        {
            return $"Vitality | Current: {this.Current}, Maximum: {this.Maximum}";
        }
    }
}
