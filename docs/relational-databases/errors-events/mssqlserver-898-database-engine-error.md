---
description: "MSSQLSERVER_898"
title: MSSQLSERVER_898
ms.custom: ""
ms.date: 02/15/2022
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, wiassaf
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "898 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_898
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|898|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|BPOOL_SCAN_LONG_DURATION|
|Message Text|`Buffer Pool scan took %I64d seconds: database ID %d, command '%ls', operation '%ls', scanned buffers %I64d, total iterated buffers %I64d, wait time %I64d ms. See 'https://go.microsoft.com/fwlink/?linkid=2132602' for more information.` |

## Explanation

Certain operations in Microsoft SQL Server trigger a scan of the buffer pool (the cache that stores database pages in memory). On systems that have a large amount of RAM (1 TB of memory or greater), scanning the buffer pool may take a long time. This slows down the operation that triggered the scan.

## User action

This is an informational message, no action is necessary.

This message can be suppressed by TF890. For more information, see [TF890](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf890).

## More information
 
For more information, see [Operations that trigger a buffer pool scan may run slowly on large-memory computers](/troubleshoot/sql/performance/buffer-pool-scan-runs-slowly-large-memory-machines).
