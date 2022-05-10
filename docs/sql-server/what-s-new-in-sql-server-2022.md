---
title: "What's new in SQL Server 2022 | Microsoft Docs"
description: Learn about new features for SQL Server 2022 (16.x), which gives you choices of development languages, data types, environments, and operating systems.
ms.date: 05/04/2022
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">=sql-server-ver15"
ms.custom:
  - intro-whats-new
---

# What's new in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]

[!INCLUDE [sqlserver2022](../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud environments, and operating systems.

This article summarizes the new features and enhancements for [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)].

For more information and known issues, see [[!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] release notes](sql-server-2022-release-notes.md).

For the best experience with [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)], use the [latest tools](../azure-data-studio/download-azure-data-studio.md).

The following sections provide an overview of these features.

## Analytics

| New feature or update | Details |
|:---|:---|
|Azure Synapse Link for SQL|Get near real time analytics over operational data in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]. With a seamless integration between operational stores in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] and Azure Synapse Analytics dedicated SQL pools, Azure Synapse Link for SQL enables you to run analytics, business intelligence and machine learning scenarios on your operational data with minimum impact on source databases with a new change feed technology. <br/><br/> For details and known limitations, see [Create Synapse Link for SQL Server 2022 Preview - Azure Synapse Analytics](/azure/synapse-analytics/synapse-link/connect-synapse-link-sql-server-2022#known-limitations). <br/></br> See also, [Known issues](/azure/synapse-analytics/synapse-link/synapse-link-for-sql-2022-known-issues)|
|Object storage integration | SQL Server 2022 Preview introduces object storage integration to the data platform, enabling you to integrate SQL Server with S3 compatible object storage in addition to Azure Storage. Object storage integration enables two scenarios in SQL Server 2022. The first is BACKUP TO URL and the second is Data Lake Virtualization.
|Data virtualization | Enhances data virtualization capabilities by integrating Polybase with S3 compatible object storage, also adding the support to Parquet file types.|
| Backup and restore to S3 compatible object storage | SQL Server 2022 extends the `BACKUP`/`RESTORE` `TO`/`FROM` `URL` syntax by adding support for a new S3 connector using the REST API.|

## Availability

| New feature or update | Details |
|:---|:---|
|Contained availability group | Create an Always On availability group that:<br/>- Manages its own metadata objects (users, logins, permissions, SQL Agent jobs etc.) at the availability group level in addition to the instance level. <br/>- Includes specialized contained system databases within the availability group. For more information, see [What is a contained availability group?](../database-engine/availability-groups/windows/contained-availability-groups-overview.md)|
|Distributed availability group |- Changing `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is supported. For more information, visit [ALTER AVAILABILITY GROUP (Transact-SQL)](../t-sql/statements/alter-availability-group-transact-sql.md)<br/>- Now using multiple TCP connections for better network bandwidth utilization across a remote link with long tcp latencies.|
| Improved availability groups | Parallel redo and improvements for readable secondary replicas. |
| Improved backup metadata | Last valid restore time|

## Security

| New feature or update | Details |
|:---|:---|
|Azure Purview integration|Apply Purview policies to any SQL Server instance that is enrolled in both Azure Arc and to Azure Purview data governance.<br/><br/>- Follow the principle of least privilege using Azure Purview data and access policies.|
|Ledger | In-database blockchain to create an immutable track record of data modifications over time. See [SQL Database ledger](/azure/azure-sql/database/ledger-landing).|
|Azure Active Directory authentication| Manage integrated authentication with Azure Active Directory.|
|Always encrypted with secure enclaves | Enable in-place encryption and richer confidential queries. Support for confidential queries with JOIN, GROUP BY, and ORDER BY. Improved performance. See [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md).| 
|Enhanced encryption | TDS 8.0 - TDS wrapped in TLS.|
|New permissions & roles | Enable least privileged access for administrative tasks. See summary information in [New granular permissions & roles](#new-granular-permissions--roles).|
|Dynamic data masking | Granular permissions for [Dynamic Data Masking](../relational-databases/security/dynamic-data-masking.md).|
| Support for PFX certificates | Supports certificate, and key backup and restore scenarios, along with integration with Azure Blob Storage service for the same. This enables adherence to security best practices and compliance standards guidelines that prohibit the usage of insecure or deprecated algorithms like RC4 and SHA-1.| 


## Performance

| New feature or update | Details |
|:---|:---|
| Query Store improvements | Accelerated query performance with no code changes with improvements to: <br/><br/>- [Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md). For more information, see [Query Store improvements](#query-store-improvements) later in this article. <br/>- Query Store on secondary replicas enables the same Query Store functionality on secondary replica workloads that is available for primary replicas. Learn more in [Query Store for secondary replicas](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#query-store-for-secondary-replicas). <br/>|
| Memory grant feedback | Optimize memory allocation is stored in the query store, so when the memory grant information is available when the query returns to cache after eviction.|
| Percentile grant |  A new algorithm improves performance of queries with widely vacillating memory requirements. Reviews more than just the single previous memory need - includes information from further back in the history as well.|
| In-memory OLTP management | - Improve memory management in large memory servers to reduce out of memory conditions <br/>- Add a new stored procedure to manually release unused memory on demand.|
| Parameter sensitive plan optimization | Automatically enables multiple, active cached plans for a single parameterized statement. Cached execution plans accommodate largely different data sizes based on the customer-provided runtime parameter value(s).|
| Query Store hints | [Query Store hints]](../relational-databases/performance/query-store-hints.md) leverage leverages Query Store to provide a method to shape query plans without changing application code. Previously only available on Azure SQL Database and Azure SQL Managed Instance, now are available in SQL Server 2022 Preview.|
| XML compression |XML compression provides a method to compress off-row XML data for both XML columns and indexes, improving capacity requirements. For more information, see [CREATE TABLE &#40;Transact-SQL&#41;](../t-sql/statements/create-table-transact-sql.md) and [CREATE INDEX &#40;Transact-SQL&#41;](../t-sql/statements/create-index-transact-sql.md).|
| Buffer pool parallel scan |Improves the performance of Buffer Pool scan operations on large-memory machines by utilizing multiple CPU cores for buffer pool scan operations. |
| Additional optimization | Improvements to string processing for columnstore indexes |
|In-Memory OLTP memory management improvements| - Improve memory management in large memory servers to reduce out of memory conditions.<br/><br/>- Add a new stored procedure to manually release unused memory on demand.|
|System page latch concurrency enhancements| Concurrent global allocation map (GAM) and shared global allocation map (SGAM) updates allows multiple threads updating GAM and SGAM pages under Shared latch.|
|Automatic Degree of parallelism (DOP) feedback |Automatically adjusts degree of parallelism for repeating queries to optimize for workloads where excessive parallelism can cause performance issues. Similar to optimizations in Azure SQL Database. See [Configure the max degree of parallelism (MAXDOP) in Azure SQL Database](/azure/azure-sql/database/configure-max-degree-of-parallelism).|
| Cardinality estimation feedback | Identifies and corrects suboptimal query execution plans for repeating queries, when these issues are caused by incorrect estimation model assumptions. See [Cardinality Estimation (SQL Server)](../relational-databases/performance/cardinality-estimation-sql-server.md). |

> [!NOTE]
> To use Query Store features, enable Query Store. For instructions, see [Enable the Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#enabling).


## Operations

| New feature or update | Details |
|:---|:---|
| Accelerated Database Recovery (ADR) improvement | There are several improvements to address persistent version store (PVS) storage and improve overall scalability. SQL Server 2022 implements a persistent version store cleaner thread per database instead of per instance and the memory footprint for PVS page tracker has been improved. There are also a number of ADR efficiencies. Concurrency improvements helps the cleanup process to work more efficiently, ADR cleans pages that could not previously be cleaned due to locking.|
| Improved Snapshot backup support |  Adds Transact-SQL support for freezing and thawing I/O without requiring a VDI client.
| Setup attached to Azure | Install Azure Arc agent via SQL Server command line setup. For more information, see [Install SQL Server from the Command Prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#install-sql-server-from-the-command-prompt).|
| Optimized plan forcing| Uses compilation replay to improve the compilation time for forced plan generation by pre-caching non-repeatable plan compilation steps. Learn more in [Optimized plan forcing with Query Store](../relational-databases/performance/optimized-plan-forcing-query-store.md).|
| Shrink database & file | Wait at low priority.|
| Max server memory calculations | During setup, the installation will configure max server memory to align with recommendations. See [Server memory configuration options](../database-engine/configure-windows/server-memory-server-configuration-options.md). |

## Language

| New feature or update | Details |
|:---|:---|
|Approximate Percentile functions |- [APPROX_PERCENTILE_CONT (Transact-SQL)](../t-sql/functions/approx-percentile-cont-transact-sql.md)<br/>- [APPROX_PERCENTILE_DISC (Transact-SQL)](../t-sql/functions/approx-percentile-disc-transact-sql.md)|
| CREATE STATISTICS | Adds [AUTO_DROP option](../relational-databases/statistics/statistics.md#auto_drop-option)<br/><br/>Automatic statistics with low priority.|
| Time series functions | You can store and analyze data that changes over time, using time-windowing, aggregation, and filtering capabilities.<br/>- [DATE_BUCKET](../t-sql/functions/date-bucket-transact-sql.md)<br/>- [FIRST_VALUE](../t-sql/functions/first-value-transact-sql.md)<br/>- [GENERATE_SERIES](../t-sql/functions/generate-series-transact-sql.md)<br/>- [LAST_VALUE](../t-sql/functions/last-value-transact-sql.md)
| JSON functions | - [ISJSON (Transact-SQL)](../t-sql/functions/isjson-transact-sql.md)<br/>- [JSON_PATH_EXISTS (Transact-SQL)](../t-sql/functions/json-path-exists-transact-sql.md)- [JSON_OBJECT (Transact-SQL)](../t-sql/functions/json-object-transact-sql.md)- [JSON_ARRAY (Transact-SQL)](../t-sql/functions/json-array-transact-sql.md)
|SELECT ... WINDOW clause | Determines the partitioning and ordering of a rowset before the window function which uses the window in OVER clause is applied. See [SELECT (Transact-SQL)](../t-sql/queries/select-transact-sql.md).|
| Resumable ALTER TABLE ADD CONSTRAINT | Support to pause, and resume a running ADD CONSTRAINT operation to perform it during maintenance windows. Resume such operation after failovers and system failures. Execute such operation on a large table despite the small log size available.
|T-SQL functions | - [GREATEST (Transact-SQL)](../t-sql/functions/logical-functions-greatest-transact-sql.md)<br/>- [LEAST (Transact-SQL)](../t-sql/functions/logical-functions-least-transact-sql.md)<br/>- [STRING_SPLIT (Transact-SQL)](../t-sql/functions/string-split-transact-sql.md).|

## Tools

| New feature or update | Details |
|:---|:---|
|Distributed Replay |  SQL Server setup no longer includes Distributed Replay. It will be available as a download |
| SQL Server Management Studio | |
|Azure Data Studio | |

## Additional information

This section provides additional information for the features highlighted above.

### New granular permissions & roles

This section summarizes the granular permissions and roles that SQL Server 2022 Preview introduces.

#### Granular permissions

- `CREATE LOGIN` and `CREATE USER`
  - Complements the existing `ALTER ANY LOGIN` and the `ALTER ANY USER` permission respectively by permitting only the creation of accounts but not altering (changing) existing users or logins.

- `VIEW SERVER SECURITY STATE` and `VIEW DATABASE SECURITY STATE`
  - All dynamic management views (DMV) in SQL Server are covered by `VIEW SERVER` respectively `VIEW DATABASE STATE`. A subset of DMVs contains information about the security configuration that is not necessary to disclose to for example performance monitoring tasks but would be required to conduct a security auditing. These are covered with this new permission

- `VIEW SERVER PERFORMANCE STATE` and `VIEW DATABASE PERFORMANCE STATE`
  - All DMVs in SQL Server are covered by `VIEW SERVER` respectively `VIEW DATABASE STATE`. A subset of DMVs contains information about the security configuration that is not necessary to disclose to for example performance monitoring tasks. The remaining DMVs are covered with this new permission.
- `VIEW ANY DEFINITION` and `VIEW SECURITY DEFINITION`
  - All catalog views in SQL Server are covered by `VIEW ANY DEFINITION` on server respectively `VIEW DEFINITION` on database. A subset of catalog views contains information about the security configuration that should be limited to a security role. These are covered with this new permission.
- `VIEW ANY CRYPTOGRAPHICALLY SECURED DEFINITION` and `VIEW CRYPTOGRAPHICALLY SECURED DEFINITION` (server and database)
  - There are a small number of columns in catalog views that contain data that is highly sensitive and should not be disclosed to everyone who may otherwise have access to the other columns. Examples: key values and passwords. While none of these are stored anywhere in clear text within SQL Server, having even access to the encrypted or hashed value may be considered too risky to disclose. These new permissions cover access to such data.

#### Roles

- `##MS_DefinitionReader##`
  - Holds `VIEW ANY DEFINITION`
- `##MS_ServerStateManager##`
  - Holds `ALTER SERVER STATE`
- `##MS_ServerStateReader##`
  - Holds `VIEW SERVER STATE`
- `##MS_SecurityDefinitionReader##`
  - Holds `VIEW ANY SECURITY DEFINITION`
- `##MS_DatabaseConnector##`
  - Holds `CONNECT ANY DATABASE`
  - Allows connect to any database on the logical server
  - The ideal counterpart for `##MS_ServerStateReader##,` `##MS_DefinitionReader##` and `##MS_SecurityDefinitionReader##`
- `##MS_DatabaseManager##`
  - Can create and alter any database.
- `##MS_LoginManager##`
  - Can create and alter any login.

### Query Store improvements

Query Store helps you better track performance history, troubleshoot query plan related issues, and enable new capabilities in Azure SQL Database and SQL Server 2022. Additionally, Query Store hints are a preview feature in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]. To use Query Store features, enable Query Store. For instructions, see [Enable the Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#Enabling).

- For databases that have been restored from other SQL Server instances and for those databases that are upgraded from an in-place upgrade to SQL Server 2022, these databases will retain the previous Query Store settings.

- For databases that are restored from previous SQL Server instances it is recommended to [enable Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#Enabling) and separately evaluate the [database compatibility level settings](../t-sql/statements/alter-database-transact-sql-compatibility-level.md) as some Intelligent Query Processing features are enabled by the compatibility level setting.

If there is concern about the overhead Query Store may introduce, administrators can leverage custom capture policies to further tune what the Query Store captures. Custom capture policies are available to help further tune Query Store captures. Custom capture policies can be used to be more selective about which queries, and query details are captured. For example, an administrator may choose to capture only the most expensive queries, repeated queries, or the queries that have a high level of compute overhead. [Custom capture policies](../t-sql/statements/alter-database-transact-sql-set-options.md#query_capture_policy_option_list--) can help Query Store capture the most important queries in your workload. Note that except for the STALE_CAPTURE_POLICY_THRESHOLD option, these options define the OR conditions that need to happen for queries to be captured in the defined Stale Capture Policy Threshold value. For example, these are the default values in the `QUERY_CAPTURE_MODE = AUTO`:

```sql
...
QUERY_CAPTURE_MODE = CUSTOM,
QUERY_CAPTURE_POLICY = ( 
STALE_CAPTURE_POLICY_THRESHOLD = 24 HOURS, 
EXECUTION_COUNT = 30, 
TOTAL_COMPILE_CPU_TIME_MS = 1000, 
TOTAL_EXECUTION_CPU_TIME_MS = 100 
)
...
```

## SQL Server Analysis Services

This release introduces new features and improvements for performance, resource governance, and client support. For specific updates, see [What's new in SQL Server Analysis Services](/analysis-services/what-s-new-in-sql-server-analysis-services).

## See also

- [`SqlServer` PowerShell module](https://www.powershellgallery.com/packages/Sqlserver)
- [SQL Server PowerShell documentation](../powershell/sql-server-powershell.md)
- [SQL Server Workshops](https://aka.ms/sqlworkshops)
- [[!INCLUDE[SQL Server 2022](../includes/sssql22-md.md)] release notes](sql-server-2022-release-notes.md)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
