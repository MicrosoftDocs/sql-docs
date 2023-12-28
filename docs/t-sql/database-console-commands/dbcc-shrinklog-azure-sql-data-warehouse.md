---
title: DBCC SHRINKLOG - Analytics Platform System (PDW)
description: DBCC SHRINKLOG reduces the size of the transaction log across the appliance for the current Analytics Platform System (PDW) database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016"
---

# DBCC SHRINKLOG - Analytics Platform System (PDW)

[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

Reduces the size of the transaction log *across the appliance* for the current [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database. The data is defragmented in order to shrink the transaction log. Over time, the database transaction log can become fragmented and inefficient. Use `DBCC SHRINKLOG` to reduce fragmentation and reduce the log size.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC SHRINKLOG
    [ ( SIZE = { target_size [ MB | GB | TB ]  } | DEFAULT ) ]
    [ WITH NO_INFOMSGS ]
[;]
```

## Arguments

#### SIZE = { *target_size* [ MB | GB | TB ]  } | DEFAULT

*target_size* is the desired size for the transaction log, across all the Compute nodes, after `DBCC SHRINKLOG` completes. It is an integer greater than zero.  

The log size is measured in megabytes (MB), gigabytes (GB), or terabytes (TB). It is the combined size of the transaction log on all of the Compute nodes.  

By default, `DBCC SHRINKLOG` reduces the transaction log to the log size stored in the metadata for the database. The `LOG_SIZE` parameter in [CREATE DATABASE (Azure Synapse Analytics)](../statements/create-database-transact-sql.md) or [ALTER DATABASE (Azure Synapse Analytics)](../statements/alter-database-transact-sql.md) determines the log size in the metadata. `DBCC SHRINKLOG` reduces the transaction log size to the default size when `SIZE = DEFAULT` is specified, or when the `SIZE` clause is omitted.

#### WITH NO_INFOMSGS

Informational messages are not displayed in the `DBCC SHRINKLOG` results.

## Permissions

Requires ALTER SERVER STATE permission.

## Remarks

`DBCC SHRINKLOG` does not change the log size stored in the metadata for the database. The metadata continues to contain the `LOG_SIZE` parameter that was specified in `CREATE DATABASE` or `ALTER DATABASE` statement.

## Examples

### A. Shrink the transaction log to the original size specified by CREATE DATABASE

Suppose the transaction log for the `Addresses` database was set to 100 MB when the `Addresses` database was created. That is, the `CREATE DATABASE` statement for `Addresses` had `LOG_SIZE = 100 MB`. Now, suppose the log grows to 150 MB and you want to shrink it back to 100 MB.

Each of the following statements attempt to shrink the transaction log for the `Addresses` database to the default size of 100 MB. If shrinking the log to 100 MB will cause data loss, `DBCC SHRINKLOG` shrinks the log to the smallest size possible, greater than 100 MB, without losing data.

```sql
USE Addresses;
GO
DBCC SHRINKLOG ( SIZE = 100 MB );
GO
DBCC SHRINKLOG ( SIZE = DEFAULT );
GO
DBCC SHRINKLOG;
GO
```

## Related content

- [DBCC (Transact-SQL)](dbcc-transact-sql.md)
- [Parallel Data Warehouse components - Analytics Platform System](../../analytics-platform-system/parallel-data-warehouse-overview.md)
