---
title: "IHindextypes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "IHindextypes"
  - "IHindextypes_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IHindextypes system table"
ms.assetid: 5eb67d59-a19d-4dba-9d2b-657f87818f6b
author: stevestein
ms.author: sstein
manager: craigg
---
# IHindextypes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **IHindextypes** system table contains one row for each non-SQL Server index type supported for non-SQL Server Publishers. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**type**|**nvarchar(255)**|The name of a supported non-SQL Server index type.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
