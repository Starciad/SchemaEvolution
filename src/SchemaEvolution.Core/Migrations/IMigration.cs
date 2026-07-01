using SchemaEvolution.Core.Data;

namespace SchemaEvolution.Core.Migrations
{
    public interface IMigration
    {
        int SourceVersion { get; }
        int TargetVersion { get; }

        IData Migrate(IData source);
    }
}
