---
title: "MSreplication_options (Transact-SQL)"
description: MSreplication_options (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSreplication_options"
  - "MSreplication_options_TSQL"
helpviewer_keywords:
  - "MSreplication_options system table"
dev_langs:
  - "TSQL"
ms.assetid: 23cf10d7-8bc1-4368-b5eb-e5576421e776
---
# MSreplication_options (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSreplication_options** table stores metadata that is used internally by replication. This table is stored in the **master** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**optname**|**sysname**|Internal use only.|  
|**value**|**bit**|Internal use only.|  
|**major_version**|**int**|Internal use only.|  
|**minor_version**|**int**|Internal use only.|  
|**revision**|**int**|Internal use only.|  
|**install_failures**|**int**|Internal use only.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
