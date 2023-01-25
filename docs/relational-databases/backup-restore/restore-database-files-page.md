---
title: "Restore Database (Files Page) | Microsoft Docs"
description: While restoring a database in SQL Server, use the Files page of the Restore Database dialog box to manage specific files to restore within the database.
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.restoredb.files.f1"
  - "sql13.swb.restoredb.files.f1 in the code"
ms.assetid: 714c36ea-a9f9-43a4-99f9-a6f73d1baf8e
author: MashaMSFT
ms.author: mathoma
---
# Restore Database (Files Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Files** page of the **Restore Database** dialog box to manage the specific files you have chosen to restore within the database.  
  
## Options  
  
### Restore database files as  
 Use to assign and manage the new file path to the restored files.  
  
 **Relocate all files to folder**  
 Relocates restored files.  
  
|Option|Description|  
|------------|-----------------|  
|**Data file folder**|Enter or search for the data file folder name that the restored data file or files should be relocated to.|  
|**Log file folder**|Enter or search for the log file or files folder that the restored log file should be relocated to.|  
  
 **Logical File Name**  
 Displays one row for each database file to be restored.  
  
 **File Type**  
 Displays the file type.  
  
 **Original File Name**  
 Displays the original file path for the restored file.  
  
 **Restore As**  
 Lists the file names that the restored files are to be saved as. Enter or search for the appropriate file name.  
  
## See Also  
 [Restore Database &#40;General Page&#41;](../../relational-databases/backup-restore/restore-database-general-page.md)   
 [Restore Database &#40;Options Page&#41;](../../relational-databases/backup-restore/restore-database-options-page.md)   
 [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md)   
 [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-tape-drive-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
  
