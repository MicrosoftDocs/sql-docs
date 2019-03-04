---
title: "CHECKPOINT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CHECKPOINT_TSQL"
  - "CHECKPOINT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "events [SQL Server], checkpoints"
  - "automatic checkpoints"
  - "writing dirty pages to disk"
  - "pages [SQL Server], dirty"
  - "checkpoints [SQL Server]"
  - "CHECKPOINT statement"
  - "log truncate mode [SQL Server]"
  - "dirty pages"
  - "logs [SQL Server], checkpoints"
  - "manual checkpoints [SQL Server]"
  - "pages [SQL Server], checkpoints"
ms.assetid: ccdfc689-ad4e-44c0-83f7-0f2cfcfb6406
author: juliemsft
ms.author: jrasnick
manager: craigg
---
# CHECKPOINT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Generates a manual checkpoint in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to which you are currently connected.  
  
> [!NOTE]  
>  For information about different types of database checkpoints and checkpoint operation in general, see [Database Checkpoints &#40;SQL Server&#41;](../../relational-databases/logs/database-checkpoints-sql-server.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CHECKPOINT [ checkpoint_duration ]  
```  
  
## Arguments  
 *checkpoint_duration*  
 Specifies the requested amount of time, in seconds, for the manual checkpoint to complete. When *checkpoint_duration* is specified, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] attempts to perform the checkpoint within the requested duration. The *checkpoint_duration* must be an expression of type **int** and must be greater than zero. When this parameter is omitted, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] adjusts the checkpoint duration to minimize the performance impact on database applications. *checkpoint_duration* is an advanced option.  
  
## Factors Affecting the Duration of Checkpoint Operations  
 In general, the amount time required for a checkpoint operation increases with the number of dirty pages that the operation must write. By default, to minimize the performance impact on other applications, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] adjusts the frequency of writes that a checkpoint operation performs. Decreasing the write frequency increases the time the checkpoint operation requires to complete. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses this strategy for a manual checkpoint unless a *checkpoint_duration* value is specified in the CHECKPOINT command.  
  
 The performance impact of using *checkpoint_duration* depends on the number of dirty pages, the activity on the system, and the actual duration specified. For example, if the checkpoint would normally complete in 120 seconds, specifying a *checkpoint_duration* of 45 seconds causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to devote more resources to the checkpoint than would be assigned by default. In contrast, specifying a *checkpoint_duration* of 180 seconds would cause [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to assign fewer resources than would be assigned by default. In general, a short *checkpoint_duration* will increase the resources devoted to the checkpoint, while a long *checkpoint_duration* will reduce the resources devoted to the checkpoint. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always completes a checkpoint if possible, and the CHECKPOINT statement returns immediately when a checkpoint completes. Therefore, in some cases, a checkpoint may complete sooner than the specified duration or may run longer than the specified duration.  
  
##  <a name="Security"></a> Security  
  
### Permissions  
 CHECKPOINT permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles, and are not transferable.  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [Database Checkpoints &#40;SQL Server&#41;](../../relational-databases/logs/database-checkpoints-sql-server.md)   
 [Configure the recovery interval Server Configuration Option](../../database-engine/configure-windows/configure-the-recovery-interval-server-configuration-option.md)   
 [SHUTDOWN &#40;Transact-SQL&#41;](../../t-sql/language-elements/shutdown-transact-sql.md)  
  
  
