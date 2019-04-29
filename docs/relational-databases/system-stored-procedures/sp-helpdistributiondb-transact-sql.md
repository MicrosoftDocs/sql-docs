---
title: "sp_helpdistributiondb (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpdistributiondb_TSQL"
  - "sp_helpdistributiondb"
helpviewer_keywords: 
  - "sp_helpdistributiondb"
ms.assetid: a2917020-26d1-4011-99f8-9212d120fd2d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpdistributiondb (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns properties of the specified distribution database. This stored procedure is executed at the Distributor on the distribution database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpdistributiondb [ [ @database= ] 'database_name' ]  
```  
  
## Arguments  
`[ @database = ] 'database_name'`
 Is the database name for which properties are returned. *database_name* is **sysname**, with a default of **%** for all databases associated with the Distributor and on which the user has permissions.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the distribution database.|  
|**min_distretention**|**int**|Minimum retention period, in hours, before transactions are deleted.|  
|**max_distretention**|**int**|Maximum retention period, in hours, before transactions are deleted.|  
|**history retention**|**int**|Number of hours to retain history.|  
|**history_cleanup_agent**|**sysname**|Name of the History Cleanup Agent.|  
|**distribution_cleanup_agent**|**sysname**|Name of the Distribution Cleanup Agent.|  
|**status**|**int**|Internal use only.|  
|**data_folder**|**nvarchar(255)**|Name of the directory used to store the database files.|  
|**data_file**|**nvarchar(255)**|Name of the database file.|  
|**data_file_size**|**int**|Initial data file size in megabytes.|  
|**log_folder**|**nvarchar(255)**|Name of the directory for the database log file.|  
|**log_file**|**nvarchar(255)**|Name of the log file.|  
|**log_file_size**|**int**|Initial log file size in megabytes.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpdistributiondb** is used in all types of replication.  
  
## Permissions  
 Members of the **db_owner** fixed database role or the **replmonitor** role in a distribution database and users in the publication access list of a publication using the distribution database can execute **sp_helpdistributiondb** to return file-related information. Members of the **public** role can execute **sp_helpdistributiondb** to return non-file-related information for distribution databases to which they have access.  
  
## See Also  
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [sp_adddistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md)   
 [sp_changedistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedistributiondb-transact-sql.md)   
 [sp_dropdistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistributiondb-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
