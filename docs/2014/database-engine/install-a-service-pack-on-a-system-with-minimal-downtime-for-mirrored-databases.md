---
title: "Install a Service Pack on a System with Minimal Downtime for Mirrored Databases | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "hotfixes [SQL Server]"
  - "database mirroring [SQL Server], upgrading system"
  - "rolling upgrades [SQL Server]"
  - "service packs [SQL Server]"
  - "upgrading mirrored database systems"
  - "upgrading SQL Server, mirrored databases"
ms.assetid: bdc63142-027d-4ead-9d3e-147331387ef5
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Install a Service Pack on a System with Minimal Downtime for Mirrored Databases
  This topic describes how to minimize downtime for mirrored databases when you install service packs and hotfixes. This process involves sequentially upgrading the instances of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] that are participating in database mirroring. This form of update, which is known as a *rolling update*, reduces downtime to only a single failover. Note that for high-performance mode sessions in which the mirror server is geographically distant from the principal server, a rolling update might be inappropriate.  
  
 A rolling update is a multi-stage process that consists of the following stages:  
  
-   Protect your data.  
  
-   If the session includes a witness, we recommend that you remove the witness. Otherwise, when the mirror server instance is being updated, database availability depends on the witness that remains connected to the principal server instance. After you remove a witness, you can update it at any time during the rolling update process without risking database downtime.  
  
    > [!NOTE]  
    >  For more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](database-mirroring/quorum-how-a-witness-affects-database-availability-database-mirroring.md).  
  
-   If a session is running in high-performance mode, change the operating mode to high-safety mode.  
  
-   Update each server instance that is involved in database mirroring. A rolling update involves upgrading the server instance that is currently the mirror server, manually failing over each of its mirrored databases, and upgrading the server instance that was first the principal server (and is now the new mirror server). At this point, you will have to resume mirroring.  
  
    > [!NOTE]  
    >  Before starting a rolling update, we recommend that you perform a practice manual failover on at least one of your mirroring sessions.  
  
-   Revert to high-performance mode, if it is required.  
  
-   Return the witness to the mirroring session, if it is required.  
  
 The procedures for these stages are described here.  
  
> [!IMPORTANT]  
>  A server instance might be performing different mirroring roles (principal server, mirror server, or witness) in concurrent mirroring sessions. In this case, you will have to adapt the basic rolling update process accordingly.  
  
### To protect your data before an update (a best practice)  
  
1.  Perform a full database backup on every principal database.  
  
     **To back up a database**  
  
    -   [Create a Full Database Backup &#40;SQL Server&#41;](../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).  
  
2.  Run the [DBCC CHECKDB](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql) command on every principal database.  
  
### To remove a witness from a session  
  
1.  If a mirroring session involves a witness, we recommend that you remove the witness before you perform a rolling update.  
  
     **To remove the witness**  
  
    -   [Remove the Witness from a Database Mirroring Session &#40;SQL Server&#41;](database-mirroring/remove-the-witness-from-a-database-mirroring-session-sql-server.md)  
  
### To change a session from high-performance mode to high-safety mode  
  
1.  If a mirroring session is running in high-performance mode, before you perform a rolling update, change the operating mode to high safety without automatic failover. Use one of the following methods:  
  
    -   In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]: Change the **Operating mode** option to **High safety without automatic failover (synchronous)** by using the [Mirroring Page](../relational-databases/databases/database-properties-mirroring-page.md) of the **Database Properties** dialog box. For information about how to access this page, see [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](database-mirroring/start-the-configuring-database-mirroring-security-wizard.md).  
  
    -   In [!INCLUDE[tsql](../includes/tsql-md.md)]: Set transaction safety to FULL. For more information, see [Change Transaction Safety in a Database Mirroring Session &#40;Transact-SQL&#41;](database-mirroring/change-transaction-safety-in-a-database-mirroring-session-transact-sql.md).  
  
### To perform the rolling update  
  
1.  To minimize downtime, we recommend the following: Start the rolling update by updating any mirroring partner that is currently the mirror server in all its mirroring sessions. You might have to update multiple server instances at this point.  
  
    > [!NOTE]  
    >  A witness can be updated at any point in the rolling update process. For example, if a server instance is a mirror server in Session 1 and is a witness in Session 2, you can update the server instance now.  
  
     The server instance to update first depends on the current configuration of your mirroring sessions, as follows:  
  
    -   If any server instance is already the mirror server in all of its mirroring sessions, install the service pack or the hotfix on that server instance.  
  
    -   If all of your server instances are currently the principal server in any mirroring sessions, select one server instance to update first. Then, manually fail over each of its principal databases and update that server instance by installing the service pack or the hotfix.  
  
     After being updated, a server instance automatically rejoins each of its mirroring sessions.  
  
     **To perform a manual failover**  
  
    -   [Manually Fail Over a Database Mirroring Session &#40;SQL Server Management Studio&#41;](database-mirroring/manually-fail-over-a-database-mirroring-session-sql-server-management-studio.md)  
  
    -   [Manually Fail Over a Database Mirroring Session &#40;Transact-SQL&#41;](database-mirroring/manually-fail-over-a-database-mirroring-session-transact-sql.md).  
  
     For information about how manual failover works, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).  
  
2.  For each mirroring session whose mirror server instance has just been updated, wait for the session to synchronize. Then, connect to the principal server instance, and manually fail over the session. On failover, the updated server instance becomes the principal server for that session, and the former principal server becomes the mirror server.  
  
     The goal of this step is for another server instance to become the mirror server in every mirroring session in which it is a partner.  
  
3.  After you fail over, we recommend that you run the [DBCC CHECKDB](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql) command on the principal database.  
  
4.  Install the service pack or the hotfix on each server instance that is now the mirror server in all mirroring sessions in which it is a partner. You might have to update multiple servers at this point.  
  
    > [!IMPORTANT]  
    >  In a complex mirroring configuration, some server instance might still be the original principal server in one or more mirroring sessions. Repeat steps 2-4 for those server instances until all instances involved are updated.  
  
5.  Resume the mirroring session.  
  
    > [!NOTE]  
    >  Automatic failover will not work until the witness has been updated.  
  
6.  Install the service packs or hotfixes on any remaining server instance that is the witness in all its mirroring sessions. After an updated witness rejoins a mirroring session, automatic failover becomes possible again. You might have to update multiple servers at this point.  
  
### To return a session to high-performance mode  
  
1.  Optionally, return to high-performance mode by using one of the following methods:  
  
    -   In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]: Change the **Operating mode** option to **High performance (asynchronous)** by using the [Mirroring Page](../relational-databases/databases/database-properties-mirroring-page.md) of the **Database Properties** dialog box.  
  
    -   In [!INCLUDE[tsql](../includes/tsql-md.md)]: Use [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql-database-mirroring) to set transaction safety to OFF.  
  
### To return a witness to a mirroring session  
  
1.  Optionally, in high-safety mode, reestablish the witness to each mirroring session.  
  
     **To reestablish the witness**  
  
    -   [Add or Replace a Database Mirroring Witness &#40;SQL Server Management Studio&#41;](database-mirroring/add-or-replace-a-database-mirroring-witness-sql-server-management-studio.md)  
  
    -   [Add a Database Mirroring Witness Using Windows Authentication &#40;Transact-SQL&#41;](database-mirroring/add-a-database-mirroring-witness-using-windows-authentication-transact-sql.md)  
  
## See Also  
 [ALTER DATABASE Database Mirroring &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-database-mirroring)   
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [Database Mirroring &#40;SQL Server&#41;](database-mirroring/database-mirroring-sql-server.md)   
 [Database Mirroring Operating Modes](database-mirroring/database-mirroring-operating-modes.md)   
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)   
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [View the State of a Mirrored Database &#40;SQL Server Management Studio&#41;](database-mirroring/view-the-state-of-a-mirrored-database-sql-server-management-studio.md)  
  
  
