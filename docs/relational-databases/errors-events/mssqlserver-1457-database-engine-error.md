---
title: "MSSQLSERVER_1457 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "1457 (Database Engine error)"
ms.assetid: 28434ba1-b033-4866-ab41-111fccef45a2
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_1457
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1457|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_PAGE_UNDO_PENDING|  
|Message Text|Synchronization of the mirror database, '%.*ls', was interrupted, leaving the database in an inconsistent state. The ALTER DATABASE command failed. Ensure that the mirror database is back up and online, and then reconnect the mirror server instance and allow the mirror database to finish synchronizing.|  
  
## Explanation  
This messages indicates that the ALTER DATABASE *database_name* SET PARTNER OFF statement has failed. The failed attempt to remove database mirroring interrupted synchronization of the mirror database. The database is in an inconsistent state.  
  
## User Action  
To resolve this error, you can take either of the following actions:  
  
-   Restore contact between the mirror server and the principal server to allow for the mirror database to synchronize.  
  
-   Drop the mirror database.  
  
    After you drop the mirror database, you can create a new mirror database from backups.  
  
    > [!NOTE]  
    > You can drop the mirror database when mirroring is still enabled only after a failed SET PARTNER OFF statement.  
  
## See Also  
[Database Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/database-mirroring-sql-server.md)  
[ALTER DATABASE &#40;Transact-SQL&#41;](~/t-sql/statements/alter-database-transact-sql-set-options.md)  
[Setting Up Database Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md)  
[Removing Database Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/removing-database-mirroring-sql-server.md)  
[Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md)  
  
