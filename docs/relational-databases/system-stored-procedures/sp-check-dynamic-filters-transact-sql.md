---
title: "sp_check_dynamic_filters (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "dynamic_filters_TSQL"
  - "sp_check_TSQL"
  - "check"
  - "sp_check_dynamic filter"
  - "check_TSQL"
  - "filters_TSQL"
  - "check_dynamic_filters_TSQL"
  - "dynamic filters"
  - "filters"
  - "check dynamic filters"
  - "sp_check_dynamic filter_TSQL"
  - "sp_check_for_sync_trigger_TSQL"
  - "sp_check"
helpviewer_keywords: 
  - "sp_check_dynamic_filters"
ms.assetid: dd7760db-a3a5-460f-bd97-b8d436015e19
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_check_dynamic_filters (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays information on parameterized row filter properties for a publication, in particular the functions used to generate a filtered data partition for a publication and whether the publication qualifies for using precomputed partitions. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_check_dynamic_filters [ @publication = ] 'publication'  
```  
  
## Arguments  
 [ **@publication**= ] **'**_publication_**'**  
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**can_use_partition_groups**|**bit**|Is if the publication qualifies for using precomputed partitions; where **1** means that precomputed partitions can be used, and **0** means that they cannot be used.|  
|**has_dynamic_filters**|**bit**|Is if at least one parameterized row filter has been defined in the publication; where **1** means that one or more parameterized row filters exist, and **0** means that no dynamic filters exist.|  
|**dynamic_filters_function_list**|**nvarchar(500)**|List of functions used to filter articles in a publication, where each function is separated by a semi-colon.|  
|**validate_subscriber_info**|**nvarchar(500)**|List of functions used to filter articles in a publication, where each function is separated by a plus sign (+).|  
|**uses_host_name**|**bit**|If the [HOST_NAME()](../../t-sql/functions/host-name-transact-sql.md) function is used in parameterized row filters, where **1** means that this function is used for dynamic filtering.|  
|**uses_suser_sname**|**bit**|If the [SUSER_SNAME()](../../t-sql/functions/suser-sname-transact-sql.md) function is used in parameterized row filters, where **1** means that this function is used for dynamic filtering.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_check_dynamic_filters** is used in merge replication.  
  
 If a publication has been defined to use precomputed partitions, **sp_check_dynamic_filters** checks for any violations of the restrictions of precomputed partitions. If any are found, an error is returned. For more information, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md).  
  
 If a publication has been defined as having parameterized row filters, but no parameterized row filters are found, an error is returned.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_check_dynamic_filters**.  
  
## See Also  
 [Manage Partitions for a Merge Publication with Parameterized Filters](../../relational-databases/replication/publish/manage-partitions-for-a-merge-publication-with-parameterized-filters.md)   
 [sp_check_join_filter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-check-join-filter-transact-sql.md)   
 [sp_check_subset_filter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-check-subset-filter-transact-sql.md)  
  
  
