---
title: "Ghost Cleanup Process Guide | Microsoft Docs"
ms.custom: ""
ms.date: "05/02/2018"
ms.prod: "sql"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "relational-databases-misc"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "ghost cleanup"
  - "ghost records"
  - "ghost clean up process" 
author: "MashaMSFT"
ms.author: "mathoma"
manager: "craigg"
---
# Ghost Cleanup Process Guide

The ghost cleanup process is a background process that deletes records off of pages that have been marked for deletion. The following topic provides an overview of the this process.

## Ghost records

When a record is deleted from from an index page on a table, the delete operation doesn't physically remove the record from the page - instead, it only marks it 'to be deleted' or *ghosted*. This is to optimize performance during a delete operation.  If records were physically deleted instead of being ghosted, then the object (such as the table or index) would be locked for the entirety of the delete operation. This would cause a performance issue by extending the duration of the delete transaction, and by blocking other transactions from accessing the object. Additionally, this helps speed up a delete rollback as the record just has to be unghosted, rather than physically reinserted. The *ghosted* record will then be physically removed asynchronously at a later time by the ghost cleanup task.


## Ghost record cleanup Task

Records that are marked for deletion, or *ghosted*, are cleaned up by the background ghost cleanup process. This background process runs sometime after the delete transaction is committed, and physically removes ghosted records from pages. The ghoest cleanup process runs automatically on an interval (every 5 seconds for SQL 2012+, every 10 seconds for SQL 2008/2008R2) and checks to see if any pages have been marked with ghost records. If it finds any, then it goes and deletes the records that are marked for deletion, or *ghosted*, touching a set number of pages with each execution.

When a record is ghosted, the database is marked as having ghosted entries, and the ghost cleanup process will only scan those databases. The ghost cleanup process will also mark the database as 'having no ghosted records' once all ghosted records have been deleted, and it will skip this database the next time it runs. The process will also skip any databases it is unable to take a shared lock on, and will try again the next time it runs.

The ghost cleanup processes runs on a single thread.

## Disabling the ghost cleanup

On high-load systems with many deletes, the ghost cleanup process can cause a performance issue from keeping pages in the buffer pool and generating IO. As such, it is possible to disable this process with the use of trace flag 661. More information about this can be found in [Tuning Options for SQL Server when running high performance workloads](https://support.microsoft.com/en-us/help/920093/tuning-options-for-sql-server-when-running-in-high-performance-workloa). However, there are performance implications of disabling the process.

If the ghost cleanup process is disabled, then ghosted records will no longer be removed from pages, and SQL will be unable to reuse this space until something is done to remove those records manually. This can cause your database to grow unnecessarily. Additionally, it can lead to page splits, which can cause a performance issue when generating execution plans and when performing index operations such as scans. For more information about page splits, see [Fill factor for an index](/indexes/specify-fill-factor-for-an-index.md.

Once the ghost cleanup process is disabled, some action needs to be taken to remove the ghosted records. One option is to execute an index rebuild, which will move data around on pages. Another option is to manually run [sp_clean_db_free_space](/system-stored-procedures/sp-clean-db-free-space-transact-sql.md), which will delete ghosted records.


## See also  
[Disabling the ghost clean up process](https://support.microsoft.com/en-us/help/920093/tuning-options-for-sql-server-when-running-in-high-performance-workloa)
[Manually removing ghosted records](/system-stored-procedures/sp-clean-db-free-space-transact-sql.md)
