---
title: "What's new in SQL Server 2019 | Microsoft Docs"
ms.custom: ""
ms.date: "09/24/2018"
ms.prod: "sql-server-2018"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "craigg"
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# What's new in SQL Server 2019

[!INCLUDE[tsql-appliesto-ssvnext-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssvnext-xxxx-xxxx-xxx.md)]

[!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud, and operating systems. This article summarizes what is new for SQL Server 2019. For more information and known issues, see the [SQL Server 2019 Release Notes](sql-server-vnext-release-notes.md).

**Try SQL Server 2019!**
- [![Download from Evaluation Center](../includes/media/download2.png)](http://go.microsoft.com/fwlink/?LinkID=829477) [Download SQL Server 2019 to install on Windows](http://go.microsoft.com/fwlink/?LinkID=829477)
- Install on Linux for [Red Hat Enterprise Server](../linux/quickstart-install-connect-red-hat.md), [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md), and [Ubuntu](../linux/quickstart-install-connect-ubuntu.md).
- [Run on SQL Server 2019 on Docker](../linux/quickstart-install-connect-docker.md).

## CTP 2.0 

Community technology preview (CTP) 2.0 is the first public release of [!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)]. The following features are added or enhanced for [!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)] CTP 2.0.

- [Database engine](#databaseengine)
  - Intelligent query processing
  - Database scoped configuration setting for online and resumable DDL operations
  - Expanded support for Persistent Memory (PMEM) devices
  - Clustered columnstore online index build and rebuild
  - UTF-8 Support
  - Lightweight query profiling infrastructure enabled by default
- [High Availability](#ha)
  - Connection redirection
- [SQL Graph](#sqlgraph)
  - Match support in `MERGE` DML
  - Derived Tables and Views support for graph tables
  - Edge Constraints
- [SQL Server on Linux](#sqllinux)
  - Replication support
  - Support for the Microsoft Distributed Transaction Coordinator (MSDTC)
  - Always On Availability Group on Docker containers with Kubernetes
  - OpenLDAP support for third-party AD providers
  - Machine Learning support
  - New container registry
  - New RHEL-based container images
- [PolyBase](#polybase)
  - New connectors for SQL Server, Oracle, Teradata, and MongoDB.
- [SQL Server Machine Learning Services](#ml)
  - Machine Learning on Linux
  - Partition-based modeling
  - Failover cluster support
- [Programmability extensions](#programmability)
  - Java language extension
- [Security](#security)
  - Always Encrypted with secure enclaves
  - Certificate management in SQL Server Configuration Manager

Continue reading for more details about these features.

## <a id="databaseengine"></a> Database Engine

### Intelligent query processing 

- Database **COMPATIBILITY_LEVEL 150** is added. To enable for a specific user database, execute:

   ```sql
   ALTER DATABASE database_name SET COMPATIBILITY_LEVEL =  150;
   ```

- **Row mode memory grant feedback** expands on the memory grant feedback feature introduced in SQL Server 2017 by adjusting memory grant sizes for both batch and row mode operators.  For an excessive memory grant condition, if the granted memory is more than two times the size of the actual used memory, memory grant feedback will recalculate the memory grant. Consecutive executions will then request less memory. For an insufficiently sized memory grant that results in a spill to disk, memory grant feedback will trigger a recalculation of the memory grant.  Consecutive executions will then request more memory. This feature is enabled by default under database compatibility level 150.

- **Approximate COUNT DISTINCT** returns the approximate number of unique non-null values in a group. This function is designed for use in big data scenarios and is optimized for the following conditions:
  - Access of data sets that are millions of rows or higher AND
  - Aggregation of a column or columns that have a large number of distinct values AND
  - Responsiveness is more critical than absolute precision. `APPROXIMATE_COUNT_DISTINCT` yields results typically within 2% of the precise answer in a small fraction of the time.

- **Batch mode on rowstore** enables batch mode without requiring a columnstore index. Batch mode processing allows query operators to process data more efficiently by working on a batch of rows at a time instead of one row at a time. A number of other scalability improvements are tied to batch mode processing.  In earlier versions, batch mode only worked in conjunction with columnstore indexes.  This feature is enabled by default under database compatibility level 150. Workloads that may benefit:
  - A significant part of the workload consists of analytical queries (as a rule of thumb, queries with operators such as joins or aggregates processing hundreds of thousands of rows or more), AND
  - The workload is CPU bound AND
  - Creating a columnstore index adds too much overhead to the transactional part of your workload, OR creating a columnstore index is not feasible because your application depends on a feature that is not yet supported with columnstore indexes.

- **Table variable deferred compilation** improves plan quality and overall performance for queries referencing table variables. During optimization and initial compilation, this feature will propagate cardinality estimates that are based on actual table variable row counts.  This accurate row count information will be used for optimizing downstream plan operations. This feature is enabled by default under database compatibility level 150.

### Resumable online index create

- **Resumable online index create** allows an index create operation to pause and resume later from where the operation was paused or failed, instead of restarting from the beginning.

  Resumable Online Index Create supports the follow scenarios:
  - Resume an index create operation after an index create failure, such as after a database failover or after running out of disk space.
  - Pause an ongoing index create operation and resume it later allowing to temporarily free system resources as required and resume this operation later.
  - Create large indexes without using a lot of log space and a long-running transaction that blocks other maintenance activities and allowing log truncation.

In case of an execution error or failure, without this feature an online index create operation must be executed again and the operation must be restarted from the beginning

With this release, we extend the resumable functionality adding this feature to available [resumable online index rebuild](http://azure.microsoft.com/blog/modernize-index-maintenance-with-resumable-online-index-rebuild/).

In addition, this feature can be setup be default for a specific database using  
[database scoped default setting for online and resumable DDL operations](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

For more information see [Resumable Online Index Create](http://azure.microsoft.com/blog/resumable-online-index-create-is-in-public-preview-for-azure-sql-db/)

### Database scoped default setting for online and resumable DDL operations 

- **Database scoped default setting for online and resumable DDL operations** allows a default behavior setting for `ONLINE` and `RESUMABLE` index operations at the database level, rather than defining these options for each individual index DDL statement such as index create or rebuild.

- Set these defaults using the `ELEVATE_ONLINE` and `ELEVATE_RESUMABLE` database scoped configuration options. Both options will cause the engine to automatically elevate supported operations to index online or resumable execution. You can enable the following behaviors using these options:

  - `FAIL_UNSUPPORTED` option allows to execute all index operations online or resumable and fail index operations that are not supported for online or resumable.
  - `WHEN_SUPPPORTED` option allows to execute supported operations online or resumable and run index unsupported operations offline or non-resumable.
  - `OFF` option allows the current behavior of executing all index operations offline and non-resumable unless explicitly specified in the DDL statement.

To override the default setting, include the ONLINE or RESUMABLE option in the index create and rebuild commands.  

Without this feature you have to specify the online and resumable options directly in the index DDL statement such as index create and rebuild.

More information:
For more information on index resumable operations see [Resumable Online Index Create](http://azure.microsoft.com/blog/resumable-online-index-create-is-in-public-preview-for-azure-sql-db/).

### Build and rebuild clustered columnstore indexes online

Convert row-store tables into columnstore format. Create clustered columnstore indexes (CCI). This was an offline process in previous versions of SQL Server - requiring all changes stop while CCI is created. SQL Server 2019 and Azure SQL Database enable customers to create or re-create CCI online. Workload will not be blocked and all changes to made on the underlying data are transparently added into the target columnstore table. Examples of new TSQL statements that can be used are:

  ```sql
  CREATE CLUSTERED COLUMNSTORE INDEX cci
    ON <tableName>
    WITH (ONLINE = ON)
  ```

  ```SQL  
  ALTER INDEX cci
    ON <tableName>
    REBUILD WITH (ONLINE = ON)
  ```

### UTF-8 Support

Full support for the widely used UTF-8 character encoding as an import or export encoding, or as database-level or column-level collation for text data. UTF-8 is allowed in the CHAR and VARCHAR datatypes, and is enabled when creating or changing an object’s collation to a collation with the `UTF8` suffix. 

For example,`LATIN1_GENERAL_100_CI_AS_SC` to `LATIN1_GENERAL_100_CI_AS_SC_UTF8`. UTF-8 is only available to Windows collations that support supplementary characters, as introduced in SQL Server 2012. Note that NCHAR and NVARCHAR allow UTF-16 encoding only, and remain unchanged.

This may provide significant storage savings, depending on the character set in use. For example, changing an existing column data type from NCHAR(10) to CHAR(10) using an UTF-8 enabled collation, translates into 50% reduction in storage requirements. This is because NCHAR(10) requires 22 bytes for storage, whereas CHAR(10) requires 12 bytes for the same Unicode string.

### Lightweight query profiling infrastructure enabled by default

The lightweight query profiling infrastructure (LWQPI) is now enabled by default. The lightweight query profiling infrastructure was introduced in SQL Server 2016 SP1. It offers a query execution statistics collection mechanism with an expected overhead of 2% CPU, compared with an overhead of up to 75% CPU for the standard query profiling mechanism. On previous versions, it was OFF by default. Database administrators could enable it with [trace flag 7412](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

## <a id="ha"></a> High Availability

- **Connection redirection**: Improved scale-out with automatic redirection of connections based on read/write intent.

## <a id="sqlgraph"></a> SQL Graph

- **Match support in `MERGE` DML** allows you to specify graph relationships in a single statement, instead of separate `INSERT`, `UPDATE`, or `DELETE` statements. Merge your current graph data from node or edge tables with new data using the `MATCH` predicates in the `MERGE` statement. This enables `UPSERT` scenarios on edge tables. Users can now use a single merge statement to insert a new edge or update an existing one between two nodes.

- **Derived Tables and Views support for graph tables** allow you to put together multiple graph objects (node or edge tables) into a single object (node or edge) with the help of `UNION ALL` operator in a derived table or view. With the help of views or derived tables created on node or edge tables, users can now use a single `MATCH` query to identify heterogeneously connected data points in their database. For example, find all the relationships that two people in a graph share with each other or find all the entities that a given person in the graph is connected to.

- **Edge Constraints** are introduced for edge tables in SQL Graph. Edge tables can connect any node to any other node in the database. With introduction of edge constraints, you can now apply some restrictions on this behavior. The new `CONNECTION` constraint can be used to specify the type of nodes a given edge table will be allowed to connect to in the schema.

## <a id="sqllinux"></a> SQL Server on Linux

- **Replication support**. CTP 2.0 supports SQL Server Replication on Linux. A Linux virtual machine with SQL Agent can be a publisher, distributor, or subscriber. 

  Create the following types of publications:
  - Transactional
  - Snapshot
  - Merge

  CTP 2.0 does not support configuration of replication with the user interface. Use [replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

- **Support for the Microsoft Distributed Transaction Coordinator (MSDTC)**: SQL Server 2019 on Linux supports the Microsoft Distributed Transactions Coordinator (MSDTC).

- **Always On Availability Group on Docker containers with Kubernetes**: Kubernetes can orchestrate containers running SQL Server instances to provide a highly available set of databases with SQL Server Always On Availability Groups. A Kubernetes operator deploys a StatefulSet including a container with **mssql-server container** and a health monitor.

- **OpenLDAP support for third-party AD providers**: SQL Server on Linux supports OpenLDAP, which allows third-party providers to join Active Directory.

- **Machine Learning on Linux**: SQL Server 2019 Machine Learning Services (In-Database) is now supported on Linux. For instructions on how to install Machine Learning Services on Linux, see [Install SQL Server 2019 Machine Learning Services R and Python support on Linux](../linux/sql-server-linux-setup-machine-learning.md).

- **New container registry**: All container images for SQL Server 2019 as well as SQL Server 2017 are now located in the Microsoft Container Registry.

- **New RHEL-based container images**: New certified RHEL-based container images are now available.

## <a id="polybase"></a> PolyBase

- **New connectors for SQL Server, Oracle, Teradata, and MongoDB**: SQL Server 2019 introduces new connectors to external data for SQL Server, Oracle, Teradata, and MongoDB.

## <a id="ml"></a> SQL Server Machine Learning Services

- **Machine Learning on Linux**: SQL Server 2019 Machine Learning Services (In-Database) is now supported on Linux. For instructions on how to install Machine Learning Services on Linux, see [Install SQL Server 2019 Machine Learning Services R and Python support on Linux](../linux/sql-server-linux-setup-machine-learning.md).

- **Partition-based modeling**: Process external scripts per partition of your data using the new parameters added to `sp_execute_external_script`. This functionality supports training many small models (one model per partition of data) instead of one large model.

- **Windows Server Failover Cluster**: Configure high availability for Machine Learning Services on a Windows Server Failover Cluster.

For detailed information, see [What's new in SQL Server Machine Learning Services](../advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md).

## <a id="programmability"></a> Programmability extensions

- **Java language extension**: Use the Java language extension to execute Java code in SQL Server. In CTP2.0, this extension is installed when you add the feature 'Machine Learning Services (in-database)' to your SQL Server instance.

## <a id="security"></a>Security

- **Always Encrypted with secure enclaves**: Expands upon Always Encrypted with in-place encryption and rich computations, by enabling computations on plaintext data inside a secure enclave on the server side. For details, see [Always Encrypted with secure enclaves](../relational-databases/security/encryption/always-encrypted-enclaves.md).

- **Certificate management in SQL Server Configuration Manager**: SSL/TLS certificates are widely used to secure access to SQL Server instances. Certificate management is now integrated into the SQL Server Configuration Manager, simplifying common tasks such as:

  - Viewing and validating certificates installed in a SQL Server instance. 
  - Viewing certificates close to expiration.
  - Deploy certificates across machines participating in Always On Availability Groups (from the node holding the primary replica).
  - Deploy certificates across machines participating in a failover cluster instance (from the active node).

Note: User must have administrator permissions on all the cluster nodes.
## Next steps

See the [SQL Server 2019 Release Notes](sql-server-vnext-release-notes.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
