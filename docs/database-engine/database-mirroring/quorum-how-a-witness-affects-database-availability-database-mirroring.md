---
title: "Quorum: How a Witness Affects Database Availability (Database Mirroring) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "quorum [SQL Server], database mirroring"
  - "running exposed in database mirroring [SQL Server]"
  - "witness-to-partner quorum [SQL Server]"
  - "partners [SQL Server], partner-to-partner quorum"
  - "witness [SQL Server], quorum"
  - "have quorum [SQL Server]"
  - "quorum [SQL Server]"
  - "mirror database [SQL Server]"
  - "full quorum [SQL Server]"
  - "high-availability mode [SQL Server]"
ms.assetid: a62d9dd7-3667-4751-a294-a61fc9caae7c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Quorum: How a Witness Affects Database Availability (Database Mirroring)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Whenever a witness is set for a database mirroring session, *quorum* is required. Quorum is a relationship that exists when two or more server instances in a database mirroring session are connected to each other. Typically, quorum involves three interconnected server instances. When a witness is set, quorum is required to make the database available. Designed for high-safety mode with automatic failover, quorum makes sure that a database is owned by only one partner at a time.  
  
 If a particular server instance becomes disconnected from a mirroring session, that instance loses quorum. If no server instances are connected, the session loses quorum and the database becomes unavailable. Three types of quorum are possible:  
  
-   A *full quorum* includes both partners and the witness.  
  
-   A *witness-to-partner quorum* consists of the witness and either partner.  
  
-   A *partner-to-partner quorum* consists of the two partners.  
  
 The following figure shows these types of quorum.  
  
 ![Quorums: full; witness and partner; both partners](../../database-engine/database-mirroring/media/dbm-failovautoquorum.gif "Quorums: full; witness and partner; both partners")  
  
 As long as the current principal server has quorum, this server owns the role of principal and continues to serve the database, unless the database owner performs a manual failover. If the principal server loses quorum, it stops serving the database. Automatic failover can occur only if the principal database has lost quorum, which guarantees that it is no longer serving the database.  
  
 A disconnected server instance saves its most recent role in the session. Typically, a disconnected server instance reconnects to the session when it restarts and regains quorum.  
  
> [!IMPORTANT]  
>  The witness should be set only when you intend to use high-safety mode with automatic failover. In high-performance mode, for which a witness is never required, we strongly recommend setting the WITNESS property to OFF. For information about the impact of a witness on high-performance mode, see [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).  
  
## Quorum in High-Safety Mode Sessions  
 In high-safety mode, quorum allows automatic failover by providing a context in which the server instances with quorum arbitrate which partner owns the role of principal. The principal server serves the database if it has quorum. If the principal server loses quorum when the synchronized mirror server and witness retain quorum, automatic failover occurs.  
  
 The quorum scenarios for high-safety mode are as follows:  
  
-   A *full quorum* that consists of both partners and the witness.  
  
     Ordinarily, all three server instances participate in a three-way quorum, called a *full quorum*. With a full quorum, the principal and mirror servers continue to perform their respective roles (unless manual failover occurs).  
  
-   A *witness-to-partner quorum* that consists of the witness and either partner.  
  
     If the network connection between the partners is lost because one of the partners has been lost, the following cases are possible:  
  
    -   The mirror server is lost, and the principal server and witness retain quorum.  
  
         In this case, the principal sets its database to DISCONNECTED and runs with mirroring in a SUSPENDED state. (This is referred to as *running exposed*, because the database is currently not being mirrored.) When the mirror server rejoins the session, the server regains quorum as mirror and starts resynchronizing its copy of the database.  
  
    -   The principal server is lost, and the witness and the mirror server retain quorum.  
  
         In this case, automatic failover occurs. For more information, see [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).  
  
    -   All the server instances lose quorum, but subsequently the mirror and witness reconnect. The database will not be served in this case.  
  
     Rarely, the network connection between failover partners is lost while both partners remain connected to the witness. In this event, two, separate witness-to-partner quorums exist, with the witness as a liaison. The witness informs the mirror server that the principal server is still connected. Therefore, automatic failover does not occur. Instead, the mirror server retains the mirror role and waits to reconnect to the principal. If the redo queue contains log records at this point, the mirror server continues to roll forward the mirror database. On reconnecting, the mirror server will resynchronize the mirror database.  
  
-   A *partner-to-partner quorum* that consists of the two partners.  
  
     As long as the partners retain quorum, the database continues in a SYNCHRONIZED state, and manual failover remains possible. Without the witness, automatic failover is not possible; but when the witness regains quorum, the session resumes regular operation, and automatic failover is supported again.  
  
-   The session loses quorum.  
  
     If all the server instances become disconnected from each other, the session is said to have *lost quorum*. As server instances reconnect to each other, they regain quorum with each other.  
  
    -   If the principal server reconnects with either of the other server instances, the database becomes available.  
  
    -   If the principal server remains disconnected, but the mirror and witness reconnect to each other, automatic failover cannot occur because data loss might occur. Therefore, the database remains unavailable, until the principal server rejoins the session.  
  
    -   When all three server instances have reconnected, full quorum is regained, and the session resumes its regular operation.  
  
> [!IMPORTANT]  
>  When a session has a partner-to-partner quorum, if either partner loses quorum, the session loses quorum. Therefore, if you expect the witness to remain disconnected for lots of time, we recommend that you temporarily remove the witness from the session. Removing the witness removes the requirement for quorum. Then, if the mirror server becomes disconnected, the principal server can continue to serve the database. For information about how to add or remove a witness, see [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md).  
  
### How Quorum Affects Database Availability  
 The following illustration shows how the witness and the partners cooperate to make sure that, at given time, only one partner owns the role of principal and only the current principal server can bring its database online. Both scenarios start with full quorum, and **Partner_A** in the principal role and **Partner_B** in the mirror role.  
  
 ![How the witness and partners cooperate](../../database-engine/database-mirroring/media/dbm-quorum-scenarios.gif "How the witness and partners cooperate")  
  
 Scenario 1 shows how after the original principal server (**Partner_A**) fails, the witness and mirror agree that the principal, **Partner_A**, is not available any longer and form quorum. The mirror, **Partner_B** then assumes the principal role. Automatic failover occurs, and **Partner_B**, brings its copy of the database online. Then **Partner_B** goes down, and the database goes offline. Later, the former principal server, **Partner_A**, reconnects to the witness regaining quorum, but on communicating with the witness, **Partner_A** learns that it cannot bring its copy of the database online, because **Partner_B** now owns the principal role. When **Partner_B** rejoins the session, it brings the database back online.  
  
 In Scenario 2, the witness loses quorum, while the partners, **Partner_A** and **Partner_B**, retain quorum with each other, and the database remains online. Then the partners lose their quorum, too, and the database goes offline. Later, the principal server, **Partner_A**, reconnects to the witness regaining quorum. The witness confirms that **Partner_A** still owns the principal role, and **Partner_A** brings the database back online.  
  
## See Also  
 [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md)   
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)   
 [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md)   
 [Possible Failures During Database Mirroring](../../database-engine/database-mirroring/possible-failures-during-database-mirroring.md)   
 [Mirroring States &#40;SQL Server&#41;](../../database-engine/database-mirroring/mirroring-states-sql-server.md)  
  
  
