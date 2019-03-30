---
title: "Copy-Only Backups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2018"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "copy-only backups [SQL Server]"
  - "COPY_ONLY option [BACKUP statement]"
  - "backups [SQL Server], copy-only backups"
ms.assetid: f82d6918-a5a7-4af8-868e-4247f5b00c52
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# Copy-Only Backups (SQL Server)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]
  A *copy-only backup* is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup that is independent of the sequence of conventional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups. Usually, taking a backup changes the database and affects how later backups are restored. However, occasionally, it is useful to take a backup for a special purpose without affecting the overall backup and restore procedures for the database. Copy-only backups serve this purpose.  
  
 The types of copy-only backups are as follows:  
  
-   Copy-only full backups (all recovery models)  
  
     A copy-only backup cannot serve as a differential base or differential backup and does not affect the differential base.  
  
     Restoring a copy-only full backup is the same as restoring any other full backup.  
  
-   Copy-only log backups (full recovery model and bulk-logged recovery model only)  
  
     A copy-only log backup preserves the existing log archive point and, therefore, does not affect the sequencing of regular log backups. Copy-only log backups are typically unnecessary. Instead, you can create a new routine log backup (using WITH NORECOVERY) and use that backup together with any previous log backups that are required for the restore sequence. However, a copy-only log backup can sometimes be useful for performing an online restore. For an example of this, see [Example: Online Restore of a Read-Write File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-write-file-full-recovery-model.md).  
  
     The transaction log is never truncated after a copy-only backup.  
  
 Copy-only backups are recorded in the **is_copy_only** column of the [backupset](../../relational-databases/system-tables/backupset-transact-sql.md) table.  
  
## To Create a Copy-Only Backup  
 You can create a copy-only backup by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or PowerShell.  

### Examples  
###  <a name="SSMSProcedure"></a> A.  Using SQL Server Management Studio  
In this example, a copy-only backup of the `Sales` database will be backed up to disk at the default backup location.

1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

2.  Expand **Databases**, right-click `Sales`, point to **Tasks**, and then click **Back Up...**.

3.  On the **General** page in the **Source** section check the **Copy-only backup** checkbox.

4.  Click **OK**.

  
###  <a name="TsqlProcedure"></a>B.  Using Transact-SQL  
This example creates a copy-only backup for the `Sales` database utilizing the COPY_ONLY parameter.  A copy-only backup of the transaction log is taken as well.

```sql
BACKUP DATABASE Sales
TO DISK = 'E:\BAK\Sales_Copy.bak'
WITH COPY_ONLY;

BACKUP LOG Sales
TO DISK = 'E:\BAK\Sales_LogCopy.trn'
WITH COPY_ONLY;
```
  
> [!NOTE]  
> COPY_ONLY has no effect when specified with the DIFFERENTIAL option.  

  
###  <a name="PowerShellProcedure"></a>C.  Using PowerShell  
This example creates a copy-only backup for the `Sales` database utilizing the -CopyOnly parameter.  
```powershell
Backup-SqlDatabase -ServerInstance 'SalesServer' -Database 'Sales' -BackupFile 'E:\BAK\Sales_Copy.bak' -CopyOnly
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To create a full or log backup**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
 **To view copy-only backups**  
  
-   [backupset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupset-transact-sql.md)  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
  
## See Also  
 [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md)   
 [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)   
 [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md)  
[BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)  
[Backup-SqlDatabase](/powershell/module/sqlserver/backup-sqldatabase)

  
