---
title: "Open an Editor (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 5d654a60-d205-49d2-a831-b3d986d60024
author: MightyPen
ms.author: genemi
manager: craigg
---
# Open an Editor (SQL Server Management Studio)
  This topic describes how to open the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query, MDX, DMX, or XML/A editors in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. When opened, each editor window appears as a tab in the central pane of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
## Before You Begin  
 [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] supports four editors: the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor for editing [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, the DMX and MDX editors for editing scripts using those languages, and the XML/A editor for editing XML/A scripts or XML files. Any of the editors can also be used to edit text files.  
  
### Limitations and Restrictions  
 If you share files with users at other sites that use distinct code pages, you should save your file with the appropriate Unicode code page to prevent errors when reading the file. Also, when saving files for UNIX or Macintosh, be sure to save your files with the appropriate document format. On the **File** menu, click **Save As**, **Save with Encoding** from the down arrow next to the **Save** button, and then choose **Unix** or **Macintosh** under **Line Endings**.  
  
### Permissions  
 Operations you perform in a code editor are subject to the permissions granted to the authentication account you used to log in. For example, if you open a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window using Windows Authentication, you cannot execute [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that reference objects your Windows login account does not have permissions to access.  
  
## How to: Open Editors  
 This section explains how to open the various editors in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
### Using the File/New Menu  
 On the **File** menu, click **New**, and then select one of the query editor options:  
  
-   **Query with Current Connection** -Opens a new editor window of the type associated with the current connection in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. The editor window uses the same authentication information as the current connection. For example, if you select an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] in Object Explorer, and then use **Query with Current Connection**, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] opens a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor connected to the same instance using the same authentication information.  
  
-   **Database Engine Query** - Opens a new [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor and a dialog to get the information required to connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   **Analysis Services MDX Query** - Opens a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] MDX Query Editor and a dialog to get the information required to connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   **Analysis Services DMX Query** - Opens a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] DMX Query Editor and a dialog to get the information required to connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   **Analysis Services XML/A Query** - Opens a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] XML/A Query Editor and a dialog to get the information required to connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
### Using the File/Open Menu  
 On the **File** menu, click **Open**, and then navigate to a file and open it. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] opens the appropriate type of editor for the file extension, copies the contents of the file into the editor window, and also opens a connection dialog if needed. For example, if you open a file with a .sql extension, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] opens a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window, copies in the contents of the .sql file, and opens a connection dialog. If you open a file with an extension not associated with a specific editor, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] opens a text editor window and copies in the contents of the file.  
  
 For more information, see [Associate File Extensions to a Code Editor](associate-file-extensions-to-a-code-editor.md).  
  
### Using the Toolbar  
 On the **Standard** toolbar, click one of the following buttons:  
  
-   **New Query** - -Opens a new editor window of the type associated with the current connection in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. The editor window uses the same authentication information as the current connection. For example, if you select an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] in Object Explorer, and then click the **New Query** button, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] opens a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor connected to the same instance using the same authentication information.  
  
-   **Database Engine Query** - Opens a new [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor and a dialog to get the information required to connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   **Analysis Services MDX Query** - Opens a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] MDX Query Editor and a dialog to get the information required to connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   **Analysis Services DMX Query** - Opens a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] DMX Query Editor and a dialog to get the information required to connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   **Analysis Services XML/A Query** - Opens a new [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] XML/A Query Editor and a dialog to get the information required to connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
### Using Object Explorer  
 From **Object Explorer**:  
  
-   Right-click the server node connected to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], then select **New Query**. This will open a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window connected to the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and set the database context of the window to the default database for the login.  
  
-   Right-click a database node, and then select **New Query**. This will open a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window connected to the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and set the database context of the window to the same database.  
  
### Using Solution Explorer  
 From **Solution Explorer**, expand a folder, right-click an item within the folder, and then click **Open** or double-click the item or file.  
  
### Using Template Browser to Open the Database Engine Query Editor  
  
-   On the **View** menu, click **Template Explorer**.  
  
-   The **Template Browser** window appears in the right pane.  
  
-   Double-click a template to open a Database Engine Query window with the text of the template. For example, to open a CREATE DATABASE template, open the **SQL Server Templates** folder, open the **Databases** folder, and double-click **create database**.  
  
  
