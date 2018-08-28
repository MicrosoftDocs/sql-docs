---
title: "What's new in SQL Server vNext | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2018"
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
# What's new in SQL Server vNext

[!INCLUDE[tsql-appliesto-ssvnext-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssvnext-xxxx-xxxx-xxx.md)]

[!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud, and operating systems. This article summarizes what is new for SQL Server vNext. For more information and known issues, see the [SQL Server vNext Release Notes](sql-server-vnext-release-notes.md).

**Try SQL Server vNext!**
- [![Download from Evaluation Center](../includes/media/download2.png)](http://go.microsoft.com/fwlink/?LinkID=829477) [Download SQL Server vNext to install on Windows](http://go.microsoft.com/fwlink/?LinkID=829477)
- Install on Linux for [Red Hat Enterprise Server](../linux/quickstart-install-connect-red-hat.md), [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md), and [Ubuntu](../linux/quickstart-install-connect-ubuntu.md).
- [Run on SQL Server vNext on Docker](../linux/quickstart-install-connect-docker.md).

## CTP 2.0 

Community technology preview (CTP) 2.0 is the first public release of [!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)]. The following features are added or enhanced for [!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)] CTP 2.0.

- Database engine
  - Intelligent query processing
- SQL Server on Linux
  - Replication support
  - Support for the Microsoft Distributed Transaction Coordinator (MSDTC)
  - Always On Availability Group on Docker containers with Kubernetes
  - OpenLDAP support for third-party AD providers
  - Machine Learning support
  - New container registry
- PolyBase
  - New connectors for SQL Server, Oracle, Teradata, and MongoDB.
- SQL Server Machine Learning Services
  - Machine Learning on Linux
  - Partition-based modeling
  - Failover cluster support
  - Java extensibility
- Security
  - Always Encrypted with enclaves
  - Database scoped default setting for online and resumable DDL operations

Continue reading for more details about these features.

## Database Engine

### Intelligent query processing 

- Database **COMPATIBILITY_LEVEL 150** is added. To enable for a specific user database, execute:

   ```sql
   ALTER DATABASE database_name SET COMPATIBILITY_LEVEL =  150;
   ```

- **Row mode memory grant feedback** expands on the memory grant feedback feature introduced in SQL Server 2017 by adjusting memory grant sizes for both batch and row mode operators.  For an excessive memory grant condition, if the granted memory is more than two times the size of the actual used memory, memory grant feedback will recalculate the memory grant. Consecutive executions will then request less memory. For an insufficiently sized memory grant that results in a spill to disk, memory grant feedback will trigger a recalculation of the memory grant.  Consecutive executions will then request more memory. This feature is enabled by default under database compatibility level 150.

- **Approximate COUNT DISTINCT** returns the approximate number of unique non-null values in a group. This function is designed for use in big data scenarios and is optimized for the following conditions:
  - Access of data sets that are millions of rows or higher AND
  - Aggregation of a column or columns that have a large number of distinct values
  - Responsiveness is more critical than absolute precision.
  In this case, `APPROXIMATE_COUNT_DISTINCT` yields results typically within 2% of the precise answer in a small fraction of the time.

- **Batch mode on rowstores** targets analytic queries. These queries are characterized as scanning many rows, and doing significant aggregations, sorts, and group-by operations across these rows. Batch mode has been reserved for queries which involve Columnstore indexes in previous releases. Performing scans and calculations on batches of 900 – 1000 rows at a time rather than row by row is much more efficient for queries where these sorts of operations are common.  For queries that can take advantage of it, batch mode can easily make queries execute many times faster than the same query against the same data in row mode. This feature is enabled by default under database compatibility level 150.

- **Scalar UDF inlining** automatically transforms scalar user-defined functions (UDF) into relational expressions and embeds them in the calling SQL query, thereby improving the performance of workloads that leverage scalar UDFs. Scalar UDF inlining facilitates cost-based optimization of operations inside UDFs, and results in efficient plans that are set-oriented and parallel as opposed to inefficient, iterative, serial execution plans. This feature is enabled by default under database compatibility level 150.

- **Table variable deferred compilation** improves plan quality and overall performance for queries referencing table variables. During optimization and initial compilation, this feature will propagate cardinality estimates that are based on actual table variable row counts.  This accurate row count information will be used for optimizing downstream plan operations. This feature is enabled by default under database compatibility level 150.

## SQL Graph

- **Match support in `MERGE` DML** allows you to specify graph relationships in a single statement, instead of separate `INSERT`, `UPDATE`, or `DELETE` statements. Merge your current graph data from node or edge tables with new data using the `MATCH` predicates in the `MERGE` statement. This enables `UPSERT` scenarios on edge tables. Users can now use a single merge statement to insert a new edge or update an existing one between two nodes.

- **Derived Tables and Views support for graph tables** allow you to put together multiple graph objects (node or edge tables) into a single object (node or edge) with the help of `UNION ALL` operator in a derived table or view. With the help of views or derived tables created on node or edge tables, users can now use a single `MATCH` query to identify heterogeneously connected data points in their database. For example, find all the relationships that two people in a graph share with each other or find all the entities that a given person in the graph is connected to.

- **Edge Constraints** are introduced for edge tables in SQL Graph. Edge tables can connect any node to any other node in the database. With introduction of edge constraints, you can now apply some restrictions on this behavior. The new `CONNECTION` constraint can be used to specify the type of nodes a given edge table will be allowed to connect to in the schema.

## SQL Server on Linux

- **Replication support**. CTP 2.0 supports SQL Server Replication on Linux. A Linux virtual machine with SQL Agent can be a publisher, distributor, or subscriber. 

  Create the following types of publications:
  - Transactional
  - Snapshot
  - Merge

  CTP 2.0 does not support configuration of replication with the user interface. Use [replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

- **Support for the Microsoft Distributed Transaction Coordinator (MSDTC)**: SQL Server vNext on Linux supports the Microsoft Distributed Transactions Coordinator (MSDTC).

- **Always On Availability Group on Docker containers with Kubernetes**: Kubernetes can orchestrate containers running SQL Server instances to provide a highly available set of databases with SQL Server Always On Availability Groups. A Kubernetes operator deploys a StatefulSet including a container with **mssql-server container** and a health monitor. 

- **OpenLDAP support for third-party AD providers**: SQL Server on Linux supports OpenLDAP, which allows third-party providers to join Active Directory.

- **Machine Learning on Linux**: Linux now supports SQL Server vNext Machine Learning Services (In-Database). For instructions on how to install Machine Learning Services on Linux, see [Install SQL Server vNext Machine Learning Services R and Python support on Linux](../linux/sql-server-linux-setup-machine-learning.md).

- **New container registry**: Certified container images for SQL Server vNext as well as SQL Server 2017 are now located in the Microsoft Container Registry.

## PolyBase

- **New connectors for SQL Server, Oracle, Teradata, and MongoDB**: SQL Server vNext introduces new connectors to external data for SQL Server, Oracle, Teradata, and MongoDB.

## SQL Server Machine Learning Services

- **Machine Learning on Linux**: Linux now supports SQL Server vNext Machine Learning Services (In-Database). For instructions on how to install Machine Learning Services on Linux, see [Install SQL Server vNext Machine Learning Services R and Python support on Linux](../linux/sql-server-linux-setup-machine-learning.md).

- **Partition-based modeling**: Process external scripts per partition of your data using the new parameters added to `sp_execute_external_script`. This functionality supports training many small models (one model per partition of data) instead of one large model.

- **Windows Server Failover Cluster**: Configure high availability for Machine Learning Services on a Windows Server Failover Cluster.

- **Java extensibility**: Use the Java language extension with Machine Learning.

For detailed information, see [What's new in SQL Server Machine Learning Services](../advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md).

## Security

- Database scoped default setting for online and resumable DDL operations.

## Next steps

See the [SQL Server vNext Release Notes](sql-server-vnext-release-notes.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
