---
title: "sysarticlecolumns (System View) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
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
  - "sysarticlecolumns view"
ms.assetid: a8dd8d13-c827-45c4-87ba-802725301382
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sysarticlecolumns (System View) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **sysarticlecolumns** view exposes additional information about columns in published articles. This view is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artid**|**int**|Identifies an article.|  
|**colid**|**int**|Identifies a column in an article.|  
|**is_udt**|**int**|Indicates whether the column is a user-defined data type (UDT) column. A value of **1** indicates a UDT column.|  
|**is_xml**|**int**|Is if the column is an **xml** column. A value of **1** indicates an **xml** column.|  
|**is_max**|**int**|Is if the column is a Large Value data type column (**varchar(max)**, **nvarchar(max)** or **varbinary(max)**). A value of **1** indicates a Large Value column.|  
  
## See Also  
 [sp_articlecolumn &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-articlecolumn-transact-sql.md)   
 [sysarticlecolumns &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sysarticlecolumns-transact-sql.md)  
  
  
