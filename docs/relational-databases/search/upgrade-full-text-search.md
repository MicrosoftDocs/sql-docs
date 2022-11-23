---
description: "Upgrade Full-Text Search"
title: "Upgrade Full-Text Search | Microsoft Docs"
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text search [SQL Server], installing"
  - "migrating full-text indexes [SQL Server]"
  - "upgrading Full-Text Search"
  - "installing Full-Text Search"
  - "full-text search [SQL Server], upgrading"
ms.assetid: 2fee4691-f2b5-472f-8ccc-fa625b654520
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Upgrade Full-Text Search

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

SQL Server upgrades full-text search during setup, or when you attach, restore, or copy database files and full-text catalogs from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

##  <a name="Upgrade_Server"></a> Upgrade a server instance  

For an in-place upgrade, an instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] is set up side-by-side with the old version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and data is migrated. If the old version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] had full-text search installed, a new version of full-text search is automatically installed. Side-by-side install means that each of the following components exists at the instance-level of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Word breakers, stemmers, and filters  
 Each instance now uses its own set of word breakers, stemmers, and filters, rather than relying on the operating system version of these components. These components are also easier to register and configure at a per-instance level. For more information, see [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md) and [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md).  
  
 Filter daemon host  
 The full-text filter daemon hosts are processes that safely load and drive extensible external components used for index and query, such as word breakers, stemmers, and filters, without compromising the integrity of the Full-Text Engine. A server instance uses a multithreaded process for all multithreaded filters and a single-threaded process for all single-threaded filters.  
  
> [!NOTE]  
>  [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] introduced a service account for the FDHOST Launcher service (MSSQLFDLauncher). This service propagates the service account information to the filter daemon host processes of a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For information about setting the service account, see [Set the Service Account for the Full-text Filter Daemon Launcher](../../relational-databases/search/set-the-service-account-for-the-full-text-filter-daemon-launcher.md).  
  
 In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], each full-text index resides in a full-text catalog that belongs to a filegroup, has a physical path, and is treated as a database file. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, a full-text catalog is a logical or virtual object that contains a group of full-text indexes. Therefore, a new full-text catalog is not treated as a database file with a physical path. However, during upgrade of any full-text catalog that contains data files, a new filegroup is created on same disk. This maintains the old disk I/O behavior after upgrade. Any full-text index from that catalog is placed in the new filegroup if the root path exists. If the old full-text catalog path is invalid, the upgrade keeps the full-text index in the same filegroup as the base table or, for a partitioned table, in the primary filegroup.  
  
## <a name="FT_Upgrade_Options"></a> Full-text upgrade options  

When upgrading a [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] instance, the user interface allows you to choose one of the following full-text upgrade options.  
  
**Import**  
 Full-text catalogs are imported. Typically, import is significantly faster than rebuild. For example, when using only one CPU, import runs about 10 times faster than rebuild. However, an imported full-text catalog does not use the new word breakers installed with the latest version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To ensure consistency in query results, full-text catalogs have to be rebuilt.  
  
> [!NOTE]  
>  Rebuild can run in multi-threaded mode, and if more than 10 CPUs are available, rebuild might run faster than import if you allow rebuild to use all of the CPUs.  
  
 If a full-text catalog is not available, the associated full-text indexes are rebuilt. This option is available for only [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] databases.  
  
 For information about the impact of importing full-text index, see "Considerations for Choosing a Full-Text Upgrade Option," later in this topic.  
  
 **Rebuild**  
 Full-text catalogs are rebuilt using the new and enhanced word breakers. Rebuilding indexes can take a while, and a significant amount of CPU and memory might be required after the upgrade.  
  
 **Reset**  
 Full-text catalogs are reset. When upgrading from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], full-text catalog files are removed, but the metadata for full-text catalogs and full-text indexes is retained. After being upgraded, all full-text indexes are disabled for change tracking and crawls are not started automatically. The catalog will remain empty until you manually issue a full population, after the upgrade completes.  
  
##  <a name="Choosing_Upgade_Option"></a> Considerations for choosing a full-text upgrade option  

When choosing the upgrade option for your upgrade, consider the following:  
  
- Do you require consistency in query results?  
  
   [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] installs new word breakers for use by Full-Text and Semantic Search. The word breakers are used both at indexing time and at query time. If you do not rebuild the full-text catalogs, your search results may be inconsistent. If you issue a full-text query that looks for a phrase that is broken differently by the word breaker in a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the current word breaker, a document or row containing the phrase might not be retrieved. This is because the indexed phrases were broken using different logic than the query is using. The solution is to repopulate (rebuild) the full-text catalogs with the new word breakers so that index time and query time behavior are identical. You can choose the Rebuild option to accomplish this, or you can rebuild manually after choosing the Import option.  
  
- Were any full-text indexes built on integer full-text key columns?  
  
   Rebuilding performs internal optimizations that improve the query performance of the upgraded full-text index in some cases. Specifically, if you have full-text catalogs that contain full-text indexes for which the full-text key column of the base table is an integer data type, rebuilding achieves ideal performance of full-text queries after upgrade. In this case, we highly recommend you to use the **Rebuild** option.  
  
   > [!NOTE]  
   > For full-text indexes, we recommend that the column serving as the full-text key be an integer data type. For more information, see [Improve the Performance of Full-Text Indexes](../../relational-databases/search/improve-the-performance-of-full-text-indexes.md).  
  
- What is the priority for getting your server instance online?  
  
   Importing or rebuilding during upgrade takes a lot of CPU resources, which delays getting the rest of the server instance upgraded and online. If getting the server instance online as soon as possible is important and if you are willing to run a manual population after the upgrade, **Reset** is suitable.  
  
## Ensure consistent query results after importing a full-text index  

If a full-text catalog was imported when upgrading a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database, mismatches between the query and the full-text index content might occur because of differences in the behavior of the old and new word breakers. In this case, to guarantee a total match between queries and the full-text index content, choose one of the following options:  
  
- Rebuild the full-text catalog that contains the full-text index ([ALTER FULLTEXT CATALOG](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md)*catalog_name* REBUILD)  
  
- Issue a FULL POPULATION on the full-text index ([ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) ON *table_name* START FULL POPULATION).  
  
 For more information about word breakers, see [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md).  
  
## Upgrade noise-word files to stoplists

When a database is upgraded from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the noise-word files are no longer used. However, the old noise-word files are stored in the `FTDATA\ FTNoiseThesaurusBak` folder, and you can use them later when updating or building the corresponding [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] stoplists.  
  
After upgrading from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]:  
  
- If you never added, modified, or deleted any noise-word files in your installation of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the system stoplist should meet your needs.  
  
- If your noise-word files were modified in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], those modifications are lost during upgrade. To re-create those updates, you must manually recreate those modifications in the corresponding stoplist. For more information, see [ALTER FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md).  
  
- If you do not want to apply any stopwords to your full-text indexes (for example, if you deleted or erased your noise-word files in your [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] installation), you must turn off the stoplist for each upgraded full-text index. Run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement (replacing *database* with the name of the upgraded database and *table* with the name of the *table*):  
  
    ```  
    Use database;   
    ALTER FULLTEXT INDEX ON table  
       SET STOPLIST OFF;  
    GO  
    ```  
  
     The STOPLIST OFF clause removes stop-word filtering, and it will trigger a population of the table, without filtering any words considered to be noise.  
  
## Backup and imported full-text catalogs

For full-text catalogs that are rebuilt or reset during upgrade (and for new full-text catalogs), the fulltext catalog is a logical concept and does not reside in a filegroup. Therefore, to back up a full-text catalog, you must identify every filegroup that contains a full-text index of the catalog and back each of them up, one by one. For more information, see [Back Up and Restore Full-Text Catalogs and Indexes](../../relational-databases/search/back-up-and-restore-full-text-catalogs-and-indexes.md).  
  
 For full-text catalogs that have been imported from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the full-text catalog is still a database file in its own filegroup. The [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] backup process for full-text catalogs still applies except that the MSFTESQL service does not exist in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. For information about the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] process, see [Backing Up and Restoring Full-Text Catalogs](/previous-versions/sql/sql-server-2005/ms142511(v=sql.90)) in SQL Server 2005 Books Online.  
  
##  <a name="Upgrade_Db"></a> Migrating full-text indexes when upgrading a database

 Database files and full-text catalogs from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be upgraded to an existing instance by using attach, restore, or the Copy Database Wizard. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] full-text indexes, if any, are either imported, reset, or rebuilt. The **upgrade_option** server property controls which full-text upgrade option the server instance uses during these database upgrades.  
  
 After you attach, restore, or copy any [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database a newer instance, the database becomes available immediately and is then automatically upgraded. Depending the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to import, if a full-text catalog is not available, the associated full-text indexes are rebuilt.  
  
 **To change full-text upgrade behavior on a server instance**
  
- [!INCLUDE[tsql](../../includes/tsql-md.md)]: Use the **upgrade\_option** action of [sp\_fulltext\_service](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md)  
  
- [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **:** Use the **Full-Text Upgrade Option** of the **Server Properties** dialog box. For more information, see [Manage and Monitor Full-Text Search for a Server Instance](../../relational-databases/search/manage-and-monitor-full-text-search-for-a-server-instance.md).  
  
##  <a name="Considerations_for_Restore"></a> Considerations for Restoring a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Full-Text Catalog
  
One method of upgrading fulltext data from a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database is to restore a full database backup to a newer instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].  
  
 While importing a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] full-text catalog, you can back up and restore the database and the catalog file. The behavior is the same as in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]:  
  
- The full database backup will include the full-text catalog. To refer to the full-text catalog, use its [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] file name, sysft_+*catalog-name*.  
  
- If the full-text catalog is offline, the backup will fail.  
  
 For more information about backing up and restoring [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] full-text catalogs, see [Backing Up and Restoring Full-Text Catalogs](./back-up-and-restore-full-text-catalogs-and-indexes.md) and [File Backup and Restore and Full-Text Catalogs](/previous-versions/sql/sql-server-2008-r2/ms190643(v=sql.105))in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Books Online.  
  
 When the database is restored on a newer instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], a new database file will be created for the full-text catalog. The default name of this file is ftrow_*catalog-name*.ndf. For example, if you *catalog-name* is `cat1`, the default name of the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] database file would be `ftrow_cat1.ndf`. But if the default name is already being used in the target directory, the new database file would be named `ftrow_`*catalog-name*`{`*GUID*`}.ndf`, where *GUID* is the Globally Unique Identifier of the new file.  
  
 After the catalogs have been imported, the **sys.database_files** and **sys.master_file**s are updated to remove the catalog entries and the **path** column in **sys.fulltext_catalogs** is set to NULL.  
  
 **To back up a database**  
  
- [Full Database Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-database-backups-sql-server.md)  
  
- [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md) (full recovery model only)  
  
 **To restore a database backup**  
  
- [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)  
  
- [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md)  
  
### Example

 The following example uses the MOVE clause in the [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) statement, to restore a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database named `ftdb1`. The [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database, log, and catalog files are moved to new locations on the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] server instance, as follows:  
  
- The database file, `ftdb1.mdf`, is moved to `C:\Program Files\Microsoft SQL Server\MSSQL.1MSSQL13.MSSQLSERVER\MSSQL\DATA\ftdb1.mdf`.  
  
- The log file, `ftdb1_log.ldf`, is moved to a log directory on your log disk drive, *log_drive*`:\`*log_directory*`\ftdb1_log.ldf`.  
  
- The catalog files that correspond to the `sysft_cat90` catalog are moved to `C:\temp`. After the full-text indexes are imported, they will automatically be placed in a database file, C:\ftrow_sysft_cat90.ndf, and the C:\temp will be deleted.  
  
```  
RESTORE DATABASE [ftdb1] FROM  DISK = N'C:\temp\ftdb1.bak' WITH  FILE = 1,  
   MOVE N'ftdb1' TO N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ftdb1.mdf',  
    MOVE N'ftdb1_log' TO N'log_drive:\log_directory\ftdb1_log.ldf',  
    MOVE N'sysft_cat90' TO N'C:\temp';  
```  
  
##  <a name="Attaching_2005_ft_catalogs"></a> Attaching a SQL Server 2005 database

In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, a full-text catalog is a logical concept that refers to a group of full-text indexes. The full-text catalog is a virtual object that does not belong to any filegroup. However, when you attach a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database that contains full-text catalog files onto a newer [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] server instance, the catalog files are attached from their previous location along with the other database files, the same as in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
The state of each attached full-text catalog on [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] is the same as when the database was detached from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. If any full-text index population was suspended by the detach operation, the population is resumed on [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], and the full-text index becomes available for full-text search.  
  
 If [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] cannot find a full-text catalog file or if the full-text file was moved during the attach operation without specifying a new location, the behavior depends on the selected full-text upgrade option. If the full-text upgrade option is **Import** or **Rebuild**, the attached full-text catalog is rebuilt. If the full-text upgrade option is **Reset**, the attached full-text catalog is reset.  
  
 For more information about detaching and attaching a database, see [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md), [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md), [sp_attach_db](../../relational-databases/system-stored-procedures/sp-attach-db-transact-sql.md), and [sp_detach_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md).  
  
## See also

 [Get Started with Full-Text Search](../../relational-databases/search/get-started-with-full-text-search.md)   
 [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md)   
 [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md)  
