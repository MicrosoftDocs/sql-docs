---
title: sp_check_join_filter (Transact-SQL)
description: "sp_check_join_filter (Transact-SQL)"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords: 
  - "sp_check_join_filter"
  - "sp_check_join_filter_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_check_join_filter"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: "03/06/2017"
---

# sp_check_join_filter (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Is used to verify a join filter between two tables to determine if the join filter clause is valid. This stored procedure also returns information about the supplied join filter, including if it can be used with precomputed partitions for the given table. This stored procedure is executed at the Publisher on the publication. For more information, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md).  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_check_join_filter [ @filtered_table = ] 'filtered_table'  
        , [@join_table = ] 'join_table'  
        , [ @join_filterclause = ] 'join_filterclause'  
```  
  
## Arguments  
`[ @filtered_table = ] 'filtered_table'`
 Is the name of a filtered table. *filtered_table* is **nvarchar(400)**, with no default.  
  
`[ @join_table = ] 'join_table'`
 Is the name of a table being joined to *filtered_table*. *join_table* is **nvarchar(400)**, with no default.  
  
`[ @join_filterclause = ] 'join_filterclause'`
 Is the join filter clause being tested. *join_filterclause* is **nvarchar(1000)**, with no default.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**can_use_partition_groups**|**bit**|Is if the publication qualifies for precomputed partitions; where **1** means that precomputed partitions can be used, and **0** means that they cannot be used.|  
|**has_dynamic_filters**|**bit**|Is if the supplied filter clause includes at least one parameterized filtering function; where **1** means that a parameterized filtering function is used, and **0** means that such a function is not used.|  
|**dynamic_filters_function_list**|**nvarchar(500)**|List of the functions in the filter clause that define a parameterized filter for an article, where each function is separated by a semi-colon.|  
|**uses_host_name**|**bit**|If the [HOST_NAME()](../../t-sql/functions/host-name-transact-sql.md) function is used in the filter clause, where **1** means that this function is present.|  
|**uses_suser_sname**|**bit**|If the [SUSER_SNAME()](../../t-sql/functions/suser-sname-transact-sql.md) function is used in the filter clause, where **1** means that this function is present.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_check_join_filter** is used in merge replication.  
  
 **sp_check_join_filter** can be executed against any related tables even if they are not published. This stored procedure can be used to verify a join filter clause before defining a join filter between two articles.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_check_join_filter**.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
