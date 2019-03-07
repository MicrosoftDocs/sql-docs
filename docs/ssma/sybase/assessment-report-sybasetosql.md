---
title: "Assessment Report (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: af24f2c4-5e86-4135-a4f3-a24faaeeefe7
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Assessment Report (SybaseToSQL)
The Assessment Report window shows the results of the conversion of database objects to [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, and can also help you estimate the complexity and cost of your migration projects.  
  
To access the Assessment Report, select objects to convert in source metadata explorer, right-click **Databases**, and then select **Create Report**.  
  
## Options  
**Conversion statistics**  
Shows the conversion statistics by statement type. This pane is visible only when a group object, such as a schema, or an object without code is selected in the left pane.  
  
**Objects by Category**  
Shows the conversion statistics by object type. This pane is visible only when a group object, such as a schema, or an object without code is selected in the left pane.  
  
**Statistics**  
Shows the conversion statistics for the selected object. This pane is visible only when an individual object with code is selected in the left pane. You might have to expand **Statistics** to view this pane.  
  
**Source navigation**  
Shows the ASE code for the selected object, and highlights code that was not converted to [!INCLUDE[tsql](../../includes/tsql-md.md)]. This pane is visible only when an individual object with code is selected in the left pane.  
  
Click the line numbers to set or clear bookmarks. Use the buttons at the top of the pane to navigate through the code.  
  
**Target navigation**  
Shows the conversion's resulting [!INCLUDE[tsql](../../includes/tsql-md.md)] code for the selected object, and error messages for code that was not converted. This pane is visible only when an individual object with code is selected in the left pane.  
  
Click the line numbers to set or clear bookmarks. Use the buttons at the top of the pane to navigate through the code.  
  
**Messages pane**  
Shows the errors, warnings, and information messages that are generated when creating the assessment report. Messages are grouped by number. To view the code that caused the error, click **Error**, **Warning**, or **Info**, expand the category of messages, and then click a message.  
  
