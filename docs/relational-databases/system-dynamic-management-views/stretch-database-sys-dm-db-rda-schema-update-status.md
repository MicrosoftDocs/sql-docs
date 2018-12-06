---
title: "sys.dm_db_rda_schema_update_status (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_rda_schema_update_status"
  - "sys.dm_db_rda_schema_update_status_TSQL"
  - "dm_db_rda_schema_update_status"
  - "dm_db_rda_schema_update_status_TSQL"
helpviewer_keywords: 
  - "sys.dm_db_rda_schema_update_status dynamic management view"
ms.assetid: 364e3caa-a7c6-4be5-a029-0b19da34de3e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Stretch Database - sys.dm_db_rda_schema_update_status
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Contains one row for each schema update task for the remote data archive of each Stretch-enabled table in the current database. Tasks are identified by their task ids.  
  
 **dm_db_rda_schema_update_status** is scoped to the current database context. Make sure you are in the database context of the Stretch-enabled table for which you want to see schema update status.  
  
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
  
## See Also  
 [Stretch Database](../../sql-server/stretch-database/stretch-database.md)  
  
  
