---
title: "Add replica to an availability group using wizard in SSMS - SQL Server"
ms.description: "Add a replica to an Always On availability group using the wizard found in SQL Server Management Studio."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.addreplicawizard.f1"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], wizards"
ms.assetid: 60d962b6-2af4-4394-9190-61939a102bc0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Add a replica to your Always On Availability group using the Availability Group Wizard in SQL Server Management
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use the **Add Replica to Availability Group Wizard** to help you a add new secondary replica to an existing Always On availability group.  
  
> [!NOTE]  
>  For information about using [!INCLUDE[tsql](../../../includes/tsql-md.md)] or PowerShell to add a secondary replica to an availability group, see [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md).  
    
##  <a name="BeforeYouBegin"></a> Before You Begin  
 If you have never added any availability replica to an availability group, see the "Server instances" and "Availability groups and replicas" sections in [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   You must be connected to the server instance that hosts the current primary replica.  
  
-   Before adding a secondary replica, verify that the host instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is in the same Windows Server Failover Cluster (WSFC) as the existing replicas but resides on a different cluster node. Also, verify that this server instance meets all other [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] prerequisites. For more information, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
-   If a server instance that you select to host an availability replica is running under a domain user account and does not yet have a database mirroring endpoint, the wizard can create the endpoint and grant CONNECT permission to the server instance service account. However, if the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is running as a built-in account, such as Local System, Local Service, or Network Service, or a nondomain account, you must use certificates for endpoint authentication, and the wizard will be unable to create a database mirroring endpoint on the server instance. In this case, we recommend that you create the database mirroring endpoints manually before you launch the Add Replica to Availability Group Wizard.  
  
     **To use certificates for a database mirroring endpoint:**  
  
     [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../../t-sql/statements/create-endpoint-transact-sql.md)  
  
     [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
-   **Prerequisites for using full initial data synchronization**  
  
    -   All the database-file paths must be identical on every server instance that hosts a replica for the availability group.  
  
    -   No primary database name can exist on any server instance that hosts a secondary replica. This means that none of the new secondary databases can exist yet.  
  
    -   You will need to specify a network share in order for the wizard to create and access backups. For the primary replica, the account used to start the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] must have read and write file-system permissions on a network share. For secondary replicas, the account must have read permission on the network share.  
  
     If you are unable to use the wizard to perform full initial data synchronization, you need to prepare your secondary databases manually. You can do this before or after running the wizard. For more information, see [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
 Also requires CONTROL ON ENDPOINT permission if you want to allow Add Replica to Availability Group Wizard to manage the database mirroring endpoint.  
  
##  <a name="SSMSProcedure"></a> Using the Add Replica to Availability Group Wizard (SQL Server Management Studio)  
 **To Use the Add Replica to Availability Group Wizard**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica of the availability group, and expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Right-click the availability group to which you are adding a secondary replica, and select the **Add Replica** command. This launches the Add Replica to Availability Group Wizard.  
  
4.  On the **Connect to Existing Secondary Replicas** page, connect to every secondary replica in the availability group. For more information, see [Connect to Existing Secondary Replicas Page &#40;Add Replica Wizard: Add Databases Wizard&#41;](../../../database-engine/availability-groups/windows/connect-to-existing-secondary-replicas-page.md).  
  
5.  On the **Specify Replicas** page, specify and configure one or more new secondary replicas for the availability group. This page contains three tabs. The following table introduces these tabs. For more information, see [Specify Replicas Page &#40;New Availability Group Wizard: Add Replica Wizard&#41;](../../../database-engine/availability-groups/windows/specify-replicas-page-new-availability-group-wizard-add-replica-wizard.md).  
  
    |Tab|Brief Description|  
    |---------|-----------------------|  
    |**Replicas**|Use this tab to specify each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that will host a new secondary replica.|  
    |**Endpoints**|Use this tab to verify the existing database mirroring endpoint, if any, for each new secondary replica. If this endpoint is lacking on a server instance whose service accounts use Windows Authentication, the wizard will attempt to create the endpoint automatically.<br /><br /> <br /><br /> Note: If any server instance is running under a non-domain user account, you need to do make a manual change to your server instance before you can proceed in the wizard. For more information, see [Prerequisites](#Prerequisites), earlier in this topic.|  
    |**Backup Preferences**|Use this tab to specify your backup preference for the availability group as a whole, if you wish to modify the current setting, and to specify your backup priorities for the individual availability replicas.|  
  
6.  If the selected replicas contain databases that have a database master key, enter the passwords for the database master keys in the **Password** column. The **Status** column indicates **Password required** for databases that have a database master key. **Next** is grayed out until the correct password is entered into the **Password** column. After you enter the passwords, click **Refresh**. If you entered the passwords correctly, the Status column indicates **Password entered**, and **Next** will become available.  
  
7.  On the **Select Initial Data Synchronization** page, choose how you want your new secondary databases to be created and joined to the availability group. Choose one of the following options:  
  
    -   **Full**  
  
         Select this option if your environment meets the requirements for automatically starting initial data synchronization (for more information, see [Prerequisites, Restrictions, and Recommendations](#Prerequisites), earlier in this topic).  
  
         If you select **Full**, after creating the availability group, the wizard will back up every primary database and its transaction log to a network share and restore the backups on every server instance that hosts a new secondary replica. The wizard will then join every new secondary database to the availability group.  
  
         In the **Specify a shared network location accessible by all replicas:** field, specify a backup share to which all of the server instance that host replicas have read-write access. The log backups will be part of your log backup chain. Store the log backup files appropriately.  
  
        > [!IMPORTANT]  
        >  For information about the required file-system permissions, see [Prerequisites](#Prerequisites), earlier in this topic.  
  
    -   **Join only**  
  
         If you have manually prepared secondary databases on the server instances that will host the new secondary replicas, you can select this option. The wizard will join these new secondary databases to the availability group.  
  
    -   **Skip initial data synchronization**  
  
         Select this option if you want to use your own database and log backups of your primary databases. For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
8.  The **Validation** page verifies whether the values you specified in this Wizard meet the requirements of the Add Replica to Availability Group Wizard. To make a change, click **Previous** to return to an earlier wizard page to change one or more values. The click **Next** to return to the **Validation** page, and click **Re-run Validation**.  
  
9. On the **Summary** page, review your choices for the new availability group. To make a change, click **Previous** to return to the relevant page. After making the change, click **Next** to return to the **Summary** page.  
  
     If you are satisfied with your selections, optionally click Script to create a script of the steps the wizard will execute. Then, to create and configure the new availability group, click **Finish**.  
  
10. The **Progress** page displays the progress of the steps for creating the availability group (configuring endpoints, creating the availability group, and joining the secondary replica to the group).  
  
11. When these steps complete, the **Results** page displays the result of each step. If all these steps succeed, the new availability group is completely configured. If any of the steps result in an error, you might need to manually complete the configuration. For information about the cause of a given error, click the associated "Error" link in the **Result** column.  
  
     When the wizard completes, click **Close** to exit.  
  
> [!IMPORTANT]  
>  After adding a replica, see the "Follow Up: After Adding a Replica" section in [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)   
 [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
  
