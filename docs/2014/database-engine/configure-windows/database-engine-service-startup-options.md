---
title: "Database Engine Service Startup Options | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "single-user mode [SQL Server], startup option"
  - "overriding default startup options"
  - "minimal configuration mode [SQL Server], startup option"
  - "default startup options"
  - "temporarily override default startup options [SQL Server]"
  - "startup options [SQL Server]"
  - "starting SQL Server, options"
ms.assetid: d373298b-f6cf-458a-849d-7083ecb54ef5
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Engine Service Startup Options
  Startup options designate certain file locations needed during startup, and specify some server wide conditions. Most users do not need to specify startup options unless you are troubleshooting the [!INCLUDE[ssDE](../../includes/ssde-md.md)] or you have an unusual problem and are directed to use a startup option by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Customer Support.  
  
> [!WARNING]  
>  Improper use of startup options can affect server performance and can prevent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from starting.  
  
## About Startup Options  
 When you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Setup writes a set of default startup options in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows registry. You can use these startup options to specify an alternate master database file, master database log file, or error log file. If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] cannot locate the necessary files, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not start.  
  
 Startup options can be set by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. For information, see [Configure Server Startup Options &#40;SQL Server Configuration Manager&#41;](scm-services-configure-server-startup-options.md).  
  
## List of Startup Options  
  
|Default startup options|Description|  
|-----------------------------|-----------------|  
|**-d**  *master_file_path*|Is the fully qualified path for the master database file (typically, C:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\Data\master.mdf). If you do not provide this option, the existing registry parameters are used.|  
|**-e**  *error_log_path*|Is the fully qualified path for the error log file (typically, C:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\LOG\ERRORLOG). If you do not provide this option, the existing registry parameters are used.|  
|**-l**  *master_log_path*|Is the fully qualified path for the master database log file (typically C:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\Data\mastlog.ldf). If you do not specify this option, the existing registry parameters are used.|  
  
|Other startup options|Description|  
|---------------------------|-----------------|  
|**-c**|Shortens startup time when starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the command prompt. Typically, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] starts as a service by calling the Service Control Manager. Because the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] does not start as a service when starting from the command prompt, use **-c** to skip this step.|  
|**-f**|Starts an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with minimal configuration. This is useful if the setting of a configuration value (for example, over-committing memory) has prevented the server from starting. Starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in minimal configuration mode places [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode. For more information, see the description for **-m** that follows.|  
|**-g**  *memory_to_reserve*|Specifies an integer number of megabytes (MB) of memory that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will leave available for memory allocations within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process, but outside the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory pool. The memory outside of the memory pool is the area used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for loading items, such as extended procedure .dll files, the OLE DB providers referenced by distributed queries, and automation objects referenced in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. The default is 256 MB.<br /><br /> Use of this option might help tune memory allocation, but only when physical memory exceeds the configured limit set by the operating system on virtual memory available to applications. Use of this option might be appropriate in large memory configurations in which the memory usage requirements of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are atypical and the virtual address space of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process is totally in use. Incorrect use of this option can lead to conditions under which an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may not start or may encounter run-time errors.<br /><br /> Use the default for the **-g** parameter unless you see any of the following warnings in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log:<br /><br /> -"Failed Virtual Allocate Bytes: FAIL_VIRTUAL_RESERVE \<size>"<br /><br /> -"Failed Virtual Allocate Bytes: FAIL_VIRTUAL_COMMIT \<size>"<br /><br /> These messages might indicate that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is trying to free parts of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory pool in order to find space for items, such as extended stored procedure .dll files or automation objects. In this case, consider increasing the amount of memory reserved by the **-g** switch.<br /><br /> Using a value lower than the default will increase the amount of memory available to the memory pool managed by the SQL Server Memory Manager and thread stacks; this may, in turn, provide some performance benefit to memory-intensive workloads in systems that do not use many extended stored procedures, distributed queries, or automation objects.|  
|**-m**|Starts an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode. When you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode, only a single user can connect, and the CHECKPOINT process is not started. CHECKPOINT guarantees that completed transactions are regularly written from the disk cache to the database device. (Typically, this option is used if you experience problems with system databases that should be repaired.) Enables the sp_configure allow updates option. By default, allow updates is disabled. Starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode enables any member of the computer's local Administrators group to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a member of the sysadmin fixed server role. For more information, see [Connect to SQL Server When System Administrators Are Locked Out](connect-to-sql-server-when-system-administrators-are-locked-out.md). For more information about single-user mode, see [Start SQL Server in Single-User Mode](start-sql-server-in-single-user-mode.md).|  
|**-m"Client Application Name"**|Limits the connections to a specified client application, when you use the **-m** option with **SQLCMD** or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For example, **-m"SQLCMD"** limits connections to a single connection and that connection must identify itself as the **SQLCMD** client program. Use this option when you are starting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode and an unknown client application is taking the only available connection. To connect through the Query Editor in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], use **-m"Microsoft SQL Server Management Studio - Query"**.<br /><br /> Client Application Name is case sensitive.<br /><br /> **\*\* Security Note \*\*** Do not use this option as a security feature. The client application provides the client application name, and can provide a false name as part of the connection string.|  
|**-n**|Does not use the Windows application log to record [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events. If you start an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with **-n**, we recommend that you also use the **-e** startup option. Otherwise, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events are not logged.|  
|**-s**|Allows you to start a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Without the **-s** parameter set, the default instance will try to start. You must switch to the appropriate BINN directory for the instance at a command prompt before starting **sqlservr.exe**. For example, if Instance1 were to use \mssql$Instance1 for its binaries, the user must be in the \mssql$Instance1\binn directory to start **sqlservr.exe -s instance1**.|  
|**-T**  *trace#*|Indicates that an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be started with a specified trace flag (*trace#*) in effect. Trace flags are used to start the server with nonstandard behavior. For more information, see [Trace Flags &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql).<br /><br /> **\*\* Important \*\*** When specifying a trace flag with the **-T** option, use an uppercase "T" to pass the trace flag number. A lowercase "t" is accepted by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but this sets other internal trace flags that are required only by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support engineers. (Parameters specified in the Control Panel startup window are not read.)|  
|**-x**|Disables the following monitoring features:<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performance monitor counters<br /><br /> Keeping CPU time and cache-hit ratio statistics<br /><br /> Collecting information for the DBCC SQLPERF command<br /><br /> Collecting information for some dynamic management views<br /><br /> Many extended-events event points<br /><br /> <br /><br /> **\*\* Warning \*\*** When you use the **-x** startup option, the information that is available for you to diagnose performance and functional problems with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is greatly reduced.|  
|**-E**|Increases the number of extents that are allocated for each file in a filegroup. This option may be helpful for data warehouse applications that have a limited number of users running index or data scans. It should not be used in other applications because it might adversely affect performance. This option is not supported in 32-bit releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
## Using Startup Options for Troubleshooting  
 Some startup options, such as single-user mode and minimal configuration mode, are principally used during troubleshooting. Starting the server for troubleshooting with the **-m** or **-f** options is easiest at the command line, while manually starting sqlservr.exe.  
  
> [!NOTE]  
>  When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started by using **net start**, startup options use a slash (/) instead of a hyphen (-).  
  
## Using Startup Options During Normal Operations  
 You may want to use some startup options every time you start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These options, such as **-g** or starting with a trace flag, are most easily done by configuring the startup parameters by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. These tool saves the startup options as registry keys, enabling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to always start with the startup options.  
  
## Compatibility Support  
 The **-h**  parameter is not supported in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. This parameter was used in earlier versions of 32-bit instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to reserve virtual memory address space for Hot Add memory metadata when AWE is enabled. For more information, see [Discontinued SQL Server Features in SQL Server 2014](../../getting-started/discontinued-sql-server-features-in-sql-server-2014.md).  
  
## Related Tasks  
 [Configure the scan for startup procs Server Configuration Option](configure-the-scan-for-startup-procs-server-configuration-option.md)  
  
 [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](start-stop-pause-resume-restart-sql-server-services.md)  
  
## See Also  
 [CHECKPOINT &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/checkpoint-transact-sql)   
 [sqlservr Application](../../tools/sqlservr-application.md)  
  
  
