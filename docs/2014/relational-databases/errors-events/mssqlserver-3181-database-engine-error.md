---
title: "MSSQLSERVER_3181 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "3181 (Database Engine error)"
ms.assetid: e6d2e716-5263-477c-ad0e-7bcbfef4c1f3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3181
    
## Details  
  
|||  
|-|-|  
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
 [Restore a Database to a New Location &#40;SQL Server&#41;](../backup-restore/restore-a-database-to-a-new-location-sql-server.md)   
 [Restore Files to a New Location &#40;SQL Server&#41;](../backup-restore/restore-files-to-a-new-location-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)   
 [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-verifyonly-transact-sql)  
  
  
