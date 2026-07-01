using SchemaEvolution.Core.Data;
using SchemaEvolution.Core.Migrations;

namespace SchemaEvolution.Sample.Migrations
{
    internal sealed class SampleV2ToV3 : IMigration
    {
        public int SourceVersion => 2;
        public int TargetVersion => 3;

        public IData Migrate(IData source)
        {
            Data.V2.SampleData dataV2 = (Data.V2.SampleData)source;

            string[] nameTokens = dataV2.Name.Split(' ', 2, System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries);

            return new Data.V3.SampleData()
            {
                FirstName = nameTokens[0],
                Health = dataV2.Health,
                Level = dataV2.Level,
                LastName = nameTokens.Length > 1 ? nameTokens[1] : string.Empty
            };
        }
    }
}
