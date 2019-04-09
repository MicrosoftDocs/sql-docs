---
title: "What's new in SQL Server 2019 | Microsoft Docs"
ms.date: 03/27/2018
ms.prod: "sql-server-2019"
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# What's new in [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud, and operating systems. This article summarizes what is new for [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]. The first section identifies features that are added in the latest preview release. The other sections of this article provide details about all of the features released to date for this [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].

For more information and known issues, see the [[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] Release Notes](sql-server-ver15-release-notes.md).

**Try [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]!**

- [![Download from Evaluation Center](../includes/media/download2.png)](https://go.microsoft.com/fwlink/?LinkID=862101) [Download [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] to install on Windows](https://go.microsoft.com/fwlink/?LinkID=862101).
- Install on Linux for [Red Hat Enterprise Server](../linux/quickstart-install-connect-red-hat.md), [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md), and [Ubuntu](../linux/quickstart-install-connect-ubuntu.md).
- [Run on [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] on Docker](../linux/quickstart-install-connect-docker.md).

**Use the [latest tools](#tools) for the best experience with [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].**

## CTP 2.4 March 2019

Community technology preview (CTP) 2.4 is the latest public release of [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]. This release includes improvements from previous CTP releases to fix bugs, improve security, and optimize performance. In addition, the following features are added or enhanced for [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] CTP 2.4.

### Big data clusters

| New feature or update | Details |
|:---|:---|
| Guidance on GPU support for running deep learning with TensorFlow in Spark. | [Deploy a big data cluster with GPU support and run TensorFlow](../big-data-cluster/spark-gpu-tensorflow.md). |
| **SqlDataPool** and **SqlStoragePool** data sources are no longer created by default. | Create these manually as needed. See the [known issues](../big-data-cluster/release-notes-big-data-cluster.md#externaltablesctp24). |
| `INSERT INTO SELECT` support for the data pool. | For an example, see [Tutorial: Ingest data into a SQL Server data pool with Transact-SQL](../big-data-cluster/tutorial-data-pool-ingest-sql.md). |
| `FORCE SCALEOUTEXECUTION` and `DISABLE SCALEOUTEXECUTION` option. | See [Big data clusters release notes](../big-data-cluster/release-notes-big-data-cluster.md#whats-new).|
| Updated AKS deployment recommendations. | When evaluating big data clusters on AKS, we now recommend using a single node of size **Standard_L8s**. |
| Spark runtime upgrade to Spark 2.4. | |
| &nbsp; | &nbsp; |

### Database engine

| New feature or update | Details |
|:-----|:-----|
|Truncation error message defaults to include table and column names, and truncated value.|[VERBOSE_TRUNCATION_WARNINGS](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#verbose-truncation)|
|New DMF `sys.dm_exec_query_plan_stats` returns the equivalent of the last known actual execution plan for most queries. |[sys.dm_exec_query_plan_stats](../relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-stats-transact-sql.md)<sup>1</sup>|
|The new `query_post_execution_plan_profile` Extended Event collects the equivalent of an actual execution plan based on lightweight profiling, unlike `query_post_execution_showplan` which uses standard profiling. |[Query profiling infrastructure](../relational-databases/performance/query-profiling-infrastructure.md)|
|Transparent Data Encryption (TDE) scan - suspend and resume.|[Transparent Data Encryption (TDE) scan - suspend and resume](../relational-databases/security/encryption/transparent-data-encryption.md#scan-suspend-resume)|
| &nbsp; | &nbsp; |

><sup>1</sup>
>This is an opt-in feature and requires [trace flag](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) 2451 to be enabled.

### SQL Server Analysis Services (SSAS)

| New feature or update | Details |
|:-----|:-----|
|Many-to-many relationships in tabular models.||
|Property settings for resource governance.||
| &nbsp; | &nbsp; |

## CTP 2.3 February 2019

### Big data clusters

| New feature or update | Details |
| :---------- | :------ |
| Submit Spark jobs on big data clusters in IntelliJ. | [Submit Spark jobs on SQL Server big data clusters in IntelliJ](../big-data-cluster/spark-submit-job-intellij-tool-plugin.md) |
| Common CLI for application deployment and cluster management. | [How to deploy an app on SQL Server 2019 big data cluster (preview)](../big-data-cluster/big-data-cluster-create-apps.md) |
| VS Code extension to deploy applications to a big data cluster. | [How to use VS Code to deploy applications to SQL Server big data clusters](../big-data-cluster/app-deployment-extension.md) |
| Changes to the **mssqlctl** tool command usage. | For more details see the [known issues for mssqlctl](../big-data-cluster/release-notes-big-data-cluster.md#mssqlctlctp23). |
| Use Sparklyr in big data cluster. | [Use Sparklyr in SQL Server 2019 big data cluster](../big-data-cluster/sparklyr-from-RStudio.md) |
| Mount external HDFS-compatible storage into big data cluster with **HDFS tiering**. | See [HDFS tiering](../big-data-cluster/hdfs-tiering.md). |
| New unified connection experience for the SQL Server master instance and the HDFS/Spark Gateway. | See [SQL Server master instance and the HDFS/Spark Gateway](../big-data-cluster/connect-to-big-data-cluster.md). |
| Deleting a cluster with **mssqlctl cluster delete** now deletes only the objects in the namespace that were part of the big data cluster. | The namespace is not deleted. However, in earlier releases this command did delete the entire namespace. |
| _Security_ endpoint names have been changed and consolidated. | **service-security-lb** and **service-security-nodeport** have been consolidated into the **endpoint-security** endpoint. |
| _Proxy_ endpoint names have been changed and consolidated. | **service-proxy-lb** and **service-proxy-nodeport** have been consolidated into the **endpoint-service-proxy** endpoint. |
| _Controller_ endpoint names have been changed and consolidated. | **service-mssql-controller-lb** and **service-mssql-controller-nodeport** have been consolidated into the **endpoint-controller** enpoint. |
| &nbsp; | &nbsp; |

### Database engine

| New feature or update | Details |
|:-----|:-----|
|Enable accelerated database recovery can be enabled per-database.| [Accelerated database recovery](../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md#adr)|
|Query Store plan forcing support for fast forward and static cursors.|[Plan forcing support for fast forward and static cursors](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#ctp23) |
|Reduced recompilations for workloads using temporary tables across multiple scopes. |[Reduced recompilations for workloads](../relational-databases/tables/tables.md#ctp23) |
|Improved indirect checkpoint scalability. |[Improved indirect checkpoint scalability](../relational-databases/logs/database-checkpoints-sql-server.md#ctp23)|
|UTF-8 support: Adds support to use UTF-8 character encoding with a BIN2 collation (`UTF8_BIN2`). |[Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md) |
|Define cascaded delete actions on an edge constraint in a graph database. |[Edge constraints](../relational-databases/tables/graph-edge-constraints.md) |
|Enable or disable `LIGHTWEIGHT_QUERY_PROFILING` with the new database scoped configuration. |[`VERBOSE_TRUNCATION_WARNINGS`](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#verbose-truncation) |
| &nbsp; | &nbsp; |

### Tools

| New feature or update | Details |
|:-----|:-----|
|Azure Data Studio supports Azure Active Directory. |[Azure Data Studio](../azure-data-studio/what-is.md) |
|Notebook view UI has moved into Azure Data Studio core. |[How to manage notebooks in Azure Data Studio](../big-data-cluster/notebooks-how-to-manage.md) |
|Added new wizard to create external data sources from Hadoop Distributed File System (HDFS) to SQL Server Big Data Cluster. | |
|Improved Notebook viewer UI. | |
|Added new Notebook APIs.| |
|Added "Reinstall Notebook dependencies" command to assist with Python package updates. | |
|Launch Azure Data Studio from SSMS.| |
| &nbsp; | &nbsp; |

### Analysis services

| New feature or update | Details |
|:-----|:-----|
|Calculation groups in tabular model.| |
| &nbsp; | &nbsp; |

## CTP 2.2 December 2018

### Big data clusters

| New feature or update | Details |
|:-----|:-----|
|Use SparkR from Azure Data Studio on a big data cluster. | |
|Deploy Python and R apps.|[Deploy applications using mssqlctl](../big-data-cluster/big-data-cluster-create-apps.md) |
| &nbsp; | &nbsp; |

### Database engine

| New feature or update | Details |
|:-----|:-----|
|Adds support to use UTF-8 character encoding with SQL Server Replication. |[Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md#ctp23) |
| &nbsp; | &nbsp; |


### SQL Server on Linux

| New feature or update | Details |
|:-----|:-----|
|Always On Availability Group on Docker containers with Kubernetes. |[Always On Availability Groups for containers](../linux/sql-server-ag-kubernetes.md) |
| &nbsp; | &nbsp; |

## CTP 2.1 November 2018

### Database engine

| New feature or update | Details |
|:-----|:-----|
|Adds support to select UTF-8 collation as default during. [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] setup. |[Collation and Unicode Support](../relational-databases/collations/collation-and-unicode-support.md#ctp23) |
|Scalar UDF inlining automatically transforms scalar user-defined functions (UDF) into relational expressions and embeds them in the calling SQL query. |[Scalar UDF Inlining](../relational-databases/user-defined-functions/scalar-udf-inlining.md) |
|The dynamic management view `sys.dm_exec_requests` column `command` shows `SELECT (STATMAN)` if a `SELECT` is waiting for a synchronous statistics update operation to complete prior to continuing query execution. | [`sys.dm_exec_requests`](../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) |
|The new wait type `WAIT_ON_SYNC_STATISTICS_REFRESH` is surfaced in the `sys.dm_os_wait_stats` dynamic management view. It shows the accumulated instance-level time spent on synchronous statistics refresh operations.|[`sys.dm_os_wait_stats`](../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md) |
|Hybrid buffer pool is a new feature of the SQL Server database engine where database pages sitting on database files placed on a persistent memory (PMEM) device will be directly accessed when required.|[Hybrid buffer pool](../database-engine/configure-windows/hybrid-buffer-pool.md) |
|[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] introduces static data masking. You can use static data masking to sanitize sensitive data in copies of SQL Server databases.|[Static data masking](../relational-databases/security/static-data-masking.md) |
|Use derived table or view aliases in graph match query |[Graph Edge Constraints](../relational-databases/tables/graph-edge-constraints.md) |
| &nbsp; | &nbsp; |

### SQL Server on Linux

| New feature or update | Details |
|:-----|:-----|
|New container registry for SQL Server. |[Get started with SQL Server containers on Docker](../linux/quickstart-install-connect-docker.md) |
| &nbsp; | &nbsp; |

### Tools

| New feature or update | Details |
|:-----|:-----|
|[Azure Data Studio](../azure-data-studio/what-is.md) supports Connect and manage [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] big data clusters. | |
| &nbsp; | &nbsp; |

## CTP 2.0 October 2018

### Big data clusters

| New feature or update | Details |
|:-----|:-----|
|Deploy a Big Data cluster with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and Spark Linux containers on Kubernetes. | |
|Access your big data from HDFS. | |
|Run Advanced analytics and machine learning with Spark. | |
|Use Spark streaming to data to SQL data pools. | |
|Run Query books that provide a notebook experience in **Azure Data Studio**.|[Data engineering](../azure-data-studio/what-is.md#data-engineering)|
| &nbsp; | &nbsp; |

### Database engine

| New feature or update | Details |
|:-----|:-----|
|Database **COMPATIBILITY_LEVEL 150** is added. |[ALTER DATABASE Compatibility Level (Transact-SQL)](../t-sql/statements/alter-database-transact-sql-compatibility-level.md) |
|Resumable Online Index Create.|[CREATE INDEX (Transact-SQL)](../t-sql/statements/create-index-transact-sql.md#resumable-indexes) |
|Row mode memory grant feedback. |[Row mode memory grant feedback](../relational-databases/performance/intelligent-query-processing.md#row-mode-memory-grant-feedback) |
|Approximate `COUNT DISTINCT`.|[Approximate query processing](../relational-databases/performance/intelligent-query-processing.md#approximate-query-processing)|
|Batch mode on rowstore.|[Batch mode on rowstore](../relational-databases/performance/intelligent-query-processing.md#batch-mode-on-rowstore) |
|Table variable deferred compilation.|[Table variable deferred compilation](../relational-databases/performance/intelligent-query-processing.md#table-variable-deferred-compilation) |
|Java language extension.|[Java language extension](../advanced-analytics/java/extension-java.md) |
|Merge your current graph data from node or edge tables with new data using the `MATCH` predicates in the `MERGE` statement. | |
|Edge constraints.|[Graph edge constraints](../relational-databases/tables/graph-edge-constraints.md) |
|Database scoped default setting for online and resumable DDL operations.| |
|Availability groups support up to 5 synchronous secondary replicas.|[Availability groups](../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md) |
|Secondary to primary replica read/write connection redirection|[Secondary to primary replica read/write connection redirection-Always On Availability Groups](../database-engine/availability-groups/windows/secondary-replica-connection-redirection-always-on-availability-groups.md) |
|SQL Data Discovery and Classification.| [SQL Data Discovery & Classification](../relational-databases/security/sql-data-discovery-and-classification.md) |
|Expanded support for persistent memory devices.|[Hybrid Buffer Pool](../database-engine/configure-windows/hybrid-buffer-pool.md) |
|Support for columnstore statistics in `DBCC CLONEDATABASE`|[Stats blob for columnstore indexes](../t-sql/database-console-commands/dbcc-clonedatabase-transact-sql.md#ctp23)|
|`sp_estimate_data_compression_savings` introduces `COLUMNSTORE` and `COLUMNSTORE_ARCHIVE`.|[Considerations for Columnstore Indexes](../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md#considerations-for-columnstore-indexes)|
|Machine Learning services supported on Windows Server Failover Cluster. |[ What's new - SQL Server Machine Learning Services](../advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md)|
|Machine Learning support for partition-based modeling.|[What's new - SQL Server Machine Learning Services](../advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md) |
|Lightweight query profiling infrastructure enabled by default |[Lightweight query execution statistics profiling infrastructure v3](../relational-databases/performance/query-profiling-infrastructure.md#lightweight-query-execution-statistics-profiling-infrastructure-v3) |
|New PolyBase connectors for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], Oracle, Teradata, and MongoDB. |[What is PolyBase?](../relational-databases/polybase/polybase-guide.md) |
|`sys.dm_db_page_info(database_id, file_id, page_id, mode)` returns information about a page in a database. |[sys.dm_db_page_info (Transact-SQL)](../relational-databases/system-dynamic-management-views/sys-dm-db-page-info-transact-sql.md)|
|Always Encrypted with secure enclaves. |[Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md) |
|Build and rebuild online clustered columnstore index. |[Perform Index Operations Online](../relational-databases/indexes/perform-index-operations-online.md) |
| &nbsp; | &nbsp; |

### SQL Server on Linux

| New feature or update | Details |
|:-----|:-----|
|Replication support |[SQL Server Replication on Linux](../linux/sql-server-linux-replication.md)
|Support for the Microsoft Distributed Transaction Coordinator (MSDTC) |[How to configure MSDTC on Linux](../linux/sql-server-linux-configure-msdtc.md) |
|OpenLDAP support for third-party AD providers |[Tutorial: Use Active Directory authentication with SQL Server on Linux](../linux/sql-server-linux-active-directory-authentication.md) |
|Machine Learning on Linux |[Configure Machine Learning on Linux](../linux/sql-server-linux-setup-machine-learning.md) |
| &nbsp; | &nbsp; |

### Master Data Services

| New feature or update | Details |
|:-----|:-----|
|The Master Data Services (MDS) portal no longer depends on Silverlight.| All the former Silverlight components have been replaced with HTML controls.|
| &nbsp; | &nbsp; |

### Security

| New feature or update | Details |
|:-----|:-----|
|Certificate management in SQL Server Configuration Manager|[Certificate Management (SQL Server Configuration Manager)](../database-engine/configure-windows/manage-certificates.md)
| &nbsp; | &nbsp; |

### Tools

| New feature or update | Details |
|:-----|:-----|
|[Azure Data Studio](../azure-data-studio/what-is.md) supports Connect and manage [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] big data clusters. |[What is Azure Data Studio](../azure-data-studio/what-is.md)|
|Supports scenarios using SQL Server big data clusters. |[SQL Server 2019 extension (preview)](../azure-data-studio/sql-server-2019-extension.md)|
|[**SQL Server Management Studio (SSMS) 18.0 (preview)**](../ssms/sql-server-management-studio-ssms.md): Supports [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].| |
|Support for Always Encrypted with secure enclaves. |[Always Encrypted with Secure Enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md)|
| &nbsp; | &nbsp; |


## Other services

As of CTP 2.4, [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] does not introduce new features for the following services:

- [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] (SSIS)
- [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] (SSRS)

## See also

- [`SqlServer` PowerShell module](https://www.powershellgallery.com/packages/Sqlserver)
- [SQL Server PowerShell documentation](../powershell/sql-server-powershell.md)

## Next steps

- [[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] Release Notes](sql-server-ver15-release-notes.md).

- [Microsoft [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]: Technical white paper](http://info.microsoft.com/rs/157-GQE-382/images/EN-US-CNTNT-white-paper-DBMod-Microsoft-SQL-Server-2019-Technical-white-paper.pdf)<br />Published in September 2018. Applies to Microsoft [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] CTP 2.0 for Windows, Linux, and Docker containers.

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
