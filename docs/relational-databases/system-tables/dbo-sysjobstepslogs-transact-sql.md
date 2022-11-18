---
title: "dbo.sysjobstepslogs (Transact-SQL)"
description: dbo.sysjobstepslogs (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.sysjobstepslogs_TSQL"
  - "sysjobstepslogs_TSQL"
  - "sysjobstepslogs"
  - "dbo.sysjobstepslogs"
helpviewer_keywords:
  - "sysjobstepslogs system table"
dev_langs:
  - "TSQL"
ms.assetid: 128c25db-0b71-449d-bfb2-38b8abcf24a0
---
# dbo.sysjobstepslogs (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains the job step log for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job steps that are configured to write job step output to a table. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**log_id**|**int**|ID of the job step log.|  
|**log**|**nvarchar(max)**|Job step log contents.|  
|**date_created**|**datetime**|Date and time that the job step log was created.|  
|**date_modified**|**datetime**|Date and time that the job step log was last modified.|  
|**log_size**|**int**|Size of the job step log in bytes.|  
|**step_uid**|**uniqueidentifier**|Unique identifier of the job step.|  
  
## See Also  
 [sp_help_jobsteplog &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-jobsteplog-transact-sql.md)   
 [sp_delete_jobsteplog &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-jobsteplog-transact-sql.md)  
  
  
