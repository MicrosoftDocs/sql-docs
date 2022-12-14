---
title: "Database Mirroring (SQL Server)"
description: Learn about database mirroring, which is a solution for increasing the availability of a SQL Server database and is implemented on a per-database basis.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "05/16/2016"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
helpviewer_keywords:
  - "partners [SQL Server]"
  - "standby servers [SQL Server]"
  - "principal database [SQL Server]"
  - "principal server [SQL Server]"
  - "mirroring partners [SQL Server]"
  - "mirrored databases [SQL Server] See database mirroring"
  - "database mirroring [SQL Server], partners"
  - "availability [SQL Server]"
  - "hot standby servers (see database monitoring [SQL Server])"
  - "database mirroring [SQL Server], about database mirroring"
  - "mirror database [SQL Server]"
  - "mirror server [SQL Server] See database mirroring"
---
# Database Mirroring (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] instead.  
  
 *Database mirroring* is a solution for increasing the availability of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Mirroring is implemented on a per-database basis and works only with databases that use the full recovery model.  
  
> [!IMPORTANT]  
>  For information about support for database mirroring, restrictions, prerequisites, recommendations for configuring partner servers, and recommendations for deploying database mirroring, see [Prerequisites, Restrictions, and Recommendations for Database Mirroring](../../database-engine/database-mirroring/prerequisites-restrictions-and-recommendations-for-database-mirroring.md).  
  
  
##  <a name="Benefits"></a> Benefits of Database Mirroring  
 Database mirroring is a simple strategy that offers the following benefits:  
  
-   Increases availability of a database.  
  
     In the event of a disaster, in high-safety mode with automatic failover, failover quickly brings the standby copy of the database online (without data loss). In the other operating modes, the database administrator has the alternative of forcing service (with possible data loss) to the standby copy of the database. For more information, see [Role Switching](#RoleSwitching), later in this topic.  
  
-   Increases data protection.  
  
     Database mirroring provides complete or almost complete redundancy of the data, depending on whether the operating mode is high-safety or high-performance. For more information, see [Operating Modes](#OperatingModes), later in this topic.  
  
     A database mirroring partner running on [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] or later versions automatically tries to resolve certain types of errors that prevent reading a data page. The partner that is unable to read a page requests a fresh copy from the other partner. If this request succeeds, the unreadable page is replaced by the copy, which usually resolves the error. For more information, see [Automatic Page Repair &#40;Availability Groups: Database Mirroring&#41;](../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md).  
  
-   Improves the availability of the production database during upgrades.  
  
     To minimize downtime for a mirrored database, you can sequentially upgrade the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are hosting the failover partners. This will incur the downtime of only a single failover. This form of upgrade is known as a *rolling upgrade*. For more information, see [Upgrading Mirrored Instances](../../database-engine/database-mirroring/upgrading-mirrored-instances.md).  
  
  
##  <a name="TermsAndDefinitions"></a> Database Mirroring Terms and Definitions  
 automatic failover  
 The process by which, when the principal server becomes unavailable, the mirror server to take over the role of principal server and brings its copy of the database online as the principal database.  
  
 failover partners  
 The two server instances (the principal server or the mirror server) that act as role-switching partners for a mirrored database.  
  
 forced service  
 A failover initiated by the database owner upon the failure of the principal server that transfers service to the mirror database while it is in an unknown state.  
  
 High-performance mode  
 The database mirroring session operates asynchronously and uses only the principal server and mirror server. The only form of role switching is forced service (with possible data loss).  
  
 High-safety mode  
 The database mirroring session operates synchronously and, optionally, uses a witness, as well as the principal server and mirror server.  
  
 manual failover  
 A failover initiated by the database owner, while the principal server is still running, that transfers service from the principal database to the mirror database while they are in a synchronized state.  
  
 mirror database  
 The copy of the database that is typically fully synchronized with the principal database.  
  
 mirror server  
 In a database mirroring configuration, the server instance on which the mirror database resides.  
  
 principal database  
 In database mirroring, a read-write database whose transaction log records are applied to a read-only copy of the database (a mirror database).  
  
 principal server  
 In database mirroring, the partner whose database is currently the principal database.  
  
 redo queue  
 Received transaction log records that are waiting on the disk of a mirror server.  
  
 role  
 The principal server and mirror server perform complementary principal and mirror roles. Optionally, the role of witness is performed by a third server instance.  
  
 role switching  
 The taking over of the principal role by the mirror.  
  
 send queue  
 Unsent transaction log records that have accumulated on the log disk of the principal server.  
  
 session  
 The relationship that occurs during database mirroring among the principal server, mirror server, and witness server (if present).  
  
 After a mirroring session starts or resumes, the process by which log records of the principal database that have accumulated on the principal server are sent to the mirror server, which writes these log records to disk as quickly as possible to catch up with the principal server.  
  
 Transaction safety  
 A mirroring-specific database property that determines whether a database mirroring session operates synchronously or asynchronously. There are two safety levels: FULL and OFF.  
  
 Witness  
 For use only with high-safety mode, an optional instance of SQL Server that enables the mirror server to recognize when to initiate an automatic failover. Unlike the two failover partners, the witness does not serve the database. Supporting automatic failover is the only role of the witness.  
  
  
##  <a name="HowWorks"></a> Overview of Database Mirroring  
 Database mirroring maintains two copies of a single database that must reside on different server instances of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. Typically, these server instances reside on computers in different locations. Starting database mirroring on a database, initiates a relationship, known as a *database mirroring session*, between these server instances.  
  
 One server instance serves the database to clients (the *principal server*). The other instance acts as a hot or warm standby server (the *mirror server*), depending on the configuration and state of the mirroring session. When a database mirroring session is synchronized, database mirroring provides a hot standby server that supports rapid failover without a loss of data from committed transactions. When the session is not synchronized, the mirror server is typically available as a warm standby server (with possible data loss).  
  
 The principal and mirror servers communicate and cooperate as *partners* in a *database mirroring session*. The two partners perform complementary roles in the session: the *principal role* and the *mirror role*. At any given time, one partner performs the principal role, and the other partner performs the mirror role. Each partner is described as *owning* its current role. The partner that owns the principal role is known as the *principal server*, and its copy of the database is the current principal database. The partner that owns the mirror role is known as the *mirror server*, and its copy of the database is the current mirror database. When database mirroring is deployed in a production environment, the principal database is the *production database*.  
  
 Database mirroring involves *redoing* every insert, update, and delete operation that occurs on the principal database onto the mirror database as quickly as possible. Redoing is accomplished by sending a stream of active transaction log records to the mirror server, which applies log records to the mirror database, in sequence, as quickly as possible. Unlike replication, which works at the logical level, database mirroring works at the level of the physical log record. Beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], the principal server compresses the stream of transaction log records before sending it to the mirror server. This log compression occurs in all mirroring sessions.  
  
> [!NOTE]  
>  A given server instance can participate in multiple concurrent database mirroring sessions with the same or different partners. A server instance can be a partner in some sessions and a witness in other sessions. The mirror server instance must be running the same edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **In This Section:**  
  
-   [Operating Modes](#OperatingModes)  
  
-   [Role Switching](#RoleSwitching)  
  
-   [Concurrent Sessions](#ConcurrentSessions)  
  
-   [Client Connections](#ClientConnections)  
  
-   [Impact of Pausing a Session on the Principal Transaction Log](#ImpactOfPausing)  
  
  
###  <a name="OperatingModes"></a> Operating Modes  
 A database mirroring session runs with either synchronous or asynchronous operation. Under asynchronous operation, the transactions commit without waiting for the mirror server to write the log to disk, which maximizes performance. Under synchronous operation, a transaction is committed on both partners, but at the cost of increased transaction latency.  
  
 There are two mirroring operating modes. One of them, *high-safety mode* supports synchronous operation. Under high-safety mode, when a session starts, the mirror server synchronizes the mirror database together with the principal database as quickly as possible. As soon as the databases are synchronized, a transaction is committed on both partners, at the cost of increased transaction latency.  
  
 The second operating mode, *high-performance mode*, runs asynchronously. The mirror server tries to keep up with the log records sent by the principal server. The mirror database might lag somewhat behind the principal database. However, typically, the gap between the databases is small. However, the gap can become significant if the principal server is under a heavy work load or the system of the mirror server is overloaded.  
  
 In high-performance mode, as soon as the principal server sends a log record to the mirror server, the principal server sends a confirmation to the client. It does not wait for an acknowledgement from the mirror server. This means that transactions commit without waiting for the mirror server to write the log to disk. Such asynchronous operation enables the principal server to run with minimum transaction latency, at the potential risk of some data loss.  
  
 All database mirroring sessions support only one principal server and one mirror server. This configuration is shown in the following illustration.  
  
 ![Partners in a database mirroring session](../../database-engine/database-mirroring/media/dbm-2-way-session-intro.gif "Partners in a database mirroring session")  
  
 High-safety mode with automatic failover requires a third server instance, known as a *witness*. Unlike the two partners, the witness does not serve the database. The witness supports automatic failover by verifying whether the principal server is up and functioning. The mirror server initiates automatic failover only if the mirror and the witness remain connected to each other after both have been disconnected from the principal server.  
  
 The following illustration shows a configuration that includes a witness.  
  
 ![A mirroring session that includes a witness](../../database-engine/database-mirroring/media/dbm-3-way-session-intro-ov.gif "A mirroring session that includes a witness")  
  
 For more information, see [Role Switching](#RoleSwitching), later in this topic.  
  
> [!NOTE]  
>  Establishing a new mirroring session or adding a witness to an existing mirroring configuration requires that all involved server instances run the same version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, when you are upgrading to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or a later version, the versions of the involved instances can vary. For more information, see [Upgrading Mirrored Instances](../../database-engine/database-mirroring/upgrading-mirrored-instances.md).  
  
  
####  <a name="TxnSafety"></a> Transaction Safety and Operating Modes  
 Whether an operating mode is asynchronous or synchronous depends on the transaction safety setting. If you exclusively use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to configure database mirroring, transaction safety settings are configured automatically when you select the operation mode.  
  
 If you use [!INCLUDE[tsql](../../includes/tsql-md.md)] to configure database mirroring, you must understand how to set transaction safety. Transaction safety is controlled by the SAFETY property of the ALTER DATABASE statement. On a database that is being mirrored, SAFETY is either FULL or OFF.  
  
-   If the SAFETY option is set to FULL, database mirroring operation is synchronous, after the initial synchronizing phase. If a witness is set in high-safety mode, the session supports automatic failover.  
  
-   If the SAFETY option is set to OFF, database mirroring operation is asynchronous. The session runs in high-performance mode, and the WITNESS option should also be OFF.  
  
 For more information, see [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).  
  
  
###  <a name="RoleSwitching"></a> Role Switching  
 Within the context of a database mirroring session, the principal and mirror roles are typically interchangeable in a process known as *role switching*. Role switching involves transferring the principal role to the mirror server. In role switching, the mirror server acts as the *failover partner* for the principal server. When a role switch occurs, the mirror server takes over the principal role and brings its copy of the database online as the new principal database. The former principal server, if available, assumes the mirror role, and its database becomes the new mirror database. Potentially, the roles can switch back and forth repeatedly.  
  
 The following three forms of role switching exist.  
  
-   *Automatic failover*  
  
     This requires high-safety mode and the presence of the mirror server and a witness. The database must already be synchronized, and the witness must be connected to the mirror server.  
  
     The role of the witness is to verify whether a given partner server is up and functioning. If the mirror server loses its connection to the principal server but the witness is still connected to the principal server, the mirror server does not initiate a failover. For more information, see [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md).  
  
-   *Manual failover*  
  
     This requires high-safety mode. The partners must be connected to each other, and the database must already be synchronized.  
  
-   *Forced service* (with possible data loss)  
  
     Under high-performance mode and high-safety mode without automatic failover, forcing service is possible if the principal server has failed and the mirror server is available.  
  
    > [!IMPORTANT]  
    >  High-performance mode is intended to run without a witness. But if a witness exists, forcing service requires that the witness is connected to the mirror server.  
  
 In any role-switching scenario, as soon as the new principal database comes online, the client applications can recover quickly by reconnecting to the database.  
  
  
###  <a name="ConcurrentSessions"></a> Concurrent Sessions  
 A given server instance can participate in multiple, concurrent database mirroring sessions (once per mirrored database) with the same or different server instances. Often, a server instance serves exclusively as a partner or a witness in all of its database mirroring sessions. However, because each session is independent of the other sessions, a server instance can act as a partner in some sessions and as a witness in other sessions. For example, consider the following four sessions among three server instances (`SSInstance_1`, `SSInstance_2`, and `SSInstance_3`). Each server instance serves as a partner in some sessions and as a witness in others:  
  
|Server instance|Session for database A|Session for database B|Session for database C|Session for database D|  
|---------------------|----------------------------|----------------------------|----------------------------|----------------------------|  
|`SSInstance_1`|Witness|Partner|Partner|Partner|  
|`SSInstance_2`|Partner|Witness|Partner|Partner|  
|`SSInstance_3`|Partner|Partner|Witness|Witness|  
  
 The following figure illustrates two server instances that are participating as partners together in two mirroring sessions. One session is for a database named **Db_1**, and the other session is for a database named **Db_2**.  
  
 ![Two server instances in two concurrent sessions](../../database-engine/database-mirroring/media/dbm-concurrent-sessions.gif "Two server instances in two concurrent sessions")  
  
 Each of the databases is independent of the others. For example, a server instance might initially be the mirror server for two databases. If one of those databases fails over, the server instance becomes the principal server for the failed-over database while remaining the mirror server for the other database.  
  
 As another example, consider a server instance that is the principal server for two or more databases running in high-safety mode with automatic failover, If the server instance fails, all of the databases automatically failover to their respective mirror databases.  
  
 When setting up a server instance to operate both as a partner and a witness, be sure that the database mirroring endpoint supports both roles (for more information, see [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)). Also, ensure that the system has sufficient resources to reduce resource contention.  
  
> [!NOTE]  
>  Because mirrored databases are independent of each other, databases cannot fail over as a group.  
  
###  <a name="ClientConnections"></a> Client Connections  
 Client-connection support for database mirroring sessions is provided by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Connect Clients to a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/connect-clients-to-a-database-mirroring-session-sql-server.md).  
  
  
###  <a name="ImpactOfPausing"></a> Impact of Pausing a Session on the Principal Transaction Log  
 At any time, the database owner can pause a session. Pausing preserves the session state while removing mirroring. When a session is paused, the principal server does not send any new log records to the mirror server. All of these records remain active and accumulate in the transaction log of the principal database. As long as a database mirroring session remains paused, the transaction log cannot be truncated. Therefore, if the database mirroring session is paused for too long, the log can fill up.  
  
 For more information, see [Pausing and Resuming Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/pausing-and-resuming-database-mirroring-sql-server.md).  
  
##  <a name="SettingUpDbmSession"></a> Setting Up Database Mirroring Session  
 Before a mirroring session can begin, the database owner or system administrator must create the mirror database, set up endpoints and logins, and, in some cases, create and set up certificates. For more information, see [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md).  
  
##  <a name="InterOp"></a> Interoperability and Coexistence with Other Database Engine Features  
 Database mirroring can be used with the following features or components of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   [Log shipping](../../database-engine/database-mirroring/database-mirroring-and-log-shipping-sql-server.md)  
  
-   [Full-text catalogs](../../database-engine/database-mirroring/database-mirroring-and-full-text-catalogs-sql-server.md)  
  
-   [Database snapshots](../../database-engine/database-mirroring/database-mirroring-and-database-snapshots-sql-server.md)  
  
-   [Replication](../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md)  
  
##  <a name="InThisSection"></a> In This Section  
 [Prerequisites, Restrictions, and Recommendations for Database Mirroring](../../database-engine/database-mirroring/prerequisites-restrictions-and-recommendations-for-database-mirroring.md)  
 Describes the prerequisites and recommendations for setting up database mirroring.  
  
 [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md)  
 Contains information about the synchronous and asynchronous operating modes for database mirroring sessions, and about switching partner roles during a database mirroring session.  
  
 [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md)  
 Describes the role of a witness in database mirroring, how to use a single witness in multiple mirroring sessions, software and hardware recommendations for witnesses, and the role of the witness in automatic failover. It also contains information about adding or removing a witness.  
  
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)  
 Contains information about switching partner roles during a database mirroring session, including automatic failover, manual failover, and forced service (with possible data loss). Also, contains information about estimating the interruption of service during role switching.  
  
 [Possible Failures During Database Mirroring](../../database-engine/database-mirroring/possible-failures-during-database-mirroring.md)  
 Discusses physical, operating system, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] problems, including hard errors and soft errors, that can cause a failure in a database mirroring session. Discusses how the mirroring time-out mechanism responds to soft errors.  
  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)  
 Discusses how the database mirroring endpoint functions.  
  
 [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md)  
 Contains topics about the prerequisites, recommendations, and steps for setting up database mirroring.  
  
 [Connect Clients to a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/connect-clients-to-a-database-mirroring-session-sql-server.md)  
 Contains topics covering client connection-string attributes and the algorithms for connecting and reconnecting a client to a mirrored database.  
  
 [Pausing and Resuming Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/pausing-and-resuming-database-mirroring-sql-server.md)  
 Discusses what happens while database mirroring is paused, including the impact on transaction log truncation, and contains descriptions about how to pause and resume database mirroring.  
  
 [Removing Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/removing-database-mirroring-sql-server.md)  
 Discusses the impact of removing mirroring and contains descriptions about how to end a session  
  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)  
 Contains information about using Database Mirroring Monitor or the **dbmmonitor** stored procedures to monitor database mirroring or sessions.  
  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
### Configuration Tasks  
 **Using SQL Server Management Studio**  
  
-   [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-the-configuring-database-mirroring-security-wizard.md)  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
 **Using Transact-SQL**  
  
-   [Allow Network Access to a Database Mirroring Endpoint Using Windows Authentication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-allow-network-access-windows-authentication.md)  
  
-   [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md)  
  
-   [Allow a Database Mirroring Endpoint to Use Certificates for Inbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-inbound-connections.md)  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-establish-session-windows-authentication.md)  
  
-   [Add a Database Mirroring Witness Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/add-a-database-mirroring-witness-using-windows-authentication-transact-sql.md)  
  
-   [Set Up a Mirror Database to Use the Trustworthy Property &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/set-up-a-mirror-database-to-use-the-trustworthy-property-transact-sql.md)  
  
 **Using Transact-SQL or SQL Server Management Studio**  
  
-   [Upgrading Mirrored Instances](../../database-engine/database-mirroring/upgrading-mirrored-instances.md)  
  
-   [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md)  
  
  
### Administrative Tasks  
 **Transact-SQL**  
  
-   [Change Transaction Safety in a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/change-transaction-safety-in-a-database-mirroring-session-transact-sql.md)  
  
-   [Manually Fail Over a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/manually-fail-over-a-database-mirroring-session-transact-sql.md)  
  
-   [Force Service in a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/force-service-in-a-database-mirroring-session-transact-sql.md)  
  
-   [Pause or Resume a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/pause-or-resume-a-database-mirroring-session-sql-server.md)  
  
-   [Remove the Witness from a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-the-witness-from-a-database-mirroring-session-sql-server.md)  
  
-   [Remove Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-database-mirroring-sql-server.md)  
  
 **SQL Server Management Studio**  
  
-   [Add or Replace a Database Mirroring Witness &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/add-or-replace-a-database-mirroring-witness-sql-server-management-studio.md)  
  
-   [Manually Fail Over a Database Mirroring Session &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/manually-fail-over-a-database-mirroring-session-sql-server-management-studio.md)  
  
-   [Pause or Resume a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/pause-or-resume-a-database-mirroring-session-sql-server.md)  
  
-   [Remove the Witness from a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-the-witness-from-a-database-mirroring-session-sql-server.md)  
  
-   [Remove Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-database-mirroring-sql-server.md)  
  
  
## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Automatic Page Repair &#40;Availability Groups: Database Mirroring&#41;](../../sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring.md)   
 [Troubleshoot Database Mirroring Configuration &#40;SQL Server&#41;](../../database-engine/database-mirroring/troubleshoot-database-mirroring-configuration-sql-server.md)   
 [Database Mirroring: Interoperability and Coexistence &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-interoperability-and-coexistence-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for Database Mirroring](../../database-engine/database-mirroring/prerequisites-restrictions-and-recommendations-for-database-mirroring.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md)  
  
  
