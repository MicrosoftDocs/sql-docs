---
description: "Assessing DB2 Schemas for Conversion (DB2ToSQL)"
title: "Assessing DB2 Schemas for Conversion (DB2ToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 8892f5a4-72ba-4406-8649-7a9d67f4c1d9
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.db2.assessment.f1"
---
# Assessing DB2 Schemas for Conversion (DB2ToSQL)
Before you load objects and migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should determine how complex the migration will be and how much time the migration will take. SSMA can create an assessment report that shows the percentage of objects that will be successfully converted. SSMA also lets you view the specific issues that cause conversion failures.  
  
## Creating Assessment Reports  
When it creates this assessment report, SSMA converts the selected DB2 database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax, and then shows the results.  
  
**To create an assessment report**  
  
1.  In DB2 Metadata Explorer, select the schemas to assess.  
  
2.  To omit individual objects, clear the check boxes next to those.  
  
3.  Right-click **Schemas**, and then select **Create Report**.  
  
    You can also analyze individual objects by right-clicking an object, and then selecting **Create Report**.  
  
    SSMA will show progress in the status bar at the bottom of the window. If the Output pane is visible, you will also see messages in the Output pane.  
  
    When the assessment is complete, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant for DB2: Assessment Report window will appear.  
  
## Using Assessment Reports  
The Assessment Report window contains three panes:  
  
-   The left pane contains the hierarchy of objects that are included in the assessment report. You can browse the hierarchy, and select objects and categories of objects to view conversion statistics and code.  
  
-   The contents of the right pane depends on the item that is selected in the left pane.  
  
    If a group of objects is selected, such a schema, or if a table is selected, the right pane contains a Conversion statistics pane and an Objects by Categories pane. The Conversion Statistics pane shows the conversion statistics for the selected objects. The Objects by Categories pane shows the conversion statistics for the object or categories of objects.  
  
    If a function, package, procedure, sequence, or view is selected, the right pane contains statistics, source code, and target code.  
  
    -   The top area shows the overall statistics for the object. You might have to expand **Statistics** to view this information.  
  
    -   The Source area shows the source code of the object that is selected in the left pane. The highlighted areas show problematic source code.  
  
    -   The Target area shows the converted code. Red text shows problematic code and error messages.  
  
-   The bottom pane shows conversion messages, grouped by message number. You can click **Errors**, **Warnings**, or **Info** to view categories of messages, and then expand a group of messages. Click an individual message to select the object in the left pane and display the details in the right pane.  
  
## Analyzing Conversion Problems By Using the Assessment Report  
The Conversion Statistics pane shows the conversion statistics. If the percentage for any category is less than 100 percent, you should determine why the conversion was not successful.  
  
**To view conversion problems**  
  
1.  Create the assessment report by using the instructions in the previous procedure.  
  
2.  In the left pane, expand schemas or folders that have a red error icon. Continue expanding items until you select an individual item that failed conversion.  
  
3.  At the top of the Source pane, click **Next Problem**.  
  
    The problematic code is highlighted, as is the related code in the Target Navigation pane.  
  
4.  Review any error messages, and then determine what you want to do with the object that caused the conversion problem:  
  
    -   Update the DB2 syntax in SSMA. You can update the syntax for procedures, functions, triggers, packaged functions and packaged procedures. To update the syntax, select the object in the DB2 Metadata Explorer pane, click the **SQL** tab, and then modify the SQL code. When you navigate away from the item, you will be prompted to save the updated syntax. You can view the reported errors for the object on the **Report** tab.  
  
    -   In DB2, you can modify the DB2 object to remove or revise problematic code. To load the updated code into SSMA, you will have to update the metadata. For more information, see [Connecting to DB2 Database &#40;DB2ToSQL&#41;](../../ssma/db2/connecting-to-db2-database-db2tosql.md).  
  
    -   You can exclude the object from migration. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer and DB2 Metadata Explorer, clear the check box next to the item before you load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data from DB2.  
  
## Next Step  
[Converting DB2 Schemas &#40;DB2ToSQL&#41;](../../ssma/db2/converting-db2-schemas-db2tosql.md)  
  
## See Also  
[Migrating DB2 Databases to SQL Server &#40;DB2ToSQL&#41;](../../ssma/db2/migrating-db2-databases-to-sql-server-db2tosql.md)  
  
