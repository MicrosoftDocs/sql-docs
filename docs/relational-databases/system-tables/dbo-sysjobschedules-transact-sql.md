---
title: "dbo.sysjobschedules (Transact-SQL)"
description: dbo.sysjobschedules (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/09/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysjobschedules"
  - "dbo.sysjobschedules"
  - "dbo.sysjobschedules_TSQL"
  - "sysjobschedules_TSQL"
helpviewer_keywords:
  - "sysjobschedules system table"
dev_langs:
  - "TSQL"
ms.assetid: ccdafec7-2a9b-4356-bffb-1caa3a12db59
---
# dbo.sysjobschedules (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains schedule information for jobs to be executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. This table is stored in the **msdb** database.  
  
> [!NOTE]  
> The **sysjobschedules** table refreshes every 20 minutes, which may affect the values returned by the **sp_help_jobschedule** stored procedure.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**schedule_id**|**int**|ID of the schedule.|  
|**job_id**|**uniqueidentifier**|ID of the job.|  
|**next_run_date**|**int**|Next date on which the job is scheduled to run. The date is formatted YYYYMMDD.|  
|**next_run_time**|**int**|Time at which the job is scheduled to run. The time is formatted HHMMSS, and uses a 24-hour clock.|  
  
## See Also  
 [dbo.sysschedules &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysschedules-transact-sql.md)  
  
  
