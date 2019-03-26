---
title: "Check Database Integrity Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.checkdatabaseintegritytask.f1"
helpviewer_keywords: 
  - "data integrity [Integration Services]"
  - "Check Database Integrity task [Integration Services]"
  - "checking database consistency"
  - "database consistency checks [Integration Services]"
  - "verifying database consistency"
  - "integrity checking [Integration Services]"
ms.assetid: 5a82fe99-4503-429f-9337-e6bac7649fe4
author: janinezhang
ms.author: janinez
manager: craigg
---
# Check Database Integrity Task
  The Check Database Integrity task checks the allocation and structural integrity of all the objects in the specified database. The task can check a single database or multiple databases, and you can choose whether to also check the database indexes.  
  
 The Check Database Integrity task encapsulates the DBCC CHECKDB statement. For more information, see [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md).  
  
## Configuration of the Check Database Integrity Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer. This task is in the **Maintenance Plan Tasks** section of the **Toolbox** in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Check Database Integrity Task &#40;Maintenance Plan&#41;](../../relational-databases/maintenance-plans/check-database-integrity-task-maintenance-plan.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
  
