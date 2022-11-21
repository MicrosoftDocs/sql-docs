---
title: "Set up database mirroring (Windows Authentication)"
description: Learn how to set up a database mirroring session with Windows Authentication using SQL Server Management Studio.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "database mirroring [SQL Server], sessions"
---
# Establish Database Mirroring Session - Windows Authentication
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] instead.  
  
 To establish a database mirroring session and to modify the properties of database mirroring for a database, use the **Mirroring** page of the **Database Properties** dialog box.Before you use the **Mirroring** page to configure database mirroring, ensure that the following requirements have been met:  
  
-   The principal and mirror server instances must be running the same edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-either Standard or Enterprise. Also, we strongly recommend that they run on comparable systems that can handle identical workloads.  
  
    > [!NOTE]  
    >  A witness server instance is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
-   The mirror database must exist and be current.  
  
     Creating a mirror database requires restoring a recent backup of the principal database (using WITH NORECOVERY) on the mirror server instance. It also requires taking one or more log backups after the full backup and restoring them in sequence to the mirror database (using WITH NORECOVERY). For more information, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
-   If the server instances are running under different domain user accounts, each requires a login in the **master** database of the others. If the login does not exist, you must create it before configuring mirroring. For more information, see [Allow Network Access to a Database Mirroring Endpoint Using Windows Authentication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-allow-network-access-windows-authentication.md).  
  
### To configure database mirroring  
  
1.  After connecting to the principal server instance, in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and select the database to be mirrored.  
  
3.  Right-click the database, select **Tasks**, and then click **Mirror**. This opens the **Mirroring** page of the **Database Properties** dialog box.  
  
4.  To begin configuring mirroring, click the **Configure Security** button to launch the Configure Database Mirroring Security Wizard.  
  
    > [!NOTE]  
    >  During a database mirroring session, you can use this wizard only to add or change the witness server instance.  
  
5.  The Configure Database Mirroring Security Wizard automatically creates the database mirroring endpoint (if none exists) on each server instance, and enters the server network addresses in the field corresponding to the role of the server instance (**Principal**, **Mirror**, or **Witness**).  
  
    > [!IMPORTANT]  
    >  When creating an endpoint, the Configure Database Mirroring Security Wizard always uses Windows Authentication. Before you can use the wizard with certificate-based authentication, the mirroring endpoint must already have been configured to use certificates on each of the server instances. Also, all the fields of the wizard's **Service Accounts** dialog box must remain empty. For information about creating a database mirroring endpoint to use certificates, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md).  
  
6.  Optionally, change the operating mode. The availability of certain operating mode(s) depends on whether you have specified a TCP address for a witness. The options are as follows:  
  
    |Option|Witness?|Explanation|  
    |------------|--------------|-----------------|  
    |**High performance (asynchronous)**|Null (if exists, not used but the session requires a quorum)|To maximize performance, the mirror database always lags somewhat behind the principal database, never quite catching up. However, the gap between the databases is typically small. The loss of a partner has the following effect:<br /><br /> If the mirror server instance becomes unavailable, the principal continues.<br /><br /> If the principal server instance becomes unavailable, the mirror stops; but if the session has no witness (as recommended) or the witness is connected to the mirror server, the mirror server is accessible as a warm standby; the database owner can force service to the mirror server instance (with possible data loss).<br /><br /> <br /><br /> For more information, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).|  
    |**High safety without automatic failover (synchronous)**|No|All committed transactions are guaranteed to be written to disk on the mirror server.<br /><br /> Manual failover is possible when the partners are connected to each other and the database is synchronized.<br /><br /> The loss of a partner has the following effect:<br /><br /> If the mirror server instance becomes unavailable, the principal continues.<br /><br /> If the principal server instance becomes unavailable, the mirror stops but is accessible as a warm standby; the database owner can force service to the mirror server instance (with possible data loss).<br /><br /> <br /><br /> For more information, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).|  
    |**High safety with automatic failover (synchronous)**|Yes (required)|All committed transactions are guaranteed to be written to disk on the mirror server.<br /><br /> Availability is maximized by including a witness server instance to support automatic failover. Note that you can select the **High safety with automatic failover (synchronous)** option only if you have first specified a witness server address.<br /><br /> Manual failover is possible when the partners are connected to each other and the database is synchronized.<br /><br /> In the presence of a witness, the loss of a partner has the following effect:<br /><br /> If the principal server instance becomes unavailable, automatic failover occurs. The mirror server instance switches to the role of principal, and it offers its database as the principal database.<br /><br /> If the mirror server instance becomes unavailable, the principal continues.<br /><br /> <br /><br /> For more information, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).<br /><br /> **&#42;&#42; Important &#42;&#42;** If the witness becomes disconnected, the partners must be connected to each other for the database to be available. For more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/quorum-how-a-witness-affects-database-availability-database-mirroring.md).|  
  
7.  When all of the following conditions exist, click **Start Mirroring** to begin mirroring:  
  
    -   You are currently connected to the principal server instance.  
  
    -   Security has been configured correctly.  
  
    -   The fully-qualified TCP addresses of the principal and mirror server instances are specified (in the **Server network addresses** section).  
  
    -   If the operating mode is set to **High safety with automatic failover (synchronous)**, the fully-qualified TCP address of the witness server instance is also specified.  
  
8.  After mirroring begins, you can change the operating mode and save the change by clicking **OK**. Note that you can switch to high-safety mode with automatic failover only if you have first specified a witness server address.  
  
    > [!NOTE]  
    >  To remove the witness, delete its server network address from the **Witness** field. If you switch from high-safety mode with automatic failover to high-performance mode, the **Witness** field is automatically cleared.  
  
## See Also  
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)   
 [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md)   
 [Database Properties &#40;Mirroring Page&#41;](../../relational-databases/databases/database-properties-mirroring-page.md)   
 [Pause or Resume a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/pause-or-resume-a-database-mirroring-session-sql-server.md)   
 [Set Up a Mirror Database to Use the Trustworthy Property &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/set-up-a-mirror-database-to-use-the-trustworthy-property-transact-sql.md)   
 [Remove Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-database-mirroring-sql-server.md)   
 [Management of Logins and Jobs After Role Switching &#40;SQL Server&#41;](../../sql-server/failover-clusters/management-of-logins-and-jobs-after-role-switching-sql-server.md)   
 [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md)   
 [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md)   
 [Add or Replace a Database Mirroring Witness &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/add-or-replace-a-database-mirroring-witness-sql-server-management-studio.md)  
  
  
