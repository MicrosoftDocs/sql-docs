---
title: "sys.database_automatic_tuning_options (Transact-SQL) | Microsoft Docs"
description: Learn how to view automatic tuning options on a SQL Database
ms.custom: ""
ms.date: "07/20/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
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
author: "jovanpop-msft"
ms.author: "jovanpop"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database\_automatic\_tuning_options (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2017-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md)]

  Returns the Automatic Tuning options for this database.  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**nvarchar(128)**|The name of the automatic tuning option. Refer to [ALTER DATABASE SET AUTOMATIC_TUNING &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md) for available options.|  
|**desired_state**|**smallint**|Indicates the desired operation mode for Automatic Tuning option, explicitly set by user.<br />0 = OFF<br />1 = ON|  
|**desired_state_desc**|**nvarchar(60)**|Textual description of the desired operation mode of Automatic Tuning option.<br />OFF<br />ON|  
|**actual_state**|**smallint**|Indicates the operation mode of Automatic Tuning option.<br />0 = OFF<br />1 = ON|  
|**actual_state_desc**|**nvarchar(60)**|Textual description of the actual operation mode of Automatic Tuning option.<br />OFF<br />ON|  
|**reason**|**smallint**|Indicates why actual and desired states are different.<br />2 = DISABLED<br />11 = QUERY_STORE_OFF<br />12 = QUERY_STORE_READ_ONLY<br />13 = NOT_SUPPORTED|   
|**reason_desc**|**nvarchar(60)**|Textual description of the reason why actual and desired states are different.<br />DISABLED = Option is disabled by system<br />QUERY_STORE_OFF = Query Store is turned off<br />QUERY_STORE_READ_ONLY = Query Store is in read-only mode<br />NOT_SUPPORTED = Available only in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise edition| 
  
## Permissions  
 Requires the `VIEW DATABASE STATE` permission.  
  
## See Also  
 [Automatic Tuning](../../relational-databases/automatic-tuning/automatic-tuning.md)   
 [ALTER DATABASE SET AUTOMATIC_TUNING &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [sys.database_query_store_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql.md)   
 [sys.dm_db_tuning_recommendations &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-tuning-recommendations-transact-sql.md)   
 
