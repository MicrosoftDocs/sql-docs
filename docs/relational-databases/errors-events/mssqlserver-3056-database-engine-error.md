---
description: "MSSQLSERVER_3056"
title: "MSSQLSERVER_3056 | Microsoft Docs"
ms.custom: ""
ms.date: "08/24/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3056 (Database Engine error)"
author: Pijocoder
ms.author: mathoma
---
# MSSQLSERVER_3056
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3056|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DMPDB_INVALID_FSDATA|  
|Message Text|The backup operation has detected an unexpected file in a FILESTREAM container. The backup operation will continue and include file '%ls'.|  
  
## Explanation

Error 3056 is raised if files exist under the FILESTREAM container (folder) that aren't created by SQL Server. The backup operation will include that file, but this will cause an inconsistent state of the filestream components in the database.

>[!WARNING]
>The FILESTREAM container is a folder managed by SQL Server. Do not add or remove files in the FILESTREAM folder

## User Action  

The error message includes the name of the unexpected file. Investigate how this file ended up in this folder.

1. Terminate your backups and ensure previous backups for this database aren't overwritten or lost
1. Run a full DBCC CHECKB against the database for which the backup failed with error 3056. Don't use with **physical_only**
1. Review the DBCC CHECKB output thoroughly; errors might be detected during different phases and be hundreds of lines apart for the same objects 
   - The last lines of CHECKDB output will give a summary count of errors. Be sure you've located the individual message for each of the counted errors
   - Do you find an error similar to this one at the bottom: `CHECKDB found 1 allocation errors and 1 consistency errors in database 'AG_Filestream'.`
1. If the only error(s) reported are [7908](mssqlserver-7908-database-engine-error.md) or [7906](mssqlserver-7906-database-engine-error.md), then you can locate the actual files reported in the error. The errors may look like this for example:

   ```output 
   Msg 7906, Level 16, State 1, Line 8
   Database error: The file "\782fc3bb-dc63-4ab8-9de6-e9dfa36454d2\NO_USER_FILE_SHOULD_BE_HERE.txt" is not a valid FILESTREAM file in container ID 65537.
   Msg 7908, Level 16, State 1, Line 8
   Table error: The file "\782fc3bb-dc63-4ab8-9de6-e9dfa36454d2\NO_USER_FILE_SHOULD_BE_HERE.txt" in the rowset directory ID 3068163f-7398-4ae7-843c-67672e29c37e is not a valid FILESTREAM file in container ID    65537.
   ```

   > [!NOTE]
   > It is recommended that you test these steps on a backup/test copy of your database before you attempt them on the production system.

1. To locate the files, run this command to find the physical folder of the FILESTREAM group

   ```sql
   SELECT name, physical_name, state_desc, type_desc 
   FROM sys.database_files
   WHERE type_desc = 'FILESTREAM'
   ```

1. In Windows Explorer, open the subfolder identified in the 7906 or 7908 error (for example \782fc3bb-dc63-4ab8-9de6-e9dfa36454d2)
1. Then find the file identified in the error message (for example NO_USER_FILE_SHOULD_BE_HERE.txt), and make a copy of this file to a temporary directory as a backup.
1. Once you ensure you have a copy, you can remove the file from the folder

1. Take steps to understand why and how this file(s) got added in this system folder and take steps to prevent further occurrence
   - Ensure proper permissions are in place for user access to this FILESTREAM folder(s)
   - Understand and ensure no applications are creating files in the FILESTREAM folder(s)

1. Run a new DBCC CHECKDB and make sure it doesn't raise any errors
1. If there are any other CHECKDB errors including 7903,7904,7905,7907, then there has been corruption or tampering of SQL Server FILESTREAM folder  beyond the mere invalid addition of foreign files. This situation not repairable manually
   - Check your hardware for any issues and resolve them
   - Ensure your system is protected from malware. Note that FILESTREAM data files should be excluded from antivirus software scanning. See [Recommendations and guidelines for improving FILESTREAM performance](../blob/filestream-sql-server.md#recommendations-and-guidelines-for-improving-filestream-performance)
   - Then restore from healthy database backup

### Run DBCC CHECKDB

If you run DBCC CHECKDB it may report error [7908](mssqlserver-7908-database-engine-error.md) or [7906](mssqlserver-7906-database-engine-error.md), but can't repair it.

### Restore from Backup

If the problem isn't hardware related and a known clean backup is available, restore the database from the backup