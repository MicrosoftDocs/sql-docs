---
title: "SCM Services - Prevent Automatic Startup of an Instance"
description: Find out how to prevent an instance of SQL Server from starting automatically. See how to set the start mode to manual to accomplish this task.
author: rwestMSFT
ms.author: randolphwest
ms.date: "01/06/2016"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "automatic SQL Server startup"
  - "SQL Server, stopping"
  - "SQL Server, automatic startup"
  - "canceling automatic startup"
  - "stopping SQL Server"
  - "preventing automatic startups [SQL Server]"
---
# SCM Services - Prevent Automatic Startup of an Instance
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how prevent an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from starting automatically in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Configuration Manager. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is normally configured to start automatically. You can change that by setting the start mode for the instance to manual.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To prevent automatic startup of an instance of SQL Server  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
    > [!NOTE]  
    >  Because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is a snap-in for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager does not appear as an application in newer versions of Windows.  
    >   
    >  -   **Windows 10 and Windows 11**:  
    >          To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, on the **Start Page**, type SQLServerManager13.msc (for [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]). For other versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replace 13 with the appropriate number. Clicking SQLServerManager13.msc opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click SQLServerManager13.msc, and then click **Open file location**. In the Windows File Explorer, right-click SQLServerManager13.msc, and then click **Pin to Start** or **Pin to taskbar**.  
    > -   **Windows 8**:  
    >          To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the **Search** charm, under **Apps**, type **SQLServerManager\<version>.msc** such as **SQLServerManager13.msc**, and then press **Enter**.  
  
2.  In SQL Server Configuration Manager, expand **Services**, and then click **SQL Server**.  
  
3.  In the details pane, right-click **MSSQLServer**, and then click **Properties.**  
  
4.  In the **SQL Server \<**_instancename_**> Properties** dialog box, on the **Service** tab, in the **General** box, set the value of **Start Mode** to **Manual**.  
  
5.  Click **OK** to close the **SQL Server \<**_instancename_**> Properties** dialog box, and then close [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
## See Also  
 [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)  
  
  
