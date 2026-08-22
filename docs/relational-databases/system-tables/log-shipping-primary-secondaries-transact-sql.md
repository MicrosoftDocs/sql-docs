---
title: "log_shipping_primary_secondaries (Transact-SQL)"
description: log_shipping_primary_secondaries (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "log_shipping_primary_secondaries_TSQL"
  - "log_shipping_primary_secondaries"
helpviewer_keywords:
  - "log_shipping_primary_secondaries system table"
dev_langs:
  - "TSQL"
---
# log_shipping_primary_secondaries (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Maps each primary database to its secondary databases. This table is stored in the **msdb** database.  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**primary_id**|**uniqueidentifier**|The ID of the primary database for the log shipping configuration.|  
|**secondary_server**|**sysname**|The name of the secondary instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration.|  
|**secondary_database**|**sysname**|The name of the secondary database in the log shipping configuration.|  
  
## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [sys.sp_add_log_shipping_primary_secondary (Transact-SQL)](../system-stored-procedures/sp-add-log-shipping-primary-secondary-transact-sql.md)
- [sys.sp_delete_log_shipping_primary_secondary (Transact-SQL)](../system-stored-procedures/sp-delete-log-shipping-primary-secondary-transact-sql.md)
- [sys.sp_help_log_shipping_primary_secondary (Transact-SQL)](../system-stored-procedures/sp-help-log-shipping-primary-secondary-transact-sql.md)
- [System Tables (Transact-SQL)](system-tables-transact-sql.md)
