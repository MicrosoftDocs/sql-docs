---
title: Database references overview
description: "Extend a SQL project with references to additional database components."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
zone_pivot_groups: sq1-sql-projects-tools
f1_keywords:
  - "sql.data.tools.adddatabasereference.dialog"
  - "sql.data.tools.newdatabase.dialog"
  - "sql.data.tools.criticalerror.dialog"
---

# Database references overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Database references in SQL projects enable you to incorporate objects that aren't included in a project by linking to another project, `.dacpac` file, or published NuGet package. The database objects added to a project can be part of the same database, a different database on the same server, or a different database on a different server. For SQL Server development, database references can be used to link to another database on the same server for three-part naming, or to link to a different database on a different server for cross-database queries. For databases with a large number of objects in distinct groups, database references can be used to break up a database into smaller, more manageable projects. Smaller project size can help to improve performance and reduce the time required to build a project during iterative local development.

:::image type="content" source="media/database-references/database-references.png" alt-text="Screenshot of Example of a SQL project referencing a dacpac, a nuget package, and a project for database references." lightbox="media/database-references/database-references.png":::

> [!NOTE]  
> Project references and NuGet package references are the recommended methods for database references in new development. Referencing NuGet packages isn't supported by original SQL projects.

## SQL project file sample and syntax

Database references are included in a project through entries in the `.sqlproj` file, similar to C# projects. When a database reference is to a different database on the same server, a `<DatabaseSqlCmdVariable>` element is included in the project reference. When a database reference is to a different database on a different server, a `<ServerSqlCmdVariable>` element is also included in the project reference. Database references to the same database don't include `<ServerSqlCmdVariable>` or `<DatabaseSqlCmdVariable>` elements.

Including a specific reference to the database reference in the SQL scripts use SQLCMD variables named in the project file to specify the database name. For example, the following SQL script references a table in the `Warehouse` database:

```sql
SELECT ProductId, StorageLocation, BinNumber
FROM [$(Warehouse)].[Production].[ProductInventory]
```

A database reference corresponding to the `$(Warehouse)` SQLCMD variable is included in the project file and contains `<DatabaseSqlCmdVariable>Warehouse</DatabaseSqlCmdVariable>`.

### Project references

In this example, a project reference is added to a SQL project `AdventureWorksSalesLT.sqlproj` that is part of the same database.

```xml
  <ItemGroup>
    <ProjectReference Include="..\AdventureWorks\AdventureWorksSalesLT.sqlproj">
      <Name>AdventureWorksSalesLT</Name>
      <Project>{d703fc7a-bc47-4aef-9dc5-cf01094ddb37}</Project>
      <Private>True</Private>
      <SuppressMissingDependenciesErrors>False</SuppressMissingDependenciesErrors>
    </ProjectReference>
  </ItemGroup>
```

### Dacpac package references

More information on package references in SQL projects can be found in the [SQL projects package references](package-references.md) article.

A [package reference](package-references.md) to the `master` system database for SQL 2022 is shown in the following example:

```xml
  <ItemGroup>
    <PackageReference Include="Microsoft.SqlServer.Dacpacs.Master" Version="160.2.1" />
  </ItemGroup>
```

### Dacpac artifact references

References to a `.dacpac` artifact file directly aren't recommended for new development in SDK-style projects. Instead, use [NuGet package references](package-references.md).

In original SQL projects, `.dacpac` file references are specified in the `.sqlproj` file with an `<ArtifactReference>` item. The following example shows a `.dacpac` artifact reference to a `.dacpac` file in a different project on the same server:

```xml
  <ItemGroup>
    <ArtifactReference Include="..\AdventureWorks\Warehouse\bin\Release\Warehouse.dacpac">
      <HintPath>..\AdventureWorks\Warehouse\bin\Release\Warehouse.dacpac</HintPath>
      <SuppressMissingDependenciesErrors>False</SuppressMissingDependenciesErrors>
      <DatabaseSqlCmdVariable>Warehouse</DatabaseSqlCmdVariable>
    </ArtifactReference>
  </ItemGroup>
```

## Add and use project references

### Add a project reference

::: zone pivot="sq1-visual-studio"

To add a project reference to a SQL project in Visual Studio, right-click the **References** node under the project in **Solution Explorer** and select **Add Database Reference**.

:::image type="content" source="media/database-references/ssdt-add-reference.png" alt-text="Screenshot of The Visual Studio dialog for database references." lightbox="media/database-references/ssdt-add-reference.png":::

The **Add Database Reference** dialog presents options for adding a reference to:

- a SQL project from the same solution
- a system database (from `.dacpac` files automatically included with Visual Studio)
- any data-tier application (`.dacpac`) file on the local file system

The dialog also provides a dropdown list to select from the following reference locations:

- same database
- different database, same server
- different database, different server

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

To add a project reference to a SQL project, add an `<ItemGroup>` item to the `.sqlproj` file with an appropriate reference item for each database reference. For example, the following project reference is added to a SQL project to reference the `WorldWideImporters` project in a different database on a different server:

```xml
  <ItemGroup>
    <ProjectReference Include="..\Contoso\WorldWideImporters.sqlproj">
      <Name>WorldWideImporters</Name>
      <Project>{d703fc7a-bc47-4aef-9dc5-cf01094ddb37}</Project>
      <SuppressMissingDependenciesErrors>False</SuppressMissingDependenciesErrors>
      <ServerSqlCmdVariable>WWIServer</ServerSqlCmdVariable>
      <DatabaseSqlCmdVariable>WorldWideImporters</DatabaseSqlCmdVariable>
    </ProjectReference>
  </ItemGroup>
```

The project reference is used in a sample view definition in the SQL project:

```sql
CREATE VIEW dbo.WorldWide_Products
AS
SELECT ProductID, ProductName, SupplierID
FROM [$(WWIServer)].[$(WorldWideImporters)].[Purchasing].[Suppliers]
```

::: zone-end

::: zone pivot="sq1-visual-studio-code"

To add a database reference to a SQL project in the SQL Database Projects extension, right-click the **Database References** node under the project in the **Database Projects** view and select **Add Database Reference**.

:::image type="content" source="media/database-references/ads-add-reference.png" alt-text="Screenshot of Azure Data Studio add reference dialog.":::

The available reference types are:

- system database
- data-tier application (`.dacpac`)
- published data-tier application (`.nupkg`)
- project

The extension also prompts to select from the following reference locations:

- same database
- different database, same server
- different database, different server

::: zone-end

::: zone pivot="sq1-command-line"

To add a project reference to a SQL project, add an `<ItemGroup>` item to the `.sqlproj` file with an appropriate reference item for each database reference. For example, the following project reference is added to a SQL project to reference the `WorldWideImporters` project in a different database on a different server:

```xml
  <ItemGroup>
    <ProjectReference Include="..\Contoso\WorldWideImporters.sqlproj">
      <Name>WorldWideImporters</Name>
      <Project>{d703fc7a-bc47-4aef-9dc5-cf01094ddb37}</Project>
      <SuppressMissingDependenciesErrors>False</SuppressMissingDependenciesErrors>
      <ServerSqlCmdVariable>WWIServer</ServerSqlCmdVariable>
      <DatabaseSqlCmdVariable>WorldWideImporters</DatabaseSqlCmdVariable>
    </ProjectReference>
  </ItemGroup>
```

The project reference is used in a sample view definition in the SQL project:

```sql
CREATE VIEW dbo.WorldWide_Products
AS
SELECT ProductID, ProductName, SupplierID
FROM [$(WWIServer)].[$(WorldWideImporters)].[Purchasing].[Suppliers]
```

::: zone-end

### Build with project references

Building a SQL project with database references might require extra configuration to ensure that the referenced objects are available during the build process. For example, if a project is being built in a continuous integration (CI) pipeline, the build agent environment needs to be set up similarly to the local development environment.

- `.dacpac` references in the SQL project require that the `.dacpac` be present on the build agent at the same relative file path as specified in the project file.
- project references in the SQL project require that the referenced project must be present on the build agent at the same relative file path as specified in the project file and be able to build successfully on the build agent.
- system database references created in original SQL projects in Visual Studio require that the build agent have Visual Studio installed.
- NuGet package references in the SQL project require the package be published to a NuGet feed that is also set as a package source for the build agent.

### Publish with project references

Publishing a `.dacpac` built from a project with database references requires no extra steps. The `.dacpac` file contains the referenced objects and the SQLCMD variables specified in the project file.

For database references to objects in the same database, the objects from the referenced project are included in the `.dacpac` file but aren't included in the deployment by default. To include the objects in the deployment, use the `/p:IncludeCompositeObjects=true` option in the SqlPackage command line tool. For example, the following command deploys the `AdventureWorks` project with the `/p:IncludeCompositeObjects=true` option to include the objects from database references to AdventureWorks:

```bash
sqlpackage /Action:Publish /SourceFile:AdventureWorks.dacpac /TargetConnectionString:{connection_string_here} /p:IncludeCompositeObjects=true
```

When you deploy a `.dacpac` file with database references to different database (on same or different server), the SQLCMD variables specified in the project file must be set to the correct values for the target environment. Setting the SQLCMD variable values during deployment is done with the `/v` option in the [SqlPackage](../../sqlpackage/sqlpackage-publish.md#sqlcmd-variables) command line tool. For example, the following command sets the `WorldWideImporters` variable to `WorldWideImporters` and the `WWIServer` variable to `localhost`:

```bash
sqlpackage /Action:Publish /SourceFile:AdventureWorks.dacpac /TargetConnectionString:{connection_string_here} /v:WorldWideImporters=WorldWideImporters /v:WWIServer=localhost
```

## Related content

- [SQL projects package references overview](package-references.md)
- [SQLCMD variables overview](sqlcmd-variables.md)
