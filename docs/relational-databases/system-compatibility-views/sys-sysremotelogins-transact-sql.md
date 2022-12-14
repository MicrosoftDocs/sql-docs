---
description: "sys.sysremotelogins (Transact-SQL)"
title: "sys.sysremotelogins (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysremotelogins"
  - "sysremotelogins_TSQL"
  - "sys.sysremotelogins"
  - "sys.sysremotelogins_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysremotelogins system table"
  - "sys.sysremotelogins compatibility view"
ms.assetid: b7ffcfa6-aed8-41d4-8b70-845439ab813d
author: rwestMSFT
ms.author: randolphwest
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
  
  
