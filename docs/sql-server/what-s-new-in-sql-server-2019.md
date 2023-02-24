---
title: "What's new in SQL Server 2019"
description: Learn about new features for SQL Server 2019 (15.x), which gives you choices of development languages, data types, environments, and operating systems.
ms.date: 07/22/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: release-landing
ms.topic: "article"
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">= sql-server-2016"
ms.custom:
  - intro-whats-new
---
# What's new in [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)]

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud environments, and operating systems.

This article summarizes the new features and enhancements for [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)].

For more information and known issues, see [[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] release notes](sql-server-version-15-release-notes.md).

For the best experience with [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)], use the [latest tools](../azure-data-studio/download-azure-data-studio.md).

[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] introduces [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)] for [!INCLUDE[sql-server](../includes/ssnoversion-md.md)]. It also provides additional capability and improvements for the SQL Server database engine, SQL Server Analysis Services, SQL Server Machine Learning Services, SQL Server on Linux, and SQL Server Master Data Services.

The following video provides a 13-minute introduction into SQL Server 2019:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-SQL-Server-2019/player?WT.mc_id=dataexposed-c9-niner]

The following sections provide an overview of these features.

## Data virtualization and [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]

Businesses today often preside over vast data estates consisting of a wide array of ever-growing data sets that are hosted in siloed data sources across the company. Gain near real-time insights from all your data with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)], which provide a complete environment for working with large sets of data, including machine learning and AI capabilities.

| New feature or update | Details |
|:---|:---|
| Scalable big data solution | [Deploy scalable clusters](../big-data-cluster/deploy-get-started.md) of SQL Server, Spark, and HDFS containers running on Kubernetes. <br/><br/> Read, write, and process big data from Transact-SQL or Spark.<br/><br/> Easily combine and analyze high-value relational data with high-volume big data.<br/><br/>Query external data sources.<br/><br/>Store big data in HDFS managed by SQL Server.<br/><br/>Query data from multiple external data sources through the cluster.<br/><br/> Use the data for AI, machine learning, and other analysis tasks.<br/><br/> [Deploy and run applications](../big-data-cluster/concept-application-deployment.md) in [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)]. <br/><br/> The SQL Server master instance provides high availability and disaster recovery for all databases by using Always On availability group technology.<br/>|
|Data virtualization with PolyBase | Query data from external SQL Server, Oracle, Teradata, MongoDB, and ODBC data sources with external tables, now with [UTF-8 encoding support](../relational-databases/collations/collation-and-unicode-support.md). For more information, see [What is PolyBase?](../relational-databases/polybase/polybase-guide.md).|

For more information, see [What are SQL Server [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)]?](../big-data-cluster/big-data-cluster-overview.md).

## Intelligent Database

[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] builds on innovations in previous versions to provide industry-leading performance out of the box. From [Intelligent Query Processing](../relational-databases/performance/intelligent-query-processing.md) to support for persistent memory devices, the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Intelligent Database features improve performance and scalability of all your database workloads without any changes to your application or database design.

### Intelligent Query Processing

With [Intelligent Query Processing](../relational-databases/performance/intelligent-query-processing.md), you know that critical parallel workloads improve when they're running at scale. At the same time, they remain adaptive to the constantly changing world of data. Intelligent Query Processing is available by default on the latest [database compatibility level](../t-sql/statements/alter-database-transact-sql-compatibility-level.md#differences-between-compatibility-level-140-and-level-150) setting, delivering broad impact that improves the performance of existing workloads with minimal implementation effort.

|New feature or update | Details |
|:---|:---|
|Row mode memory grant feedback |Expands on the batch mode memory grant feedback feature by adjusting memory grant sizes for both batch and row mode operators. This adjustment can automatically correct excessive grants, which result in wasted memory and reduced concurrency. It can also correct insufficient memory grants that cause expensive spills to disk. See [Row mode memory grant feedback](../relational-databases/performance/intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback). |
|Batch mode on rowstore | Enables batch mode execution without requiring columnstore indexes. Batch mode execution uses CPU more efficiently during analytical workloads but, until [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)], it was used only when a query included operations with columnstore indexes. However, some applications might use features that aren't supported with columnstore indexes and, therefore, can't leverage batch mode. Starting with [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)], batch mode is enabled on eligible analytical workloads whose queries include operations with any type of index (rowstore or columnstore). See [Batch mode on rowstore](../relational-databases/performance/intelligent-query-processing-details.md#batch-mode-on-rowstore). |
|Scalar UDF Inlining|Automatically transforms scalar UDFs into relational expressions and embeds them in the calling SQL query. This transformation improves the performance of workloads that take advantage of scalar UDFs. See [Scalar UDF Inlining](../relational-databases/performance/intelligent-query-processing-details.md#scalar-udf-inlining).|
|Table variable deferred compilation|Improves plan quality and overall performance for queries that reference table variables. During optimization and initial compilation, this feature propagates cardinality estimates that are based on actual table variable row counts. This accurate row count information optimizes downstream plan operations. See [Table variable deferred compilation](../relational-databases/performance/intelligent-query-processing-details.md#table-variable-deferred-compilation). |
|Approximate query processing with `APPROX_COUNT_DISTINCT` |For scenarios when absolute precision isn't important but responsiveness is critical, `APPROX_COUNT_DISTINCT` aggregates across large datasets while using fewer resources than `COUNT(DISTINCT())` for superior concurrency. See [Approximate query processing](../relational-databases/performance/intelligent-query-processing-details.md#approximate-query-processing).|


### In-Memory Database
[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [In-Memory Database](../relational-databases/in-memory-database.md) technologies leverage modern hardware innovation to deliver unparalleled performance and scale. [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] builds on earlier innovations in this area, such as in-memory online transaction processing (OLTP), to unlock a new level of scalability across all your database workloads.  

|New feature or update | Details |
|:---|:---|
|Hybrid buffer pool| New feature of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] where database pages sitting on database files placed on a persistent memory (PMEM) device will be directly accessed when required. See [Hybrid buffer pool](../database-engine/configure-windows/hybrid-buffer-pool.md).|
|Memory-optimized TempDB metadata| [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] introduces a new feature that is part of the [In-Memory Database](../relational-databases/in-memory-database.md) feature family, memory-optimized TempDB metadata, which effectively removes this bottleneck and unlocks a new level of scalability for TempDB heavy workloads. In [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)], the system tables involved in managing temporary table metadata can be moved into latch-free non-durable memory-optimized tables. See [Memory-Optimized TempDB Metadata](../relational-databases/databases/tempdb-database.md#memory-optimized-tempdb-metadata).|
| In-Memory OLTP support for Database Snapshots | [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] introduces support for creating [Database Snapshots](../relational-databases/databases/database-snapshots-sql-server.md) of databases that include memory-optimized filegroups. |

### Intelligent performance
[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] builds on Intelligent Database innovations in previous releases to ensure that [it just runs faster](/archive/blogs/bobsql/). These improvements help overcome known resource bottlenecks and provide options for configuring your database server to provide predictable performance across all your workloads.

|New feature or update | Details |
|:---|:---|
|`OPTIMIZE_FOR_SEQUENTIAL_KEY`|Turns on an optimization within the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] that helps improve throughput for high-concurrency inserts into the index. This option is intended for indexes that are prone to last-page insert contention, which is typically seen with indexes that have a sequential key, such as an identity column, sequence, or date/time column. See [CREATE INDEX](../t-sql/statements/create-index-transact-sql.md#sequential-keys).|
|Forcing fast forward and static cursors | Provides Query Store plan forcing support for fast forward and static cursors. See [Plan forcing support for fast forward and static cursors](../relational-databases/performance/tune-performance-with-the-query-store.md#ctp23).|
|Resource governance| The configurable value for the `REQUEST_MAX_MEMORY_GRANT_PERCENT` option of `CREATE WORKLOAD GROUP` and `ALTER WORKLOAD GROUP` has been changed from an integer to a float data type, to allow more granular control of memory limits. See [ALTER WORKLOAD GROUP](../t-sql/statements/alter-workload-group-transact-sql.md) and [CREATE WORKLOAD GROUP](../t-sql/statements/create-workload-group-transact-sql.md).|
|Reduced recompilations for workloads| Improves performance when using temporary tables across multiple scopes by reducing unnecessary recompilations. See [Reduced recompilations for workloads](../relational-databases/tables/tables.md#ctp23). |
|Indirect checkpoint scalability |See [Improved indirect checkpoint scalability](../relational-databases/logs/database-checkpoints-sql-server.md#improved-indirect-checkpoint-scalability).|
|Concurrent PFS updates|[Page Free Space (PFS) pages](https://techcommunity.microsoft.com/t5/SQL-Server/Under-the-covers-GAM-SGAM-and-PFS-pages/ba-p/383125) are special pages within a database file that SQL Server uses to help locate free space when it allocates space for an object. Page latch contention on PFS pages is commonly associated with [TempDB](https://support.microsoft.com/help/2154845/recommendations-to-reduce-allocation-contention-in-sql-server-tempdb-d), but it can also occur on user databases when there are many concurrent object allocation threads. This improvement changes the way that concurrency is managed with PFS updates so that they can be updated under a shared latch, rather than an exclusive latch. This behavior is on by default in all databases (including TempDB) starting with [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)].|
|Scheduler worker migration |Worker migration allows an idle scheduler to migrate a worker from the runnable queue of another scheduler on the same NUMA node and immediately resume the task of the migrated worker. This enhancement provides more balanced CPU usage in situations where long-running tasks happen to be assigned to the same scheduler. See [SQL Server 2019 Intelligent Performance - Worker Migration](https://techcommunity.microsoft.com/t5/SQL-Server/SQL-Server-2019-Intelligent-Performance-Worker-Migration/ba-p/939610) for more information. |

### Monitoring
Monitoring improvements unlock performance insights over any database workload, just when you need them.

|New feature or update | Details |
|:---|:---|
|`WAIT_ON_SYNC_STATISTICS_REFRESH` | A new wait type in `sys.dm_os_wait_stats` dynamic management view. It shows the accumulated instance-level time spent on synchronous statistics refresh operations. See [`sys.dm_os_wait_stats`](../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md).|
|Custom capture policy for Query Store | When this policy is enabled, additional Query Store configurations are available under a new Query Store Capture Policy setting, to fine-tune data collection in a specific server. See [ALTER DATABASE SET options](../t-sql/statements/alter-database-transact-sql-set-options.md).|
|`LIGHTWEIGHT_QUERY_PROFILING`| A new database scoped configuration. See [`LIGHTWEIGHT_QUERY_PROFILING`](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#lqp). |
|`sys.dm_exec_requests` column `command` | Shows `SELECT (STATMAN)` if a `SELECT` is waiting for a synchronous statistics update operation to finish before it continues the query execution. See [`sys.dm_exec_requests`](../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md).|
|`sys.dm_exec_query_plan_stats` | A new dynamic management function (DMF) that returns the equivalent of the last known actual execution plan for all queries. See [sys.dm_exec_query_plan_stats](../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md).|
|`LAST_QUERY_PLAN_STATS` | A new database-scoped configuration that enables `sys.dm_exec_query_plan_stats`. See [ALTER DATABASE SCOPED CONFIGURATION](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).|
|`query_post_execution_plan_profile` | An extended event that collects the equivalent of an actual execution plan that's based on lightweight profiling, unlike `query_post_execution_showplan`, which uses standard profiling. See [Query profiling infrastructure](../relational-databases/performance/query-profiling-infrastructure.md).|
|`sys.dm_db_page_info(database_id, file_id, page_id, mode)` | A new DMF that returns information about a page in a database. See [sys.dm_db_page_info (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-page-info-transact-sql.md).|

## Developer experience
[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] continues to provide a world-class developer experience with enhancements to graph and spatial data types, UTF-8 support, and a new extensibility framework that allows developers to use the language of their choice to gain insights across all their data.

### Graph

|New feature or update | Details |
|:---|:---|
|Edge constraint cascade delete actions | You can now define cascaded delete actions on an edge constraint in a graph database. See [Edge constraints](../relational-databases/tables/graph-edge-constraints.md). |
|New graph function - `SHORTEST_PATH` | You can now use `SHORTEST_PATH` inside `MATCH` to find the shortest path between any two nodes in a graph or to perform arbitrary length traversals.|
|Partition tables and indexes| Graph tables now support table and index partitioning. |
|Use derived table or view aliases in graph match query |See [Graph match query](../t-sql/queries/match-sql-graph.md). |

### Unicode support
Support businesses across different countries and regions, where the requirement of providing global multilingual database applications and services is critical to meeting customer demands and complying with specific market regulations. 

|New feature or update | Details |
|:---|:---|
|Support for UTF-8 character encoding |Supports UTF-8 for import and export encoding, and as database-level or column-level collation for string data. Support includes PolyBase external tables, and Always Encrypted (when not used with Enclaves). See [Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md).|

### Language extensions

|New feature or update | Details |
|:---|:---|
|New Java language SDK | Simplifies the development of Java programs that can be run from SQL Server. See [Microsoft Extensibility SDK for Java for SQL Server](../language-extensions/how-to/extensibility-sdk-java-sql-server.md). |
|Java language SDK is open source |The [Microsoft Extensibility SDK for Java for Microsoft SQL Server](../language-extensions/how-to/extensibility-sdk-java-sql-server.md) is now open source and [available on GitHub](https://github.com/microsoft/sql-server-language-extensions).|
|Support for Java data types|See [Java data types](../language-extensions/how-to/java-to-sql-data-types.md).|
|New default Java Runtime | SQL Server now includes Azul Systems Zulu Embedded for Java support throughout the product. See [Free supported Java in SQL Server 2019 is now available](https://cloudblogs.microsoft.com/sqlserver/2019/07/24/free-supported-java-in-sql-server-2019-is-now-available/). |
|SQL Server Language Extensions| Execute external code with the extensibility framework. See [SQL Server Language Extensions](../language-extensions/language-extensions-overview.md).
|Register external languages|A new Data Definition Language (DDL), `CREATE EXTERNAL LANGUAGE`, registers external languages, such as Java, in SQL Server. See [CREATE EXTERNAL LANGUAGE](../t-sql/statements/create-external-language-transact-sql.md). |

### Spatial

|New feature or update | Details |
|:---|:---|
| New spatial reference identifiers (SRIDs) |[Australian GDA2020](http://www.ga.gov.au/scientific-topics/positioning-navigation/geodesy/datums-projections/gda2020) provides a more robust and accurate datum that's more closely aligned with global positioning systems. The new SRIDs are:<ul><li>7843 for geographic 2D</li><li>7844 for geographic 3D</li></ul> For definitions of new SRIDs, see [sys.spatial_reference_systems](../relational-databases/system-catalog-views/sys-spatial-reference-systems-transact-sql.md) view. |

### Error messages
When an extract, transform, and load (ETL) process fails because the source and the destination don't have matching data types and/or length, troubleshooting used to be time-consuming, especially in large datasets. [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] allows faster insights into data truncation errors.

|New feature or update | Details |
|:---|:---|
|Verbose truncation warnings | The data truncation error message defaults to include table and column names, and the truncated value. See [VERBOSE_TRUNCATION_WARNINGS](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#verbose-truncation).|

## Mission-critical security
[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provides a security architecture that's designed to allow database administrators and developers to create secure database applications and counter threats. Each version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has improved on previous versions with the introduction of new features and functionality, and [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] continues to build on this story.

|New feature or update | Details |
|:---|:---|
|Always Encrypted with secure enclaves|Expands upon Always Encrypted with in-place encryption and rich computations by enabling computations on plaintext data inside a server-side secure enclave. In-place encryption improves the performance and the reliability of cryptographic operations (encrypting columns, rotating columns, encryption keys, and so on), because it avoids moving data out of the database.<br><br> Support for rich computations (pattern matching and comparison operations) unlocks Always Encrypted to a much broader set of scenarios and applications that demand sensitive data protection, while also requiring richer functionality in Transact-SQL queries. See [Always Encrypted with Secure Enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md).|
|Certificate management in SQL Server Configuration Manager|Certificate management tasks such as viewing and deploying certificates is now possible by using SQL Server Configuration Manager. See [Certificate Management (SQL Server Configuration Manager)](../database-engine/configure-windows/manage-certificates.md).|
|Data Discovery & Classification|Data Discovery & Classification provides capabilities for classifying and labeling columns in user tables. Classifying sensitive data (business, financial, healthcare, personally identifiable information (PII), etc.) can play a pivotal role in an organizational information protection stature. It can serve as infrastructure for:<ul><li>Helping meet data privacy standards and regulatory compliance requirements</li><li>Various security scenarios, such as monitoring (auditing) and alerting on anomalous access to sensitive data</li><li>Making it easier to identify where sensitive data resides in the enterprise so admins can take the right steps securing the database</li></ul>|
|SQL Server Audit|[Auditing](../relational-databases/security/auditing/sql-server-audit-database-engine.md) has also been enhanced to include a new field `data_sensitivity_information` in the audit log record, which contains the sensitivity classifications (labels) of the actual data that was returned by the query. For details and examples, see [`ADD SENSITIVITY CLASSIFICATION`](../t-sql/statements/add-sensitivity-classification-transact-sql.md).|

## High availability
One common task that everyone who deploys [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has to account for is making sure that all mission critical [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances and the databases within them are available whenever the business and end users need them. Availability is a key pillar of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] platform, and [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] introduces many new features and enhancements that allow businesses to ensure that their database environments are highly available.

### Availability Groups

|New feature or update | Details |
|:---|:---|
|Up to five synchronous replicas|[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] increases the maximum number of synchronous replicas to 5, up from 3 in [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. You can configure this group of five replicas to have automatic failover within the group. There is one primary replica, plus four synchronous secondary replicas.|
|Secondary-to-primary replica connection redirection| Allows client application connections to be directed to the primary replica regardless of the target server specified in the connection string. For details, see [Secondary to primary replica read/write connection redirection (Always On Availability Groups)](../database-engine/availability-groups/windows/secondary-replica-connection-redirection-always-on-availability-groups.md).|
|HADR Benefits| Every Software Assurance customer of SQL Server will be able to use three enhanced benefits for any SQL Server release that is still supported by Microsoft. For details, see [our announcement here.](https://cloudblogs.microsoft.com/sqlserver/2019/10/30/new-high-availability-and-disaster-recovery-benefits-for-sql-server/)

### Recovery

|New feature or update | Details |
|:---|:---|
|Accelerated database recovery | Reduce the time to recover after a restart or a long-running transaction rollback with accelerated database recovery (ADR). See [Accelerated database recovery](../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md#adr).|

### Resumable operations

|New feature or update | Details |
|:---|:---|
|Online clustered columnstore index build and rebuild | See [Perform Index Operations Online](../relational-databases/indexes/perform-index-operations-online.md). |
|Resumable online rowstore index build | See [Perform Index Operations Online](../relational-databases/indexes/perform-index-operations-online.md). |
|Suspend and resume initial scan for Transparent Data Encryption (TDE)|See [Transparent Data Encryption (TDE) scan - suspend and resume](../relational-databases/security/encryption/transparent-data-encryption.md#scan-suspend-resume).|

## Platform choice
[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] builds on the innovations that were introduced in [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] to allow you to run [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on your platform of choice with more functionality and security than ever before.

### <a id="sql-server-on-linux"></a>Linux

| New feature or update | Details |
|:-----|:-----|
|Replication support | See [SQL Server Replication on Linux](../linux/sql-server-linux-replication.md). |
|Support for the Microsoft Distributed Transaction Coordinator (MSDTC) | See [How to configure MSDTC on Linux](../linux/sql-server-linux-configure-msdtc.md). |
|OpenLDAP support for third-party AD providers | See [Tutorial: Use Active Directory authentication with SQL Server on Linux](../linux/sql-server-linux-active-directory-authentication.md). |
|Machine Learning Services on Linux | See [Install SQL Server Machine Learning Services (Python and R) on Linux](../linux/sql-server-linux-setup-machine-learning.md). |
|TempDB improvements | By default, a new installation of SQL Server on Linux creates multiple TempDB data files, based on the number of logical cores (with up to eight data files). This doesn't apply to in-place minor or major version upgrades. Each TempDB file is 8 MB with an auto growth of 64 MB. This behavior is similar to the default SQL Server installation on Windows. |
|PolyBase on Linux | See [Install PolyBase](../relational-databases/polybase/polybase-linux-setup.md) on Linux for non-Hadoop connectors.<br/><br/>See [PolyBase type mapping](../relational-databases/polybase/polybase-type-mapping.md). |
| Change Data Capture (CDC) support | Change Data Capture (CDC) is now supported on Linux for [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)]. |

### Containers
The easiest way to get started working with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is to use containers. [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] builds on the innovations introduced in earlier versions to enable you to deploy [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] containers on new platforms, in a safer manner, and with more functionality.

|New feature or update | Details |
|:---|:---|
| Microsoft Container Registry | The [Microsoft Container Registry](https://azure.microsoft.com/blog/microsoft-syndicates-container-catalog/) now replaces Docker Hub for new official Microsoft container images, including [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)]. |
| Non-root containers | [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] introduces the ability to create safer containers by starting the [!INCLUDE[sql-server](../includes/ssnoversion-md.md)] process as a non-root user by default. See [build and run SQL Server containers as a non-root user](../linux/sql-server-linux-docker-container-deployment.md). |
| Red Hat certified container images | Starting with [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)], you can run SQL Server containers on Red Hat Enterprise Linux. |
| PolyBase and Machine Learning support| [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] introduces new ways to work with SQL Server Containers such as Machine Learning Services and PolyBase. Check out some examples in the [SQL Server in container GitHub repository](https://github.com/microsoft/mssql-docker/tree/master/linux/preview/examples). |

## Setup options

|New feature or update | Details |
|:---|:---| 
|New memory setup options | Sets the *min server memory (MB)* and *max server memory (MB)* server configurations during installation. See [Database Engine Configuration - Memory page](./install/instance-configuration.md#memory) and the `USESQLRECOMMENDEDMEMORYLIMITS`, `SQLMINMEMORY`, and `SQLMAXMEMORY` parameters in [Install SQL Server from the Command Prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#Install). The proposed value aligns with the memory configuration guidelines in [Server Memory Configuration Options](../database-engine/configure-windows/server-memory-server-configuration-options.md#manually).| 
|New parallelism setup options | Sets the *max degree of parallelism* server configuration during installation. See [Database Engine Configuration - MaxDOP page](./install/instance-configuration.md#maxdop) and the `SQLMAXDOP` parameter in [Install SQL Server from the Command Prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#Install). The default value aligns with the max degree of parallelism guidelines in [Configure the max degree of parallelism Server Configuration Option](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md#recommendations).| 
|Setup warning on Server/CAL license Product Key|If an Enterprise Server/CAL license Product Key is entered, and the machine has more than 20 physical cores, or 40 logical cores when Hyper-Threading is enabled, a warning is shown during setup. Users can still acknowledge the limitation and continue setup, or enter a License Key that supports the operating system maximum number of processors.|

## <a id="ml"></a> SQL Server Machine Learning Services

|New feature or update | Details |
|:---|:---|
|Partition-based modeling | You can process external scripts per partition of your data by using the new parameters added to `sp_execute_external_script`. This functionality supports training many small models (one model per partition of data) instead of one large model. See [Create partition-based models](../machine-learning/tutorials/r-tutorial-create-models-per-partition.md).|
|Windows Server Failover Cluster| You can configure high availability for Machine Learning Services on a Windows Server Failover Cluster.|

## SQL Server Analysis Services

This release introduces new features and improvements for performance, resource governance, and client support.

| New feature or update | Details |
|:---|:---|
|Calculation groups in tabular models| Calculation groups can significantly reduce the number of redundant measures by grouping common measure expressions as *calculation items*. To learn more, see [Calculation groups in tabular model](/analysis-services/tabular-models/calculation-groups). |
|Query interleaving| Query interleaving is a tabular mode system configuration that can improve user query response times in high-concurrency scenarios. To learn more, see [Query interleaving](/analysis-services/tabular-models/query-interleaving). |
|Many-to-many relationships in tabular models| Allows many-to-many relationships between tables where both columns are non-unique. To learn more, see [Relationships in tabular models](/analysis-services/tabular-models/relationships-ssas-tabular).|
|Property settings for resource governance| This release includes new memory settings: Memory\QueryMemoryLimit, DbpropMsmdRequestMemoryLimit, and OLAP\Query\RowsetSerializationLimit for resource governance. To learn more, see [Memory settings](/analysis-services/server-properties/memory-properties).|
|Governance setting for Power BI cache refreshes | This release introduces the ClientCacheRefreshPolicy property, which overrides caching dashboard tile data and report data for initial load of Live connect reports by the Power BI service. To learn more, see [General Properties](/analysis-services/server-properties/general-properties). |
| Online attach  | Online attach can be used for synchronization of read-only replicas in on-premises query scale-out environments. To learn more, see [Online attach](/analysis-services/what-s-new-in-sql-server-analysis-services#online-attach). |

## SQL Server Integration Services

This release introduces new features to improve file operations.

| New feature or update | Details |
|:---|:---|
|Flexible file task |Perform file operations on Local File System, Azure Blob Storage, and Azure Data Lake Storage Gen2. See [Flexible File Task](../integration-services/control-flow/flexible-file-task.md).|
|Flexible file source and destination |Read and write data for Azure Blob Storage, and Azure Data Lake Storage Gen2. See [Flexible File Source](../integration-services/data-flow/flexible-file-source.md) and [Flexible File Destination](../integration-services/data-flow/flexible-file-destination.md). |

## SQL Server [!INCLUDE[master-data-services](../includes/ssmdsshort-md.md)]

| New feature or update | Details |
|:---|:---|
|Support for Azure SQL Managed Instance databases| Host [!INCLUDE[master-data-services](../includes/ssmdsshort-md.md)] on Azure SQL Managed Instance. See [[!INCLUDE[master-data-services](../includes/ssmdsshort-md.md)] installation and configuration](../master-data-services/master-data-services-installation-and-configuration.md#SetUpWeb).|
|New HTML controls| HTML controls replace all former Silverlight components. Silverlight dependency removed.|

## SQL Server Reporting Services

This release of SQL Server Reporting Services features support for Azure SQL Managed Instances, Power BI Premium datasets, enhanced accessibility, Azure Active Directory Application Proxy, and Transparent Database Encryption. It also brings an update to Microsoft Report Builder. See [What's new in SQL Server Reporting Services](../reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md) for details.

## See also

- [`SqlServer` PowerShell module](https://www.powershellgallery.com/packages/Sqlserver)
- [SQL Server PowerShell documentation](../powershell/sql-server-powershell.md)

## Next steps

- [SQL Server Workshops](https://aka.ms/sqlworkshops)
- [[!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] release notes](sql-server-version-15-release-notes.md)
- [Microsoft [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)]: Technical white paper](https://aka.ms/sql2019whitepaper)
- [What's new in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]](what-s-new-in-sql-server-2022.md)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
