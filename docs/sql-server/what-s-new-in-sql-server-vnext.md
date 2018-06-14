---
title: "What's new in SQL Server vNext | Microsoft Docs"
ms.custom: ""
ms.date: "07/15/2018"
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
monikerRange: "= sql-server-ver15 || = sqlallproducts-allversions"
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

- Database engine - Intelligent query processor
- SQL Server on Linux
  - Replication support
  - Active Directory Impersonation
  - Always On Availability Group on Docker containers with Kubernetes
- SQL Server Machine Learning Services - High availability with Windows Server failover cluster
- Security 
  - Always Encrypted with enclaves
  - Database scoped default setting for online and resumeable DDL operations 


Continue reading for more details about these features.

## Database Engine

### Intelligent query processor 

- **Row mode memory grant feedback** expands on the memory grant feedback feature introduced in SQL Server 2017 by adjusting memory grant sizes for both batch and row mode operators.  For an excessive memory grant condition, if the granted memory is more than two times the size of the actual used memory, memory grant feedback will recalculate the memory grant. Consecutive executions will then request less memory. For an insufficiently sized memory grant that results in a spill to disk, memory grant feedback will trigger a recalculation of the memory grant.  Consecutive executions will then request more memory. 

  <!-- 
  To enable the public preview of row mode memory grant feedback, enable database compatibility level 150 for the database you are connected to when executing the query.
  
  **IsMemoryGrantFeedbackAdjusted** and **LastRequestedMemory** attributes are added to the `MemoryGrantInfo` query plan XML element. These execution plan attributes provide better visibility into the current state of a memory grant feedback operation for both row and batch mode. **IsMemoryGrantFeedbackAdjusted** attribute allows you to check the state of memory grant feedback for the statement within an actual query execution plan. **LastRequestedMemory** attribute shows the granted memory in Kilobytes (KB) from the prior query execution.-->

- **Approximate COUNT DISTINCT**

- **Batch mode on rowstores**

- **Scalar UDF inlining** automatically transforms scalar user defined functions (UDF) into relational expressions and embeds them in the calling SQL query, thereby improving the performance of workloads that leverage scalar UDFs. Scalar UDF inlining facilitates cost-based optimization of operations inside UDFs, and results in efficient plans that are set-oriented and parallel as opposed to inefficient, iterative, serial execution plans.

- **Table variable deferred compilation** improves plan quality and overall performance for queries referencing table variables. During optimization and initial compilation, this feature will propagate cardinality estimates that are based on actual table variable row counts.  This accurate row count information will be used for optimizing downstream plan operations.

## SQL Server on Linux

- **Replication support**. CTP 2.0 supports SQL Server Replication on Linux. A Linux virtual machine with SQL Agent can be a publisher, distributor, or subscriber. 

  Create the following types of publications:
  - Transactional
  - Snapshot
  - Merge

  CTP 2.0 does not support configuration of replication with the user interface. Use [replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).
  
- **Always On Availability Group on Docker containers with Kubernetes**. Kubernetes can orchestrate containers running SQL Server instances to provide a highly available set of databases with SQL Server Always On Availability Groups. A Kubernetes operator deploys a StatefulSet including an **mssql-server container** and a container for a health monitoring agent. An additional **AG agent container** is deployed to each pod of the StatefulSet  to automatically create and monitor the availability group and fail over in case of health issues.

- **Active Directory impersonation**

## SQL Server Machine Learning Services

- **sp_execute_external_script**: Process external scripts per partition of your data using the new parameters added to `sp_execute_external_script`. This functionality supports training many small models (one model per partition of data) instead of one large model.

- **Windows Server Failover Cluster**: Configure high availability for Machine Learning Services on a Windows Server Failover Cluster.

For detailed information, see [What's new in SQL Server Machine Learning Services](../advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md).

## Security

- Database scoped default setting for online and resumable DDL operations.

## Next steps

See the [SQL Server vNext Release Notes](sql-server-vnext-release-notes.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
