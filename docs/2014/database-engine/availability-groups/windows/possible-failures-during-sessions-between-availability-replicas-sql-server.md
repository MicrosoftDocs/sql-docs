---
title: "Possible Failures During Sessions Between Availability Replicas (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "troubleshooting [SQL Server, HADR]"
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], troubleshooting"
ms.assetid: cd613898-82d9-482f-a255-0230a6c7d6fe
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Possible Failures During Sessions Between Availability Replicas (SQL Server)
  Physical, operating system, or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] problems can cause a failure in a session between two availability replicas. An availability replica does not regularly check the components on which Sqlservr.exe relies to verify whether they are functioning correctly or have failed. However, for some types of failures, the affected component reports an error to Sqlservr.exe. An error reported by another component is called a *hard error*. To detect other failures that would otherwise go unnoticed, [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] implements its own session-timeout mechanism. Specifies the session-timeout period in seconds. This time-out period is the maximum time that a server instance waits to receive a PING message from another instance before considering that other instance to be disconnected. When a session timeout occurs between two availability replicas, the availability replicas assume that a failure has occurred and declares a *soft error*.  
  
> [!IMPORTANT]  
>  Failures in databases other than the primary database are not detectable. Moreover, a data disk failure is unlikely to be detected unless the database is restarted because of a data disk failure.  
  
 The speed of error detection and, therefore, the reaction time to a failure, depends on whether the error is hard or soft. Some hard errors, such as network failures are reported immediately. However, in some cases, component-specific time-out periods can delay the reporting of some hard errors. For soft errors, the length of the session-timeout period determines the speed of error detection. By default, this period is 10 seconds. This is the minimum recommended value.  
  
## Failures Due to Hard Errors  
 Possible causes of hard errors include (but are not limited to) the following conditions:  
  
-   A broken connection or wire  
  
-   A bad network card  
  
-   A router change  
  
-   Changes in the firewall  
  
-   Endpoint reconfiguration  
  
-   Loss of the drive where the transaction log resides  
  
-   Operating system or process failure  
  
 For example, when the log drive on the primary database becomes unresponsive and fails, the operating system informs Sqlservr.exe that a serious error has occurred.  
  
 Some components, such as network components and some IO subsystems, have their own time-outs to determine failures. Such time-outs are independent of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], which has no knowledge of them and is completely unaware of their behavior. In these cases, the time-out delay increases the time between a failure and when the availability replica receives the resulting hard error.  
  
> [!NOTE]  
>  The only active error checking performed for availability replicas occurs for soft error cases. For more information, see "Failures Due to Soft Errors," later in this topic.  
  
 To help you interpret the error conditions that occur on the network, ask a network engineer what error messages are sent to a port when the following events occur on a TCP connection:  
  
-   DNS is not working.  
  
-   Cables are unplugged.  
  
-   [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows has a firewall that blocks a specific port.  
  
-   The application that is monitoring a port fails.  
  
-   A Windows-based server is renamed.  
  
-   A Windows-based server is rebooted.  
  
> [!NOTE]  
>  [!INCLUDE[ssHADRc](../../../includes/sshadrc-md.md)] does not protect against problems specific to client accessing the servers. For example, consider a case in which a public network adapter handles client connections to the primary replica, while a private network interface card handles traffic among the server instances that are hosting the replicas of an availability group. In this case, failure of the public network adapter would prevent clients from accessing the databases.  
  
## Failures Due to Soft Errors  
 Conditions that might cause session timeouts include (but are not limited to) the following:  
  
-   Network errors such as TCP link time-outs, dropped or corrupted packets, or packets that are in an incorrect order.  
  
-   A hanging operating system, server, or database state.  
  
-   A Windows server timing out.  
  
-   Insufficient computing resources, such as a CPU or disk overload, the transaction log filling up, or the system is running out of memory or threads. In these cases, you must increase the time-out period, reduce the workload, or change the hardware to handle the workload.  
  
### The Session-Timeout Mechanism  
 Because soft errors are not detectable directly by a server instance, a soft error could potentially cause an availability replica to wait indefinitely for a response from the other availability replica in a session. To prevent this, [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] implements a session time-out mechanism, based on the connected availability replicas sending out a ping on each open connection at a fixed interval. Receiving a ping during the time-out period indicates that the connection is still open and that the server instances are communicating over it. On receiving a ping, a replica resets its time-out counter on that connection. For information about the relationship of availability mode and session timeouts, see [ Availability Modes (AlwaysOn Availability Groups)](availability-modes-always-on-availability-groups.md).  
  
 The primary and secondary replicas ping each other to signal that they are still active, and a session-timeout limit prevents either replica from waiting indefinitely to receive a ping from the other replica. The session-timeout limit is a user-configurable replica property with a default value of 10 seconds. Receiving a ping during the time-out period indicates that the connection is still open and that the server instances are communicating over it. On receiving a ping, an availability replica resets its time-out counter on that connection.  
  
 If no ping is received from the other replica within the session-timeout period, the connection times out. The connection is closed, and the timed-out replica enters the DISCONNECTED state. Even if a disconnected replica is configured for synchronous-commit mode, transactions will not wait for that replica to reconnect and resynchronize.  
  
## Responding to an Error  
 Regardless of the type of error, a server instance that detects an error responds appropriately based on the role of the instance, the availability mode of the session, and the state of any other connection in the session. For information about what occurs on the loss of a partner, see [ Availability Modes (AlwaysOn Availability Groups)](availability-modes-always-on-availability-groups.md).  
  
## Related Tasks  
 **To change the time-out value (synchronous-commit availability mode only)**  
  
-   [Change the Session-Timeout Period for an Availability Replica &#40;SQL Server&#41;](change-the-session-timeout-period-for-an-availability-replica-sql-server.md)  
  
 **To view the current time-out value**  
  
-   Query **session_timeout** in [sys.availability_replicas &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-availability-replicas-transact-sql).  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)  
  
  
