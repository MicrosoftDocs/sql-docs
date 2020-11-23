---
title: "Connect to SQL Server when system administrators are locked out | Microsoft Docs"
description: Learn how to regain access to SQL Server as a system administrator if you've been mistakenly locked out. 
ms.custom: contperfq4
ms.date: 05/20/2020
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "sa account"
  - "connecting when locked out [SQL Server]"
  - "locked out [SQL Server]"
ms.assetid: c0c0082e-b867-480f-a54b-79f2a94ceb67
author: markingmyname
ms.author: maghan
---
# Connect to SQL Server when system administrators are locked out 
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
This article describes how you can regain access to the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] as a system administrator if you've been locked out.  A system administrator can lose access to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] due to one of the following reasons:  
  
-   All logins that are members of the sysadmin fixed server role have been removed by mistake.  
  
-   All Windows Groups that are members of the sysadmin fixed server role have been removed by mistake.  
  
-   The logins that are members of the sysadmin fixed server role are for individuals who have left the company or who are not available.  
  
-   The sa account is disabled or no one knows the password.  
  
## Resolution

In order to resolve your access issue, we recommend that you start the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode. This mode prevents other connections from occurring while you try to regain access. From here, you can connect to your instance of SQL Server and add your login to the **sysadmin** server role. Detailed steps for this solution are provided in the [step-by-step-instructions](#step-by-step-instructions) section.


You can start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode with either the `-m` or `-f` options from the command line. Any member of the computer's local Administrators group can then connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a member of the **sysadmin** fixed server role.  

When you start the instance in single-user mode, first stop the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service. Otherwise, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent might connect first, taking the only available connection to the server and blocking you from logging in.

It's also possible for an unknown client application to take the only available connection before you are able to log in. In order to prevent this from happening, you can use the `-m` option followed by an application name to limit connections to a single connection from the specified application. For example, starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with `-mSQLCMD` limits connections to a single connection that identifies itself as the **sqlcmd** client program. To connect through the Query Editor in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], use `-m"Microsoft SQL Server Management Studio - Query"`.  


> [!IMPORTANT]  
> Do not use `-m` with an application name as a security feature. Client applications specify the application name through the connection string settings, so it can easily be spoofed with a false name.

The following table summarizes the different ways to start your instance in single-user mode in the command line.

| Option | Description | When to use |
|:---|:---|:---|
|`-m` | Limits connections to a single connection | When there are no other users attempting to connect to the instance or you are not sure of the application name you are using to connect to the instance. |
|`-mSQLCMD`| Limits connections to a single connection that must identify itself as the **sqlcmd** client program| When you plan to connect to the instance with **sqlcmd** and you want to prevent other applications from taking the only available connection. |
|`-m"Microsoft SQL Server Management Studio - Query"`| Limits connections to a single connection that must identify itself as the **Microsoft SQL Server Management Studio - Query** application.| When you plan to connect to the instance through the Query Editor in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and you want to prevent other applications from taking the only available connection. |
|`-f`| Limits connections to a single connection and starts the instance in minimal configuration | When some other configuration is preventing you from starting. |
| &nbsp; | &nbsp; | &nbsp; |
  
For step-by-step instructions about how to start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, see [Start SQL Server in Single-User Mode](../../database-engine/configure-windows/start-sql-server-in-single-user-mode.md).

## Step by step instructions

The following step-by-step instructions describe how to grant system administrator permissions to a SQL Server login that mistakenly no longer has access.

These instructions assume,

* [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on Windows 8 or higher. Slight adjustments for earlier versions of SQL Server or Windows are provided where applicable.

* [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is installed on the computer.  

Perform these instructions while logged in to Windows as a member of the local administrators group.

1.  From the Windows Start menu, right-click the icon for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager and choose **Run as administrator** to pass your administrator credentials to Configuration Manager.  
  
2.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the left pane, select **SQL Server Services**. In the right-pane, find your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. (The default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes **(MSSQLSERVER)** after the computer name. Named instances appear in upper case with the same name that they have in Registered Servers.) Right-click the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then click **Properties**.  
  
3.  On the **Startup Parameters** tab, in the **Specify a startup parameter** box, type `-m` and then click **Add**. (That's a dash then lower case letter m.)  
  
    > [!NOTE]  
    >  For some earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] there is no **Startup Parameters** tab. In that case, on the **Advanced** tab, double-click **Startup Parameters**. The parameters open up in a very small window. Be careful not to change any of the existing parameters. At the very end, add a new parameter `;-m` and then click **OK**. (That's a semi-colon then a dash then lower case letter m.)  
  
4.  Click **OK**, and after the message to restart, right-click your server name, and then click **Restart**.  
  
5.  After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has restarted, your server will be in single-user mode. Make sure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is not running. If started, it will take your only connection.  
  
6.  From the Windows Start menu, right-click the icon for [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and select **Run as administrator**. This will pass your administrator credentials to SSMS.
  
    > [!NOTE]  
    >  For earlier versions of Windows, the **Run as administrator** option appears as a sub-menu.  
  
     In some configurations, SSMS will attempt to make several connections. Multiple connections will fail because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in single-user mode. Based on your scenario, perform one of the following actions.  
  
    1.  Connect with Object Explorer using Windows Authentication, which includes your Administrator credentials. Expand **Security**, expand **Logins**, and double-click your own login. On the **Server Roles** page, select **sysadmin**, and then click **OK**.  
  
    2.  Instead of connecting with Object Explorer, connect with a Query Window using Windows Authentication (which includes your Administrator credentials). (You can only connect this way if you did not connect with Object Explorer.) Execute code such as the following to add a new Windows Authentication login that is a member of the **sysadmin** fixed server role. The following example adds a domain user named `CONTOSO\PatK`.  
  
        ```  
        CREATE LOGIN [CONTOSO\PatK] FROM WINDOWS;  
        ALTER SERVER ROLE sysadmin ADD MEMBER [CONTOSO\PatK];  
        ```  
  
    3.  If your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running in mixed authentication mode, connect with a Query Window using Windows Authentication (which includes your Administrator credentials). Execute code such as the following to create a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login that is a member of the **sysadmin** fixed server role.  
  
        ```  
        CREATE LOGIN TempLogin WITH PASSWORD = '************';  
        ALTER SERVER ROLE sysadmin ADD MEMBER TempLogin;  
        ```  
  
        > [!WARNING]  
        >  Replace ************ with a strong password.  
  
    4.  If your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running in mixed authentication mode and you want to reset the password of the **sa** account, connect with a Query Window using Windows Authentication (which includes your Administrator credentials). Change the password of the **sa** account with the following syntax.  
  
        ```  
        ALTER LOGIN sa WITH PASSWORD = '************';  
        ```  
  
        > [!WARNING]  
        >  Replace ************ with a strong password.  

7. Close [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
8. These next few steps change SQL Server back to multi-user mode. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the left pane, select **SQL Server Services**.

9. In the right-pane, right-click the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then click **Properties**.  
  
10. On the **Startup Parameters** tab, in the **Existing parameters** box, select `-m` and then click **Remove**.  
  
    > [!NOTE]  
    >  For some earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] there is no **Startup Parameters** tab. In that case, on the **Advanced** tab, double-click **Startup Parameters**. The parameters open up in a very small window. Remove the `;-m` which you added earlier, and then click **OK**.  
  
11. Right-click your server name, and then click **Restart**. Make sure to start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent again if you stopped it before starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode.
  
Now you should be able to connect normally with one of the accounts which is now a member of the **sysadmin** fixed server role.  
  
## See Also  

* [Configure server startup options](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md)
* [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md)  
