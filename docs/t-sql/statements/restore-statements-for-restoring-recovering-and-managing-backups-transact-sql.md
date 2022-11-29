---
title: Restoring, recovering, and managing backups
description: Transact-SQL RESTORE statements for restoring, recovering, and managing backups.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/30/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: seo-lt-2019​
helpviewer_keywords:
  - "restoring files [SQL Server], RESTORE statement"
  - "RESTORE statement, about restore operations"
  - "database restores [SQL Server], RESTORE statement"
  - "restoring databases [SQL Server], RESTORE statement"
  - "database backups [SQL Server], RESTORE statement"
  - "backing up databases [SQL Server], RESTORE statement"
  - "file restores [SQL Server], RESTORE statement"
  - "transaction log backups [SQL Server], RESTORE statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017"
---
# RESTORE Statements for Restoring, Recovering, and Managing Backups (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)]

  This section describes the RESTORE statements for backups. In addition to the main RESTORE {DATABASE | LOG} statement for restoring and recovering backups, a number of auxiliary RESTORE statements help you manage your backups and plan your restore sequences. The auxiliary RESTORE commands include: RESTORE FILELISTONLY, RESTORE HEADERONLY, RESTORE LABELONLY, RESTORE REWINDONLY, and RESTORE VERIFYONLY.  
  
> [!IMPORTANT]  
>  In previous versions of SQL Server, any user could obtain information about backup sets and backup devices by using the RESTORE FILELISTONLY, RESTORE HEADERONLY, RESTORE LABELONLY, and RESTORE VERIFYONLY Transact-SQL statements. Because they reveal information about the content of the backup files, in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions these statements require CREATE DATABASE permission. This requirement secures your backup files and protects your backup information more fully than in previous versions. For information about this permission, see [GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md).  
  
## In This Section  
  
|Statement|Description|  
|---------------|-----------------|  
|[RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)|Describes the RESTORE DATABASE and RESTORE LOG Transact-SQL statements used to restore and recover a database from backups taken using the BACKUP command. RESTORE DATABASE is used for databases under all recovery models. RESTORE LOG is used only under the full and bulk-logged recovery models. RESTORE DATABASE can also be used to revert a database to a database snapshot.|  
|[RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md)|Documents the arguments described in the "Syntax" sections of the RESTORE statement and of the associated set of auxiliary statements: RESTORE FILELISTONLY, RESTORE HEADERONLY, RESTORE LABELONLY, RESTORE REWINDONLY, and RESTORE VERIFYONLY. Most of the arguments are supported by only a subset of these six statements. The support for each argument is indicated in the description of the argument.|  
|[RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)|Describes the RESTORE FILELISTONLY Transact-SQL statement, which is used to return a result set containing a list of the database and log files contained in the backup set.|  
|[RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)|Describes the RESTORE HEADERONLY Transact-SQL statement, which is used to return a result set containing all the backup header information for all backup sets on a particular backup device.|  
|[RESTORE LABELONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)|Describes the RESTORE LABELONLY Transact-SQL statement, which is used to return a result set containing information about the backup media identified by the given backup device.|  
|[RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)|Describes the RESTORE REWINDONLY Transact-SQL statement, which is used to rewind and close tape devices that were left open by BACKUP or RESTORE statements executed with the NOREWIND option.|  
|[RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)|Describes the RESTORE VERIFYONLY Transact-SQL statement, which is used to verify the backup but does not restore it, and checks to see that the backup set is complete and the entire backup is readable; does not attempt to verify the structure of the data.|  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)  
  
  
