---
description: "Add Existing Items to a Project"
title: "Add Existing Items to a Project"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "projects [SQL Server Management Studio], item additions"
  - "adding project items"
ms.assetid: 084b3879-e96b-45a7-b421-6a4b0db2b92b
author: "markingmyname"
ms.author: "maghan"
---
# Add Existing Items to a Project
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
Add new items to a project to extend application functionality. An existing item can be a query or a miscellaneous file. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] has two project types: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Script Project, and Analysis Services Script Project. The project type determines the query files that you can add to the project. For example, you can add a [!INCLUDE[tsql](../../includes/tsql-md.md)] query (a file with a .sql extension) to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Script project, but you cannot add it to an Analysis Services Script Project. To associate additional file extensions to a project type, see [How to: Associate File Extensions to a Code Editor](../scripting/associate-file-extensions-to-a-code-editor.md).  
  
### To add an existing query or a miscellaneous file to a project  
  
1.  In Solution Explorer, select a target project.  
  
2.  On the **Project** menu, click **Add Existing Item**.  
  
    **Look in**  
    Locate the files or folders to add to your project from this list. For XML Web services and ASP.NET Web applications, the files are located on the Web server.  
  
    **Desktop**  
    Displays the files and folders located on the desktop.  
  
    **My Projects**  
    Displays the files and folders at the default **My Projects** location.  
  
    **My Computer**  
    Displays the contents of your **My Computer** folder.  
  
    **File name**  
    Use this option to filter the files and folders that are displayed. Enter the full or partial file name on which to filter; use an asterisk (`*`) as a wildcard.  
  
    > [!NOTE]  
    > Navigate to Web and network locations by entering the URL or network path in the **File name** box. For example, **`https://mywebsite`** displays the files available at the mywebsite Web location, and **\\\myserver\myshare** displays the files available at the myshare location on myserver.  
  
    **Files of type**  
    Use this option to filter files based on file extension. Each product lists default filters of the most common file types.  
  
    **Add**  
    Use this drop-down button to add the item to the project and open the item in its default editor.  
  
    -   **Add**  
  
        Copies the existing item to your project folder on disk and adds the item under the selected project in Solution Explorer. Any changes made to the item are not reflected in the item at the original location. This is the default editor for the file type.  
  
    -   **Add As Link**  
  
        Adds the selected file to the project and opens it with the default editor for the file type. This option opens the originally selected file and does not copy the file to the project folder.  
  
3.  If you are adding query files, the connection dialog will prompt you to specify a connection for the query. You can click **Cancel** on the connection dialog if you do not want to associate a connection to the query.  
  
4.  The file will be added to the **Queries** or **Miscellaneous Files** folder of the project.  
  
## See Also  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Add New Items to a Project](../../ssms/solution/add-new-items-to-a-project.md)  
[Remove or Delete an Item or Project](../../ssms/solution/remove-or-delete-an-item-or-project.md)  
