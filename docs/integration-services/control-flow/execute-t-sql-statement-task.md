---
description: "Execute T-SQL Statement Task"
title: "Execute T-SQL Statement Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.executetsqlstatementtask.f1"
helpviewer_keywords: 
  - "Transact-SQL statements, SSIS"
  - "statements [Integration Services]"
  - "Execute T-SQL Statement task [Integration Services]"
ms.assetid: 7e9086ca-d27e-46c0-bfad-d61333ebd55e
author: chugugrace
ms.author: chugu
---
# Execute T-SQL Statement Task

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The Execute T-SQL Statement task runs Transact-SQL statements. For more information, see [Transact-SQL Reference &#40;Database Engine&#41;](../../t-sql/language-reference.md) and [Integration Services &#40;SSIS&#41; Queries](../../integration-services/integration-services-ssis-queries.md).  
  
 This task is similar to the Execute SQL task. However, the Execute T-SQL Statement task supports only the Transact-SQL version of the SQL language and you cannot use this task to run statements on servers that use other dialects of the SQL language. If you need to run parameterized queries, save the query results to variables, or use property expressions, you should use the Execute SQL task instead of the Execute T-SQL Statement task. For more information, see [Execute SQL Task](../../integration-services/control-flow/execute-sql-task.md).  
  
## Configuration of the Execute T-SQL Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This task is in the **Maintenance Plan Tasks** section of the **Toolbox** in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Execute T-SQL Statement Task &#40;Maintenance Plan&#41;](../../relational-databases/maintenance-plans/execute-t-sql-statement-task-maintenance-plan.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](./add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
  
## See Also  
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)   
 [MERGE in Integration Services Packages](../../integration-services/control-flow/merge-in-integration-services-packages.md)  
  
