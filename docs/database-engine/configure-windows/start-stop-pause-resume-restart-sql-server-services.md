---
title: Start, stop, pause, resume, and restart SQL Server services
description: Find out how to start, stop, pause, resume, or restart various SQL Server services. See how to use Transact-SQL, PowerShell, and other tools for these actions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/22/2024
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

This article describes how to start, stop, pause, resume, or restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)], the SQL Server Agent, or the SQL Server Browser service on Windows by using SQL Server Configuration Manager, [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] (SSMS), **net** commands from a command prompt, Transact-SQL, or PowerShell.

For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux, see [Start, stop, and restart SQL Server services on Linux](../../linux/sql-server-linux-start-stop-restart-sql-server-services.md).

## Identify the service

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components are executable programs that run as Windows services. Windows services can run without displaying any activity on the computer screen and without user interaction on the command line.

| Service | Description |
| --- | --- |
| **Database Engine service** | The [!INCLUDE [ssDE](../../includes/ssde-md.md)] service can be the default instance (limit one per computer) or can be one of many named instances on the computer. Use [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md) to find out which instances of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] are installed on the computer. The default instance (if you install it) is listed as **SQL Server (MSSQLSERVER)**. Named instances (if you install them) are listed as **SQL Server (<instance_name>)**. By default, [!INCLUDE [ssNoVersion](../../includes/ssexpress-md.md)] is installed as **SQL Server (SQLEXPRESS)**. |
| **SQL Server Agent service** | The SQL Server Agent service executes scheduled administrative tasks, which are called jobs and alerts. For more information, see [SQL Server Agent](../../ssms/agent/sql-server-agent.md). SQL Server Agent isn't available in every edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md). |
| **SQL Server Browser service** | The SQL Server Browser service listens for incoming requests for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resources and provides clients information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer. A single instance of the SQL Server Browser service is used by all instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installed on the computer. |

If you pause the [!INCLUDE [ssDE](../../includes/ssde-md.md)] service, users who are already connected can continue to work until their connections are broken, but new users can't connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. Use **Pause** when you want to wait for users to complete their work before you stop the service, which lets them complete transactions that are in progress. **Resume** allows the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to accept new connections again. The SQL Server Agent service can't be paused or resumed.

The SQL Server Configuration Manager and SSMS display the current status of services by using the following icons.

| | SQL Server Configuration Manager | SQL Server Management Studio (SSMS) |
| ---: | :--- | :--- |
| **Started** | A green arrow on the icon next to the service name | A white arrow on a green circle icon next to the service name |
| **Stopped** | A red square on the icon next to the service name | A white square on a red circle icon next to the service name |
| **Paused** | Two vertical blue lines on the icon next to the service name | Two vertical white lines on a blue circle icon next to the service name |
| **Restarting** | A red square indicates that the service stopped, and then a green arrow indicates that the service started successfully | None |

You don't have access to all possible options when using SQL Server Configuration Manager or SSMS, depending on the state of the service. For example, if the service is already started, **Start** is unavailable.

When running on a cluster, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)] service is best managed by using Cluster Administrator.

## Permissions

By default, only members of the local administrator group can start, stop, pause, resume, or restart a service. To grant non-administrators the ability to manage services, see [How to grant users rights to manage services](/troubleshoot/windows-server/windows-security/grant-users-rights-manage-services). (The process is similar on other versions of Windows Server.)

Stopping the [!INCLUDE [ssDE](../../includes/ssde-md.md)] by using the Transact-SQL `SHUTDOWN` command requires membership in the **sysadmin** or **serveradmin** fixed server roles, and isn't transferable.

## SQL Server Configuration Manager

The SQL Server Configuration Manager is a snap-in for the Microsoft Management Console program, and it might not appear as an application in some versions of Windows. For more information, see [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

### Start SQL Server Configuration Manager

From the **Start** menu, select **All Programs > Microsoft SQL Server > Configuration Tools > SQL Server Configuration Manager**.

### <a id="configmande"></a> Start, stop, pause, resume, or restart an instance of the SQL Server Database Engine

1. Start SQL Server Configuration Manager, using the instructions in the previous section.

1. If the **User Account Control** dialog box appears, select **Yes**.

1. In SQL Server Configuration Manager, in the left pane, select **SQL Server Services**.

1. In the results pane, right-click **SQL Server (MSSQLServer)** or a named instance, and then select **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.

1. Select **OK** to close the SQL Server Configuration Manager.

> [!NOTE]  
> To start an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] with startup options, see [SCM Services - Configure server startup options](scm-services-configure-server-startup-options.md).

> [!IMPORTANT]  
> Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], when you set the **Start Mode** for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service to *Automatic* in Configuration Manager, the service will start in *Automatic (Delayed Start)* mode instead, even though the **Start Mode** shows as *Automatic*.

### Start, stop, pause, resume, or restart the SQL Server Browser or an instance of the SQL Server Agent

1. Start SQL Server Configuration Manager, using the instructions in the previous section.

1. If the **User Account Control** dialog box appears, select **Yes**.

1. In SQL Server Configuration Manager, in the left pane, select **SQL Server Services**.

1. In the results pane, right-click **SQL Server Browser**, or **SQL Server Agent (MSSQLServer)** or **SQL Server Agent (*<instance_name>*)** for a named instance, and then select **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.

1. Select **OK** to close the SQL Server Configuration Manager.

> [!NOTE]  
> SQL Server Agent can't be paused.

## SQL Server Management Studio

Use [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] to manage the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] services.

### <a id="ssmsde"></a> Start, stop, pause, resume, or restart an instance of the Database Engine

1. In Object Explorer, connect to the instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)], right-click the instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] you want to start, and then select **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.

    Or, in Registered Servers, right-click the instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] you want to start, point to **Service Control**, and then select **Start**, **Stop**, **Pause**, **Resume**, or **Restart**.

1. If the **User Account Control** dialog box appears, select **Yes**.

1. When prompted if you want to act, select **Yes**.

### Start, stop, or restart an instance of the SQL Server Agent

1. In Object Explorer, connect to the instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)], right-click **SQL Server Agent**, and then select **Start**, **Stop**, or **Restart**.

1. If the **User Account Control** dialog box appears, select **Yes**.

1. When prompted if you want to act, select **Yes**.

## <a id="CommandPrompt"></a> Command Prompt window using net commands

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services can be started, stopped, or paused by using Windows **net** commands.

### <a id="dbDefault"></a> Start the default instance of the Database Engine

- From a command prompt, enter one of the following commands:

  ```cmd
  net start "SQL Server (MSSQLSERVER)"
  ```

   -or-

  ```cmd
  net start MSSQLSERVER
  ```

### <a id="dbNamed"></a> Start a named instance of the Database Engine

- From a command prompt, enter one of the following commands. Replace *\<instancename>* with the name of the instance you want to manage.

    ```cmd
    net start "SQL Server (instancename)"
    ```

   -or-

   ```cmd
   net start MSSQL$instancename
   ```

### <a id="dbStartup"></a> Start the Database Engine with startup options

- Add startup options to the end of the `net start "SQL Server (MSSQLSERVER)"` statement, separated by a space. When started using `net start`, startup options use a slash (/) instead of a hyphen (-).

  ```cmd
  net start "SQL Server (MSSQLSERVER)" /f /m
  ```

   -or-

  ```cmd
  net start MSSQLSERVER /f /m
  ```

  > [!NOTE]  
  > For more information about startup options, see [Database Engine Service startup options](database-engine-service-startup-options.md).

### <a id="agDefault"></a> Start the SQL Server Agent on the default instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]

- From a command prompt, enter one of the following commands:

   ```cmd
   net start "SQL Server Agent (MSSQLSERVER)"
   ```

   -or-

  ```cmd
  net start SQLSERVERAGENT
  ```

### <a id="agNamed"></a> Start the SQL Server Agent on a named instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]

- From a command prompt, enter one of the following commands. Replace *instancename* with the name of the instance you want to manage.

  ```cmd
  net start "SQL Server Agent (instancename)"
  ```

   -or-

  ```cmd
  net start SQLAgent$instancename
  ```

For information about how to run SQL Server Agent in verbose mode for troubleshooting, see [sqlagent90 Application](../../tools/sqlagent90-application.md).

### <a id="Browser"></a> Start the SQL Server Browser

- From a command prompt, enter one of the following commands:

  ```cmd
  net start "SQL Server Browser"
  ```

   -or-

  ```cmd
  net start SQLBrowser
  ```

### <a id="pauseStop"></a> Pause or stop services from the command prompt window

To pause or stop services, modify the commands in the following ways.

- To pause a service, replace `net start` with `net pause`.

- To stop a service, replace `net start` with `net stop`.

## <a id="TsqlProcedure"></a> Transact-SQL

The [!INCLUDE [ssDE](../../includes/ssde-md.md)] can be stopped by using the `SHUTDOWN` statement.

### Stop the Database Engine using Transact-SQL

- To wait for currently running Transact-SQL statements and stored procedures to finish, and then stop the [!INCLUDE [ssDE](../../includes/ssde-md.md)], execute the following statement.

  ```sql
  SHUTDOWN;
  ```

- To stop the [!INCLUDE [ssDE](../../includes/ssde-md.md)] immediately, execute the following statement.

  ```sql
  SHUTDOWN WITH NOWAIT;
  ```

For more information about the `SHUTDOWN` statement, see [SHUTDOWN](../../t-sql/language-elements/shutdown-transact-sql.md).

## <a id="PowerShellProcedure"></a> PowerShell

You can manage the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] services using PowerShell.

### Start and stop Database Engine services

1. At a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell command prompt, by executing the following command. Replace `computername` with the name of your computer.

   ```powershell
   # Get a reference to the ManagedComputer class.
   CD SQLSERVER:\SQL\computername
   $Wmi = (get-item .).ManagedComputer
   ```

1. Identify the service that you want to stop or start. Pick one of the following lines. Replace `instancename` with the name of the named instance.

   - To get a reference to the default instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

     ```powershell
     $DfltInstance = $Wmi.Services['MSSQLSERVER']
     ```

   - To get a reference to a named instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

     ```powershell
     $DfltInstance = $Wmi.Services['MSSQL$instancename']
      ```

   - To get a reference to the SQL Server Agent service on the default instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

     ```powershell
     $DfltInstance = $Wmi.Services['SQLSERVERAGENT']
     ```

   - To get a reference to the SQL Server Agent service on a named instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

     ```powershell
     $DfltInstance = $Wmi.Services['SQLAGENT$instancename']
     ```

   - To get a reference to the SQL Server Browser service.

     ```powershell
     $DfltInstance = $Wmi.Services['SQLBROWSER']
     ```

1. Complete the example to start and then stop the selected service.

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

## Check and enable disabled instances

To determine whether a SQL Server service instance is disabled, follow these steps:

1. Identify the service that you're trying to check by using the information in the [Start, stop, pause, resume, and restart SQL Server services](start-stop-pause-resume-restart-sql-server-services.md) section.
1. In [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md), select **SQL Server Services** and then locate the service you're interested in.
1. If the value of the **Start Mode** column is set to **Other (Boot, System, Disabled or Unknown)**, it typically means the corresponding service is disabled. To enable the service, follow these steps:

   1. In the Name column, right-click on the corresponding service and then switch to **Service** tab in the ***\<Service name\>* Properties** window.

   1. Review the value in the **Start Mode** column and verify that it's set to **Disabled**.

   1. Change the value to either **Manual** or **Automatic** per your requirements. For more information, see [SCM Services - Configure server startup options](scm-services-configure-server-startup-options.md).

## <a id="ServiceController"></a> Use the ServiceController class

You can use the `ServiceController` class to programmatically control the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service, or any other Windows service. For an example using C#, see [ServiceController Class](/dotnet/api/system.serviceprocess.servicecontroller).

## Troubleshoot service startup issues

When you attempt to start [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services, they might not start if there's a configuration problem. You can review the service specific logs to identify the problem and resolve it. Detailed troubleshooting steps and resolution for specific issues are available in the following articles:

- [SQL Server startup errors on a standalone server](/troubleshoot/sql/database-engine/startup-shutdown/sql-server-startup-errors)
- [The SQL Server service and the SQL Server Agent Service fail to start on a standalone server](/troubleshoot/sql/database-engine/startup-shutdown/agent-service-fails-start-stand-alone-server)
- [SQL Server agent crashes when you try to start it](/troubleshoot/sql/database-engine/startup-shutdown/sql-server-agent-crashes-upon-start)

## Related content

- [SQL Server installation guide](../install-windows/install-sql-server.md)
- [View and Read SQL Server Setup Log Files](../install-windows/view-and-read-sql-server-setup-log-files.md)
- [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)
- [Start SQL Server with Minimal Configuration](start-sql-server-with-minimal-configuration.md)
