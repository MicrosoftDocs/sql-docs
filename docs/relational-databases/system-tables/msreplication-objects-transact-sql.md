---
title: "MSreplication_objects (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSreplication_objects"
  - "MSreplication_objects_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSreplication_objects system table"
ms.assetid: 08f9710d-976d-448e-bead-ac9835e87bc5
author: stevestein
ms.author: sstein
manager: craigg
---
# MSreplication_objects (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSreplication_objects** table contains one row for each object that is associated with replication in the Subscriber database. This table is stored in the subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**publisher**|**sysname**|The name of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**publication**|**sysname**|The name of the publication.|  
|**object_name**|**sysname**|The name of the object.|  
|**object_type**|**char(2)**|The object type:<br /><br /> **u** = Table.<br /><br /> **t** = Trigger.<br /><br /> **p** = Stored Procedure.|  
|**article**|**sysname**|The name of the article with which the object is associated.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
