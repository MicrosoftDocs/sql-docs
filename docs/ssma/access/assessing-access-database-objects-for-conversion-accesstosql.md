---
title: "Assessing Access Database Objects for Conversion (AccessToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "assessing SQL"
  - "assessing syntax"
  - "assessment reports"
  - "creating assessment reports"
  - "estimating migration effort"
  - "reports"
  - "SQL, assessing"
  - "syntax, assessing"
ms.assetid: 8b9e23d6-da62-437a-8c05-8ad2628b9441
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Assessing Access Database Objects for Conversion (AccessToSQL)
Before you load objects and migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you should determine how much of the migration will be successful, and how long the conversion might take. SSMA can create an assessment report that shows the percentage of objects that were successfully converted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure syntax and time estimates for performing the migration. SSMA also lets you view the specific issues that caused conversion failures.  
  
## Creating Assessment Reports  
When it creates an assessment report, SSMA converts the selected Access database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure syntax, and then shows the results.  
  
**To create an assessment report**  
  
1.  In Access Metadata Explorer, select the database or databases that you want to assess.  
  
2.  To omit individual objects, clear the check boxes next to the objects you do not want to assess.  
  
3.  Right-click **Databases**, and then select **Create Report**.  
  
    You can also analyze individual objects by right-clicking an object and then selecting **Create Report**.  
  
    SSMA shows progress in the status bar at the bottom of the window. If the Output pane is visible, you will also see messages in the Output pane.  
  
When the assessment is complete, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant for Access : Assessment Report window appears.  
  
## Using Assessment Reports  
The Assessment Report window contains three panes: an explorer, a details pane, and a message pane.  
  
-   The explorer pane lets you browse the objects that were assessed. You can click items in this pane to drill down to individual tables, indexes, and keys.  
  
-   The details pane shows the conversion statistics for the selected object.  
  
-   The message pane shows the errors, warnings, and informational messages for the conversion, and time estimates for performing the migration and individual error correction steps.  
  
You should correct errors before you run the assessment report again or convert schemas. To find errors, click the **Errors** button in the messages pane, and then expand each error to view a list of objects where the error occurred. If you click an object in the messages pane, all errors and warnings for that object will appear in the details pane.  
  
## Next Step  
[Converting Access Database Objects](converting-access-database-objects-accesstosql.md)  
  
## See Also  
[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)  
  
