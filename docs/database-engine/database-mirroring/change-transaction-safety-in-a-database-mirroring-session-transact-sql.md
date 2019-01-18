---
title: "Change Transaction Safety in a Database Mirroring Session (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "transaction safety [SQL Server database mirroring]"
ms.assetid: 8b03bb82-8589-4558-8545-9942fe008391
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Change Transaction Safety in a Database Mirroring Session (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Transaction safety is the attribute that controls the operating mode of the session. At any time, however, the database owner can change the transaction safety. By default, the level of transaction safety is set to FULL (synchronous operating mode).  
  
 Turning off transaction safety shifts the session into asynchronous operating mode, which maximizes performance. If the principal becomes unavailable, the mirror stops but is available as a warm standby (failover requires forcing service with possible data loss).  
  
### To turn on transaction safety  
  
1.  Connect to the principal server.  
  
2.  Issue the following Transact-SQL statement:  
  
    ```  
    ALTER DATABASE <database> SET PARTNER SAFETY FULL  
    ```  
  
     where *\<database>* is the name of the mirrored database.  
  
### To turn off transaction safety  
  
1.  Connect to the principal server.  
  
2.  Issue the following statement:  
  
    ```  
    ALTER DATABASE <database> SET PARTNER SAFETY OFF  
    ```  
  
     where *\<database>* is the mirrored database.  
  
## See Also  
 [ALTER DATABASE Database Mirroring &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)   
 [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md)  
  
  
