---
title: "MSrepl_originators (Transact-SQL)"
description: MSrepl_originators (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSrepl_originators_TSQL"
  - "MSrepl_originators"
helpviewer_keywords:
  - "MSrepl_originators system table"
dev_langs:
  - "TSQL"
ms.assetid: a3ac20a6-73f6-4fdc-ad5f-5f72746c9871
---
# MSrepl_originators (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSrepl_originators** table contains one row for each updatable Subscriber from which the transaction originated. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|Identifies the updating Subscriber.|  
|**publisher_database_id**|**int**|Identifies the publication database.|  
|**srvname**|**sysname**|The name of the updating server.|  
|**dbname**|**sysname**|The name of the updating database.|  
|**publication_id**|**int**|Identifies the publication.|  
|**dbversion**|**int**|Identifies the database version.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
