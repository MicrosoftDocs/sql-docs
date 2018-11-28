---
title: "Copy-Only Backups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
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
---
# Copy-Only Backups (SQL Server)
  A *copy-only backup* is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup that is independent of the sequence of conventional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups. Usually, taking a backup changes the database and affects how later backups are restored. However, occasionally, it is useful to take a backup for a special purpose without affecting the overall backup and restore procedures for the database. Copy-only backups serve this purpose.  
  
 The types of copy-only backups are as follows:  
  
-   Copy-only full backups (all recovery models)  
  
     A copy-only backup cannot serve as a differential base or differential backup and does not affect the differential base.  
  
     Restoring a copy-only full backup is the same as restoring any other full backup.  
  
-   Copy-only log backups (full recovery model and bulk-logged recovery model only)  
  
     A copy-only log backup preserves the existing log archive point and, therefore, does not affect the sequencing of regular log backups. Copy-only log backups are typically unnecessary. Instead, you can create a new routine log backup (using WITH NORECOVERY) and use that backup together with any previous log backups that are required for the restore sequence. However, a copy-only log backup can sometimes be useful for performing an online restore. For an example of this, see [Example: Online Restore of a Read-Write File &#40;Full Recovery Model&#41;](example-online-restore-of-a-read-write-file-full-recovery-model.md).  
  
     The transaction log is never truncated after a copy-only backup.  
  
 Copy-only backups are recorded in the **is_copy_only** column of the [backupset](/sql/relational-databases/system-tables/backupset-transact-sql) table.  
  
## To Create a Copy-Only Backup  
 You can create a copy-only backup by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell.  
  
###  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
1.  On the **General** page of the **Back Up Database** dialog box, select the **Copy Only Backup** option.  
  
###  <a name="TsqlProcedure"></a> Using Transact-SQL  
 The essential [!INCLUDE[tsql](../../../includes/tsql-md.md)] syntax is as follows:  
  
-   For a copy-only full backup:  
  
     BACKUP DATABASE *database_name* TO \<backup_device*>* ... WITH COPY_ONLY ...  
  
    > [!NOTE]  
    >  COPY_ONLY has no effect when specified with the DIFFERENTIAL option.  
  
-   For a copy-only log backup:  
  
     BACKUP LOG *database_name* TO *\<*backup_device*>* ... WITH COPY_ONLY ...  
  
###  <a name="PowerShellProcedure"></a> Using PowerShell  
  
1.  Use the `Backup-SqlDatabase` cmdlet with the `-CopyOnly` parameter.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To create a full or log backup**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)  
  
 **To view copy-only backups**  
  
-   [backupset &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/backupset-transact-sql)  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../powershell/sql-server-powershell-provider.md)  
  

  
## See Also  
 [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md)   
 [Recovery Models &#40;SQL Server&#41;](recovery-models-sql-server.md)   
 [Copy Databases with Backup and Restore](../databases/copy-databases-with-backup-and-restore.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](restore-and-recovery-overview-sql-server.md)  
  
  
