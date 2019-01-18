---
title: "Assessing SAP ASE Database Objects for Conversion (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/01/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: eb996b7c-1eef-4f73-b5e6-2fa6faf7336c
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Assessing SAP ASE database objects for conversion (SybaseToSQL)
Before you load objects and migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL, you should determine how complexity of the migration and how much time it should take. SSMA can create an assessment report that shows the percentage of objects and procedures that will successfully be converted to [!INCLUDE[tsql](../../includes/tsql-md.md)]. SSMA also lets you view the specific issues that can cause conversion failures.  
  
## Create assessment reports  
When creating this assessment report, SSMA converts the selected SAP Adaptive Server Enterprise (ASE) database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL syntax, and then shows the results.  
  
**To create an assessment report**  
  
1.  In Sybase Metadata Explorer, select the databases that you want to assess.  
  
2.  To omit individual objects, clear the check boxes next to the objects that you do not want to assess.  
  
3.  Right-click **Databases**, and then select **Create Report**.  
  
    You can also analyze individual objects by right-clicking an object, and then selecting **Create Report**.  
  
    SSMA shows progress in the status bar at the bottom of the window. If the Output pane is visible, you will also see any related messages.  
  
    When the assessment is complete, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant for Sybase : Assessment Report window will appear.  
  
## Use assessment reports  
The Assessment Report window contains three panes:  
  
-   The left pane contains the hierarchy of objects that are included in the assessment report. You can browse the hierarchy and select objects and object categories to view conversion statistics and code.  
  
-   The contents of the right pane varies based on which  item is selected in the left pane.  
  
    If a group of objects (such as a schema) or a table is selected, the right pane displays two panes. The **Conversion Statistics** pane shows the conversion statistics for the selected objects. The **Objects by Categories** pane shows the conversion statistics for the object or categories of objects.  
  
    If a stored procedure, view, or trigger is selected, the right pane contains statistics, source code, and target code.  
  
    -   The top area shows the overall statistics for the object. You might have to expand **Statistics** to view this information. 
    -   The Source area shows the source code of the object that is selected in the left pane. The highlighted areas show problematic source code.  
    -   The Target area shows the converted code. Red text shows problematic code and error messages.  
  
-   The bottom pane shows conversion messages, grouped by message number. Select **Errors**, **Warnings**, or **Info** to view categories of messages, and then expand a group of messages. Click on an individual message to select the object in the left pane and then display the details in the right pane.  
  
## Analyze conversion problems by using the assessment report  
The **Conversion Statistics panes** show the conversion statistics. If the percentage for any category is less than 100 percent, you should determine why the conversion was not successful.  
  
**To view conversion problems**  
  
1.  Create the assessment report by using the instructions in the previous procedure.  
  
2.  In the left pane, expand schemas or folders that have a red error icon. Continue expanding items until you select an individual item that failed conversion.  
  
3.  At the top of the Source pane, select **Next Problem**.  
    The problematic code is highlighted, as is the related code in the **Target Navigation** pane.  
  
4.  Review any error messages, and then determine what you want to do with the object that caused the conversion problem:  
  
    -   Update the ASE syntax in SSMA. You can update the syntax only for stored procedures and triggers. To update the syntax, select the object in the Sybase Metadata Explorer pane, click the **SQL** tab, and then edit the SQL code. When you navigate away from the item, you are prompted to save the updated syntax. View the reported errors for the object on the **Report** tab.  
  
    -   In ASE, you can alter the ASE object to remove or revise problematic code. To load the updated code into SSMA, you will have to update the metadata. For more information, see [Connecting to Sybase ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-sybase-ase-sybasetosql.md).  
  
    -   You can exclude the object from migration. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Metadata Explorer and Sybase Metadata Explorer, clear the check box next to the item before you load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL and migrate data from ASE.
  
## Next steps  
[Converting SAP ASE Database Objects &#40;SybaseToSQL&#41;](../../ssma/sybase/converting-sybase-ase-database-objects-sybasetosql.md)  
  
## See Also  
[Migrating SAP ASE Databases to SQL Server - Azure SQL DB &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
  
