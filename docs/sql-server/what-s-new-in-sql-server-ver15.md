---
title: "What's new in SQL Server 2019 | Microsoft Docs"
ms.date: 08/28/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# What's new in [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]

[!INCLUDE[tsql-appliesto-ss-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss-xxxx-xxxx-xxx-md.md)]

[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud environments, and operating systems.

This article summarizes the new features and enhancements for [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].

For more information and known issues, see [[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] release notes](sql-server-ver15-release-notes.md).

For the best experience with [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)], use the [latest tools](what-s-new-in-sql-server-ver15-prerelease.md#tools).

[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] introduces [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)] for [!INCLUDE[sql-server](../includes/ssnoversion-md.md)]. It also provides additional capability and improvements for the SQL Server database engine, SQL Server Analysis Services, SQL Server Machine Learning Services, SQL Server on Linux, and SQL Server Master Data Services.

The following sections provide an overview of these features.

## Data virtualization and [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]

Businesses today often preside over vast data estates consisting of a wide array of ever-growing data sets that are hosted in siloed data sources across the company. Gain near real-time insights from all your data with SQL Server 2019 [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)], which provide a complete environment for working with large sets of data, including machine learning and AI capabilities.

| New feature or update | Details |
|:---|:---|
| Scalable big data solution | [Deploy scalable clusters](../big-data-cluster/deploy-get-started.md) of SQL Server, Spark, and HDFS containers running on Kubernetes. <br/><br/> Read, write, and process big data from Transact-SQL or Spark.<br/><br/> Easily combine and analyze high-value relational data with high-volume big data.<br/><br/>Query external data sources.<br/><br/>Store big data in HDFS managed by SQL Server.<br/><br/>Query data from multiple external data sources through the cluster.<br/><br/> Use the data for AI, machine learning, and other analysis tasks.<br/><br/> [Deploy and run applications](../big-data-cluster/concept-application-deployment.md) in [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)]. <br/><br/> The SQL Server master instance provides high availability and disaster recovery for all databases by using Always On availability group technology.<br/>|
|Data virtualization with PolyBase | Query data from external SQL Server, Oracle, Teradata, MongoDB, and ODBC data sources with external tables, now with [UTF-8 encoding support](../relational-databases/collations/collation-and-unicode-support.md). For more information, see [What is PolyBase?](../relational-databases/polybase/polybase-guide.md).|
| &nbsp; | &nbsp; |

For more information, see [What are SQL Server [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)]?](../big-data-cluster/big-data-cluster-overview.md).

[[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] (CTP) announcement archive](what-s-new-in-sql-server-ver15-prerelease.md) contains a list of features that have been announced and changed for all previous CTP releases of this feature.

## Intelligent database
[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] builds on innovations in previous versions to provide industry-leading performance out of the box. From [Intelligent query processing](../relational-databases/performance/intelligent-query-processing.md) to support for persistent memory devices, the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] intelligent database features improve performance and scalability of all your database workloads without any changes to your application or database design.

### Intelligent query processing
With [intelligent query processing](../relational-databases/performance/intelligent-query-processing.md), you know that critical parallel workloads improve when they're running at scale. At the same time, they remain adaptive to the constantly changing world of data. Intelligent query processing is available by default on the latest [database compatibility level](../t-sql/statements/alter-database-transact-sql-compatibility-level.md#differences-between-compatibility-level-140-and-level-150) setting, delivering broad impact that improves the performance of existing workloads with minimal implementation effort.

|New feature or update | Details |
|:---|:---|
|Row mode memory grant feedback |Expands on the batch mode memory grant feedback feature by adjusting memory grant sizes for both batch and row mode operators. This adjustment can automatically correct excessive grants, which result in wasted memory and reduced concurrency. It can also correct insufficient memory grants that cause expensive spills to disk. See [Row mode memory grant feedback](../relational-databases/performance/intelligent-query-processing.md#row-mode-memory-grant-feedback). |
|Batch mode on rowstore | Enables batch mode execution without requiring columnstore indexes. Batch mode execution uses CPU more efficiently during analytical workloads but, until [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)], it was used only when a query included operations with columnstore indexes. However, some applications might use features that aren't supported with columnstore indexes and, therefore, can't leverage batch mode. Starting with [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)], batch mode is enabled on eligible analytical workloads whose queries include operations with any type of index (rowstore or columnstore). See [Batch mode on rowstore](../relational-databases/performance/intelligent-query-processing.md#batch-mode-on-rowstore). |
|Scalar UDF Inlining|Automatically transforms scalar UDFs into relational expressions and embeds them in the calling SQL query. This transformation improves the performance of workloads that take advantage of scalar UDFs. See [Scalar UDF Inlining](../relational-databases/performance/intelligent-query-processing.md#scalar-udf-inlining).|
|Table variable deferred compilation|Improves plan quality and overall performance for queries that reference table variables. During optimization and initial compilation, this feature propagates cardinality estimates that are based on actual table variable row counts. This accurate row count information optimizes downstream plan operations. See [Table variable deferred compilation](../relational-databases/performance/intelligent-query-processing.md#table-variable-deferred-compilation). |
|Approximate query processing with `APPROX_COUNT_DISTINCT `|For scenarios when absolute precision isn't important but responsiveness is critical, `APPROX_COUNT_DISTINCT` aggregates across large datasets by using fewer resources than `COUNT(DISTINCT())` for superior concurrency. See [Approximate query processing](../relational-databases/performance/intelligent-query-processing.md#approximate-query-processing).|
| &nbsp; | &nbsp; |


### In-memory database
[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [In-memory database](../relational-databases/in-memory-database.md) technologies leverage modern hardware innovation to deliver unparalleled performance and scale. [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] builds on earlier innovations in this area, such as in-memory online transaction processing (OLTP), to unlock a new level of scalability across all your database workloads.  

|New feature or update | Details |
|:---|:---|
|Hybrid buffer pool| A new feature of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)], where database pages sitting on database files that are placed on a persistent memory (PMEM) device are directly accessed when required. See [Hybrid buffer pool](../database-engine/configure-windows/hybrid-buffer-pool.md).|
|Memory-optimized `tempdb` metadata| [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] introduces a new feature that is part of the [in-memory database](../relational-databases/in-memory-database.md) feature family, memory-optimized `tempdb` metadata, which effectively removes this bottleneck and unlocks a new level of scalability for `tempdb` heavy workloads. In [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)], the system tables involved in managing temp table metadata can be moved into latch-free, non-durable, memory-optimized tables. See [Memory-optimized `tempdb` metadata](../relational-databases/databases/tempdb-database.md#memory-optimized-tempdb-metadata).|
| In-memory OLTP support for database snapshots | [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] introduces support for creating [database snapshots](../relational-databases/databases/database-snapshots-sql-server.md) of databases that include memory-optimized filegroups. |
| &nbsp; | &nbsp; |

### Intelligent performance
[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] builds on intelligent database innovations in previous releases to ensure that [It Just Runs Faster](https://blogs.msdn.microsoft.com/bobsql/tag/it-just-runs-faster/). These improvements help overcome known resource bottlenecks and provide options for configuring your database server to provide predictable performance across all your workloads.

|New feature or update | Details |
|:---|:---|
|`OPTIMIZE_FOR_SEQUENTIAL_KEY`|Turns on an optimization within the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] that helps improve throughput for high-concurrency inserts into the index. This option is intended for indexes that are prone to last-page insert contention, which is typically seen with indexes that have a sequential key, such as an identity column, sequence, or date/time column. See [CREATE INDEX](../t-sql/statements/create-index-transact-sql.md#sequential-keys).|
|Forcing fast forward and static cursors | A Query Store plan that forces support for fast forward and static cursors. See [Plan forcing support for fast forward and static cursors](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#ctp23).|
|Resource governance| The configurable value for the `REQUEST_MAX_MEMORY_GRANT_PERCENT` option of `CREATE WORKLOAD GROUP` and `ALTER WORKLOAD GROUP` has been changed from an integer to a float data type, to allow more granular control of memory limits. See [ALTER WORKLOAD GROUP](../t-sql/statements/alter-workload-group-transact-sql.md) and [CREATE WORKLOAD GROUP](../t-sql/statements/create-workload-group-transact-sql.md).|
|Reduced recompilations for workloads| Improves the use of temporary tables across multiple scopes. See [Reduced recompilations for workloads](../relational-databases/tables/tables.md#ctp23). |
|Indirect checkpoint scalability |See [Improved indirect checkpoint scalability](../relational-databases/logs/database-checkpoints-sql-server.md#ctp23).|
|Concurrent PFS updates|[Page Free Space (PFS) pages](https://techcommunity.microsoft.com/t5/SQL-Server/Under-the-covers-GAM-SGAM-and-PFS-pages/ba-p/383125) are special pages within a database file that SQL Server uses to help locate free space when it allocates space for an object. Page latch contention on PFS pages is commonly associated with [`tempdb`](https://support.microsoft.com/en-us/help/2154845/recommendations-to-reduce-allocation-contention-in-sql-server-tempdb-d), but it can also occur on user databases when there are many concurrent object allocation threads. This improvement changes the way that concurrency is managed with PFS updates so that they can be updated under a shared latch, rather than an exclusive latch. This behavior is on by default in all databases (including `tempdb`) starting with [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].|
| &nbsp; | &nbsp; |

### Improvements in monitoring
Monitoring improvements unlock performance insights over any database workload, just when you need them.

|New feature or update | Details |
|:---|:---|
|`WAIT_ON_SYNC_STATISTICS_REFRESH` | A new wait type in `sys.dm_os_wait_stats` dynamic management view. It shows the accumulated instance-level time spent on synchronous statistics refresh operations. See [`sys.dm_os_wait_stats`](../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md).|
|Custom capture policy for Query Store|When they're enabled, additional Query Store configurations are available under a new Query Store Capture Policy setting, to fine-tune data collection in a specific server. See [ALTER DATABASE SET options](../t-sql/statements/alter-database-transact-sql-set-options.md).|
|`LIGHTWEIGHT_QUERY_PROFILING`| A new database scoped configuration. See [`LIGHTWEIGHT_QUERY_PROFILING`](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#lqp). |
|`sys.dm_exec_requests` column `command` | Shows `SELECT (STATMAN)` if a `SELECT` is waiting for a synchronous statistics update operation to finish before it continues the query execution. See [`sys.dm_exec_requests`](../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md).|
|`sys.dm_exec_query_plan_stats` | A new dynamic management function (DMF) that returns the equivalent of the last known actual execution plan for all queries. See [sys.dm_exec_query_plan_stats](../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md).|
|`LAST_QUERY_PLAN_STATS` | A new database-scoped configuration that enables `sys.dm_exec_query_plan_stats`. See [ALTER DATABASE SCOPED CONFIGURATION](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).|
|`query_post_execution_plan_profile` | An extended event that collects the equivalent of an actual execution plan that's based on lightweight profiling, unlike `query_post_execution_showplan`, which uses standard profiling. See [Query profiling infrastructure](../relational-databases/performance/query-profiling-infrastructure.md).|
|`sys.dm_db_page_info(database_id, file_id, page_id, mode)` | A new DMF that returns information about a page in a database. See [sys.dm_db_page_info (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-page-info-transact-sql.md).|
| &nbsp; | &nbsp; |

## Developer experience
[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] continues to provide a world-class developer experience with enhancements to graph and spatial data types, UTF-8 support, and a new extensibility framework that allows developers to use the language of their choice to gain insights across all their data.

### Graph

|New feature or update | Details |
|:---|:---|
|Edge constraint cascade delete actions | Defines cascaded delete actions on an edge constraint in a graph database. See [Edge constraints](../relational-databases/tables/graph-edge-constraints.md). |
|New graph function - `SHORTEST_PATH` | Uses `SHORTEST_PATH` inside `MATCH` to find the shortest path between any two nodes in a graph or to perform arbitrary length traversals.|
|Partition tables and indexes| Divides the data of partitioned tables and indexes into units that can be spread across more than one filegroup in a graph database. |
|Use derived table or view aliases in graph match query |See [Graph match query](../t-sql/queries/match-sql-graph.md). |
| &nbsp; | &nbsp; |

### Unicode support
Support businesses across different countries and regions, where the requirement of providing global multilingual database applications and services is critical to meeting customer demands and complying with specific market regulations. 

|New feature or update | Details |
|:---|:---|
|Support for UTF-8 character encoding |Supports the UTF-8 protocol for import and export encoding, and as database-level or column-level collation for string data. Support includes PolyBase external tables, and Always Encrypted. See [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).|
| &nbsp; | &nbsp; |

### Language extensions

|New feature or update | Details |
|:---|:---|
|New Java language SDK | Simplifies the development of Java programs that can be run from SQL Server. See [Microsoft Extensibility SDK for Java for SQL Server](../language-extensions/how-to/extensibility-sdk-java-sql-server.md). |
|Java language SDK is open source |The [Microsoft Extensibility SDK for Java for Microsoft SQL Server](https://docs.microsoft.com/sql/language-extensions/how-to/extensibility-sdk-java-sql-server) is now open source and [available on GitHub](https://github.com/microsoft/sql-server-language-extensions).|
|Support for Java data types|See [Java data types](../language-extensions/how-to/java-to-sql-data-types.md).|
|New default Java Runtime | SQL Server now includes Azul Systems Zulu Embedded for Java support throughout the product. See [Free supported Java in SQL Server 2019 is now available](https://cloudblogs.microsoft.com/sqlserver/2019/07/24/free-supported-java-in-sql-server-2019-is-now-available/). |
|SQL Server language extensions| Execute external code with the extensibility framework. See [SQL Server language extensions](https://docs.microsoft.com/sql/language-extensions/language-extensions-overview).
|Register external languages|A new Data Definition Language (DDL), `CREATE EXTERNAL LANGUAGE`, registers external languages, such as Java, in SQL Server. See [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). |
| &nbsp; | &nbsp; |

### Spatial

|New feature or update | Details |
|:---|:---|
| New spatial reference identifiers (SRIDs) |[Australian GDA2020](http://www.ga.gov.au/scientific-topics/positioning-navigation/geodesy/datums-projections/gda2020) provides more robust and accurate data, which is more closely aligned with global positioning systems. The new SRIDs are:<ul><li>7843 for geographic 2D</li><li>7844 for geographic 3D</li></ul> For definitions of new SRIDs, see [sys.spatial_reference_systems](../relational-databases/system-catalog-views/sys-spatial-reference-systems-transact-sql.md) view. |
| &nbsp; | &nbsp; |

### Error messages
When an extract, transform, and load (ETL) process fails because the source and the destination don't have matching data types or length, troubleshooting used to be time-consuming, especially in large datasets. [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] allows faster insights into data truncation errors.

|New feature or update | Details |
|:---|:---|
|Verbose truncation warnings | A truncation error message defaults to include table and column names, and truncated value. See [VERBOSE_TRUNCATION_WARNINGS](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#verbose-truncation).|
| &nbsp; | &nbsp; |

## Mission-critical security
[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provides a security architecture that's designed to allow database administrators and developers to create secure database applications and counter threats. Each version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has improved on previous versions with the introduction of new features and functionality, and [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] continues to build on this story.

|New feature or update | Details |
|:---|:---|
|Always Encrypted with secure enclaves|Expands upon Always Encrypted with in-place encryption and rich computations by enabling computations on plaintext data inside a server-side secure enclave. In-place encryption improves the performance and the reliability of cryptographic operations (encrypting columns, rotating columns, encryption keys, and so on), because it avoids moving data out of the database.<br><br> Support for rich computations (pattern matching and comparison operations) unlocks Always Encrypted to a much broader set of scenarios and applications that demand sensitive data protection, while also requiring richer functionality in Transact-SQL queries. See [Always Encrypted with Secure Enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md).|
|Certificate management in SQL Server Configuration Manager|See [Certificate Management (SQL Server Configuration Manager)](../database-engine/configure-windows/manage-certificates.md).|
| &nbsp; | &nbsp; |

## High availability
One common task that everyone who deploys [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has to account for is making sure that all mission critical [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances and the databases within them are available whenever the business and end users need them. Availability is a key pillar of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] platform, and [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] introduces many new features and enhancements that allow businesses to ensure that their database environments are highly available.

### Availability groups

|New feature or update | Details |
|:---|:---|
|Up to five synchronous replicas|[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] increases the maximum number of synchronous replicas to five, up from three in [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. You can configure this group of five replicas to have automatic failover within the group. There is one primary replica, plus four synchronous secondary replicas.|
|Secondary-to-primary replica connection redirection| Allows client application connections to be directed to the primary replica regardless of the target server that's specified in the connection string. See [Secondary-to-primary replica read/write connection redirection (Always On availability groups)](../database-engine/availability-groups/windows/secondary-replica-connection-redirection-always-on-availability-groups.md).|
| &nbsp; | &nbsp; |

### Recovery

|New feature or update | Details |
|:---|:---|
|Accelerated database recovery | Enables accelerated database recovery per-database. See [Accelerated database recovery](../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md#adr).|
| &nbsp; | &nbsp; |

### Resumable operations

|New feature or update | Details |
|:---|:---|
|Online clustered columnstore index build and rebuild | See [Perform Index Operations Online](../relational-databases/indexes/perform-index-operations-online.md). |
|Resumable online rowstore index build | See [Perform Index Operations Online](../relational-databases/indexes/perform-index-operations-online.md). |
|Suspend and resume initial scan for Transparent Data Encryption (TDE)|See [Transparent Data Encryption (TDE) scan - suspend and resume](../relational-databases/security/encryption/transparent-data-encryption.md#scan-suspend-resume).|
| &nbsp; | &nbsp; |

## Platform choice
[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] builds on the innovations that were introduced in [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] to allow you to run [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on your platform of choice with more functionality and security than ever before.

### <a id="sql-server-on-linux"></a>Linux

| New feature or update | Details |
|:-----|:-----|
|Replication support | See [SQL Server Replication on Linux](../linux/sql-server-linux-replication.md).
|Support for the Microsoft Distributed Transaction Coordinator (MSDTC) | See [How to configure MSDTC on Linux](../linux/sql-server-linux-configure-msdtc.md). |
|OpenLDAP support for third-party AD providers | See [Tutorial: Use Active Directory authentication with SQL Server on Linux](../linux/sql-server-linux-active-directory-authentication.md). |
|Machine learning on Linux | See [Configure machine learning on Linux](../linux/sql-server-linux-setup-machine-learning.md). |
|`tempdb` improvements | By default, a new installation of SQL Server on Linux creates multiple `tempdb` data files, based on the number of logical cores (with up to eight data files). This doesn't apply to in-place minor or major version upgrades. Each `tempdb` file is 8 MB with an auto growth of 64 MB. This behavior is similar to the default SQL Server installation on Windows. |
| PolyBase on Linux | See [Install PolyBase](../relational-databases/polybase/polybase-linux-setup.md) on Linux for non-Hadoop connectors.<br/><br/>See [PolyBase type mapping](../relational-databases/polybase/polybase-type-mapping.md). |
| Change Data Capture (CDC) support | Change Data Capture (CDC) is now supported on Linux for [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]. |
| &nbsp; | &nbsp; |

### Containers
The easiest way to get started working with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is to use containers. [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] builds on the innovations introduced in earlier versions to enable you to deploy [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] containers on new platforms, in a safer manner, and with more functionality.

|New feature or update | Details |
|:---|:---|
| Microsoft Container Registry | The [Microsoft Container Registry](https://azure.microsoft.com/blog/microsoft-syndicates-container-catalog/) now replaces Docker Hub for new official Microsoft container images, including [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]. |
| Non-root containers | [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] introduces the ability to create safer containers by starting the [!INCLUDE[sql-server](../includes/ssnoversion-md.md)] process as a non-root user by default. See [build and run SQL Server containers as a non-root user](../linux/sql-server-linux-configure-docker.md#buildnonrootcontainer). |
| Red Hat certified container images | Starting with [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)], you can run SQL Server containers on Red Hat Enterprise Linux. |
| PolyBase and machine learning support| [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] introduces new ways to work with SQL Server Containers such as Machine Learning Services and PolyBase. Check out some examples in the [SQL Server in container GitHub repository](https://github.com/microsoft/mssql-docker/tree/master/linux/preview/examples). |
| &nbsp; | &nbsp; |

## Setup options

|New feature or update | Details |
|:---|:---| 
|New memory setup options | Sets the *min server memory (MB)* and *max server memory (MB)* server configurations during installation. See [Database Engine Configuration - Memory page](https://docs.microsoft.com/sql/sql-server/install/instance-configuration?view=sql-server-ver15#memory) and the `USESQLRECOMMENDEDMEMORYLIMITS`, `SQLMINMEMORY`, and `SQLMAXMEMORY` parameters in [Install SQL Server from the Command Prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#Install). The proposed value aligns with the memory configuration guidelines in [Server Memory Configuration Options](../database-engine/configure-windows/server-memory-server-configuration-options.md#setting-the-memory-options-manually).| 
|New parallelism setup options | Sets the *max degree of parallelism* server configuration during installation. See [Database Engine Configuration - MaxDOP page](https://docs.microsoft.com/sql/sql-server/install/instance-configuration?view=sql-server-ver15#maxdop) and the `SQLMAXDOP` parameter in [Install SQL Server from the Command Prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#Install). The default value aligns with the maximum degree of parallelism guidelines in [Configure the max degree of parallelism Server Configuration Option](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#Guidelines).| 
| &nbsp; | &nbsp; |

## <a id="ml"></a> SQL Server Machine Learning Services

|New feature or update | Details |
|:---|:---|
|Partition-based modeling|Processes external scripts per partition of your data by using the new parameters added to `sp_execute_external_script`. This functionality supports training many small models (one model per partition of data) instead of one large model. See [Create partition-based models](../advanced-analytics/tutorials/r-tutorial-create-models-per-partition.md).|
|Windows Server Failover Cluster| Configures high availability for Machine Learning Services on a Windows Server Failover Cluster.|
| &nbsp; | &nbsp; |

## [!INCLUDE[master-data-services](../includes/ssmdsshort-md.md)]

| New feature or update | Details |
|:---|:---|
|Support for Azure SQL Database managed-instance databases| Host [!INCLUDE[master-data-services](../includes/ssmdsshort-md.md)] on a managed instance. See [[!INCLUDE[master-data-services](../includes/ssmdsshort-md.md)] installation and configuration](../master-data-services/master-data-services-installation-and-configuration.md#SetUpWeb).|
|New HTML controls| HTML controls replace all former Silverlight components. Silverlight dependency removed.|
| &nbsp; | &nbsp; |

## SQL Server Analysis Services

| New feature or update | Details |
|:---|:---|
|Query interleaving| See [Query interleaving](https://docs.microsoft.com/analysis-services/tabular-models/query-interleaving). |
|Multidimensional expression (MDX) query support for tabular models with calculation groups | See [Calculation groups](what-s-new-in-sql-server-ver15-prerelease.md#calc-ctp24). |
|Calculation groups in tabular model| See [Calculation groups in tabular model](what-s-new-in-sql-server-ver15-prerelease.md#calc-ctp24). |
|MDX query support for tabular models with calculation groups | See [Calculation groups](what-s-new-in-sql-server-ver15-prerelease.md#calc-ctp24). |
|Dynamic formatting of measures using calculation groups |Allows you to conditionally change format strings for measures with [calculation groups](what-s-new-in-sql-server-ver15-prerelease.md#calc-ctp24). For example, with currency conversion, you can display a measure by using a variety of foreign currency formats.|
|Many-to-many relationships in tabular models| See [Many-to-many relationships in tabular models](what-s-new-in-sql-server-ver15-prerelease.md#many-to-many-ctp24).|
|Property settings for resource governance| See [Property settings for resource governance](what-s-new-in-sql-server-ver15-prerelease.md#property-ctp24).|
| Governance setting for Power BI cache refreshes.  | The Power BI service caches dashboard tile data and report data for initial load of Live Connect report, causing an excessive number of cache queries being submitted to SSAS, and in extreme cases overload the server. This release introduces the **ClientCacheRefreshPolicy** property, which allows you to override this behavior at the server level. To learn more, see [General Properties](https://docs.microsoft.com/analysis-services/server-properties/general-properties). |
| Online attach  | Allows you to attach a tabular model as an online operation. Online attach can be used for synchronization of read-only replicas in on-premises query scale-out environments. To learn more, see [Online attach](what-s-new-in-sql-server-ver15-prerelease.md#online-attach-ctp32). |
| &nbsp; | &nbsp; |

## See also

- [`SqlServer` PowerShell module](https://www.powershellgallery.com/packages/Sqlserver)
- [SQL Server PowerShell documentation](../powershell/sql-server-powershell.md)

## Next steps

- See [[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] release notes](sql-server-ver15-release-notes.md).
- [Microsoft [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]: Technical white paper](http://info.microsoft.com/rs/157-GQE-382/images/EN-US-CNTNT-white-paper-DBMod-Microsoft-SQL-Server-2019-Technical-white-paper.pdf)<br>Published September 2018. Applies to Microsoft [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] CTP 2.0 for Windows, Linux, and Docker containers.

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
