---
title: "sys.sp_xtp_merge_checkpoint_files (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/28/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sp_xtp_merge_checkpoint_files_TSQL"
  - "sys.sp_xtp_merge_checkpoint_files"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_xtp_merge_checkpoint_files"
ms.assetid: da04df2a-f7a1-41e7-a1ef-2d5d68919892
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.sp_xtp_merge_checkpoint_files (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]

  **sys.sp_xtp_merge_checkpoint_files** merges all data and delta files in the transaction range specified.  
  
 For more information, see [Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
||  
|-|  
|**Note**: This stored procedure is deprecated in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. It is no longer needed, and cannot be used, starting [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].|  
  
## Syntax  
  
```  
  
sys.sp_xtp_merge_checkpoint_files database_name, @transaction_lower_bound, @transaction_upper_bound  
[;]  
```  
  
## Arguments  
 *database_name*  
 The name of the database on which to invoke the merge. If the database does not have in-memory tables, this procedure returns with user error. If the database is offline, it returns an error.  
  
 *lower_bound_Tid*  
 The (bigint) lower bound of transactions for a data file as shown in [sys.dm_db_xtp_checkpoint_files &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-files-transact-sql.md) corresponding to the start checkpoint file of the merge. An error is generated for invalid transactonId value.  
  
 *upper_bound_Tid*  
 The (bigint) upper bound of transactions for a data file as shown in [sys.dm_db_xtp_checkpoint_files &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-files-transact-sql.md). An error is generated for invalid transactonId value.  
  
## Return Code Values  
 None  
  
## Cursors Returned  
 None  
  
## Permissions  
 Requires sysadmin fixed server role and the db_owner fixed database role.  
  
## Remarks  
 Merges all data and delta files in the valid range to produce a single data and delta file. This procedure does not honor the merge policy.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
