---
title: DacFx and SqlPackage Release Notes
description: Review features, fixes, and changes in current and previous releases of Microsoft DacFx and the SqlPackage command-line utility.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mahyon, llali
ms.date: 09/15/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: release-notes
ms.collection:
  - data-tools
ms.custom:
  - "tools|sos"
  - msecd-doc-authoring-108
ai-usage: ai-assisted
---

# Release notes for SqlPackage

**[Download the latest version](sqlpackage-download.md)**

This article lists the features and fixes delivered by the released versions of SqlPackage.

## How to read these release notes

The **Applies to** column in each section is scoped as follows:

- **SqlPackage CLI** - command-line actions (publish, import, export, extract, Parquet, diagnostics, dotnet tool)
- **MSBuild / SQL projects** - SQL project build (`Microsoft.Build.Sql` SDK, SQL Server Data Tools (SSDT) integration)
- **DacFx API / Schema compare** - `Microsoft.SqlServer.DacFx` NuGet APIs, schema compare
- **Platform** - ScriptDom, Microsoft.Data.SqlClient, .NET support, system DACPACs, compatibility defaults

## Current releases (170.x)

The following releases are the currently supported versions of SqlPackage.

## 170.5.96.0 SqlPackage

**Release date:** September 15, 2026

```bash
dotnet tool install -g microsoft.sqlpackage --version 170.5.96.0
```

| Platform | Download |
| --- | --- |
| Windows .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2379708) |
| Windows .NET Framework | [.msi file](https://go.microsoft.com/fwlink/?linkid=2379707) |
| macOS .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2379817) |
| Linux .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2379816) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Extract | Added the `DacExtractOptions.PermissionGranteeScope` property and the `DacPermissionGranteeScope.DatabaseRolesOnly` option to extract only permissions granted or denied to database roles. By default, extract operations continue to include permissions for all database principals. The option is also available as a SqlPackage extract property. | SqlPackage CLI; DacFx API / Schema compare |
| Platform | Added the read-only `SqlCmdVariables` and `TargetPlatform` properties to `DacPackage`, which let callers inspect a package's SQLCMD variables and target platform before deployment. | DacFx API / Schema compare |
| Schema compare | Added the `IncludeAll()` and `ExcludeAll()` methods to `SchemaComparisonResult` to change the inclusion state of all excludable schema differences. | DacFx API / Schema compare |
| SQL projects | Added `SkipExistingModelValidation` to `TSqlObjectOptions`. This option lets model add, update, and delete operations proceed when the model already has errors, supporting live IntelliSense model updates in SQL projects. | DacFx API / Schema compare |
| Vector | Added support for vector column dimensions and base types in table-valued parameters and function return types. | MSBuild / SQL projects; DacFx API / Schema compare |
| ScriptDom | Updated ScriptDom to version 180.102.0. | Platform |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Code analysis | Fixed an issue where rule SR0007 incorrectly reported nullable-column warnings for the null-safe `IS DISTINCT FROM` and `IS NOT DISTINCT FROM` predicates. [GitHub issue](https://github.com/microsoft/DacFx/issues/825) | MSBuild / SQL projects |
| Deployment | Fixed an issue where `DoNotDropExtendedProperties=True` wasn't honored because of conflicting deployment options. [GitHub issue](https://github.com/microsoft/DacFx/issues/139) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where DacFx generated an unnecessary `ALTER TRIGGER` statement when a trigger schema wasn't defined. [GitHub issue](https://github.com/microsoft/DacFx/issues/761) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where publishing inline constraints from a project emitted a redundant `ALTER TABLE ... ADD CONSTRAINT` statement. [GitHub issue](https://github.com/microsoft/DacFx/issues/792) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where deployment generated an incorrect drop statement for unnamed default constraints. [GitHub issue](https://github.com/microsoft/DacFx/issues/807) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where `sp_addextendedproperty` used `@level1type=N'NULL'` for an extended property on a security policy. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where a BACPAC import or deployment could deploy a stored procedure that uses `OPENROWSET(BULK ..., DATA_SOURCE = ...)` before its external data source. [GitHub issue](https://github.com/microsoft/DacFx/issues/809) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where a deployment preview report didn't show default constraints that a table rebuild would re-create after column reordering. The generated deployment script was already correct. [GitHub issue](https://github.com/microsoft/vscode-mssql/issues/21132) | SqlPackage CLI; DacFx API / Schema compare |
| Export | Fixed an issue where a large BACPAC export failed with a `Stream was too long` overflow in `System.IO.Packaging`. | SqlPackage CLI |
| Export | Fixed an issue when exporting large, fixed-length Always Encrypted columns to a BACPAC. | SqlPackage CLI |
| Extract | Fixed an `InvalidCastException` when extracting object-level permissions on a security policy. [GitHub issue](https://github.com/microsoft/DacFx/issues/800) | SqlPackage CLI; DacFx API / Schema compare |
| Extract | Fixed an issue where Azure SQL Database extraction lost user-to-login mappings for Microsoft Entra external logins or conventional SQL logins. [GitHub issue](https://github.com/microsoft/DacFx/issues/571) | SqlPackage CLI; DacFx API / Schema compare |
| Extract | Fixed an issue where extracting a SQL project emitted per-batch `SET ANSI_NULLS` and `SET QUOTED_IDENTIFIER` options into the generated `.sqlproj`, which prevented deployment of objects that require those options in Data Warehouse in Microsoft Fabric. | SqlPackage CLI; MSBuild / SQL projects |
| Extract | Fixed an issue where reverse engineering failed with `SQL72018: SqlExternalLanguage could not be imported` when system-defined external languages were present in SQL Server 2027. | SqlPackage CLI; DacFx API / Schema compare |
| Extract | Fixed an issue where reverse engineering external languages on Azure SQL Managed Instance failed because `is_system_language` couldn't be bound. | SqlPackage CLI; DacFx API / Schema compare |
| Import | Fixed an issue where a BACPAC import left a large `VARBINARY(MAX)` value empty when the table's only unique key contained a `NULL` column. | SqlPackage CLI |
| Import | Fixed an issue where cloud BACPAC imports with vector indexes failed before table data was loaded. Vector index creation now completes during the data phase. | SqlPackage CLI |
| Platform | Fixed an issue where legacy DACPAC deserialization failed on non-seekable package-part streams. | SqlPackage CLI; DacFx API / Schema compare |
| Platform | Fixed an issue where `DacPackage.Unpack` allowed package-part paths to resolve outside the requested destination directory. | DacFx API / Schema compare |
| Schema compare | Fixed an issue where schema comparison against Data Warehouse in Microsoft Fabric included primary key and other constraints from excluded tables in the generated deployment script. | DacFx API / Schema compare |
| Schema compare | Fixed an issue where schema comparison emitted redundant child-element scripts for inline-scriptable elements. | DacFx API / Schema compare |
| SQL projects | Fixed an issue where `SQL71558` identifier-casing warnings were reported for references resolved through synonyms. [GitHub issue](https://github.com/microsoft/DacFx/issues/819) | MSBuild / SQL projects |
| SQL projects | Fixed an issue where creating or updating a SQL project produced nondeterministic object ordering. [GitHub issue](https://github.com/microsoft/DacFx/issues/795) | MSBuild / SQL projects |
| SQL projects | Fixed an issue where SQL project builds failed when referencing external data, such as with `OPENROWSET`. [GitHub issue](https://github.com/microsoft/DacFx/issues/802) | MSBuild / SQL projects |
| SQL projects | Fixed `VECTOR_SEARCH` table alias and distance column resolution in SQL project builds. | MSBuild / SQL projects; DacFx API / Schema compare |
| SQL projects | Updated error message locations to use `(line,col)` instead of `(line,col,line,col)`. | MSBuild / SQL projects |

## 170.4.83.3 SqlPackage

**Release date:** June 3, 2026

```bash
dotnet tool install -g microsoft.sqlpackage --version 170.4.83.3
```

| Platform | Download |
| --- | --- |
| Windows .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2366026) |
| Windows .NET Framework | [.msi file](https://go.microsoft.com/fwlink/?linkid=2366025) |
| macOS .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2365929) |
| Linux .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2365831) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Database option | Added support for database option `AUTOMATIC_INDEX_COMPACTION`. [Automatic index compaction documentation](../../relational-databases/indexes/automatic-index-compaction.md) | SqlPackage CLI; DacFx API / Schema compare |
| Dynamic data masking | Added support of dynamic data masking to the extract and publish operations for Data Warehouse in Microsoft Fabric. | SqlPackage CLI; DacFx API / Schema compare |
| Extract | Added support to extract a database to SQL project format. [GitHub issue](https://github.com/microsoft/DacFx/issues/760) | SqlPackage CLI; DacFx API / Schema compare |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/6.1.5) v6.1.5. | Platform |
| ScriptDom | Updated ScriptDom to version 180.18.1. | Platform |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed deploy failures caused by schema-bound function blocking `ALTER TABLE` operations. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue with preserving external model ownership information when deploying to a database. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue with deploying a table with `CLUSTER BY` to a Data Warehouse in Microsoft Fabric. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue with deploying a table with `IDENTITY` column to a Data Warehouse in Microsoft Fabric. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where a DacPac file is searched in uppercase form during deployment, which could break deployment in some locales such as Turkish. [GitHub issue](https://github.com/microsoft/DacFx/issues/383) | SqlPackage CLI; DacFx API / Schema compare |
| Import | Fixed an issue where database and DDL triggers inserting data to tables could interfere with the import process. | SqlPackage CLI; DacFx API / Schema compare |
| Schema compare | Fixed an issue where increasing the length of a **varchar** field leads to a data loss warning, and a check that fails when rows are present. [GitHub issue](https://github.com/microsoft/DacFx/issues/724) | SqlPackage CLI; DacFx API / Schema compare |
| SQL projects | Fixed a bug when generating SQL files from a project in non-Windows machines. | MSBuild / SQL projects |
| SQL projects | Fixed an issue where generated scripts using a `.scmp` file as source contained unwanted `SET ANSI_NULLS OFF` commands. [GitHub issue](https://github.com/microsoft/DacFx/issues/172) | MSBuild / SQL projects; DacFx API / Schema compare |
| Schema compare | Fixed an issue where the exception message was incorrect when the `TargetScripts` tag was missing from a `.scmp` file. [GitHub issue](https://github.com/microsoft/DacFx/issues/111) | DacFx API / Schema compare |
| Deployment | Fixed an issue where a `NullReferenceException` occurred when using `IncludeCompositeObjects=true` with a `SameDatabase` reference. [GitHub issue](https://github.com/microsoft/DacFx/issues/775) | SqlPackage CLI; DacFx API / Schema compare |
| Platform | Improved the error message when specifying an unsupported storage model on .NET Core. [GitHub issue](https://github.com/microsoft/DacFx/issues/47) | Platform; SqlPackage CLI |
| Fabric Data Warehouse | Fixed an issue where deploying tables with `IDENTITY` columns to Fabric Data Warehouse failed due to unsupported `SEED` or `INCREMENT` syntax. [GitHub issue](https://github.com/microsoft/DacFx/issues/747) | SqlPackage CLI; DacFx API / Schema compare |

## 170.3.93 SqlPackage

**Release date:** February 10, 2026

```bash
dotnet tool install -g microsoft.sqlpackage --version 170.3.93
```

| Platform | Download |
| --- | --- |
| Windows .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2350827) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2350826) |
| macOS .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2350828) |
| Linux .NET 10 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2350619) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Added support for database options `ACCELERATED_DATABASE_RECOVERY` and `OPTIMIZED_LOCKING`. | SqlPackage CLI; DacFx API / Schema compare |
| Permissions | Enhances permission publishing to include `EXECUTE ON EXTERNAL MODEL` permissions. | SqlPackage CLI; DacFx API / Schema compare |
| Platform | Added .NET 10 support to the DacFx library and the SqlPackage CLI. The SqlPackage `dotnet tool` is available for both .NET 8 and .NET 10. | Platform; SqlPackage CLI; DacFx API / Schema compare |
| Platform | Added .NET Standard 2.0 support to the DacFx library. | Platform; DacFx API / Schema compare |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/6.1.3) v6.1.3. | Platform |
| ScriptDom | Updated ScriptDom to version 170.157.0. | Platform |
| Vector | Extends vector column support to allow changing the base type. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed a bug with deploying to [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] when the target database includes a security policy. | SqlPackage CLI; DacFx API / Schema compare |
| Export | Fixed an issue where exported `.dacpac` files fail XSD schema validation due to boolean attribute values using `True`/`False` values instead of lowercase `true`/`false`. [GitHub issue](https://github.com/microsoft/DacFx/issues/604) | SqlPackage CLI |
| Import | Fixed an issue where importing a table with special characters in the name (such as `/`, `"`, or `$`) silently fails to import data without warning the user. [GitHub issue](https://github.com/microsoft/DacFx/issues/637) | SqlPackage CLI |
| Ledger | Fixed an issue where a ledger table with a computed column causes a `NullReferenceException` during model validation and build. [GitHub issue](https://github.com/microsoft/DacFx/issues/735) | MSBuild / SQL projects; DacFx API / Schema compare |
| SQL projects | Fixed an issue where building a SQL project with an inline clustered columnstore index definition on a table fails with a syntax error. [GitHub issue](https://github.com/microsoft/DacFx/issues/719) | MSBuild / SQL projects |
| SQL projects | Fixed an issue where a clustered columnstore index on a table with `NVARCHAR(MAX)` or other LOB-type columns incorrectly reports an error that columnstore indexes aren't supported with vector columns. [GitHub issue](https://github.com/microsoft/DacFx/issues/713) | MSBuild / SQL projects |
| Vector | Fixed an issue where procedures using `VECTOR_SEARCH` report a validation warning that the column reference couldn't be resolved. [GitHub issue](https://github.com/microsoft/DacFx/issues/706) | MSBuild / SQL projects; DacFx API / Schema compare |

## 170.2.70 SqlPackage

**Release date:** October 14, 2025

```bash
dotnet tool install -g microsoft.sqlpackage --version 170.2.70
```

| Platform | Download |
| --- | --- |
| Windows .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2338326) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2338524) |
| macOS .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2338443) |
| Linux .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2338525) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| External models | Added support for external models in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] for import, export, extract, deployment, and SQL project build. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| External models | Added support for functions `AI_GENERATE_CHUNKS` and `AI_GENERATE_EMBEDDINGS`. | SqlPackage CLI; DacFx API / Schema compare |
| JSON | Added support for JSON indexes in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] for import, export, extract, deployment, and SQL project build. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| JSON | Added support for JSON functions `JSON_ARRAYAGG`, `JSON_OBJECTAGG`, and `JSON_QUERY`. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| Platform | The SqlPackage `.zip` build .NET SDK is updated to 8.0.414 | Platform; SqlPackage CLI |
| Regex | Added support for the `REGEXP_LIKE` function. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| ScriptDom | Updated ScriptDom to version 170.128.0. | Platform |
| Vector | Added support for the vector indexes in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] for import, export, extract, deployment, and SQL project build. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| Vector | Expands support for vector data type to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and specifying the 32-bit float size. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| Vector | Added support for Vector function `VECTOR_SEARCH`. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Data masking | Fixed an issue where the datetime data masking functions weren't recognized during project build. [GitHub issue](https://github.com/microsoft/DacFx/issues/476) | MSBuild / SQL projects |
| External models | Fixed an issue where external model dependencies on a database-scoped credential cause the project build to fail. | MSBuild / SQL projects |
| JSON | Fixed a bug when comparing JSON indexes with the default JSON path. | DacFx API / Schema compare; SqlPackage CLI |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.1.7) v5.1.7. | Platform |
| XML | Fixed an issue where the XML query path ([xQuery](../../xquery/xquery-language-reference-sql-server.md)) wasn't treated as case-sensitive during model comparison. Even on case-insensitive databases, the xQuery path is case-sensitive. [GitHub issue](https://github.com/microsoft/DacFx/issues/231) | DacFx API / Schema compare; SqlPackage CLI |

## 170.1.61 SqlPackage

**Release date:** July 30, 2025

```bash
dotnet tool install -g microsoft.sqlpackage --version 170.1.61
```

| Platform | Download |
| --- | --- |
| Windows .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2329922) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2329732) |
| macOS .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2329924) |
| Linux .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2329923) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Data virtualization | Added support for objects related to [data virtualization](/azure/azure-sql/database/data-virtualization-overview?view=azuresql&tabs=sas&preserve-view=true) for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] in import, export, extract, and publish operations. | SqlPackage CLI |
| Data virtualization | Added [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] to the [publish with parquet files preview](sqlpackage-with-data-in-parquet-files.md), enabling the use of Azure Blob Storage and parquet files to import data to a database. Extracting data to parquet files isn't available for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] as it depends on [functionality not available in Azure SQL Database](/azure/azure-sql/database/data-virtualization-overview?view=azuresql&tabs=sas&preserve-view=true#limitations), `CREATE EXTERNAL TABLE AS SELECT`. | SqlPackage CLI |
| Deployment | Added support for publish properties `/p:IgnorePreDeployScript` and `/p:IgnorePostDeployScript`. These properties default to `false` and when enabled result in the deployment plan omitting the corresponding scripts. [GitHub issue](https://github.com/microsoft/DacFx/issues/647) | SqlPackage CLI; DacFx API / Schema compare |
| Permissions | Added support for the permission `ALTER ANY EXTERNAL MIRROR` for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]. This permission is required to export a database that contains one or more [mirrored tables](/fabric/database/mirrored-database/azure-sql-database-limitations). [GitHub issue](https://github.com/microsoft/DacFx/issues/648) | SqlPackage CLI; DacFx API / Schema compare |
| Permissions | Added support for the permissions `CREATE ANY EXTERNAL MODEL` and `ALTER ANY EXTERNAL MODEL` for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. | SqlPackage CLI; DacFx API / Schema compare |
| Permissions | Added support for the permission `ALTER ANY INFORMATION PROTECTION` for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. | SqlPackage CLI; DacFx API / Schema compare |
| Platform | The SqlPackage `.zip` build .NET SDK is updated to 8.0.412. | Platform; SqlPackage CLI |
| ScriptDom | Updated ScriptDom to version 170.0.64. | Platform |
| ScriptDom | Updates the Data warehouse in Fabric platform to use the `TSqlFabricDWParser` in ScriptDom. | Platform; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Always Encrypted | Fixed an issue where the `Interactive` option for the `/AzureKeyVaultAuthMethod` publish parameter wasn't working correctly. | SqlPackage CLI |
| Deployment | Fixed an issue where the `DbScopedConfigMaxDOPSecondary` [property](../sql-database-projects/concepts/project-properties.md) wasn't set correctly in the deployment. [GitHub issue](https://github.com/microsoft/DacFx/issues/597) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where publishing tables containing foreign key constraints fails with syntax not supported in Data warehouse in Microsoft Fabric. | SqlPackage CLI |
| Deployment | Fixed an issue where server objects were included when deploying to [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]. Server objects aren't supported in [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]. [GitHub issue](https://github.com/microsoft/DacFx/issues/646) | SqlPackage CLI |
| Deployment | Fixed an issue where clustered columnstore indexes are created by first creating a clustered index, which increases deployment overhead and time. [GitHub issue](https://github.com/microsoft/DacFx/issues/264) | SqlPackage CLI |
| Extract | Fixed an issue where extracting a database from [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] results in unusable user definition scripts. [GitHub issue](https://github.com/microsoft/DacFx/issues/631) | SqlPackage CLI |

## 170.0.94 SqlPackage

**Release date:** April 15, 2025

```bash
dotnet tool install -g microsoft.sqlpackage --version 170.0.94
```

| Platform | Download |
| --- | --- |
| Windows .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2316204) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2316310) |
| macOS .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2316113) |
| Linux .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2316311) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| DACPACVerify | Support for verifying the contents of two `.dacpac` files is added in the preview tool [Microsoft.DacpacVerify](https://www.nuget.org/packages/Microsoft.dacpacverify). | DacFx API / Schema compare |
| Extract | Added support for unpacking a `.dacpac` file to a folder. The `/Action:Extract` command is used with `/SourceFile:` and `/TargetFile:` parameters. The target file must be a folder and when a source file (`.dacpac`) is specified no database connection properties are valid. The property `/p:ExtractTarget=SchemaObjectType` is required for extracting from a `.dacpac` file instead of a source database. The executable `Dacunpack.exe` is removed from the `DacFx.msi` installer. | SqlPackage CLI; DacFx API / Schema compare |
| Parquet | Added support for the data types timestamp, rowversion, uniqueidentifier, text, ntext, image, json, xml, and vector when using Parquet files in Azure Blob Storage with SqlPackage extract and publish operations. | SqlPackage CLI |
| Parquet | Added support for exporting and importing table data to BCP files when the table contains an unsupported data type while using Parquet files in Azure Blob Storage for the remaining data with SqlPackage extract and publish operations. An example data type is **sql_variant**, and a table containing a column of that type is written to BCP in the `.dacpac` while the remaining tables are written to Parquet files in Azure Blob Storage. | SqlPackage CLI |
| Platform | Updated SqlPackage for .NET Framework version to .NET Framework 4.7.2. | Platform; SqlPackage CLI |
| Platform | Removed support for .NET 6. | Platform; SqlPackage CLI |
| ScriptDom | Updated ScriptDom to version 170.18.0. | Platform |
| SQL projects | The default compatibility level for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] is now set to 170. For more information, see [Database compatibility level 170 in Azure SQL Database and SQL database in Microsoft Fabric](https://techcommunity.microsoft.com/blog/azuresqlblog/database-compatibility-level-170-in-azure-sql-database-and-sql-database-in-micro/4405102). | MSBuild / SQL projects |
| Vector | Vector data type is now supported in the target platform `Azure SQL Database` for import, export, extract, deployment, and SQL project build. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an issue where deploying a change to an external table causes all external tables to be dropped and recreated if the modified table contained no values for `REJECT_VALUE` or `REJECT_SAMPLE_VALUE`. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where altering a column to expand its length results in an incorrect data loss warning if the column's collation is specified and matches the database default collation. [GitHub issue](https://github.com/microsoft/DacFx/issues/453) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where sensitivity classification changes on a table cause indexes to be recreated. [GitHub issue](https://github.com/microsoft/DacFx/issues/567) | SqlPackage CLI; DacFx API / Schema compare |
| Export | Optimized the use of table size estimation during export operations. | SqlPackage CLI |
| Extract | Fixed an issue where extracting a `.dacpac` with a database reference fails. [GitHub issue](https://github.com/microsoft/DacFx/issues/343) | SqlPackage CLI; DacFx API / Schema compare |
| Fabric Data Warehouse | Fixed an issue where the `sp_refreshsqlmodule` system stored procedure is called during a publish operation. Fabric Data Warehouse doesn't support `sp_refreshsqlmodule` and this causes the deployment to fail. | SqlPackage CLI |
| Fabric Data Warehouse | Fixed an issue where table constraints are included in a `.dacpac` but not `.sql` files when a database was extracted from a Fabric Data Warehouse. [GitHub issue](https://github.com/microsoft/DacFx/issues/589) | SqlPackage CLI |
| SQL projects | Fixed an issue where building without SSDT installed in Visual Studio results in incorrect build behavior. [GitHub issue](https://github.com/microsoft/DacFx/issues/99) | MSBuild / SQL projects |
| SQL projects | Fixed an issue where build warnings are duplicated in output when code analysis is enabled. [GitHub issue](https://github.com/microsoft/DacFx/issues/481) | MSBuild / SQL projects |
| SQL projects | Fixed an issue where [service broker](../../database-engine/configure-windows/sql-server-service-broker.md) parameters set to variables result in false warnings. [GitHub issue](https://github.com/microsoft/DacFx/issues/326) | MSBuild / SQL projects |

## Archived releases (162.x and earlier versions)

You can find release notes for version 162.x and older versions in the [Release notes for SqlPackage (archive)](release-notes-sqlpackage-archive.md).
