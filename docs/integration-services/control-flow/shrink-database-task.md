---
description: "Shrink Database Task"
title: "Shrink Database Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.shrinkdatabasetask.f1"
helpviewer_keywords: 
  - "Shrink Database task"
  - "database shrinking [Integration Services]"
  - "shrinking databases"
ms.assetid: e66286f8-97b1-4e5a-86b4-e56f1932b7d5
author: chugugrace
ms.author: chugu
---
# Shrink Database Task

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Shrink Database task reduces the size of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database data and log files.  
  
 By using the Shrink Database task, a package can shrink files for a single database or multiple databases.  
  
 Shrinking data files recovers space by moving pages of data from the end of the file to unoccupied space closer to the front of the file. When enough free space is created at the end of the file, data pages at end of the file can deallocated and returned to the file system.  
  
> [!WARNING]  
>  Data that is moved to shrink a file can be scattered to any available location in the file. This causes index fragmentation and can slow the performance of queries that search a range of the index. To eliminate the fragmentation, consider rebuilding the indexes on the file after shrinking.  
  
## Commands  
 The Shrink Database task encapsulates a DBCC SHRINKDATABASE command, including the following arguments and options:  
  
-   *database_name*  
  
-   *target_percent*  
  
-   NOTRUNCATE or TRUNCATEONLY.  
  
 If the Shrink Database task shrinks multiple databases, the task runs multiple SHRINKDATABASE commands, one for each database. All instances of the SHRINKDATABASE command use the same argument values, except for the *database_name* argument. For more information, see [DBCC SHRINKDATABASE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).  
  
## Configuration of the Shrink Database Task  
 You can set properties through the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This task is in the **Maintenance Plan Tasks** section of the **Toolbox** in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For more information about the properties that you can set in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Shrink Database Task &#40;Maintenance Plan&#41;](../../relational-databases/maintenance-plans/shrink-database-task-maintenance-plan.md)  
  
 For more information about setting these properties in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](./add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  
