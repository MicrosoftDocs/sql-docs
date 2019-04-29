---
title: "sp_showrowreplicainfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_showrowreplicainfo_TSQL"
  - "sp_showrowreplicainfo"
helpviewer_keywords: 
  - "sp_showrowreplicainfo"
ms.assetid: 6a9dbc1a-e1e1-40c4-97cb-8164a2288f76
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_showrowreplicainfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays information about a row in a table that is being used as an article in merge replication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_showrowreplicainfo [ [ @ownername = ] 'ownername' ]  
    [ , [ @tablename =] 'tablename' ]   
        , [ @rowguid =] rowguid   
    [ , [ @show = ] 'show' ]   
```  
  
## Arguments  
`[ @ownername = ] 'ownername'`
 Is the name of the table owner. *ownername* is **sysname**, with a default of NULL. This parameter is useful to differentiate tables if a database contains multiple tables with the same name, but each table has a different owner.  
  
`[ @tablename = ] 'tablename'`
 Is the name of the table that contains the row for which the information is returned. *tablename* is **sysname**, with a default of NULL.  
  
`[ @rowguid = ] rowguid`
 Is the unique identifier of the row. *rowguid* is **uniqueidentifier**, with no default.  
  
`[ @show = ] 'show'`
 Determines the amount of information to return in the result set. *show* is **nvarchar(20)** with a default of BOTH. If **row**, only row version information is returned. If **columns**, only column version information is returned. If **both**, both row and column information is returned.  
  
## Result Sets for Row Information  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**server_name**|**sysname**|Name of the server hosting the database that made the row version entry.|  
|**db_name**|**sysname**|Name of the database that made this entry.|  
|**db_nickname**|**binary(6)**|Nickname of the database that made this entry.|  
|**version**|**int**|Version of the entry.|  
|**current_state**|**nvarchar(9)**|Returns information on the current state of the row.<br /><br /> **y** - Row data represents the current state of the row.<br /><br /> **n** - Row data does not represent the current state of the row.<br /><br /> **\<n/a>** - Not applicable.<br /><br /> **\<unknown>** - Current state cannot be determined.|  
|**rowversion_table**|**nchar(17)**|Indicates whether the row versions are stored in the [MSmerge_contents](../../relational-databases/system-tables/msmerge-contents-transact-sql.md) table or the [MSmerge_tombstone](../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md) table.|  
|**comment**|**nvarchar(255)**|Additional information about this row version entry. Usually, this field is empty.|  
  
## Result Sets for Column Information  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**server_name**|**sysname**|Name of the server hosting the database that made the column version entry.|  
|**db_name**|**sysname**|Name of the database that made this entry.|  
|**db_nickname**|**binary(6)**|Nickname of the database that made this entry.|  
|**version**|**int**|Version of the entry.|  
|**colname**|**sysname**|Name of the article column that the column version entry represents.|  
|**comment**|**nvarchar(255)**|Additional information about this column version entry. Usually, this field is empty.|  
  
## Result Set for both  
 If the value **both** is chosen for *show*, then both the row and column result sets is returned.  
  
## Remarks  
 **sp_showrowreplicainfo** is used in merge replication.  
  
## Permissions  
 **sp_showrowreplicainfo** can only be executed by members of the **db_owner** fixed database role on the publication database or by members of the publication access list (PAL) on the publication database.  
  
## See Also  
 [Detect and Resolve Merge Replication Conflicts](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
