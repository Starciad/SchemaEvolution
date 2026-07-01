using SchemaEvolution.Core.Data;
using SchemaEvolution.Core.Mappers;
using SchemaEvolution.Core.StorageModels;
using SchemaEvolution.Sample.Data.V5;
using SchemaEvolution.Sample.StorageModels;

namespace SchemaEvolution.Sample.Mappers
{
    internal sealed class SampleMapper : IMapper
    {
        public IData ToData(IStorageModel value)
        {
            SampleStorageModel sampleStorageModel = (SampleStorageModel)value;

            return new SampleData()
            {
                Attributes = new()
                {
                    Dexterity = sampleStorageModel.Attributes.Dexterity,
                    Intelligence = sampleStorageModel.Attributes.Intelligence,
                    Strength = sampleStorageModel.Attributes.Strength
                },
                FirstName = sampleStorageModel.FirstName,
                Health = new()
                {
                    Current = sampleStorageModel.Health.Current,
                    Maximum = sampleStorageModel.Health.Maximum
                },
                LastName = sampleStorageModel.LastName,
                Level = sampleStorageModel.Level
            };
        }

        public IStorageModel ToStorageModel(IData value)
        {
            SampleData sampleData = (SampleData)value;

            return new SampleStorageModel()
            {
                Attributes = new()
                {
                    Dexterity = sampleData.Attributes.Dexterity,
                    Intelligence = sampleData.Attributes.Intelligence,
                    Strength = sampleData.Attributes.Strength
                },
                FirstName = sampleData.FirstName,
                Health = new()
                {
                    Current = sampleData.Health.Current,
                    Maximum = sampleData.Health.Maximum
                },
                LastName = sampleData.LastName,
                Level = sampleData.Level
            };
        }
    }
}
