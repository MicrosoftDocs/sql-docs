---
title: "Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Configuration Manager, start and stop services"
  - "stopping SQL Server Agent"
  - "parameters [SQL Server], startup options"
  - "SQL Server, startup options"
  - "Database Engine [SQL Server], starting and stopping services"
  - "pausing SQL Server"
  - "PowerShell [SQL Server], starting and stopping services"
  - "single-user mode [SQL Server], starting in"
  - "SQL Server Management Studio [SQL Server], starting or stopping services"
  - "stopping SQL Server Browser service"
  - "starting SQL Server Agent"
  - "default instances [SQL Server], starting and stopping"
  - "SQL Server Agent, starting and stopping"
  - "command prompt [SQL Server], starting and stopping SQL Server services"
  - "continuing SQL Server"
  - "starting SQL Server Database Engine"
  - "net stop commands [SQL Server]"
  - "command prompt [SQL Server], SQL Browser service"
  - "Configuration Manager, start and stop services"
  - "resuming SQL Server"
  - "startup options [SQL Server]"
  - "named instances [SQL Server], starting and stopping"
  - "net start commands [SQL Server]"
  - "SQL Server, starting and stopping"
  - "stopping SQL Server"
  - "starting SQL Server Browser service"
  - "SQL Server Database Engine setting startup options"
  - "administering SQL Server, starting and stopping services"
  - "Management Studio [SQL Server], starting or stopping services"
ms.assetid: 32660a02-e5a1-411a-9e57-7066ca459df6
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service
  This topic describes how to start, stop, pause, resume, or restart the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], **net** commands from a command prompt, [!INCLUDE[tsql](../../includes/tsql-md.md)], or PowerShell.  
  
-   **Before you begin:**  
  
    -   [What are these services?](#Services)  
  
    -   [Additional Information](#MoreInformation)  
  
    -   [Security](#Security)  
  
-   **Instructions using:**  
  
    -   [SQL Server Configuration Manager](#SSCMProcedure)  
  
    -   [SQL Server Management Studio](#SSMSProcedure)  
  
    -   [net Commands from a Command Prompt window](#CommandPrompt)  
  
    -   [Transact-SQL](#TsqlProcedure)  
  
    -   [PowerShell](#PowerShellProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Services"></a> What is the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] service, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service, and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service?  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components are executable programs that run as a Windows service. Programs that run as a Windows service can continue to operate without displaying any activity on the computer screen.  
  
 **[!INCLUDE[ssDE](../../includes/ssde-md.md)] service**  
 The executable process that is the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] can be the default instance (limit one per computer), or can be one of many named instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to determine which instances of [!INCLUDE[ssDE](../../includes/ssde-md.md)] are installed on the computer. The default instance (if you install it) is listed as **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (MSSQLSERVER)**. Named instances (if you install them) are listed as **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (<instance_name>)**. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Express is installed as **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SQLEXPRESS)**.  
  
 **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service**  
 A Windows service that executes scheduled administrative tasks, which are called jobs and alerts. For more information, see [SQL Server Agent](../../ssms/agent/sql-server-agent.md). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service**  
 A Windows service that listens for incoming requests for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources and provides clients information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer. A single instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is used for all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on the computer.  
  
###  <a name="MoreInformation"></a> Additional Information  
  
-   Pausing the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service prevents new users from connecting to the [!INCLUDE[ssDE](../../includes/ssde-md.md)], but users who are already connected can continue to work until their connections are broken. Use pause when you want to wait for users to complete work before you stop the service. This enables them to complete transactions that are in progress. Resume allows the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to accept new connections again. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service cannot be paused or resumed.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] display the current status of services by using the following icons.  
  
     **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager**  
  
    -   A green arrow on the icon next to the service name indicates that the service is started.  
  
    -   A red square on the icon next to the service name indicates that the service is stopped.  
  
    -   Two vertical blue lines on the icon next to the service name indicates that the service is paused.  
  
    -   When restarting the [!INCLUDE[ssDE](../../includes/ssde-md.md)], a red square will indicate that the service stopped, and then a green arrow will indicate that he service started successfully.  
  
     **[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]**  
  
    -   A white arrow on a green circle icon next to the service name indicates that the service is started.  
  
    -   A white square on a red circle icon next to the service name indicates that the service is stopped.  
  
    -   Two vertical white lines on a blue circle icon next to the service name indicates that the service is paused.  
  
-   When using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], only options that are possible will be available. For example, if the service is already started, **Start** will be unavailable.  
  
-   When running on a cluster, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] service is best managed by using Cluster Administrator.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 By default, only members of the local administrators group can start, stop, pause, resume or restart a service. To grant non-administrators the ability to manage services, see [How to grant users rights to manage services in Windows Server 2003](https://support.microsoft.com/kb/325349). (The process is similar on other versions of Windows.)  
  
 Stopping the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by using the [!INCLUDE[tsql](../../includes/tsql-md.md)]`SHUTDOWN` command requires membership in the **sysadmin** or **serveradmin** fixed server roles, and is not transferable.  
  
##  <a name="SSCMProcedure"></a> Using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager  
  
#### To start, stop, pause, resume, or restart the an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  If the **User Account Control** dialog box appears, click **Yes**.  
  
3.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the left pane, click **SQL Server Services**.  
  
4.  In the results pane, right-click **SQL Server (MSSQLServer)** or a named instance, and then click **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.  
  
5.  Click **OK** to close [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
> [!NOTE]  
>  To start an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] with startup options, see [Configure Server Startup Options &#40;SQL Server Configuration Manager&#41;](scm-services-configure-server-startup-options.md).  
  
#### To start, stop, pause, resume, or restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser or an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  If the **User Account Control** dialog box appears, click **Yes**.  
  
3.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the left pane, click **SQL Server Services**.  
  
4.  In the results pane, right-click **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser**, or **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent (MSSQLServer)** or **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent (<instance_name>)** for a named instance, and then click **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.  
  
5.  Click **OK** to close [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent cannot be paused.  
  
##  <a name="SSMSProcedure"></a> Using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio  
  
#### To start, stop, pause, resume, or restart the an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]  
  
1.  In Object Explorer, connect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], right-click the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] you want to start, and then click **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.  
  
     Or, in Registered Servers, right-click the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] you want to start, point to **Service Control**, and then click **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.  
  
2.  If the **User Account Control** dialog box appears, click **Yes**.  
  
3.  When prompted if you want to perform the action, click **Yes**.  
  
#### To start, stop, or restart the an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
1.  In Object Explorer, connect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], right-click **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent**, and then click **Start**, **Stop**, or **Restart**.  
  
2.  If the **User Account Control** dialog box appears, click **Yes**.  
  
3.  When prompted if you want to perform the action, click **Yes**.  
  
##  <a name="CommandPrompt"></a> From the Command Prompt Window using net Commands  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services can be started, stopped, or paused by using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows **net** commands.  
  
###  <a name="dbDefault"></a> To start the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
-   From a command prompt, enter one of the following commands:  
  
     **net start "SQL Server (MSSQLSERVER)"**  
  
     -or-  
  
     **net start MSSQLSERVER**  
  
###  <a name="dbNamed"></a> To start a named instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
-   From a command prompt, enter one of the following commands. Replace *\<instancename>* with the name of the instance you want to manage.  
  
     **net start "SQL Server (** *instancename* **)"**  
  
     -or-  
  
     **net start MSSQL$** *instancename*  
  
###  <a name="dbStartup"></a> To start the [!INCLUDE[ssDE](../../includes/ssde-md.md)] with startup options  
  
-   Add startup options to the end of the **net start "SQL Server (MSSQLSERVER)"** statement, separated by a space. When started using **net start**, startup options use a slash (/) instead of a hyphen (-).  
  
     **net start "SQL Server (MSSQLSERVER)" /f /m**  
  
     -or-  
  
     **net start MSSQLSERVER /f /m**  
  
    > [!NOTE]  
    >  For more information about startup options, see [Database Engine Service Startup Options](database-engine-service-startup-options.md).  
  
###  <a name="agDefault"></a> To start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent on the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
-   From a command prompt, enter one of the following commands:  
  
     **net start "SQL Server Agent (MSSQLSERVER)"**  
  
     -or-  
  
     **net start SQLSERVERAGENT**  
  
###  <a name="agNamed"></a> To start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent on a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
-   From a command prompt, enter one of the following commands. Replace *instancename* with the name of the instance you want to manage.  
  
     **net start "SQL Server Agent(** *instancename* **)"**  
  
     -or-  
  
     **net start SQLAgent$** *instancename*  
  
 For information about how to run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent in verbose mode for troubleshooting, see [sqlagent90 Application](../../tools/sqlagent90-application.md).  
  
###  <a name="Browser"></a> To start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser  
  
-   From a command prompt, enter one of the following commands:  
  
     **net start "SQL Server Browser"**  
  
     -or-  
  
     **net start SQLBrowser**  
  
###  <a name="pauseStop"></a> To pause or stop services from the Command Prompt window  
  
-   To pause or stop services modify the commands in the following ways.  
  
    -   To pause a service, replace **net start** with **net pause**.  
  
    -   To stop a service, replace **net start** with use **net stop**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] can be stopped by using the `SHUTDOWN` statement.  
  
#### To stop the [!INCLUDE[ssDE](../../includes/ssde-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)]  
  
-   To wait for currently running [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and stored procedures to finish, and then stop the [!INCLUDE[ssDE](../../includes/ssde-md.md)], execute the following statement.  
  
    ```tsql  
    SHUTDOWN;   
    ```  
  
-   To stop the [!INCLUDE[ssDE](../../includes/ssde-md.md)] immediately, execute the following statement.  
  
    ```tsql  
    SHUTDOWN WITH NOWAIT;   
    ```  
  
 For more information about the `SHUTDOWN` statement, see [SHUTDOWN &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/shutdown-transact-sql).  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
  
#### To start and stop [!INCLUDE[ssDE](../../includes/ssde-md.md)] services  
  
1.  In a Command Prompt window, start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell by executing the following command.  
  
    ```ms-dos  
    sqlps  
    ```  
  
2.  At a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell command prompt, by executing the following command. Replace `computername` with the name of your computer.  
  
    ```powershell  
    # Get a reference to the ManagedComputer class.  
    CD SQLSERVER:\SQL\computername  
    $Wmi = (get-item .).ManagedComputer  
  
    ```  
  
3.  Identify the service that you want to stop or start. Pick one of the following lines. Replace `instancename` with the name of the named instance.  
  
    -   To get a reference to the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
        ```powershell  
        $DfltInstance = $Wmi.Services['MSSQLSERVER']  
        ```  
  
    -   To get a reference to a named instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
        ```powershell  
        $DfltInstance = $Wmi.Services['MSSQL$instancename']  
        ```  
  
    -   To get a reference to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
        ```powershell  
        $DfltInstance = $Wmi.Services['SQLSERVERAGENT']  
        ```  
  
    -   To get a reference to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service on a named instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
        ```powershell  
        $DfltInstance = $Wmi.Services['SQLAGENT$instancename']  
        ```  
  
    -   To get a reference to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service.  
  
        ```powershell  
        $DfltInstance = $Wmi.Services['SQLBROWSER']  
        ```  
  
4.  Complete the example to start and then stop the selected service.  
  
    ```powershell  
    # Display the state of the service.  
    $DfltInstance  
    # Start the service.  
    $DfltInstance.Start();  
    # Wait until the service has time to start.  
    # Refresh the cache.  
    $DfltInstance.Refresh();   
    # Display the state of the service.  
    $DfltInstance  
    # Stop the service.  
    $DfltInstance.Stop();  
    # Wait until the service has time to stop.  
    # Refresh the cache.  
    $DfltInstance.Refresh();   
    # Display the state of the service.  
    $DfltInstance  
    ```  
  
## See Also  
 [Start SQL Server with Minimal Configuration](start-sql-server-with-minimal-configuration.md)   
 [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md)  
  
  
