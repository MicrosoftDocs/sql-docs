---
title: "Options (Text Editor: Editor Tab and Status Bar Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.sqleditors.editorcontextsettings"
  - "VS.ToolsOptionsPages.Text_Editor.EditorTabAndStatusBar"
ms.assetid: e4815678-7885-4631-878f-c6a2b857ee05
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Options (Text Editor: Editor Tab and Status Bar Page)
  The **Editor Tab and Status Bar Page** lets you customize the information displayed by the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] Query Editors. You can specify the level of information displayed in the tab and status bar of the Query Editor window, and whether the status bar appears at the top or bottom of the editor window.  
  
## Option Settings by Editor  
 The editor tab and the status bar are available in all of the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] editors, but have different levels of functionality.  
  
 The Database Engine Query Editor can connect to a server group, in which case it opens connections to all the instances in the Server Group and can simultaneously run the same query on each connection. You can use this dialog to specify that the status bar has different colors when connected to multiple instances rather than one instance.To change the multi-server results options, use the [Options (Query Results/SQL Server/Multi-Server)](../../2014/database-engine/options-query-results-sql-server-multi-server.md) page.  
  
## Status Bar Content  
 Specifies the information that appears in the Query Editor status bar.  
  
 **Display execution time**  
 Includes the script execution time. The settings are as follows:  
  
 **None**  
 The status bar displays no time information.  
  
 **End**  
 The status bar displays the current time of day while the script is running. When the script completes, the display shows the time of day that the script completed.  
  
 **Elapsed**  
 The status bar displays the length of time that the script has been running. When the script completes, the display shows how much time was taken to run the script.  
  
 **Include database name**  
 Includes the name of the current database for the connection. When the query editor is first opened, this is the default database for the login. The database context can later be changed by using the Transact-SQL USE statement.  
  
 **Include login name**  
 Includes the login name.  
  
 **Include row count**  
 Includes a count of the rows that have been processed by the script that is currently executing.  
  
 **Include server name**  
 Includes the server name. For local connections, this is the instance name. For remote connections, this is the remote computer name and instance name.  
  
## Status Bar Layout and Colors  
 Specifies the colors for items in the Query Editor status bar.  
  
 **Group connections**  
 Set the color of the status bar when the Query Editor has more than one connection.  
  
 **Single server connections**  
 Set the color of the status bar when the Query Editor has one connection.  
  
 **Status bar location**  
 Specifies the location of the status bar. The settings are as follows:  
  
 **Top**  
 The status bar appears at the top of the Query Editor window.  
  
 **Bottom**  
 The status bar appears at the bottom of the Query Editor window.  
  
## Tab Text  
 Specifies the text that appears in the tab at the top of a Query Editor window. If the text is too long to display, you can view the full string in a tooltip that is displayed if you hover over the tab.  
  
 **Include database name**  
 Includes the name of the current database for the connection. When the query editor is first opened, this is the default database for the login. The database context can later be changed by using the Transact-SQL USE statement.  
  
 **Include file name**  
 Includes the name of the file where the script is stored.  
  
 **Include folder name**  
 Includes the path of the folder where the script file is stored.  
  
 **Include login name**  
 Includes the login name.  
  
 **Include server name**  
 Includes the server name. For local connections, this is the instance name. For remote connections, this is the remote computer name and instance name.  
  
## See Also  
 [Options &#40;Environment: Fonts and Colors Page&#41;](../ssms/menu-help/options-environment-fonts-and-colors-page.md)   
 [Color Coding in Query Editors](../relational-databases/scripting/color-coding-in-query-editors.md)  
  
  
