---
title: Start, stop, pause, resume, and restart SQL Server services
description: Find out how to start, stop, pause, resume, or restart various SQL Server services. See how to use Transact-SQL, PowerShell, and other tools for these actions.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 02/24/2022
ms.service: sql
ms.subservice: configuration
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
---

# Start, stop, pause, resume, and restart SQL Server services

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-windows-only.md)]

This article describes how to start, stop, pause, resume, or restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)], the SQL Server Agent, or the SQL Server Browser service on Windows by using SQL Server Configuration Manager, SQL Server Management Studio (SSMS), **net** commands from a command prompt, Transact-SQL, or PowerShell.

For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux, see [Start, stop, and restart SQL Server services on Linux](../../linux/sql-server-linux-start-stop-restart-sql-server-services.md).

## Identify the service

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components are executable programs that run as Windows services. Windows services can run without displaying any activity on the computer screen and without user interaction on the command line.

### [!INCLUDE[ssDE](../../includes/ssde-md.md)] service

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] service can be the default instance (limit one per computer) or can be one of many named instances on the computer. Use [**SQL Server Configuration Manager**](../../relational-databases/sql-server-configuration-manager.md) to find out which instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are installed on the computer. The default instance (if you install it) is listed as **SQL Server (MSSQLSERVER)**. Named instances (if you install them) are listed as **SQL Server (<instance_name>)**. By default, [!INCLUDE[ssNoVersion](../../includes/ssexpress-md.md)] is installed as **SQL Server (SQLEXPRESS)**.

### SQL Server Agent service

The SQL Server Agent service executes scheduled administrative tasks, which are called jobs and alerts. For more information, see [SQL Server Agent](../../ssms/agent/sql-server-agent.md). SQL Server Agent is not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2019](../../sql-server/editions-and-components-of-sql-server-2019.md).

### SQL Server Browser service

The SQL Server Browser service listens for incoming requests for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources and provides clients information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer. A single instance of the SQL Server Browser service is used by all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on the computer.

### Additional Information

- If you pause the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service, users who are already connected can continue to work until their connections are broken, but new users can't connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Use **Pause** when you want to wait for users to complete their work before you stop the service, which lets them complete transactions that are in progress. **Resume** allows the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to accept new connections again. The SQL Server Agent service cannot be paused or resumed.  

- The SQL Server Configuration Manager and SSMS display the current status of services by using the following icons.  

||SQL Server Configuration Manager|SQL Server Management Studio (SSMS)|
|---:|:---|:---|
|**Started**|A green arrow on the icon next to the service name|A white arrow on a green circle icon next to the service name|
|**Stopped**|A red square on the icon next to the service name|A white square on a red circle icon next to the service name|
|**Paused**|Two vertical blue lines on the icon next to the service name|Two vertical white lines on a blue circle icon next to the service name|
|**Restarting**|A red square indicates that the service stopped, and then a green arrow indicates that the service started successfully|None|

- You won't have access to all possible options when using SQL Server Configuration Manager or SSMS, depending on the state of the service. For example, if the service is already started, **Start** is unavailable.

- When running on a cluster, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] service is best managed by using Cluster Administrator.  

### Permissions

By default, only members of the local administrator group can start, stop, pause, resume, or restart a service. To grant non-administrators the ability to manage services, see [How to grant users rights to manage services in Windows Server 2003](https://support.microsoft.com/kb/325349). (The process is similar on other versions of Windows Server.)

Stopping the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by using the Transact-SQL **SHUTDOWN** command requires membership in the **sysadmin** or **serveradmin** fixed server roles, and is not transferable.

## SQL Server Configuration Manager

### Start SQL Server Configuration Manager

On the **Start** menu, select **All Programs > Microsoft SQL Server > Configuration Tools > SQL Server Configuration Manager**.

The SQL Server Configuration Manager is a snap-in for the Microsoft Management Console program, and it may not appear as an application in some versions of Windows. See [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md) for more information.

### <a name="configmande"></a> Start, stop, pause, resume, or restart an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)]

1. Start SQL Server Configuration Manager, using the instructions above.

2. If the **User Account Control** dialog box appears, select **Yes**.

3. In SQL Server Configuration Manager, in the left pane, select **SQL Server Services**.

4. In the results pane, right-click **SQL Server (MSSQLServer)** or a named instance, and then select **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.

5. Select **OK** to close the SQL Server Configuration Manager.

> [!NOTE]
> To start an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] with startup options, see [Configure Server Startup Options &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md).  

### Start, stop, pause, resume, or restart the SQL Server Browser or an instance of the SQL Server Agent

1. Start SQL Server Configuration Manager, using the instructions above.

2. If the **User Account Control** dialog box appears, select **Yes**.

3. In SQL Server Configuration Manager, in the left pane, select **SQL Server Services**.

4. In the results pane, right-click **SQL Server Browser**, or **SQL Server Agent (MSSQLServer)** or **SQL Server Agent (<instance_name>)** for a named instance, and then select **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.  

5. Select **OK** to close the SQL Server Configuration Manager.  

> [!NOTE]
> SQL Server Agent cannot be paused.

## SQL Server Management Studio

### <a name="ssmsde"></a> Start, stop, pause, resume, or restart an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]

1. In Object Explorer, connect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], right-click the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] you want to start, and then select **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.

    Or, in Registered Servers, right-click the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] you want to start, point to **Service Control**, and then select **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.

2. If the **User Account Control** dialog box appears, select **Yes**.

3. When prompted if you want to act, select **Yes**.  

### Start, stop, or restart an instance of the SQL Server Agent

1. In Object Explorer, connect to the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], right-click **SQL Server Agent**, and then select **Start**, **Stop**, or **Restart**.

2. If the **User Account Control** dialog box appears, select **Yes**.

3. When prompted if you want to act, select **Yes**.

## <a name="CommandPrompt"></a> Command Prompt window using net commands

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services can be started, stopped, or paused by using Windows **net** commands.

### <a name="dbDefault"></a> Start the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]

- From a command prompt, enter one of the following commands:  
  
  ```cmd
  net start "SQL Server (MSSQLSERVER)"
  ```

   -or-  

  ```cmd
  net start MSSQLSERVER
  ```

### <a name="dbNamed"></a> Start a named instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]

- From a command prompt, enter one of the following commands. Replace *\<instancename>* with the name of the instance you want to manage.  
  
    ```cmd
    net start "SQL Server (instancename)"
    ```
   
   -or-  
  
   ```cmd
   net start MSSQL$instancename
   ```
  
### <a name="dbStartup"></a> Start the [!INCLUDE[ssDE](../../includes/ssde-md.md)] with startup options  

- Add startup options to the end of the **net start "SQL Server (MSSQLSERVER)"** statement, separated by a space. When started using **net start**, startup options use a slash (/) instead of a hyphen (-).  
  
  ```cmd
  net start "SQL Server (MSSQLSERVER)" /f /m
  ```
   -or-  
  
  ```cmd
  net start MSSQLSERVER /f /m
  ```
  
  > [!NOTE]
  >  For more information about startup options, see [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md).  
  
### <a name="agDefault"></a> Start the SQL Server Agent on the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]
  
- From a command prompt, enter one of the following commands:  
  
   ```cmd 
   net start "SQL Server Agent (MSSQLSERVER)"
   ```
  
   -or-  
  
  ```cmd
  net start SQLSERVERAGENT
  ```
  
### <a name="agNamed"></a> Start the SQL Server Agent on a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
- From a command prompt, enter one of the following commands. Replace *instancename* with the name of the instance you want to manage.  
  
  ```cmd
  net start "SQL Server Agent (instancename)"
  ```
  
   -or-  
  
  ```cmd
  net start SQLAgent$instancename
  ```
  
 For information about how to run SQL Server Agent in verbose mode for troubleshooting, see [sqlagent90 Application](../../tools/sqlagent90-application.md).  

### <a name="Browser"></a> Start the SQL Server Browser  

- From a command prompt, enter one of the following commands:  
  
  ```cmd
  net start "SQL Server Browser"
  ```
  
   -or-  
  
  ```cmd
  net start SQLBrowser
  ```
  
### <a name="pauseStop"></a> To pause or stop services from the Command Prompt window  

- To pause or stop services, modify the commands in the following ways.  

- To pause a service, replace **net start** with **net pause**.  

- To stop a service, replace **net start** with use **net stop**.  

## <a name="TsqlProcedure"></a> Transact-SQL

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] can be stopped by using the **SHUTDOWN** statement.  

### To stop the [!INCLUDE[ssDE](../../includes/ssde-md.md)] using Transact-SQL

- To wait for currently running Transact-SQL statements and stored procedures to finish, and then stop the [!INCLUDE[ssDE](../../includes/ssde-md.md)], execute the following statement.  
  
    ```sql
    SHUTDOWN;
    ```
  
- To stop the [!INCLUDE[ssDE](../../includes/ssde-md.md)] immediately, execute the following statement.  
  
    ```sql
    SHUTDOWN WITH NOWAIT;
    ```

For more information about the **SHUTDOWN** statement, see [SHUTDOWN &#40;Transact-SQL&#41;](../../t-sql/language-elements/shutdown-transact-sql.md).  

## <a name="PowerShellProcedure"></a> PowerShell

### Start and stop [!INCLUDE[ssDE](../../includes/ssde-md.md)] services

1. In a Command Prompt window, start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell by executing the following command.  

    ```cmd
    sqlps
    ```

2. At a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell command prompt, by executing the following command. Replace `computername` with the name of your computer.  

    ```powershell
    # Get a reference to the ManagedComputer class.
    CD SQLSERVER:\SQL\computername
    $Wmi = (get-item .).ManagedComputer
    ```

3. Identify the service that you want to stop or start. Pick one of the following lines. Replace `instancename` with the name of the named instance.

    - To get a reference to the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

        ```powershell
        $DfltInstance = $Wmi.Services['MSSQLSERVER']
        ```

    - To get a reference to a named instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

        ```powershell
        $DfltInstance = $Wmi.Services['MSSQL$instancename']
        ```

    - To get a reference to the SQL Server Agent service on the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  

        ```powershell
        $DfltInstance = $Wmi.Services['SQLSERVERAGENT']
        ```

    - To get a reference to the SQL Server Agent service on a named instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  

        ```powershell
        $DfltInstance = $Wmi.Services['SQLAGENT$instancename']
        ```

    - To get a reference to the SQL Server Browser service.

        ```powershell
        $DfltInstance = $Wmi.Services['SQLBROWSER']
        ```

4. Complete the example to start and then stop the selected service.
  
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
  
## <a name="ServiceController"></a> Use the ServiceController class

You can use the `ServiceController` class to programmatically control the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service, or any other Windows service. For an example using C#, see [ServiceController Class](/dotnet/api/system.serviceprocess.servicecontroller).

## Next steps

- [Overview of SQL Server Setup Documentation](../install-windows/install-sql-server.md)
- [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)
- [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)
- [Start SQL Server with Minimal Configuration](../../database-engine/configure-windows/start-sql-server-with-minimal-configuration.md)