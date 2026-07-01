using SchemaEvolution.Core.Mappers;
using SchemaEvolution.Core.Migrations;

using System;
using System.Collections.Generic;

namespace SchemaEvolution.Core
{
    public sealed class ComponentSchema
    {
        private readonly IMapper mapper;
        private readonly IMigration[] migrations;
        private readonly Type[] versionTypes;

        public IMapper Mapper => this.mapper;

        public ComponentSchema(IMapper mapper, IMigration[] migrations, Type[] versionTypes)
        {
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(versionTypes);
            ArgumentNullException.ThrowIfNull(migrations);

            if (versionTypes.Length == 0)
            {
                throw new InvalidOperationException("At least one version type must be provided.");
            }

            if (migrations.Length != versionTypes.Length - 1)
            {
                throw new InvalidOperationException($"The number of migrations ({migrations.Length}) must be one less than the number of version types ({versionTypes.Length}).");
            }

            this.mapper = mapper;
            this.migrations = migrations;
            this.versionTypes = versionTypes;
        }

        public Type GetVersionType(int version)
        {
            return this.versionTypes[version - 1];
        }

        public IMigration GetMigration(int version)
        {
            return this.migrations[version - 1];
        }

        public IEnumerable<IMigration> GetMigrations(int sourceVersion, int targetVersion)
        {
            if (sourceVersion >= targetVersion)
            {
                yield break;
            }

            for (int version = sourceVersion; version < targetVersion; version++)
            {
                yield return GetMigration(version);
            }
        }
    }
}
