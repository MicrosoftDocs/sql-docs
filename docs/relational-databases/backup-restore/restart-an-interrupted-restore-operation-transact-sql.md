---
title: "Restart an Interrupted Restore Operation (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "interrupted restore operation"
  - "restoring databases [SQL Server], restarting interrupted operation"
  - "resetting options changed after backup"
  - "database restores [SQL Server], restarting interrupted operation"
  - "restarting interrupted restore operation"
  - "restoring interrupted operation [SQL Server]"
ms.assetid: 6413a07d-fd90-448d-8f29-12c5a1972618
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restart an Interrupted Restore Operation (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic explains how to restart an interrupted restore operation.  
  
### To restart an interrupted restore operation  
  
1.  Execute the interrupted RESTORE statement again, specifying:  
  
    -   The same clauses used in the original RESTORE statement.  
  
    -   The RESTART clause.  
  
## Example  
 This example restarts an interrupted restore operation.  
  
```sql  
-- Restore a full database backup of the AdventureWorks database.  
RESTORE DATABASE AdventureWorks  
   FROM DISK = 'C:\AdventureWorks.bck'  
GO  
-- The restore operation halted prematurely.  
-- Repeat the original RESTORE statement specifying WITH RESTART.  
RESTORE DATABASE AdventureWorks   
   FROM DISK = 'C:\AdventureWorks.bck'  
   WITH RESTART  
GO  
```  
  
## See Also  
 [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md)   
 [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
  
