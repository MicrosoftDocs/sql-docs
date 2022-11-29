---
title: "IHindextypes (Transact-SQL)"
description: IHindextypes (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "IHindextypes"
  - "IHindextypes_TSQL"
helpviewer_keywords:
  - "IHindextypes system table"
dev_langs:
  - "TSQL"
ms.assetid: 5eb67d59-a19d-4dba-9d2b-657f87818f6b
---
# IHindextypes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **IHindextypes** system table contains one row for each non-SQL Server index type supported for non-SQL Server Publishers. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**type**|**nvarchar(255)**|The name of a supported non-SQL Server index type.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
