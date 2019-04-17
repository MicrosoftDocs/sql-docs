---
title: "Assessment Report (DB2ToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 9e13eba0-e3cf-4205-974f-c00f982061de
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Assessment Report (DB2ToSQL)
The Assessment Report window shows the results of the conversion of database objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, and can also help you estimate the complexity and cost of your migration projects.  
  
To access the Assessment Report, select objects to convert in source metadata explorer, right-click **Schemas** or **Synonyms**, and then select **Create Report**.  
  
## Options  
  
|||  
|-|-|  
|Term|Definition|  
|**Conversion statistics**|Shows the conversion statistics by statement type. This pane is visible when a group object, such as a schema, or an object without code is selected in the left pane.|  
|**Objects by Categories**|Shows the number of objects by category. This pane is visible only when a group object, such as a schema, or an object without code is selected in the left pane.|  
|**Statistics**|Shows the conversion statistics for the selected object. This pane is visible only when an individual object with code is selected in the left pane. You might have to expand **Statistics**, which is immediately above the **Source** pane, to view this pane.|  
|**Source**|Shows the DB2 code for the selected object, and highlights code that was not converted to [!INCLUDE[tsql](../../includes/tsql-md.md)]. This pane is visible only when an individual object with code is selected in the left pane.<br /><br />Click the line numbers to set or clear bookmarks. Use the buttons at the top of the pane to navigate through the code.|  
|**Target**|Shows the conversion's resulting [!INCLUDE[tsql](../../includes/tsql-md.md)] code for the selected object, and error messages for code that was not converted. This pane is visible only when an individual object with code is selected in the left pane.<br /><br />Click the line numbers to set or clear bookmarks. Use the buttons at the top of the pane to navigate through the code.|  
|**Messages pane**|Shows the errors, warnings, and informational messages that were generated while creating the assessment report. Messages are grouped by number. To view the code that caused the error, click **Errors**, **Warnings**, or **Info**, expand the category of messages, and then click a message.|  
  
