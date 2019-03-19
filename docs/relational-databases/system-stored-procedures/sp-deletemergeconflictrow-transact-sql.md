---
title: "sp_deletemergeconflictrow (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_deletemergeconflictrow"
  - "sp_deletemergeconflictrow_TSQL"
helpviewer_keywords: 
  - "sp_deletemergeconflictrow"
ms.assetid: 64cf1186-28b8-4cd9-88f1-a7808a9c8d60
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_deletemergeconflictrow (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Deletes rows from a conflict table or the [MSmerge_conflicts_info &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msmerge-conflicts-info-transact-sql.md) table. This stored procedure is executed at the computer where the conflict table is stored, in any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_deletemergeconflictrow [ [ @conflict_table = ] 'conflict_table' ]  
    [ , [ @source_object = ] 'source_object' ]  
    { , [ @rowguid = ] 'rowguid'  
        , [ @origin_datasource = ] 'origin_datasource' ] }  
    [ , [ @drop_table_if_empty = ] 'drop_table_if_empty' ]  
```  
  
## Arguments  
 [ **@conflict_table=**] **'**_conflict_table_**'**  
 Is the name of the conflict table. *conflict_table* is **sysname**, with a default of **%**. If the *conflict_table* is specified as NULL or **%**, the conflict is assumed to be a delete conflict and the row matching *rowguid* and *origin_datasource* and *source_object* is deleted from the [MSmerge_conflicts_info &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msmerge-conflicts-info-transact-sql.md) table.  
  
 [ **@source_object=**] **'**_source_object_**'**  
 Is the name of the source table. *source_object* is **nvarchar(386)**, with a default of NULL.  
  
 [ **@rowguid =**] **'**_rowguid_**'**  
 Is the row identifier for the delete conflict. *rowguid* is **uniqueidentifier**, with no default.  
  
 [ **@origin_datasource=**] **'**_origin_datasource_**'**  
 Is the origin of the conflict. *origin_datasource* is **varchar(255)**, with no default.  
  
 [ **@drop_table_if_empty=**] **'**_drop_table_if_empty_**'**  
 Is a flag indicating that the *conflict_table* is to be dropped if is empty. *drop_table_if_empty* is **varchar(10)**, with a default of FALSE.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_deletemergeconflictrow** is used in merge replication.  
  
 [MSmerge_conflicts_info &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msmerge-conflicts-info-transact-sql.md) table is a system table and is not deleted from the database, even if it is empty.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_deletemergeconflictrow**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
