---
description: "Projects (SQL Server Management Studio)"
title: "Projects (SQL Server Management Studio)"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: c13af859-ca66-4e43-b76a-0650ac6566c0
author: "markingmyname"
ms.author: "maghan"
---
# Projects (SQL Server Management Studio)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
A [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] project is a collection of logically related scripts and files that can be saved together for database administration and development.  
  
## Script Project Overview  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] script projects are displayed in the Solution Explorer component of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. A script project can contain zero or more project files. You can add a project to a solution or combine more than one project within a solution.  
  
Projects can include the following:  
  
-   **Connections**. A connection, as persisted within a project, will contain login information, server name, default database, preferred protocol, authentication type, and connection properties. Connection information may optionally be stored with a script (see below).  
  
-   **SQLScripts**. Frequently used SQL scripts for the user. Double-clicking a .sql file within the project will cause the SQL Editor to open the selected script.  
  
-   **MDX, DMX, and XMLAScripts**. Frequently used MDX scripts for the user. Double-clicking an .mdx file within the project will cause the Editor to open the selected script.  
  
-   **Misc**. This folder can be used for files that do not neatly fit into any of the other default node types, such as a text file that contains the project objectives.  
  
Projects can also be integrated into a source code control system.  
  
## Connecting to an Instance of SQL Server from a Script Project  
A script project may contain connections to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a project by clicking the connection. This will open an SQL Script window that is connected to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] defined in the connection you selected. If you open a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or MDX script with a connection that uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication, you will be prompted for the password using the **Connect to SQL Server** dialog box after the Editor has been opened and the script loaded.  
  
The connection will be closed after the corresponding window is closed.  
  
To modify information about a connection, use the properties window in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
## Project Tasks  
  
|Task Description|Topic|  
|--------------------|---------|  
|Describes how to create a new project in a solution.|[Create a Project](../../ssms/solution/create-a-project.md)|  
|Describes how to add an existing project to a solution.|[Add an Existing Project to a Solution](../../ssms/solution/add-an-existing-project-to-a-solution.md)|  
|Describes how to change the default location at which project files are saved.|[Change the Default Location for Projects](../../ssms/solution/change-the-default-location-for-projects.md)|  
|Describes how to view the current properties for a project.|[View Project Properties](../../ssms/solution/view-project-properties.md)|  
|Describes how to add new items, such as connections or script files, to a project.|[Add New Items to a Project](../../ssms/solution/add-new-items-to-a-project.md)|  
|Describes how to establish the connection information for a query.|[Associate a Query with a Connection in a Project](../../ssms/solution/associate-a-query-with-a-connection-in-a-project.md)|  
|Describes how to change the connection information for a query.|[Change the Connection Associated with a Query](../../ssms/solution/change-the-connection-associated-with-a-query.md)|  
|Describes how to change connection properties.|[View or Change the Properties of a Connection in a Project](../../ssms/solution/view-or-change-the-properties-of-a-connection-in-a-project.md)|  
  
## See Also  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Solutions &#40;SQL Server Management Studio&#41;](../../ssms/solution/solutions-sql-server-management-studio.md)  
[Solution Explorer Source Control](./solution-explorer.md)  
