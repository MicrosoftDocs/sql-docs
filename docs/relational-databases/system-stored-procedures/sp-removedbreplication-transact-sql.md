---
title: "sp_removedbreplication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_removedbreplication"
  - "sp_removedbreplication_TSQL"
helpviewer_keywords: 
  - "sp_removedbreplication"
ms.assetid: cb98d571-d1eb-467b-91f7-a6e091009672
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_removedbreplication (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This stored procedure removes all replication objects on the publication database on the Publisher instance of SQL Server or on the subscription database on the Subscriber instance of SQL Server. Execute in the appropriate database, or if the execution is in the context of another database on the same instance, specify the database where the replication objects should be removed. This procedure does not remove objects from other databases, such as the distribution database.  
  
> [!NOTE]  
>  This procedure should be used only if other methods of removing replication objects have failed.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_removedbreplication [ [ @dbname = ] 'dbname' ]  
    [ , [ @type = ] type ]   
```  
  
## Arguments  
 [ **@dbname=**] **'***dbname***'**  
 Is the name of the database. *dbname* is **sysname**, with a default value of NULL. When NULL, the current database will be used.  
  
 [ **@type** = ] *type*  
 Is the type of replication for which database objects are being removed. *type* is **nvarchar(5)** and can be one of the following values.  
  
|||  
|-|-|  
|**tran**|Removes transactional replication publishing objects.|  
|**merge**|Removes merge replication publishing objects.|  
|**both** (default)|Removes all replication publishing objects.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_removedbreplication** is used in all types of replication.  
  
 **sp_removedbreplication** is useful when restoring a replicated database that has no replication objects needing to be restored.  
  
 **sp_removedbreplication** cannot be used against a database that is marked as read-only.  
  
## Example  
 [!code-sql[HowTo#sp_removedbreplication](../../relational-databases/replication/codesnippet/tsql/sp-removedbreplication-t_1.sql)]  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_removedbreplication**.  
  
## Example  
  
```  
-- Remove replication objects from the subscription database on MYSUB.  
DECLARE @subscriptionDB AS sysname  
SET @subscriptionDB = N'AdventureWorksReplica'  
  
-- Remove replication objects from a subscription database (if necessary).  
USE master  
EXEC sp_removedbreplication @subscriptionDB  
GO  
  
```  
  
## See Also  
 [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
