---
title: "IHconstrainttypes (Transact-SQL)"
description: IHconstrainttypes (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "IHconstrainttypes_TSQL"
  - "IHconstrainttypes"
helpviewer_keywords:
  - "IHconstrainttypes system table"
dev_langs:
  - "TSQL"
ms.assetid: 955d6fa9-0b31-4335-a3cd-e4c4d90ad308
---
# IHconstrainttypes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **IHconstrainttypes** system table contains one row for each type of non-SQL Server constraint supported for non-SQL Server Publishers. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**type**|**nvarchar(255)**|The name of a supported non-SQL Server constraint type.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
