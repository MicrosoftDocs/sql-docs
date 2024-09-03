---
title: Requirements for using memory-optimized tables
description: Learn about the requirements for using In-Memory OLTP, including SQL Database version, memory & storage considerations, and installation.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/05/2023
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
---
# Requirements for using memory-optimized tables

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes the requirements for adoption of In-Memory features in SQL Server.

## Requirements

In addition to the [SQL Server 2022: Hardware and software requirements](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md), the following are requirements to use [!INCLUDE [inmemory-md](../../includes/inmemory-md.md)]:

- [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP 1 and later versions, any edition. For [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] RTM (pre-SP1), you need Enterprise, Developer, or Evaluation edition.

- [!INCLUDE [inmemory-md](../../includes/inmemory-md.md)] requires the 64-bit version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] needs enough memory to hold the data in memory-optimized tables and indexes, and extra memory to support the online workload. For more information, see [Estimate Memory Requirements for Memory-Optimized Tables](estimate-memory-requirements-for-memory-optimized-tables.md).

- When running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in a virtual machine (VM), ensure there's enough memory allocated to the VM to support the memory needed for memory-optimized tables and indexes. Depending on the VM host application, the configuration option to guarantee memory allocation for the VM could be called Memory Reservation or, when using Dynamic Memory, Minimum RAM. Make sure these settings are sufficient for the needs of the databases in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Free disk space that is two times the size of your durable memory-optimized tables.

- A processor needs to support the instruction `cmpxchg16b` to use [!INCLUDE [inmemory-md](../../includes/inmemory-md.md)]. All modern 64-bit processors support `cmpxchg16b`.

  If you use a virtual machine and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] displays an error caused by an older processor, see if the VM host application has a configuration option to allow `cmpxchg16b`. If not, you could use Hyper-V, which supports `cmpxchg16b` without needing to modify a configuration option.

- [!INCLUDE [inmemory-md](../../includes/inmemory-md.md)] is installed as part of **Database Engine Services**.

  To install report generation ([Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md)) and [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (to manage [!INCLUDE [inmemory-md](../../includes/inmemory-md.md)] via [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer), [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

> [!NOTE]
> - For more information specific to in-memory data in Azure SQL Database, see [Optimize performance by using in-memory technologies in Azure SQL Database](/azure/azure-sql/database/in-memory-oltp-overview?view=azuresql-db&preserve-view=true) and [Blog: [!INCLUDE [inmemory](../../includes/inmemory-md.md)] in Azure SQL Database](https://azure.microsoft.com/blog/in-memory-oltp-in-azure-sql-database/).
> - For more information specific to in-memory data in Azure SQL Managed Instance, see [Optimize performance by using in-memory technologies in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/in-memory-oltp-overview?view=azuresql-mi&preserve-view=true).

## Important notes on using [!INCLUDE [inmemory](../../includes/inmemory-md.md)]

- In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, there is no limit on the size of memory-optimized tables, other than available memory.

- In [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], the total in-memory size of all durable tables in a database shouldn't exceed 250 GB. For more information, see [Estimate Memory Requirements for Memory-Optimized Tables](estimate-memory-requirements-for-memory-optimized-tables.md).

> [!NOTE]  
> Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP 1, Standard and Express editions support [!INCLUDE [inmemory-md](../../includes/inmemory-md.md)], but they impose quotas on the amount of memory you can use for memory-optimized tables in a given database. In Standard edition this is 32 GB per database; in Express edition this is 352MB per database.

- If you create one or more databases with memory-optimized tables, you should enable Instant File Initialization (IFI) by granting the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service startup account the *SE_MANAGE_VOLUME_NAME* user right. Without IFI, memory-optimized storage files (data and delta files) are initialized on creation, which can have a negative effect on the performance of your workload. For more information about IFI, including how to enable it, see [Database instant file initialization](../databases/database-instant-file-initialization.md).

- [!INCLUDE [known-issue-memory-optimized](../../includes/paragraph-content/known-issue-memory-optimized.md)]

## Related content

- [In-Memory OLTP overview and usage scenarios](overview-and-usage-scenarios.md)
- [Database instant file initialization](../databases/database-instant-file-initialization.md)
- [Memory management architecture guide](../memory-management-architecture-guide.md)
