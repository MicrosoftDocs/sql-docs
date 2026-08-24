---
title: "MSrepl_version (Transact-SQL)"
description: MSrepl_version (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSrepl_version"
  - "MSrepl_version_TSQL"
helpviewer_keywords:
  - "MSrepl_version system table"
dev_langs:
  - "TSQL"
---
# MSrepl_version (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSrepl_version** table contains one row with the current version of replication installed. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**major_version**|**int**|The major version number of the distribution database.|  
|**minor_version**|**int**|The minor version number of the distribution database.|  
|**revision**|**int**|The revision number.|  
|**db_existed**|**bit**|Indicates whether the distribution database exists before **sp_adddistributiondb** is called.|  
  
## Related content

- [Replication Tables (Transact-SQL)](replication-tables-transact-sql.md)
- [Replication Views (Transact-SQL)](../system-views/replication-views-transact-sql.md)
