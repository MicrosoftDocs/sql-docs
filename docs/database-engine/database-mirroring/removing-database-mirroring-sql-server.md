---
title: "Removing Database Mirroring (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], removing"
  - "stopping database mirroring [SQL Server]"
  - "removing database mirroring [SQL Server]"
ms.assetid: 40c72091-8f03-4037-8b55-5e95309fe145
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Removing Database Mirroring (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The database owner can manually stop a database mirroring session at any time, at either partner.  
  
## Impact of Removing Mirroring  
 When mirroring is removed, the following occurs:  
  
-   The relationship between the partners and between each partner and the witness breaks permanently, if any relationship exists.  
  
     If the partners are communicating with each other when the session is stopped, their relationship is immediately broken on both computers. If the partners are not communicating (the database is in a DISCONNECTED state at the time of stopping), the relationship is broken immediately on the partner from which mirroring is stopped; when the other partner tries to reconnect, it discovers that the database mirroring session has ended.  
  
-   Information about the mirroring session is dropped, unlike when pausing a session. Mirroring is removed on both the principal database and the mirror database. In **sys.databases**, the **mirroring_state** column and all other mirroring columns are set to NULL. For more information, see [sys.database_mirroring &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-transact-sql.md).  
  
-   Each partner server instance is left with a separate copy of the database.  
  
-   The mirror database is left in the RESTORING state (see the **state** column of **sys.databases**), because the mirror database was created by using RESTORE WITH NORECOVERY. At this point, you can drop the former mirror database or restore it using WITH RECOVERY. When you recover the database, it will have diverged from the former principal database because the recovery starts a new recovery fork.  
  
> [!NOTE]  
>  To continue mirroring after stopping a session, you must establish a new database mirroring session. If you create a log backup after stopping mirroring, you must apply it to the mirror database before restarting mirroring.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To remove database mirroring**  
  
-   [Remove Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-database-mirroring-sql-server.md)  
  
 **To start database mirroring**  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-establish-session-windows-authentication.md)  
  
  
## See Also  
 [ALTER DATABASE Database Mirroring &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Pausing and Resuming Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/pausing-and-resuming-database-mirroring-sql-server.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
  
