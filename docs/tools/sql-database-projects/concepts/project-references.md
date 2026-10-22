---
title: Project References Overview
description: Learn about dependencies between SQL projects and other projects, including database references and .NET project references.
author: cwebb-msft
ms.author: chweb
ms.reviewer: drskwier
ms.date: 01/21/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ai-usage: ai-assisted
---

# Project references overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

With project references in SQL database projects, you can create dependencies between your SQL project and other projects. There are two primary types of project references:

- **Database references** - Dependencies between SQL projects or references to `.dacpac` files and NuGet packages that provide database object definitions.
- **.NET project references** - References from .NET projects to SQL projects for scenarios like integration testing, deployment automation, and code generation.

When you understand when and how to use each type of reference, you can structure your database development workflow effectively.

## Database references

Database references allow a SQL project to incorporate objects from another SQL project, a `.dacpac` file, or a published NuGet package. These references are used when your database objects depend on objects defined elsewhere, such as tables in a shared schema or system database objects.

A basic database reference to another SQL project in the same solution looks like this:

```xml
<ItemGroup>
  <ProjectReference Include="..\Database1\Database1.sqlproj" />
</ItemGroup>
```

Database references support three relationship types:

- **Same database** - Objects from the referenced project become part of the same database model.
- **Different database, same server** - Reference objects using three-part naming with a SQLCMD variable for the database name.
- **Different database, different server** - Reference objects using four-part naming with SQLCMD variables for both server and database names.

For detailed information about configuring database references, including examples for each relationship type and guidance on building and publishing projects with references, see [Database references overview](database-references.md).

## .NET project references

.NET projects can reference SQL projects to integrate database development with application code. This reference type is useful when your .NET application needs access to the SQL project's build output (the `.dacpac` file) for testing, deployment, or code generation purposes.

### Use cases

Common scenarios for referencing a SQL project from a .NET project include:

- **Integration tests** - Test projects that deploy the database schema to a test container or local instance before running tests.
- **Deployment automation** - Console applications or tools that programmatically deploy the `.dacpac` to target environments.
- **Model code generation** - Applications that generate code based on the database schema defined in the SQL project.

### Configure the project reference

When you add a project reference from a .NET project to a SQL project, you must include the `ReferenceOutputAssembly="false"` attribute. This attribute tells the .NET build process to treat the SQL project as a build dependency without attempting to reference it as a .NET assembly.

```xml
<ItemGroup>
  <ProjectReference Include="..\Database1\Database1.sqlproj" ReferenceOutputAssembly="false" />
</ItemGroup>
```

The `ReferenceOutputAssembly="false"` setting is required because SQL projects produce a `.dacpac` file as their primary output, not a .NET assembly. Without this attribute, the .NET build process attempts to load the `.dacpac` as an assembly and fails with an error similar to:

```output
error CS0009: Metadata file 'Database1.dacpac' could not be opened -- Unknown file format.
```

### Access the DACPAC in your .NET project

After you configure the project reference, the SQL project builds before your .NET project. The `.dacpac` file is available in the SQL project's output directory (typically `bin/Debug` or `bin/Release`).

To access the `.dacpac` programmatically in your .NET code, reference the file path relative to your project structure. For example, in an integration test that uses Testcontainers to create a SQL Server instance:

```csharp
// Path to the dacpac file built by the referenced SQL project
var dacpacPath = Path.Combine(
    Directory.GetCurrentDirectory(),
    "..", "..", "..", "..",
    "Database1", "bin", "Debug",
    "Database1.dacpac");

// Use DacFx to deploy the dacpac to your test database
var dacServices = new DacServices(connectionString);
using var dacpac = DacPackage.Load(dacpacPath);
dacServices.Deploy(dacpac, "TestDatabase");
```

### Copy the dacpac to the output directory

For easier access to the `.dacpac` file, configure your .NET project to copy it to the output directory during build. Add the following configuration to your `.csproj` file:

```xml
<ItemGroup>
  <None Include="..\Database1\bin\$(Configuration)\Database1.dacpac"
        CopyToOutputDirectory="PreserveNewest"
        Link="Database1.dacpac" />
</ItemGroup>
```

This configuration copies the `.dacpac` file to your .NET project's output directory, so you can reference it using a simpler path:

```csharp
var dacpacPath = Path.Combine(
    AppContext.BaseDirectory,
    "Database1.dacpac");
```

## Related content

- [Database references overview](database-references.md)
- [SQL projects package references](package-references.md)
- [SQL projects system objects](system-objects.md)
