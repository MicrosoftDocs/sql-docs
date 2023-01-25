---
title: "sysmergearticlecolumns (Transact-SQL)"
description: sysmergearticlecolumns (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sysmergearticlecolumns"
  - "sysmergearticlecolumns_TSQL"
helpviewer_keywords:
  - "sysmergearticlecolumns system table"
dev_langs:
  - "TSQL"
ms.assetid: 1ad8663f-a624-42a2-8641-fefac3433c97
---
# sysmergearticlecolumns (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **sysmergearticlecolumns** table contains one row for each table column that is published in a merge publication, and maps each column to its merge article. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artid**|**int**|Identifies an article.|  
|**colid**|**smallint**|Identifies a column in an article.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
