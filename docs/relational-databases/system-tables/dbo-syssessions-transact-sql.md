---
title: "dbo.syssessions (Transact-SQL)"
description: dbo.syssessions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "12/30/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.syssessions_TSQL"
  - "dbo.syssessions"
  - "syssessions_TSQL"
  - "syssessions"
helpviewer_keywords:
  - "syssessions system table"
dev_langs:
  - "TSQL"
ms.assetid: 187819b6-c7f4-4a26-b74c-0a89e96695cf
---
# dbo.syssessions (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Each time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts, it creates a new session. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent uses sessions to preserve the status of jobs when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is restarted or stopped unexpectedly. Each row of the **syssessions** table contains information about one session. Use the **sysjobactivity** table to view the job state at the end of each session.  
  
 This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|ID of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent session. This session_id is not the SPID for the session, but rather an IDENTITY value within this system table.|  
|**agent_start_date**|**datetime**|Date and time that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service was started for this session.|  
  
## Remarks  
 Only users who are members of the **sysadmin** fixed server role can access this table.  
  
## See Also  
 [dbo.sysjobactivity &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysjobactivity-transact-sql.md)  
  
  
