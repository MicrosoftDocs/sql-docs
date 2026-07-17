---
title: DacFx and SqlPackage Release Notes (Archive)
description: Release note archive for Microsoft SqlPackage for 162.x and earlier versions.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, llali
ms.date: 03/16/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: release-notes
ms.collection:
  - data-tools
ms.custom:
  - "tools|sos"
---

# Release notes for SqlPackage (archive)

**[Download the latest version](sqlpackage-download.md)**

This article lists the features and fixes delivered by the released versions of SqlPackage.

## Current releases

For the latest version information, see [Release notes for SqlPackage](release-notes-sqlpackage.md).

## How to read these release notes

The **Applies to** column in each section is scoped as follows:

- **SqlPackage CLI** - command-line actions (publish, import, export, extract, Parquet, diagnostics, dotnet tool)
- **MSBuild / SQL projects** - SQL project build (`Microsoft.Build.Sql` SDK, SQL Server Data Tools (SSDT) integration)
- **DacFx API / Schema compare** - `Microsoft.SqlServer.DacFx` NuGet APIs, schema compare
- **Platform** - ScriptDom, Microsoft.Data.SqlClient, .NET support, system DACPACs, compatibility defaults

## Archived releases (162.x and earlier versions)

The following releases are archived and no longer supported.

## 162.5.57 SqlPackage

**Release date:** November 21, 2024

```bash
dotnet tool install -g microsoft.sqlpackage --version 162.5.57
```

| Platform | Download |
| --- | --- |
| Windows .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2297835) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2297931) |
| macOS .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2297647) |
| Linux .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2297646) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| DACPACs | System DACPAC updates for Synapse Serverless and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. | Platform; DacFx API / Schema compare |
| Diagnostics | Added a new command line parameter to specify the logging level. `/DiagnosticsLevel:` | SqlPackage CLI |
| Diagnostics | Added a new command line parameter to output a `.zip` diagnostics package, containing target and source model information along with diagnostic logging, deploy script, and deploy report. `/DiagnosticPackageFile:` | SqlPackage CLI |
| Fabric Data Warehouse | Added support for publish to Fabric Data Warehouse databases where table alter statements are required. | SqlPackage CLI; DacFx API / Schema compare |
| [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] | Added support for [SQL database in Microsoft Fabric](/fabric/database/sql/overview) in the target platform `SqlDbFabricDatabaseSchemaProvider`. | SqlPackage CLI; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an issue where a deployment fails if there's a stored procedure or function referencing a memory-optimized system-versioned table, and the database is missing a memory-optimized system-versioned table due to being created by `DBCC CLONEDATABASE`. [GitHub issue](https://github.com/microsoft/DacFx/issues/417) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where deployments to Synapse Serverless with role membership changes fails. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where deployments with a master key fail if the password on the master key wasn't set. [Developer Community](https://developercommunity.visualstudio.com/t/Error-when-publishing-SQL-DB-locally:-T/10732457) | SqlPackage CLI; DacFx API / Schema compare |
| Import | Fixed an issue with clustered columnstore indexes with nvarchar(max), varchar(max), and varbinary(max) types fail to import. [GitHub issue](https://github.com/microsoft/DacFx/issues/475) | SqlPackage CLI |
| Import | Fixed an issue where importing a database with DDL triggers fails because the triggers are enabled before the data import is completed. | SqlPackage CLI |

## 162.4.92 SqlPackage

**Release date:** September 18, 2024

```bash
dotnet tool install -g microsoft.sqlpackage --version 162.4.92
```

| Platform | Download |
| --- | --- |
| Windows .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2286801) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2286487) |
| macOS .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2286382) |
| Linux .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2286802) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | The default compatibility level for new databases in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is now set to 160. [Blog post](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-database-compatibility-level-160-in-azure/ba-p/4172039) | Platform; SqlPackage CLI |
| JSON | JSON data type is now supported in the target platform `Azure SQL Database` for import, export, extract, deployment, and SQL project build. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an issue where a partition function with a bit conversion function results in table rebuilds during deployment. [GitHub issue](https://github.com/microsoft/DacFx/issues/458) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where deploying a change to an external table causes all external tables to be dropped and recreated. [GitHub issue](https://github.com/microsoft/DacFx/issues/452) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where temporal tables with spaces in the column names for `system_time` columns produce invalid deployment scripts. [Developer Community](https://developercommunity.visualstudio.com/t/SSDT-schema-compare-not-working-for-temp/10400594) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where changing the column type between types that are compatible for [type cast](../../t-sql/functions/cast-and-convert-transact-sql.md) on a table resulted in an unnecessary table rebuild during deployment. [GitHub issue](https://github.com/microsoft/DacFx/issues/361) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where the deployment script generated for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] includes an ignored statement to turn off Query Store regardless of SQL project settings. | SqlPackage CLI |
| Export | Fixed an issue where a BACPAC export fails during serialization but the trace log doesn't contain the failure message. [GitHub issue](https://github.com/microsoft/DacFx/issues/310) | SqlPackage CLI |
| Extract | Fixed an issue where the extract operation reorders the indexes on a table when writing the table definition out to `.sql` files. | SqlPackage CLI; DacFx API / Schema compare |
| JSON | Fixed an issue where the [isjson](../../t-sql/functions/isjson-transact-sql.md) function's `json_type_constraint` parameter wasn't recognized as a second parameter. [GitHub issue](https://github.com/microsoft/DacFx/issues/375) | SqlPackage CLI; DacFx API / Schema compare |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.1.6) v5.1.6. | Platform |
| Schema compare | Fixed an issue where schema compare expects all statements to be in the same batch, resulting in duplicate statements. [GitHub issue](https://github.com/microsoft/DacFx/issues/474) | DacFx API / Schema compare |
| ScriptDOM | References [ScriptDOM 16.1.9142](https://github.com/microsoft/SqlScriptDOM/blob/main/release-notes/161.91/161.9142.1.md) | Platform |

## 162.3.566 SqlPackage

**Release date:** June 24, 2024

```bash
dotnet tool install -g microsoft.sqlpackage --version 162.3.566
```

| Platform | Download |
| --- | --- |
| Windows .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2277003) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2276908) |
| macOS .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2276909) |
| Linux .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2277004) |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an issue where the deployment contributor [API DeploymentPlanModifier](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanmodifier?view=sql-dacfx-162&preserve-view=true) methods are set as static methods. [GitHub issue](https://github.com/microsoft/DacFx/issues/461) | DacFx API / Schema compare |
| Platform | The SqlPackage `.zip` build .NET SDK is updated from 8.0.301 to 8.0.302 | Platform; SqlPackage CLI |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| Import | A BACPAC file created with SqlPackage export, except when installed by the `.msi` file, might fail to import with the Azure portal and Azure PowerShell when larger than 4 GB. | Import the BACPAC with SqlPackage or create the BACPAC file with SqlPackage installed by the `.msi` file. |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 162.3.563 SqlPackage

**Release date:** June 6, 2024

```bash
dotnet tool install -g microsoft.sqlpackage --version 162.3.563
```

| Platform | Download |
| --- | --- |
| Windows .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2273950) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2274058) |
| macOS .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2274060) |
| Linux .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2274059) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Microsoft Fabric | Added preview support for the target platform `SqlDbFabricDatabaseSchemaProvider`, representing Microsoft Fabric mirrored SQL databases. The [data types supported](../../t-sql/statements/create-external-table-as-select-transact-sql.md#supported-data-types) in this target platform are limited to data types supported for mirroring to Microsoft Fabric. | SqlPackage CLI; DacFx API / Schema compare |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.1.5) v5.1.5. | Platform |
| ScriptDOM | References [ScriptDOM 161.9109](https://github.com/microsoft/SqlScriptDOM/blob/main/release-notes/161.91/161.9109.0.md). | Platform |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Code analysis | Fixed an issue where the build output from code analysis rules wasn't formed consistent with MSBuild diagnostic format guidelines. [GitHub issue](https://github.com/microsoft/DacFx/issues/415) | MSBuild / SQL projects |
| Deployment | Fixed an issue where the deployment of an index with the `ONLINE` property set and a [large object type](../../t-sql/data-types/data-types-transact-sql.md#data-type-categories) (LOB) fails. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where the deployment of column encryption fails on a temporal table. [GitHub issue](https://github.com/microsoft/DacFx/issues/440) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Reverts the changes made to avoid storing absolute paths of referenced DACPACs after project build due to issues with backward compatibility in new behavior. [GitHub issue](https://github.com/microsoft/DacFx/issues/329) | MSBuild / SQL projects; DacFx API / Schema compare |
| Extract | Fixed an issue where columns used in a multi-column distribution (MCD) table were incorrectly scripting as allowing `NULL` values. | SqlPackage CLI; DacFx API / Schema compare |
| ScriptDOM | Fixed an issue where selecting unspecified (`*`) columns from the table-valued function `OPEN_JSON` causes the SQL project fail to build. [GitHub issue](https://github.com/microsoft/DacFx/issues/420) | MSBuild / SQL projects; DacFx API / Schema compare |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| Deployment | The deployment contributor API DeploymentPlanModifier methods are set as static methods. | Fixed in SqlPackage 162.3.566. |
| Import | A BACPAC file created with SqlPackage export, except when installed by the `.msi` file, might fail to import with the Azure portal and Azure PowerShell when larger than 4 GB. | Import the BACPAC with SqlPackage or create the BACPAC file with SqlPackage installed by the `.msi` file. |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 162.2.111 SqlPackage

**Release date:** February 27, 2024

```bash
dotnet tool install -g microsoft.sqlpackage --version 162.2.111
```

| Platform | Download |
| --- | --- |
| Windows .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2261576) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2262108) |
| macOS .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2261849) |
| Linux .NET 8 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2261577) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.1.4) v5.1.4. | Platform |
| Platform | SqlPackage self-contained (.zip) downloads are now built with .NET 8. | Platform; SqlPackage CLI |
| Platform | SqlPackage `dotnet tool` is now available for both .NET 6 and .NET 8. [GitHub issue](https://github.com/microsoft/DacFx/issues/372) | Platform; SqlPackage CLI |
| Platform | SqlPackage [preview releases](./sqlpackage-download.md#preview-releases) are now available in the `dotnet tool` feed. | Platform; SqlPackage CLI |
| Azure Synapse Analytics | Added validation to the `DW_COMPATIBILITY_LEVEL` project property to ensure that the value is within the valid options of 0, 10, 20, 30, 40, 50, 9000 during project build. | MSBuild / SQL projects |
| Deployment | Added support for `ONLINE` index [operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md). Online index operations can be applied as a SqlPackage command line [publish property](./sqlpackage-publish.md#properties-specific-to-the-publish-action), `/p:PerformIndexOperationsOnline`, and as a component in the SQL project model. [GitHub issue](https://github.com/microsoft/DacFx/issues/27) | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| Parquet | Improvements to extract and publish operations with data in Parquet files, including performance improvements with parallel import of data and log file size reduction. | SqlPackage CLI |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an issue where packages with functions used in the `APPLY` clause of a view fail to deploy. The previous error message was that the function wasn't found because the view was incorrectly deployed before the function. [GitHub issue](https://github.com/microsoft/DacFx/issues/106) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where function keywords such as `NATIVE_COMPILATION` and `SCHEMABINDING` weren't correctly recognized and included in the deployment script. [GitHub issue](https://github.com/microsoft/DacFx/issues/308), [Developer Community](https://developercommunity.visualstudio.com/t/Database-project-schema-compare-generate/10224098) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where references to system tables in a VALUES clause fail to build with an error message that the value can't be null. [Developer Community](https://developercommunity.visualstudio.com/t/The-SqlBuildTask-task-failed-unexpecte/10525319) | MSBuild / SQL projects; DacFx API / Schema compare |
| Deployment | Fixed an issue where the absolute paths of referenced DACPACs were stored in the DACPAC after project build instead of the relative paths. [GitHub issue](https://github.com/microsoft/DacFx/issues/329) | MSBuild / SQL projects; DacFx API / Schema compare |
| Deployment | Fixed an issue where the creation of a disabled clustered index causes the deployment to fail if another disabled index was to be created. [GitHub issue](https://github.com/microsoft/DacFx/issues/386) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where a synonym to user-defined data types resulted in an unresolved reference. [GitHub issue](https://github.com/microsoft/DacFx/issues/327) | MSBuild / SQL projects; DacFx API / Schema compare |
| Extract | Fixed an issue where the `DacVersion` property wasn't being set if a version was specified for the package that didn't follow `major.minor.build` format. [GitHub issue](https://github.com/microsoft/DacFx/issues/110) | SqlPackage CLI; DacFx API / Schema compare |
| Import | Fixed an issue where certain collations, including `Chinese_PRC_CI_AS`, fail to import with an error message that the collation wasn't supported. [GitHub issue](https://github.com/microsoft/DacFx/issues/292) | SqlPackage CLI |
| Schema compare | Fixed an issue where databases with `UTF8` collation don't give the correct result. | DacFx API / Schema compare |
| Schema compare | Fixed an issue where schema compare doesn't include external data source, external file format, and external table objects when evaluating Synapse serverless SQL pools. | DacFx API / Schema compare |
| Security | Fixed SqlPackage on .NET support for universal authentication (`/ua`), which supports Microsoft Entra ID authentication with multifactor authentication. (MFA). | SqlPackage CLI |
| System DACPACs | Fixed an issue where the `pdw*` views weren't included in the [Synapse Data Warehouse](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Synapse.Master) `master.dacpac`. [GitHub issue](https://github.com/microsoft/DacFx/issues/268), [Developer Community](https://developercommunity.visualstudio.com/t/masterdacpac-for-Azure-SQL-DW-Synapse-/10459631) | Platform; DacFx API / Schema compare |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| Import | A BACPAC file created with SqlPackage export, except when installed by the `.msi` file, might fail to import with the Azure portal and Azure PowerShell when larger than 4 GB. | Import the BACPAC with SqlPackage or create the BACPAC file with SqlPackage installed by the `.msi` file. |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 162.1.172 SqlPackage

**Release date:** January 9, 2024

```bash
dotnet tool install -g microsoft.sqlpackage --version 162.1.172
```

| Platform | Download |
| --- | --- |
| Windows .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2257374) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2257373) |
| macOS .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2257375) |
| Linux .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2257477) |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.1.3) v5.1.3. | Platform |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| Import | A BACPAC file created with SqlPackage export, except when installed by the `.msi` file, might fail to import with the Azure portal and Azure PowerShell when larger than 4 GB. | Import the BACPAC with SqlPackage or create the BACPAC file with SqlPackage installed by the `.msi` file. |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 162.1.167 SqlPackage

**Release date:** October 19, 2023

| Platform | Download |
| --- | --- |
| Windows .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2249738) |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2249478) |
| macOS .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2249674) |
| Linux .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2249739) |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.1.1) v5.1.1. | Platform |
| Azure Synapse Analytics | Added support for `PARSER_VERSION` in `FORMAT_OPTIONS` for Azure Synapse Analytics serverless SQL pools. [Documentation](../../t-sql/statements/create-external-file-format-transact-sql.md#format_options) | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Added support for multi-column distribution (MCD) in `CREATE VIEW` for Azure Synapse Analytics dedicated SQL pools. [GitHub issue](https://github.com/microsoft/DacFx/issues/224) | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Added support for /p:TableData property on extract operations to Parquet files, enabling the ability to specify which tables to export data for. [GitHub issue](https://github.com/microsoft/DacFx/issues/16) | SqlPackage CLI |
| Fabric Data Warehouse | Added support for extract and publish for Fabric Data Warehouse databases. Publish capabilities don't support changes that require existing tables to be altered. The target platform enum value is `SqlDwUnifiedDatabaseSchemaProvider` in SQL database projects. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| Parquet | Added preview support for extract and publish with data stored in Parquet files in Azure Blob Storage with [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions aren't supported. Data must be in supported data types for [CETAS](/azure/synapse-analytics/sql/develop-tables-cetas#supported-data-types). Extract and publish with Parquet files offers performance improvements over import/export to BACPAC files in many scenarios. | SqlPackage CLI |
| Publish | Added `/p:AllowTableRecreation` property to publish operation. The default (true) is consistent with previous behavior, where a table change might require that a table is recreated while the table data is preserved however the deployment might take a significant amount of time or change tracking data could be lost. Setting the property `/p:AllowTableRecreation` to false results in the deployment not starting if recreation is needed for any table. [GitHub issue](https://github.com/microsoft/DacFx/issues/28) | SqlPackage CLI; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Application | Fixed an issue where the SqlPackage CLI throws an exception when the output was redirected. [GitHub issue](https://github.com/microsoft/DacFx/issues/261) | SqlPackage CLI |
| Azure Synapse Analytics | Fixed an issue where a publish operation fails to parse a statement containing the `filepath()` or `filename()` [functions](/azure/synapse-analytics/sql/query-specific-files). | SqlPackage CLI; DacFx API / Schema compare |
| Import | `AUTO_DROP` option is excluded from statistics when importing a BACPAC to a version of SQL Server that doesn't support `AUTO_DROP`. | SqlPackage CLI |
| Import | Fixed an issue where imports of databases containing `ALTER` or `CREATE` of availability groups fails to import. | SqlPackage CLI |
| Export | Fixed an issue where dropped ledger columns were included in a BACPAC export, resulting in an error message during import. | SqlPackage CLI |
| Export | Fixed an issue where /p:CompressionOption wasn't honored when exporting to a BACPAC file. | SqlPackage CLI |
| Extract | Fixed an issue where /p:ExtractTarget options for non-DACPAC options still required the target file to have a `.dacpac` extension. [GitHub issue](https://github.com/microsoft/DacFx/issues/128) | SqlPackage CLI |
| Ledger | Fixed an issue where import or publish of a database containing a dropped ledger table fails due to attempting to create permissions for the dropped table. | SqlPackage CLI; DacFx API / Schema compare |
| Ledger | Fixed an issue where import of a database containing a dropped ledger table fails due to attempting to import data to the dropped table. | SqlPackage CLI |
| Polybase | Fixed an issue where [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] databases with `RDBMS` external tables couldn't be exported or extracted. [GitHub issue](https://github.com/microsoft/DacFx/issues/199) | SqlPackage CLI |
| Publish | Fixed `DropObjectsNotInSource` to not drop objects that are permissions or role memberships. Use `DropPermissionsNotInSource` or `DropRoleMembersNotInSource` to enable dropping permissions or role memberships. [GitHub issue](https://github.com/microsoft/DacFx/issues/339) | SqlPackage CLI; DacFx API / Schema compare |
| Publish | Fixed an issue where the publish operation fails when the user connecting doesn't have access to **`master`** in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. | SqlPackage CLI |
| Publish | Fixed an issue where deployments that include interactions with column encryption experience intermittent execution timeout errors. | SqlPackage CLI; DacFx API / Schema compare |
| Publish | Fixed an issue where deploying a DACPAC built with .NET/.NET Core fails if `RegisterDataTierApplication` was set to true. [GitHub issue](https://github.com/microsoft/DacFx/issues/18) | SqlPackage CLI |
| Publish | Fixed an issue where system versioned table is modified and a new schema is created results in the deployment failing. [GitHub issue](https://github.com/microsoft/DacFx/issues/309) | SqlPackage CLI; DacFx API / Schema compare |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| Import | A BACPAC file created with SqlPackage export, except when installed by the `.msi` file, might fail to import with the Azure portal and Azure PowerShell when larger than 4 GB. | Import the BACPAC with SqlPackage or create the BACPAC file with SqlPackage installed by the `.msi` file. |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 162.0.52 SqlPackage

**Release date:** May 11, 2023

| Platform | Download | Version |
| --- | --- | --- |
| Windows .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2236505) | 162.0.52 |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2236347) | 162.0.52 |
| macOS .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2236426) | 162.0.52 |
| Linux .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2236425) | 162.0.52 |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Platform | SqlPackage now collects usage data, including anonymous feature usage and diagnostic data. For more information, see [Usage data collection](sqlpackage.md#usage-data-collection). | SqlPackage CLI |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.1.0) v5.1.0 | Platform |
| Azure Synapse Analytics | Added support for [DW_COMPATIBILITY_LEVEL](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#dw_compatibility_level---auto--10--20--30--40--50--9000-). | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| ScriptDOM | ScriptDOM is now available as a standalone package on [NuGet](https://www.nuget.org/packages/Microsoft.SqlServer.TransactSql.ScriptDom) and is open source on [GitHub](https://github.com/microsoft/SqlScriptDOM). | Platform |
| System DACPACs | The **`master`** and **`msdb`** system DACPACs are now available on NuGet as [Microsoft.SqlServer.Dacpacs.Master](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Master) and [Microsoft.SqlServer.Dacpacs.Msdb](https://www.nuget.org/packages/Microsoft.SqlServer.Dacpacs.Msdb). More information on the system DACPACs and consuming DACPACs as a package reference is available in the [DacFx GitHub repository](https://github.com/microsoft/DacFx). | Platform; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Azure Synapse Analytics | Fixed an issue where the default command timeout wasn't set properly when connecting to Azure Synapse Analytics serverless SQL pools. | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Fixed an issue where Azure Synapse Analytics serverless SQL pools incorrectly determine the default data and log paths. | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Fixed an issue where Azure Synapse Analytics serverless SQL pools incorrectly determine the default login, user, and schema. | SqlPackage CLI; DacFx API / Schema compare |
| [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] | Fixed an issue where temporal history retention wasn't correctly recognized as not configured (null). | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where the deployment plan fails to detect a dependency on a table/view in subqueries within `FROM VALUES` clause. [GitHub issue](https://github.com/microsoft/DacFx/issues/156) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where deployment fails when the target database contains a rule bound to a column. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where deployment fails when the target database contains a rule bound to a column with a user-defined type. [GitHub issue](https://github.com/microsoft/DacFx/issues/245) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where the retention period on a temporal table is reset to the default value when another change is made to the table. [GitHub issue](https://github.com/microsoft/DacFx/issues/258) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where updates to a primary key aren't included in the deployment when the table has compression options specified. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where a nonclustered index on a partitioned table is rebuilt even when no changes are made to the table. [GitHub issue](https://github.com/microsoft/DacFx/issues/202) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where `IgnoreColumnOrder` property isn't honored by a history table when no changes are made to a system-versioned table except the columns are reordered. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where external tables are dropped and redeployed despite no changes when the table definition contained SQLCMD variables. [GitHub issue](https://github.com/microsoft/DacFx/issues/249) | SqlPackage CLI; DacFx API / Schema compare |
| Export | Fixed an issue where the diagnostic information provided during an export operation incorrectly reports the size of a table in KB instead of Bytes. [GitHub issue](https://github.com/microsoft/DacFx/issues/209) | SqlPackage CLI |
| Import | Fixed an issue where a Microsoft Entra ID user can't be created during import to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], causing the import to fail. [GitHub issue](https://github.com/microsoft/DacFx/issues/260) | SqlPackage CLI |
| Ledger | Fixed an issue where SqlPackage wasn't correctly identifying the error when the Ledger history table or view have an invalid two-part name. | SqlPackage CLI; DacFx API / Schema compare |
| Permissions | Fixed an issue where permissions assigned to a user in the database model aren't recognized, causing the project build or SqlPackage operation to fail. | MSBuild / SQL projects; SqlPackage CLI; DacFx API / Schema compare |
| Query Store | Fixed an issue where the `flush_interval_seconds` [Query Store option](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md) wasn't correctly validated with a minimum value of 60 seconds. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Fixed an issue where the function `DATETRUNC` wasn't recognized as a built-in function. [Developer Community](https://developercommunity.visualstudio.com/t/Visual-Studio-build-solution-not-recogni/10333180) | MSBuild / SQL projects; DacFx API / Schema compare |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Fixed an issue where the function `DATE_BUCKET` wasn't recognized as a built-in function. | MSBuild / SQL projects; DacFx API / Schema compare |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| Import | A BACPAC file created with SqlPackage export, except when installed by the `.msi` file, might fail to import with the Azure portal and Azure PowerShell when larger than 4 GB. | Import the BACPAC with SqlPackage or create the BACPAC file with SqlPackage installed by the `.msi` file. |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 161.8089.0 SqlPackage

**Release date:** February 13, 2023

| Platform | Download | Version | Build |
| --- | --- | --- | --- |
| Windows .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2224909) | 161.8089.0 | 16.1.8089.0 |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2224908) | 161.8089.0 | 16.1.8089.0 |
| macOS .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2225106) | 161.8089.0 | 16.1.8089.0 |
| Linux .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2225105) | 161.8089.0 | 16.1.8089.0 |

> [!IMPORTANT]  
> Version 161 of SqlPackage encrypts database connections by default. Previously successful connections with self-signed certificates or without encryption might not connect with v161 without updating the SqlPackage parameters. For more information, see <https://aka.ms/dacfx-connection>.

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Added the ability for the `GenerateSmartDefaults` property on publish to populate values from a default constraint when set to `true`. [GitHub issue](https://github.com/microsoft/DacFx/issues/38) | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Added support for [serverless SQL pools](./sqlpackage-for-azure-synapse-analytics.md#support-for-serverless-sql-pools) in Extract and Publish operations. | SqlPackage CLI; DacFx API / Schema compare |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Added support for [ordered clustered columnstore indexes](../../relational-databases/indexes/columnstore-indexes-design-guidance.md#use-an-ordered-columnstore-index-for-large-data-warehouse-tables). | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an issue where publishing to SQL on Linux fails due to the default data and log paths being empty. [GitHub issue](https://github.com/microsoft/DacFx/issues/136) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where publishing an external table with file format changes results in an error. [GitHub issue](https://github.com/microsoft/DacFx/issues/120) | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where publishing with a column encrypted with randomized encryption doesn't fail immediately if the column encryption key (CEK) was inaccessible, delaying the deployment error until the column encryption step. | SqlPackage CLI |
| Refactor | Fixed an issue where a renamed column on a system versioned table results in the system versioning being turned off and not turned back on. [GitHub issue](https://github.com/microsoft/DacFx/issues/203) | SqlPackage CLI; DacFx API / Schema compare |
| Platform | Fixed an issue where SqlPackage operations fail on RHEL 9 due to an encryption error. Moves to use the 6.0.10 version of the .NET 6 runtime. [GitHub issue](https://github.com/microsoft/DacFx/issues/168) | Platform; SqlPackage CLI |
| Schema compare | Fixed an issue where the `DoNotEvaluateSqlCmdVariables` property for *Publish* and *Script* results in the SqlCmd variables also not being evaluated on both the source and target instead of only the source. | DacFx API / Schema compare; SqlPackage CLI |
| ScriptDOM | Fixed external table support for `REJECT_SAMPLE_VALUE`. | Platform |
| ScriptDOM | Fixed an issue where compression options couldn't be applied to a table with a clustered index. | Platform |
| SQL projects | Fixed an issue where valid options for the `QueryStoreFlushInterval` are incorrectly reported as invalid. [Developer Community](https://developercommunity.visualstudio.com/t/SQL72003:-The-value-300-for-property-Que/10210937) | MSBuild / SQL projects |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 161.6374.0 SqlPackage

**Release date:** November 9, 2022

| Platform | Download | Version | Build |
| --- | --- | --- | --- |
| Windows .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2215400) | 161.6374.0 | 16.1.6374.0 |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2215326) | 161.6374.0 | 16.1.6374.0 |
| macOS .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2215401) | 161.6374.0 | 16.1.6374.0 |
| Linux .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2215501) | 161.6374.0 | 16.1.6374.0 |

> [!IMPORTANT]  
> Version 161 of SqlPackage encrypts database connections by default. Previously successful connections with self-signed certificates or without encryption might not connect with v161 without updating the SqlPackage parameters. For more information, see <https://aka.ms/dacfx-connection>.

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Platform | Changes connections to use encryption and not trust the server certificate by default. This is a breaking change for connections using self-signed certificates or without encryption by default. For more information, see <https://aka.ms/dacfx-connection>. | Platform; SqlPackage CLI |
| Platform | References [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/5.0.1) v5.0.1 | Platform |
| Platform | SqlPackage is now available for [installation](sqlpackage-download.md) as a `dotnet tool` for Windows, macOS, and Linux platforms. | Platform; SqlPackage CLI |
| Always Encrypted | Added support for VBS (Virtualization-based security) with [secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md). | SqlPackage CLI; DacFx API / Schema compare |
| Connectivity | Added support for TDS 8.0 and parameters for `/SourceHostNameInCertificate` and `/TargetHostNameInCertificate` to SqlPackage operations. | SqlPackage CLI |
| Replication | Added support for [sp_addpublication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md#automatically-handle-conflicts-with-last-write-wins) with peer-to-peer replication. | SqlPackage CLI; DacFx API / Schema compare |
| ScriptDOM | Added support for `IS NOT DISTINCT FROM` syntax with predicate subqueries. | Platform |
| Server-level roles | Added support for additional [fixed server roles](../../relational-databases/security/authentication-access/server-level-roles.md#fixed-server-level-roles-introduced-in-sql-server-2022): `##MS_DatabaseConnector##`, `##MS_LoginManager##`, `##MS_DatabaseManager##`, `##MS_ServerStateManager##`, `##MS_ServerStateReader##`, `##MS_ServerPerformanceStateReader##`, `##MS_ServerSecurityStateReader##`, `##MS_DefinitionReader##`, `##MS_PerformanceDefinitionReader##`, `##MS_SecurityDefinitionReader##`. | SqlPackage CLI; DacFx API / Schema compare |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Added support for [T-SQL function changes associated with SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md#language): `GREATEST()`, `LEAST()`, `STRING_SPLIT()`, `DATETRUNC()`, `LTRIM()`, `RTRIM()`, and `TRIM()`. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Added support for [JSON function changes associated with SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md#language): `ISJSON()`, `JSON_PATH_EXISTS()`, `JSON_OBJECT()`, and `JSON_ARRAY()`. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Added support for [bit manipulation functions associated with SQL Server 2022](../../t-sql/functions/bit-manipulation-functions-overview.md): `LEFT_SHIFT()`, `RIGHT_SHIFT()`, `BIT_COUNT()`, `GET_BIT()`, and `SET_BIT()`. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Added support for [time series function changes associated with SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md#language): `DATE_BUCKET()`, `GENERATE_SERIES()`, `FIRST_VALUE()`, and `LAST_VALUE()`. | SqlPackage CLI; MSBuild / SQL projects; DacFx API / Schema compare |
| Statistics | Added support for [STATISTICS `AUTO_DROP` option](../../t-sql/statements/create-statistics-transact-sql.md). | SqlPackage CLI; DacFx API / Schema compare |
| XML compression | Added support for XML compression on [XML indexes](../../relational-databases/xml/xml-indexes-sql-server.md#xml-compression). | SqlPackage CLI; DacFx API / Schema compare |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 19.2 SqlPackage

**Release date:** September 22, 2022

| Platform | Download | Version | Build |
| --- | --- | --- | --- |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2209512) | 19.2 | 16.0.6296.0 |
| macOS .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2209610) | 19.2 | 16.0.6296.0 |
| Linux .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2209513) | 19.2 | 16.0.6296.0 |
| Windows .NET 6 | [.zip file](https://go.microsoft.com/fwlink/?linkid=2209609) | 19.2 | 16.0.6296.0 |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Connection pooling | Enables connection pooling for all connections if the environment variable `CONNECTION_POOLING_ENABLED` is set to True. This is recommended for operations with Microsoft Entra ID username/password connections to avoid Microsoft Authentication Library (MSAL) throttling. | SqlPackage CLI |
| Deployment options | Surfaces friendly names for deployment options in DacFx .NET APIs. | DacFx API / Schema compare |
| Dynamic Data Masking | Added support for [granular UNMASK permissions](../../relational-databases/security/dynamic-data-masking.md#granular) in Import/Export and Extract/Publish. | SqlPackage CLI; DacFx API / Schema compare |
| Ledger | Added SQL Ledger history table in schema model for validation and export/extract, doesn't import or publish the history table to a database. | SqlPackage CLI; DacFx API / Schema compare; MSBuild / SQL projects |
| Platform | SqlPackage is now built with .NET 6 | Platform; SqlPackage CLI |
| [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Added support for permissions `ALTER LEDGER CONFIGURATION`, `VIEW PERFORMANCE DEFINITION`, `VIEW ANY PERFORMANCE DEFINITION`. Learn more about the permission definitions available in the [permissions documentation](../../relational-databases/security/permissions-database-engine.md). | SqlPackage CLI; DacFx API / Schema compare |
| XML compression | [XML compression](../../t-sql/statements/create-table-transact-sql.md#xml_compression) support in ScriptDOM, Import/Export, and Extract/Publish. More information on XML data and XML compression is available in the [XML data documentation](../../relational-databases/xml/xml-data-sql-server.md). | SqlPackage CLI; DacFx API / Schema compare; Platform |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Export | Fixed an issue where export fails when a table had stats with computed columns | SqlPackage CLI |
| Import | Fixed an issue where the import gets stuck at 95% | SqlPackage CLI |
| ScriptDOM | Fixed an issue where `STRING_SPLIT` doesn't support a `NULL` ordinal value | Platform |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |
| XML compression | XML compression of an XML index isn't yet supported in SqlPackage. | N/A |

## 19.1 SqlPackage

**Release date:** May 24, 2022

| Platform | Download | Version | Build |
| --- | --- | --- | --- |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2196438) | 19.1 | 16.0.6161.0 |
| macOS .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2196439) | 19.1 | 16.0.6161.0 |
| Linux .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2196335) | 19.1 | 16.0.6161.0 |
| Windows .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2196334) | 19.1 | 16.0.6161.0 |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Azure Synapse Analytics | Added support for [Native external data source](/azure/synapse-analytics/sql/develop-tables-external-tables?tabs=native#syntax-for-create-external-data-source). | SqlPackage CLI; DacFx API / Schema compare |
| Extract | Added support for `ExtractTarget` property on extract operations. Extract now supports extracting to `.sql` as a file per object organized in a single folder, object type, schema, or object type and schema. | SqlPackage CLI |
| ScriptDOM | Added support for `IS NOT DISTINCT FROM` syntax. | Platform |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Azure Synapse Analytics | Fixed a publish operation for table name change where table name includes '/' character. | SqlPackage CLI; DacFx API / Schema compare |
| Export | Fixed export of a SQL ledger history table with dependencies. | SqlPackage CLI |
| Extract | Fixed an extract operation failure where an offset clause using a function is used in a stored procedure. | SqlPackage CLI; DacFx API / Schema compare |
| Extract | Fixed warnings on extract operation for ledger tables. | SqlPackage CLI |
| General | Fixed an issue where command timeout setting wasn't properly applied. | SqlPackage CLI; DacFx API / Schema compare |
| Import | Fixed an issue where full text index gets disabled on import. | SqlPackage CLI |
| Publish | Fixed an issue where publish operation drops and recreates a clustered columnstore index when a column is added. | SqlPackage CLI; DacFx API / Schema compare |
| Publish | Fixed an issue where graph tables fail to deploy when a partition function includes leading zeros. | SqlPackage CLI; DacFx API / Schema compare |
| ScriptDOM | Fixed an issue where `IIF` condition is enclosed in parenthesis fails to parse. | Platform |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| Deployment | Increased deployment time when deploying using Microsoft Entra ID user/password authentication due to Microsoft Authentication Library (MSAL) throttling. [More Information on GitHub](https://github.com/microsoft/DacFx/issues/92) | Use an alternative authentication method, such as [Microsoft Entra service principals with Azure SQL](/azure/azure-sql/database/authentication-aad-service-principal) |
| Deployment | SqlPackage on .NET Core for Windows, macOS, and Linux fails during a publish operation with an error message "Unrecognized configuration section system.diagnostics" when in-place encryption is used for Always Encrypted with secure enclaves. | Remove the file `sqlpackage.dll.config` from the SqlPackage folder. |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 19.0 SqlPackage

**Release date:** January 25, 2022

| Platform | Download | Version | Build |
| --- | --- | --- | --- |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2185764) | 19.0 | 16.0.5400.1 |
| macOS .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2185765) | 19.0 | 16.0.5400.1 |
| Linux .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2185670) | 19.0 | 16.0.5400.1 |
| Windows .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2185669) | 19.0 | 16.0.5400.1 |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Always Encrypted | Added support for in-place encryption for Always Encrypted columns. Publish can now use a server-side secure enclave to encrypt, decrypt, and re-encrypt database columns in-place. This avoids the expense of moving the data outside of the database. See prerequisites for in-place encryption in [Configure column encryption in-place using Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves-configure-encryption.md). Note: In-place encryption is supported only with the offline approach. | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Added support for column-level symmetric encryption. | SqlPackage CLI; DacFx API / Schema compare |
| Ledger | Added support for exporting and importing databases with ledger tables. The following limitations apply to Export: Ledger history tables and dropped ledger tables aren't migrated; the values of `GENERATED ALWAYS` columns and the data in ledger system views isn't migrated; the value of the database-level Ledger property is ignored. | SqlPackage CLI |
| Platform | Added support for .NET 6 as the target framework | Platform |
| Platform | References Microsoft.Data.SqlClient (3.0) instead of System.Data.SqlClient in .NET Framework version. Upgrade Microsoft.Data.SqlClient from 2.1.3 to 3.0 for .NET Core version. | Platform |
| Platform | Upgrades .NET Framework target version to .NET 4.6.2 | Platform |
| ScriptDOM | Added support for Sql160 parser. | Platform |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an issue with interpretation of table distribution on column within a stored procedure. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue with "Drop objects not in source" option during publish operation. | SqlPackage CLI |
| Deployment | Fixed an issue deploying a DACPAC with temporal table having sensitivity classification. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed a bug when variables are verified even when `DoNotEvaluateSqlCmdVariables` is set to true | SqlPackage CLI; DacFx API / Schema compare |
| Extract | Fixed an issue with Refactor log of referenced DACPAC according to includeCompositeObjects selection. | SqlPackage CLI; DacFx API / Schema compare |
| Import | Fixed an issue with importing database scope configurations that aren't supported in target server | SqlPackage CLI |
| SQL Project | Fixed an issue where incremental statistics caused an issue with the project build when applied to a primary key. | MSBuild / SQL projects |
| SQL Project | Fixed building a project with file tables. | MSBuild / SQL projects |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported. | N/A |
| Deployment | Increased deployment time when deploying using Microsoft Entra ID user/password authentication due to Microsoft Authentication Library (MSAL) throttling. [More Information on GitHub](https://github.com/microsoft/DacFx/issues/92) | Use an alternative authentication method, such as [Microsoft Entra service principals with Azure SQL](/azure/azure-sql/database/authentication-aad-service-principal) |
| ScriptDOM | Parsing a large file can result in a stack overflow. | None |

## 18.8 SqlPackage

**Release date:** October 4, 2021

| Platform | Download | Version | Build |
| --- | --- | --- | --- |
| Windows | [.msi file](https://go.microsoft.com/fwlink/?linkid=2164920) | 18.8 | 15.0.5282.3 |
| macOS .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2165009) | 18.8 | 15.0.5282.3 |
| Linux .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2165008) | 18.8 | 15.0.5282.3 |
| Windows .NET Core | [.zip file](https://go.microsoft.com/fwlink/?linkid=2165007) | 18.8 | 15.0.5282.3 |

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Export | Added option `VerifyExtraction` to change behavior of schema model validation on export | SqlPackage CLI |
| Azure SQL | Support for ledger database and tables, including import and export actions. | SqlPackage CLI |
| Platform | Upgrade Microsoft.Data.SqlClient from 2.0.0 to 2.1.3 for .NET Core version | Platform |
| Azure Synapse Analytics | Support for column encryption with symmetric key | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Support for column encryption with `CREATE CERTIFICATE` | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Support for `MERGE` statement | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Variable parameterization for AE columns, new publish property `IsAlwaysEncryptedParameterizationEnabled` | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Support for `IgnoreWorkloadClassifiers` and `IgnoreDatabaseWorkloadGroups` publish properties | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Support for external language runtimes | SqlPackage CLI; DacFx API / Schema compare |
| ScriptDOM | Support for ledger database and tables | Platform |
| ScriptDOM | Support for `INCLUDE` columns in inline index definitions | Platform |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an issue where external user deployment to [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] fails | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed deployment order involving temporal tables to drop dependencies before turning off system versioning | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed Always Encrypted deployment bug with error "Invalid object name '#tmpErrors'" | SqlPackage CLI |
| Export | Fixed validation for SqlPackage parameters `ExcludeObjectType` / `ExcludeObjectTypes`, and `DoNotDropObjectType` / `DoNotDropObjectTypes` | SqlPackage CLI |
| Export | Fixed export failure when there are change data capture (CDC) objects in database by excluding | SqlPackage CLI |
| Extract | Added a retry to extract validation when first time fails due to race condition | SqlPackage CLI |
| Import | Fixed occasional deadlocks when importing to Azure by setting `MAXDOP` to 1 | SqlPackage CLI |
| Import | Fixed import failure when temporal table has dependency on security policy with schema binding on | SqlPackage CLI |
| Platform | `DacFramework.msi` is now signed by "Microsoft SQL Server Data-Tier Application Framework" instead of "SQL Server 2012" | Platform |
| Platform | Default to large arrays in x64 SqlPackage, fixed some scenarios involving large databases | Platform; SqlPackage CLI |
| Schema Compare | Fixed schema compare failing for equal databases with database scoped configurations | DacFx API / Schema compare |
| Schema Compare | Fixed schema compare with columnstore indexes | DacFx API / Schema compare |
| SQL Project | Fixed a bug with build error for "`GRANT EXECUTE ANY EXTERNAL SCRIPT`" | MSBuild / SQL projects |
| SQL Project | Fixed a bug where database project with columnstore index and a (n)varchar(max) column builds successfully but fails at deployment | MSBuild / SQL projects; SqlPackage CLI |
| SQL Project | Fixed unresolved reference warnings for table distribution columns within Stored Procedures | MSBuild / SQL projects |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported | N/A |
| Deployment | The Azure SQL ledger table feature isn't yet supported | N/A |

## 18.7.1 SqlPackage

**Release date:** June 2, 2021

**Build:** 15.0.5164.1

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Auditing | Added support for [Get started with Azure SQL Managed Instance auditing](/azure/azure-sql/managed-instance/auditing-configure). | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Added support for [PREDICT](../../t-sql/queries/predict-transact-sql.md). | SqlPackage CLI; DacFx API / Schema compare |
| Logging | Added SqlPackage version and architecture information to diagnostic log file. | SqlPackage CLI |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Export | Fixed an issue where exporting a table with text or image in the first column fails without a clustered index. | SqlPackage CLI |
| Export | Fixed an issue where exporting a table without a clustered index that has the order of columns in a statistic in a different order than the table create script fails. | SqlPackage CLI |

## 18.7 SqlPackage

**Release date:** March 10, 2021

**Build:** 15.0.5084.2

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Extract/Publish Big Data to/from Azure Storage. For more info, see [SqlPackage for Big Data](SqlPackage-for-azure-synapse-analytics.md) | SqlPackage CLI |
| Azure Synapse Analytics | Row level security support (inline table-valued function, security policy, security predicate) | SqlPackage CLI; DacFx API / Schema compare |
| Azure Synapse Analytics | Workload classification support | SqlPackage CLI; DacFx API / Schema compare |
| Azure SQL Edge | External streaming job support | SqlPackage CLI; DacFx API / Schema compare |
| Azure SQL Edge | Added table and database options for data retention. | SqlPackage CLI; DacFx API / Schema compare |
| Import | Added two new index option properties for import operation. `DisableIndexesForDataPhase` (Disable indexes before importing data into SQL Server, default true) and `RebuildIndexesOfflineForDataPhase` (Rebuild indexes offline after importing data into SQL Server, default false) | SqlPackage CLI |
| Logging | Added property for all operations (`HashObjectNamesInLogs`) that turns all object names into a hash string in log messages. | SqlPackage CLI |
| Performance | Improvements to import and export performance, including additional logging to help determining additional bottlenecks. | SqlPackage CLI |
| SQLCMD | Added property for Deployment and Schema Compare (`DoNotEvaluateSqlCmdVariables`) that specifies whether SQLCMD variables are replaced with values. | SqlPackage CLI; DacFx API / Schema compare; MSBuild / SQL projects |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Changed default `MAXDOP` from 0 to 8 for [Azure SQL](https://techcommunity.microsoft.com/t5/azure-sql/changing-default-maxdop-in-azure-sql-database/ba-p/1538528), updating schema model default in DacFx | SqlPackage CLI; DacFx API / Schema compare |
| Schema Compare | Fixed stored procedures using `OUT` and `OUTPUT` keywords to be ignored as a difference | DacFx API / Schema compare |
| Deployment | Fixed additional validation for Big Data tokens | SqlPackage CLI |
| Build/Deployment | Fixed schema model cleanup of temp external tables for final DACPAC consistency. | SqlPackage CLI; DacFx API / Schema compare |
| Build/Deployment | Fixed error handling and non-Edge 150 RE. | SqlPackage CLI; DacFx API / Schema compare |
| Import/Deployment | Fixed sequence value restored during deployment | SqlPackage CLI |
| Deployment | Fixed an issue where changing the compression option on clustered index caused the table to be recreated instead of alter index. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where a clustered columnstore index was dropped and recreated if table column changed. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed external users getting dropped and recreated during deployment. | SqlPackage CLI |
| Schema Compare | Fixed schema compare issue with external streaming job. | DacFx API / Schema compare |
| Import | Fixed a null reference exception raised when enabling ambient setting `ReliableDdlEnabled` scripting a deployment report. | SqlPackage CLI |
| Deployment | Fixed an issue where deployment steps containing system versioning is created in the incorrect order. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where schema compare update or DACPAC deploy failed due to target containing temporal tables. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed identity value reseeding after deployment based on target's previous last value. | SqlPackage CLI |

### Known issues

| Feature | Details | Workaround |
| --- | --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported | N/A |
| Deployment | In an incremental deploy scenario, when the user is dropping a temporal table along with dropping objects that are dependent on it, like functions, stored procedures etc. the deployment can fail. The script generation order tries to turn off `SYSTEM_VERSIONING` on the table that is a prerequisite for dropping the table, but the order of generated steps is incorrect. [GitHub issue](https://github.com/microsoft/azuredatastudio/issues/14655) | Generate the deployment script, move the System_Versioning `OFF` step to just before the table being dropped and then run the script. |

## 18.6 SqlPackage

**Release date:** September 18, 2020

**Build:** 15.0.4897.1

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Platform | Updated SqlPackage for .NET Core version to .NET Core 3.1 | Platform; SqlPackage CLI |
| Always Encrypted | Added support for secure enclave import and export for [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Added support to ignore change data capture enabled tables when exporting from [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] | SqlPackage CLI |
| Deployment | Added support for index option `OPTIMIZE_FOR_SEQUENTIAL_KEY` in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Added support for identity columns for Azure Synapse Analytics | SqlPackage CLI; DacFx API / Schema compare |
| Help | Output the SqlPackage version in the help (/?) and support the /version parameter | SqlPackage CLI |

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed an incorrect deployment script generated when targeting [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] as a non-**sysadmin** user | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed loading deployment contributors when running script actions | SqlPackage CLI; DacFx API / Schema compare |
| Help | Outputs correct elapsed time in SqlPackage when operations take longer than a day | SqlPackage CLI |
| Deployment | Fixed DACPAC registration when deploying for .NET Core | SqlPackage CLI |
| Deployment | Fixed SqlPackage on .NET Core handling of the `/accessToken` (`/at`) parameter | SqlPackage CLI |
| Deployment | Allow `ALTER TABLE` statements in stored procedures as non-top-level statements | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed Azure Synapse Analytics validation of materialized views to be case insensitive | SqlPackage CLI; DacFx API / Schema compare |

### Known issues

| Feature | Details |
| --- | --- |
| Deployment | The Azure Synapse Analytics Workload Management feature (Workload Groups and Workload Classifiers) isn't yet supported |

## 18.5.1 SqlPackage

**Release date:** June 24, 2020

**Build:** 15.0.4826.1

### Fixes

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed a regression that was introduced in 18.5 causing there to be an "Incorrect syntax near 'type'" error when deploying a DACPAC or importing a BACPAC with a user with external login to on premises | SqlPackage CLI; DacFx API / Schema compare |

## 18.5 SqlPackage

**Release date:** April 28, 2020

**Build:** 15.0.4769.1

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Data Sensitivity classification now supported for SQL Server 2008 and up, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and Azure Synapse Analytics | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Add Azure Synapse Analytics support for table constraints | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Add Azure Synapse Analytics support for ordered clustered columnstore index | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Add support for External Data Source (Oracle, Teradata, MongoDB/CosmosDB, ODBC, Big Data Cluster) and External Table for [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] Big Data Cluster | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Add SQL Database Edge Instance as supported edition | SqlPackage CLI |
| Deployment | Support [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] server names of the form '\<server>.\<dnszone>.database.windows.net' | SqlPackage CLI |
| Deployment | Add support for copy command in Azure Synapse Analytics | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Add deployment option `IgnoreTablePartitionOptions` during Publish to avoid table recreation when there's change in partition function on table for Azure Synapse Analytics | SqlPackage CLI; DacFx API / Schema compare |
| .NET Core | Add support for Microsoft.Data.SqlClient in .NET Core version of SqlPackage | Platform; SqlPackage CLI |

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed parsing json path as expression | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed generating `GRANT` statements for `AlterAnyDatabaseScopedConfiguration` and `AlterAnySensitivityClassification` permissions | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed External Script permission not being recognized | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed inline property - the implicit addition of the property shouldn't show in difference but explicit mention should show through script | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where changing a Table referenced by a Materialized View (MV) causes Alter View statements to be generated. Alter View statements aren't supported for MVs for Azure Synapse Analytics. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed publish failing when adding column to a table with data for Azure Synapse Analytics | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed update script should move data to a new table when changing the distribution column type (data loss scenario) for Azure Synapse Analytics | SqlPackage CLI; DacFx API / Schema compare |
| ScriptDom | Fixed ScriptDom bug where it couldn't recognize inline constraints defined after an inline index | Platform |
| ScriptDom | Fixed ScriptDom `SYSTEM_TIME` missing closing parenthesis when in a batch statement | Platform |
| Always Encrypted | Fixed #tmpErrors table failing to drop if SqlPackage reconnects and the temp table is already gone because the temporary table goes away when the connection dies | SqlPackage CLI; DacFx API / Schema compare |

### Known issues

| Feature | Details |
| --- | --- |
| Deployment | A regression was introduced in 18.5 causing there to be an "Incorrect syntax near 'type'" error when deploying a DACPAC or importing a BACPAC with a user with external login to on premises. Workaround is to use SqlPackage 18.4 and it will be fixed in the next SqlPackage release. |
| .NET Core | Importing BACPACs with sensitivity classification fails with "Internal connection fatal error" because of this [known issue](https://github.com/dotnet/SqlClient/issues/559) in Microsoft.Data.SqlClient. This will be fixed in the next SqlPackage release. |

## 18.4.1 SqlPackage

**Release date:** December 13, 2019

**Build:** 15.0.4630.1

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| ScriptDom | A ScriptDom parsing regression was introduced in 18.3.1 where 'RENAME' is incorrectly treated as a top-level token, cause parsing to fail. | Platform |

### Known issues

| Feature | Details |
| --- | --- |
| Deployment | A regression was introduced in 18.4.1 causing there to be an "Object reference not set to an instance of an object." error when deploying a DACPAC or importing a BACPAC with a user with external login. Workaround is to use SqlPackage 18.4 and it will be fixed in the next SqlPackage release. |

## 18.4 SqlPackage

**Release date:** October 29, 2019

**Build:** 15.0.4573.2

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Add support to deploy to Azure Synapse Analytics. | SqlPackage CLI; DacFx API / Schema compare |
| Platform | SqlPackage .NET Core generally available for macOS, Linux, and Windows. | Platform; SqlPackage CLI |
| Security | Remove SHA1 code signing. | Platform |
| Deployment | Add support for new Azure database editions: GeneralPurpose, BusinessCritical, Hyperscale | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Add [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] support for Microsoft Entra ID user and groups. | SqlPackage CLI |
| Deployment | Support the /AccessToken parameter for SqlPackage on .NET Core. | SqlPackage CLI |

### Known issues

| Feature | Details |
| --- | --- |
| ScriptDom | A ScriptDom parsing regression was introduced in 18.3.1 where 'RENAME' is incorrectly treated as a top-level token, cause parsing to fail. This will be fixed in the next SqlPackage release. |

### Known issues for .NET Core

| Feature | Details |
| --- | --- |
| Import | For `.bacpac` files with compressed files over 4 GB in size, you might need to use the .NET Core version of SqlPackage to perform the import. This behavior is due to how .NET Core generates zip headers, which although valid, aren't readable by the .NET Full Framework version of SqlPackage. |
| Deployment | The parameter /p:Storage=File isn't supported. Only Memory is supported on .NET Core. |
| Always Encrypted | SqlPackage .NET Core doesn't support Always Encrypted columns. |
| Security | SqlPackage .NET Core doesn't support the /ua parameter for multifactor authentication. |
| Deployment | Older V2 DACPAC and BACPAC files that use json data serialization aren't supported. |

## 18.3.1 SqlPackage

**Release date:** September 13, 2019

**Build:** 15.0.4538.1

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Add support to deploy to Azure Synapse Analytics (preview). | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Add /p:DatabaseLockTimeout=(`INT`32 '60') parameter to SqlPackage. | SqlPackage CLI |
| Deployment | Add /p:LongRunningCommandTimeout=(`INT`32) parameter to SqlPackage. | SqlPackage CLI |
| Export/Extract | Add /p:TempDirectoryForTableData=(STRING) parameter to SqlPackage. | SqlPackage CLI |
| Deployment | Allow deployment contributors to be loaded from additional locations. Deployment contributors are loaded from the same directory as the target `.dacpac` being deployed, the Extensions directory relative to the SqlPackage binary, and the /p:AdditionalDeploymentContributorPaths=(STRING) parameter added to SqlPackage where additional directory locations can be specified. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Add support for `OPTIMIZE_FOR_SEQUENTIAL_KEY`. | SqlPackage CLI; DacFx API / Schema compare |

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed automatic indexes so that they aren't dropped on deployment. | SqlPackage CLI; DacFx API / Schema compare |
| Always Encrypted | Fixed handling of Always Encrypted varchar columns. | SqlPackage CLI; DacFx API / Schema compare |
| Build/Deployment | Fixed the `nodes()` method resolution for xml column sets. | SqlPackage CLI; DacFx API / Schema compare |
| ScriptDom | Fixed additional cases where the 'URL' string was interpreted as a top level token. | Platform |
| Graph | Fixed generated SQL for pseudo column references in constraints. | SqlPackage CLI; DacFx API / Schema compare |
| Export | Generate random passwords that meet complexity requirements. | SqlPackage CLI |
| Deployment | Fixed command timeouts when retrieving constraints. | SqlPackage CLI |
| .NET Core (preview) | Fixed diagnostic logging to a file. | Platform; SqlPackage CLI |
| .NET Core (preview) | Use streaming to export table data to support large tables. | SqlPackage CLI |

## 18.2 SqlPackage

**Release date:** April 15, 2019

**Build:** 15.0.4384.2

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Graph | Add graph table support for edge constraints and edge constraint clauses. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Enabled model validation rule to support 32 columns for index keys for SQL Server 2016 and up. | SqlPackage CLI; DacFx API / Schema compare |

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed reverse engineering a SQL Server 2016 RTM database due to an unsupported query hint being used. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed deployment ordering of auto close alter statements to occur before create filegroup statements. | SqlPackage CLI; DacFx API / Schema compare |
| ScriptDom | Fixed ScriptDom parsing regression where the 'URL' string was interpreted as a top level token. | Platform |
| Deployment | Fixed a null reference exception when parsing an alter table add index statement. | SqlPackage CLI; DacFx API / Schema compare |
| Schema Compare | Fixed schema compare for nullable persisted computed columns always showing as different. | DacFx API / Schema compare |

## 18.1 SqlPackage

**Release date:** February 1, 2019

**Build:** 15.0.4316.1

Preview release.

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Added support for UTF8 collations. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Enabled nonclustered columnstore indexes on an indexed view. | SqlPackage CLI; DacFx API / Schema compare |
| Platform | Moved to .NET Core 2.2. | Platform |
| Schema Compare | Use memory backed storage for schema compare on .NET Core. | DacFx API / Schema compare |

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| Performance | Performance fix to use the legacy cardinality estimator for reverse engineering queries. | SqlPackage CLI; DacFx API / Schema compare |
| Performance | Fixed a significant schema compare performance issue when generating a script. | DacFx API / Schema compare |
| Schema Compare | Fixed the schema drift detection logic to ignore certain extended event (XEvent) sessions. | DacFx API / Schema compare |
| Graph | Fixed import ordering for graph tables. | SqlPackage CLI; DacFx API / Schema compare |
| Export | Fixed exporting external tables with object permissions. | SqlPackage CLI |

### Known issues

This release includes cross-platform preview builds of SqlPackage that target .NET Core 2.2. The SqlPackage can run on macOS and Linux.

| Known issue | Details |
| --- | --- |
| Deployment | For .NET Core, build and deployment contributors aren't supported. |
| Deployment | For .NET Core, older DACPAC and BACPAC files that use json data serialization aren't supported. |
| Deployment | For .NET Core, referenced DACPACs (for example `master.dacpac`) might not resolve due to issues with case-sensitive file systems. A workaround is to capitalize the name of the reference file (for example `MASTER.DACPAC`). |

## 18.0 SqlPackage

**Release date:** October 24, 2018

**Build:** 15.0.4200.1

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Added support for database compatibility level 150. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Added support for [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. | SqlPackage CLI; DacFx API / Schema compare |
| Performance | Added `MaxParallelism` command-line parameter to specify the degree of parallelism for database operations. | SqlPackage CLI |
| Security | Added `AccessToken` command-line parameter to specify an authentication token when connecting to SQL Server. | SqlPackage CLI |
| Import | Added support to stream BLOB/CLOB data types for imports. | SqlPackage CLI |
| Deployment | Added support for scalar UDF `INLINE` option. | SqlPackage CLI; DacFx API / Schema compare |
| Graph | Added support for graph table `MERGE` syntax. | SqlPackage CLI; DacFx API / Schema compare |

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| Graph | Fixed unresolved pseudo-column for graph tables. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed creating a database with memory optimized file groups when memory optimized tables are used. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed including extended properties on external tables. | SqlPackage CLI; DacFx API / Schema compare |

## 17.8 SqlPackage

**Release date:** June 22, 2018

**Build:** 14.0.4079.2

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Diagnostics | Improved error messages for connection failures, including the SqlClient exception message. | SqlPackage CLI |
| Deployment | Support index compression on single partition indexes for import/export. | SqlPackage CLI; DacFx API / Schema compare |

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| Deployment | Fixed a reverse engineering issue for XML column sets with SQL Server 2017 and later. | SqlPackage CLI; DacFx API / Schema compare |
| Deployment | Fixed an issue where scripting the database compatibility level 140 was ignored for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. | SqlPackage CLI; DacFx API / Schema compare |

## 17.4.1 SqlPackage

**Release date:** January 25, 2018

**Build:** 14.0.3917.1

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Import/Export | Added `ThreadMaxStackSize` command-line parameter to parse Transact-SQL with a large number of nested statements. | SqlPackage CLI |
| Deployment | Database catalog collation support. | SqlPackage CLI; DacFx API / Schema compare |

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| Import | When importing an [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] BACPAC to an on-premises instance, fixed errors due to *Database master keys without password aren't supported in this version of SQL Server*. | SqlPackage CLI |
| Graph | Fixed an unresolved pseudo column error for graph tables. | SqlPackage CLI; DacFx API / Schema compare |
| Schema Compare | Fixed SQL authentication to compare schemas. | DacFx API / Schema compare |

## 17.4.0 SqlPackage

**Release date:** December 12, 2017

**Build:** 14.0.3881.1

### Features

| Feature | Details | Applies to |
| --- | --- | --- |
| Deployment | Added support for *temporal retention policy* on SQL Server 2017+ and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. | SqlPackage CLI; DacFx API / Schema compare |
| Diagnostics | Added /DiagnosticsFile:"C:\Temp\SqlPackage.log" command-line parameter to specify a file path to save diagnostic information. | SqlPackage CLI |
| Diagnostics | Added /Diagnostics command-line parameter to log diagnostic information to the console. | SqlPackage CLI |

### Fixes

| Fix | Details | Applies to |
| --- | --- | --- |
| Deployment | No longer blocks when encountering a database compatibility level that isn't understood. Instead, the latest [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] or on-premises platform is assumed. | SqlPackage CLI; DacFx API / Schema compare |
