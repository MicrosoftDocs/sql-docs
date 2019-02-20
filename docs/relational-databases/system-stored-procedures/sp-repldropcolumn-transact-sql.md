---
title: "sp_repldropcolumn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_repldropcolumn_TSQL"
  - "sp_repldropcolumn"
helpviewer_keywords: 
  - "sp_repldropcolumn"
ms.assetid: fdc1ec5f-f108-42b4-a2d8-f06a71913ab8
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_repldropcolumn (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops a column from an existing table article that has been published. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]
>  This stored procedure has been deprecated and is being supported mainly for backward-compatibility. It should only be used with [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] Publishers and [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] republishing Subscribers. This procedure should not be used on columns with data types that were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or higher.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_repldropcolumn [ @source_object = ] 'source_object', [ @column = ] 'column'   
    [ , [ @from_agent = ] from_agent]   
    [ , [ @schema_change_script = ] 'schema_change_script' ]   
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]   
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]   
```  
  
## Arguments  
 [ @source_object = ] '*source_object*'  
 Is the name of the table article that contains the column to drop. *source_object* is nvarchar(258), with no default.  
  
 [ @column = ] '*column*'  
 Is the name of the column in the table to be dropped. *column* is sysname, with no default.  
  
 [ @from_agent = ] *from_agent*  
 If the stored procedure is being executed by a replication agent. *from_agent* is int, with a default of 0, where a value of 1 is used when this stored procedure is being executed by a replication agent, and in every other case the default value of 0 should be used.  
  
 [ @schema_change_script = ] '*schema_change_script*'  
 Specifies the name and path of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] script used to modify the system generated custom stored procedures. *schema_change_script* is nvarchar(4000), with a default of NULL. Replication allows user-defined custom stored procedures to replace one or more of the default procedures used in transactional replication. *schema_change_script* is executed after a schema change is made to a replicated table article using sp_repldropcolumn, and can be used to do one of the following:  
  
-   If custom stored procedures are automatically regenerated, *schema_change_script* can be used to drop these custom stored procedures and replace them with user-defined custom stored procedures that supports the new schema.  
  
-   If custom stored procedures are not automatically regenerated, *schema_change_script*can be used to regenerate these stored procedures or to create user-defined custom stored procedures.  
  
 [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*  
 Enables or disables the ability to have a snapshot invalidated. *force_invalidate_snapshot* is a bit, with a default of 1.  
  
 1 specifies that changes to the article may cause the snapshot to be invalid, and if that is the case, a value of 1 gives permission for the new snapshot to occur.  
  
 0 specifies that changes to the article do not cause the snapshot to be invalid.  
  
 [ @force_reinit_subscription = ] *force_reinit_subscription*  
 Enables or disables the ability to have the subscription reinitializated. *force_reinit_subscription* is a bit with a default of 0.  
  
 0 specifies that changes to the article do not cause the subscription to be reinitialized.  
  
 1 specifies that changes to the article may cause the subscription to be reinitialized, and if that is the case, a value of 1 gives permission for the subscription reinitialization to occur.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Only members of the sysadmin fixed server role at the Publisher or members of the db_owner or db_ddladmin fixed database roles on the publication database can execute sp_repldropcolumn.  
  
## See Also  
 [Deprecated Features in SQL Server Replication](../../relational-databases/replication/deprecated-features-in-sql-server-replication.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
