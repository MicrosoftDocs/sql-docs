---
title: "sqlservr Application | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
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
ms.assetid: 60e8ef0a-0851-41cf-a6d8-cca1e04cbcdb
author: stevestein
ms.author: sstein
manager: craigg
---
# sqlservr Application
  The **sqlservr** application starts, stops, pauses, and continues an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] from a command prompt.  
  
## Syntax  
  
```  
  
      sqlservr [-sinstance_name] [-c] [-dmaster_path] [-f]   
     [-eerror_log_path] [-lmaster_log_path] [-m]  
     [-n] [-Ttrace#] [-v] [-x] [-gnumber]  
```  
  
## Arguments  
 **-s** _instance_name_  
 Specifies the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to connect to. If no named instance is specified, **sqlservr** starts the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  When starting an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you must use the **sqlservr** application in the appropriate directory for that instance. For the default instance, run **sqlservr** from the \MSSQL\Binn directory. For a named instance, run **sqlservr** from the \MSSQL$*instance_name*\Binn directory.  
  
 **-c**  
 Indicates that an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is started independently of the Windows Service Control Manager. This option is used when starting [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] from a command prompt, to shorten the amount of time it takes for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to start.  
  
> [!NOTE]  
>  When you use this option, you cannot stop [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Service Manager or the **net stop** command, and if you log off the computer, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is stopped.)  
  
 **-d** _master_path_  
 Indicates the fully qualified path for the **master** database file. There are no spaces between **-d** and *master_path*. If you do not provide this option, the existing registry parameters are used.  
  
 **-f**  
 Starts an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with minimal configuration. This is useful if the setting of a configuration value (for example, over-committing memory) has prevented the server from starting.  
  
 **-e** _error_log_path_  
 Indicates the fully qualified path for the error log file. If not specified, the default location is *\<Drive>*:\Program Files\Microsoft SQL Server\MSSQL\Log\Errorlog for the default instance and *\<Drive>*:\Program Files\Microsoft SQL Server\MSSQL$*instance_name*\Log\Errorlog for a named instance. There are no spaces between **-e** and *error_log_path*.  
  
 **-l** _master_log_path_  
 Indicates the fully qualified path for the **master** database transaction log file. There are no spaces between **-l** and *master_log_path*.  
  
 **-m**  
 Indicates to start an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in single-user mode. Only a single user can connect when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is started in single-user mode. The CHECKPOINT mechanism, which guarantees that completed transactions are regularly written from the disk cache to the database device, is not started. (Typically, this option is used if you experience problems with system databases that require repair.) Enables the **sp_configure allow updates** option. By default, **allow updates** is disabled.  
  
 **-n**  
 Allows you to start a named instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Without the **-s** parameter set, the default instance attempts to start. You must switch to the appropriate BINN directory for the instance at a command prompt before starting **sqlservr.exe**. For example, if Instance1 were to use \mssql$Instance1 for its binaries, the user must be in the \mssql$Instance1\binn directory to start **sqlservr.exe -s instance1**. If you start an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with the **-n** option, it is advisable to use the **-e** option too, or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] events are not logged.  
  
 **-T** _trace#_  
 Indicates that an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] should be started with a specified trace flag (*trace#*) in effect. Trace flags are used to start the server with nonstandard behavior. For more information, see [Trace Flags &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql).  
  
> [!IMPORTANT]  
>  When specifying a trace flag, use **-T** to pass the trace flag number. A lowercase t (**-t**) is accepted by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]; however, **-t** sets other internal trace flags required by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] support engineers.  
  
 **-v**  
 Displays the server version number.  
  
 **-x**  
 Disables the keeping of CPU time and cache-hit ratio statistics. Allows maximum performance.  
  
 **-g** _memory_to_reserve_  
 Specifies an integer number of megabytes (MB) of memory that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] leaves available for memory allocations within the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process, but outside the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory pool. The memory outside of the memory pool is the area used by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] for loading items such as extended procedure `.dll` files, the OLE DB providers referenced by distributed queries, and automation objects referenced in [!INCLUDE[tsql](../includes/tsql-md.md)] statements. The default is 256 MB.  
  
 Use of this option may help tune memory allocation, but only when physical memory exceeds the configured limit set by the operating system on virtual memory available to applications. Use of this option may be appropriate in large memory configurations in which the memory usage requirements of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] are atypical and the virtual address space of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process is totally in use. Incorrect use of this option can lead to conditions under which an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may not start or may encounter run-time errors.  
  
 Use the default for the **-g** parameter unless you see any of the following warnings in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] error log:  
  
-   "Failed Virtual Allocate Bytes: FAIL_VIRTUAL_RESERVE \<size>"  
  
-   "Failed Virtual Allocate Bytes: FAIL_VIRTUAL_COMMIT \<size>"  
  
 These messages may indicate that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is trying to free parts of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory pool in order to find space for items such as extended stored procedure .dll files or automation objects. In this case, consider increasing the amount of memory reserved by the **-g**``switch.  
  
 Using a value lower than the default increases the amount of memory available to the buffer pool and thread stacks; this may, in turn, provide some performance benefit to memory-intensive workloads in systems that do not use many extended stored procedures, distributed queries, or automation objects.  
  
## Remarks  
 In most cases, the sqlservr.exe program is only used for troubleshooting or major maintenance. When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is started from the command prompt with sqlservr.exe, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not start as a service, so you cannot stop [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using **net** commands. Users can connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools show the status of the service, so [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager correctly indicates that the service is stopped. [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] can connect to the server, but it also indicates that the service is stopped.  
  
## Compatibility Support  
 The **-h**  parameter is not supported in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. This parameter was used in earlier versions of 32-bit instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to reserve virtual memory address space for Hot Add memory metadata when AWE is enabled. For more information, see [Discontinued SQL Server Features in SQL Server 2014](../../2014/getting-started/discontinued-sql-server-features-in-sql-server-2014.md).  
  
## See Also  
 [Database Engine Service Startup Options](../database-engine/configure-windows/database-engine-service-startup-options.md)  
  
  
