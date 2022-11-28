---
description: "sp_mergemetadataretentioncleanup (Transact-SQL)"
title: "sp_mergemetadataretentioncleanup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_mergemetadataretentioncleanup"
  - "sp_mergemetadataretentioncleanup_TSQL"
helpviewer_keywords: 
  - "sp_mergemetadataretentioncleanup"
ms.assetid: 4e8d6343-2a38-421d-a3f3-c37d437a0f88
author: markingmyname
ms.author: maghan
---
# sp_mergemetadataretentioncleanup (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Performs a manual cleanup of metadata in the [MSmerge_genhistory](../../relational-databases/system-tables/msmerge-genhistory-transact-sql.md), [MSmerge_contents](../../relational-databases/system-tables/msmerge-contents-transact-sql.md), [MSmerge_tombstone](../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md), [MSmerge_past_partition_mappings](../../relational-databases/system-tables/msmerge-past-partition-mappings-transact-sql.md), and [MSmerge_current_partition_mappings](../../relational-databases/system-tables/msmerge-current-partition-mappings.md) system tables. This stored procedure is executed at each Publisher and Subscriber in the topology.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_mergemetadataretentioncleanup [ [ @num_genhistory_rows = ] num_genhistory_rows OUTPUT ]  
    [ , [ @num_contents_rows = ] num_contents_rows OUTPUT ]   
    [ , [ @num_tombstone_rows = ] num_tombstone_rows OUTPUT ]   
    [ , [ @aggressive_cleanup_only = ] aggressive_cleanup_only ]  
```  
  
## Arguments  
`[ @num_genhistory_rows = ] num_genhistory_rows OUTPUT`
 Returns the number of rows cleaned-up from the [MSmerge_genhistory](../../relational-databases/system-tables/msmerge-genhistory-transact-sql.md) table. *num_genhistory_rows* is **int**, with a default of **0**.  
  
`[ @num_contents_rows = ] num_contents_rows OUTPUT`
 Returns the number of rows cleaned-up from the [MSmerge_contents](../../relational-databases/system-tables/msmerge-contents-transact-sql.md) table. *num_contents_rows* is **int**, with a default of **0**.  
  
`[ @num_tombstone_rows = ] num_tombstone_rows OUTPUT`
 Returns the number of rows cleaned-up from the [MSmerge_tombstone](../../relational-databases/system-tables/msmerge-tombstone-transact-sql.md) table. *num_tombstone_rows* is **int**, with a default of **0**.  
  
`[ @aggressive_cleanup_only = ] aggressive_cleanup_only`
 Internal use only.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
  
> [!IMPORTANT]  
>  If there are multiple publications on a database, and any one of those publications uses an infinite publication retention period, running **sp_mergemetadataretentioncleanup** does not clean up the merge replication change tracking metadata for the database. For this reason, use infinite publication retention with caution. To determine if a publication has an infinite retention period, execute [sp_helpmergepublication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpmergepublication-transact-sql.md) at the Publisher and note any publications in the result set with a value of **0** for **retention**.  
  
## Permissions  
 Only members of the **db_owner** fixed database role or users in the publication access list for a published database can execute **sp_mergemetadataretentioncleanup**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
