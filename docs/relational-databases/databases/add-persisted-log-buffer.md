---
description: "Explains how to add a persisted log buffer to a database in SQL Server 2019 and later. Provides Transact SQL examples."
title: "Add persisted log buffer to a database"
ms.custom: ""
ms.date: "10/30/2019"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "PMEM"
  - "persistent memory"
  - "persisted log buffer"
  - "add log file"
  - "create log buffer"
  - "remove log buffer"
ms.assetid: 8ead516a-1334-4f40-84b2-509d0a8ffa45
author: "briancarrig"
ms.author: "brcarrig"
ms.reviewer: mikeray
---

# Add persisted log buffer to a database
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to add a persisted log buffer to a database in [!INCLUDE[sqlv15](../../includes/sssql19-md.md)] and above using [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## Permissions

Requires ALTER permission on the database.  

## Configure persistent memory device (Linux)

To configure a persistent memory device in [Linux](../../linux/sql-server-linux-configure-pmem.md).

## Configure persistent memory device (Windows)

To configure a persistent memory device in [Windows](../../database-engine/configure-windows/configure-persistent-memory.md).
  
## Add a persisted log buffer to a database  

The following example adds a persisted log buffer.

```sql
ALTER DATABASE <MyDB> 
  ADD LOG FILE 
  (
    NAME = <DAXlog>, 
    FILENAME = '<Filepath to DAX Log File>', 
    SIZE = 20MB
  );
```

For example:

```sql
ALTER DATABASE WideWorldImporters 
  ADD LOG FILE 
  (
    NAME = wwi_log2, 
    FILENAME = 'F:/SQLTLog/wwi_log2.pldf', 
    SIZE = 20MB
  );
```

The log file on the DAX volume will be sized at 20 MB regardless of the size specified with the ADD FILE command.

The volume or mount the new log file is placed must be formatted with DAX enabled (NTFS) or mounted with the DAX option (XFS/EXT4).

## Remove a persisted log buffer

To safely remove a persisted log buffer, the database must be placed in single user mode in order to drain the persisted log buffer.

The following example removes a persisted log buffer.

```sql
ALTER DATABASE <MyDB> SET SINGLE_USER;
ALTER DATABASE <MyDB> REMOVE FILE <DAXlog>;
ALTER DATABASE <MyDB> SET MULTI_USER;
```
For example:

```sql
ALTER DATABASE WideWorldImporters SET SINGLE_USER;
ALTER DATABASE WideWorldImporters REMOVE FILE wwi_log2;
ALTER DATABASE WideWorldImporters SET MULTI_USER;
```

## Limitations

[Transparent Data Encryption (TDE)](../security/encryption/transparent-data-encryption.md) is not compatible with persisted log buffer.

[Availability Groups](../../t-sql/statements/create-availability-group-transact-sql.md) can only use this feature on secondary replicas due to the requirement by the log reader agent for standard log writing semantics on the primary. However, the small log file must be created on all nodes (ideally on DAX volumes or mounts). In the event of a failover, the persisted log buffer path must exist, in order for the failover to be successful.

In cases where the path or file isn't present during an Availability Group failover event, or database startup, the database enters a `RECOVERY PENDING` state until the issue is resolved.

## Interoperability with other PMEM features

When both Persisted log buffer and [Hybrid Buffer Pool](../../database-engine/configure-windows/hybrid-buffer-pool.md) are jointly enabled, along with start-up [trace flag 809](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md), Hybrid buffer pool will operate in what is known as _Direct Write_ mode.

## Back up and restore operations

Normal restore conditions apply. If persisted log buffer is restored to a DAX volume or mount, it will continue to function, otherwise it can be safely removed.
  
## Next steps

- [How It Works (It Just Runs Faster): Non-Volatile Memory SQL Server Tail Of Log Caching on NVDIMM](https://techcommunity.microsoft.com/t5/sql-server-blog/how-it-works-it-just-runs-faster-non-volatile-memory-sql-server/ba-p/3209699)
- [Transaction Commit latency acceleration using Storage Class Memory in Windows Server 2016/SQL Server 2016 SP1](https://techcommunity.microsoft.com/t5/sql-server-blog/transaction-commit-latency-acceleration-using-storage-class/ba-p/384995)
- [Hybrid Buffer Pool](../../database-engine/configure-windows/hybrid-buffer-pool.md)
