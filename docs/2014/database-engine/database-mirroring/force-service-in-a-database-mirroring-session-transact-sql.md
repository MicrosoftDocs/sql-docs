---
title: "Force Service in a Database Mirroring Session (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "forced service [SQL Server]"
  - "database mirroring [SQL Server], forcing service"
ms.assetid: 8b6ffe77-35f3-4e2a-a658-8a38a8e1c794
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Force Service in a Database Mirroring Session (Transact-SQL)
  In high-performance mode and high-safety mode without automatic failover, if the principal server fails while the mirror server is available, the database owner can make the database available by forcing service to fail over (with possible data loss) to the mirror database. This option is available only under all the following conditions:  
  
-   The principal server is down.  
  
-   WITNESS is set to OFF or is connected to the mirror server.  
  
> [!CAUTION]  
>  Forced service is strictly a disaster recovery method. Forcing service may involve some data loss. Therefore, force service only if you are willing to risk losing some data in order to restore service to the database immediately. If forcing service risks losing significant data, we recommend that you stop mirroring and manually resynchronize the databases. For more information about the risks of forcing service, see [Database Mirroring Operating Modes](database-mirroring-operating-modes.md).  
  
 Forcing service suspends the session and starts a new recovery fork. The effect of forcing service is similar to removing mirroring and recovering the former principal database. However, forcing service facilitates resynchronizing the databases (with possible data loss) when mirroring resumes.  
  
### To force service in a database mirroring session  
  
1.  Connect to the mirror server.  
  
2.  Issue the following statement:  
  
     ALTER DATABASE *<database_name>* SET PARTNER FORCE_SERVICE_ALLOW_DATA_LOSS  
  
     where *<database_name>* is the mirrored database.  
  
     The mirror server immediately transitions to principal server, and mirroring is suspended.  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [Database Mirroring Operating Modes](database-mirroring-operating-modes.md)  
  
  
