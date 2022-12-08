---
title: "dbo.sysjobservers (Transact-SQL)"
description: dbo.sysjobservers (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/26/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysjobservers"
  - "sysjobservers_TSQL"
  - "dbo.sysjobservers"
  - "dbo.sysjobservers_TSQL"
helpviewer_keywords:
  - "sysjobservers system table"
dev_langs:
  - "TSQL"
ms.assetid: 9abcc20f-a421-4591-affb-62674d04575e
---
# dbo.sysjobservers (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stores the association or relationship of a particular job with one or more target servers. This table is stored in the msdb database.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|job_id|**uniqueidentifier**|Job identification number.|  
|server_id|**int**|Server identification number.|  
|last_run_outcome|**tinyint**|Outcome for the job's last run:<br /><br /> **0** = Fail<br /><br /> **1** = Succeed<br /><br /> **2** = Retry<br /><br /> **3** = Cancel<br /><br /> **4** = In progress<br /><br /> **5** = Unknown (see the following Remarks section) |  
|last_outcome_ message|**nvarchar(1024)**|Associated message, if any, with the last_run_outcome column.|  
|last_run_date|**int**|Date the job was last run.|  
|last_run_time|**int**|Time the job was last run.|  
|last_run_duration|**int**|Duration that the job was run, in hours, minutes, and seconds. Computed by using the formula: (*hours*\*10000) + (*minutes*\*100) + *seconds*.|  


## Remarks

A value above *4* means that the SQL Agent does not know the state of that job. The *last_run_outcome* is initially set to *5* when a job gets created.


## See Also

[SQL Server Agent Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sql-server-agent-tables-transact-sql.md)  
