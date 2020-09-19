---
description: "Add persisted log buffer to a database"
title: "Add persisted log buffer to a database"
ms.custom: ""
ms.date: "10/30/2019"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: configuration
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
manager: amitban
---

# Add persisted log buffer to a database
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to add a persisted log buffer to a database in [!INCLUDE[sqlv15](../../includes/sssqlv15-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## Permissions

Requires ALTER permission on the database.  

## Configure persistent memory device (Linux)

To configure a persistent memory device in [Linux](../../linux/sql-server-linux-configure-pmem.md).

## Configure persistent memory device (Windows)

To configure a persistent memory device in [Windows](/windows-server/storage/storage-spaces/deploy-pmem/).
  
## Add a persisted log buffer to a database  

The following examples adds a persisted log buffer.

```sql
ALTER DATABASE <MyDB> 
  ADD LOG FILE 
  (
    NAME = <DAXlog>, 
    FILENAME = '<Filepath to DAX Log File>', 
    SIZE = 20MB
  );
```

The volume or mount the new log file is placed must be formatted with DAX (NTFS) or mounted with the DAX option (XFS/EXT4).

## Remove a persisted log buffer

To safely remove a persisted log buffer, the database must be placed in single user mode in order to drain the persisted log buffer.

The following example places removes a persisted log buffer.

```sql
ALTER DATABASE <MyDB> SET SINGLE_USER;
ALTER DATABASE <MyDB> REMOVE FILE <DAXlog>;
ALTER DATABASE <MyDB> SET MULTI_USER;
```

## Limitations

[Transparent Data Encryption (TDE)](../security/encryption/transparent-data-encryption.md) is not compatible with persisted log buffer.

[Availability Groups](../../t-sql/statements/create-availability-group-transact-sql.md) can only use this feature on secondary replicas due to need for normal log writing semantics on the primary. However, the small log file must be created on all nodes (ideally on DAX volumes or mounts).

## Backup and restore operations

Normal restore conditions apply. If persisted log buffer is restored to a DAX volume or mount, it will continue to function, otherwise it can be safely removed.
  
## Next steps

- [How It Works (It Just Runs Faster): Non-Volatile Memory SQL Server Tail Of Log Caching on NVDIMM](https://blogs.msdn.microsoft.com/bobsql/2016/11/08/how-it-works-it-just-runs-faster-non-volatile-memory-sql-server-tail-of-log-caching-on-nvdimm/)
- [Data exposed: Latency and Durability with SQL Server 2016](https://channel9.msdn.com/Shows/Data-Exposed/Latency-and-Durability-with-SQL-Server-2016)
- [Transaction Commit latency acceleration using Storage Class Memory in Windows Server 2016/SQL Server 2016 SP1](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/12/02/transaction-commit-latency-acceleration-using-storage-class-memory-in-windows-server-2016sql-server-2016-sp1/)
