---
title: "Prepare a secondary database for an availability group"
description: "A description for how to manually prepare a secondary database to join an Always On availability group."
ms.custom: "seodec18"
ms.date: "07/25/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.availabilitygroup.preparedbs.f1"
  - "sql13.swb.availabilitygroup.configsecondarydbs.f1"
helpviewer_keywords: 
  - "secondary databases [SQL Server], in availability group"
  - "secondary databases [SQL Server]"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], databases"
ms.assetid: 9f2feb3c-ea9b-4992-8202-2aeed4f9a6dd
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Prepare a secondary database for an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
This topic describes how to prepare a database for an Always On availability group in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell. Preparing a database requires two steps: 

1. Restore a recent database backup of the primary database and subsequent log backups onto each server instance that hosts the secondary replica, using RESTORE WITH NORECOVERY
2. Join the restored database to the availability group.  
  
> [!TIP]  
>  If you have an existing log shipping configuration, you might be able to convert the log shipping primary database along with one or more of its secondary databases to an availability group primary replica and one or more secondary replicas. For more information, see [Prerequisites for migrating from log Shipping to Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-migrating-log-shipping-to-always-on-availability-groups.md).  

##  <a name="Prerequisites"></a> Prerequisites and restrictions  
  
-   Make sure that the system where you plan to place database possesses a disk drive with sufficient space for the secondary databases.  
  
-   The name of the secondary database must be the same as the name of the primary database.  
  
-   Use RESTORE WITH NORECOVERY for every restore operation.  
  
-   If the secondary database needs to reside on a different file path (including the drive letter) than the primary database, the restore command must also use the WITH MOVE option for each of the database files to specify them to the path of the secondary database.  
  
-   If you restore the database filegroup by filegroup, be sure to restore the whole database.  
  
-   After restoring the database, you must restore (WITH NORECOVERY) every log backup created since the last restored data backup.  
  
##  <a name="Recommendations"></a> Recommendations  
  
-   On stand-alone instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], we recommend that, if possible, the file path (including the drive letter) of a given secondary database be identical to the path of the corresponding primary database. This is because if you move the database files when creating a secondary database, a later add-file operation might fail on the secondary database and cause the secondary database to be suspended.  
  
-   Before preparing your secondary databases, we strongly recommend that you suspend scheduled log backups on the databases in the availability group until the initialization of secondary replicas has completed.  
  
###  <a name="Security"></a> Security  
 When a database is backed up, the [TRUSTWORTHY database property](../../../relational-databases/security/trustworthy-database-property.md) is set to OFF. Therefore, TRUSTWORTHY is always OFF on a newly restored database.  
  
####  <a name="Permissions"></a> Permissions  
 BACKUP DATABASE and BACKUP LOG permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles. For more information, see [BACKUP &#40;Transact-SQL&#41;](../../../t-sql/statements/backup-transact-sql.md).  
  
 When the database being restored does not exist on the server instance, the RESTORE statement requires CREATE DATABASE permissions. For more information, see [RESTORE &#40;Transact-SQL&#41;](../../../t-sql/statements/restore-statements-transact-sql.md).  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
> [!NOTE]  
>  If the backup and restore file paths are identical between the server instance that hosts the primary replica and every instance that hosts a secondary replica, you should be able create secondary replica databases by with [New Availability Group Wizard](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md), [Add Replica to Availability Group Wizard](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md), or [Add Database to Availability Group Wizard](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md).  
  
 **To prepare a secondary database**  
  
1.  Unless you already have a recent database backup of the primary database, create a new full or differential database backup. As a best practice, place this backup and any subsequent log backups onto the recommended network share.  
  
2.  Create at least one new log backup of the primary database.

   >[!NOTE]
   >A transaction log backup may not be required if a transaction log backup has not been previously captured on the database in the primary replica. Microsoft recommends taking a transaction log backup each time a new database is joined to the availability group. 
  
3.  On the server instance that hosts the secondary replica, restore the full database backup of the primary database (and optionally a differential backup) followed by any subsequent log backups.  
  
     On the **RESTORE DATABASE Options** page, select **Leave the database non-operational, and do not roll back the uncommitted transactions. Additional transaction logs can be restored. (RESTORE WITH NORECOVERY)**.  
  
     If the file paths of the primary database and the secondary database differ, for example, if the primary database is on drive 'F:' but the server instance that hosts the secondary replica lacks an F: drive, include the MOVE option in your WITH clause.  
  
4.  To complete configuration of the secondary database, you need to join the secondary database to the availability group. For more information, [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md).  
  
> [!NOTE]  
>  For information about how to perform these backup and restore operations, see [Related Backup and Restore Tasks](#RelatedTasks), later in this section.  
  
###  <a name="RelatedTasks"></a> Related Backup and Restore Tasks  
 **To create a database backup**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](../../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md)  
  
 **To create a log backup**  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
 **To restore backups**  
  
-   [Restore a Database Backup Using SSMS](../../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md)  
  
-   [Restore a Differential Database Backup &#40;SQL Server&#41;](../../../relational-databases/backup-restore/restore-a-differential-database-backup-sql-server.md)  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)  
  
-   [Restore a Database to a New Location &#40;SQL Server&#41;](../../../relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md)  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To prepare a secondary database**  
  
> [!NOTE]  
>  For an example of this procedure, see [Example (Transact-SQL)](#ExampleTsql), earlier in this topic.  
  
1.  Unless you have a recent full backup of the primary database, connect to the server instance that hosts the primary replica and create a full database backup. As a best practice, place this backup and any subsequent log backups onto the recommended network share.  
  
2.  On the server instance that hosts the secondary replica, restore the full database backup of the primary database (and optionally a differential backup) followed by all subsequent log backups. Use WITH NORECOVERY for every restore operation.  
  
     If the file paths of the primary database and the secondary database differ, for example, if the primary database is on drive 'F:' but the server instance that hosts the secondary replica lacks an F: drive, include the MOVE option in your WITH clause.  
  
3.  If any log backups have been taken on the primary database since the required log backup, you must also copy these to the server instance that hosts the secondary replica and apply each of those log backups to the secondary database, starting with the earliest and always using RESTORE WITH NORECOVERY.  
  
    > [!NOTE]  
    >  A log backup would not exist if the primary database has just been created and no log backup has been taken yet or if the recovery model has just been changed from simple to full.  
  
4.  To complete configuration of the secondary database, you need to join the secondary database to the availability group. For more information, [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md).  
  
> [!NOTE]  
>  For information about how to perform these backup and restore operations, see [Related Backup and Restore Tasks](#RelatedTasks), later in this topic.  
  
###  <a name="ExampleTsql"></a> Transact-SQL Example  
 The following example prepares a secondary database. This example uses the [!INCLUDE[ssSampleDBobject](../../../includes/sssampledbobject-md.md)] sample database, which uses the simple recovery model by default.  
  
1.  To use the [!INCLUDE[ssSampleDBobject](../../../includes/sssampledbobject-md.md)] database, modify it to use the full recovery model:  
  
    ```  
    USE master;  
    GO  
    ALTER DATABASE MyDB1   
    SET RECOVERY FULL;  
    GO  
    ```  
  
2.  After modifying the recovery model of the database from SIMPLE to FULL, create a full backup, which can be used to create the secondary database. Because the recovery model has just been changed, the WITH FORMAT option is specified to create a new media set. This is useful to separate the backups under the full recovery model from any previous backups made under the simple recovery model. For the purpose of this example, the backup file (C:\\[!INCLUDE[ssSampleDBobject](../../../includes/sssampledbobject-md.md)].bak) is created on the same drive as the database.  
  
    > [!NOTE]  
    >  For a production database, you should always back up to a separate device.  
  
     On the server instance that hosts the primary replica (`INSTANCE01`), create a full backup of the primary database as follows:  
  
    ```  
    BACKUP DATABASE MyDB1   
        TO DISK = 'C:\MyDB1.bak'   
        WITH FORMAT  
    GO  
    ```  
  
3.  Copy the full backup to the server instance that hosts the secondary replica.  
  
4.  Restore the full backup, using RESTORE WITH NORECOVERY, onto the server instance that hosts the secondary replica. The restore command depends on whether the paths of primary and secondary databases are identical.  
  
    -   **If the paths are identical:**  
  
         On the computer that hosts the secondary replica, restore the full backup as follows:  
  
        ```  
        RESTORE DATABASE MyDB1   
            FROM DISK = 'C:\MyDB1.bak'   
            WITH NORECOVERY  
        GO  
        ```  
  
    -   **If the paths differ:**  
  
         If the path of the secondary database differs from the path of the primary database (for instance, their drive letters differ), creating the secondary database requires that the restore operation include a MOVE clause.  
  
        > [!IMPORTANT]  
        >  If the path names of the primary and secondary databases differ, you cannot add a file. This is because on receiving the log for the add file operation, the server instance of the secondary replica attempts to place the new file in the same path as used by the primary database.  
  
         For example, the following command restores a backup of a primary database that resides in the data directory of the default instance of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA. The restore database operation must move the database to the data directory of a remote instance of [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] named  (*Always On1*), which hosts the secondary replica on another cluster node. There, the data and log files are restored to the *C:\Program Files\Microsoft SQL Server\MSSQL13.Always On1\MSSQL\DATA* directory . The restore operation uses WITH NORECOVERY, to leave the secondary database in the restoring database.  
  
        ```  
        RESTORE DATABASE MyDB1  
          FROM DISK='C:\MyDB1.bak'  
         WITH NORECOVERY,   
            MOVE 'MyDB1_Data' TO   
             'C:\Program Files\Microsoft SQL Server\MSSQL13.Always On1\MSSQL\DATA\MyDB1_Data.mdf',   
            MOVE 'MyDB1_Log' TO  
             'C:\Program Files\Microsoft SQL Server\MSSQL13.Always On1\MSSQL\DATA\MyDB1_Data.ldf';  
        GO  
        ```  
  
5.  After you restore the full backup, you must create a log backup on the primary database. For example, the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement backs up the log to the a backup file named *E:\MyDB1_log.bak*:  
  
    ```  
    BACKUP LOG MyDB1   
      TO DISK = 'E:\MyDB1_log.trn'   
    GO  
    ```  
  
6.  Before you can join the database to the secondary replica, you must apply the required log backup (and any subsequent log backups).  
  
     For example, the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement restores the first log from *C:\MyDB1.bak*:  
  
    ```  
    RESTORE LOG MyDB1   
      FROM DISK = 'E:\MyDB1_log.trn'   
        WITH FILE=1, NORECOVERY  
    GO  
    ```  
  
7.  If any additional log backups occur before the database joins the secondary replica, you must also restore all of those log backups, in sequence, to the server instance that hosts the secondary replica using RESTORE WITH NORECOVERY.  
  
     For example, the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement restores two additional logs from *E:\MyDB1_log.bak*:  
  
    ```  
    RESTORE LOG MyDB1   
      FROM DISK = 'E:\MyDB1_log.bak'   
        WITH FILE=2, NORECOVERY  
    GO  
    RESTORE LOG MyDB1   
      FROM DISK = 'E:\MyDB1_log.bak'   
        WITH FILE=3, NORECOVERY  
    GO  
    ```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
 **To prepare a secondary database**  
  
1.  If you need to create a recent backup of the primary database, change directory (**cd**) to the server instance that hosts the primary replica.  
  
2.  Use the **Backup-SqlDatabase** cmdlet to create each of the backups.  
  
3.  Change directory (**cd**) to the server instance that hosts the secondary replica.  
  
4.  To restore the database and log backups of each primary database, use the **restore-SqlDatabase** cmdlet, specifying the **NoRecovery** restore parameter. If the file paths differ between the computers that host the primary replica and the target secondary replica, also use the **RelocateFile** restore parameter.  
  
    > [!NOTE]  
    >  To view the syntax of a cmdlet, use the **Get-Help** cmdlet in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell environment. For more information, see [Get Help SQL Server PowerShell](../../../relational-databases/scripting/get-help-sql-server-powershell.md).  
  
5.  To complete configuration of the secondary database, you need to join it to the availability group. For more information, [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md).  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
###  <a name="ExamplePSscript"></a> Sample backup and restore script and command  
 The following PowerShell commands back up a full database backup and transaction log to a network share and restore those backups from that share. This example assumes that the file path to which the database is restored is the same as the file path on which the database was backed up.  
  
```  
# Create database backup  
Backup-SqlDatabase -Database "MyDB1" -BackupFile "\\share\backups\MyDB1.bak" -ServerInstance "SourceMachine\Instance"  
# Create log backup  
Backup-SqlDatabase -Database "MyDB1" -BackupAction "Log" -BackupFile "\\share\backups\MyDB1.trn" -ServerInstance "SourceMachine\Instance"  
# Restore database backup   
Restore-SqlDatabase -Database "MyDB1" -BackupFile "\\share\backups\MyDB1.bak" -NoRecovery -ServerInstance "DestinationMachine\Instance"  
# Restore log backup   
Restore-SqlDatabase -Database "MyDB1" -BackupFile "\\share\backups\MyDB1.trn" -RestoreAction "Log" -NoRecovery -ServerInstance "DestinationMachine\Instance"  
  
```  
  
##  <a name="FollowUp"></a> Next steps  
 To complete configuration of the secondary database, join the newly restored database to the availability group. For more information, see [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md).  
  
## See also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../../t-sql/statements/backup-transact-sql.md)   
 [RESTORE Arguments &#40;Transact-SQL&#41;](../../../t-sql/statements/restore-statements-arguments-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../../t-sql/statements/restore-statements-transact-sql.md)   
 [Troubleshoot a Failed Add-File Operation &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
  
  
