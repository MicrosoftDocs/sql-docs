---
title: "sp_register_custom_scripting (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_register_custom_scripting"
  - "sp_register_custom_scripting_TSQL"
helpviewer_keywords: 
  - "sp_register_custom_scripting"
ms.assetid: a8159282-de3b-4b9e-bdc9-3d3fce485c7f
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_register_custom_scripting (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Replication allows user-defined custom stored procedures to replace one or more of the default procedures used in transactional replication. When a schema change is made to a replicated table, these stored procedures are re-created. **sp_register_custom_scripting** registers a stored procedure or [!INCLUDE[tsql](../../includes/tsql-md.md)] script file that is executed when a schema change occurs to script out the definition for a new user-defined custom stored procedure. This new user-defined custom stored procedure should reflect the new schema for the table. **sp_register_custom_scripting** is executed at the Publisher on the publication database, and the registered script file or stored procedure is executed at the Subscriber when a schema change occurs.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_register_custom_scripting [ @type  = ] 'type'  
    [ @value = ] 'value'   
    [ , [ @publication = ] 'publication' ]  
    [ , [ @article = ] 'article' ]  
```  
  
## Arguments  
`[ @type = ] 'type'`
 Is the type of custom stored procedure or script being registered. *type* is **varchar(16)**, with no default, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**insert**|Registered custom stored procedure is executed when an INSERT statement is replicated.|  
|**update**|Registered custom stored procedure is executed when an UPDATE statement is replicated.|  
|**delete**|Registered custom stored procedure is executed when a DELETE statement is replicated.|  
|**custom_script**|Script is executed at the end of the data definition language (DDL) trigger.|  
  
`[ @value = ] 'value'`
 Name of a stored procedure or name and fully-qualified path to the [!INCLUDE[tsql](../../includes/tsql-md.md)] script file that is being registered. *value* is **nvarchar(1024)**, with no default.  
  
> [!NOTE]  
>  Specifying NULL for *value*parameter will unregister a previously registered script, which is the same as running [sp_unregister_custom_scripting](../../relational-databases/system-stored-procedures/sp-unregister-custom-scripting-transact-sql.md).  
  
 When the value of *type* is **custom_script**, the name and full path of a [!INCLUDE[tsql](../../includes/tsql-md.md)] script file is expected. Otherwise, *value* must be the name of a registered stored procedure.  
  
`[ @publication = ] 'publication'`
 Name of the publication for which the custom stored procedure or script is being registered. *publication* is **sysname**, with a default of **NULL**.  
  
`[ @article = ] 'article'`
 Name of the article for which the custom stored procedure or script is being registered. *article* is **sysname**, with a default of **NULL**.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_register_custom_scripting** is used in snapshot and transactional replication.  
  
 This stored procedure should be executed prior to making a schema change to a replicated table. For more information about using this stored procedure, see [Regenerate Custom Transactional Procedures to Reflect Schema Changes](../../relational-databases/replication/transactional/transactional-articles-regenerate-to-reflect-schema-changes.md).  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role can execute **sp_register_custom_scripting**.  
  
## See Also  
 [sp_unregister_custom_scripting &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unregister-custom-scripting-transact-sql.md)  
  
  
