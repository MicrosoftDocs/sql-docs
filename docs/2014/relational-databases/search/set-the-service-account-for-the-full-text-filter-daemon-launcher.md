---
title: "Set the Service Account for the Full-text Filter Daemon Launcher | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text search [SQL Server], FDHOST Launcher (MSSQLFDLauncher) service account"
  - "FDHOST Launcher (MSSQLFDLauncher) [SQL Server]"
ms.assetid: 3ab1d101-7ae0-488f-9b57-468e2517b737
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Set the Service Account for the Full-text Filter Daemon Launcher
  This topic describes how to set the service account for the SQL Full-text Filter Daemon Launcher service (MSSQLFDLauncher) by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. The SQL Full-text Filter Daemon Launcher service is used by full-text search ssNoVersionto start the filter daemon host process, which handles full-text search filtering and word breaking. This service must be running to use full-text search.  
  
 The SQL Full-text Filter Daemon Launcher service is an instance-aware service that is associated with a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The SQL Full-text Filter Daemon Launcher service propagates the service account information to each filter daemon host process.  
  
  
##  <a name="setting"></a> Setting the Service Account  
  
#### To set the SQL Full-text Filter Daemon Launcher service account for full-text search  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  In **SQL Server Configuration Manager**, click **SQL Server Services**, right-click **SQL Full-text Filter Daemon Launcher (*`instance name`*)**, and then click **Properties**.  
  
3.  Click the **Log On** tab of the dialog box, and then select or enter the account under which to run each process created by the SQL Full-text Filter Daemon Launcher service.  
  
4.  After you close the dialog box, click **Restart** to restart the SQL Full-text Filter Daemon Launcher service.  
  
  
##  <a name="error"></a> If the SQL Full-text Filter Daemon Launcher Service Does Not Start  
 If the SQL Full-text Filter Daemon Launcher service does not start, one or more of the following might be the cause:  
  
-   The password associated with the SQL Full-text Filter Daemon Launcher service account has expired.  
  
     If you use a local user account for the SQL Full-text Filter Daemon Launcher service and your password expires, you need to:  
  
    1.  Set a new Windows password for the account.  
  
    2.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, update the SQL Full-text Filter Daemon Launcher service to use the new password.  
  
-   The user account or password of the service account is incorrect.  
  
     The SQL Full-text Filter Daemon Launcher service might attempt to log in with an incorrect user account and password. Follow the procedures above to verify that the user account for the service has not been changed.  
  
-   The account used to log in to the service does not have privileges.  
  
     You might be using an account that does not have login privileges on the computer where the server instance is installed. Verify that you are logging in with an account that has User rights and permissions on the local computer.  
  
-   Another instance of the same named pipe is already running.  
  
     The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service acts as a named pipe server for the SQL Full-text Filter Daemon Launcher service client. If the named pipe was already created by another process before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts, an error will be logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the Windows Event Log, and full-text search will not be available.  Determine what process or application is attempting to use the same named pipe and stop the application.  
  
-   The SQL Full-text Filter Daemon Launcher service is not configured correctly.  
  
     The service may not be configured correctly on the local computer.  
  
     If named pipes functionality has been disabled on the local computer, or if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has been configured to use a named pipe other than the default named pipe, the SQL Full-text Filter Daemon Launcher service might not start.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service group does not have permission to start SQL Full-text Filter Daemon Launcher service.  
  
     During the installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service group is granted default permission to manage, query, and start the SQL Full-text Filter Daemon Launcher service. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service group permissions to the SQL Full-text Filter Daemon Launcher service account have been removed after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation, the SQL Full-text Filter Daemon Launcher service will not start, and full-text search will be disabled. Make certain the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service group has permissions to the SQL Full-text Filter Daemon Launcher service account.  
  
  
## See Also  
 [Managing Services How-to Topics &#40;SQL Server Configuration Manager&#41;](../../database-engine/managing-services-how-to-topics-sql-server-configuration-manager.md)  
 [Upgrade Full-Text Search](upgrade-full-text-search.md)  
  
  
