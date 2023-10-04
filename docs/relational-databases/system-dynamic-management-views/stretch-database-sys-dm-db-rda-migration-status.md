---
title: "sys.dm_db_rda_migration_status (Transact-SQL)"
description: Learn how sys.dm_db_rda_migration_status contains one row for each batch of migrated data from each Stretch-enabled table on the local instance of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_rda_migration_status"
  - "sys.dm_db_rda_migration_status_TSQL"
  - "dm_db_rda_migration_status"
  - "dm_db_rda_migration_status_TSQL"
helpviewer_keywords:
  - "sys.dm_db_rda_migration_status dynamic management view"
dev_langs:
  - "TSQL"
---
# Stretch Database - sys.dm_db_rda_migration_status

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Contains one row for each batch of migrated data from each Stretch-enabled table on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Batches are identified by their start time and end time.

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

`sys.dm_db_rda_migration_status` is scoped to the current database context. Make sure you're in the database context of the Stretch-enable tables for which you want to see migration status.

In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], the output of **sys.dm_db_rda_migration_status** is limited to 200 rows.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**table_id**|**int**|The ID of the table from which rows were migrated.|  
|**database_id**|**int**|The ID of the database from which rows were migrated.|  
|**migrated_rows**|**bigint**|The number of rows migrated in this batch.|  
|**start_time_utc**|**datetime**|The UTC time at which the batch started.|  
|**end_time_utc**|**datetime**|The UTC time at which the batch finished.|  
|**error_number**|**int**|If the batch fails, the error number of the error that occurred; otherwise, null.|  
|**error_severity**|**int**|If the batch fails, the severity of the error that occurred; otherwise, null.|  
|**error_state**|**int**|If the batch fails, the state of the error that occurred; otherwise, null.<br /><br /> The **error_state** indicates the condition or location where the error occurred.|

## See also

- [Stretch Database](../../sql-server/stretch-database/stretch-database.md)  
