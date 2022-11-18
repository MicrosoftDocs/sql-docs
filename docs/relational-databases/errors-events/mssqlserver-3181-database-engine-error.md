---
description: "MSSQLSERVER_3181"
title: "MSSQLSERVER_3181 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3181 (Database Engine error)"
ms.assetid: e6d2e716-5263-477c-ad0e-7bcbfef4c1f3
author: MashaMSFT
ms.author: mathoma
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
  
## See Also  
[Restore a Database to a New Location &#40;SQL Server&#41;](~/relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md)  
[Restore Files to a New Location &#40;SQL Server&#41;](~/relational-databases/backup-restore/restore-files-to-a-new-location-sql-server.md)  
[RESTORE &#40;Transact-SQL&#41;](~/t-sql/statements/restore-statements-transact-sql.md)  
[RESTORE VERIFYONLY &#40;Transact-SQL&#41;](~/t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
