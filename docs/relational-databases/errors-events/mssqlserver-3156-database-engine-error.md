---
description: "MSSQLSERVER_3156"
title: "MSSQLSERVER_3156 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3156 (Database Engine error)"
ms.assetid: 345d8ed4-177e-4ec3-bab3-25d30000e323
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_3156
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|3156|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LDDB_CANT_WRITE|  
|Message Text|File '%ls' cannot be restored to '%ls'. Use WITH MOVE to identify a valid location for the file.|  
  
## Explanation  
This general message identifies the logical or physical file names of files that could not be restored because of a problem with the specified location.  
  
### Possible Causes  
The possible causes include the following:  
  
-   You might need access to the specified Windows directory.  
  
-   You might have mistyped the path or specified a path that does not exist.  
  
-   The file name might be being used by a file that cannot be overwritten.  
  
## User Action  
Look at the error logs for other messages that provide more details.  
  
Correct the problem with the specified location, for example, by granting access, or use the WITH MOVE option in your RESTORE statement to relocate the file.  
  
## See Also  
[Restore a Database to a New Location &#40;SQL Server&#41;](~/relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md)  
[Restore Files to a New Location &#40;SQL Server&#41;](~/relational-databases/backup-restore/restore-files-to-a-new-location-sql-server.md)  
[RESTORE &#40;Transact-SQL&#41;](~/t-sql/statements/restore-statements-transact-sql.md)  
  
