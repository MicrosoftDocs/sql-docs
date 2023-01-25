---
description: "Rebuild Index Task"
title: "Rebuild Index Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.rebuildindextask.f1"
helpviewer_keywords: 
  - "rebuilding indexes"
  - "indexes [Integration Services]"
  - "Rebuild Index task"
ms.assetid: 021884dd-e72d-47b2-99e8-b741410509c3
author: chugugrace
ms.author: chugu
---
# Rebuild Index Task

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Rebuild Index task rebuilds indexes in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database tables and views. For more information about managing indexes, see [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).  
  
 By using the Rebuild Index task, a package can rebuild indexes in a single database or multiple databases. If the task rebuilds only the indexes in a single database, you can choose the views and tables whose indexes the task rebuilds.  
  
 This task encapsulates an ALTER INDEX REBUILD statement with the following index rebuild options:  
  
-   Specify a FILLFACTOR percentage or use the original FILLFACTOR amount.  
  
-   Set SORT_IN_TEMPDB = ON to store the intermediate sort result used to rebuild the index in tempdb. When the intermediate sort result is set to OFF, the result is stored in the same database as the index.  
  
-   Set PAD_INDEX = ON to allocate the free space specified by FILLFACTOR to the intermediate-level pages of the index.  
  
-   Set IGNORE_DUP_KEY = ON to allow a multirow insert operation that includes records that violate unique constraints to insert the records that do not violate the unique constraints.  
  
-   Set ONLINE = ON to not hold table locks so that queries or updates to the underlying table can proceed during re-indexing.  
  
    > [!NOTE]  
    >  Online index operations are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
-   Specify a value for MAXDOP to limit the number of processors used in a parallel plan execution.  
  
-   Specify WAIT_AT_LOW_PRIORITY, MAX_DURATION, and ABORT_AFTER_WAIT to control how long the index operation waits for low priority locks.  
  
 For more information about the ALTER INDEX statement and index rebuild options, see [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
  
> [!IMPORTANT]  
>  The time the task takes to create the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that the task runs is proportionate to the number of indexes the task rebuilds. If the task is configured to rebuild indexes in all the tables and views in a database with a large number of indexes, or to rebuild indexes in multiple databases, the task can take a considerable amount of time to generate the Transact-SQL statement.  
  
## Configuration of the Rebuild Index Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This task is in the **Maintenance Plan Tasks** section of the **Toolbox** in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
 [Rebuild Index Task &#40;Maintenance Plan&#41;](../../relational-databases/maintenance-plans/rebuild-index-task-maintenance-plan.md)  
  
## Related Tasks  
 For more about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Set the Properties of a Task or Container](./add-or-delete-a-task-or-a-container-in-a-control-flow.md).  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
