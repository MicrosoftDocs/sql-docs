---
title: "Reorganize Index Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.reorganizeindextask.f1"
helpviewer_keywords: 
  - "reorganizing indexes"
  - "Reorganize Index task [Integration Services]"
  - "indexes [Integration Services]"
ms.assetid: 9ed87861-e5c3-4fcd-8760-d112f4c0af0c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Reorganize Index Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Reorganize Index task reorganizes indexes in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database tables and views. For more information about managing indexes, see [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).  
  
 By using the Reorganize Index task, a package can reorganize indexes in a single database or multiple databases. If the task reorganizes only the indexes in a single database, you can choose the views or the tables whose indexes the task reorganizes. The Reorganize Index task also includes an option to compact large object data. Large object data is data with the **image**, **text**, **ntext**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml** data type. For more information, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).  
  
 The Reorganize Index task encapsulates the Transact-SQL ALTER INDEX statement. If you choose to compact large object data, the statement uses the REORGANIZE WITH (LOB_COMPACTION = ON) clause, otherwise LOB_COMPACTION is set to OFF. For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
  
> [!IMPORTANT]  
>  The time the task takes to create the Transact-SQL statement that the task runs is proportionate to the number of indexes the task reorganizes. If the task is configured to reorganize indexes in all the tables and views in a database that holds a large number of indexes, or to reorganize indexes in multiple databases, the task can take a considerable amount of time to generate the Transact-SQL statement.  
  
## Configuration of the Reorganize Index Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This task is in the **Maintenance Plan Tasks** section of the **Toolbox** in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Reorganize Index Task &#40;Maintenance Plan&#41;](../../relational-databases/maintenance-plans/reorganize-index-task-maintenance-plan.md)  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b).  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
