---
title: DBCC SHRINKLOG (Parallel Data Warehouse)
description: "DBCC SHRINKLOG (Parallel Data Warehouse)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/16/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016"
---

# DBCC SHRINKLOG (Parallel Data Warehouse)

[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

Reduces the size of the transaction log *across the appliance* for the current [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database. The data is defragmented in order to shrink the transaction log. Over time, the database transaction log can become fragmented and inefficient. Use DBCC SHRINKLOG to reduce fragmentation and reduce the log size.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC SHRINKLOG   
    [ ( SIZE = { target_size [ MB | GB | TB ]  } | DEFAULT ) ]   
    [ WITH NO_INFOMSGS ]   
[;]  
```  

## Arguments

SIZE = { *target_size* [ MB \| **GB** \| TB ]  } \| **DEFAULT**.  
*target_size* is the desired size for the transaction log, across all the Compute nodes, after DBCC SHRINKLOG completes. It is an integer greater than 0.  
The log size is measured in megabytes (MB), gigabytes (GB), or terabytes (TB). It is the combined size of the transaction log on all of the Compute nodes.  
By default, DBCC SHRINKLOG reduces the transaction log to the log size stored in the metadata for the database. The log size in the metadata is determined by the LOG_SIZE parameter in [CREATE DATABASE &#40;Azure Synapse Analytics&#41;](../statements/create-database-transact-sql.md) or [ALTER DATABASE &#40;Azure Synapse Analytics&#41;](../statements/alter-database-transact-sql.md). DBCC SHRINKLOG reduces the transaction log size to the default size when `SIZE=DEFAULT` is specified, or when the `SIZE` clause is omitted.
  
WITH NO_INFOMSGS  
Informational messages are not displayed in the DBCC SHRINKLOG results.  
  
## Permissions

Requires ALTER SERVER STATE permission.

## General Remarks

DBCC SHRINKLOG does not change the log size stored in the metadata for the database. The metadata continues to contain the LOG_SIZE parameter that was specified in CREATE DATABASE or ALTER DATABASE statement.
  
## Examples

### A. Shrink the transaction log to the original size specified by CREATE DATABASE.  
Suppose the transaction log for the Addresses database was set to 100 MB when the Addresses database was created. That is, the CREATE DATABASE statement for Addresses had LOG_SIZE = 100 MB. Now, suppose the log has grown to 150 MB and you want to shrink it back to 100 MB.
  
Each of the following statements will attempt to shrink the transaction log for the Addresses database to the default size of 100 MB. If shrinking the log to 100 MB will cause data loss, DBCC SHRINKLOG will shrink the log to the smallest size possible, greater than 100 MB, without losing data.

```sql
USE Addresses;  
DBCC SHRINKLOG ( SIZE = 100 MB );  
DBCC SHRINKLOG ( SIZE = DEFAULT );  
DBCC SHRINKLOG;  
```