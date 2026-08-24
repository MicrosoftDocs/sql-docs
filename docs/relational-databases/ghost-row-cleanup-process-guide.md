---
title: "Ghost Cleanup Process Guide"
description: Learn about the ghost cleanup process, a background process that physically removes rows that are marked for deletion from data pages.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: dfurman, randolphwest
ms.date: 12/08/2025
ms.service: sql
ms.subservice: supportability
ms.topic: concept-article
helpviewer_keywords:
  - "ghost cleanup"
  - "ghost rows"
  - "ghost clean up process"
---

# Ghost cleanup process guide

Ghost cleanup is a background process that physically removes the rows that were marked for deletion by DML statements. The following article provides an overview of this process.

## Ghost rows

Rows that are deleted from the leaf level pages of an index aren't physically removed from the page. Instead, the row is marked for future removal, or *ghosted*. This means that the row stays on the page but a bit is changed in the row header to indicate that the row is a ghost. This is to optimize performance during a delete operation. Ghosts are necessary for row-level locking and for snapshot isolation transactions where the database engine must maintain older row versions.

## Ghost cleanup task

Rows that are marked for deletion, or *ghosted*, are cleaned up by the background ghost cleanup process when they're no longer required. Ghost cleanup runs periodically and checks to see if any pages have ghosted rows. If it finds any, it physically removes these rows. There's a single ghost cleanup thread for all databases on a [!INCLUDE [ssde-md](../includes/ssde-md.md)] instance.

When a row is ghosted, the database is marked as having ghosted entries. The ghost cleanup process only scans such databases. The ghost cleanup process also marks the database as having no ghosted rows once all ghosted rows are removed, and skips this database the next time it runs. The process also skips any database if it can't acquire a shared lock on the database. It retries lock acquisition on the database the next time it runs.

The following query returns an approximate number of ghosted rows in a database.

```sql
SELECT SUM(ghost_record_count) AS total_ghost_records,
       DB_NAME(database_id) AS database_name
FROM sys.dm_db_index_physical_stats(NULL, NULL, NULL, NULL, 'SAMPLED')
GROUP BY database_id
ORDER BY total_ghost_records DESC;
```

## Disable ghost cleanup

In high-load systems with many deletes, the ghost cleanup process might reduce performance if it replaces many of the frequently accessed pages in the buffer pool with other pages that have ghosted rows. As a result, the frequently accessed pages must be re-read from disk, generating extra disk I/O and increasing query latency. If this occurs, you can disable ghost cleanup using [trace flag 661](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf661).

Without ghost cleanup, your database can grow unnecessarily large, which can also reduce performance due to extra I/O and memory consumption. Since the ghost cleanup process removes rows that are marked as ghosts, disabling the process leaves these rows on the page, preventing the database engine from reusing this space. This forces the database engine to add data to new pages instead, leading to bloated database files, and can also cause [page splits](indexes/specify-fill-factor-for-an-index.md). Page splits increase disk I/O, which can reduce query performance. If ghost cleanup is disabled, the database might run out of space.

> [!WARNING]  
> Disabling the ghost cleanup process permanently isn't recommended.

To remove ghost rows when ghost cleanup is disabled, rebuild indexes on tables where rows were deleted. Rebuilding an index creates new pages from existing data, omitting ghosted rows in the process.

## Related content

- [Page and extent architecture guide](pages-and-extents-architecture-guide.md)
