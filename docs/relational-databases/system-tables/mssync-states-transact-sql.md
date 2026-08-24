---
title: "MSsync_states (Transact-SQL)"
description: MSsync_states (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSsync_states"
  - "MSsync_states_TSQL"
helpviewer_keywords:
  - "MSsync_states system table"
dev_langs:
  - "TSQL"
---
# MSsync_states (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSsync_states** table tracks which publication is still in concurrent snapshot mode. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher_id**|**smallint**|The ID of the publisher.|  
|**publisher_db**|**sysname**|The Name of the publication database.|  
|**publication_id**|**int**|The ID of the publication.|  
  
## Related content

- [Mapping System Tables to System Views (Transact-SQL)](mapping-system-tables-to-system-views-transact-sql.md)
- [Integration Services Tables (Transact-SQL)](integration-services-tables-transact-sql.md)
- [Backup and Restore Tables (Transact-SQL)](backup-and-restore-tables-transact-sql.md)
- [Log Shipping Tables (Transact-SQL)](log-shipping-tables-transact-sql.md)
