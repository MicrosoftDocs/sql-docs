---
title: "Configure availability group in SSMS"
description: Create and configure an Always On availability group with the 'New Availability Group Wizard' in SQL Server Management Studio (SSMS).
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.newagwizard.f1"
  - "sql13.swb.newavgroupwiz.f1"
helpviewer_keywords:
  - "New Availability Group Wizard, availability replicas"
  - "Availability Groups [SQL Server], wizards"
  - "Availability Groups [SQL Server], creating"
---
# Use the Availability Group Wizard (SQL Server Management Studio)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to use the **New Availability Group Wizard** in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] to create and configure an Always On availability group in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)]. An *availability group* defines a set of user databases that will fail over as a single unit and a set of failover partners, known as *availability replicas*, that support failover.  
  
> [!NOTE]  
>  For an introduction to availability groups, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
    
##  <a name="BeforeYouBegin"></a> Before You Begin  
 We strongly recommend that you read this section before attempting to create your first availability group.  
  
###  <a name="Prerequisites"></a> Prerequisites, Restrictions, and Recommendations  

In most cases, you can use the New Availability Group Wizard to complete all of the tasks require to create and configure an availability group. However, you might need to complete some of the tasks manually.  
  
- If you are using a Windows Server Failover Cluster (WSFC) cluster type to host availability group, verify that the instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that host the availability replicas reside on different cluster servers (or nodes) within the same WSFC. Also, verify that each of the server instances meets all other [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] prerequisites. For more information, we strongly recommend that you read [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
-   If a server instance that you select to host an availability replica is running under a domain user account and does not yet have a database mirroring endpoint, the wizard can create the endpoint and grant CONNECT permission to the server instance service account. However, if the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is running as a built-in account, such as Local System, Local Service, or Network Service, or a nondomain account, you must use certificates for endpoint authentication, and the wizard will be unable to create a database mirroring endpoint on the server instance. In this case, we recommend that you create the database mirroring endpoints manually before you launch the New Availability Group Wizard.  
  
     **To use certificates for a database mirroring endpoint:**  
  
     [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../../t-sql/statements/create-endpoint-transact-sql.md)  
  
     [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
-   SQL Server Failover Cluster Instances (FCIs) do not support automatic failover by availability groups, so any availability replica that is hosted by an FCI can only be configured for manual failover.  
  
-   **Prerequisites for the wizard to perform full initial data synchronization**  
  
    -   All the database-file paths must be identical on every server instance that hosts a replica for the availability group.  
  
    -   No primary database name can exist on any server instance that hosts a secondary replica. This means that none of the new secondary databases can exist yet.  
  
    -   You will need to specify a network share in order for the wizard to create and access backups. For the primary replica, the account used to start the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] must have read and write file-system permissions on a network share. For secondary replicas, the account must have read permission on the network share.  
  
        > [!IMPORTANT]  
        >  The log backups will be part of your log backup chain. Store the log backup files appropriately.  
  
     If you are unable to use the wizard to perform full initial data synchronization, you need to prepare your secondary databases manually. You can do this before or after running the wizard. For more information, see [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
 Also requires CONTROL ON ENDPOINT permission if you want to allow Availability Group Wizard to manage the database mirroring endpoint.  
  
##  <a name="RunAGwiz"></a> Using the New Availability Group Wizard  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  To launch the New Availability Group Wizard, select the **New Availability Group Wizard** command.  
  
4.  The first time you run this wizard, an **Introduction** page appears. To bypass this page in the future, you can click **Do not show this page again**. After reading this page, click **Next**.  
  
5.  On the **Specify Availability Group Options** page, enter the name of the new availability group in the **Availability group name** field. This name must be a valid [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] identifier that is unique on the cluster and in your domain as a whole. The maximum length for an availability group name is 128 characters.

6. Next, specify the cluster type. The possible cluster types depend on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] version and operating system. Choose either **WSFC**, **EXTERNAL**, or **NONE**. For details see [Specify Availability Group Name Page](specify-availability-group-name-page.md)
 
6.  On the **Select Databases** page, the grid lists user databases on the connected server instance that are eligible to become the *availability databases*. Select one or more of the listed databases to participate in the new availability group. These databases will initially be the initial *primary databases*.  
  
     For each listed database, the **Size** column displays the database size, if known. The **Status** column indicates whether a given database meets the [prerequisites](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)for availability databases. It the prerequisites are not met, a brief status description indicates the reason that the database is ineligible; for example, if it does not use the full recovery model. For more information, click the status description.  
  
     If you change a database to make it eligible, click **Refresh** to update the databases grid.  
  
     If the database contains a database master key, enter the password for the database master key in the **Password** column.  
  
7.  On the **Specify Replicas** page, specify and configure one or more replicas for the new availability group. This page contains four tabs. The following table introduces these tabs. For more information, see the [Specify Replicas Page &#40;New Availability Group Wizard: Add Replica Wizard&#41;](../../../database-engine/availability-groups/windows/specify-replicas-page-new-availability-group-wizard-add-replica-wizard.md) topic.  
  
    |Tab|Brief Description|  
    |---------|-----------------------|  
    |**Replicas**|Use this tab to specify each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that will host a secondary replica. Note that the server instance to which you are currently connected must host the primary replica.|  
    |**Endpoints**|Use this tab to verify any existing database mirroring endpoints and also, if this endpoint is lacking on a server instance whose service accounts use Windows Authentication, to create the endpoint automatically.<br /><br /> Note: If any server instance is running under a non-domain user account, you need to do make a manual change to your server instance before you can proceed in the wizard. For more information, see [Prerequisites](#Prerequisites), earlier in this topic.|  
    |**Backup Preferences**|Use this tab to specify your backup preference for the availability group as a whole and your backup priorities for the individual availability replicas.|  
    |**Listener**|Use this tab to create an availability group listener. By default, the wizard does not create a listener.|  
  
8.  On the **Select Initial Data Synchronization** page, choose how you want your new secondary databases to be created and joined to the availability group. Choose one of the following options:  
  
    -   **Automatic seeding**  
  
         SQL Server automatically creates the secondary replicas for every database in the group. Automatic seeding requires that the data and log file paths are the same on every SQL Server instance participating in the group. Available on [!INCLUDE[sssql16-md.md](../../../includes/sssql16-md.md)] and later. See [Automatically initialize Always On Availability group](automatically-initialize-always-on-availability-group.md).
    
    -   **Full database and log backup**  
  
         Select this option if your environment meets the requirements for automatically starting initial data synchronization (for more information, see [Prerequisites, Restrictions, and Recommendations](#Prerequisites), earlier in this topic).  
  
         If you select **Full**, after creating the availability group, the wizard will back up every primary database and its transaction log to a network share and restore the backups on every server instance that hosts an secondary replica. The wizard will then join every secondary database to the availability group.  
  
         In the **Specify a shared network location accessible by all replicas:** field, specify a backup share to which all of the server instance that host replicas have read-write access. For more information, see [Prerequisites](#Prerequisites), earlier in this topic.  In the validation step, the wizard will perform a test to make sure the provided network location is valid, the test will create a database on the primary replica named "BackupLocDb_" followed by a Guid and perform backup to the provided network location, then restore it on the secondary replicas. It is safe to delete this database along with its backup history and backup file in case the wizard failed to delete them.
  
    -   **Join only**  
  
         If you have manually prepared secondary databases on the server instances that will host the secondary replicas, you can select this option. The wizard will join the existing secondary databases to the availability group.  
  
    -   **Skip initial data synchronization**  
  
         Select this option if you want to use your own database and log backups of your primary databases. For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
9. The **Validation** page verifies whether the values you specified in this Wizard meet the requirements of the New Availability Group Wizard. To make a change, click **Previous** to return to an earlier wizard page to change one or more values. The click **Next** to return to the **Validation** page, and click **Re-run Validation**.  
  
10. On the **Summary** page, review your choices for the new availability group. To make a change, click **Previous** to return to the relevant page. After making the change, click **Next** to return to the **Summary** page.  
  
    > [!IMPORTANT]  
    >  When the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account of a server instance that will host a new availability replica does not already exist as a login, the New Availability Group Wizard needs to create the login. On the **Summary** page, the wizard displays the information for the login that is to be created. If you click **Finish**, the wizard creates this login for the SQL Server service account and grants the login CONNECT permission.  
  
     If you are satisfied with your selections, optionally click **Script** to create a script of the steps the wizard will execute. Then, to create and configure the new availability group, click **Finish**.  
  
11. The **Progress** page displays the progress of the steps for creating the availability group (configuring endpoints, creating the availability group, and joining the secondary replica to the group).  
  
12. When these steps complete, the **Results** page displays the result of each step. If all these steps succeed, the new availability group is completely configured. If any of the steps result in an error, you might need to manually complete the configuration or use a wizard for the failed step. For information about the cause of a given error, click the associated "Error" link in the **Result** column.  
  
     When the wizard completes, click **Close** to exit.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To complete availability group configuration**  
  
-   [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)  
  
 **Alternative ways to create an availability group**  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Create an Availability Group &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md)  
  
-   [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-sql-server-powershell.md)  
  
 **To enable Always On Availability Groups**  
  
-   [Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)  
  
 **To configure a database mirroring endpoint**  
  
-   [Create a Database Mirroring Endpoint for Always On Availability Groups &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md)  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
 **To troubleshoot Always On Availability Groups configuration**  
  
-   [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
-   [Troubleshoot a Failed Add-File Operation &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](/archive/blogs/psssql/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases)  
  
     [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](/archive/blogs/psssql/)  
  
-   **Whitepapers:**  
  
     [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)  
  
## Alternate ways to create availability groups

As an alternative to using the New Availability Group Wizard, you can use [!INCLUDE[tsql](../../../includes/tsql-md.md)] or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell cmdlets. For more information, see [Create an Availability Group &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md) or [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-sql-server-powershell.md).  


## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)  
