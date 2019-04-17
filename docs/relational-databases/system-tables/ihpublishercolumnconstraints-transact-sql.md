---
title: "IHpublishercolumnconstraints (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "IHpublishercolumnconstraints"
  - "IHpublishercolumnconstraints_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IHpublishercolumnconstraints system table"
ms.assetid: d7a41da6-e067-430a-8da2-3f6745b8a4f3
author: stevestein
ms.author: sstein
manager: craigg
---
# IHpublishercolumnconstraints (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **IHpublishercolumnconstraints** system table maps columns of a non-SQL Server publication in the [IHpublishercolumns](../../relational-databases/system-tables/ihpublishercolumns-transact-sql.md) system table to constraints in the [IHpublisherconstraints](../../relational-databases/system-tables/ihpublisherconstraints-transact-sql.md) system table. This table is stored in the distribution database.  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publishercolumn_id**|**int**|Identifies the column from [IHpublishercolumns](../../relational-databases/system-tables/ihpublishercolumns-transact-sql.md) with an associated constraint.|  
|**publisherconstraint_id**|**int**|Identifies a constraint from [IHpublisherconstraints](../../relational-databases/system-tables/ihpublisherconstraints-transact-sql.md) associated with the column.|  
|**indid**|**int**|Indicates position of the column in the published table.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
