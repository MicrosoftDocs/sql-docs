---
title: Database instant file initialization
description: Learn about instant file initialization and how to enable it on your database. Instant file initialization (IFI) allows for faster file operations.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/12/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "initializing files [SQL Server]"
  - "instant file initialization [SQL Server]"
  - "fast file initialization [SQL Server]"
  - "file initialization [SQL Server]"
  - "IFI [SQL Server]"
  - "database instant file initialization [SQL Server]"
---
# Database instant file initialization

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

In this article, you learn about instant file initialization (IFI) and how to enable it to speed up growth for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] database files.

By default, data and log files are initialized to overwrite any existing data left on the disk from previously deleted files. Data and log files are first initialized by zeroing the files (filling with zeros) when you perform the following operations:

- Create a database.
- Add data or log files, to an existing database.
- Increase the size of an existing file (including autogrow operations).
- Restore a database or filegroup.

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], instant file initialization allows for faster execution of the previously mentioned file operations, since it reclaims used disk space without filling that space with zeros. Instead, disk content is overwritten as new data is written to the files.

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], instant file initialization is available for transaction log files only.

## Instant file initialization and the transaction log

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

Historically, transaction log files couldn't be initialized instantaneously. However, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] (all editions) and in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], transaction log autogrowth events up to 64 MB can benefit from instant file initialization. The default auto growth size increment for new databases is 64 MB. Transaction log file autogrowth events larger than 64 MB can't benefit from instant file initialization.

Instant file initialization is allowed for transaction log growth on databases that have transparent data encryption (TDE) enabled, due to the nature of how the transaction log file is expanded, and the fact that the transaction log is written into in a serial fashion.

- Instant file initialization is in use for General Purpose and Business Critical tiers of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] only to benefit the growth of transaction log files.
- Instant file initialization isn't configurable in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

## Enable instant file initialization

Instant file initialization of data files is only available if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service startup account is granted `SE_MANAGE_VOLUME_NAME`. Members of the Windows Administrator group have this right and can grant it to other users by adding them to the **Perform Volume Maintenance Tasks** security policy. The `SE_MANAGE_VOLUME_NAME` right isn't required for instant file initialization of growth events up to 64 MB in the transaction log, which was introduced with the release of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

> [!IMPORTANT]  
> Some feature usage, such as [Transparent data encryption (TDE)](../security/encryption/transparent-data-encryption.md), can prevent instant file initialization. Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], and on [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], IFI is allowed on the transaction log. For more information, see [Instant file initialization and the transaction log](#instant-file-initialization-and-the-transaction-log).

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, this permission can be granted to the service account at install time, during setup.

If you use the [command prompt install](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md), add the `/SQLSVCINSTANTFILEINIT` argument, or check the box *Grant Perform Volume Maintenance Task privilege to SQL Server Database Engine Service* in the [installation wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md).

To grant an account the `Perform volume maintenance tasks` permission:

1. On the computer where the data file will be created, open the **Local Security Policy** application (`secpol.msc`).

1. In the left pane, expand **Local Policies**, and then select **User Rights Assignment**.

1. In the right pane, double-click **Perform volume maintenance tasks**.

1. Select **Add User or Group** and add the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service account.

1. Select **Apply**, and then close all **Local Security Policy** dialog boxes.

1. Restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service.

1. Check the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log at startup.

   **Applies to:** [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] SP4, [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] SP2, and [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later).

   1. If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service startup account is granted `SE_MANAGE_VOLUME_NAME`, an informational message that resembles the following example is logged:

      `Database Instant File Initialization: enabled. For security and performance considerations see the topic 'Database Instant File Initialization' in SQL Server Books Online. This is an informational message only. No user action is required.`

   1. If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service startup account was **not** granted `SE_MANAGE_VOLUME_NAME`, an informational message that resembles the following example is logged:

      `Database Instant File Initialization: disabled. For security and performance considerations see the topic 'Database Instant File Initialization' in SQL Server Books Online. This is an informational message only. No user action is required.`

   > [!NOTE]  
   > In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], use the value of `instant_file_initialization_enabled` in the [sys.dm_server_services](../system-dynamic-management-views/sys-dm-server-services-transact-sql.md) dynamic management view to identify if instant file initialization is enabled for your instance.

## Security considerations

We recommend enabling instant file initialization as the benefits can outweigh the security risk.

When you use instant file initialization, the deleted disk content is overwritten only as new data is written to the files. For this reason, the deleted content is potentially accessible by an unauthorized principal, until some other data writes on that specific area of the data file.

While the database file is attached to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this information disclosure risk is reduced by the discretionary access control list (DACL) on the file. This DACL allows file access only to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account and the local administrator. However, when the file is detached, it's potentially accessible by a user or service that doesn't have `SE_MANAGE_VOLUME_NAME`.

Similar considerations exist when:

- *The database is backed up.* If the backup file isn't protected with an appropriate DACL, the deleted content can become available to an unauthorized user or service.

- *A file is grown using IFI*. A [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] administrator could potentially access the raw page contents and see the previously deleted content.

- *The database files are hosted on a storage area network*. It's also possible that the storage area network always presents new pages as preinitialized, and having the operating system reinitialize the pages might be unnecessary overhead.

If the potential for disclosing deleted content is a concern, you should take one or both of the following actions:

- Always make sure that any detached data files and backup files have restrictive DACLs.

- Disable instant file initialization for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To do so, revoke `SE_MANAGE_VOLUME_NAME` from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service startup account.

  > [!NOTE]  
  > Disabling will increase allocation times for data files, and only affects files that are created or increased in size after the user right is revoked.

### SE_MANAGE_VOLUME_NAME user right

The `SE_MANAGE_VOLUME_NAME` user privilege can be assigned in **Windows Administrative Tools**, **Local Security Policy** applet. Under **Local Policies** select **User Right Assignment** and modify the **Perform volume maintenance tasks** property.

## Performance considerations

The Database File initialization process writes zeros to the new regions of the file under initialization. The duration of this process depends on size of file portion that is initialized and on the response time and capacity of the storage system. If the initialization takes a long time, you might see the following messages recorded in the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] error log and the Application Log.

```output
Msg 5144
Autogrow of file '%.*ls' in database '%.*ls' was cancelled by user or timed out after %d milliseconds. Use ALTER DATABASE to set a smaller FILEGROWTH value for this file or to explicitly set a new file size.
```

```output
Msg 5145
Autogrow of file '%.*ls' in database '%.*ls' took %d milliseconds. Consider using ALTER DATABASE to set a smaller FILEGROWTH for this file.
```

A long autogrow of a database and/or transaction log file can cause query performance problems. An operation that requires the autogrowth of a file holds on to resources such as locks or latches during the duration of the file grow operation. You might see long waits on latches for allocation pages. The operation that requires the long autogrow shows a wait type of `PREEMPTIVE_OS_WRITEFILEGATHER`.

## Related content

- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Manage the size of the transaction log file](../logs/manage-the-size-of-the-transaction-log-file.md)
- [SQL Server Instant File Initialization: SetFileValidData (Windows) vs fallocate (Linux)](/archive/blogs/bobsql/sql-server-instant-file-initialization-setfilevaliddata-windows-vs-fallocate-linux)
