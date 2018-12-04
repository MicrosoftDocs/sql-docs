---
title: "sysmergesubsetfilters (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sysmergesubsetfilters"
  - "sysmergesubsetfilters_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysmergesubsetfilters system table"
ms.assetid: f91d1c6c-3132-47f6-926c-88f56848cafe
author: stevestein
ms.author: sstein
manager: craigg
---
# sysmergesubsetfilters (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains join filter information for partitioned articles. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**filtername**|**sysname**|The name of the filter used to create the article.|  
|**join_filterid**|**int**|The ID of the object representing the join filter.|  
|**pubid**|**uniqueidentifier**|The ID of the publication.|  
|**artid**|**uniqueidentifier**|The ID of the article.|  
|**art_nickname**|**int**|The nickname of the article.|  
|**join_articlename**|**sysname**|The name of the table to join to determine whether the row belongs.|  
|**join_nickname**|**int**|The nickname of the table to join to determine whether the row belongs.|  
|**join_unique_key**|**int**|Indicates a join on a unique key of **join_tablename**:<br /><br /> 0 = Not a unique key.<br /><br /> 1 = A unique key.|  
|**expand_proc**|**sysname**|The name of the stored procedure used by the Merge Agent to identify the rows that need to be sent or removed from a Subscriber.|  
|**join_filterclause**|**nvarchar(1000)**|The filter clause used for the join.|  
|**filter_type**|**tinyint**|Specifies the filter type, which can be one of the following:<br /><br /> 1 = Join filter.<br /><br /> 2 = Logical record link.<br /><br /> 3 = Both a join filter and a logical record link.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
