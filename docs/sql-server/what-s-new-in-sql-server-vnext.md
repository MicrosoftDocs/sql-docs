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
---
# What's new in SQL Server vNext

[!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud, and operating systems. This article summarizes what is new for SQL Server vNext. For more information and known issues, see the [SQL Server vNext Release Notes](sql-server-vnext-release-notes.md).

**Try SQL Server vNext!**
- [![Download from Evaluation Center](../includes/media/download2.png)](http://go.microsoft.com/fwlink/?LinkID=829477) [Download SQL Server vNext to install on Windows](http://go.microsoft.com/fwlink/?LinkID=829477)
- Install on Linux for [Red Hat Enterprise Server](../linux/quickstart-install-connect-red-hat.md), [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md), and [Ubuntu](../linux/quickstart-install-connect-ubuntu.md).
- [Run on SQL Server vNext on Docker](../linux/quickstart-install-connect-docker.md).

## CTP 2.0 

Community technology preview (CTP) 2.0 is the first public release of [!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)]. The following list summarizes new improvements and updates in [!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)] CTP 2.0.

- Database engine improvement example.
- SQL Server on Linux added support for replication.
- SQL Server Machine Learning Services

The following sections summarize all of the updates for SQL Server vNext by area.

## Database Engine

- **Row mode memory grant feedback** expands on the memory grant feedback feature introduced in SQL Server 2017 by adjusting memory grant sizes for both batch and row mode operators.  For an excessive memory grant condition, if the granted memory is more than two times the size of the actual used memory, memory grant feedback will recalculate the memory grant. Consecutive executions will then request less memory. For an insufficiently sized memory grant that results in a spill to disk, memory grant feedback will trigger a recalculation of the memory grant.  Consecutive executions will then request more memory.  To enable the public preview of row mode memory grant feedback, enable database compatibility level 150 for the database you are connected to when executing the query.

- **IsMemoryGrantFeedbackAdjusted** and **LastRequestedMemory** attributes are added to the `MemoryGrantInfo` query plan XML element. These execution plan attributes provide better visibility into the current state of a memory grant feedback operation for both row and batch mode. **IsMemoryGrantFeedbackAdjusted** attribute allows you to check the state of memory grant feedback for the statement within an actual query execution plan. **LastRequestedMemory** attribute shows the granted memory in Kilobytes (KB) from the prior query execution.

- Intelligent query processing introduces
  - **Approximate COUNT DISTINCT**
  - **Batch mode on rowstores**
  - **Memory grant feedback for row mode**
  - **Scalar T-SQL inline user-defined function (UDF)**
  - **Table variable deferred compilation**

## SQL Server on Linux

- Replication support.

- Active Directory impersonation.

## SQL Server Machine Learning Services

- **sp_execute_external_script**: Process external scripts per partition of your data using the new parameters added to `sp_execute_external_script`. This functionality supports training many small models (one model per partition of data) instead of one large model.

- **Windows Server Failover Cluster**: Configure high availability with Machine Learning Services on a Windows Server Failover Cluster. 

For detailed information, see [What's new in SQL Server Machine Learning Services](../advanced-analytics/what-s-new-in-sql-server-machine-learning-services.md).

## Security

- Database scoped default setting for online and resumable DDL operations. 


## Next steps

See the [SQL Server vNext Release Notes](sql-server-vnext-release-notes.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
