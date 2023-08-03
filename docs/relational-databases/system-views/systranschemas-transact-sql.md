---
title: "systranschemas (Transact-SQL)"
description: "systranschemas (Transact-SQL)"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "09/13/2022"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "systranschemas"
  - "systranschemas_TSQL"
helpviewer_keywords:
  - "systranschemas system table"
dev_langs:
  - "TSQL"
---
# systranschemas (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **systranschemas** table is used to track schema changes in articles published in transactional and snapshot publications and for Change Data Capture. This table is stored in both publication and subscription databases.
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**tabid**|**int**|Identifies the table article on which the schema change occurred.|  
|**startlsn**|**binary**|LSN value at the start of the schema change.|  
|**endlsn**|**binary**|LSN value at the end of the schema change.|  
|**typeid**|**int**|Type of schema change.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
