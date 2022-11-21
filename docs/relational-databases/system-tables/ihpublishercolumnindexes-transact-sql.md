---
title: "IHpublishercolumnindexes (Transact-SQL)"
description: IHpublishercolumnindexes (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "IHpublishercolumnindexes"
  - "IHpublishercolumnindexes_TSQL"
helpviewer_keywords:
  - "IHpublishercolumnindexes system table"
dev_langs:
  - "TSQL"
ms.assetid: 95b95a1d-b502-4838-825f-82a456487e25
---
# IHpublishercolumnindexes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **IHpublishercolumnindexes** system table maps columns of a non-SQL Server publication in the [IHpublishercolumns](../../relational-databases/system-tables/ihpublishercolumns-transact-sql.md) system table to indexes in the [IHpublisherindexes](../../relational-databases/system-tables/ihpublisherindexes-transact-sql.md) system table. This table is stored in the distribution database.  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publishercolumn_id**|**int**|Identifies the column from [IHpublishercolumns](../../relational-databases/system-tables/ihpublishercolumns-transact-sql.md) with an associated index.|  
|**publisherindex_id**|**int**|Identifies an index from the [IHpublisherindexes](../../relational-databases/system-tables/ihpublisherindexes-transact-sql.md) table associated with the column.|  
|**indid**|**int**|Indicates position of the column in the published table.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
