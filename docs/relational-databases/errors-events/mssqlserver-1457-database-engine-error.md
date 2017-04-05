---
title: "MSSQLSERVER_1457 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "1457 (Database Engine error)"
ms.assetid: 28434ba1-b033-4866-ab41-111fccef45a2
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_1457
  
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
[Database Mirroring &#40;SQL Server&#41;](../Topic/Database%20Mirroring%20(SQL%20Server).md)  
[ALTER DATABASE &#40;Transact-SQL&#41;](../Topic/ALTER%20DATABASE%20(Transact-SQL).md)  
[Setting Up Database Mirroring &#40;SQL Server&#41;](../Topic/Setting%20Up%20Database%20Mirroring%20(SQL%20Server).md)  
[Removing Database Mirroring &#40;SQL Server&#41;](../Topic/Removing%20Database%20Mirroring%20(SQL%20Server).md)  
[Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../Topic/Prepare%20a%20Mirror%20Database%20for%20Mirroring%20(SQL%20Server).md)  
  
