using SchemaEvolution.Core.Data;
using SchemaEvolution.Core.Migrations;
using SchemaEvolution.Sample.Data.V4;

namespace SchemaEvolution.Sample.Migrations
{
    internal sealed class SampleV3ToV4 : IMigration
    {
        public int SourceVersion => 3;
        public int TargetVersion => 4;

        public IData Migrate(IData source)
        {
            Data.V3.SampleData dataV3 = (Data.V3.SampleData)source;

            Vitality vitality = new()
            {
                Current = dataV3.Health,
                Maximum = 100.0f
            };

            return new Data.V4.SampleData()
            {
                FirstName = dataV3.FirstName,
                Health = vitality,
                LastName = dataV3.LastName,
                Level = dataV3.Level
            };
        }
    }
}
