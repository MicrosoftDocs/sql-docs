---
title: "MSmerge_tombstone (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSmerge_tombstone_TSQL"
  - "MSmerge_tombstone"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MSmerge_tombstone system table"
ms.assetid: 8b3fc7bf-729b-40f2-8a26-e7dfbe8ddb38
author: stevestein
ms.author: sstein
manager: craigg
---
# MSmerge_tombstone (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSmerge_tombstone** table contains information on deleted rows and allows deletes to be propagated to other Subscribers. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**rowguid**|**uniqueidentifier**|The row identifier.|  
|**tablenick**|**int**|The nickname of the table.|  
|**type**|**tinyint**|The type of delete:<br /><br /> 1 = User delete.<br /><br /> 5 = Row no longer belongs to the filtered partition.<br /><br /> 6 = System delete.|  
|**lineage**|**varbinary(249)**|Indicates the version of the record that was deleted, and which updates were known when it was deleted. Allows rules for consistent resolution of a conflict when one Subscriber updates a row while it is being deleted at another Subscriber.|  
|**generation**|**int**|Is assigned when a row is deleted. If a Subscriber requests generation N, only tombstones with generation >= N are sent.|  
|**logical_record_parent_rowguid**|**uniqueidentifier**|Identifies the logical record to which a deleted row belongs.|  
|**logical_record_lineage**|**Varbinary(501)**|The subscriber nickname, version number pairs that are used to maintain a history of deletes for the logical record to which this row belongs.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
