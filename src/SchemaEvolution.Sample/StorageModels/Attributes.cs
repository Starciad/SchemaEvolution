namespace SchemaEvolution.Sample.StorageModels
{
    internal sealed class Attributes
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }

        public override string ToString()
        {
            return $"Attributes | Strength: {this.Strength}, Dexterity: {this.Dexterity}, Intelligence: {this.Intelligence}";
        }
    }
}
