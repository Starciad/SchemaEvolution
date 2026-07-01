using SchemaEvolution.Core.Data;
using SchemaEvolution.Core.Migrations;

namespace SchemaEvolution.Sample.Migrations
{
    internal sealed class SampleV1ToV2 : IMigration
    {
        public int SourceVersion => 1;
        public int TargetVersion => 2;

        public IData Migrate(IData source)
        {
            Data.V1.SampleData dataV1 = (Data.V1.SampleData)source;

            return new Data.V2.SampleData()
            {
                Health = 100.0f,
                Level = dataV1.Level,
                Name = dataV1.Name
            };
        }
    }
}
