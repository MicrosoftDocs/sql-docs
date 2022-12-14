---
title: sp_check_subset_filter (Transact-SQL)
description: "sp_check_subset_filter (Transact-SQL)"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords: 
  - "sp_check_subset_filter"
  - "sp_check_subset_filter_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_check_subset_filter"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: "03/06/2017"
---

# sp_check_subset_filter (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Is used to check a filter clause against any table to determine if the filter clause is valid for the table. This stored procedure returns information about the supplied filter, including if the filter qualifies for use with precomputed partitions. This stored procedure is executed at the Publisher on the database containing the publication.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_check_subset_filter [ @filtered_table = ] 'filtered_table'  
        , [ @subset_filterclause = ] 'subset_filterclause'  
    [ , [ @has_dynamic_filters = ] has_dynamic_filters OUTPUT ]  
```  
  
## Arguments  
`[ @filtered_table = ] 'filtered_table'`
 Is the name of a filtered table. *filtered_table* is **nvarchar(400)**, with no default.  
  
`[ @subset_filterclause = ] 'subset_filterclause'`
 Is the filter clause being tested. *subset_filterclause* is **nvarchar(1000)**, with no default.  
  
`[ @has_dynamic_filters = ] has_dynamic_filters`
 Is if the filter clause is a parameterized row filter. *has_dynamic_filters* is **bit**, with a default of NULL and is an output parameter. Returns a value of **1** when the filter clause is a parameterized row filter.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**can_use_partition_groups**|**bit**|Is if the publication qualifies for using precomputed partitions; where **1** means that precomputed partitions can be used, and **0** means that they cannot be used.|  
|**has_dynamic_filters**|**bit**|Is if the supplied filter clause includes at least one parameterized row filter; where **1** means that a parameterized row filter is used, and **0** means that such a function is not used.|  
|**dynamic_filters_function_list**|**nvarchar(500)**|List of the functions in the filter clause that dynamically filter an article, where each function is separated by a semi-colon.|  
|**uses_host_name**|**bit**|If the [HOST_NAME()](../../t-sql/functions/host-name-transact-sql.md) function is used in the filter clause, where **1** means that this function is present.|  
|**uses_suser_sname**|**bit**|If the [SUSER_SNAME()](../../t-sql/functions/suser-sname-transact-sql.md) function is used in the filter clause, where **1** means that this function is present.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_check_subset_filter** is used in merge replication.  
  
 **sp_check_subset_filter** can be executed against any table even if the table is not published. This stored procedure can be used to verify a filter clause before defining a filtered article.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_check_subset_filter**.  
  
## See Also  
 [Optimize Parameterized Filter Performance with Precomputed Partitions](../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md)  
  
  
