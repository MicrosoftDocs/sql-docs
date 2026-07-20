---
title: Write Pages in the Database Engine
description: Learn how the SQL Server Database Engine writes data and index pages to the buffer cache and physical storage subsystem.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/31/2025
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "pages"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Write pages in the Database Engine

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]


The I/O from an instance of the [!INCLUDE [ssDE](../includes/ssde-md.md)] includes logical and physical writes. A logical write occurs when data is modified in a page in the buffer cache. A physical write occurs when the page is written from the [buffer cache](memory-management-architecture-guide.md) to disk.

When a page is modified in the buffer cache, it isn't immediately written back to disk; instead, the page is marked as dirty. This means that a page can have more than one logical write made before it's physically written to disk. For each logical write, a transaction log record is inserted in the log cache that records the modification. The log records must be written to disk before the associated dirty page is removed from the buffer cache and written to disk.

SQL Server uses a technique known as write-ahead logging (WAL) that prevents writing a dirty page before the associated log record is written to disk. This is essential to the correct working of the recovery manager. For more information, see [Write-ahead transaction log](sql-server-transaction-log-architecture-and-management-guide.md#WAL).

## How SQL Server writes a modified data page

The following illustration shows the process for writing a modified data page.

:::image type="content" source="media/writing-pages/writing-pages.png" alt-text="Screenshot of Writing_Pages.":::

When the buffer manager writes a page, it searches for adjacent dirty pages that can be included in a single gather-write operation. Adjacent pages have consecutive page IDs and are from the same file; the pages don't have to be contiguous in memory. The search continues both forward and backward until one of the following events occurs:

- A clean page is found.
- 32 pages have been found.
- A dirty page is found whose log sequence number (LSN) hasn't yet been flushed in the log.
- A page is found that can't be immediately latched.

In this way, the entire set of pages can be written to disk with a single gather-write operation.

Just before a page is written, the form of page protection specified in the database is added to the page.

- If torn page protection is added, the page must be latched exclusively (EX) for the I/O. This is because the torn page protection modifies the page, making it unsuitable for any other thread to read.

- If checksum page protection is added, or the database uses no page protection, the page is latched with an update (UP) latch for the I/O. This latch prevents anyone else from modifying the page during the write, but still allows readers to use it.

For more information about disk I/O page protection options, see [Buffer management](memory-management-architecture-guide.md#buffer-management).

## How dirty pages are written to disk

A dirty page is written to disk in one of three ways:

- [Lazy writing](#lazy-write-process)
- [Eager writing](#eager-write-process)
- [Checkpoint](#checkpoint-process)

The lazy writing, eager writing, and checkpoint processes don't wait for the I/O operation to complete. They always use asynchronous (or overlapped) I/O and continue with other work, checking for I/O success later. This allows SQL Server to maximize both CPU and I/O resources for the appropriate tasks.

### Lazy write process

The lazy writer is a system process that keeps free buffers available by removing infrequently used pages from the buffer cache. Dirty pages are first written to disk.

### Eager write process

The eager write process writes dirty data pages associated with minimally logged operations such as bulk insert and select into. This process allows creating and writing new pages to take place in parallel. That is, the calling operation doesn't have to wait until the entire operation finishes before writing the pages to disk.

### Checkpoint process

The checkpoint process periodically scans the buffer cache for buffers with pages from a specified database and writes all dirty pages to disk. Checkpoints save time during a later recovery by creating a point at which all dirty pages are guaranteed to have been written to disk.

The user might request a checkpoint operation by using the `CHECKPOINT` command, or the [!INCLUDE [ssDE](../includes/ssde-md.md)] might generate automatic checkpoints based on the amount of log space used and time elapsed since the last checkpoint. In addition, a checkpoint is generated when certain activities occur. For example, when a data or log file is added or removed from a database, or when the instance of SQL Server is stopped.

For more information, see [Checkpoints and the active portion of the log](sql-server-transaction-log-architecture-and-management-guide.md#checkpoints-and-the-active-portion-of-the-log).

## Related content

- [Pages and extents architecture guide](pages-and-extents-architecture-guide.md)
- [Read data pages in the Database Engine](reading-pages.md)

