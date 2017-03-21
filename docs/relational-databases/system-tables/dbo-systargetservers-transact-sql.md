---
title: "dbo.systargetservers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.systargetservers_TSQL"
  - "dbo.systargetservers"
  - "systargetservers_TSQL"
  - "systargetservers"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "systargetservers system table"
ms.assetid: 479d1314-be37-4d19-ac9c-419fc9110e53
caps.latest.revision: 28
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# dbo.systargetservers (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Records which target servers are currently enlisted in this multiserver operation domain.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**server_id**|**int**|Server ID.|  
|**server_name**|**sysname**|Server name.|  
|**location**|**nvarchar(200)**|Location of the specified target server.|  
|**time_zone_adjustment**|**int**|Time adjustment interval, in hours, from Greenwich mean time (GMT).|  
|**enlist_date**|**datetime**|Date and time that the specified target server was enlisted.|  
|**last_poll_date**|**datetime**|Date and time that the specified target server last polled the multiserver's **sysdownloadlist** system table for jobs to run.|  
|**status**|**int**|Status of the target server:<br /><br /> **1** = Normal<br /><br /> **2** = Re-sync Pending<br /><br /> **4** = Suspected Offline|  
|**local_time_at_last_poll**|**datetime**|Date and time the target server was last polled for job operations.|  
|**enlisted_by_nt_user**|**nvarchar(100)**|Username of the person executing **sp_msx_enlist** on the target server.|  
|**poll_internal**|**int**|Number of seconds to elapse before the target server polls the master server for new download instructions.|  
  
  