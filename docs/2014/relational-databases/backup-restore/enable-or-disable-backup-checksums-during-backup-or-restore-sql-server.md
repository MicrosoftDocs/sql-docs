---
title: "Enable or Disable Backup Checksums During Backup or Restore (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "backup checksums [SQL Server]"
  - "disabling checksums"
  - "checksums [SQL Server]"
ms.assetid: 6786bd1e-ad97-430a-8dfb-d4ba952d6c4d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Enable or Disable Backup Checksums During Backup or Restore (SQL Server)
  This topic describes how to enable or disable backup checksums when you are backing up or restoring a database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To enable or disable backup checksums, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 BACKUP  
 BACKUP DATABASE and BACKUP LOG permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles.  
  
 Ownership and permission problems on the backup device's physical file can interfere with a backup operation. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to read and write to the device; the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service runs must have write permissions. However, [sp_addumpdevice](/sql/relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql), which adds an entry for a backup device in the system tables, does not check file access permissions. Such problems on the backup device's physical file may not appear until the physical resource is accessed when the backup or restore is attempted.  
  
 RESTORE  
 If the database being restored does not exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database (for the FROM DATABASE_SNAPSHOT option, the database always exists).  
  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To enable or disable checksums during a backup operation  
  
1.  Follow the steps to [create a database backup](create-a-full-database-backup-sql-server.md).  
  
2.  On the **Options** page, in the **Reliability** section, click **Perform checksum before writing to media**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To enable or disable backup checksum for a backup operation  
  
1.  Connect to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  To enable backup checksums in a [BACKUP](/sql/t-sql/statements/backup-transact-sql) statement, specify the WITH CHECKSUM option. To disable backup checksums, specify the WITH NO_CHECKSUM option. This is the default behavior, except for a compressed backup. The following example specifies that checksums be performed.  
  
```sql  
BACKUP DATABASE AdventureWorks2012   
 TO DISK = 'Z:\SQLServerBackups\AdvWorksData.bak'  
   WITH CHECKSUM;  
GO  
```  
  
#### To enable or disable backup checksum for a restore operation  
  
1.  Connect to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  To enable backup checksums in a [RESTORE](/sql/t-sql/statements/restore-statements-transact-sql) statement, specify the WITH CHECKSUM option. This is the default behavior for a compressed backup. To disable backup checksums, specify the WITH NO_CHECKSUM option. This is the default behavior, except for a compressed backup. The following example specifies that backup checksums be performed.  
  
```sql  
RESTORE DATABASE AdventureWorks2012   
 FROM DISK = 'Z:\SQLServerBackups\AdvWorksData.bak'  
   WITH CHECKSUM;  
GO  
```  
  
> [!WARNING]  
>  If you explicitly request CHECKSUM for a restore operation and if the backup contains backup checksums, backup checksums and page checksums are both verified, as in the default case. However, if the backup set lacks backup checksums, the restore operation fails with a message indicating that checksums are not present.  
  
## See Also  
 [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-filelistonly-transact-sql)   
 [RESTORE HEADERONLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-headeronly-transact-sql)   
 [RESTORE LABELONLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-labelonly-transact-sql)   
 [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-verifyonly-transact-sql)   
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [backupset &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/backupset-transact-sql)   
 [RESTORE Arguments &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-arguments-transact-sql)   
 [Possible Media Errors During Backup and Restore &#40;SQL Server&#41;](possible-media-errors-during-backup-and-restore-sql-server.md)   
 [Specify Whether a Backup or Restore Operation Continues or Stops After Encountering an Error &#40;SQL Server&#41;](specify-if-backup-or-restore-continues-or-stops-after-error.md)  
  
  
