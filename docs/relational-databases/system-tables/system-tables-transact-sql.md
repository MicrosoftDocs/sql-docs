---
title: "System Tables (Transact-SQL)"
description: System Tables (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "status information [SQL Server]"
  - "tables [SQL Server], system tables"
  - "information retrieval [SQL Server]"
  - "status information [SQL Server], system tables"
  - "system information [SQL Server]"
  - "system tables [SQL Server]"
  - "system tables [SQL Server], about system tables"
  - "system tables [SQL Server], retrieving information from"
  - "retrieving system table information"
dev_langs:
  - "TSQL"
ms.assetid: 56b8ad51-930c-4e5c-8d99-8c939d5b70ac
---
# System Tables (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The topics in this section describe the system tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The system tables should not be changed directly by any user. For example, do not try to modify system tables with DELETE, UPDATE, or INSERT statements, or user-defined triggers.  
  
 Referencing documented columns in system tables is permissible. However, many of the columns in system tables are not documented. Applications should not be written to directly query undocumented columns. Instead, to retrieve information stored in the system tables, applications should use any one of the following components:  
  
-   System stored procedures  
  
-   [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and functions  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO)  
  
-   Replication Management Objects (RMO)  
  
-   Database API catalog functions  
  
 These components make up a published API for obtaining system information from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[msCoName](../../includes/msconame-md.md)] maintains the compatibility of these components from release to release. The format of the system tables depends upon the internal architecture of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and may change from release to release. Therefore, applications that directly access the undocumented columns of system tables may have to be changed before they can access a later version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## In This Section  
 The system table topics are organized by the following feature areas:  

:::row:::
    :::column:::
        [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)

        [Change Data Capture Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/change-data-capture-tables-transact-sql.md)

        [Database Maintenance Plan Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/database-maintenance-plan-tables-transact-sql.md)

        [SQL Server Extended Events Tables &#40;Transact-SQL&#41;](../../relational-databases/extended-events/xevents-references-system-objects.md#system-tables)

        [Integration Services Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/integration-services-tables-transact-sql.md)
    :::column-end:::
    :::column:::
        [Log Shipping Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/log-shipping-tables-transact-sql.md)

        [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)

        [SQL Server Agent Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sql-server-agent-tables-transact-sql.md)

        [sys.sysoledbusers &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysoledbusers-transact-sql.md)

        [systranschemas &#40;Transact-SQL&#41;](../../relational-databases/system-views/systranschemas-transact-sql.md)
    :::column-end:::
:::row-end:::

## See Also  
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
