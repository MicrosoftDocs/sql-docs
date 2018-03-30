---
title: "Set an Instance of SQL Server to Start Automatically (SQL Server Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "2016-01-07"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "automatic SQL Server startup"
  - "SQL Server, automatic startup"
  - "starting SQL Server, automatically"
ms.assetid: aa2b6bde-e76d-4fea-a560-54a63745d9b1
caps.latest.revision: 34
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---
# Set an Instance of SQL Server to Start Automatically (SQL Server Configuration Manager)
  This topic describes how to set an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to start automatically in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] by using SQL Server Configuration Manager. During setup, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is normally configured to start automatically. If this was not done, you can change that setting at any time.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To set an instance of SQL Server to start automatically  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
    > [!NOTE]  
    >  Because [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager is a snap-in for the [!INCLUDE[msCoName](../includes/msconame-md.md)] Management Console program and not a stand-alone program, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager does not appear as an application in newer versions of Windows.  
    >   
    >  -   **Windows 10**:  
    >          To open [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager, on the **Start Page**, type SQLServerManager12.msc (for [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]). For previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] replace 12 with a smaller number. Clicking SQLServerManager12.msc opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click SQLServerManager12.msc, and then click **Open file location**. In the Windows File Explorer, right-click SQLServerManager12.msc, and then click **Pin to Start** or **Pin to taskbar**.  
    > -   **Windows 8**:  
    >          To open [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager, in the **Search** charm, under **Apps**, type **SQLServerManager\<version>.msc** such as `SQLServerManager12.msc`, and then press **Enter**.  
  
2.  In **SQL Server Configuration Manager**, expand **Services**, and then click **SQL Server**.  
  
3.  In the details pane, right-click the name of the instance you want to start automatically, and then click **Properties**.  
  
4.  In the **SQL Server \<***instancename***> Properties** dialog box, set **Start Mode** to **Automatic**.  
  
5.  Click **OK**, and then close [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager.  
  
## See Also  
 [Prevent Automatic Startup of an Instance of SQL Server &#40;SQL Server Configuration Manager&#41;](../../2014/database-engine/scm-services-prevent-automatic-startup-of-an-instance.md)   
 [Connect to Another Computer &#40;SQL Server Configuration Manager&#41;](../../2014/database-engine/connect-to-another-computer-sql-server-configuration-manager.md)   
 [Configure WMI to Show Server Status in SQL Server Tools](../../2014/database-engine/configure-wmi-to-show-server-status-in-sql-server-tools.md)  
  
  