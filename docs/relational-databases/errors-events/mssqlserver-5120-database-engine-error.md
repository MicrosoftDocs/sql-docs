---
description: "MSSQLSERVER_5120"
title: MSSQLSERVER_5120
ms.custom: ""
ms.date: 07/25/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "5120 (Database Engine error)"
ms.assetid: 
author: PijoCoder
ms.author: jopilov
---
# MSSQLSERVER_5120
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|5120|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DSK_FCB_FAILURE|  
|Message Text|Table error: Unable to open the physical file "%.*ls". Operating system error %d: "%ls".|  
  
## Explanation  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was unable to open a database file.  The operating system error provided in the message points to more specific underlying reasons for the failure. You may commonly see this error together with other errors like [17204](mssqlserver-17204-database-engine-error.md) or [17207](mssqlserver-17207-database-engine-error.md).
  
## User action  
  
  Diagnose and correct the operating system error, then retry the operation. There are multiple states that can help Microsoft narrow down the area in the product where the area is occurring. 
  
### Access is denied 
If you are getting the `Access is Denied` operating system error = 5, consider these methods:
   -  Check the permissions that are set of the file by looking at the properties of the file in Windows Explorer. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses Windows groups to provision Access Control on the various file resources. Make sure the appropriate group [with names like SQLServerMSSQLUser$ComputerName$MSSQLSERVER or SQLServerMSSQLUser$ComputerName$InstanceName] has the required permissions on the database file that is mentioned in the error message. Review [Configure File System Permissions for Database Engine Access](/previous-versions/sql/2014/database-engine/configure-windows/configure-file-system-permissions-for-database-engine-access?view=sql-server-2014&preserve-view=true) for more details. Ensure that the Windows group actually includes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account or the service SID.
   -  Review the user account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is currently running. You can use the Windows Task Manager to get this information. Look for the "User Name" value for the executable "sqlservr.exe". Also if you recently changed the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account, know that the supported way to do this operation is to use the [SQL Server Configuration Manager](../sql-server-configuration-manager.md) utility. 
   -  Depending on the type of operation (opening databases during server startup, attaching a database, database restore, and so on), the account that is used for impersonation and accessing the database file may vary. Review the topic [Securing Data and Log Files](/previous-versions/sql/sql-server-2008-r2/ms189128(v=sql.105)) to understand which operation sets what permission and to which accounts. Use a tool like Windows SysInternals [Process Monitor](/sysinternals/downloads/procmon) to understand if the file access is happening under the security context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance service startup account [or Service SID] or an impersonated account.

      If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is impersonating the user credentials of the login that executes the ALTER DATABASE or CREATE DATABASE operation, you will notice the following information in the Process Monitor tool (an example).
      
        ```
        Date & Time:      3/27/2010 8:26:08 PM
        Event Class:        File System
        Operation:          CreateFile
        Result:                ACCESS DENIED
        Path:                  C:\Program Files\Microsoft SQL Server\MSSQL13.SQL2016\MSSQL\DATA\attach_test.mdf
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
  
  
### Attaching files that reside on a network-attached storage  
If you cannot re-attach a database that resides on network-attached storage, a message like this may be logged in the Application log.

`Msg 5120, Level 16, State 101, Line 1 Unable to open the physical file "\\servername\sharename\filename.mdf". Operating system error 5: (Access is denied.).`

This problem occurs because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resets the file permissions when the database is detached. When you try to reattach the database, a failure occurs because of limited share permissions.

To resolve, follow these steps:
1. Use the -T startup option to start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use this startup option to turn on trace flag 1802 in [SQL Server Configuration Manager](../sql-server-configuration-manager.md) (see [Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-transact-sql.md) for information on 1802). For more information about how to change the startup parameters, see [Database Engine Service Startup Options](../../database-engine/configure-windows/database-engine-service-startup-options.md).

2. Use the following command to detach the database.
   ```sql
    exec sp_detach_db DatabaseName
    go 
   ```

3. Use the following command to reattach the database.
   ```sql
   exec sp_attach_db DatabaseName, '\\Network-attached storage_Path\DatabaseMDFFile.mdf', '\\Network-attached storage_Path\DatabaseLDFFile.ldf'
   go
   ```
