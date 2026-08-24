---
title: "MSSQLSERVER_3181"
description: "MSSQLSERVER_3181"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3181 (Database Engine error)"
---
# MSSQLSERVER_3181
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|3181|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LDDB_STORAGE_VERIFY|  
|Message Text|Attempting to restore this backup may encounter storage space problems. Subsequent messages will provide details.|  
  
## Explanation  
The RESTORE VERIFYONLY statement checks the available storage space on the disk to which the database is to be restored.  
  
### Possible Causes  
The available disk space may be insufficient to restore the backup being verified.  
  
## User Action  
Restore the backup to a location with sufficient disk space, or provide more space on the disk.  
  
## Related content

- [Restore a database to a new location (SQL Server)](../backup-restore/restore-a-database-to-a-new-location-sql-server.md)
- [Restore Files to a New Location (SQL Server)](../backup-restore/restore-files-to-a-new-location-sql-server.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [RESTORE Statements - VERIFYONLY (Transact-SQL)](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)
