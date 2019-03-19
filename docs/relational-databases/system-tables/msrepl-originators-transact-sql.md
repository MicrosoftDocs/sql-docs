---
title: "MSrepl_originators (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSrepl_originators_TSQL"
  - "MSrepl_originators"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSrepl_originators system table"
ms.assetid: a3ac20a6-73f6-4fdc-ad5f-5f72746c9871
author: stevestein
ms.author: sstein
manager: craigg
---
# MSrepl_originators (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSrepl_originators** table contains one row for each updatable Subscriber from which the transaction originated. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|Identifies the updating Subscriber.|  
|**publisher_database_id**|**int**|Identifies the publication database.|  
|**srvname**|**sysname**|The name of the updating server.|  
|**dbname**|**sysname**|The name of the updating database.|  
|**publication_id**|**int**|Identifies the publication.|  
|**dbversion**|**int**|Identifies the database version.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
