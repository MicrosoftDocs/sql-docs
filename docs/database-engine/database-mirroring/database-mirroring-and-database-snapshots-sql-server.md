---
title: "Database Mirroring and Database Snapshots (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], interoperability"
  - "snapshots [SQL Server database snapshots], database mirroring"
  - "database snapshots [SQL Server], database mirroring"
ms.assetid: 0bf1be90-7ce4-484c-aaa7-f8a782f57c5f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring and Database Snapshots (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can take advantage of a mirror database that you are maintaining for availability purposes to offload reporting. To use a mirror database for reporting, you can create a database snapshot on the mirror database and direct client connection requests to the most recent snapshot. A database snapshot is a static, read-only, transaction-consistent snapshot of its source database as it existed at the moment of the snapshot's creation. To create a database snapshot on a mirror database, the database must be in the synchronized mirroring state.  
  
 Unlike the mirror database itself, a database snapshot is accessible to clients. As long as the mirror server is communicating with the principal server, you can direct reporting clients to connect to a snapshot. Note that because a database snapshot is static, new data is not available. To make relatively recent data available to your users, you must create a new database snapshot periodically and have applications direct incoming client connections to the newest snapshot.  
  
 A new database snapshot is almost empty, but it grows over time as more and more database pages are updated for the first time. Because every snapshot on a database grows incrementally in this way, each database snapshot consumes as much resources as a normal database. Depending on the configurations of the mirror server and principal server, having an excessive number of database snapshots on a mirror database might decrease performance on the principal database. Therefore, we recommend that you keep only a few relatively recent snapshots on your mirror databases. Typically, after you create a replacement snapshot, you should redirect incoming queries to the new snapshot and drop the earlier snapshot after any current queries complete.  
  
> [!NOTE]  
>  For more information about database snapshots, see [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md).  
  
 If role switching occurs, the database and its snapshots are restarted, temporarily disconnecting users. Afterwards, the database snapshots remain on the server instance where they were created, which has become the new principal database. Users can continue to use the snapshots after the failover. However, this places an additional load on the new principal server. If performance is a concern in your environment, we recommend that you create a snapshot on the new mirror database when it becomes available, redirect clients to the new snapshot, and drop all of the database snapshots from the former mirror database.  
  
> [!NOTE]  
>  For a dedicated reporting solution that scales out well, consider replication. For more information, see [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md).  
  
## Example  
 This example creates snapshots on a mirrored database.  
  
 Assume that the database of a database mirroring session is [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]. This example creates three database snapshots on the mirror copy of the `AdventureWorks` database, which resides on the `F` drive. The snapshots are named `AdventureWorks_0600`, `AdventureWorks_1200`, and `AdventureWorks_1800` to identify their approximate creation times.  
  
1.  Create the first database snapshot on the mirror of [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)].  
  
    ```  
    CREATE DATABASE AdventureWorks_0600  
    ON (NAME = 'datafile', FILENAME = 'F:\AdventureWorks_0600.SNP')  
       AS SNAPSHOT OF AdventureWorks2012  
    ```  
  
2.  Create the second database snapshot on the mirror of [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]. Users who are still using `AdventureWorks_0600` can continue to use it.  
  
    ```  
    CREATE DATABASE AdventureWorks_1200  
    ON (NAME = 'datafile', FILENAME = 'F:\AdventureWorks_1200.SNP')  
       AS SNAPSHOT OF AdventureWorks2012  
    ```  
  
     At this point, new client connections can be programmatically directed to the latest snapshot.  
  
3.  Create the third snapshot on the mirror [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]. Users who are still using `AdventureWorks_0600` or `AdventureWorks_1200` can continue to use them.  
  
    ```  
    CREATE DATABASE AdventureWorks_1800  
    ON (NAME = 'datafile', FILENAME = 'F:\AdventureWorks_1800.SNP')  
        AS SNAPSHOT OF AdventureWorks2012  
    ```  
  
     At this point, new client connections can be programmatically directed to the latest snapshot.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/create-a-database-snapshot-transact-sql.md)  
  
-   [View a Database Snapshot &#40;SQL Server&#41;](../../relational-databases/databases/view-a-database-snapshot-sql-server.md)  
  
-   [Drop a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/drop-a-database-snapshot-transact-sql.md)  
  
  
## See Also  
 [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md)   
 [Connect Clients to a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/connect-clients-to-a-database-mirroring-session-sql-server.md)  
  
  
