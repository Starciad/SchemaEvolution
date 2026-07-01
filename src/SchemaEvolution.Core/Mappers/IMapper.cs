using SchemaEvolution.Core.Data;
using SchemaEvolution.Core.StorageModels;

namespace SchemaEvolution.Core.Mappers
{
    public interface IMapper
    {
        IStorageModel ToStorageModel(IData value);
        IData ToData(IStorageModel value);
    }
}
