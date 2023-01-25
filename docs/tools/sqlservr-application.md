---
title: "sqlservr Application"
description: The sqlservr application starts, stops, pauses, and continues an instance of SQL Server from a command prompt.
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "command prompt utilities [SQL Server], sqlservr"
  - "command prompt [SQL Server], pausing/resuming instance of SQL Server"
  - "starting instance of SQL Server"
  - "command prompt [SQL Server], continuing instance of SQL Server"
  - "sqlservr utility"
  - "pausing instance of SQL Server"
  - "stopping instance of SQL Server"
  - "resuming SQL Server"
  - "command prompt [SQL Server], stopping instance of SQL Server"
  - "command prompt [SQL Server], starting instance of SQL Server"
  - "continuing instance of SQL Server"
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 10/07/2021
---

# sqlservr Application

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

The **sqlservr** application starts, stops, pauses, and continues an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] from a command prompt. Use this procedure to start [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] only for troubleshooting purposes. 

## Syntax

```cmd
sqlservr [-s instance_name] [-c] [-d master_path] [-f] 
     [-e error_log_path] [-l master_log_path] [-m]
     [-n] [-T trace#] [-v] [-x]
```

## Arguments

**-s** *instance_name*
Specifies the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to connect to. If no named instance is specified, **sqlservr** starts the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

> [!IMPORTANT]
>When starting an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you must use the **sqlservr** application in the appropriate directory for that instance. For the default instance, run **sqlservr** from the \MSSQL\Binn directory. For a named instance, run **sqlservr** from the \MSSQL$*instance_name*\Binn directory.

 **-c**
 Indicates that an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is started independently of the Windows Service Control Manager. This option is used when starting [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] from a command prompt, to shorten the amount of time it takes for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to start.

> [!NOTE]
>When you use this option, you cannot stop [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Service Manager or the **net stop** command, and if you log off the computer, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is stopped.)

**-d** *master_path*
Indicates the fully qualified path for the **master** database file. There are no spaces between **-d** and *master_path*. If you do not provide this option, the existing registry parameters are used.

**-f**
Starts an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with minimal configuration. This is useful if the setting of a configuration value (for example, over-committing memory) has prevented the server from starting.

**-e** *error_log_path*
Indicates the fully qualified path for the error log file. If not specified, the default location is `*\<Drive>*:\Program Files\Microsoft SQL Server\MSSQL\Log\Errorlog` for the default instance and `*\<Drive>*:\Program Files\Microsoft SQL Server\MSSQL$*instance_name*\Log\Errorlog` for a named instance. There are no spaces between **-e** and *error_log_path*.

**-l** *master_log_path*
Indicates the fully qualified path for the **master** database transaction log file. There are no spaces between **-l** and *master_log_path*.

**-m**
Indicates to start an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in single-user mode. Only a single user can connect when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is started in single-user mode. The CHECKPOINT mechanism, which guarantees that completed transactions are regularly written from the disk cache to the database device, is not started. (Typically, this option is used if you experience problems with system databases that require repair.) Enables the **sp_configure allow updates** option. By default, **allow updates** is disabled.

**-n**
Allows you to start a named instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Without the **-s** parameter set, the default instance attempts to start. You must switch to the appropriate BINN directory for the instance at a command prompt before starting **sqlservr.exe**. For example, if Instance1 were to use \mssql$Instance1 for its binaries, the user must be in the \mssql$Instance1\binn directory to start **sqlservr.exe -s instance1**. If you start an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with the **-n** option, it is advisable to use the **-e** option too, or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] events are not logged.

**-T** *trace#*
Indicates that an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] should be started with a specified trace flag (*trace#*) in effect. Trace flags are used to start the server with nonstandard behavior. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

>[!IMPORTANT]
>When specifying a trace flag, use **-T** to pass the trace flag number. A lowercase t (**-t**) is accepted by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]; however, **-t** sets other internal trace flags required by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] support engineers.

**-v**
Displays the server version number.

**-x**
Disables the keeping of CPU time and cache-hit ratio statistics. Allows maximum performance.

## Remarks
It is recommended to use the methods described in [Database Engine Service Startup Options](../database-engine/configure-windows/database-engine-service-startup-options.md) instead of using the sqlservr.exe program to start [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. In most cases, the sqlservr.exe program is only used for advanced troubleshooting or major maintenance. When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is started from the command prompt with sqlservr.exe, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not start as a service, so you cannot stop [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using **net** commands. Users can connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools show the status of the service, so [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager correctly indicates that the service is stopped. [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] can connect to the server, but it also indicates that the service is stopped.

## Compatibility Support
The following parameters are obsolete and not supported in [!INCLUDE[sssql19-md](../includes/sssql19-md.md)].

|Parameter | More information|
|:-----|:-----|
|**-h** | In earlier versions of 32-bit instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to reserve virtual memory address space for Hot Add memory metadata when AWE is enabled. Supported through [!INCLUDE[sssql14](../includes/sssql14-md.md)]. For more information, see [Discontinued SQL Server Features in SQL Server 2016](../database-engine/discontinued-database-engine-functionality-in-sql-server.md).|
|**-g** | *memory_to_reserve*<br/><br>Applies to earlier versions of 32-bit instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Supported through [!INCLUDE[sssql14](../includes/sssql14-md.md)]. Specifies an integer number of megabytes (MB) of memory that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] leaves available for memory allocations within the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process, but outside the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory pool. For more information, see [the SQL Server 2014 documentation on Server Memory Configuration Options](/previous-versions/sql/2014/database-engine/configure-windows/server-memory-server-configuration-options?view=sql-server-2014&preserve-view=true).|

## See Also
 [Database Engine Service Startup Options](../database-engine/configure-windows/database-engine-service-startup-options.md)
