---
title: "Use the Availability Group Wizard (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.newagwizard.f1"
  - "sql12.swb.newavgroupwiz.f1"
helpviewer_keywords: 
  - "New Availability Group Wizard, availability replicas"
  - "Availability Groups [SQL Server], wizards"
  - "Availability Groups [SQL Server], creating"
ms.assetid: e1f1dccc-9e65-471d-8fd1-b45085c9484a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Use the Availability Group Wizard (SQL Server Management Studio)
  This topic describes how to use the New Availability Group Wizard (in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]) to create and configure an AlwaysOn availability group in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. An *availability group* defines a set of user databases that will fail over as a single unit and a set of failover partners, known as *availability replicas*, that support failover.  
  
> [!NOTE]  
>  For an introduction to availability groups, see [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md).  
  
-   **Before you begin:**  
  
     [Prerequisites, Restrictions, and Recommendations](#PrerequisitesRestrictions)  
  
     [Security](#Security)  
  
-   **To create and configure an availability group, using:**  [New Availability Group Wizard (SQL Server Management Studio)](#RunAGwiz)  
  
> [!NOTE]  
>  As an alternative to using the New Availability Group Wizard, you can use [!INCLUDE[tsql](../../../includes/tsql-md.md)] or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PowerShell cmdlets. For more information, see [Create an Availability Group &#40;Transact-SQL&#41;](create-an-availability-group-transact-sql.md) or [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../powershell/sql-server-powershell.md).  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 We strongly recommend that you read this section before attempting to create your first availability group.  
  
###  <a name="PrerequisitesRestrictions"></a> Prerequisites, Restrictions, and Recommendations  
 In most cases, you can use the New Availability Group Wizard to complete all of the tasks require to create and configure an availability group. However, you might need to complete some of the tasks manually.  
  
-   Before creating an availability group, verify that the instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that host availability replicas reside on different Windows Server Failover Clustering (WSFC) node within the same WSFC failover cluster. Also, verify that each of the server instance meets all other [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] prerequisites. For more information, we strongly recommend that you read [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-restrictions-recommendations-always-on-availability.md).  
  
-   If a server instance that you select to host an availability replica is running under a domain user account and does not yet have a database mirroring endpoint, the wizard can create the endpoint and grant CONNECT permission to the server instance service account. However, if the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is running as a built-in account, such as Local System, Local Service, or Network Service, or a nondomain account, you must use certificates for endpoint authentication, and the wizard will be unable to create a database mirroring endpoint on the server instance. In this case, we recommend that you create the database mirroring endpoints manually before you launch the New Availability Group Wizard.  
  
     `To use certificates for a database mirroring endpoint:`  
  
     [CREATE ENDPOINT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-endpoint-transact-sql)  
  
     [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
-   SQL Server Failover Cluster Instances (FCIs) do not support automatic failover by availability groups, so any availability replica that is hosted by an FCI can only be configured for manual failover.  
  
-   If a database is encrypted or even contains a Database Encryption Key (DEK), you cannot use the [!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)] or [!INCLUDE[ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)] to add the database to an availability group. Even if an encrypted database has been decrypted, its log backups might contain encrypted data. In this case, full initial data synchronization could fail on the database. This is because the restore log operation might require the certificate that was used by the database encryption keys (DEKs), and that certificate might be unavailable.  
  
     To make a decrypted database eligible to add to an availability group using the wizard:  
  
    1.  Create a log backup of the primary database.  
  
    2.  Create a full database backup of the primary database.  
  
    3.  Restore the database backup on the server instance that hosts the secondary replica.  
  
    4.  Create a new log backup from primary database.  
  
    5.  Restore this log backup on the secondary database.  
  
-   **Prerequisites for the wizard to perform full initial data synchronization**  
  
    -   All the database-file paths must be identical on every server instance that hosts a replica for the availability group.  
  
    -   No primary database name can exist on any server instance that hosts a secondary replica. This means that none of the new secondary databases can exist yet.  
  
    -   You will need to specify a network share in order for the wizard to create and access backups. For the primary replica, the account used to start the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] must have read and write file-system permissions on a network share. For secondary replicas, the account must have read permission on the network share.  
  
        > [!IMPORTANT]  
        >  The log backups will be part of your log backup chain. Store the log backup files appropriately.  
  
     If you are unable to use the wizard to perform full initial data synchronization, you need to prepare your secondary databases manually. You can do this before or after running the wizard. For more information, see [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
 Also requires CONTROL ON ENDPOINT permission if you want to allow Availability Group Wizard to manage the database mirroring endpoint.  
  
##  <a name="RunAGwiz"></a> Using the New Availability Group Wizard  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica.  
  
2.  Expand the **AlwaysOn High Availability** node and the **Availability Groups** node.  
  
3.  To launch the New Availability Group Wizard, select the **New Availability Group Wizard** command.  
  
4.  The first time you run this wizard, an **Introduction** page appears. To bypass this page in the future, you can click **Do not show this page again**. After reading this page, click **Next**.  
  
5.  On the **Specify Availability Group Name** page, enter the name of the new availability group in the **Availability group name** field. This name must be a valid [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] identifier that is unique on the WSFC failover cluster and in your domain as a whole. The maximum length for an availability group name is 128 characters.  
  
6.  On the **Select Databases** page, the grid lists user databases on the connected server instance that are eligible to become the *availability databases*. Select one or more of the listed databases to participate in the new availability group. These databases will initially be the initial *primary databases*.  
  
     For each listed database, the **Size** column displays the database size, if known. The **Status** column indicates whether a given database meets the [prerequisites](prereqs-restrictions-recommendations-always-on-availability.md)for availability databases. It the prerequisites are not met, a brief status description indicates the reason that the database is ineligible; for example, if it does not use the full recovery model. For more information, click the status description.  
  
     If you change a database to make it eligible, click **Refresh** to update the databases grid.  
  
7.  On the **Specify Replicas** page, specify and configure one or more replicas for the new availability group. This page contains four tabs. The following table introduces these tabs. For more information, see the [Specify Replicas Page &#40;New Availability Group Wizard: Add Replica Wizard&#41;](specify-replicas-page-new-availability-group-wizard-add-replica-wizard.md) topic.  
  
    |Tab|Brief Description|  
    |---------|-----------------------|  
    |**Replicas**|Use this tab to specify each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that will host a secondary replica. Note that the server instance to which you are currently connected must host the primary replica.|  
    |**Endpoints**|Use this tab to verify any existing database mirroring endpoints and also, if this endpoint is lacking on a server instance whose service accounts use Windows Authentication, to create the endpoint automatically. **Note:**  If any server instance is running under a non-domain user account, you need to do make a manual change to your server instance before you can proceed in the wizard. For more information, see [Prerequisites](#PrerequisitesRestrictions), earlier in this topic.|  
    |**Backup Preferences**|Use this tab to specify your backup preference for the availability group as a whole and your backup priorities for the individual availability replicas.|  
    |**Listener**|Use this tab to create an availability group listener. By default, the wizard does not create a listener.|  
  
8.  On the **Select Initial Data Synchronization** page, choose how you want your new secondary databases to be created and joined to the availability group. Choose one of the following options:  
  
    -   **Full**  
  
         Select this option if your environment meets the requirements for automatically starting initial data synchronization (for more information, see [Prerequisites, Restrictions, and Recommendations](#PrerequisitesRestrictions), earlier in this topic).  
  
         If you select **Full**, after creating the availability group, the wizard will back up every primary database and its transaction log to a network share and restore the backups on every server instance that hosts an secondary replica. The wizard will then join every secondary database to the availability group.  
  
         In the **Specify a shared network location accessible by all replicas:** field, specify a backup share to which all of the server instance that host replicas have read-write access. For more information, see [Prerequisites](#PrerequisitesRestrictions), earlier in this topic.  
  
    -   **Join only**  
  
         If you have manually prepared secondary databases on the server instances that will host the secondary replicas, you can select this option. The wizard will join the existing secondary databases to the availability group.  
  
    -   **Skip initial data synchronization**  
  
         Select this option if you want to use your own database and log backups of your primary databases. For more information, see [Start Data Movement on an AlwaysOn Secondary Database &#40;SQL Server&#41;](start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
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
  
-   [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md)  
  
 **Alternative ways to create an availability group**  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Create an Availability Group &#40;Transact-SQL&#41;](create-an-availability-group-transact-sql.md)  
  
-   [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../powershell/sql-server-powershell.md)  
  
 **To enable AlwaysOn Availability Groups**  
  
-   [Enable and Disable AlwaysOn Availability Groups &#40;SQL Server&#41;](enable-and-disable-always-on-availability-groups-sql-server.md)  
  
 **To configure a database mirroring endpoint**  
  
-   [Create a Database Mirroring Endpoint for AlwaysOn Availability Groups &#40;SQL Server PowerShell&#41;](database-mirroring-always-on-availability-groups-powershell.md)  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
 **To troubleshoot AlwaysOn Availability Groups configuration**  
  
-   [Troubleshoot AlwaysOn Availability Groups Configuration &#40;SQL Server&#41;deleted](troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
-   [Troubleshoot a Failed Add-File Operation &#40;AlwaysOn Availability Groups&#41;](troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [AlwaysON - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](https://blogs.msdn.com/b/psssql/archive/2012/05/17/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases.aspx)  
  
     [SQL Server AlwaysOn Team Blogs: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Videos:**  
  
     [Microsoft SQL Server Code-Named "Denali" AlwaysOn Series,Part 1: Introducing the Next Generation High Availability Solution](http://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI302)  
  
     [Microsoft SQL Server Code-Named "Denali" AlwaysOn Series,Part 2: Building a Mission-Critical High Availability Solution Using AlwaysOn](http://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI404)  
  
-   **Whitepapers:**  
  
     [Microsoft SQL Server AlwaysOn Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](http://sqlcat.com/)  
  
## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-mirroring/the-database-mirroring-endpoint-sql-server.md)   
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-restrictions-recommendations-always-on-availability.md)  
  
  
