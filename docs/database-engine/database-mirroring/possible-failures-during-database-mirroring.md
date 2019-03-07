---
title: "Possible Failures During Database Mirroring | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "time-out period [SQL Server database mirroring]"
  - "soft errors [SQL Server]"
  - "database mirroring [SQL Server], troubleshooting"
  - "timeout errors [SQL Server]"
  - "troubleshooting [SQL Server], database mirroring"
  - "hard errors"
  - "failed database mirroring sessions [SQL Server]"
ms.assetid: d7031f58-5f49-4e6d-9a62-9b420f2bb17e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Possible Failures During Database Mirroring
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Physical, operating system, or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] problems can cause a failure in a database mirroring session. Database mirroring does not regularly check the components on which Sqlservr.exe relies to verify whether they are functioning correctly or have failed. However, for some types of failures, the affected component reports an error to Sqlservr.exe. An error reported by another component is called a *hard error*. To detect other failures that would otherwise go unnoticed, database mirroring implements its own time-out mechanism. When a mirroring time-out occurs, database mirroring assumes that a failure has occurred and declares a *soft error*. However, some failures that happen at the SQL Server instance level do not cause mirroring to time-out and can go undetected.  
  
> [!IMPORTANT]  
>  Failures in databases other than the mirrored database are not detectable in a database mirroring session. Moreover, a data disk failure is unlikely to be detected, unless the database is restarted because of a data disk failure.  
  
 The speed of error detection and, therefore, the reaction time of the mirroring session to a failure, depends on whether the error is hard or soft. Some hard errors, such as network failures are reported immediately. However, in some cases, component-specific time-out periods can delay the reporting of some hard errors. For soft errors, the length of the mirroring time-out period determines the speed of error detection. By default, this period is 10 seconds. This is the minimum recommended value.  
  
## Failures Due to Hard Errors  
 Possible causes of hard errors include (but are not limited to) the following conditions:  
  
-   A broken connection or wire  
  
-   A bad network card  
  
-   A router change  
  
-   Changes in the firewall  
  
-   Endpoint reconfiguration  
  
-   Loss of the drive where the transaction log resides  
  
-   Operating system or process failure  
  
 For example, when the log drive on the principal database becomes unresponsive and fails, the operating system informs Sqlservr.exe that a serious error has occurred.  
  
 Some components, such as network components and some IO subsystems, have their own time-outs to determine failures. Such time-outs are independent of database mirroring, which has no knowledge of them and is completely unaware of their behavior. In these cases, the time-out delay increases the time between a failure and when database mirroring receive the resulting hard error.  
  
> [!NOTE]  
>  The only active error checking performed for database mirroring occurs for soft error cases. For more information, see "Failures Due to Soft Errors," later in this topic.  
  
 To help you interpret the error conditions that occur on the network, ask a network engineer what error messages are sent to a port when the following events occur on a TCP connection:  
  
-   DNS is not working.  
  
-   Cables are unplugged.  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows has a firewall that blocks a specific port.  
  
-   The application that is monitoring a port fails.  
  
-   A Windows-based server is renamed.  
  
-   A Windows-based server is rebooted.  
  
> [!NOTE]  
>  Mirroring does not protect against problems specific to client accessing the servers. For example, consider a case in which a public network adapter handles client connections to the principal server instance, while a private network interface card handles all mirroring traffic among server instances. In this case, failure of the public network adapter would prevent clients from accessing the database, though the database would continue to be mirrored.  
  
## Failures Due to Soft Errors  
 Conditions that might cause mirroring time-outs include (but are not limited to) the following:  
  
-   Network errors such as TCP link time-outs, dropped or corrupted packets, or packets that are in an incorrect order.  
  
-   A hanging operating system, server, or database state.  
  
-   A Windows server timing out.  
  
-   Insufficient computing resources, such as a CPU or disk overload, the transaction log filling up, or the system is running out of memory or threads. In these cases, you must increase the time-out period, reduce the workload, or change the hardware to handle the workload.  
  
### The Mirroring Time-Out Mechanism  
 Because soft errors are not detectable directly by a server instance, a soft error could potentially cause a server instance to wait indefinitely. To prevent this, database mirroring implements its own time-out mechanism, based on each server instance in a mirroring session sending out a ping on each open connection at a fixed interval.  
  
 To keep a connection open, a server instance must receive a ping on that connection in the time-out period defined, plus the time that is required to send one more ping. Receiving a ping during the time-out period indicates that the connection is still open and that the server instances are communicating over it. On receiving a ping, a server instance resets its time-out counter on that connection.  
  
 If no ping is received on a connection during the time-out period, a server instance considers the connection to have timed out. The server instance closes the timed-out connection and handles the time-out event according to the state and operating mode of the session.  
  
 Even if the other server is actually proceeding correctly, a time-out is considered a failure. If the time-out value for a session is too short for the regular responsiveness of either partner, false failures can occur. A false failure occurs when one server instance successfully contacts another whose response time is so slow that its pings are not received before the time-out period expires.  
  
 In high-performance mode sessions, the time-out period is always 10 seconds. This is generally enough to avoid false failures. In high-safety mode sessions, the default time-out period is 10 seconds, but you can change the duration. To avoid false failures, we recommend that the mirroring time-out period always be 10 seconds or more.  
  
 **To change the time-out value (high-safety mode only)**  
  
-   Use the [ALTER DATABASE \<database> SET PARTNER TIMEOUT \<integer>](../../t-sql/statements/alter-database-transact-sql.md) statement.  
  
 **To view the current time-out value**  
  
-   Query **mirroring_connection_timeout** in [sys.database_mirroring](../../relational-databases/system-catalog-views/sys-database-mirroring-transact-sql.md).  
  
## Responding to an Error  
 Regardless of the type of error, a server instance that detects an error responds appropriately based on the role of the instance, the operating mode of the session, and the state of any other connection in the session. For information about what occurs on the loss of a partner, see [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).  
  
## See Also  
 [Estimate the Interruption of Service During Role Switching &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/estimate-the-interruption-of-service-during-role-switching-database-mirroring.md)   
 [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md)   
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)  
  
  
