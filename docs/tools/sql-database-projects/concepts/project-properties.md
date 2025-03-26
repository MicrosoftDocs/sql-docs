---
title: SQL Projects Properties
description: "Learn about the properties that you can set for SQL database projects."
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 02/19/2025
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
---

# SQL projects properties

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

In addition to the contents of the individual `.sql` files, SQL database projects contain properties that define the project's behavior and database-level settings. These properties are stored in the `.sqlproj` file and can be set by editing the `.sqlproj` file directly. Some SQL projects tools, such as Visual Studio and VS Code, provide access to edit a few or many of the project properties in a graphical user interface. This article provides an overview of the properties that you can set for SQL database projects.

Commonly used SQL projects properties include:

- [Target platform (DSP)](target-platform.md)
- [Code analysis](sql-code-analysis/sql-code-analysis.md)
- [DacApplicationName and DacVersion](#data-tier-application-properties)
- [Default schema](#default-schema)
- [TreatTSqlWarningsAsErrors](#t-sql-warnings)

## Disable database options changes

During SQL project publish, changes to the database options are scripted based on the values defined in the project properties and default project values. To prevent the database options from being modified during publish, using a tool like [SqlPackage CLI](../../sqlpackage/sqlpackage-publish.md) or Visual Studio, set the publish property to `ScriptDatabaseOptions` to false. This setting can also be incorporated in a publish profile.

## Common project properties

The [target platform](target-platform.md) property specifies the version of SQL Server that the project targets. The `DSP` property is used to set the target platform for the SQL project. More information on the target platform can be found in the [target platform](target-platform.md) article.

Code analysis can dramatically improve the continuous integration and deployment process by catching potential issues early in the development lifecycle. Learn more about enabling code analysis and including custom rules in the [SQL code analysis](sql-code-analysis/sql-code-analysis.md) article.

### Data-tier application properties

The following properties are used to define the data-tier application (DAC) that is created when the SQL project is built.

- **DacApplicationName**: The name of the data-tier application `.dacpac`. The default value is the project name.
- **DacDescription**: An optional description of the data-tier application `.dacpac`.
- **DacVersion**: The version of the data-tier application `.dacpac`. The default value is `1.0.0.0`.

### Default schema

The `DefaultSchema` property sets the default schema for the SQL project. This property applies to 1-part named objects. The default value is `dbo`.

### T-SQL warnings

The `SuppressTSqlWarnings` and `TreatTSqlWarningsAsErrors` properties control how T-SQL warnings are handled during project build. The `SuppressTSqlWarnings` property suppresses T-SQL warnings during project build, specified as a comma-separated list of error numbers.

The `TreatTSqlWarningsAsErrors` property treats T-SQL warnings as errors, causing any T-SQL warnings to fail the build. The default value for `TreatTSqlWarningsAsErrors` is `False`.

## Example usage of project properties

The following example shows how to set the `CompatibilityMode`, `IsChangeTrackingOn`, and `TreatTSqlWarningsAsErrors` properties in a SQL project file. The `CompatibilityMode` property is set to `130`, the `IsChangeTrackingOn` property is set to `True`, and the `TreatTSqlWarningsAsErrors` property is set to `True`. The `TreatSqlWarningsAsErrors` property is only set to `True` on the `Release` build configuration.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="1.0.0-rc1" />
  <PropertyGroup>
    <Name>AdventureWorks</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql160DatabaseSchemaProvider</DSP>
    <ModelCollation>1033, CI</ModelCollation>
    <ProjectGuid>{00000000-0000-0000-0000-000000000000}</ProjectGuid>
    <RunSqlCodeAnalysis>true</RunSqlCodeAnalysis>
    <CompatibilityMode>130</CompatibilityMode>
    <IsChangeTrackingOn>True</IsChangeTrackingOn>
  </PropertyGroup>
  <PropertyGroup Condition="'$(Configuration)'=='Release'">
    <TreatTSqlWarningsAsErrors>True</TreatTSqlWarningsAsErrors>
  </PropertyGroup>
</Project>
```

## All project properties

Some project properties are associated with database options that apply to only SQL Server databases or specific versions of SQL Server. Before including a project property in your project, review the associated documentation for the database option to understand the behavior of the property and database requirements.

| Property | `DATABASE SET` option | UI label | SQL project default value | Allowed values |
|---|---|---|---|---|
| AllowSnapshotIsolation | `ALLOW_SNAPSHOT_ISOLATION` | Database settings, Operational, Allow snapshot isolation | False | {True&#124;False} |
| AnsiNulls| `ANSI_NULLS` | Database settings, SET ANSI_NULLS | True | {True&#124;False} |
| AnsiPadding | `ANSI_PADDING` | Database settings, SET ANSI_PADDING | True | {True&#124;False} |
| AnsiWarnings | `ANSI_WARNINGS` | Database settings, SET ANSI_WARNINGS | True | {True&#124;False} |
| ArithAbort | `ARITHABORT` | Database settings, SET ARITHABORT | True | {True&#124;False} |
| AutoClose | `AUTO_CLOSE` | Database settings, Operational, Auto close | False | {True&#124;False} |
| AutoCreateStatistics | `AUTO_CREATE_STATISTICS` | Database settings, Operational, Auto create statistics | True | {True&#124;False} |
| AutoCreateStatisticsIncremental | `AUTO_CREATE_STATISTICS` (`INCREMENTAL`) | Database settings, Operational, Auto create incremental | False | {True&#124;False} |
| AutoShrink | `AUTO_SHRINK` | Database settings, Operational, Auto shrink | False | {True&#124;False} |
| AutoUpdateStatistics | `AUTO_UPDATE_STATISTICS` | Database settings, Operational, Auto update statistics | True | {True&#124;False} |
| AutoUpdateStatisticsAsynchronously | `AUTO_UPDATE_STATISTICS_ASYNC` | Database settings, Operational, Auto update statistics asynchronously | False | {True&#124;False} |
| ChangeTrackingRetentionPeriod | `CHANGE_RETENTION` | Database settings, Operational, Change tracking retention period | 2 | {integer} |
| ChangeTrackingRetentionUnit | `CHANGE_RETENTION` | Database settings, Operational, Change tracking retention period | MINUTES | {DAYS&#124;HOURS&#124;MINUTES} |
| CloseCursorOnCommitEnabled | `CURSOR_CLOSE_ON_COMMIT` | Database settings, Operational, Close cursor on commit enabled | False | {True&#124;False} |
| CompatibilityMode | `COMPATIBILITY_LEVEL` | Database settings, Compatibility level | | {100&#124;110&#124;120&#124;130&#124;140&#124;150&#124;160&#124;170}<sup>1</sup> |
| ConcatNullYieldsNull | `CONCAT_NULL_YIELDS_NULL` | Database settings, SET CONCAT_NULL_YIELDS_NULL | True | {True&#124;False} |
| Containment | `CONTAINMENT` | Database settings, Containment | NONE | {NONE&#124;PARTIAL} |
| DacApplicationName | | Data-tier application (`.dacpac`) properties, name | The project name | {string} |
| DacDescription | | Data-tier application (`.dacpac`) properties, description | | {string} |
| DacVersion | | Data-tier application (`.dacpac`) properties, version | 1.0.0.0 | {semantic version number} |
| DatabaseAccess | `db_user_access_option` | Database settings, Database access | MULTI_USER | {MULTI_USER&#124;SINGLE_USER&#124;RESTRICTED_USER} |
| DatabaseChaining | `DB_CHAINING` | Database settings, Database chaining | False | {True&#124;False} |
| DatabaseDefaultFulltextLanguage | `DEFAULT_FULLTEXT_LANGUAGE` | Database settings, Default fulltext language | 1033 |  {integer [language id](../../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md)} |
| DatabaseDefaultLanguage | `DEFAULT_LANGUAGE` | Database settings, Default language | 1033 | {integer [language id](../../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md)} |
| DatabaseState | `db_state_option` | Database settings, Database state | ONLINE | {ONLINE&#124;OFFLINE} |
| DbScopedConfigLegacyCardinalityEstimation | `LEGACY_CARDINALITY_ESTIMATION`<sup>2</sup> | Database scoped configuration, Legacy cardinality estimation | Off | {Off&#124;On} |
| DbScopedConfigLegacyCardinalitySecondaryEstimation | `LEGACY_CARDINALITY_SECONDARY_ESTIMATION`<sup>2</sup> | Database scoped configuration, Legacy cardinality estimation for secondary | Primary | {Primary&#124;Off&#124;On} |
| DbScopedConfigParameterSniffing | `PARAMETER_SNIFFING`<sup>2</sup> | Database scoped configuration, Parameter sniffing | On | {On&#124;Off} |
| DbScopedConfigParameterSniffingSecondary | `PARAMETER_SNIFFING_SECONDARY`<sup>2</sup> | Database scoped configuration, Parameter sniffing for secondary | Primary | {Primary&#124;Off&#124;On} |
| DbScopedConfigOptimizerHotfixes | `OPTIMIZER_HOTFIXES`<sup>2</sup> | Database scoped configuration, Query optimizer hotfixes | Off | {Off&#124;On} |
| DbScopedConfigOptimizerHotfixesSecondary | `OPTIMIZER_HOTFIXES_SECONDARY`<sup>2</sup> | Database scoped configuration, Query optimizer hotfixes for secondary | Primary | {Primary&#124;Off&#124;On} |
| DbScopedConfigMaxDOP | `MAXDOP`<sup>2</sup> | Database scoped configuration, Max degrees of parallelism | 0 | {integer} |
| DbScopedConfigMaxDOPSecondary | `MAXDOP_SECONDARY`<sup>2</sup> | Database scoped configuration, Max degrees of parallelism for secondary | | {integer} |
| DbScopedConfigDWCompatibilityLevel | `DW_COMPATIBILITY_LEVEL`<sup>2</sup> | Database scoped configuration, DW compatibility level | 0 | {0&#124;10&#124;20&#124;30&#124;40&#124;50&#124;9000}<sup>3</sup> |
| DefaultCollation | `COLLATE`<sup>4</sup> | Database settings, Database collation | SQL_Latin1_General_CP1_CI_AS | See [SQL Server collation name](../../../t-sql/statements/sql-server-collation-name-transact-sql.md) for valid values. |
| DefaultCursor | `CURSOR_DEFAULT` | Database settings, Operational, Default cursor | Local | {Global&#124;Local} |
| DefaultFilegroup | | Database settings, Operational, Default filegroup | PRIMARY | {string} |
| DefaultFileStreamFilegroup | Database settings, Operational, Default filestream filegroup | | {string} |
| DefaultSchema | | General project setting, default schema | dbo | {string} |
| DelayedDurability | `DELAYED_DURABILITY` | Database settings, Operational, Transactions delayed durability | DISABLED | {DISABLED&#124;ALLOWED&#124;FORCED} |
| DSP | | The [target platform](target-platform.md) for the SQL project | | See [target platform](target-platform.md) for valid values. |
| EnableFullTextSearch | | Database settings, Enable full text search | True | {True&#124;False} |
| FileStreamDirectoryName | `FILESTREAM` (`DIRECTORY_NAME`) | Database settings, FILESTREAM directory name | | {string} |
| IsBrokerPriorityHonored | `HONOR_BROKER_PRIORITY` | Database settings, Broker priority honored | False | {True&#124;False} |
| IsChangeTrackingAutoCleanupOn | `CHANGE_TRACKING` | Database settings, Operational, Change tracking auto cleanup | True | {True&#124;False} |
| IsChangeTrackingOn | `CHANGE_TRACKING` | Database settings, Operational, Change tracking | False | {True&#124;False} |
| IsEncryptionOn | `ENCRYPTION` | Database settings, Encryption enabled | False | {True&#124;False} |
| IsLedgerOn | `LEDGER`<sup>4</sup> | Database settings, Enable Ledger | False | {True&#124;False} |
| IsNestedTriggersOn | `NESTED_TRIGGERS` | Database settings, Nested triggers enabled | True | {True&#124;False} |
| IsTransformNoiseWordsOn | `TRANSFORM_NOISE_WORDS` | Database settings, Transform noise words | False | {True&#124;False} |
| MemoryOptimizedElevateToSnapshot | `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT` | Database settings, Operational, Memory optimized elevate to snapshot | False | {True&#124;False} |
| ModelCollation | | Project settings, Collation | 1033,CI | {integer [language id](../../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md)}, {CI&#124;CS} |
| NonTransactedFileStreamAccess | `NON_TRANSACTED_ACCESS` | Database settings, FILESTREAM non-transacted access | OFF | {OFF&#124;READ_ONLY&#124;FULL} |
| NumericRoundAbort | `NUMERIC_ROUNDABORT` | Database settings, SET NUMERIC_ROUNDABORT | False | {True&#124;False} |
| OutputPath | | Build settings, Output path | `bin\Debug` and `bin\Release` | {string} |
| PageVerify | `PAGE_VERIFY` | Database settings, Operational, Page verify | NONE | {NONE&#124;TORN_PAGE_DETECTION&#124;CHECKSUM} |
| Parameterization | `PARAMETERIZATION` | Database settings, Parameterization | SIMPLE | {SIMPLE&#124;FORCED} |
| QueryStoreCaptureMode | `QUERY_STORE` (`QUERY_CAPTURE_MODE`) | Database settings, Operational, Query store capture mode | ALL | {OFF&#124;ALL&#124;AUTO} |
| QueryStoreDesiredState | `QUERY_STORE` (`OPERATION_MODE`) | Database settings, Operational, Query store operation mode | OFF | {OFF&#124;READ_WRITE&#124;READ_ONLY} |
| QueryStoreFlushInterval | `QUERY_STORE` (`DATA_FLUSH_INTERVAL_SECONDS`) | Database settings, Operational, Query store data flush interval (seconds) | 900 | {integer} |
| QueryStoreIntervalLength | `QUERY_STORE` (`INTERVAL_LENGTH_MINUTES`) | Database settings, Operational, Query store interval length (minutes) | 60 | {integer} |
| QueryStoreMaxPlansPerQuery | `QUERY_STORE` (`MAX_PLANS_PER_QUERY`) | Database settings, Operational, Query store max plans per query | 200 | {integer} |
| QueryStoreMaxStorageSize | `QUERY_STORE` (`MAX_STORAGE_SIZE_MB`) | Database settings, Operational, Query store max storage size (MB) | 100 | {integer} |
| QueryStoreStaleQueryThreshold | `QUERY_STORE` (`STALE_QUERY_THRESHOLD_DAYS`) | Database settings, Operational, Query store stale query threshold (days) | 367 | {integer} |
| QuotedIdentifier | `QUOTED_IDENTIFIER` | Database settings, SET QUOTED_IDENTIFIER | True | {True&#124;False} |
| ReadCommittedSnapshot | `READ_COMMITTED_SNAPSHOT` | Database settings, Operational, Read committed snapshot | False | {True&#124;False} |
| Recovery | `RECOVERY` | Database settings, Operational, Recovery | FULL | {FULL&#124;SIMPLE&#124;BULK_LOGGED} |
| RecursiveTriggersEnabled | `RECURSIVE_TRIGGERS` | Database settings, Recursive triggers enabled | False | {True&#124;False} |
| ServiceBrokerOption | `SERVICE_BROKER` | Database settings, Service broker options | DisableBroker | {DisableBroker&#124;EnableBroker&#124;NewBroker&#124;ErrorBrokerConversations} |
| SuppressTSqlWarnings | | Build settings, Suppress T-SQL warnings (comma-separated list of T-SQL warning codes) | | {string} |
| TargetRecoveryTimePeriod | Database settings, Operational, target recovery time (seconds) | Specifies the frequency of indirect checkpoints on a per-database basis. | 60 | {integer} |
| TargetRecoveryTimeUnit | | Database settings, Operational, target recovery time | SECONDS | {MINUTES&#124;SECONDS} |
| TreatTSqlWarningsAsErrors | | Build settings, Treat T-SQL warnings as errors | False | {True&#124;False} |
| Trustworthy | `TRUSTWORTHY` | Database settings, Trustworthy | False | {True&#124;False} |
| TwoDigitYearCutoff | `TWO_DIGIT_YEAR_CUTOFF` | Database settings, Two digit year cutoff | 2049 | {integer} |
| UpdateOptions | `db_update_option` | Database settings, Update options | READ_WRITE | {READ_WRITE&#124;READ_ONLY} |
| ValidateCasingOnIdentifiers | | General project setting, validate the casing of identifiers | True | {True&#124;False} |

1. The default value differs based on engine edition and server settings.
1. [Database scoped configuration](../../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) options.
1. AUTO is set with the 0 value.
1. Applies as a `CREATE DATABASE` [option](../../../t-sql/statements/create-database-transact-sql.md) only.

## Related content

- [Target platform overview](target-platform.md)
- [SQL code analysis to improve code quality](sql-code-analysis/sql-code-analysis.md)
- [SQL projects tools](../sql-projects-tools.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../../t-sql/statements/alter-database-transact-sql-set-options.md)
