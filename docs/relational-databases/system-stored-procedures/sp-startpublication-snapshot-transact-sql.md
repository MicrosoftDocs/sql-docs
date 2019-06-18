---
title: "sp_startpublication_snapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_startpublication_snapshot"
  - "sp_startpublication_snapshot_TSQL"
helpviewer_keywords: 
  - "sp_startpublication_snapshot"
ms.assetid: 2cf568ee-0679-4d19-a394-27210bff61e5
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_startpublication_snapshot (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Used to start the Snapshot Agent job that generates the initial snapshot for a publication. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_startpublication_snapshot [ @publication = ] 'publication'   
    [ , [ @publisher = ] 'publisher' ]  
```  
  
## Arguments  
`[ @publication = ] 'publication'`
 Is the name of the publication. *publication* is **sysname**, with no default.  
  
`[ @publisher = ] 'publisher'`
 Is the name of a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *publisher* is **sysname**, with a default value of NULL. You should not specify this parameter for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_startpublication_snapshot** is used with all types of replication.  
  
 For a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publisher, this stored procedure is executed at the Distributor on the distribution database.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_startpublication_snapshot**.  
  
## See Also  
 [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)   
 [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md)   
 [sp_changepublication_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md)  
  
  
