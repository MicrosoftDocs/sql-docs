---
title: Back up & Restore Full-Text Catalogs and Indexes
description: Learn how to back up and restore full-text indexes created in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "full-text indexes [SQL Server], backing up"
  - "full-text search [SQL Server], back up and restore"
  - "recovery [full-text search]"
  - "backups [SQL Server], full-text indexes"
  - "full-text indexes [SQL Server], restoring"
  - "restore operations [full-text search]"
---
# Back up and restore full-text catalogs and indexes

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article explains how to back up and restore full-text indexes created in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)]. The full-text catalog is a logical concept and doesn't reside in a filegroup. Therefore, to back up a full-text catalog, you must identify every filegroup that contains a full-text index that belongs to the catalog. Then you must back up those filegroups, one by one.

> [!IMPORTANT]  
> You can import full-text catalogs when upgrading a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database. Each imported full-text catalog is a database file in its own filegroup. To back up an imported catalog, back up its filegroup, in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] Books Online.

<a id="backingup"></a>

## Back up the full-text indexes of a full-text catalog

<a id="Find_FTIs_of_a_Catalog"></a>

### Find the full-text indexes of a full-text catalog

To retrieve the properties of the full-text indexes, use the following [SELECT](../../t-sql/queries/select-transact-sql.md) statement, which selects columns from the [sys.fulltext_indexes](../system-catalog-views/sys-fulltext-indexes-transact-sql.md) and [sys.fulltext_catalogs](../system-catalog-views/sys-fulltext-catalogs-transact-sql.md) catalog views.

```sql
USE AdventureWorks2025;
GO

DECLARE @TableID AS INT;

SET @TableID = (SELECT OBJECT_ID('AdventureWorks2025.Production.Product'));

SELECT object_name(@TableID),
       i.is_enabled,
       i.change_tracking_state,
       i.has_crawl_completed,
       i.crawl_type,
       c.name AS fulltext_catalog_name
FROM sys.fulltext_indexes AS i, sys.fulltext_catalogs AS c
WHERE i.fulltext_catalog_id = c.fulltext_catalog_id;
GO
```

<a id="Find_FG_of_FTI"></a>

### Find the filegroup or file that contains a full-text index

When a full-text index is created, it's placed in one of the following locations:

- A user-specified filegroup.
- The same filegroup as base table or view, for a nonpartitioned table.
- The primary filegroup, for a partitioned table.

> [!NOTE]  
> For information about creating a full-text index, see [Create and manage full-text indexes](create-and-manage-full-text-indexes.md) and [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md).

To find the filegroup of a full-text index on a table or view, use the following query, where *object_name* is the name of the table or view:

```sql
SELECT name
FROM sys.filegroups AS f, sys.fulltext_indexes AS i
WHERE f.data_space_id = i.data_space_id
      AND i.object_id = object_id('object_name');
```

<a id="Back_up_FTIs_of_FTC"></a>

### Back up the filegroups that contain full-text indexes

After you find the filegroups that contain the indexes of a full-text catalog, back up each filegroup. During the backup process, full-text catalogs might not be dropped or added.

The first backup of a filegroup must be a full file backup. After you create a full file backup for a filegroup, you can back up only the changes in a filegroup by creating one or more differential file backups based on the full file backup.

### Back up files and filegroups

- [Back Up Files and Filegroups](../backup-restore/back-up-files-and-filegroups-sql-server.md)
- [BACKUP](../../t-sql/statements/backup-transact-sql.md)

<a id="Restore_FTI"></a>

## Restore a full-text index

Restoring a backed-up filegroup restores the full-text index files and the other files in the filegroup. By default, the filegroup is restored to the disk location where it was backed up.

If a full-text indexed table was online and a population was running when the backup was created, the population resumes after the restore.

### Restore a filegroup

- [Restore Files and Filegroups](../backup-restore/restore-files-and-filegroups-sql-server.md)
- [Restore Files and Filegroups over Existing Files](../backup-restore/restore-files-and-filegroups-over-existing-files-sql-server.md)
- [Restore Files to a New Location](../backup-restore/restore-files-to-a-new-location-sql-server.md)
- [RESTORE Statements](../../t-sql/statements/restore-statements-transact-sql.md)

## Related content

- [Manage and Monitor Full-Text Search for a Server Instance](manage-and-monitor-full-text-search-for-a-server-instance.md)
- [Upgrade Full-Text Search (SQL Server Search)](upgrade-full-text-search.md)
