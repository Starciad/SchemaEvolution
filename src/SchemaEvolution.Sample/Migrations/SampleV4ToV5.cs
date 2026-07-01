using SchemaEvolution.Core.Data;
using SchemaEvolution.Core.Migrations;
using SchemaEvolution.Sample.Data.V5;

namespace SchemaEvolution.Sample.Migrations
{
    internal sealed class SampleV4ToV5 : IMigration
    {
        public int SourceVersion => 4;
        public int TargetVersion => 5;

        public IData Migrate(IData source)
        {
            Data.V4.SampleData dataV4 = (Data.V4.SampleData)source;

            Attributes attributes = new()
            {
                Dexterity = 10,
                Intelligence = 10,
                Strength = 10
            };

            return new Data.V5.SampleData()
            {
                Attributes = attributes,
                FirstName = dataV4.FirstName,
                Health = dataV4.Health,
                LastName = dataV4.LastName,
                Level = dataV4.Level
            };
        }
    }
}
