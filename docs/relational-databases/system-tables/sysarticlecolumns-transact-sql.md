---
title: "sysarticlecolumns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sysarticlecolumns"
  - "sysarticlecolumns_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysarticlecolumns system table"
ms.assetid: 55918592-e05d-43b6-843b-7e4d82fa6275
author: stevestein
ms.author: sstein
manager: craigg
---
# sysarticlecolumns (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **sysarticlecolumns** table contains one row for each table column that is published in a snapshot or transactional publication, and maps each column to its article. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artid**|**int**|Identifies an article.|  
|**colid**|**smallint**|Identifies a column in an article.|  
|**is_udt**|**bit**|Indicates whether the column is a user-defined data type (UDT) column. A value of **1** indicates a UDT column.|  
|**is_xml**|**bit**|Indicates whether the column is an **xml** column. A value of **1** indicates an xml column.|  
|**is_max**|**bit**|Indicates whether the column is a Large Value data type column, **varchar(max)**, **nvarchar(max)**, and **varbinary(max)**. A value of **1** indicates a Large Value column.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
