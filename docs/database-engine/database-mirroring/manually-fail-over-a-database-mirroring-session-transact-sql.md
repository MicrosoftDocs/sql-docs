---
title: "Manually fail over a database mirror to partner"
description: "Instructions to manually fail over a principle database mirror to a secondary partner using Transact-SQL (T-SQL)."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "failover [SQL Server], database mirroring"
  - "manual failover [SQL Server]"
  - "database mirroring [SQL Server], failover"
---
# Manually Fail Over a Database Mirroring Session (Transact-SQL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  When the mirrored database is synchronized (that is, when the database is in the SYNCHRONIZED state), the database owner can initiate manual failover to the mirror server. Manual failover can be initiated only from the principal server.  
  
### To manually fail over a database mirroring session  
  
1.  Connect to the principal server.  
  
2.  Set the database context to the **master** database:  
  
     **USE master;**  
  
3.  Issue the following statement on the principal server:  
  
     [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md) *database_name* SET PARTNER FAILOVER, where *database_name* is the mirrored database.  
  
     This initiates an immediate transition of the mirror server to the principal role.  
  
 On the former principal, clients are disconnected from the database and in-flight transactions are rolled back.  
  
> [!NOTE]  
>  Transactions that have been prepared by using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator but are still not committed when a failover occurs are considered aborted after the database has failed over.  
  
## See Also  
 [ALTER DATABASE Database Mirroring &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)   
 [Manually Fail Over a Database Mirroring Session &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/manually-fail-over-a-database-mirroring-session-sql-server-management-studio.md)   
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)  
  
  
