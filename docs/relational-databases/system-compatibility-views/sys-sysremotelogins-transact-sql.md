---
title: "sys.sysremotelogins (Transact-SQL)"
description: "sys.sysremotelogins (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysremotelogins"
  - "sysremotelogins_TSQL"
  - "sys.sysremotelogins"
  - "sys.sysremotelogins_TSQL"
helpviewer_keywords:
  - "sysremotelogins system table"
  - "sys.sysremotelogins compatibility view"
dev_langs:
  - "TSQL"
---
# sys.sysremotelogins (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each remote user that is permitted to call remote stored procedures on an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**remoteserverid**|**smallint**|Remote server identification.|  
|**remoteusername**|**sysname**|Login name of the user on a remote server.|  
|**status**|**smallint**|Returns 0.|  
|**sid**|**varbinary(85)**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user security ID.|  
|**changedate**|**datetime**|Date and time the remote user was added.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
