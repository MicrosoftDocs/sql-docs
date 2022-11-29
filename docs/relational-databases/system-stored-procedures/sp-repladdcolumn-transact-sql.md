---
description: "sp_repladdcolumn (Transact-SQL)"
title: "sp_repladdcolumn (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_repladdcolumn_TSQL"
  - "sp_repladdcolumn"
helpviewer_keywords: 
  - "sp_repladdcolumn"
ms.assetid: d6220f9f-c738-4f9c-bcf8-419994e86c81
author: markingmyname
ms.author: maghan
---
# sp_repladdcolumn (Transact-SQL)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Adds a column to an existing table article that has been published. Allows the new column to be added to all publishers that publish this table, or just add the column to a specific publication that publishes the table. This stored procedure is executed at the Publisher on the publication database.  
  
> [!IMPORTANT]
>  This stored procedure has been deprecated and is being supported for backward-compatibility. It should only be used with [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] Publishers and [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] republishing Subscribers. This procedure should not be used on columns with data types that were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or higher.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_repladdcolumn [ @source_object = ] 'source_object', [ @column = ] 'column' ]  
    [ , [ @typetext = ] 'typetext' ]  
    [ , [ @publication_to_add = ] 'publication_to_add' ]  
    [ , [ @from_agent = ] from_agent ]  
    [ , [ @schema_change_script = ] 'schema_change_script' ]  
    [ , [ @force_invalidate_snapshot = ] force_invalidate_snapshot ]  
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]  
```  
  
## Arguments  
 [ @source_object =] '*source_object*'  
 Is the name of the table article that contains the new column to add. *source_object* is **nvarchar(358**), with no default.  
  
 [ @column =] '*column*'  
 Is the name of the column in the table to be added for replication. *column* is **sysname**, with no default.  
  
 [ @typetext =] '*typetext*'  
 Is the definition of the column being added. *typetext* is **nvarchar(3000)**, with no default. For example, if the column order_filled is being added, and it is a single character field, not NULL, and has a default value of **N**, order_filled would be the *column* parameter, while the definition of the column, **char(1) NOT NULL CONSTRAINT constraint_name DEFAULT 'N'** would be the *typetext* parameter value.  
  
 [ @publication_to_add =] '*publication_to_add*'  
 Is the name of the publication to which the new column is added. *publication_to_add* is **nvarchar(4000)**, with a default of **ALL**. If **ALL**, then all publications containing this table are affected. If *publication_to_add* is specified, then only this publication has the new column added.  
  
 [ @from_agent = ] *from_agent*  
 If the stored procedure is being executed by a replication agent. *from_agent* is **int**, with a default of **0**, where a value of **1** is used when this stored procedure is being executed by a replication agent, and in every other case the default value of **0**should be used.  
  
 [ @schema_change_script =] '*schema_change_script*'  
 Specifies the name and path of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] script used to modify the system generated custom stored procedures. *schema_change_script* is **nvarchar(4000)**, with a default of NULL. Replication allows user-defined custom stored procedures to replace one or more of the default procedures used in transactional replication. *schema_change_script* is executed after a schema change is made to a replicated table article using sp_repladdcolumn, and can be used to do one of the following:  
  
-   If custom stored procedures are automatically regenerated, *schema_change_script* can be used to drop these custom stored procedures and replace them with user-defined custom stored procedures that supports the new schema.  
  
-   If custom stored procedures are not automatically regenerated, *schema_change_script*can be used to regenerate these stored procedures or to create user-defined custom stored procedures.  
  
 [ @force_invalidate_snapshot = ] *force_invalidate_snapshot*  
 Enables or disables the ability to have a snapshot invalidated. *force_invalidate_snapshot* is a **bit**, with a default of **1**.  
  
 **1** specifies that changes to the article may cause the snapshot to be invalid, and if that is the case, a value of **1** gives permission for the new snapshot to occur.  
  
 **0** specifies that changes to the article do not cause the snapshot to be invalid.  
  
 [ @force_reinit_subscription = ] *force_reinit_subscription*  
 Enables or disables the ability to have the subscription reinitializated. *force_reinit_subscription* is a **bit** with a default of **0**.  
  
 **0** specifies that changes to the article do not cause the subscription to be reinitialized.  
  
 **1** specifies that changes to the article may cause the subscription to be reinitialized, and if that is the case, a value of **1** gives permission for the subscription reinitialization to occur.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Only members of the sysadmin fixed server role and the db_owner fixed database role can execute sp_repladdcolumn.  
  
## See Also  
 [Deprecated Features in SQL Server Replication](../../relational-databases/replication/deprecated-features-in-sql-server-replication.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
