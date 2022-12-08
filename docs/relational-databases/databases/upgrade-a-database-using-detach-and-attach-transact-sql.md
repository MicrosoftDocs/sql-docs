---
description: "Upgrade a database using detach and attach (Transact-SQL)"
title: "Upgrade a database using detach & attach (Transact-SQL)"
ms.date: 06/03/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "database attaching [SQL Server]"
  - "upgrading databases"
  - "database upgrades [SQL Server]"
  - "database detaching [SQL Server]"
  - "detaching databases [SQL Server]"
  - "attaching databases [SQL Server]"
ms.assetid: 99f66ed9-3a75-4e38-ad7d-6c27cc3529a9
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-dt-2019
---
# Upgrade a database using detach and attach (Transact-SQL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
This topic describes how to use detach and attach operations to upgrade a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. After being attached to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], the database is available immediately and is automatically upgraded. This prevents the database from being used with an older version of the [!INCLUDE[ssde_md](../../includes/ssde_md.md)]. However, metadata upgrade does not affect the [database compatibility level](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md) setting of a database. See more information in [Database Compatibility Level After Upgrade](#dbcompat) later in this topic.  
  
 **In this topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
-   **To Upgrade a SQL Server Database:**  
  
     [Using detach and attach operations](#SSMSProcedure)  
  
-   **Follow Up:**    

     [After Upgrading a SQL Server Database](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The system databases cannot be attached.  
  
-   Attach and detach disable cross-database ownership chaining for the database by setting its **cross db ownership chaining** option to 0. For information about enabling chaining, see [cross db ownership chaining Server Configuration Option](../../database-engine/configure-windows/cross-db-ownership-chaining-server-configuration-option.md).  
  
-   When attaching a replicated database that was copied instead of detached:  
  
    -   If you attach the database to an upgraded version of the same server instance, you must execute **sp_vupgrade_replication** to upgrade replication after the attach operation finishes. For more information, see [sp_vupgrade_replication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-vupgrade-replication-transact-sql.md).  
  
    -   If you attach the database to a different server instance (regardless of version), you must execute **sp_removedbreplication** to remove replication after the attach operation finishes. For more information, see [sp_removedbreplication &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md).  
  
###  <a name="Recommendations"></a> Recommendations  
We recommend that you do not attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure. Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database.  
  
##  <a name="SSMSProcedure"></a> To Upgrade a Database by Using Detach and Attach  
  
1.  Detach the database. For more information, see [Detach a Database](../../relational-databases/databases/detach-a-database.md).  
  
2.  Optionally, move the detached database file or files and the log file or files.  
  
     You should move the log files along with the data files, even if you intend to create new log files. In some cases, reattaching a database requires its existing log files. Therefore, always keep all the detached log files until the database has been successfully attached without them.  
  
    > [!NOTE]  
    >  If you try to attach the database without specifying the log file, the attach operation will look for the log file in its original location. If the original copy of the log still exists in that location, that copy is attached. To avoid using the original log file, either specify the path of the new log file or remove the original copy of the log file (after copying it to the new location).  
  
3.  Attach the copied files to the instance of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. For more information, see [Attach a Database](../../relational-databases/databases/attach-a-database.md).  
  
## Example  
 The following example upgrades a copy of a database from an earlier version of SQL Server. The [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are executed in a Query Editor window that is connected to the server instance to which is attached.  
  
1.  Detach the database by executing the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements:  
  
    ```sql  
    USE master;  
    GO  
    EXEC sp_detach_db @dbname = N'MyDatabase';  
    GO  
    ```  
  
2.  Using the method of your choice, copy the data and log files to the new location.  
  
    > [!IMPORTANT]  
    > For a production database, preferably place the database and transaction log on separate disks. These drive different I/O and file growth requirements and it is considered a best practice to keep them separate.  
  
    To copy files over the network to a disk on a remote computer, use the universal naming convention (UNC) name of the remote location. A UNC name takes the form `\\Servername\Sharename\Path\Filename`. As with writing files to the local hard disk, the appropriate permissions that are required to read or write to a file on the remote disk must be granted to the user account used by the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
3.  Attach the moved database and, optionally, its log by executing the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
    ```sql  
    USE master;  
    GO  
    CREATE DATABASE MyDatabase   
        ON (FILENAME = 'C:\MySQLServer\MyDatabase.mdf'),  
        (FILENAME = 'C:\MySQLServer\Database.ldf')  
        FOR ATTACH;  
    GO  
    ```  
  
    In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], a newly attached database is not immediately visible in Object Explorer. To view the database, in Object Explorer, click **View,** and then **Refresh**. When the **Databases** node is expanded in Object Explorer, the newly attached database now appears in the list of databases.  
  
##  <a name="FollowUp"></a> Follow Up: After Upgrading a SQL Server Database  
If the database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **upgrade_option** server property. If the upgrade option is set to import (**upgrade_option** = 2) or rebuild (**upgrade_option** = 0), the full-text indexes will be unavailable during the upgrade. Depending the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to import, the associated full-text indexes are rebuilt if a full-text catalog is not available. To change the setting of the **upgrade_option** server property, use [sp_fulltext_service](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md).  
  
### <a name="dbcompat"></a> Database Compatibility Level After Upgrade  

After the upgrade, the database compatibility level remains at the compatibility level before the upgrade, unless the previous compatibility level is not supported on the new version. In this case, the upgraded database compatibility level is set to the lowest supported compatibility level.

For example, if you attach a database that was compatibility level 90 before attaching it to an instance of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], after the upgrade the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

### Managing Metadata on the Upgraded Server Instance  
When you attach a database onto another server instance, to provide a consistent experience to users and applications, you might have to re-create some or all of the metadata for the database, such as logins, jobs, and permissions, on the other server instance. For more information, see [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md).  
  
### Service Master Key and Database Master Key Encryption changes from 3DES to AES  
 [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and higher versions uses the AES encryption algorithm to protect the service master key (SMK) and the database master key (DMK). AES is a newer encryption algorithm than 3DES used in earlier versions. When a database is first attached or restored to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a copy of the database master key (encrypted by the service master key) is not yet stored in the server. You must use the `OPEN MASTER KEY` statement to decrypt the database master key (DMK). Once the DMK has been decrypted, you have the option of enabling automatic decryption in the future by using the `ALTER MASTER KEY REGENERATE` statement to provision the server with a copy of the DMK, encrypted with the service master key (SMK). When a database has been upgraded from an earlier version, the DMK should be regenerated to use the newer AES algorithm. For more information about regenerating the DMK, see [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md). The time required to regenerate the DMK key to upgrade to AES depends upon the number of objects protected by the DMK. Regenerating the DMK key to upgrade to AES is only necessary once, and has no impact on future regenerations as part of a key rotation strategy.  
  
  
