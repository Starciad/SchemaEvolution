# Schema Evolution

This repository presents a proof of concept for a version-aware migration mechanism designed for component persistence during save operations. The objective is not to provide a general-purpose third-party library, but to demonstrate an architectural approach for preserving compatibility across successive schema revisions in stateful systems.

## Research Objective

Modern applications often require the ability to load previously saved components whose internal structure may differ from the current runtime representation. In such scenarios, a direct deserialization process may fail or produce semantic loss when the serialized form no longer matches the active model.

This project explores a structured alternative based on explicit versioning, ordered migrations, and schema-driven transformation. The central idea is to preserve the integrity of persisted component state by applying a deterministic sequence of migration steps until the data reaches the target representation expected by the current application.

## Conceptual Context

The implementation is framed as a persistence-oriented migration system for components that are serialized and later restored during a save/load lifecycle. The model assumes that:

1. each component version is represented by a distinct data contract;
2. the system maintains a sequence of migrations between consecutive versions;
3. deserialization is performed using the source version and then progressively transformed toward the target version;
4. the final object is mapped into an in-memory storage model suitable for runtime execution.

This design is relevant in contexts where backward compatibility is required for previously stored data, especially when the application evolves over time and schema changes are unavoidable.

## Demonstration Scenario

The sample program in [src/SchemaEvolution.Sample/Program.cs](src/SchemaEvolution.Sample/Program.cs) illustrates the complete workflow using a component that evolves from version 1 to version 5.

The execution flow is as follows:

1. An instance of an older schema version is created.
2. The object is serialized into a stream.
3. The stream is deserialized using the source version metadata.
4. The system applies each migration in order until the target version is reached.
5. The migrated result is mapped into the latest storage model used by the application.

This example intentionally preserves multiple historical data types within the codebase to show how a migration pipeline can preserve compatibility without requiring the loss of legacy information.

## Architectural Components

### ComponentSchema

The class in [src/SchemaEvolution.Core/ComponentSchema.cs](src/SchemaEvolution.Core/ComponentSchema.cs) defines the schema configuration for a versioned component. It stores:

- the mapper used to convert between persisted data and runtime models;
- the ordered list of migrations;
- the set of version-specific types recognized by the system.

It also validates the consistency of the migration graph by ensuring that the number of migrations is compatible with the number of available schema versions.

### SchemaSerializer

The abstract base class in [src/SchemaEvolution.Core/SchemaSerializer.cs](src/SchemaEvolution.Core/SchemaSerializer.cs) acts as the orchestration layer for the migration pipeline. Its responsibilities include:

- serializing a versioned data object to a stream;
- deserializing data using the source schema version;
- applying migrations sequentially until the target version is reached;
- mapping the final result into the target storage model.

Concrete serializers implement only the format-specific operations required for reading and writing the underlying payload.

### IData

The interface in [src/SchemaEvolution.Core/Data/IData.cs](src/SchemaEvolution.Core/Data/IData.cs) represents the versioned data contract that participates in the migration process. Each schema revision is expected to define a concrete type that implements this contract.

### IMapper

The interface in [src/SchemaEvolution.Core/Mappers/IMapper.cs](src/SchemaEvolution.Core/Mappers/IMapper.cs) defines the transformation boundary between persisted data and runtime state. It supports conversion in both directions:

- from an IData instance to an application storage model;
- from a storage model to an IData instance for serialization.

### IMigration

The interface in [src/SchemaEvolution.Core/Migrations/IMigration.cs](src/SchemaEvolution.Core/Migrations/IMigration.cs) defines a single transformation step. Each migration is characterized by:

- a source version;
- a target version;
- a conversion procedure that translates one schema representation into another.

### IStorageModel

The interface in [src/SchemaEvolution.Core/StorageModels/IStorageModel.cs](src/SchemaEvolution.Core/StorageModels/IStorageModel.cs) represents the runtime model used by the application after deserialization and migration. This is the layer that the application operates on during execution.

## Implementation Notes

The sample uses MessagePack as the serialization backend, but the architecture is intentionally decoupled from the transport format. The core contribution of the project lies in its migration strategy rather than in the choice of serializer.

The design emphasizes explicit version control and deterministic transformation. Although maintaining historical schema classes introduces some overhead, it provides a transparent mechanism for preserving compatibility across evolving component definitions.

## Project Structure

- [src/SchemaEvolution.Core](src/SchemaEvolution.Core) contains the core abstractions and migration infrastructure.
- [src/SchemaEvolution.Sample](src/SchemaEvolution.Sample) contains the executable proof-of-concept demonstrating the full lifecycle.
- [src/SchemaEvolution.Project.sln](src/SchemaEvolution.Project.sln) is the solution file for the project.

## Build and Execution

### Build

```bash
dotnet build src/SchemaEvolution.Project.sln
```

### Run the sample

```bash
dotnet run --project src/SchemaEvolution.Sample/SchemaEvolution.Sample.csproj
```

## Summary

This project should be understood as a conceptual demonstration of migration-based persistence for evolving components. Its purpose is to validate the feasibility of using a versioned migration pipeline as a strategy for preserving save compatibility in systems where component schemas change over time.
