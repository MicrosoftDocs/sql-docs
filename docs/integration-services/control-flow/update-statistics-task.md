---
title: "Update Statistics Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.updatestatisticstask.f1"
helpviewer_keywords: 
  - "updating statistics"
  - "Update Statistics task [Integration Services]"
ms.assetid: 0247483b-f092-4511-8fa8-3610108bd1bc
author: janinezhang
ms.author: janinez
manager: craigg
---
# Update Statistics Task
  The Update Statistics task updates information about the distribution of key values for one or more statistics groups (collections) in the specified table or indexed view. For more information, see [Statistics](../../relational-databases/statistics/statistics.md).  
  
 By using the Update Statistics task, a package can update statistics for a single database or multiple databases. If the task updates only the statistics in a single database, you can choose the views or the tables whose statistics the task updates. You can configure the update to update all statistics, column statistics only, or index statistics only.  
  
 This task encapsulates an UPDATE STATISTICS statement, including the following arguments and clauses:  
  
-   The *table_name* or *view_name* argument.  
  
-   If the update applies to all statistics, the WITH ALL clause is implied.  
  
-   If the update applies only to columns, the WITH COLUMN clause is included.  
  
-   If the update applies only to indexes, the WITH INDEX clause is included.  
  
 If the Update Statistics task updates statistics in multiple databases, the task runs multiple UPDATE STATISTICS statements, one for each table or view. All instances of UPDATE STATISTICS use the same clause, but different *table_name* or *view_name* values. For more information, see [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md) and [UPDATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/update-statistics-transact-sql.md).  
  
> [!IMPORTANT]  
>  The time the task takes to create the Transact-SQL statement that the task runs is proportionate to the number of statistics the task updates. If the task is configured to update statistics in all the tables and views in a database with a large number of indexes, or to update statistics in multiple databases, the task can take a considerable amount of time to generate the Transact-SQL statement.  
  
## Configuration of the Update Statistics Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This task is in the **Maintenance Plan Tasks** section of the **Toolbox** in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Update Statistics Task &#40;Maintenance Plan&#41;](../../relational-databases/maintenance-plans/update-statistics-task-maintenance-plan.md)  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
