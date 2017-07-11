---
title: "Start SQL Server Management Studio | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 25ffaea6-0eee-4169-8dd0-1da417c28fc6
caps.latest.revision: 45
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Lesson 1-1 - Start SQL Server Management Studio
To begin this tutorial, let's take a look at [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## Opening SQL Server Management Studio  
  
#### To open SQL Server Management Studio  
  
1.  How you start [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] (SSMS) depends upon your operating system.  
* For newer versions of Windows with a **Start Page**, on the **Start Page**, type **[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]** and the program will appear. Click the program to open SSMS. You might want to right-click the program and pin it to the **Start Page**.   
* For older versions of Windows, on the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], and then click **SQL Server Management Studio**. Alternately from the **Run** dialog box, type **SSMS.exe** and then click **OK**.  
  
    > [!NOTE]  
    >  If SSMS doesn't appear, you may not have successfully installed SSMS. Install SSMS from the [download center](https://msdn.microsoft.com/library/mt238290.aspx). SSMS is not automatically installed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016. Use the latest version to access all of the features.  
  
2.  In the next step, you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the **Object Explorer** component of SSMS. If the Object Explorer pane is not visible, on the **View** menu, click **Object Explorer**. In the Object Explorer menu, click the **Connect** button, and then click **Database Engine**. The **Connect to Server** dialog box should appear. (If you have previously installed SSMS, the user settings might be causing the **Connect to Server** dialog box to appear automatically.)  
  
3.  In the **Connect to Server** dialog box, complete the **Server name** box. You can connect to one of three types of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each type has a slightly different format for the **Server name** box. Pick one of the following formats:  
--  **A default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:** When you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a computer, you can specify that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be a default (unnamed instance) or a named instance. If you are connecting to a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], insert the name of the computer. For example, if you are running SSMS on a computer named Accounting and you are connecting to a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  installed on that computer, type **Accounting** in the **Server name** box.  
--  **A named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:** During setup of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can specify a name for the instance; for example on a computer named Accounting, you could specify a named instance named **Receivables**. To connect to a named instance, in the **Server name** box, type the computer name backslash instance name; for example **Accounting\Receivables**.  
--  **An Azure SQL Database:** The format of the server name for SQL Database is SQL_Server_name.database.windows.net, for example **mydb2.database.windows.net**. If you  have trouble determining your server name, visit the Azure portal for help creating a connection string.  
  
4. In the **Authentication** area  
  
## Management Studio Components  
[!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] presents information in windows dedicated to specific types of information. Database information is shown in Object Explorer and document windows.  
  
-   Object Explorer is a tree view of all the database objects in a server. This can include the databases of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. Object Explorer includes information for all servers to which it is connected. When you open [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you are prompted to connect Object Explorer to the settings that were last used. You can double-click any server in the Registered Servers component to connect to it, but you do not have to register a server to connect.  
  
-   The document window is the largest portion of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. The document windows can contain query editors and browser windows. By default, the Summary page is displayed, connected to the instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] on the current computer.  
  
## Showing Additional Windows  
  
#### To show the Registered Servers window  
  
1.  On the **View** menu, click **Registered Servers**.  
  
    The Registered Servers window appears above or adjacent to Object Explorer. You can drag it and dock it in various locations. Registered Servers lists servers which you manage frequently. You can add and remove servers from this list. The only servers listed are the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances on the computer where you are running [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
2.  If your server does not appear, in Registered Servers, right-click **Database Engine**, click **Tasks**, and then click **Update Local Server Registration**. To add additional SQL Servers or a SQL Database, right-click a location in Registered Servers, and then click **New Server Registration**. Complete the **Login** area, like you completed the **Connect to Server** dialog box.  
  
## Next Task in Lesson  
[Connect with Registered Servers and Object Explorer](../../tools/sql-server-management-studio/lesson-1-2-connect-with-registered-servers-and-object-explorer.md)  
  
