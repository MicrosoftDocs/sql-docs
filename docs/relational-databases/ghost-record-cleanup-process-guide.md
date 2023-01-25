---
title: "Ghost cleanup process guide | Microsoft Docs"
description: Learn about the ghost cleanup process, a background process that deletes records off of pages that have been marked for deletion in SQL Server.
ms.custom: ""
ms.date: "05/02/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "ghost cleanup"
  - "ghost records"
  - "ghost clean up process" 
author: MashaMSFT
ms.author: mathoma
---
# Ghost cleanup process guide

The ghost cleanup process is a single-threaded background process that deletes records off of pages that have been marked for deletion. The following article provides an overview of this process.

## Ghost records

Records that are deleted from a leaf level of an index page aren't physically removed from the page - instead, the record is marked as 'to be deleted', or *ghosted*. This means that the row stays on the page but a bit is changed in the row header to indicate that the row is really a ghost. This is to optimize performance during a delete operation. Ghosts are necessary for row-level locking, but are also necessary for snapshot isolation where we need to maintain the older versions of rows.

## Ghost record cleanup task

Records that are marked for deletion, or *ghosted*, are cleaned up by the background ghost cleanup process. This background process runs sometime after the delete transaction is committed, and physically removes ghosted records from pages. The ghost cleanup process runs automatically on an interval (every 5 seconds for SQL Server 2012+, every 10 seconds for SQL Server 2008/2008R2) and checks to see if any pages have been marked with ghost records. If it finds any, then it goes and deletes the records that are marked for deletion, or *ghosted*, touching at most 10 pages with each execution.

When a record is ghosted, the database is marked as having ghosted entries, and the ghost cleanup process will only scan those databases. The ghost cleanup process will also mark the database as 'having no ghosted records' once all ghosted records have been deleted, and it will skip this database the next time it runs. The process will also skip any databases it is unable to take a shared lock on, and will try again the next time it runs.

The below query can identify how many ghosted records exist in a single database. 

 ```sql
 SELECT sum(ghost_record_count) total_ghost_records, db_name(database_id) 
 FROM sys.dm_db_index_physical_stats (NULL, NULL, NULL, NULL, 'SAMPLED')
 group by database_id
 order by total_ghost_records desc
```

## Disable the ghost cleanup

On high-load systems with many deletes, the ghost cleanup process can cause a performance issue from keeping pages in the buffer pool and generating IO. As such, it is possible to disable this process with the use of [trace flag 661](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md). However, there are performance implications from disabling the process.

Disabling the ghost cleanup process can cause your database to grow unnecessarily large and can lead to performance issues. Since the ghost cleanup process removes records that are marked as ghosts, disabling the process will leave these records on the page, preventing SQL Server from reusing this space. This forces SQL Server to add data to new pages instead, leading to bloated database files, and can also cause [page splits](indexes/specify-fill-factor-for-an-index.md). Page splits lead to performance issues when creating execution plans, and when doing scan operations. 

Once the ghost cleanup process is disabled, some action needs to be taken to remove the ghosted records. One option is to execute an index rebuild, which will move data around on pages. Another option is to manually run [sp_clean_db_free_space](system-stored-procedures/sp-clean-db-free-space-transact-sql.md) (to clean all database data files) or [sp_clean_db_file_free_space](system-stored-procedures/sp-clean-db-file-free-space-transact-sql.md) (to clean a single database datafile), which will delete ghosted records.

 >[!warning]
 > Disabling the ghost cleanup process is not generally recommended. Doing so should be tested thoroughly in a controlled environment before being implemented permanently in a production environment.


## Next steps  
[Disabling the ghost clean up process](https://support.microsoft.com/help/920093/tuning-options-for-sql-server-when-running-in-high-performance-workloa)
<br>[Remove ghost records from a single database file](system-stored-procedures/sp-clean-db-file-free-space-transact-sql.md)
<br>[Remove ghost records from all database data files](system-stored-procedures/sp-clean-db-free-space-transact-sql.md)


