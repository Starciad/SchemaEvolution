using SchemaEvolution.Core;
using SchemaEvolution.Sample.Mappers;
using SchemaEvolution.Sample.Migrations;
using SchemaEvolution.Sample.StorageModels;

using System;
using System.IO;

namespace SchemaEvolution.Sample
{
    internal static class Program
    {
        // A component schema explicitly defines the mapper, migrations, and version types
        // that make up a versioned component. This setup is required for any component undergoing
        // version-aware serialization. We only have one target component in this sample.
        // Note: All migrations and version types must be provided in ascending order.

        private static readonly ComponentSchema sampleComponentSchema = new(
            mapper: new SampleMapper(),

            migrations: [
                new SampleV1ToV2(),
                new SampleV2ToV3(),
                new SampleV3ToV4(),
                new SampleV4ToV5(),
            ],

            versionTypes: [
                typeof(Data.V1.SampleData),
                typeof(Data.V2.SampleData),
                typeof(Data.V3.SampleData),
                typeof(Data.V4.SampleData),
                typeof(Data.V5.SampleData),
            ]
        );

        // The custom serializer coordinates the entire pipeline. `SampleSerializer` extends 
        // `SchemaSerializer` to provide specific read/write stream operations. While this sample 
        // uses MessagePack for binary serialization, you can adapt the base class to handle JSON, 
        // XML, or any other underlying format.

        private static readonly SampleSerializer sampleSerializer = new();

        private static void Main()
        {
            Console.WriteLine("=======================================================");
            Console.WriteLine("                Schema Evolution Sample                ");
            Console.WriteLine("=======================================================");
            Console.WriteLine("This sample demonstrates how to use the Schema Evolution");
            Console.WriteLine("library to serialize and deserialize data across");
            Console.WriteLine("different versions of a schema.");
            Console.WriteLine("=======================================================");
            Console.WriteLine();

            // We instantiate a `SampleData` object from the `Data.V1` namespace. 
            // By intentionally keeping legacy data structures in the codebase (V1 through V5), 
            // we maintain full backward compatibility. This prevents data loss and ensures that 
            // older saves can smoothly migrate through the pipeline up to the latest format.

            Data.V1.SampleData sampleDataV1 = new()
            {
                Level = 1,
                Name = "Sample Data"
            };

            Console.WriteLine("[ STEP 1 ] Initializing Original Data");
            Console.WriteLine("Target: Version 1");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine(sampleDataV1);
            Console.WriteLine();

            Console.WriteLine("[ STEP 2 ] Serializing Data");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Serializing original sample data...");

            // We use a MemoryStream to hold the binary output in memory, acting as a substitute 
            // for saving a file directly to the user's device.

            using MemoryStream ms = new();

            // Here we serialize the V1 component. In a real-world scenario, you would typically 
            // serialize the latest schema version derived from an active `IStorageModel`. We are 
            // starting with V1 solely to demonstrate the migration process later.

            sampleSerializer.Serialize(ms, sampleDataV1);

            Console.WriteLine($"Serialization successful. Stream length: {ms.Length} bytes.");
            Console.WriteLine();

            Console.WriteLine("[ STEP 3 ] Deserializing and Migrating");
            Console.WriteLine("Target: Version 1 -> Version 5");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Applying migrations up to the latest version...");

            // With the stream populated, we reset its position to simulate reading from a file.
            // The deserialization pipeline requires both the source and target versions upfront. 

            // Typically, the source version is extracted from the file's saved header, while 
            // the target version reflects your application's latest state. The method then extracts 
            // the exact legacy object, applies migrations sequentially, and maps the final `IData` 
            // result into your mutable `IStorageModel`.

            // The `IStorageModel` handles runtime state. When you need to save again, this model 
            // is converted back into a read-only `IData` object representing the newest schema.

            ms.Position = 0;
            SampleStorageModel sampleStorageModel = sampleSerializer.Deserialize<SampleStorageModel>(ms, sampleComponentSchema, 1, 5);

            Console.WriteLine("Deserialization and migration completed successfully.");
            Console.WriteLine();

            Console.WriteLine("[ STEP 4 ] Final Result");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Deserialized Sample Storage Model (Version 5):");
            Console.WriteLine($"\n{sampleStorageModel}\n");
            Console.WriteLine("=======================================================");

            // This completes the schema evolution cycle. We now have a robust pipeline that 
            // prevents deserialization crashes—especially critical when using strict, schema-bound 
            // libraries like MessagePack.
            
            // While maintaining older class definitions represents a minor trade-off in codebase 
            // size, it guarantees absolute safety and transparency as your data formats evolve over time.
        }
    }
}
