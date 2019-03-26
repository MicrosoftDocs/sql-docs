---
title: "sp_restoredbreplication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_restoredbreplication"
  - "sp_restoredbreplication_TSQL"
helpviewer_keywords: 
  - "sp_restoredbreplication"
ms.assetid: a2c5ee32-e6d9-46e9-8031-8ff13c20acf7
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_restoredbreplication (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes replication settings if restoring a database to the non-originating server, database, or system that is otherwise not capable of running replication processes. When restoring a replicated database to a server or database other than the one where the backup was taken, replication settings cannot be preserved. On the restore, the server calls **sp_restoredbreplication** directly to automatically remove replication metadata from the restored database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_restoredbreplication [ @srv_orig = ] 'original_server_name'  
        , [ @db_orig = ] 'original_database_name'  
    [ , [ @keep_replication = ] keep_replication ]  
    [ , [ @perform_upgrade = ] perform_upgrade ]  
```  
  
## Arguments  
 [ **@srv_orig =** ] **'***original_server_name***'**  
 The name of the server where the back up was created. *original_server_name* is **sysname**, with no default.  
  
 [ **@db_orig =** ] **'***original_database_name***'**  
 The name of the database that was backed up. *original_database_name* is **sysname**, with no default.  
  
 [ **@keep_replication =** ] *keep_replication*  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 [ **@perform_upgrade=** ] *perform_upgrade*  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_restoredbreplication** is used in all types of replication.  
  
## Permissions  
 Only members of the **sysadmin** or **dbcreator** fixed server role or the **dbo** database schema can execute **sp_restoredbreplication**.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)  
  
  
