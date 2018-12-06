---
title: "Options (Text Editor-Transact-SQL-IntelliSense) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "VS.ToolsOptionsPages.Text_Editor.SQL.Advanced"
dev_langs: 
  - "TSQL"
ms.assetid: 1855d916-5bf9-4d94-b0fb-9f9bb05ff950
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor-Transact-SQL-IntelliSense)
  The **Options** dialog box lets you change the IntelliSense settings for the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor. These settings are available when, on the **Tools** menu, you click **Options**, expand the **Text Editor** folder, expand the **Transact-SQL** folder, and then click **Advanced**.  
  
## Transact-SQL IntelliSense Settings  
 Specifies the IntelliSense options for the [!INCLUDE[ssDE](../includes/ssde-md.md)] Query Editor.  
  
### Enable IntelliSense  
 Enables the IntelliSense features. When this box is not selected, the specific IntelliSense options are unavailable. By default, this check box is selected.  
  
 **Underline errors**  
 Identifies syntax and semantic errors in [!INCLUDE[tsql](../includes/tsql-md.md)] statements in the query window. All errors and warnings appear in red. Errors and warnings are supported only for the [!INCLUDE[tsql](../includes/tsql-md.md)] statements for which IntelliSense has been implemented. By default, this check box is selected.  
  
 **Outline statements**  
 Enables the outlining feature when a file is opened. This creates collapsible blocks of [!INCLUDE[tsql](../includes/tsql-md.md)] code. By default, this check box is selected.  
  
 **Maximum script size**  
 Specifies the size at which IntelliSense functionality is disabled. If a script exceeds the specified size, a warning message is issued. All IntelliSense features, except color coding, are disabled for that editor window. IntelliSense is reenabled when enough text is deleted to reduce the script size to under the limit. Enabling IntelliSense for large script sizes can decrease performance on slow computers. The allowed sizes are 100 KB, 500 KB, 1 MB, 2 MB, and Unlimited. The default is 1 MB.  
  
 **Casing for built-in function names**  
 Specifies whether the names of built-in [!INCLUDE[tsql](../includes/tsql-md.md)] functions that are included in completion lists use uppercase letters, such as DATEADD; or lowercase letters, such as dateadd. Select the option that best matches the [!INCLUDE[tsql](../includes/tsql-md.md)] coding conventions that you are using.  
  
## See Also  
 [Troubleshooting IntelliSense &#40;SQL Server Management Studio&#41;](../relational-databases/scripting/troubleshooting-intellisense.md)  
  
  
