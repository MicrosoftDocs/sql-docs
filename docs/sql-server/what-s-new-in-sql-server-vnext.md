---
title: "What's new in SQL Server vNext | Microsoft Docs"
ms.custom: ""
ms.date: "04/11/2018"
ms.prod: "sql-server-2018"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "server-general"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "craigg-msft"
ms.author: "craigg"
manager: "craigg"
---
# What's new in SQL Server vNext

SQL Server vNext builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud, and operating systems. This article summarizes what is new for SQL Server vNext. For more information and known issues, see the [SQL Server vNext Release Notes](sql-server-vnext-release-notes.md).

**Try SQL Server vNext!**
- [![Download from Evaluation Center](../includes/media/download2.png)](http://go.microsoft.com/fwlink/?LinkID=829477) [Download SQL Server vNext to install on Windows](http://go.microsoft.com/fwlink/?LinkID=829477)
- Install on Linux for [Red Hat Enterprise Server](../linux/quickstart-install-connect-red-hat.md), [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md), and [Ubuntu](../linux/quickstart-install-connect-ubuntu.md).
- [Run on SQL Server vNext on Docker](../linux/quickstart-install-connect-docker.md).

## CTP 1.4 (March - 2018)

The following list summarizes new improvements and updates in SQL Server vNext CTP 1.4.

- Database engine improvement example.
- SQL Server on Linux added support for replication.

The following sections summarize all of the updates for SQL Server vNext by area.

## Database Engine

- **Row mode memory grant feedback** expands on the memory grant feedback feature introduced in SQL Server 2017 by adjusting memory grant sizes for both batch and row mode operators.  For an excessive memory grant condition, if the granted memory is more than two times the size of the actual used memory, memory grant feedback will recalculate the memory grant. Consecutive executions will then request less memory. For an insufficiently sized memory grant that results in a spill to disk, memory grant feedback will trigger a recalculation of the memory grant.  Consecutive executions will then request more memory.  To enable the public preview of row mode memory grant feedback, enable database compatibility level 150 for the database you are connected to when executing the query.

- We are also introducing two new query execution plan attributes for better visibility into the current state of a memory grant feedback operation for both row and batch mode with the **IsMemoryGrantFeedbackAdjusted** and **LastRequestedMemory** attributes added to the MemoryGrantInfo query plan XML element. The new LastRequestedMemory attribute shows the granted memory in Kilobytes (KB) from the prior query execution. The new IsMemoryGrantFeedbackAdjusted attribute allows you to check the state of memory grant feedback for the statement within an actual query execution plan. 

## SQL Server on Linux

- Replication support. (CTP 1.4)

## Next steps

See the [SQL Server vNext Release Notes](sql-server-vnext-release-notes.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
