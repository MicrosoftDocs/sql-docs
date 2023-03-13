---
title: "sys.syscurconfigs (Transact-SQL)"
description: "sys.syscurconfigs (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.syscurconfigs"
  - "sys.syscurconfigs_TSQL"
  - "syscurconfigs"
  - "syscurconfigs_TSQL"
helpviewer_keywords:
  - "sys.syscurconfigs compatibility view"
  - "syscurconfigs system table"
dev_langs:
  - "TSQL"
---
# sys.syscurconfigs (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains an entry for each current configuration option. Also, this view contains four entries that describe the configuration structure. **syscurconfigs** is built dynamically when queried by a user. For more information, see [sys.sysconfigures &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysconfigures-transact-sql.md).  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**value**|**int**|User-modifiable value for the variable. This is used by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] only if RECONFIGURE has been executed.|  
|**config**|**smallint**|Configuration variable number.|  
|**comment**|**nvarchar(255)**|Explanation of the configuration option.|  
|**status**|**smallint**|Bitmap indicating the status for the option. Possible values include the following:<br /><br /> 0 = Static. Setting takes effect when the server is restarted.<br /><br /> 1 = Dynamic. Variable takes effect when the RECONFIGURE statement is executed.<br /><br /> 2 = Advanced. Variable is displayed only when the **show advanced options** is set.<br /><br /> 3 = Dynamic and advanced.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
