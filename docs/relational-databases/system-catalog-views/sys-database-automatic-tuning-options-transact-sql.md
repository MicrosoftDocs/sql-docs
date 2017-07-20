---
title: "sys.database_automatic_tuning_options (Transact-SQL) | Microsoft Docs"
description: Learn how to view automatic tuning options on a SQL Database
ms.custom: ""
ms.date: "07/20/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "database_automatic_tuning_options_tsql"
  - "database_automatic_tuning_options"
  - "sys.database_automatic_tuning_options_tsql"
  - "sys.database_automatic_tuning_options"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database_automatic_tuning_options catalog view"
  - "sys.database_automatic_tuning_options catalog view"
ms.assetid: 16b47d55-8019-41ff-ad34-1e0112178067
caps.latest.revision: 24
author: "jovanpop-msft"
ms.author: "jovanpop"
manager: "jhubbard"
---
# sys.database\_automatic\_tuning_options (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssvNxt-asdb-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

  Returns the Automatic Tuning options for this database.  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**nvarchar(128)**|The name of the automatic tuning option, for example, `FORCE_LAST_GOOD_PLAN`|  
|**desired_state**|**smallint**|Indicates the desired operation mode for Automatic Tuning option, explicitly set by user.|  
|**desired_state_desc**|**nvarchar(60)**|Textual description of the desired operation mode of Automatic Tuning option:<br />`OFF` (0)<br />`ON` (1)|  
|**actual_state**|**smallint**|Indicates the operation mode of Automatic Tuning option.|  
|**actual_state_desc**|**nvarchar(60)**|Textual description of the actual operation mode of Automatic Tuning option.<br />`OFF` (0)<br />`ON` (1)|  
|**reason**|**smallint**|Indicates why actual and desired state are different.|  
|**reason_desc**|**nvarchar(60)**|Textual description of the reason why actual and desired state are different.|  
  
## Permissions  
 Requires the `VIEW DATABASE STATE` permission.  
  
## Remarks
 The values in `reason` and `reason_desc` columns might be:

|reason|reason\_desc|Description|
|-----------------|---------------|-----------------| 
| 2	| `DISABLED`	| Option is disabled by system.|
| 11	| `QUERY_STORE_OFF`	| Query Store is turned off.|
| 12	| `QUERY_STORE_READ_ONLY`	| Query Store is in read-only mode.|
| 13	| `NOT_SUPPORTED`	| Available only in Enterprise edition of SQL Server.|


## See Also  
 [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md)   
 [ALTER DATABASE SET AUTOMATIC_TUNING &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)   
 [sys.dm_db_tuning_recommendations &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)   
 