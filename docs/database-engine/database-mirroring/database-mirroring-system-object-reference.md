---
title: "Database mirroring system object reference"
description: "See information about database mirroring system objects: system catalog views, system dynamic management views, and system tables."
author: MashaMSFT
ms.author: mathoma
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
---
# Database mirroring system object reference
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## System catalog views

| System catalog view | Description|
| :------ | :----------------------------- |
| [sys.database_mirroring_witnesses](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)   | Contains a row for every witness role that a server plays in a database mirroring partnership. |

## System dynamic management views

| System dynamic management view | Description|
| :------ | :----------------------------- |
| [sys.dm_db_mirroring_auto_page_repair](../../relational-databases/system-dynamic-management-views/database-mirroring-sys-dm-db-mirroring-auto-page-repair.md)   | Returns a row for every automatic page-repair attempt on any mirrored database on the server instance.  |
| [sys.dm_db_mirroring_connections](../../relational-databases/system-dynamic-management-views/database-mirroring-sys-dm-db-mirroring-connections.md)    | Returns a row for each connection established for database mirroring. |

## System tables

| System table | Description|
| :------ | :----------------------------- |
| [sysdbmaintplan_databases](../../relational-databases/system-tables/sysdbmaintplan-databases-transact-sql.md)   | Returns information about database mirroring maintenance plans. |
| [sysdbmaintplan_history](../../relational-databases/system-tables/sysdbmaintplan-history-transact-sql.md)    | Returns information about the history of database mirroring maintenance plans. |
| [sysdbmaintplan_jobs](../../relational-databases/system-tables/sysdbmaintplan-jobs-transact-sql.md)    |Returns information about database mirroring maintenance plans jobs.  |
| [sysdbmaintplans](../../relational-databases/system-tables/sysdbmaintplans-transact-sql.md)    | Returns information about database mirroring plans.  |


## See Also  
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   

  
  
