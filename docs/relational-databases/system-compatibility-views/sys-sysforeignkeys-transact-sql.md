---
title: "sys.sysforeignkeys (Transact-SQL)"
description: "sys.sysforeignkeys (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysforeignkeys"
  - "sys.sysforeignkeys"
  - "sys.sysforeignkeys_TSQL"
  - "sysforeignkeys_TSQL"
helpviewer_keywords:
  - "sysforeignkeys system table"
  - "sys.sysforeignkeys compatibility view"
dev_langs:
  - "TSQL"
---
# sys.sysforeignkeys (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains information about the FOREIGN KEY constraints that are in the definitions of tables in the database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**constid**|**int**|ID of the FOREIGN KEY constraint.|  
|**fkeyid**|**int**|Object ID of the table with the FOREIGN KEY constraint.|  
|**rkeyid**|**int**|Object ID of the table referenced in the FOREIGN KEY constraint.|  
|**fkey**|**smallint**|ID of the referencing column.|  
|**rkey**|**smallint**|ID of the referenced column.|  
|**keyno**|**smallint**|Position of the column in the reference column list.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
