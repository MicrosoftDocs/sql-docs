---
title: "Back Up and Restore Full-Text Catalogs and Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text indexes [SQL Server], backing up"
  - "full-text search [SQL Server], back up and restore"
  - "recovery [full-text search]"
  - "backups [SQL Server], full-text indexes"
  - "full-text indexes [SQL Server], restoring"
  - "restore operations [full-text search]"
ms.assetid: 6a4080d9-e43f-4b7b-a1da-bebf654c1194
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Back Up and Restore Full-Text Catalogs and Indexes
  This topic explains how to back up and restore full-text indexes created in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the full-text catalog is a logical concept and does not reside in a filegroup. Therefore, to back up a full-text catalog in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must identify every filegroup that contains a full-text index that belongs to the catalog. Then you must back up those filegroups, one by one.  
  
> [!IMPORTANT]  
>  It is possible to import full-text catalogs when upgrading a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database. Each imported full-text catalog is a database file in its own filegroup. To back up an imported catalog, simply back up its filegroup. For more information, see [Backing Up and Restoring Full-Text Catalogs](https://go.microsoft.com/fwlink/?LinkID=121052), in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Books Online.  
  
##  <a name="backingup"></a> Backing Up the Full-Text Indexes of a Full-Text Catalog  
  
###  <a name="Find_FTIs_of_a_Catalog"></a> Finding the Full-Text Indexes of a Full-Text Catalog  
 You can retrieve the properties of the full-text indexes by using the following [SELECT](/sql/t-sql/queries/select-transact-sql) statement, which selects columns from the [sys.fulltext_indexes](/sql/relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql) and [sys.fulltext_catalogs](/sql/relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql) catalog views.  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @TableID int;  
SET @TableID = (SELECT OBJECT_ID('AdventureWorks2012.Production.Product'));  
SELECT object_name(@TableID), i.is_enabled, i.change_tracking_state,   
   i.has_crawl_completed, i.crawl_type, c.name as fulltext_catalog_name   
   FROM sys.fulltext_indexes i, sys.fulltext_catalogs c   
   WHERE i.fulltext_catalog_id = c.fulltext_catalog_id;  
GO  
```  
  

  
###  <a name="Find_FG_of_FTI"></a> Finding the Filegroup or File That Contains a Full-Text Index  
 When a full-text index is created, it is placed in one of the following locations:  
  
-   A user-specified filegroup.  
  
-   The same filegroup as base table or view, for a nonpartitioned table.  
  
-   The primary filegroup, for a partitioned table.  
  
> [!NOTE]  
>  For information about creating a full-text index, see [Create and Manage Full-Text Indexes](create-and-manage-full-text-indexes.md) and [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql).  
  
 To find the filegroup of full-text index on a table or view, use the following query, where *object_name* is the name of the table or view:  
  
```  
SELECT name FROM sys.filegroups f, sys.fulltext_indexes i   
   WHERE f.data_space_id = i.data_space_id   
      and i.object_id = object_id('object_name');  
GO  
  
```  
  

  
###  <a name="Back_up_FTIs_of_FTC"></a> Backing Up the Filegroups That Contain Full-Text Indexes  
 After you find the filegroups that contain the indexes of a full-text catalog, you need back up each of the filegroups. During the backup process, full-text catalogs may not be dropped or added.  
  
 The first backup of a filegroup must be a full file backup. After you have created a full file backup for a filegroup, you could back up only the changes in a filegroup by creating a series of one or more differential file backups that are based on the full file backup.  
  
 **To back up files and filegroups**  
  
-   [Back Up Files and Filegroups &#40;SQL Server&#41;](../backup-restore/back-up-files-and-filegroups-sql-server.md)  
  
-   [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)  
  

  
##  <a name="Restore_FTI"></a> Restoring a Full-Text Index  
 Restoring a backed-up filegroup restores the full-text index files, as well as the other files in the filegroup. By default, the filegroup is restored to the disk location on which the filegroup was backed up.  
  
 If a full-text indexed table was online and a population was running when the backup was created, the population is resumed after the restore.  
  
 **To restore a filegroup**  
  
-   [Restore Files and Filegroups &#40;SQL Server&#41;](../backup-restore/restore-files-and-filegroups-sql-server.md)  
  
-   [Restore Files and Filegroups over Existing Files &#40;SQL Server&#41;](../backup-restore/restore-files-and-filegroups-over-existing-files-sql-server.md)  
  
-   [Restore Files to a New Location &#40;SQL Server&#41;](../backup-restore/restore-files-to-a-new-location-sql-server.md)  
  
-   [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)  
  

  
## See Also  
 [Manage and Monitor Full-Text Search for a Server Instance](manage-and-monitor-full-text-search-for-a-server-instance.md)   
 [Upgrade Full-Text Search](upgrade-full-text-search.md)  
  
  
