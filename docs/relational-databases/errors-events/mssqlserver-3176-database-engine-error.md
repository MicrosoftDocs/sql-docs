---
description: "MSSQLSERVER_3176"
title: "MSSQLSERVER_3176 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3176 (Database Engine error)"
ms.assetid: 4be24c64-2d52-4cb4-b4d7-36efbe4555b6
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_3176
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|3176|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LDDB_FILE_CLAIM|  
|Message Text|File '%ls' is claimed by '%ls'(%d) and '%ls'(%d). The WITH MOVE clause can be used to relocate one or more files.|  
  
## Explanation  
Attempting to use a file for more than one purpose.  
  
### Possible Causes  
Another database is already using the file name.  
  
## User Action  
Restore the database files to a different location. In a RESTORE statement, use a WITH MOVE clause to move each file. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], change the file locations in the **Restore the database files as** grid of the **Restore Database Options** dialog box.  
  
## See Also  
[Restore a Database to a New Location &#40;SQL Server&#41;](~/relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md)  
[Restore Files to a New Location &#40;SQL Server&#41;](~/relational-databases/backup-restore/restore-files-to-a-new-location-sql-server.md)  
[RESTORE &#40;Transact-SQL&#41;](~/t-sql/statements/restore-statements-transact-sql.md)  
  
