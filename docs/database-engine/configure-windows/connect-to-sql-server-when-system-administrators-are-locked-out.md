---
title: "Connect to SQL Server when system administrators are locked out | Microsoft Docs"
description: Learn how to regain access to SQL Server as a system administrators if you've been mistakenly locked out. 
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
author: MikeRayMSFT
ms.author: mikeray
---
# Connect to SQL Server when system administrators are locked out 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
This topic describes how you can regain access to the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] as a system administrator if you've been locked out.  A system administrator can lose access to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] due to one of the following reasons:  
  
-   All logins that are members of the sysadmin fixed server role have been removed by mistake.  
  
-   All Windows Groups that are members of the sysadmin fixed server role have been removed by mistake.  
  
-   The logins that are members of the sysadmin fixed server role are for individuals who have left the company or who are not available.  
  
-   The sa account is disabled or no one knows the password.  
  
## Resolution

In order to resolve your access issue, we recommend that you start the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode. This mode prevents other connections from occurring, while you try to regain access. From here 

When you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, first stop the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service. Otherwise, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent might connect first and prevent you from connecting as a second user. 

You can start the instance in single-user mode with either the **-m** or **-f** options on the command line.

> [!IMPORTANT]  
>  Do not use any of the `-m options` as a security feature. The client application provides the client application name, and can provide a false name as part of the connection string.

The following table summarizes the different ways to start your instance in single-user mode in the command line.

Option | Description | When to use
---|---|---
`-m"sqlcmd"`| Limits connections to a single connection and that connection must identify itself as the **sqlcmd** client program| When you are starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode and an unknown client application is taking the only available connection.
`-m"Microsoft SQL Server Management Studio - Query"`| Limits connections to a single connection| To connect through the Query Editor in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]
`-f`| Limits connections to a single connection and starts the instance in minimal configuration | When some other configuration is preventing you from starting.

Any member of the computer's local Administrators group can then connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a member of the **sysadmin** fixed server role.  
  
For step-by-step instructions about how to start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, see [Start SQL Server in Single-User Mode](../../database-engine/configure-windows/start-sql-server-in-single-user-mode.md).

For additional detail on other start up options, see [Configure server startup options](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md).

## Step-by-step instructions

The following step by step instructions describe how to grant system administrator permissions to a SQL Server login that mistakenly no longer has access.

These instructions assume,

* [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] running on Windows 8 or higher. Slight adjustments for earlier versions of SQL Server or Windows are provided where applicable.

* [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is installed on the computer.  

Perform these instructions while logged in to Windows as a member of the local administrators group.

1.  From the Start page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. On the **View** menu, select **Registered Servers**. (If your server is not already registered, right-click **Local Server Groups**, point to **Tasks**, and then click **Register Local Servers**.)  
  
2.  In the Registered Servers area, right-click your server, and then click **SQL Server Configuration Manager**. This should ask for permission to run as administrator, and then open the Configuration Manager program.  
  
3.  Close [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
4.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the left pane, select **SQL Server Services**. In the right-pane, find your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. (The default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes **(MSSQLSERVER)** after the computer name. Named instances appear in upper case with the same name that they have in Registered Servers.) Right-click the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then click **Properties**.  
  
5.  On the **Startup Parameters** tab, in the **Specify a startup parameter** box, type `-m` and then click **Add**. (That's a dash then lower case letter m.)  
  
    > [!NOTE]  
    >  For some earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] there is no **Startup Parameters** tab. In that case, on the **Advanced** tab, double-click **Startup Parameters**. The parameters open up in a very small window. Be careful not to change any of the existing parameters. At the very end, add a new parameter `;-m` and then click **OK**. (That's a semi-colon then a dash then lower case letter m.)  
  
6.  Click **OK**, and after the message to restart, right-click your server name, and then click **Restart**.  
  
7.  After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has restarted your server will be in single-user mode. Make sure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is not running. If started, it will take your only connection.  
  
8.  On the Windows 8 start screen, right-click the icon for [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. At the bottom of the screen, select **Run as administrator**. This will pass your administrator credentials to SSMS.
  
    > [!NOTE]  
    >  For earlier versions of Windows, the **Run as administrator** option appears as a sub-menu.  
  
     In some configurations, SSMS will attempt to make several connections. Multiple connections will fail because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in single-user mode. Based on your scenario, perform one of the following actions.  
  
    1.  Connect with Object Explorer using Windows Authentication, which includes your Administrator credentials. Expand **Security**, expand **Logins**, and double-click your own login. On the **Server Roles** page, select **sysadmin**, and then click **OK**.  
  
    2.  Instead of connecting with Object Explorer, connect with a Query Window using Windows Authentication (which includes your Administrator credentials). (You can only connect this way if you did not connect with Object Explorer.) Execute code such as the following to add a new Windows Authentication login that is a member of the **sysadmin** fixed server role. The following example adds a domain user named `CONTOSO\PatK`.  
  
        ```  
        CREATE LOGIN [CONTOSO\PatK] FROM WINDOWS;  
        ALTER SERVER ROLE sysadmin ADD MEMBER [CONTOSO\PatK];  
        ```  
  
    3.  If your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running in mixed authentication mode, connect with a Query Window using Windows Authentication (which includes your Administrator credentials). Execute code such as the following to create a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login that is a member of the **sysadmin** fixed server role.  
  
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

1. Close SSMS.  
  
1. These next few steps change SQL Server back to multi-user mode. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the left pane, select **SQL Server Services**.

1. In the right-pane, right-click the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then click **Properties**.  
  
1. On the **Startup Parameters** tab, in the **Existing parameters** box, select `-m` and then click **Remove**.  
  
    > [!NOTE]  
    >  For some earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] there is no **Startup Parameters** tab. In that case, on the **Advanced** tab, double-click **Startup Parameters**. The parameters open up in a very small window. Remove the `;-m` which you added earlier, and then click **OK**.  
  
1. Right-click your server name, and then click **Restart**. Make sure to start SQL Server Agent again.
  
Now you should be able to connect normally with one of the accounts which is now a member of the **sysadmin** fixed server role.  
  
## See Also  

* [Configure server startup options](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md)
* [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md)  
