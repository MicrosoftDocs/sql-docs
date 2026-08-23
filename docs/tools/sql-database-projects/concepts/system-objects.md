---
title: SQL Projects System Objects
description: Learn about referencing system objects in SQL projects.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, maghan
ms.date: 10/10/2025
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ms.collection:
  - data-tools
---

# SQL projects system objects

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

SQL projects validate database object references during the project build process. By default, SQL projects don't include system objects in the database model, which can lead to validation errors if your project contains references to system objects. To resolve these validation errors, you would include a database reference to the `master.dacpac` for the target platform of your project.

The `master.dacpac` database reference can be added as a [package reference](package-references.md) in Microsoft.Build.Sql SDK-style SQL projects or as an [artifact reference](database-references.md) in both SDK-style and original SQL projects.

## Add a package reference

The available system database packages are:

- [SQL Server `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Master)
- [SQL Server `msdb` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Msdb)
- [Azure SQL Database `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Azure.Master)
- [SQL database in Fabric system objects](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.DbFabric)
- [Azure Synapse Analytics `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Synapse.Master)
- [Azure Synapse Analytics serverless pools `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.SynapseServerless.Master)

The most direct method for adding a package reference to a SQL project is to use the .NET command-line interface (CLI). The following example adds a package reference to the Azure SQL Database `master` system database to a SQL project:

```bash
dotnet add <path-to-sqlproj> package Microsoft.SqlServer.Dacpacs.Azure.Master
```

This command adds the following entry to the `.sqlproj` file (the package version will reflect the latest version available at the time the command is run):

```xml
...
    <ItemGroup>
        <PackageReference Include="Microsoft.SqlServer.Dacpacs.Azure.Master" Version="170.0.1" />
    </ItemGroup>
</Project>
```

## Add an artifact reference

The VS Code and Visual Studio SQL project interfaces provide a method for adding an artifact reference to the `master.dacpac` file for the target platform of your project.

The resulting edits to the `.sqlproj` file will look similar to the following example, which adds an artifact reference to the Azure SQL Database `master` system database in Visual Studio:

```xml
<ItemGroup>
    <ArtifactReference Include="$(DacPacRootPath)\Extensions\Microsoft\SQLDB\Extensions\SqlServer\AzureV12\SqlSchemas\master.dacpac">
      <HintPath>$(DacPacRootPath)\Extensions\Microsoft\SQLDB\Extensions\SqlServer\AzureV12\SqlSchemas\master.dacpac</HintPath>
      <SuppressMissingDependenciesErrors>False</SuppressMissingDependenciesErrors>
      <DatabaseVariableLiteralValue>master</DatabaseVariableLiteralValue>
    </ArtifactReference>
  </ItemGroup>
```

The `master.dacpac` files are referenced from the installation location of the applications, which can be fragile for some CI/CD systems. You can copy the `master.dacpac` file to a location within your solution and update the `Include` and `HintPath` attributes to point to that location if your build system doesn't include the system database files.

## SQL database in Fabric

The SQL database in Fabric system objects are included in the `Microsoft.SqlServer.Dacpacs.DbFabric` package, but additional steps might be required to configure the database reference correctly for the SQL database in Fabric environment.

The SQL project created with the integrated source control in Fabric includes the package reference and the `DatabaseVariableLiteralValue` property set to `master`. This property is required because SQL database in Fabric doesn't provide access to the `master` database but the same system objects can be referenced in the user database.

If you create a new SQL project in Visual Studio or VS Code, you must update the `DatabaseVariableLiteralValue` property to `master` to match the database name used in the SQL database in Fabric environment.

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.SqlServer.Dacpacs.DbFabric">
      <SuppressMissingDependenciesErrors>False</SuppressMissingDependenciesErrors>
      <DatabaseVariableLiteralValue>master</DatabaseVariableLiteralValue>
      <Version>170.0.0</Version>
    </PackageReference>
  </ItemGroup>
```

## Related content

- [Database references overview](database-references.md)
- [SQL projects package references](package-references.md)
- [SqlPackage](../../sqlpackage/sqlpackage.md)
