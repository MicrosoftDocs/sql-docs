---
title: "MSrepl_queuedtraninfo (Transact-SQL)"
description: MSrepl_queuedtraninfo (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSrepl_queuedtraninfo_TSQL"
  - "MSrepl_queuedtraninfo"
helpviewer_keywords:
  - "MSrepl_queuedtraninfo system table"
dev_langs:
  - "TSQL"
---
# MSrepl_queuedtraninfo (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSreplication_queuedtraninfo** table is used by the replication process to store information about the queued commands issued by all the queued updating subscriptions that are using SQL-based queued updating. This table is stored in the Subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|The name of the Publisher.|  
|**publisher_db**|**sysname**|The name of the publication database.|  
|**publication**|**sysname**|The name of the publication.|  
|**tranid**|**sysname**|The transaction ID under which the queued command was executed.|  
|**maxorderkey**|**bigint**|Internal-use only.|  
|**commandcount**|**bigint**|Internal-use only.|  
  
## Related content

- [Replication Tables (Transact-SQL)](replication-tables-transact-sql.md)
- [Replication Views (Transact-SQL)](../system-views/replication-views-transact-sql.md)
