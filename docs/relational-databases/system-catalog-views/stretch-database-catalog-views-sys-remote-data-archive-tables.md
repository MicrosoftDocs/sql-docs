---
title: "sys.remote_data_archive_tables (Transact-SQL)"
description: Learn how sys.remote_data_archive_tables contains one row for each remote table that stores data from a Stretch-enabled local table.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: 07/25/2022
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sys.remote_data_archive_tables"
  - "sys.remote_data_archive_tables_TSQL"
  - "remote_data_archive_tables"
  - "remote_data_archive_tables_TSQL"
helpviewer_keywords:
  - "sys.remote_data_archive_tables catalog view"
dev_langs:
  - "TSQL"
---
# Stretch Database Catalog Views - sys.remote_data_archive_tables

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Contains one row for each remote table that stores data from a Stretch-enabled local table.

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|The object ID of the Stretch-enabled local table.|  
|**remote_database_id**|**int**|The auto-generated local identifier of the remote database.|  
|**remote_table_name**|**sysname**|The name of the table in the remote database that corresponds to the Stretch-enabled local table.|  
|**filter_predicate**|**nvarchar(max)**|The filter predicate, if any, that identifies rows in the table to be migrated. If the value is null, the entire table is eligible to be migrated.<br /><br /> For more info, see [Enable Stretch Database for a table](../../sql-server/stretch-database/enable-stretch-database-for-a-table.md) and [Select rows to migrate by using a filter predicate](~/sql-server/stretch-database/select-rows-to-migrate-by-using-a-filter-function-stretch-database.md).|  
|**migration_direction**|**tinyint**|The direction in which data is currently being migrated. The available values are the following.<br/>1 (outbound)<br/>2 (inbound)|  
|**migration_direction_desc**|**nvarchar(60)**|The description of the direction in which data is currently being migrated. The available values are the following.<br/>outbound (1)<br/>inbound (2)|  
|**is_migration_paused**|**bit**|Indicates whether migration is currently paused.|  
|**is_reconciled**|**bit**| Indicates whether the remote table and the SQL Server table are in sync.<br/><br/>When the value of **is_reconciled** is 1 (true), the remote table and the SQL Server table are in sync, and you can run queries that include the remote data.<br/><br/>When the value of **is_reconciled** is 0 (false), the remote table and the SQL Server table are not in sync. Recently migrated rows have to be migrated again. This occurs when you restore the remote Azure database, or when you delete rows manually from the remote table. Until you reconcile the tables, you can't run queries that include the remote data. To reconcile the tables, run [sys.sp_rda_reconcile_batch](../../relational-databases/system-stored-procedures/sys-sp-rda-reconcile-batch-transact-sql.md). |

## See also

- [Stretch Database](../../sql-server/stretch-database/stretch-database.md)  
