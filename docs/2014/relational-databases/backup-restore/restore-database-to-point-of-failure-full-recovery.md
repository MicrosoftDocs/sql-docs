---
title: "Restore a Database to the Point of Failure Under the Full Recovery Model (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "point of failure [SQL Server]"
  - "restoring databases [SQL Server], point of failure"
  - "database restores [SQL Server], point of failure"
ms.assetid: 04106e18-bbf7-4a5e-a2e1-3d65319814d5
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore a Database to the Point of Failure Under the Full Recovery Model (Transact-SQL)
  This topic explains how to restore to the point of failure. The topic is relevant only for databases that are using the full or bulk-logged recovery models.  
  
### To restore to the point of failure  
  
1.  Back up the tail of the log by running the following basic [BACKUP](/sql/t-sql/statements/backup-transact-sql) statement:  
  
    ```  
    BACKUP LOG <database_name> TO <backup_device>   
       WITH NORECOVERY, NO_TRUNCATE;  
    ```  
  
2.  Restore a full database backup by running the following basic [RESTORE DATABASE](/sql/t-sql/statements/restore-statements-transact-sql) statement:  
  
    ```  
    RESTORE DATABASE <database_name> FROM <backup_device>   
       WITH NORECOVERY;  
    ```  
  
3.  Optionally, restore a differential database backup by running the following basic RESTORE DATABASE statement:  
  
    ```  
    RESTORE DATABASE <database_name> FROM <backup_device>   
       WITH NORECOVERY;  
    ```  
  
4.  Apply each transaction log, including the tail-log backup you created in step 1, by specifying WITH NORECOVERY in the RESTORE LOG statement:  
  
    ```  
    RESTORE LOG <database_name> FROM <backup_device>   
       WITH NORECOVERY;  
    ```  
  
5.  Recover the database by running the following RESTORE DATABASE statement:  
  
    ```  
    RESTORE DATABASE <database_name>   
       WITH RECOVERY;  
    ```  
  
## Example  
 Before you can run the example, you must complete the following preparations:  
  
1.  The default recovery model of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database is the simple recovery model. Because this recovery model does not support restoring to the point of a failure, set [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] to use the full recovery model by running the following [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql) statement:  
  
    ```  
    USE master;  
    GO  
    ALTER DATABASE AdventureWorks2012 SET RECOVERY FULL;  
    ```  
  
2.  Create a full database back of the database by using the following BACKUP statement:  
  
    ```  
    BACKUP DATABASE AdventureWorks2012 TO DISK = 'C:\AdventureWorks2012_Data.bck';  
    ```  
  
3.  Create a routine log backup:  
  
    ```  
    BACKUP LOG AdventureWorks2012 TO DISK = 'C:\AdventureWorks2012_Log.bck';  
    ```  
  
 The following example restores the backups that are created previously, after creating a tail-log backup of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. (This step assumes that the log disk can be accessed.)  
  
 First, the example creates a tail-log backup of the database that captures the active log and leaves the database in the Restoring state. Then, the example restores the database backup, applies the routine log backup created previously, and applies the tail-log backup. Finally, the example recovers the database in a separate step.  
  
> [!NOTE]  
>  The default behavior is to recover a database as part of the statement that restores the final backup.  
  
```  
/* Example of restoring a to the point of failure */  
-- Step 1: Create a tail-log backup by using WITH NORECOVERY.  
BACKUP LOG AdventureWorks2012  
   TO DISK = 'C:\AdventureWorks2012_Log.bck'  
   WITH NORECOVERY;  
GO  
-- Step 2: Restore the full database backup.  
RESTORE DATABASE AdventureWorks2012  
   FROM DISK = 'C:\AdventureWorks2012_Data.bck'  
   WITH NORECOVERY;  
GO  
-- Step 3: Restore the first transaction log backup.  
RESTORE LOG AdventureWorks2012  
   FROM DISK = 'C:\AdventureWorks2012_Log.bck'  
   WITH NORECOVERY;  
GO  
-- Step 4: Restore the tail-log backup.  
RESTORE LOG AdventureWorks2012  
   FROM  DISK = 'C:\AdventureWorks2012_Log.bck'  
   WITH NORECOVERY;  
GO  
-- Step 5: Recover the database.  
RESTORE DATABASE AdventureWorks2012  
   WITH RECOVERY;  
GO  
```  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)  
  
  
