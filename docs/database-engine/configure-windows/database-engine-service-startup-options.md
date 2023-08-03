---
title: Database Engine Service startup options
description: Become familiar with SQL Server Database Engine startup options. View tips on how to use them, and learn about the purpose of each option.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/21/2023
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "single-user mode [SQL Server], startup option"
  - "overriding default startup options"
  - "minimal configuration mode [SQL Server], startup option"
  - "default startup options"
  - "temporarily override default startup options [SQL Server]"
  - "startup options [SQL Server]"
  - "starting SQL Server, options"
  - "single-user mode [SQL Server], startup parameter"
  - "overriding default startup parameters"
  - "minimal configuration mode [SQL Server], startup parameter"
  - "default startup parameters"
  - "temporarily override default startup parameters [SQL Server]"
  - "startup parameters [SQL Server]"
  - "starting SQL Server, parameters"
---
# Database Engine Service startup options

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Startup options designate certain file locations needed during startup, and specify some server wide conditions. Most users don't need to specify startup options unless you are troubleshooting the [!INCLUDE[ssDE](../../includes/ssde-md.md)] or you have an unusual problem and are directed to use a startup option by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Customer Support.

> [!WARNING]  
> Improper use of startup options can affect server performance and can prevent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from starting.  
>  
> Start SQL Server on Linux with the `mssql` user to prevent future startup issues. Example: `sudo -u mssql /opt/mssql/bin/sqlservr [STARTUP OPTIONS]`

## About startup options

When you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Setup writes a set of default startup options in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows registry. You can use these startup options to specify an alternate `master` database file, `master` database log file, or error log file. If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] can't locate the necessary files, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] won't start.

Startup options can be set by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. For information, see [Configure Server Startup Options (SQL Server Configuration Manager)](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md).

The following screenshot shows the Startup Parameters tab in the SQL Server Properties dialog, where you can modify startup parameters.

:::image type="content" source="media/database-engine-service-startup-options/trace-flag.png" alt-text="Screenshot of the SQL Server (MSSQLSERVER) Properties dialog, with the Startup Parameters tab selected.":::

## List of startup options

### Default startup options

| Options | Description |
| --- | --- |
| **-d** *master_file_path* | The fully qualified path for the `master` database file (typically, C:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\Data\master.mdf). If you don't provide this option, the existing registry parameters are used. |
| **-e** *error_log_path* | The fully qualified path for the error log file (typically, C:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\LOG\ERRORLOG). If you don't provide this option, the existing registry parameters are used. |
| **-l** *master_log_path* | The fully qualified path for the `master` database log file (typically C:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\Data\mastlog.ldf). If you don't specify this option, the existing registry parameters are used. |

### Other startup options

| Options | Description |
| --- | --- |
| **-c** | Shortens startup time when starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the command prompt. Typically, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] starts as a service by calling the Service Control Manager. Because the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] doesn't start as a service when starting from the command prompt, use **-c** to skip this step. |
| **-f** | Starts an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with minimal configuration. This is useful if the setting of a configuration value (for example, over-committing memory) has prevented the server from starting. Starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in minimal configuration mode places [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode. For more information, see the description for **-m** that follows. |
| **-kDecimalNumber** | This startup parameter limits the number of checkpoint I/O requests per second, where the **DecimalNumber** represents the checkpoint speed in MB per second. Changing this value can impact the speed of taking backups, or going through the recovery process so proceed with caution. That is, if you specify a very low value for the parameter, you may experience a longer recovery time and backups may take a slightly longer time to finish because a checkpoint process that a backup initiates is also delayed.<br /><br />Instead of using this parameter, you use the following methods to help eliminate I/O bottlenecks on your system:<br /><br />- Provide appropriate hardware to sustain I/O requests that are posted by SQL Server<br /><br />- Perform sufficient application tuning |
| **-m** | Starts an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode. When you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, only a single user can connect, and the CHECKPOINT process isn't started. CHECKPOINT guarantees that completed transactions are regularly written from the disk cache to the database device. (Typically, this option is used if you experience problems with system databases that should be repaired.) Enables the `sp_configure` allow updates option. By default, allow updates is disabled. Starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode enables any member of the computer's local Administrators group to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a member of the sysadmin fixed server role. For more information, see [Connect to SQL Server When System Administrators Are Locked Out](../../database-engine/configure-windows/connect-to-sql-server-when-system-administrators-are-locked-out.md). For more information about single-user mode, see [Start SQL Server in Single-User Mode](../../database-engine/configure-windows/start-sql-server-in-single-user-mode.md). |
| **-mClient Application Name** | Limits the connections to a specified client application. For example, `-mSQLCMD` limits connections to a single connection and that connection must identify itself as the SQLCMD client program. Use this option when you are starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode and an unknown client application is taking the only available connection. Use `"Microsoft SQL Server Management Studio - Query"` to connect with the SSMS Query Editor. The SSMS Query Editor option can't be configured by using [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] Configuration Manager because it includes the dash character, which is rejected by the tool.<br /><br />Client Application Name is case sensitive. Double quotes are required if the application name contains spaces or special characters.<br /><br />**Examples when starting from the command line:**<br /><br />`C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\sqlservr -s MSSQLSERVER -m"Microsoft SQL Server Management Studio - Query"`<br /><br />`C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\sqlservr -s MSSQLSERVER -mSQLCMD`<br /><br />**Security note:** Don't use this option as a security feature. The client application provides the client application name, and can provide a false name as part of the connection string. |
| **-n** | Doesn't use the Windows application log to record [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events. If you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with **-n**, we recommend that you also use the **-e** startup option. Otherwise, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events aren't logged. |
| **-s** | Allows you to start a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Without the **-s** parameter set, the default instance tries to start. You must switch to the appropriate BINN directory for the instance at a command prompt before starting **sqlservr.exe**. For example, if Instance1 were to use `\mssql$Instance1` for its binaries, the user must be in the `\mssql$Instance1\binn` directory to start **sqlservr.exe -s instance1**. |
| **-T** *trace#* | Indicates that an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be started with a specified trace flag (*trace#*) in effect. Trace flags are used to start the server with nonstandard behavior. For more information, see [Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).<br /><br />**Important:** When specifying a trace flag with the **-T** option, use an uppercase `T` to pass the trace flag number and no space between the `-T` option and the number of the trace flag. A lowercase `t` is accepted by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but this sets other internal trace flags that are required only by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support engineers. (Parameters specified in the Control Panel startup window aren't read.) |
| **-x** | Disables the following monitoring features:<br /><br />- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance monitor counters<br />- Keeping CPU time and cache-hit ratio statistics<br />- Collecting information for the DBCC SQLPERF command<br />- Collecting information for some dynamic management views<br />- Many extended-events event points<br /><br />**Warning:** When you use the **-x** startup option, the information that is available for you to diagnose performance and functional problems with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is greatly reduced. |
| **-E** | Increases the number of extents that are allocated for each file in a filegroup. This option may be helpful for data warehouse applications that have a limited number of users running index or data scans. It shouldn't be used in other applications because it might adversely affect performance. This option isn't supported in 32-bit releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. |

## Use startup options for troubleshooting

Some startup options, such as single-user mode and minimal configuration mode, are principally used during troubleshooting. Starting the server for troubleshooting with the `-m` or `-f` options is easiest at the command line, while manually starting sqlservr.exe.

> [!NOTE]  
> When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started by using `net start`, startup options use a slash (`/`) instead of a hyphen (`-`).

## Use startup options during normal operations

You may want to use some startup options every time you start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These options, such as starting with a trace flag, are most easily done by configuring the startup parameters by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. This tool saves the startup options as registry keys, enabling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to always start with the startup options.

## Compatibility support

For options that have been removed from previous releases, see [sqlservr Application](../../tools/sqlservr-application.md#compatibility-support).

## See also

- [CHECKPOINT (Transact-SQL)](../../t-sql/language-elements/checkpoint-transact-sql.md)
- [sqlservr Application](../../tools/sqlservr-application.md)

## Next steps

- [Configure the scan for startup procs Server Configuration Option](../../database-engine/configure-windows/configure-the-scan-for-startup-procs-server-configuration-option.md)
- [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)
- [Configure Server Startup Options (SQL Server Configuration Manager)](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md)
