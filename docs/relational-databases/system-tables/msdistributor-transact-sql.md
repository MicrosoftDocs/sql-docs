---
title: "MSdistributor (Transact-SQL)"
description: MSdistributor (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSdistributor"
  - "MSdistributor_TSQL"
helpviewer_keywords:
  - "MSdistributor system table"
dev_langs:
  - "TSQL"
ms.assetid: 981e9903-0b4b-4508-ac6d-2ee4c813a3d0
---
# MSdistributor (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSdistributor** table contains the Distributor properties. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**property**|**sysname**|The name of the property|  
|**value**|**nvarchar(3000)**|The value of the property|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
