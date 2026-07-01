using SchemaEvolution.Core.Data;
using SchemaEvolution.Core.Mappers;
using SchemaEvolution.Core.Migrations;
using SchemaEvolution.Core.StorageModels;

using System;
using System.IO;

namespace SchemaEvolution.Core
{
    public abstract class SchemaSerializer
    {
        protected abstract IData OnDeserialize(Stream stream, Type versionType);
        protected abstract void OnSerialize(Stream stream, IData data);

        public void Serialize(Stream stream, IData data)
        {
            OnSerialize(stream, data);
        }

        public void Serialize(Stream stream, IMapper mapper, IStorageModel storageModel)
        {
            OnSerialize(stream, mapper.ToData(storageModel));
        }

        private static void Migrate(ref IData data, ComponentSchema componentSchema, int sourceVersion, int targetVersion)
        {
            if (sourceVersion >= targetVersion)
            {
                return;
            }

            foreach (IMigration migration in componentSchema.GetMigrations(sourceVersion, targetVersion))
            {
                data = migration.Migrate(data);
            }
        }

        public TStorageModel Deserialize<TStorageModel>(Stream stream, ComponentSchema componentSchema, int sourceVersion, int targetVersion) where TStorageModel : IStorageModel
        {
            IData data = OnDeserialize(stream, componentSchema.GetVersionType(sourceVersion));
            Migrate(ref data, componentSchema, sourceVersion, targetVersion);
            return (TStorageModel)componentSchema.Mapper.ToStorageModel(data);
        }
    }
}
