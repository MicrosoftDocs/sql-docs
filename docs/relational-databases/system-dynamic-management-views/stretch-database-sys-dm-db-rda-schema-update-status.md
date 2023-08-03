---
title: "sys.dm_db_rda_schema_update_status (Transact-SQL)"
description: Learn how sys.dm_db_rda_schema_update_status contains a row for each schema update task for the remote data archive of each Stretch-enabled table in the database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_rda_schema_update_status"
  - "sys.dm_db_rda_schema_update_status_TSQL"
  - "dm_db_rda_schema_update_status"
  - "dm_db_rda_schema_update_status_TSQL"
helpviewer_keywords:
  - "sys.dm_db_rda_schema_update_status dynamic management view"
dev_langs:
  - "TSQL"
---
# Stretch Database - sys.dm_db_rda_schema_update_status

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Contains one row for each schema update task for the remote data archive of each Stretch-enabled table in the current database. Tasks are identified by their task ids.

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

`sys.dm_db_rda_schema_update_status` is scoped to the current database context. Make sure you are in the database context of the Stretch-enabled table for which you want to see schema update status.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**table_id**|**int**|The ID of the local Stretch-enabled table whose remote data archive schema is being updated.|  
|**database_id**|**int**|The ID of the database that contains the local Stretch-enabled table.|  
|**task_id**|**bigint**|The ID of the remote data archive schema update task.|  
|**task_type**|**int**|The type of the remote data archive schema update task.|  
|**task_type_desc**|**nvarchar**|The description of the type of the remote data archive schema update task.|  
|**task_state**|**int**|The state of the remote data archive schema update task.|  
|**task_state_des**|**nvarchar**|The description of the state of the remote data archive schema update task.|  
|**start_time_utc**|**datetime**|The UTC time at which the remote data archive schema update started.|  
|**end_time_utc**|**datetime**|The UTC time at which the remote data archive schema update finished.|  
|**error_number**|**int**|If the remote data archive schema update fails, the error number of the error that occurred; otherwise, null.|  
|**error_severity**|**int**|If the remote data archive schema update fails, the severity of the error that occurred; otherwise, null.|  
|**error_state**|**int**|If the remote data archive schema update fails, the state of the error that occurred; otherwise, null. The error_state indicates the condition or location where the error occurred.|

## See also

- [Stretch Database](../../sql-server/stretch-database/stretch-database.md)
