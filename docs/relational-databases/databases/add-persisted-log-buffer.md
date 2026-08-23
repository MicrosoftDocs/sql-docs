---
title: "Add persistent log buffer to a database"
description: "Explains how to add a persistent log buffer to a database in SQL Server 2016 and later. Provides Transact-SQL examples."
author: dplessMSFT
ms.author: dpless
ms.date: "11/22/2024"
ms.service: sql
ms.subservice: configuration
ms.custom: linux-related-content
ms.topic: how-to
helpviewer_keywords:
  - "PMEM"
  - "persistent memory"
  - "persistent log buffer"
  - "persisted log buffer"
  - "add log file"
  - "create log buffer"
  - "remove log buffer"
---

# Add persistent log buffer to a database

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to add and remove a persistent log buffer to a database in [!INCLUDE[sqlv16](../../includes/sssql16-md.md)] and above using [!INCLUDE[tsql](../../includes/tsql-md.md)].

## Permissions

Requires the `ALTER` permission on the database.

## Configure persistent memory device (Linux)

To configure a persistent memory device in [Linux](../../linux/sql-server-linux-configure-pmem.md).

## Configure persistent memory device (Windows)

To configure a persistent memory device in [Windows](../../database-engine/configure-windows/configure-persistent-memory.md).
  
## Add a persistent log buffer to a database

The volume or the mount point for the new log file must be formatted with DAX enabled (NTFS) or mounted with the DAX option (XFS/EXT4).

Use the following syntax to add a persistent log buffer to an existing database. The syntax differs depending on the version of [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)].

### Add persistent log buffer in [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] and later

```sql
ALTER DATABASE [DB] SET PERSISTENT_LOG_BUFFER = ON (DIRECTORY_NAME = 'path-to-directory-on-a-DAX-volume');
```

For example:

```sql
ALTER DATABASE WideWorldImporters SET PERSISTENT_LOG_BUFFER = ON (DIRECTORY_NAME = 'F:\SQLTLog');
```

The name of the persistent log file buffer is generated automatically. The size of the file is always 20 megabytes.

### Add persistent log buffer in [!INCLUDE[sqlv16](../../includes/sssql16-md.md)]

```sql
ALTER DATABASE [DB] ADD LOG FILE
(
NAME = [DAXlogLogicalName],
FILENAME = 'path-to-log-file-on-a-DAX-volume',
SIZE = 20 MB
);
```

For example:

```sql
ALTER DATABASE WideWorldImporters ADD LOG FILE
(
NAME = wwi_log2, 
FILENAME = 'F:\SQLTLog\wwi_log2.pldf',
SIZE = 20 MB
);
```

The log buffer file on the DAX volume will be sized at 20 megabytes regardless of the size specified with the `ALTER DATABASE ADD LOG FILE` command.

## Remove a persistent log buffer from a database

To safely remove a persistent log buffer, the database must be placed in single user mode in order to drain the persistent log buffer.

When you remove a persistent log buffer, the log buffer file on disk is deleted.

The syntax differs depending on the version of [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)].

### Remove persistent log buffer in [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] and later

```sql
ALTER DATABASE [DB] SET PERSISTENT_LOG_BUFFER = OFF;
```

For example:

```sql
ALTER DATABASE WideWorldImporters SET PERSISTENT_LOG_BUFFER = OFF;
```

### Remove persistent log buffer in [!INCLUDE[sqlv16](../../includes/sssql16-md.md)]

```sql
ALTER DATABASE [DB] SET SINGLE_USER;
ALTER DATABASE [DB] REMOVE FILE [DAXlogLogicalName];
ALTER DATABASE [DB] SET MULTI_USER;
```

For example:

```sql
ALTER DATABASE WideWorldImporters SET SINGLE_USER;
ALTER DATABASE WideWorldImporters REMOVE FILE wwi_log2;
ALTER DATABASE WideWorldImporters SET MULTI_USER;
```

## Limitations

[Transparent Data Encryption (TDE)](../security/encryption/transparent-data-encryption.md) is not compatible with persistent log buffer.

[Availability Groups](../../t-sql/statements/create-availability-group-transact-sql.md) can only use this feature on secondary replicas due to the requirement by the log reader agent for standard log writing semantics on the primary. However, a small log file must be created on all nodes (ideally on DAX volumes or mounts). In the event of a failover, the persistent log buffer path must exist, in order for the failover to be successful.

> [!CAUTION]
> If the persistent log buffer path or file isn't present during an Availability Group failover event, or database startup, the database enters a `RECOVERY PENDING` state until the issue is resolved.

## Interoperability with other PMEM features

When both persistent log buffer and [Hybrid Buffer Pool](../../database-engine/configure-windows/hybrid-buffer-pool.md) are enabled, along with the startup [trace flag 809](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md), Hybrid Buffer Pool will operate in what is known as _Direct Write_ mode.

## Back up and restore operations

Normal restore conditions apply. If persistent log buffer is restored to a DAX volume or mount, it continues to function. If the log is restored to a non-DAX disk volume, it can be safely removed using the `ALTER DATABASE REMOVE FILE` command.

## Related content

- [How It Works (It Just Runs Faster): Non-Volatile Memory SQL Server Tail Of Log Caching on NVDIMM](https://techcommunity.microsoft.com/t5/sql-server-blog/how-it-works-it-just-runs-faster-non-volatile-memory-sql-server/ba-p/3209699)
- [Transaction Commit latency acceleration using Storage Class Memory in Windows Server 2016/SQL Server 2016 SP1](https://techcommunity.microsoft.com/t5/sql-server-blog/transaction-commit-latency-acceleration-using-storage-class/ba-p/384995)
- [Hybrid buffer pool](../../database-engine/configure-windows/hybrid-buffer-pool.md)
