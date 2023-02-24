---
description: "MSSQLSERVER_17204"
title: "MSSQLSERVER_17204 | Microsoft Docs"
ms.custom: ""
ms.date: 07/25/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17204 (Database Engine error)"
ms.assetid: 40db66f9-dd5e-478c-891e-a06d363a2552
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_17204
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |
| :-------- | :---- |
|Product Name|SQL Server|  
|Event ID|17204|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBLKIO_DEVOPENFAILED|  
|Message Text|%ls: Could not open file %ls for file number %d.  OS error: %ls.|  
  
## Explanation  
SQL Server was unable to open the specified file because of the specified OS error.  

You may see error 17204 in the Windows Application Event or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Error log when SQL Server cannot open a database and/or transaction log files. Here is an example of what the error may look like:

``` 
Error: 17204, Severity: 16, State: 1.
FCB::Open failed: Could not open file c:\Program Files\Microsoft SQL Server\MSSQL10.SQL2008\MSSQL\data\MyDB_Prm.mdf for file number 1.  OS error: 5(Access is denied.).
```

You may see these errors during the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance startup process or any Database operation that attempts to start the database (for example, ALTER DATABASE). In some scenarios, you may see both 17204 and 17207 errors and in other occasions you might just see one of them.

If a user database runs into these errors, that database will be left in the RECOVERY_PENDING state and applications cannot access the database. If a system database encounters these errors, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance will not start and you cannot connect to this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A failure with a system database could also result in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster resource to go offline.

## Cause
Before any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database can be used, the database needs to be started. The database startup process involves: 
1. Initializing various data structures that represent the database and the database files
1. Opening all the files that belong to the database
1. Running recovery on the database 

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the [CreateFile](/windows/win32/api/fileapi/nf-fileapi-createfilea) Windows API function to open the files that belong to a database.
 
Messages 17204 (and 17207) indicate that an error was encountered while [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempted to open the database files during the startup process.
 
These error messages contain the following information:
1. Name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] function that is attempting to open the file. The function name that you normally observe in these error messages is one of the following:
   - FCB::Open - file has encountered an error when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempts to open it
   - FileMgr::StartPrimaryDataFiles - a primary data file or a file belonging to the primary file group
   - FileMgr::StartSecondaryDataFiles - a file belonging to a secondary file group
   - FileMgr::StartLogFiles - a transaction log file
   - STREAMFCB::Startup - SQL FileStream container
   - FCB::RemoveAlternateStreams
  
      
1. The state information distinguishes multiple locations within a function that can generate this error message
1. The full physical path for the file
1. The File ID corresponding to the file
1. The Operating System error code and error description. In some instances, you'll see only the error code.
 
The operating system error information printed in these error messages is the root cause leading to error 17204. Common causes for these error messages are a permission issue or an incorrect path to the file.


## User Action  
1. Resolving error 17204 involves understanding the associated operating system error code and diagnosing that error. Once the operating system error condition is resolved, then you can attempt to restart the database (using ALTER DATABASE SET ONLINE for example) or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to bring the affected database online. In some cases, you may not be able to resolve the operating system error. Then, you have to take specific corrective actions. We'll discuss these actions in this section.
1. If the 17204 error message contains only an error code and not an error description, then you can try resolving the error code using the command from an operating system shell: net helpmsg \<error code\> . If you are getting an 8-digit status code as the error code, then you can refer to the information sources like [How do I convert an HRESULT to a Win32 error code?](https://devblogs.microsoft.com/oldnewthing/20061103-07/?p=29133) to decode what these status codes into OS errors.
1. If you are getting the `Access is Denied` operating system error = 5, consider these methods:
   -  Check the permissions that are set of the file by looking at the properties of the file in Windows Explorer. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses Windows groups to provision Access Control on the various file resources. Make sure the appropriate group [with names like SQLServerMSSQLUser$ComputerName$MSSQLSERVER or SQLServerMSSQLUser$ComputerName$InstanceName] has the required permissions on the database file that is mentioned in the error message. Review [Configure File System Permissions for Database Engine Access](/previous-versions/sql/2014/database-engine/configure-windows/configure-file-system-permissions-for-database-engine-access) for more details. Ensure that the Windows group actually includes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account or the service SID.
   -  Review the user account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is currently running. You can use the Windows Task Manager to get this information. Look for the "User Name" value for the executable "sqlservr.exe". Also if you recently changed the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account, know that the supported way to do this operation is through the SQL Server Configuration Manager utility. More information on this is available at [SQL Server Configuration Manager](../sql-server-configuration-manager.md). 
   -  Depending on the type of operation - opening databases during server startup, attaching a database, database restore, etc. - the account that is used for impersonation and accessing the database file may vary. Review the topic [Securing Data and Log Files](/previous-versions/sql/sql-server-2008-r2/ms189128(v=sql.105)) to understand which operation sets what permission and to which accounts. Use a tool like Windows SysInternals [Process Monitor](/sysinternals/downloads/procmon) to understand if the file access is happening under the security context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance service startup account [or Service SID] or an impersonated account.

      If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is impersonating the credentials of the user that executes the ALTER DATABASE or CREATE DATABASE operation, you will notice the following information in the Process Monitor tool (an example):
        
        ```output
        Date & Time:      3/27/2010 8:26:08 PM
        Event Class:        File System
        Operation:          CreateFile
        Result:                ACCESS DENIED
        Path:                  C:\Program Files\Microsoft SQL Server\MSSQL10.SQL2008\MSSQL\DATA\attach_test.mdf
        TID:                   4288
        Duration:             0.0000366
        Desired Access:Generic Read/Write
        Disposition:        Open
        Options:            Synchronous IO Non-Alert, Non-Directory File, Open No Recall
        Attributes:          N
        ShareMode:       Read
        AllocationSize:   n/a
        Impersonating: DomainName\UserName
        ```
  
1. If you are getting `The system cannot find the file specified` OS error = 3:
   - Review the complete path from the error message
   - Ensure the disk drive and the folder path is visible and accessible from Windows Explorer
   - Review the Windows Event log to find out if any problems exist with this disk drive
   - If the path is incorrect and if this database already exists in the system, you can change the database file paths using the methods explained in the topic [Move Database Files](../databases/move-database-files.md). You may have to use this procedure, especially for system database files that encounter 17204 or 17207 and you are working through a disaster recovery scenario where the specified disk drives are unavailable. This topic also explains how you can identify the current location of the various system databases [master, model, tempdb, msdb and mssqlsystemresource].
   - If you see this error because the database files are missing, you have to restore the database from a valid backup.
     - If the database file associated with the error belongs to a secondary filegroup, then you can optionally mark that filegroup offline, bring the database online and then perform a restore of that filegroup alone. For more information, refer to the OFFLINE section of the topic [ALTER DATABASE File and Filegroup Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).
     - If the file that produced the error is a transaction log file, review the information under the sections "FOR ATTACH" and "FOR ATTACH_REBUILD_LOG" of the topic [CREATE DATABASE (Transact-SQL)](../../t-sql/statements/create-database-transact-sql.md) to understand how you can recreate the missing transaction log files.
   - Ensure that any disk or network location [like iSCSI drive] is available before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempts to access the database files on these locations. If needed create the required dependencies in Cluster Administrator or Service Control Manager.
1. If you're getting the `The process cannot access the file because it is being used by another process` operating system error = 32:
   - Use a tool like [Process Explorer](/sysinternals/downloads/process-explorer) or [Handle](/sysinternals/downloads/handle) from Windows Sysinternals to find out if another process or service has acquired exclusive lock on this database file
   - Stop that process from accessing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database files. Common examples include anti-virus programs (see guidance for file exclusions in the following [KB article](https://support.microsoft.com/help/309422/choosing-antivirus-software-for-computers-that-run-sql-server) )
   - In a cluster environment, make sure that the sqlservr.exe process from the previous owning node has actually released the handles to the database files. Normally, this doesn't occur, but misconfigurations of the cluster or I/O paths can lead to such issues.
