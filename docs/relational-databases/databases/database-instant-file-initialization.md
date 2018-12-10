---
title: "Database Instant File Initialization | Microsoft Docs"
ms.custom: ""
ms.date: "01/09/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "initializing files [SQL Server]"
  - "instant file initialization [SQL Server]"
  - "fast file initialization [SQL Server]"
  - "file initialization [SQL Server]"
  - "IFI [SQL Server]"
  - "database instant file initialization [SQL Server]"
ms.assetid: 1ad468f5-4f75-480b-aac6-0b01b048bd67
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Database File Initialization
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
Data and log files are initialized to overwrite any existing data left on the disk from previously deleted files. Data and log files are first initialized by zeroing the files (filling with zeros) when you perform one of the following operations:  
  
- Create a database.  
- Add data or log files, to an existing database.  
- Increase the size of an existing file (including autogrow operations).  
- Restore a database or filegroup.  
  
File initialization causes these operations to take longer. However, when data is written to the files for the first time, the operating system does not have to fill the files with zeros.  
  
## Instant File Initialization (IFI)  
In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], data files can be initialized instantaneously to avoid zeroing operations. Instant file initialization allows for fast execution of the previously mentioned file operations. Instant file initialization reclaims used disk space without filling that space with zeros. Instead, disk content is overwritten as new data is written to the files. Log files cannot be initialized instantaneously.  
  
> [!NOTE]
> Instant file initialization is available only on [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[winxppro](../../includes/winxppro-md.md)] or [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)] or later versions.  
> 
> [!IMPORTANT]
> Instant file initialization is available only for data files. Log files will always be zeroed when being created, or growing in size.
  
Instant file initialization is only available if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account has been granted *SE_MANAGE_VOLUME_NAME*. Members of the Windows Administrator group have this right and can grant it to other users by adding them to the **Perform Volume Maintenance Tasks** security policy.  
  
> [!IMPORTANT]
> Some feature usage, such as [Transparent Data Encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md), can prevent Instant File Initialization.  
  
To grant an account the `Perform volume maintenance tasks` permission:  
  
1.  On the computer where the backup file will be created, open the **Local Security Policy** application (`secpol.msc`).  
  
2.  In the left pane, expand **Local Policies**, and then click **User Rights Assignment**.  
  
3.  In the right pane, double-click **Perform volume maintenance tasks**.  
  
4.  Click **Add User or Group** and add any user accounts that are used for backups.  
  
5.  Click **Apply**, and then close all **Local Security Policy** dialog boxes.  

> [!NOTE]
> Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], this permission can be granted to the service account at install time, during setup. If using the [command prompt install](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md), add the /SQLSVCINSTANTFILEINIT argument, or check the box *Grant Perform Volume Maintenance Task privilege to SQL Server Database Engine Service* in the [installation wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md).

> [!NOTE]
> Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP4, and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the column *instant_file_initialization_enabled* in the [sys.dm_server_services](../../relational-databases/system-dynamic-management-views/sys-dm-server-services-transact-sql.md) DMV can be used to identify if instant file initialization is enabled.

## Remarks
If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account is granted *SE_MANAGE_VOLUME_NAME*, an informational message that resembles the following is logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log at startup: 

```
Database Instant File Initialization: enabled. For security and performance considerations see the topic 'Database Instant File Initialization' in SQL Server Books Online. This is an informational message only. No user action is required.
```

If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account has **not** been granted *SE_MANAGE_VOLUME_NAME*, an informational message that resembles the following is logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log at startup: 

```
Database Instant File Initialization: disabled. For security and performance considerations see the topic 'Database Instant File Initialization' in SQL Server Books Online. This is an informational message only. No user action is required.
```

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP4, [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)])

## Security Considerations  
When using Instant File Initialization (IFI), because the deleted disk content is overwritten only as new data is written to the files, the deleted content might be accessed by an unauthorized principal, until some other data writes on that specific area of the data file. 
While the database file is attached to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this information disclosure risk is reduced by the discretionary access control list (DACL) on the file. This DACL allows file access only to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account and the local administrator. 
However, when the file is detached, it may be accessed by a user or service that does not have *SE_MANAGE_VOLUME_NAME*. A similar consideration exists when the database is backed up: if the backup file is not protected with an appropriate DACL, the deleted content can become available to an unauthorized user or service.  

Another consideration is that when a file is grown using IFI, a SQL Server administrator could potentially access the raw page contents and see the previously deleted content.

If the database files are hosted on a storage area network, it is also possible that the storage area network always presents new pages as pre-initialized, and having the operating system re-initialize the pages might be unnecessary overhead.
 
> [!NOTE]
> If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed in a secure physical environment, the performance benefits of enabling instant file initialization can outweigh the security risk and hence the reason for this recommendation.
  
If the potential for disclosing deleted content is a concern, you should take one or both of the following actions:  
  
- Always make sure that any detached data files and backup files have restrictive DACLs.  
- Disable instant file initialization for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by revoking *SE_MANAGE_VOLUME_NAME* from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service startup account. 

> [!IMPORTANT]
> Disabling instant file initialization will increase allocation times for data files.  
  
> [!NOTE]  
> Disabling instant file initialization only affects files that are created or increased in size after the user right is revoked.  
  
## See Also  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)  
  
  
