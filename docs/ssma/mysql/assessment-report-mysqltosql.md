---
description: "Assessment Report (MySQLToSQL)"
title: "Assessment Report (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 5525d989-024c-402d-9e84-faa4721cc5b9
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.mysql.assessmentreport.f1"
---
# Assessment Report (MySQLToSQL)
The Assessment Report window shows the results of the conversion of database objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, and can also help you estimate the complexity and cost of your migration projects.  
  
To access the Assessment Report, select objects to convert in source metadata explorer, right-click **Schemas**, and then select **Create Report**.  
  
## Options  
  
|**Term**|**Definition**|  
|-|-|  
|**Conversion statistics**|Shows the conversion statistics by statement type. This pane is visible when a group object, such as a schema, or an object without code is selected in the left pane.|  
|**Objects by Categories**|Shows the number of objects by category. This pane is visible only when a group object, such as a schema, or an object without code is selected in the left pane.|  
|**Statistics**|Shows the conversion statistics for the selected object. This pane is visible only when an individual object with code is selected in the left pane. You might have to expand **Statistics**, which is immediately above the **Source** pane, to view this pane.|  
|**Source**|Shows the MySQL code for the selected object, and highlights code that was not converted to [!INCLUDE[tsql](../../includes/tsql-md.md)]. This pane is visible only when an individual object with code is selected in the left pane.<br /><br />Click the line numbers to set or clear bookmarks. Use the buttons at the top of the pane to navigate through the code.|  
|**Target**|Shows the conversion's resulting [!INCLUDE[tsql](../../includes/tsql-md.md)] code for the selected object, and error messages for code that was not converted. This pane is visible only when an individual object with code is selected in the left pane.<br /><br />Click the line numbers to set or clear bookmarks. Use the buttons at the top of the pane to navigate through the code.|  
|**Messages pane**|Shows the errors, warnings, and informational messages that were generated while creating the assessment report. Messages are grouped by number. To view the code that caused the error, click **Errors**, **Warnings**, or **Info**, expand the category of messages, and then click a message.|  
  
