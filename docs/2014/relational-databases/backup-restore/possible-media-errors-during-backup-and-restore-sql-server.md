---
title: "Possible Media Errors During Backup and Restore (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "media errors [SQL Server]"
  - "CONTINUE_AFTER_ERROR option"
  - "errors [SQL Server], backups"
  - "backups [SQL Server], errors"
  - "RESTORE VERIFYONLY statement"
  - "backup media [SQL Server], error management"
  - "page checksums [SQL Server]"
  - "backup checksums [SQL Server]"
  - "backing up [SQL Server], media errors"
  - "RESTORE statement, media errors"
  - "NO_CHECKSUM option"
  - "checksums [SQL Server]"
ms.assetid: 83a27b29-1191-4f8d-9648-6e6be73a9b7c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Possible Media Errors During Backup and Restore (SQL Server)
  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] gives you the option of recovering a database despite detected errors. An important new error-detection mechanism is the optional creation of a backup checksum that can be created by a backup operation and validated by a restore operation. You can control whether an operation checks for errors and whether the operation stops or continues on encountering an error. If a backup contains a backup checksum, RESTORE and RESTORE VERIFYONLY statements can check for errors.  
  
> [!NOTE]  
>  Mirrored backups provide up to four copies (mirrors) of a media set, providing alternative copies for recovering from errors caused by damaged media. For more information, see [Mirrored Backup Media Sets &#40;SQL Server&#41;](mirrored-backup-media-sets-sql-server.md).  
  
  
  
##  <a name="BckChecksums"></a> Backup Checksums  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports three types of checksums: a checksum on pages, a checksum in log blocks, and a backup checksum. When generating a backup checksum, BACKUP verifies that the data read from the database is consistent with any checksum or torn-page indication that is present in the database.  
  
 The BACKUP statement optionally computes a backup checksum on the backup stream; if page-checksum or torn-page information is present on a given page, when backing up the page, BACKUP also verifies the checksum and torn-page status and the page ID, of the page. When creating a backup checksum, a backup operation does not add any checksums to pages. Pages are backed up as they exist in the database, and the pages are unmodified by backup.  
  
 Due to the overhead verifying and generating backup checksums, using backup checksums poses a potential performance impact. Both the workload and the backup throughput may be affected. Therefore, using backup checksums is optional. When deciding to generate checksums during a backup, carefully monitor the CPU overhead incurred as well as the impact on any concurrent workload on the system.  
  
 BACKUP never modifies the source page on disk nor the contents of a page.  
  
 When backup checksums are enabled, a backup operation performs the following steps:  
  
1.  Before writing a page to the backup media, the backup operation verifies the page-level information (page checksum or torn page detection), if either exists. If neither exists, backup cannot verify the page. Unverified the pages are included as is, and their contents are added to the overall backup checksum.  
  
     If the backup operation encounters a page error during verification, the backup fails.  
  
    > [!NOTE]  
    >  For more information about page checksums and torn page detection, see the PAGE_VERIFY option of the ALTER DATABASE statement. For more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options).  
  
2.  Regardless of whether page checksums are present, BACKUP generates a separate backup checksum for the backup streams. Restore operations can optionally use the backup checksum to validate that the backup is not corrupted. The backup checksum is stored on the backup media, not on the database pages. The backup checksum can optionally be used at restore time.  
  
3.  The backup set is flagged as containing backup checksums (in the **has_backup_checksums** column of **msdb..backupset)**. For more information, see [backupset &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/backupset-transact-sql).  
  
 During a restore operation, if backup checksums are present on the backup media, by default, both the RESTORE and RESTORE VERIFYONLY statements verify the backup checksums and page checksums. If there is no backup checksum, either restore operation proceeds without any verification; this is because without a backup checksum, restore cannot reliably verify page checksums.  
  
## Response to Page Checksum Errors During a Backup or Restore Operation  
 By default, after encountering a page checksum error, a BACKUP or RESTORE operation fails and a RESTORE VERIFYONLY operation continues. However, you can control whether a given operation fails on encountering an error or continues as best it can.  
  
 If a BACKUP operation continues after encountering errors, the operation performs the following steps:  
  
1.  Flags the backup set on the backup media as containing errors and tracks the page in the **suspect_pages** table in the **msdb** database. For more information, see [suspect_pages &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/suspect-pages-transact-sql).  
  
2.  Logs the error in the SQL Server error log.  
  
3.  Marks the backup set as containing this type of error (in the **is_damaged** column of **msdb.backupset)**. For more information, see [backupset &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/backupset-transact-sql).  
  
4.  Issues a message that the backup was successfully generated, but contains page errors.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To enable or disable backup checksums**  
  
-   [Enable or Disable Backup Checksums During Backup or Restore &#40;SQL Server&#41;](enable-or-disable-backup-checksums-during-backup-or-restore-sql-server.md)  
  
 **To control the response to a error during a backup operation**  
  
-   [Specify Whether a Backup or Restore Operation Continues or Stops After Encountering an Error &#40;SQL Server&#41;](specify-if-backup-or-restore-continues-or-stops-after-error.md)  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [backupset &#40;Transact-SQL&#41;](/sql/relational-databases/system-tables/backupset-transact-sql)   
 [Mirrored Backup Media Sets &#40;SQL Server&#41;](mirrored-backup-media-sets-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)   
 [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-verifyonly-transact-sql)  
  
  
