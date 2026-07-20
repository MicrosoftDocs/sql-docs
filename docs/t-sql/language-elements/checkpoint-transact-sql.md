---
title: "CHECKPOINT (Transact-SQL)"
description: "CHECKPOINT generates a manual checkpoint in the current database."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/20/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CHECKPOINT_TSQL"
  - "CHECKPOINT"
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
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# CHECKPOINT (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Generates a manual checkpoint in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to which you are currently connected.  

> [!TIP]
>  For information about different types of database checkpoints and checkpoint operation in general, see [Database checkpoints (SQL Server)](../../relational-databases/logs/database-checkpoints-sql-server.md).

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
CHECKPOINT [ checkpoint_duration ]  
```  

## Arguments

#### *checkpoint_duration*

Specifies the requested amount of time, in seconds, for the manual checkpoint to complete. 

*checkpoint_duration* is an advanced option.

When *checkpoint_duration* is specified, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] attempts to perform the checkpoint within the requested duration. 

The *checkpoint_duration* must be an expression of type **int** and must be greater than zero.

When this parameter is omitted, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] adjusts the checkpoint duration to minimize the performance impact on database applications. 

## Factors affecting the duration of checkpoint operations

 In general, the amount time required for a checkpoint operation increases with the number of dirty pages that the operation must write. By default, to minimize the performance impact on other applications, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] adjusts the frequency of writes that a checkpoint operation performs. Decreasing the write frequency increases the time the checkpoint operation requires to complete. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses this strategy for a manual checkpoint unless a *checkpoint_duration* value is specified in the `CHECKPOINT` command.  

 The performance impact of using *checkpoint_duration* depends on the number of dirty pages, the activity on the system, and the actual duration specified. 

 - For example, if the checkpoint would normally complete in 120 seconds, specifying a *checkpoint_duration* of 45 seconds causes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to devote more resources to the checkpoint than would be assigned by default. 

 - In contrast, specifying a *checkpoint_duration* of 180 seconds would cause [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to assign fewer resources than would be assigned by default. 

In general, a short *checkpoint_duration* will increase the resources devoted to the checkpoint, while a long *checkpoint_duration* will reduce the resources devoted to the checkpoint. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always completes a checkpoint if possible, and the `CHECKPOINT` statement returns immediately when a checkpoint completes. Therefore, in some cases, a checkpoint can complete sooner than the specified duration or might run longer than the specified duration.

<a id="Security"></a>

## Permissions

 `CHECKPOINT` permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles, and are not transferable.  

## Related content

- [ALTER DATABASE (Transact-SQL)](../statements/alter-database-transact-sql.md)
- [Database checkpoints (SQL Server)](../../relational-databases/logs/database-checkpoints-sql-server.md)
- [Server configuration: recovery interval (min)](../../database-engine/configure-windows/configure-the-recovery-interval-server-configuration-option.md)
- [SHUTDOWN (Transact-SQL)](shutdown-transact-sql.md)
