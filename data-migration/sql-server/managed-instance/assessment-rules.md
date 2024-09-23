---
title: "Assessment rules for SQL Server to Azure SQL Managed Instance migration"
description: Assessment rules to identify issues with the source SQL Server instance that must be addressed before migrating to Azure SQL Managed Instance.
author: rajeshsetlem
ms.author: rsetlem
ms.reviewer: mathoma, randolphwest
ms.date: 06/26/2024
ms.service: azure-sql-managed-instance
ms.subservice: migration-guide
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# Assessment rules for SQL Server to Azure SQL Managed Instance migration

[!INCLUDE [appliesto-sqlmi](../../includes/appliesto-sqlmi.md)]

Migration tools validate your source SQL Server instance by running several assessment rules. The rules identify issues that must be addressed before migrating your SQL Server database to Azure SQL Managed Instance.

This article provides a list of the rules used to assess the feasibility of migrating your SQL Server database to Azure SQL Managed Instance.

## Rules summary

| Rule Title | Level | Category | Details |
| --- | --- | --- | --- |
| `AnalysisCommandJob` | Instance | Warning | [AnalysisCommand job step isn't supported in Azure SQL Managed Instance.](#AnalysisCommandJob) |
| `AnalysisQueryJob` | Instance | Warning | [AnalysisQuery job step isn't supported in Azure SQL Managed Instance.](#AnalysisQueryJob) |
| `AssemblyFromFile` | Database | Issue | ['CREATE ASSEMBLY' and 'ALTER ASSEMBLY' with a file parameter are unsupported in Azure SQL Managed Instance.](#AssemblyFromFile) |
| `BulkInsert` | Database | Issue | [BULK INSERT with non-Azure blob data source isn't supported in Azure SQL Managed Instance.](#BulkInsert) |
| `ClrStrictSecurity` | Database | Warning | [CLR assemblies marked as SAFE or EXTERNAL_ACCESS are considered UNSAFE.](#ClrStrictSecurity) |
| `ComputeClause` | Database | Warning | [COMPUTE clause is no longer supported and has been removed.](#ComputeClause) |
| `CryptographicProvider` | Database | Issue | [A use of CREATE CRYPTOGRAPHIC PROVIDER or ALTER CRYPTOGRAPHIC PROVIDER was found. This isn't supported in Azure SQL Managed Instance.](#CryptographicProvider) |
| `DatabasePrincipalAlias` | Database | Issue | [SYS.DATABASE_PRINCIPAL_ALIASES is no longer supported and has been removed.](#DatabasePrincipalAlias) |
| `DbCompatLevelLowerThan100` | Database | Warning | [Database compatibility level below 100 isn't supported.](#DbCompatLevelLowerThan100) |
| `DisableDefCNSTCHK` | Database | Issue | [SET option DISABLE_DEF_CNST_CHK is no longer supported and has been removed.](#DisableDefCNSTCHK) |
| `FastFirstRowHint` | Database | Warning | [FASTFIRSTROW query hint is no longer supported and has been removed.](#FastFirstRowHint) |
| `FileStream` | Database | Issue | [FILESTREAM and FileTable aren't supported in Azure SQL Managed Instance.](#FileStream) |
| `LinkedServerWithNonSQLProvider` | Database | Issue | [Linked server with non-SQL Server Provider isn't supported in Azure SQL Managed Instance.](#LinkedServerWithNonSQLProvider) |
| `MergeJob` | Instance | Warning | [Merge job step isn't supported in Azure SQL Managed Instance.](#MergeJob) |
| `MIDatabaseSize` | Database | Issue | [Azure SQL Managed Instance doesn't support database size greater than 16 TB.](#MIDatabaseSize) |
| `MIHeterogeneousMSDTCTransactSQL` | Database | Issue | [BEGIN DISTRIBUTED TRANSACTION with non-SQL Server remote server isn't supported in Azure SQL Managed Instance.](#MIHeterogeneousMSDTCTransactSQL) |
| `MIHomogeneousMSDTCTransactSQL` | Database | Issue | [BEGIN DISTRIBUTED TRANSACTION is supported across multiple servers for Azure SQL Managed Instance.](#MIHomogeneousMSDTCTransactSQL) |
| `MIInstanceSize` | Instance | Warning | [Maximum instance storage size in Azure SQL Managed Instance can't be greater than 8 TB.](#MIInstanceSize) |
| `MultipleLogFiles` | Database | Issue | [Azure SQL Managed Instance doesn't support databases with multiple log files.](#MultipleLogFiles<) |
| `NextColumn` | Database | Issue | [Tables and Columns named NEXT lead to an error In Azure SQL Managed Instance.](#NextColumn) |
| `NonANSILeftOuterJoinSyntax` | Database | Warning | [Non-ANSI style left outer join is no longer supported and has been removed.](#NonANSILeftOuterJoinSyntax) |
| `NonANSIRightOuterJoinSyntax` | Database | Warning | [Non-ANSI style right outer join is no longer supported and has been removed.](#NonANSIRightOuterJoinSyntax) |
| `NumDbExceeds100` | Instance | Warning | [Azure SQL Managed Instance supports a maximum of 100 databases per instance.](#NumDbExceeds100) |
| `OpenRowsetWithNonBlobDataSourceBulk` | Database | Issue | [OpenRowSet used in bulk operation with non-Azure blob storage data source isn't supported in Azure SQL Managed Instance.](#OpenRowsetWithNonBlobDataSourceBulk) |
| `OpenRowsetWithNonSQLProvider` | Database | Issue | [OpenRowSet with non-SQL provider isn't supported in Azure SQL Managed Instance.](#OpenRowsetWithNonSQLProvider) |
| `PowerShellJob` | Instance | Warning | [PowerShell job step isn't supported in Azure SQL Managed Instance.](#PowerShellJob) |
| `QueueReaderJob` | Instance | Warning | [Queue Reader job step isn't supported in Azure SQL Managed Instance.](#QueueReaderJob) |
| `RAISERROR` | Database | Warning | [Legacy style RAISERROR calls should be replaced with modern equivalents.](#RAISERROR) |
| `SqlMail` | Database | Warning | [SQL Mail is no longer supported.](#SqlMail) |
| `SystemProcedures110` | Database | Warning | [Detected statements that reference removed system stored procedures that aren't available in Azure SQL Managed Instance.](#SystemProcedures110) |
| `TraceFlags` | Instance | Warning | [Trace flags not supported in Azure SQL Managed Instance were found.](#TraceFlags) |
| `TransactSqlJob` | Instance | Warning | [TSQL job step includes unsupported commands in Azure SQL Managed Instance.](#TransactSqlJob) |
| `WindowsAuthentication` | Instance | Warning | [Database users mapped with Windows authentication (integrated security) aren't supported in Azure SQL Managed Instance.](#WindowsAuthentication) |
| `XpCmdshell` | Database | Issue | [xp_cmdshell isn't supported in Azure SQL Managed Instance.](#XpCmdshell) |

## <a id="AnalysisCommandJob"></a> AnalysisCommand job

**Title: AnalysisCommand job step is not supported in Azure SQL Managed Instance.**  
**Category**: Warning

**Description**  
It is a job step that runs an Analysis Services command. AnalysisCommand job step isn't supported in Azure SQL Managed Instance.

**Recommendation**  
Review affected objects section in Azure Migrate to see all jobs using Analysis Service Command job step and evaluate if the job step or the affected object can be removed. Alternatively, migrate to SQL Server on Azure VMs.

More information: [SQL Server Agent differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#sql-server-agent)

## <a id="AnalysisQueryJob"></a> AnalysisQuery job

**Title: AnalysisQuery job step is not supported in Azure SQL Managed Instance.**  
**Category**: Warning

**Description**  
It is a job step that runs an Analysis Services query. AnalysisQuery job step isn't supported in Azure SQL Managed Instance.

**Recommendation**  
Review affected objects section in Azure Migrate to see all jobs using Analysis Service Query job step and evaluate if the job step or the affected object can be removed. Alternatively, migrate to SQL Server on Azure VMs.

More information: [SQL Server Agent differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#sql-server-agent)

## <a id="AssemblyFromFile"></a> Assembly from file

**Title: 'CREATE ASSEMBLY' and 'ALTER ASSEMBLY' with a file parameter are unsupported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
Azure SQL Managed Instance doesn't support `CREATE ASSEMBLY` or `ALTER ASSEMBLY` with a file parameter. A binary parameter is supported. See the Affected Objects section for the specific object where the file parameter is used.

**Recommendation**  
Review objects using `CREATE ASSEMBLY` or `ALTER ASSEMBLY` with a file parameter. If any such objects that are required, convert the file parameter to a binary parameter. Alternatively, migrate to SQL Server on Azure VMs.

More information: [CLR differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#clr)

## <a id="BulkInsert"></a> BULK INSERT

**Title: BULK INSERT with non-Azure blob data source is not supported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
Azure SQL Managed Instance can't access file shares or Windows folders. See the "Affected Objects" section for the specific uses of BULK INSERT statements that don't reference an Azure blob. Objects with 'BULK INSERT' where the source isn't Azure Blob Storage doesn't work after migrating to Azure SQL Managed Instance.

**Recommendation**  
You need to convert BULK INSERT statements that use local files or file shares to use files from Azure Blob Storage instead, when migrating to Azure SQL Managed Instance.

More information: [Bulk Insert and OPENROWSET differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#bulk-insert--openrowset)

## <a id="ClrStrictSecurity"></a> CLR security

**Title: CLR assemblies marked as SAFE or EXTERNAL_ACCESS are considered UNSAFE**  
**Category**: Warning

**Description**  
CLR Strict Security mode is enforced in Azure SQL Managed Instance. This mode is enabled by default and introduces breaking changes for databases containing user-defined CLR assemblies marked either SAFE or EXTERNAL_ACCESS.

**Recommendation**  

[!INCLUDE [code-access-security](../../includes/code-access-security.md)]

## <a id="ComputeClause"></a> COMPUTE clause

**Title: COMPUTE clause is no longer supported and has been removed.**  
**Category**: Warning

**Description**  
The COMPUTE clause generates totals that appear as additional summary columns at the end of the result set. However, this clause is no longer supported in Azure SQL Managed Instance.

**Recommendation**  
The T-SQL module needs to be rewritten using the ROLLUP operator instead. The following code demonstrates how COMPUTE can be replaced with ROLLUP:

```sql
USE AdventureWorks2022;
GO

SELECT SalesOrderID,
    UnitPrice,
    UnitPriceDiscount
FROM Sales.SalesOrderDetail
ORDER BY SalesOrderID COMPUTE SUM(UnitPrice),
    SUM(UnitPriceDiscount) BY SalesOrderID;
GO

SELECT SalesOrderID,
    UnitPrice,
    UnitPriceDiscount,
    SUM(UnitPrice) AS UnitPrice,
    SUM(UnitPriceDiscount) AS UnitPriceDiscount
FROM Sales.SalesOrderDetail
GROUP BY SalesOrderID,
    UnitPrice,
    UnitPriceDiscount
WITH ROLLUP;
```

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

## <a id="CryptographicProvider"></a> Cryptographic provider

**Title: A use of CREATE CRYPTOGRAPHIC PROVIDER or ALTER CRYPTOGRAPHIC PROVIDER was found, which is not supported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
Azure SQL Managed Instance doesn't support CRYPTOGRAPHIC PROVIDER statements because it can't access files. See the Affected Objects section for the specific uses of CRYPTOGRAPHIC PROVIDER statements. Objects with 'CREATE CRYPTOGRAPHIC PROVIDER' or 'ALTER CRYPTOGRAPHIC PROVIDER' doesn't work correctly after migrating to Azure SQL Managed Instance.

**Recommendation**  
Review objects with 'CREATE CRYPTOGRAPHIC PROVIDER' or 'ALTER CRYPTOGRAPHIC PROVIDER'. In any such objects that are required, remove the uses of these features. Alternatively, migrate to SQL Server on Azure VMs.

More information: [Cryptographic provider differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#cryptographic-providers)

## <a id="DbCompatLevelLowerThan100"></a> Database compatibility

**Title: Database compatibility level below 100 is not supported**  
**Category**: Warning

**Description**  
Database Compatibility Level is a valuable tool to help with database modernization, by allowing the SQL Server Database Engine to be upgraded, while keeping connecting applications functional status by maintaining the same pre-upgrade Database Compatibility Level. Azure SQL Managed Instance doesn't support compatibility levels below 100. When the database with compatibility level below 100 is restored on Azure SQL Managed Instance, the compatibility level is upgraded to 100.

**Recommendation**  
Evaluate if the application functionality is intact when the database compatibility level is upgraded to 100 on Azure SQL Managed Instance. Alternatively, migrate to SQL Server on Azure VMs.

More information: [Supported compatibility levels in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#compatibility-levels)

## <a id="DatabasePrincipalAlias"></a> Database principal alias

**Title: SYS.DATABASE_PRINCIPAL_ALIASES is no longer supported and has been removed.**  
**Category**: Issue

**Description**  
`sys.database_principal_aliases` is no longer supported and has been removed in Azure SQL Managed Instance.

**Recommendation**  
Use roles instead of aliases.

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

## <a id="DisableDefCNSTCHK"></a> DISABLE_DEF_CNST_CHK option

**Title: SET option DISABLE_DEF_CNST_CHK is no longer supported and has been removed.**  
**Category**: Issue

**Description**  
SET option DISABLE_DEF_CNST_CHK is no longer supported and has been removed in Azure SQL Managed Instance.

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

## <a id="FastFirstRowHint"></a> FASTFIRSTROW hint

**Title: FASTFIRSTROW query hint is no longer supported and has been removed.**  
**Category**: Warning

**Description**  
FASTFIRSTROW query hint is no longer supported and has been removed in Azure SQL Managed Instance.

**Recommendation**  
Instead of FASTFIRSTROW query hint use OPTION (FAST n).

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

## <a id="FileStream"></a> FILESTREAM

**Title: FILESTREAM and FileTable are not supported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
The FILESTREAM feature, which allows you to store unstructured data such as text documents, images, and videos in NTFS file system, isn't supported in Azure SQL Managed Instance. **This database can't be migrated as the backup containing FILESTREAM filegroups can't be restored on Azure SQL Managed Instance.**

**Recommendation**  
Upload the unstructured files to Azure Blob storage and store metadata related to these files (name, type, URL location, storage key etc.) in Azure SQL Managed Instance. You might have to re-engineer your application to enable streaming blobs to and from Azure SQL Managed Instance. Alternatively, migrate to SQL Server on Azure VMs.

More information: [Streaming Blobs To and From SQL Azure blog](https://azure.microsoft.com/blog/streaming-blobs-to-and-from-sql-azure/)

## <a id="MIHeterogeneousMSDTCTransactSQL"></a> Heterogeneous MS DTC

**Title: BEGIN DISTRIBUTED TRANSACTION with non-SQL Server remote server is not supported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
Distributed transaction started by Transact SQL BEGIN DISTRIBUTED TRANSACTION and managed by Microsoft Distributed Transaction Coordinator (MS DTC) isn't supported in Azure SQL Managed Instance if the remote server isn't SQL Server.

**Recommendation**  
Review affected objects section in Azure Migrate to see all objects using BEGIN DISTRUBUTED TRANSACTION. Consider migrating the participant databases to Azure SQL Managed Instance where distributed transactions across multiple instances are supported. For more information, see [Transactions across multiple servers for Azure SQL Managed Instance](/azure/azure-sql/database/elastic-transactions-overview#transactions-for-sql-managed-instance).

Alternatively, migrate to SQL Server on Azure VMs.

## <a id="MIHomogeneousMSDTCTransactSQL"></a> Homogenous MS DTC

**Title: BEGIN DISTRIBUTED TRANSACTION is supported across multiple servers for Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
Distributed transaction started by Transact SQL BEGIN DISTRIBUTED TRANSACTION and managed by Microsoft Distributed Transaction Coordinator (MS DTC) is supported across multiple servers for Azure SQL Managed Instance.

**Recommendation**  
Review affected objects section in Azure Migrate to see all objects using BEGIN DISTRUBUTED TRANSACTION. Consider migrating the participant databases to Azure SQL Managed Instance where distributed transactions across multiple instances are supported. For more information, see [Transactions across multiple servers for Azure SQL Managed Instance](/azure/azure-sql/database/elastic-transactions-overview#transactions-for-sql-managed-instance).

Alternatively, migrate to SQL Server on Azure VMs.

## <a id="LinkedServerWithNonSQLProvider"></a> Linked server (non-SQL provider)

**Title: Linked server with non-SQL Server Provider is not supported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
Linked servers enable the SQL Server Database Engine to execute commands against OLE DB data sources outside of the instance of SQL Server. Linked server with non-SQL Server Provider isn't supported in Azure SQL Managed Instance.

**Recommendation**  
Azure SQL Managed Instance doesn't support linked server functionality if the remote server provider is non-SQL Server like Oracle, Sybase etc.

The following actions are recommended to eliminate the need for linked servers:

- Identify the dependent databases from remote non-SQL servers and consider moving these into the database being migrated.
- Migrate the dependent databases to supported targets like SQL Managed Instance, SQL Database, Azure Synapse, and SQL Server instances.
- Consider creating linked server between Azure SQL Managed Instance and SQL Server on Azure Virtual Machines (SQL VM). Then from the SQL VM, create a linked server to Oracle, Sybase, etc. This approach does involve two hops but can be used as temporary workaround.
- Alternatively, migrate to SQL Server on Azure VMs.

More information: [Linked Server differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#linked-servers)

## <a id="MergeJob"></a> Merge job

**Title: Merge job step is not supported in Azure SQL Managed Instance.**  
**Category**: Warning

**Description**  
It is a job step that activates the replication Merge Agent. The Replication Merge Agent is a utility executable that applies the initial snapshot held in the database tables to the Subscribers. It also merges incremental data changes that occurred at the Publisher after the initial snapshot was created, and reconciles conflicts either according to the rules you configure, or using a custom resolver you create. Merge job step isn't supported in Azure SQL Managed Instance.

**Recommendation**  
Review affected objects section in Azure Migrate to see all jobs using Merge job step and evaluate if the job step or the affected object can be removed. Alternatively, migrate to SQL Server on Azure VMs.

More information: [SQL Server Agent differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#sql-server-agent)

## <a id="MIDatabaseSize"></a> SQL Managed Instance database size

**Title: Azure SQL Managed Instance does not support database size greater than 16 TB.**  
**Category**: Issue

**Description**  
The size of the database is greater than maximum instance reserved storage. **This database can't be selected for migration as the size exceeded the allowed limit.**

**Recommendation**  
Evaluate if the data can be archived compressed or sharded into multiple databases. Alternatively, migrate to SQL Server on Azure VMs.

More information: [Hardware characteristics of Azure SQL Managed Instance](/azure/azure-sql/managed-instance/resource-limits#hardware-configuration-characteristics)

## <a id="MIInstanceSize"></a> SQL Managed Instance instance size

**Title: Maximum instance storage size in Azure SQL Managed Instance cannot be greater than 8 TB.**  
**Category**: Warning

**Description**  
The size of all databases is greater than maximum instance reserved storage.

**Recommendation**  
Consider migrating the databases to different Azure SQL Managed Instances or to SQL Server on Azure Virtual Machines if all the databases must exist on the same instance.

More information: [Hardware characteristics of Azure SQL Managed Instance](/azure/azure-sql/managed-instance/resource-limits#hardware-configuration-characteristics)

## <a id="MultipleLogFiles<"></a> Multiple log files

**Title: Azure SQL Managed Instance does not support multiple log files.**  
**Category**: Issue

**Description**  
SQL Server allows a database to log to multiple files. This database has multiple log files, which isn't supported in Azure SQL Managed Instance. **This database can't be migrated as the backup can't be restored on Azure SQL Managed Instance.**

**Recommendation**  
Azure SQL Managed Instance supports only a single log per database. You need to delete all but one of the log files before migrating this database to Azure:

```sql
ALTER DATABASE [database_name] REMOVE FILE [log_file_name]
```

More information: [Unsupported database options in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#database-options)

## <a id="NextColumn"></a> Next column

**Title: Tables and Columns named NEXT will lead to an error In Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
Tables or columns named NEXT were detected. Sequences, introduced in Microsoft SQL Server, use the ANSI standard NEXT VALUE FOR function. Tables or columns named NEXT and column aliased as VALUE with the ANSI standard AS omitted can cause an error.

**Recommendation**  
Rewrite statements to include the ANSI standard AS keyword when aliasing a table or column. For example, when a column is named NEXT and that column is aliased as VALUE, the query SELECT NEXT VALUE FROM TABLE causes an error, and should be rewritten as SELECT NEXT AS VALUE FROM TABLE. Similarly, for a table named NEXT and aliased as VALUE, the query SELECT Col1 FROM NEXT VALUE causes an error, and should be rewritten as SELECT Col1 FROM NEXT AS VALUE.

## <a id="NonANSILeftOuterJoinSyntax"></a> Non-ANSI style left outer join

**Title: Non-ANSI style left outer join is no longer supported and has been removed.**  
**Category**: Warning

**Description**  
Non-ANSI style left outer join is no longer supported and has been removed in Azure SQL Managed Instance.

**Recommendation**  
Use ANSI join syntax.

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

## <a id="NonANSIRightOuterJoinSyntax"></a> Non-ANSI style right outer join

**Title: Non-ANSI style right outer join is no longer supported and has been removed.**  
**Category**: Warning

**Description**  
Non-ANSI style right outer join is no longer supported and has been removed in Azure SQL Managed Instance.

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

**Recommendation**  
Use ANSI join syntax.

## <a id="NumDbExceeds100"></a> Databases exceed 100

**Title: Azure SQL Managed Instance supports a maximum of 100 databases per instance.**  
**Category**: Warning

**Description**  
Maximum number of databases supported in Azure SQL Managed Instance is 100, unless the instance storage size limit has been reached.

**Recommendation**  
Consider migrating the databases to different Azure SQL Managed Instances or to SQL Server on Azure Virtual Machines if all the databases must exist on the same instance.

More information: [Azure SQL Managed Instance Resource Limits](/azure/azure-sql/managed-instance/resource-limits#service-tier-characteristics)

## <a id="OpenRowsetWithNonBlobDataSourceBulk"></a> OPENROWSET (non-BLOB data source)

**Title: OpenRowSet used in bulk operation with non-Azure blob storage data source is not supported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
OPENROWSET supports bulk operations through a built-in BULK provider that enables data from a file to be read and returned as a rowset. OPENROWSET with non-Azure blob storage data source isn't supported in Azure SQL Managed Instance.

**Recommendation**  
Azure SQL Managed Instance can't access file shares and Windows folders, so the files must be imported from Azure Blob Storage. Therefore, only blob type DATASOURCE is supported in OPENROWSET function. Alternatively, migrate to SQL Server on Azure VMs.

More information: [Bulk Insert and OPENROWSET differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#bulk-insert--openrowset)

## <a id="OpenRowsetWithNonSQLProvider"></a> OPENROWSET (non-SQL provider)

**Title: OpenRowSet with non-SQL provider is not supported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
This method is an alternative to accessing tables in a linked server and is a one-time, ad hoc method of connecting and accessing remote data by using OLE DB. OpenRowSet with non-SQL provider isn't supported in Azure SQL Managed Instance.

**Recommendation**  
OPENROWSET function can be used to execute queries only on SQL Server instances (either managed, on-premises, or in Virtual Machines). The providers `SQLNCLI`, `SQLNCLI11`, `SQLOLEDB`, and `MSOLEDBSQL` (recommended) are supported. The [Microsoft OLE DB Driver for SQL Server](/sql/connect/oledb/oledb-driver-for-sql-server) is recommended for new development.

The recommendation action is to identify the dependent databases from remote non-SQL Servers, and consider moving these into the instance being migrated.

More information: [Bulk Insert and OPENROWSET differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#bulk-insert--openrowset)

## <a id="PowerShellJob"></a> PowerShell job

**Title: PowerShell job step is not supported in Azure SQL Managed Instance.**  
**Category**: Warning

**Description**  
It is a job step that runs a PowerShell script. PowerShell job step isn't supported in Azure SQL Managed Instance.

**Recommendation**  
Review affected objects section in Azure Migrate to see all jobs using PowerShell job step and evaluate if the job step or the affected object can be removed. Evaluate if Azure Automation can be used. Alternatively, migrate to SQL Server on Azure VMs.

More information: [SQL Server Agent differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#sql-server-agent)

## <a id="QueueReaderJob"></a> Queue Reader job

**Title: Queue Reader job step is not supported in Azure SQL Managed Instance.**  
**Category**: Warning

**Description**  
It is a job step that activates the replication Queue Reader Agent. The Replication Queue Reader Agent is an executable that reads messages stored in a Microsoft SQL Server queue or a Microsoft Message Queue and then applies those messages to the Publisher. Queue Reader Agent is used with snapshot and transactional publications that allow queued updating. Queue Reader job step isn't supported in Azure SQL Managed Instance.

**Recommendation**  
Review affected objects section in Azure Migrate to see all jobs using Queue Reader job step and evaluate if the job step or the affected object can be removed. Alternatively, migrate to SQL Server on Azure VMs.

More information: [SQL Server Agent differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#sql-server-agent)

## <a id="RAISERROR"></a> RAISERROR

**Title: Legacy style RAISERROR calls should be replaced with modern equivalents.**  
**Category**: Warning

**Description**  
RAISERROR calls like the below example are termed as legacy-style because they don't include the commas and the parenthesis. `RAISERROR 50001 'this is a test'`. This method of calling RAISERROR is no longer supported and removed in Azure SQL Managed Instance.

**Recommendation**  
Rewrite the statement using the current RAISERROR syntax, or evaluate if the modern approach of `BEGIN TRY { } END TRY BEGIN CATCH { THROW; } END CATCH` is feasible.

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

## <a id="SqlMail"></a> SQL Mail

**Title: SQL Mail has been no longer supported.**  
**Category**: Warning

**Description**  
SQL Mail has been no longer supported and removed in Azure SQL Managed Instance.

**Recommendation**  
Use Database Mail.

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

## <a id="SystemProcedures110"></a> SystemProcedures110

**Title: Detected statements that reference removed system stored procedures that are not available in Azure SQL Managed Instance.**  
**Category**: Warning

**Description**  
Following unsupported system and extended stored procedures can't be used in Azure SQL Managed Instance - `sp_dboption`, `sp_addserver`, `sp_dropalias`,`sp_activedirectory_obj`, `sp_activedirectory_scp`, and `sp_activedirectory_start`.

**Recommendation**  
Remove references to unsupported system procedures that have been removed in Azure SQL Managed Instance.

More information: [Discontinued Database Engine Functionality in SQL Server](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016#Denali)

## <a id="TransactSqlJob"></a> Transact-SQL job

**Title: TSQL job step includes unsupported commands in Azure SQL Managed Instance**  
**Category**: Warning

**Description**  
It is a job step that runs Transact-SQL scripts at scheduled time. TSQL job step includes unsupported commands, which aren't supported in Azure SQL Managed Instance.

**Recommendation**  
Review affected objects section in Azure Migrate to see all jobs that include unsupported commands in Azure SQL Managed Instance and evaluate if the job step or the affected object can be removed. Alternatively, migrate to SQL Server on Azure VMs.

More information: [SQL Server Agent differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#sql-server-agent)

## <a id="TraceFlags"></a> Trace flags

**Title: Trace flags not supported in Azure SQL Managed Instance were found**  
**Category**: Warning

**Description**  
Azure SQL Managed Instance supports only limited number of global trace flags. Session trace flags aren't supported.

**Recommendation**  
Review affected objects section in Azure Migrate to see all trace flags that aren't supported in Azure SQL Managed Instance and evaluate if they can be removed. Alternatively, migrate to SQL Server on Azure VMs.

More information: [Trace flags](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql#trace-flags)

## <a id="WindowsAuthentication"></a> Windows authentication

**Title: Database users mapped with Windows authentication (integrated security) are not supported in Azure SQL Managed Instance**  
**Category**: Warning

**Description**  
Azure SQL Managed Instance supports two types of authentication:

- SQL Authentication, which uses a username and password
- Microsoft Entra authentication, which uses identities managed by Microsoft Entra ID and is supported for managed and integrated domains.

Database users mapped with Windows authentication (integrated security) aren't supported in Azure SQL Managed Instance.

**Recommendation**  
Federate the local Active Directory with Microsoft Entra ID. The Windows identity can then be replaced with the equivalent Microsoft Entra identities. Alternatively, migrate to SQL Server on Azure VMs.

More information: [SQL Managed Instance security capabilities](/azure/azure-sql/database/security-overview#authentication)

## <a id="XpCmdshell"></a> xp_cmdshell

**Title: xp_cmdshell is not supported in Azure SQL Managed Instance.**  
**Category**: Issue

**Description**  
`xp_cmdshell`, which spawns a Windows command shell and passes in a string for execution isn't supported in Azure SQL Managed Instance.

**Recommendation**  
Review affected objects section in Azure Migrate to see all objects using `xp_cmdshell` and evaluate if the reference to `xp_cmdshell` or the affected object can be removed. Consider exploring Azure Automation that delivers cloud-based automation and configuration service. Alternatively, migrate to SQL Server on Azure VMs.

More information: [Stored Procedure differences in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#stored-procedures-functions-and-triggers)

## Related content

- [Migration guide: SQL Server to Azure SQL Managed Instance](guide.md)
- [Services and tools available for data migration scenarios](/azure/dms/dms-tools-matrix)
- [Service Tiers in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview#service-tiers)
- [T-SQL differences between SQL Server & Azure SQL Managed Instance](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server)
- [Azure total Cost of Ownership Calculator](https://azure.microsoft.com/pricing/tco/calculator/)
- [Cloud Adoption Framework for Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-scale)
- [Best practices for costing and sizing workloads migrate to Azure](/azure/cloud-adoption-framework/migrate/azure-best-practices/migrate-best-practices-costs)
- [Data Access Migration Toolkit (Preview)](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit)
- [Overview of Database Experimentation Assistant](/sql/dea/database-experimentation-assistant-overview)
