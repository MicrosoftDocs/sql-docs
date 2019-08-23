---
title: "Change the Service Startup Account for SQL Server (SQL Server Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "01/07/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server services, startup account changes"
  - "startup accounts [SQL Server]"
  - "changing startup accounts for services"
ms.assetid: d721c796-0397-46a7-901b-1a9a3c3fb385
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Change the Service Startup Account for SQL Server (SQL Server Configuration Manager)
  This topic describes how to Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to change the start up options of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services and to change the service accounts that are used by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or PowerShell. For more information about how to select an appropriate service account, see [Configure Windows Service Accounts and Permissions](configure-windows-service-accounts-and-permissions.md).  
  
> [!IMPORTANT]  
>  When you change the service startup account for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service (the [!INCLUDE[ssDE](../../includes/ssde-md.md)]) must be restarted for the change to take effect. When the service is restarted, all databases associated with that instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be unavailable until the service successfully restarts. If you have to change the service startup account of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, make sure that you do so during regularly scheduled maintenance or when the databases can be taken offline without interrupting daily operations.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Clustered servers  
  
     Changing the service account that is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be performed from the active node of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cluster.  
  
     When running on Windows Server 2008 (in a non-default configuration using Domain groups), changing the service account that is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent requires [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to stop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by taking the resource groups offline.  
  
-   SKU Upgrade ([!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] to non-Express SKU)  
  
     During [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] installation, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is configured to use the Network Service account but disabled. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager can change the account assigned for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service but the service cannot be enabled or started. After SKU upgrade from [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] to non-Express, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is not automatically enabled, but can be enabled when needed by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager and changing the service start mode to Manual or Automatic.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To change the SQL Server service startup account  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
    > [!NOTE]  
    >  Because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager is a snap-in for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console program and not a stand-alone program, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager does not appear as an application in newer versions of Windows.  
    >   
    >  -   **Windows 10**:  
    >          To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, on the **Start Page**, type SQLServerManager12.msc (for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]). For previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replace 12 with a smaller number. Clicking SQLServerManager12.msc opens the Configuration Manager. To pin the Configuration Manager to the Start Page or Task Bar, right-click SQLServerManager12.msc, and then click **Open file location**. In the Windows File Explorer, right-click SQLServerManager12.msc, and then click **Pin to Start** or **Pin to taskbar**.  
    > -   **Windows 8**:  
    >          To open [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the **Search** charm, under **Apps**, type **SQLServerManager\<version>.msc** such as `SQLServerManager12.msc`, and then press **Enter**.  
  
2.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, click **SQL Server Services**.  
  
3.  In the details pane, right-click the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance for which you want to change the service startup account, and then click **Properties**.  
  
4.  In the **SQL Server \<***instancename***> Properties** dialog box, click the **Log On** tab, and select a **Log on as** account type.  
  
5.  After selecting the new service startup account, click **OK**.  
  
     A message box asks whether you want to restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
6.  Click **Yes**, and then close [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
## See Also  
 [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](start-stop-pause-resume-restart-sql-server-services.md)   
 [Configure WMI to Show Server Status in SQL Server Tools](../../ssms/configure-wmi-to-show-server-status-in-sql-server-tools.md)  
  
  
