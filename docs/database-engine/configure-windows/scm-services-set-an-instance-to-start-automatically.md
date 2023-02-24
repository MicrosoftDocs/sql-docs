---
title: "SCM Services - Set an Instance to Start Automatically"
description: Find out how to set an instance of SQL Server to start automatically. Learn about the default configuration, and see how to set the start mode to automatic.
author: rwestMSFT
ms.author: randolphwest
ms.date: "01/06/2016"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "automatic SQL Server startup"
  - "SQL Server, automatic startup"
  - "starting SQL Server, automatically"
---
# SCM Services - Set an Instance to Start Automatically
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to set an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to start automatically in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Configuration Manager. During setup, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is normally configured to start automatically. If this was not done, you can change that setting at any time.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To set an instance of SQL Server to start automatically  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
    > [!NOTE]  
    >  Because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is a snap-in for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager does not appear as an application in newer versions of Windows.  
    >   
    >  -   **Windows 10 and Windows 11**:  
    >          To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, on the **Start Page**, type SQLServerManager13.msc (for [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]). For other versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replace 13 with the appropriate number. Clicking SQLServerManager13.msc opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click SQLServerManager13.msc, and then click **Open file location**. In the Windows File Explorer, right-click SQLServerManager13.msc, and then click **Pin to Start** or **Pin to taskbar**.  
    > -   **Windows 8**:  
    >          To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the **Search** charm, under **Apps**, type **SQLServerManager\<version>.msc** such as **SQLServerManager13.msc**, and then press **Enter**.  
  
2.  In **SQL Server Configuration Manager**, expand **Services**, and then click **SQL Server**.  
  
3.  In the details pane, right-click the name of the instance you want to start automatically, and then click **Properties**.  
  
4.  In the **SQL Server \<**_instancename_**> Properties** dialog box, set **Start Mode** to **Automatic**.  
  
5.  Click **OK**, and then close [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
## See Also  
 [Prevent Automatic Startup of an Instance of SQL Server &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/scm-services-prevent-automatic-startup-of-an-instance.md)   
 [Connect to Another Computer &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/scm-services-connect-to-another-computer.md)   
 [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)  
  
  
