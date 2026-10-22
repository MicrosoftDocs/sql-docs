---
title: SQL Projects Package References
description: Reference database objects with package references.
author: cwebb-msft
ms.author: chweb
ms.reviewer: drskwier
ms.date: 03/11/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ms.collection:
  - data-tools
ms.custom:
  - ignite-2024
---

# SQL projects package references

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Package references in SQL projects allow you to reference database objects from other projects or NuGet packages. The database objects added to a project through package references can be part of the same database, a different database on the same server, or a different database on a different server.

> [!NOTE]  
> Package references are the recommended method for referencing database objects in new development. Referencing NuGet packages is only supported in SDK-style SQL projects.

## Database object package references

Package references are one of several methods for adding database objects to a SQL project as a [database reference](database-references.md). Package references can contain objects for the same database, a different database on the same server, or a different database on a different server. Package references can be used to break up a database into smaller, more manageable projects, which can help to reduce the time required to build a project during iterative local development.

:::image type="content" source="media/package-references/database-references.png" alt-text="Screenshot of Example of a SQL project referencing two packages and one project for database references." lightbox="media/package-references/database-references.png":::

### SQL project file sample and syntax

Package references are added to a SQL project through entries in the `.sqlproj` file, similar to C# projects. When a package reference is to a different database on the same server, a `<DatabaseSqlCmdVariable>` element is included in the package reference. When a package reference is to a different database on a different server, a `<ServerSqlCmdVariable>` element is also included in the package reference. Package references to the same database don't include `<ServerSqlCmdVariable>` or `<DatabaseSqlCmdVariable>` elements.

The following example includes a package reference to the `Contoso.AdventureWorks.SalesLT` package as a database reference for the same database where the objects in the package become part of the database model in the SQL project:

```xml
...
  <ItemGroup>
    <PackageReference Include="Contoso.AdventureWorks.SalesLT" Version="1.1.0" />
  </ItemGroup>
</Project>
```

The following example includes a package reference to the `Contoso.AdventureWorks` package as a database reference for a different database (AdventureWorks) on the same server where the objects in the package become part of the database model in the SQL project:

```xml
...
  <ItemGroup>
    <PackageReference Include="Contoso.AdventureWorks" Version="1.1.0">
      <DatabaseSqlCmdVariable>AdventureDB</DatabaseSqlCmdVariable>
      <DacpacName>AdventureWorks</DacpacName>
    </PackageReference>
  </ItemGroup>
  <ItemGroup>
    <SqlCmdVariable Include="AdventureDB">
      <DefaultValue>AdventureWorks</DefaultValue>
      <Value>$(SqlCmdVar__1)</Value>
    </SqlCmdVariable>
  </ItemGroup>
</Project>
```

In this example, the AdventureWorks `.dacpac` file is published as a package `Contoso.AdventureWorks` version `1.1.0` to a NuGet feed. The `<DatabaseSqlCmdVariable>` element specifies the name of the database on the same server where the objects in the package are located and would be used to indicate this reference in three-part naming. The [SQLCMD variable](sqlcmd-variables.md) `AdventureDB` is used to set the database name at deployment time and is used in the project similarly to this example query:

```sql
SELECT *
FROM [$(AdventureDB)].dbo.Customers;
```

The `<DacpacName>` element specifies the name of the `.dacpac` file for the package reference, without the file extension or path. The `<DacpacName>` element is optional and is only required when the name of the `.dacpac` file is different from the name of the package.

### System databases

The SQL system databases (`master`, `msdb`) are published on NuGet.org as database reference packages. These packages contain the schema for the system databases and can be used as package references in SQL projects. The system database packages are versioned to align with the version of SQL Server they're associated with. For example, the `master` system database package for SQL Server 2022 is `Microsoft.SqlServer.Dacpacs.Master` version `160.2.1` and can be added to a SQL project as a package reference:

```xml
...
  <ItemGroup>
    <PackageReference Include="Microsoft.SqlServer.Dacpacs.Master" Version="160.2.1" />
  </ItemGroup>
</Project>
```

Minor version changes reflect bug fixes and minor changes to the schema within a SQL Server version.

The available system database packages are:

- [SQL Server `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Master)
- [SQL Server `msdb` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Msdb)
- [Azure SQL Database `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Azure.Master)
- [SQL database in Fabric system objects](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.DbFabric)
- [Data warehouse in Fabric `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.FabricDw)
- [Azure Synapse Analytics `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Synapse.Master)
- [Azure Synapse Analytics serverless pools `master` system database](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.SynapseServerless.Master)

### Package dacpac NuGet packages

A database reference package is a NuGet package that contains a `.dacpac` file. The NuGet package can be published to a NuGet feed, such as Azure Artifacts, for use in SQL projects. Creating this package follows the same process as creating a NuGet package for other types of projects. For more information, see [Creating a package with the dotnet CLI](/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli).

:::image type="content" source="media/package-references/build-pack.png" alt-text="Screenshot of Summary of package reference for SQL projects process." lightbox="media/package-references/build-pack.png":::

To package a `.sqlproj` file as a NuGet package, use the `dotnet pack` command from the command line. By default, the `dotnet pack` command creates a NuGet package from the `.sqlproj` file in the `bin/Debug` folder.

Package metadata can be specified by properties inside the `<PropertyGroup>` element in the `.sqlproj` file. For example, the following properties specify the package ID, version, description, authors, and company:

```xml
<PackageId>Contoso.AdventureWorks.SalesLT</PackageId>
<Version>1.0.0</Version>
<Description>AdventureWorks database SalesLT objects</Description>
<Authors>DevTeam</Authors>
<Company>Contoso Outdoors</Company>
```

The `.nupkg` file created by the `dotnet pack` command can be published to a NuGet feed for use in SQL projects. These database objects can be viewed by anyone with access to the package, so consideration should be made for selecting a public or private feed location. For more information, see [Hosting with private package feeds](/nuget/hosting-packages/overview).

When referencing a package where the `PackageId` is different from the name of the `.dacpac` file, the `<DacpacName>` element is required in the package reference when consuming the package.

## Extended code analysis rules package references

Package references can also be used to reference more code analysis rules that were developed as part of [code analysis extensibility](code-analysis-extensibility.md). The package reference for a code analysis rule package is similar to the package reference for a database object package. The following example shows a package reference to a custom code analysis rule package:

```xml
<ItemGroup>
  <PackageReference Include="Your.CustomCode.AnalysisRules" Version="1.2.3" />
</ItemGroup>
```

The package being referenced should be available on a [NuGet feed](/dotnet/core/tools/dotnet-nuget-add-source#examples), such as NuGet.org, Azure Artifacts, or a local source. When a package containing code analysis rules is referenced, the rules are automatically included in the SQL project and are evaluated when the project property `RunSqlCodeAnalysis` is set to true. The rules can be disabled or elevated to return an error individually in the project properties.

## Related content

- [Database references overview](database-references.md)
- [SQL projects system objects](system-objects.md)
- [Code analysis rules extensibility overview](code-analysis-extensibility.md)
- [Creating a package with the dotnet CLI](/nuget/create-packages/creating-a-package-dotnet-cli)
- [Hosting with private package feeds](/nuget/hosting-packages/overview)
