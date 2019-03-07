---
title: "Upgrade Full-Text Search | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text search [SQL Server], installing"
  - "migrating full-text indexes [SQL Server]"
  - "upgrading Full-Text Search"
  - "installing Full-Text Search"
  - "full-text search [SQL Server], upgrading"
ms.assetid: 2fee4691-f2b5-472f-8ccc-fa625b654520
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Upgrade Full-Text Search
  Upgrading full-text search to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is done during setup and when database files and full-text catalogs from the earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are attached, restored, or copied using the Copy Database Wizard.  
  
 This topic discusses the following aspects of upgrading full-text search:  
  
-   [Upgrading a Server Instance](#Upgrade_Server)  
  
-   [Full-Text Upgrade Options](#FT_Upgrade_Options)  
  
-   [Considerations for Choosing a Full-Text Upgrade Option](#Choosing_Upgade_Option)  
  
-   [Migrating Full-Text Indexes When Upgrading a Database to SQL Server 2014](#Upgrade_Db)  
  
-   [Considerations for Restoring a SQL Server 2005 Full-Text Catalog to SQL Server 2014](#Considerations_for_Restore)  
  
-   [Attaching a SQL Server 2005 database to SQL Server 2014](#Attaching_2005_ft_catalogs)  
  
##  <a name="Upgrade_Server"></a> Upgrading a Server Instance  
 For an in-place upgrade, an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is set up side-by-side with the old version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and data is migrated. If the old version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] had full-text search installed, a new version of full-text search is automatically installed. Side-by-side install means that each of the following components exists at the instance-level of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Word breakers, stemmers, and filters  
 Each instance now uses its own set of word breakers, stemmers, and filters, rather than relying on the operating system version of these components. These components are also easier to register and configure at a per-instance level. For more information, see [Configure and Manage Word Breakers and Stemmers for Search](configure-and-manage-word-breakers-and-stemmers-for-search.md) and [Configure and Manage Filters for Search](configure-and-manage-filters-for-search.md).  
  
 Filter daemon host  
 The full-text filter daemon hosts are processes that safely load and drive extensible external components used for index and query, such as word breakers, stemmers, and filters, without compromising the integrity of the Full-Text Engine. A server instance uses a multithreaded process for all multithreaded filters and a single-threaded process for all single-threaded filters.  
  
> [!NOTE]  
>  [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] introduced a service account for the FDHOST Launcher service (MSSQLFDLauncher). This service propagates the service account information to the filter daemon host processes of a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For information about setting the service account, see [Set the Service Account for the Full-text Filter Daemon Launcher](set-the-service-account-for-the-full-text-filter-daemon-launcher.md).  
  
 In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], each full-text index resides in a full-text catalog that belongs to a filegroup, has a physical path, and is treated as a database file. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, a full-text catalog is a logical or virtual object that contains a group of full-text indexes. Therefore, a new full-text catalog is not treated as a database file with a physical path. However, during upgrade of any full-text catalog that contains data files, a new filegroup is created on same disk. This maintains the old disk I/O behavior after upgrade. Any full-text index from that catalog is placed in the new filegroup if the root path exists. If the old full-text catalog path is invalid, the upgrade keeps the full-text index in the same filegroup as the base table or, for a partitioned table, in the primary filegroup.  
  
> [!NOTE]  
>  [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[tsql](../../includes/tsql-md.md)] DDL statements that specify full-text catalogs continue to work correctly.  
  
##  <a name="FT_Upgrade_Options"></a> Full-Text Upgrade Options  
 When upgrading a server instance to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the user interface allows you to choose one of the following full-text upgrade options.  
  
 Import  
 Full-text catalogs are imported. Typically, import is significantly faster than rebuild. For example, when using only one CPU, import runs about 10 times faster than rebuild. However, an imported full-text catalog does not use the new word breakers installed with the latest version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To ensure consistency in query results, full-text catalogs have to be rebuilt.  
  
> [!NOTE]  
>  Rebuild can run in multi-threaded mode, and if more than 10 CPUs are available, rebuild might run faster than import if you allow rebuild to use all of the CPUs.  
  
 If a full-text catalog is not available, the associated full-text indexes are rebuilt. This option is available for only [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] databases.  
  
 For information about the impact of importing full-text index, see "Considerations for Choosing a Full-Text Upgrade Option," later in this topic.  
  
 Rebuild  
 Full-text catalogs are rebuilt using the new and enhanced word breakers. Rebuilding indexes can take a while, and a significant amount of CPU and memory might be required after the upgrade.  
  
 Reset  
 Full-text catalogs are reset. When upgrading from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], full-text catalog files are removed, but the metadata for full-text catalogs and full-text indexes is retained. After being upgraded, all full-text indexes are disabled for change tracking and crawls are not started automatically. The catalog will remain empty until you manually issue a full population, after the upgrade completes.  
  
##  <a name="Choosing_Upgade_Option"></a> Considerations for Choosing a Full-Text Upgrade Option  
 When choosing the upgrade option for your upgrade, consider the following:  
  
-   Do you require consistency in query results?  
  
     [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installs new word breakers for use by Full-Text and Semantic Search. The word breakers are used both at indexing time and at query time. If you do not rebuild the full-text catalogs, your search results may be inconsistent. If you issue a full-text query that looks for a phrase that is broken differently by the word breaker in a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the current word breaker, a document or row containing the phrase might not be retrieved. This is because the indexed phrases were broken using different logic than the query is using. The solution is to repopulate (rebuild) the full-text catalogs with the new word breakers so that index time and query time behavior are identical. You can choose the Rebuild option to accomplish this, or you can rebuild manually after choosing the Import option.  
  
-   Were any full-text indexes built on integer full-text key columns?  
  
     Rebuilding performs internal optimizations that improve the query performance of the upgraded full-text index in some cases. Specifically, if you have full-text catalogs that contain full-text indexes for which the full-text key column of the base table is an integer data type, rebuilding achieves ideal performance of full-text queries after upgrade. In this case, we highly recommend you to use the **Rebuild** option.  
  
    > [!NOTE]  
    >  For full-text indexes in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], we recommend that the column serving as the full-text key be an integer data type. For more information, see [Improve the Performance of Full-Text Indexes](improve-the-performance-of-full-text-indexes.md).  
  
-   What is the priority for getting your server instance online?  
  
     Importing or rebuilding during upgrade takes a lot of CPU resources, which delays getting the rest of the server instance upgraded and online. If getting the server instance online as soon as possible is important and if you are willing to run a manual population after the upgrade, **Reset** is suitable.  
  
## Ensuring Consistent Query Results after Importing a SQL Server 2005 Full-Text Index  
 If a full-text catalog was imported when upgrading a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], mismatches between the query and the full-text index content might occur because of differences in the behavior of the old and new word breakers. In this case, to guarantee a total match between queries and the full-text index content, choose one of the following options:  
  
-   Rebuild the full-text catalog that contains the full-text index ([ALTER FULLTEXT CATALOG](/sql/t-sql/statements/alter-fulltext-catalog-transact-sql)*catalog_name* REBUILD)  
  
-   Issue a FULL POPULATION on the full-text index ([ALTER FULLTEXT INDEX](/sql/t-sql/statements/alter-fulltext-index-transact-sql) ON *table_name* START FULL POPULATION).  
  
 For more information about word breakers, see [Configure and Manage Word Breakers and Stemmers for Search](configure-and-manage-word-breakers-and-stemmers-for-search.md).  
  
## Upgrading Noise-Word Files to Stoplists  
 [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] noise words have been replaced by stopwords in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions. When a database is upgraded to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the noise-word files are no longer used. However, the old noise-word files are stored in the FTDATA\ FTNoiseThesaurusBak folder, and you can use them later when updating or building the corresponding [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] stoplists.  
  
 After upgrading from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]:  
  
-   If you never added, modified, or deleted any noise-word files in your installation of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the system stoplist should meet your needs.  
  
-   If your noise-word files were modified in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], those modifications are lost during upgrade. To re-create those updates, you must manually recreate those modifications in the corresponding [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] stoplist. For more information, see [ALTER FULLTEXT STOPLIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-stoplist-transact-sql).  
  
-   If you do not want to apply any stopwords to your full-text indexes (for example, if you deleted or erased your noise-word files in your [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] installation), you must turn off the stoplist for each upgraded full-text index. Run the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement (replacing *database* with the name of the upgraded database and *table* with the name of the *table*):  
  
    ```  
    Use database;   
    ALTER FULLTEXT INDEX ON table  
       SET STOPLIST OFF;  
    GO  
    ```  
  
     The STOPLIST OFF clause removes stop-word filtering, and it will trigger a population of the table, without filtering any words considered to be noise.  
  
## Backup and Imported Full-Text Catalogs  
 For full-text catalogs that are rebuilt or reset during upgrade (and for new full-text catalogs), the fulltext catalog is a logical concept and does not reside in a filegroup. Therefore, to back up a full-text catalog in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you must identify every filegroup that contains a full-text index of the catalog and back each of them up, one by one. For more information, see [Back Up and Restore Full-Text Catalogs and Indexes](back-up-and-restore-full-text-catalogs-and-indexes.md).  
  
 For full-text catalogs that have been imported from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the full-text catalog is still a database file in its own filegroup. The [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] backup process for full-text catalogs still applies except that the MSFTESQL service does not exist in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For information about the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] process, see [Backing Up and Restoring Full-Text Catalogs](https://go.microsoft.com/fwlink/?LinkId=209154) in SQL Server 2005 Books Online.  
  
##  <a name="Upgrade_Db"></a> Migrating Full-Text Indexes When Upgrading a Database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
 Database files and full-text catalogs from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be upgraded to an existing [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] server instance by using attach, restore, or the Copy Database Wizard. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] full-text indexes, if any, are either imported, reset, or rebuilt. The **upgrade_option** server property controls which full-text upgrade option the server instance uses during these database upgrades.  
  
 After you attach, restore, or copy any [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the database becomes available immediately and is then automatically upgraded. Depending the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to import, if a full-text catalog is not available, the associated full-text indexes are rebuilt.  
  
 **To change full-text upgrade behavior on a server instance**  
  
-   [!INCLUDE[tsql](../../includes/tsql-md.md)]: Use the **upgrade\_option** action of [sp\_fulltext\_service](/sql/relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql)  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **:** Use the **Full-Text Upgrade Option** of the **Server Properties** dialog box. For more information, see [Manage and Monitor Full-Text Search for a Server Instance](manage-and-monitor-full-text-search-for-a-server-instance.md).  
  
##  <a name="Considerations_for_Restore"></a> Considerations for Restoring a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Full-Text Catalog to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
 One method of upgrading fulltext data from a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is to restore a full database backup to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 While importing a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] full-text catalog, you can back up and restore the database and the catalog file. The behavior is the same as in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]:  
  
-   The full database backup will include the full-text catalog. To refer to the full-text catalog, use its [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] file name, sysft_+*catalog-name*.  
  
-   If the full-text catalog is offline, the backup will fail.  
  
 For more information about backing up and restoring [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] full-text catalogs, see [Backing Up and Restoring Full-Text Catalogs](https://go.microsoft.com/fwlink/?LinkId=121052) and [File Backup and Restore and Full-Text Catalogs](https://go.microsoft.com/fwlink/?LinkId=121053)in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Books Online.  
  
 When the database is restored on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], a new database file will be created for the full-text catalog. The default name of this file is ftrow_*catalog-name*.ndf. For example, if you *catalog-name* is `cat1`, the default name of the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] database file would be `ftrow_cat1.ndf`. But if the default name is already being used in the target directory, the new database file would be named `ftrow_`*catalog-name*`{`*GUID*`}.ndf`, where *GUID* is the Globally Unique Identifier of the new file.  
  
 After the catalogs have been imported, the **sys.database_files** and **sys.master_file**s are updated to remove the catalog entries and the **path** column in **sys.fulltext_catalogs** is set to NULL.  
  
 **To back up a database**  
  
-   [Full Database Backups &#40;SQL Server&#41;](../backup-restore/full-database-backups-sql-server.md)  
  
-   [Transaction Log Backups &#40;SQL Server&#41;](../backup-restore/transaction-log-backups-sql-server.md) (full recovery model only)  
  
 **To restore a database backup**  
  
-   [Complete Database Restores &#40;Simple Recovery Model&#41;](../backup-restore/complete-database-restores-simple-recovery-model.md)  
  
-   [Complete Database Restores &#40;Full Recovery Model&#41;](../backup-restore/complete-database-restores-full-recovery-model.md)  
  
### Example  
 The following example uses the MOVE clause in the [RESTORE](/sql/t-sql/statements/restore-statements-transact-sql) statement, to restore a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database named `ftdb1`. The [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database, log, and catalog files are moved to new locations on the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] server instance, as follows:  
  
-   The database file, `ftdb1.mdf`, is moved to `C:\Program Files\Microsoft SQL Server\MSSQL.1MSSQL12.MSSQLSERVER\MSSQL\DATA\ftdb1.mdf`.  
  
-   The log file, `ftdb1_log.ldf`, is moved to a log directory on your log disk drive, *log_drive*`:\`*log_directory*`\ftdb1_log.ldf`.  
  
-   The catalog files that correspond to the `sysft_cat90` catalog are moved to `C:\temp`. After the full-text indexes are imported, they will automatically be placed in a database file, C:\ftrow_sysft_cat90.ndf, and the C:\temp will be deleted.  
  
```  
RESTORE DATABASE [ftdb1] FROM  DISK = N'C:\temp\ftdb1.bak' WITH  FILE = 1,  
   MOVE N'ftdb1' TO N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ftdb1.mdf',  
    MOVE N'ftdb1_log' TO N'log_drive:\log_directory\ftdb1_log.ldf',  
    MOVE N'sysft_cat90' TO N'C:\temp';  
```  
  
##  <a name="Attaching_2005_ft_catalogs"></a> Attaching a SQL Server 2005 Database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
 In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, a full-text catalog is a logical concept that refers to a group of full-text indexes. The full-text catalog is a virtual object that does not belong to any filegroup. However, when you attach a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database that contains full-text catalog files onto a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] server instance, the catalog files are attached from their previous location along with the other database files, the same as in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
 The state of each attached full-text catalog on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is the same as when the database was detached from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. If any full-text index population was suspended by the detach operation, the population is resumed on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], and the full-text index becomes available for full-text search.  
  
 If [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] cannot find a full-text catalog file or if the full-text file was moved during the attach operation without specifying a new location, the behavior depends on the selected full-text upgrade option. If the full-text upgrade option is **Import** or **Rebuild**, the attached full-text catalog is rebuilt. If the full-text upgrade option is **Reset**, the attached full-text catalog is reset.  
  
 For more information about detaching and attaching a database, see [Database Detach and Attach &#40;SQL Server&#41;](../databases/database-detach-and-attach-sql-server.md), [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql), [sp_attach_db](/sql/relational-databases/system-stored-procedures/sp-attach-db-transact-sql), and [sp_detach_db &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-detach-db-transact-sql).  
  
## See Also  
 [Get Started with Full-Text Search](get-started-with-full-text-search.md)   
 [Configure and Manage Word Breakers and Stemmers for Search](configure-and-manage-word-breakers-and-stemmers-for-search.md)   
 [Configure and Manage Filters for Search](configure-and-manage-filters-for-search.md)  
  
  
