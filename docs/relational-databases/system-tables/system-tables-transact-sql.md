---
title: "System Tables (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
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
ms.assetid: 56b8ad51-930c-4e5c-8d99-8c939d5b70ac
author: stevestein
ms.author: sstein
manager: craigg
---
# System Tables (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

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
  
|||  
|-|-|  
|[Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)|[Log Shipping Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/log-shipping-tables-transact-sql.md)|  
|[Change Data Capture Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/change-data-capture-tables-transact-sql.md)|[Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)|  
|[Database Maintenance Plan Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/database-maintenance-plan-tables-transact-sql.md)|[SQL Server Agent Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sql-server-agent-tables-transact-sql.md)|  
|[SQL Server Extended Events Tables &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/6d52ff03-f5aa-4f0f-8c98-9b49dc76f94e)|[sys.sysoledbusers &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysoledbusers-transact-sql.md)|  
|[Integration Services Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/integration-services-tables-transact-sql.md)|[systranschemas &#40;Transact-SQL&#41;](../../relational-databases/system-views/systranschemas-transact-sql.md)|  
  
## See Also  
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
