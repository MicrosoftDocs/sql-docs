---
title: "sp_unregister_custom_scripting (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_unregister_custom_scripting_TSQL"
  - "sp_unregister_custom_scripting"
helpviewer_keywords: 
  - "sp_unregister_custom_scripting"
ms.assetid: b6e9e0d2-9144-434d-88af-4874f2582399
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_unregister_custom_scripting (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This stored procedure removes a user-defined custom stored procedure or [!INCLUDE[tsql](../../includes/tsql-md.md)] script file that was registered by executing [sp_register_custom_scripting](../../relational-databases/system-stored-procedures/sp-register-custom-scripting-transact-sql.md). This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_unregister_custom_scripting [ @type  = ] 'type'  
    [ , [ @publication = ] 'publication' ]  
    [ , [ @article = ] 'article' ]  
```  
  
## Arguments  
 [ **@type** = ] **'***type***'**  
 Is the type of custom stored procedure or script being removed. *type* is **varchar(16)**, with no default, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**insert**|Registered custom stored procedure or script is executed when an INSERT statement is replicated.|  
|**update**|Registered custom stored procedure or script is executed when an UPDATE statement is replicated.|  
|**delete**|Registered custom stored procedure or script is executed when a DELETE statement is replicated.|  
|**custom_script**|Registered custom stored procedure or script is executed at the end of the data definition language (DDL) trigger.|  
  
 [ **@publication** = ] **'***publication***'**  
 Name of the publication for which the custom stored procedure or script is being removed. *publication* is **sysname**, with a default of NULL.  
  
 [ **@article** = ] **'***article***'**  
 Name of the article for which the custom stored procedure or script is being removed. *article* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_unregister_custom_scripting** is used in snapshot and transactional replication.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role can execute **sp_unregister_custom_scripting**.  
  
## See Also  
 [sp_register_custom_scripting &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-register-custom-scripting-transact-sql.md)  
  
  
