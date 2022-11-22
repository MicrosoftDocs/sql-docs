---
title: "Configure Editors (SQL Server Management Studio)"
description: Learn how to customize the operation of the SQL Server Management Studio editors by setting options in the Options dialog.
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: e7c7a8ef-f561-4258-a7b6-c445dba69f87
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Configure Editors (SQL Server Management Studio)

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can customize the operation of the SQL Server Management Studio editors by configuring the options for each editor.  
  
## Setting Editor Options  
 Most of the editor options are set by using the **Tools** menu and selecting **Options...** to display an **Options** dialog. In the **Options** dialog, open the **Text Editor** node in the left pane to set code and text editing options. The nodes under Text Editor apply to specific editors:  
  
1.  **All Languages** - options set using this node apply to all of the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] editors. You can override these settings by using the other nodes to set different options for a specific editor.  
  
2.  **Plain Text** - options set using this node apply to the MDX, DMX, and text editors.  
  
3.  **Transact-SQL** - options set using this node apply to the Database Engine Query Editor.  
  
4.  **XML** - options set using this node apply to the XML for Analysis editor.  
  
 Open the **Query Execution** or **Query Results** nodes to customize the execution of queries and how the results are displayed.  
  
## Editor Configuration Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to specify that an editor be opened by double-clicking on a file with a specified extension in Windows Explorer.|[Associate File Extensions to a Code Editor](./associate-file-extensions-to-a-code-editor.md)|  
|Describes how to customize fonts to make code and text more readable.|[Change Font Color, Size, and Style](./change-font-color-size-and-style.md)|  
|Describes how to view properties.|[Use the Properties Window in Management Studio](./use-the-properties-window-in-management-studio.md)|  
|Location of the F1 help pages for the editor options dialogs.|[Query Options Pages F1 Help](../f1-help/f1-help-for-server-connections-sql-server-management-studio.md)|