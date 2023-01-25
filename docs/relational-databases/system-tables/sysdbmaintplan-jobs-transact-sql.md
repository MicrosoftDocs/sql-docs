---
title: "sysdbmaintplan_jobs (Transact-SQL)"
description: sysdbmaintplan_jobs (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysdbmaintplan_jobs"
  - "sysdbmaintplan_jobs_TSQL"
helpviewer_keywords:
  - "sysdbmaintplan_jobs system table"
dev_langs:
  - "TSQL"
ms.assetid: bc65cd70-6ef2-4c17-be11-877ecf4efe50
---
# sysdbmaintplan_jobs (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This table is included in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to preserve existing information for instances upgraded from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not change the contents of this table. This table is stored in the **msdb** database.  
  
 [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**plan_id**|**uniqueidentifier**|Database maintenance plan ID.|  
|**job_id**|**uniqueidentifier**|ID of a job associated with the database maintenance plan.|  
  
  
