---
title: "Upgrading Mirrored Instances | Microsoft Docs"
ms.custom: ""
ms.date: "02/01/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading SQL Server, rolling upgrade of mirrored databases"
  - "database mirroring [SQL Server], upgrading system"
  - "rolling upgrades [SQL Server]"
ms.assetid: 0e73bd23-497d-42f1-9e81-8d5314bcd597
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Upgrading Mirrored Instances
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  When upgrading a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] mirrored  instance to a new [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version, to a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]service pack or cumulative update, or to a new Windows service pack or cumulative update, you can reduce downtime for each mirrored database to only a single manual failover by performing a rolling upgrade (or two manual failovers if failing back to the original primary). A rolling upgrade is a multi-stage process that in its simplest form involves upgrading the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance that is currently acting as the mirror server in a mirroring session, then manually failing over the mirrored database, upgrading the former principal [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance, and resuming mirroring. In practice, the exact process will depend on the operating mode and the number and layout of mirroring session running on the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instances that you are upgrading.  
  
> [!NOTE]  
>  For information on using database mirroring with log shipping during a migration, download this [Database Mirroring and Log Shipping whitepaper](https://t.co/RmO6ruCT4J).  
  
## Prerequisites  
 Before you begin, review the following important information:  
  
-   [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md): Verify that you can upgrade to SQL Server 2016 from your version of the Windows operating system and version of SQL Server. For example, you cannot upgrade directly from a SQL Server 2005 instance to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
-   [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md): Select the appropriate upgrade method and steps based on your review of supported version and edition upgrades and also based on other components installed in your environment to upgrade components in the correct order.  
  
-   [Plan and Test the Database Engine Upgrade Plan](../../database-engine/install-windows/plan-and-test-the-database-engine-upgrade-plan.md): Review the release notes and known upgrade issues, the pre-upgrade checklist, and develop and test the upgrade plan.  
  
-   [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md):  Review the software requirements for installing [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. If additional software is required, install it on each node before you begin the upgrade process to minimize any downtime.  
  
## Recommended Preparation (Best Practices)  
 Before starting a rolling upgrade, we recommend that you:  
  
1.  Perform a practice manual failover on at least one of your mirroring sessions:  
  
    -   [Manually Fail Over a Database Mirroring Session &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/manually-fail-over-a-database-mirroring-session-sql-server-management-studio.md)  
  
    -   [Manually Fail Over a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/manually-fail-over-a-database-mirroring-session-transact-sql.md).  
  
    > [!NOTE]  
    >  For information about how manual failover works, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).  
  
2.  Protect your data:  
  
    1.  Perform a full database backup on every principal database:  
  
         [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).  
  
    2.  Run the [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) command on every principal database.  
  
## Stages of a Rolling Upgrade  
 The specific steps of a rolling upgrade depend on the operating mode of the mirroring configuration. However, the basic stages are the same.  
  
> [!NOTE]  
>  For information about the operating modes, see [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).  
  
 The following illustration is a flowchart that shows the basic stages of a rolling upgrade for each operating mode. The corresponding procedures are described after the illustration.  
  
 ![Flowchart showing steps of a rolling upgrade](../../database-engine/database-mirroring/media/dbm-rolling-upgrade.gif "Flowchart showing steps of a rolling upgrade")  
  
> [!IMPORTANT]  
>  A server instance might be performing different mirroring roles (principal server, mirror server, or witness) in concurrent mirroring sessions. In this case, you will have to adapt the basic rolling upgrade process accordingly. For more information, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).  
  
> [!NOTE]  
>  In many cases, after the rolling upgrade is completed, you will failback to the original principal server.  
  
### To change a session from high-performance mode to high-safety mode  
  
1.  If a mirroring session is running in high-performance mode, before you perform a rolling upgrade, change the operating mode to high safety without automatic failover.  
  
    > [!IMPORTANT]  
    >  If the mirror server is geographically distant from the principal server, a rolling upgrade might be inappropriate.  
  
    -   In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]: Change the **Operating mode** option to **High safety without automatic failover (synchronous)** by using the [Mirroring Page](../../relational-databases/databases/database-properties-mirroring-page.md) of the **Database Properties** dialog box. For information about how to access this page, see [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-the-configuring-database-mirroring-security-wizard.md).  
  
    -   In [!INCLUDE[tsql](../../includes/tsql-md.md)]: Set transaction safety to FULL. For more information, see [Change Transaction Safety in a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/change-transaction-safety-in-a-database-mirroring-session-transact-sql.md)  
  
### To remove a witness from a session  
  
1.  If a mirroring session involves a witness, we recommend that you remove the witness before you perform a rolling upgrade. Otherwise, when the mirror server instance is being upgraded, database availability depends on the witness that remains connected to the principal server instance. After you remove a witness, you can upgrade it at any time during the rolling upgrade process without risking database downtime.  
  
    > [!NOTE]  
    >  For more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/quorum-how-a-witness-affects-database-availability-database-mirroring.md).  
  
    -   [Remove the Witness from a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-the-witness-from-a-database-mirroring-session-sql-server.md)  
  
### To perform the rolling upgrade  
  
1.  To minimize downtime, we recommend the following: Start the rolling upgrade by updating any mirroring partner that is currently the mirror server in all its mirroring sessions. You might have to update multiple server instances at this point.  
  
    > [!NOTE]  
    >  A witness can be upgraded at any point in the rolling upgrade process. For example, if a server instance is a mirror server in Session 1 and is a witness in Session 2, you can upgrade the server instance now.  
  
     The server instance to upgrade first depends on the current configuration of your mirroring sessions, as follows:  
  
    -   If any server instance is already the mirror server in all its mirroring sessions, upgrade the server instance to the new version.  
  
    -   If all your server instances are currently the principal server in any mirroring sessions, select one server instance to upgrade first. Then, manually fail over each of its principal databases and upgrade that server instance.  
  
     After being upgraded, a server instance automatically rejoins each of its mirroring sessions.  
  
2.  For each mirroring session whose mirror server instance has just been upgraded, wait for the session to synchronize. Then, connect to the principal server instance, and manually fail over the session. On failover, the upgraded server instance becomes the principal server for that session, and the former principal server becomes the mirror server.  
  
     The goal of this step is for another server instance to become the mirror server in every mirroring session in which it is a partner.  
  
     **Restrictions after you failover to an upgraded server instance.**  
  
     After failing over from an earlier server instance to a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] server instance, the database session is suspended. It cannot be resumed until the other partner has been upgraded. However, the principal server is still accepting connections and allowing data access and modifications on the principal database.  
  
    > [!NOTE]  
    >  Establishing a new mirroring session requires that the server instances all be running the same version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
3.  After you fail over, we recommend that you run the [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) command on the principal database.  
  
4.  Upgrade each server instance that is now the mirror server in all mirroring sessions in which it is a partner. You might have to update multiple servers at this point.  
  
    > [!IMPORTANT]  
    >  In a complex mirroring configuration, some server instance might still be the original principal server in one or more mirroring sessions. Repeat steps 2-4 for those server instances until all instances involved are upgraded.  
  
5.  Resume the mirroring session.  
  
    > [!NOTE]  
    >  Automatic failover will not work until the witness has been upgraded and added back into the mirroring session.  
  
6.  Upgrade any remaining server instance that is the witness in all its mirroring sessions. After an upgraded witness rejoins a mirroring session, automatic failover becomes possible again. You might have to update multiple servers at this point.  
  
### To return a session to high-performance mode  
  
1.  Optionally, return to high-performance mode by using one of the following methods:  
  
    -   In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]: Change the **Operating mode** option to **High performance (asynchronous)** by using the [Mirroring Page](../../relational-databases/databases/database-properties-mirroring-page.md) of the **Database Properties** dialog box.  
  
    -   In [!INCLUDE[tsql](../../includes/tsql-md.md)]: Use [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)to set transaction safety to OFF.  
  
### To add a witness back into a mirroring session  
  
1.  Optionally, in high-safety mode, reestablish the witness to each mirroring session.  
  
     **To return a witness**  
  
    -   [Add or Replace a Database Mirroring Witness &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/add-or-replace-a-database-mirroring-witness-sql-server-management-studio.md)  
  
    -   [Add a Database Mirroring Witness Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/add-a-database-mirroring-witness-using-windows-authentication-transact-sql.md)  
  
## See Also  
 [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)   
 [Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md)   
 [ALTER DATABASE Database Mirroring &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [View the State of a Mirrored Database &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/view-the-state-of-a-mirrored-database-sql-server-management-studio.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)   
 [Force Service in a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/force-service-in-a-database-mirroring-session-transact-sql.md)   
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md)  
  
  
