---
title: "Prerequisites, Restrictions, and Recommendations for availability groups"
description: "A description of the prerequisites, restrictions and recommendations for deploying an Always On availability group."
ms.custom: "seodec18"
ms.date: "03/22/2019"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], server instance"
  - "Availability Groups [SQL Server], deploying"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "Availability Groups [SQL Server], about"
  - "Availability Groups [SQL Server], prerequisites and restrictions"
  - "Availability Groups [SQL Server], Failover Cluster Instances"
  - "Availability Groups [SQL Server], databases"
  - "Availability Groups [SQL Server]"
ms.assetid: edbab896-42bb-4d17-8d75-e92ca11f7abb
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Prerequisites, Restrictions, and Recommendations for Always On availability groups
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This article describes considerations for deploying [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], including prerequisites, restrictions, and recommendations for host computers, Windows Server failover clusters (WSFC), server instances, and availability groups. For each of these components security considerations and required permissions, if any, are indicated.  
  
> [!IMPORTANT]  
>  Before you deploy [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], we strongly recommend that you read every section of this topic.  
    
##  <a name="DotNetHotfixes"></a> .Net Hotfixes that Support Availability Groups  
 Depending on the [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] components and features you will use with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], you may need to install additional .Net hotfixes identified in the following table. The hotfixes can be installed in any order.  
  
||Dependent Feature|Hotfix|Link|  
|------|-----------------------|------------|----------|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|[!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]|Hotfix for .Net 3.5 SP1 adds support to SQL Client for Always On features of Read-intent, readonly, and multisubnetfailover. The hotfix needs to be installed on each [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report server.|KB 2654347: [Hotfix for .Net 3.5 SP1 to add support for Always On features](https://go.microsoft.com/fwlink/?LinkId=242896)|  
  

###  <a name="SystemRequirements"></a> Checklist: Requirements (Windows System)  
 To support the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature, ensure that every computer that is to participate in one or more availability groups meets the following fundamental requirements:  
  
||Requirement|Link|  
|------|-----------------|----------|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Ensure that the system is not a domain controller.|Availability groups are not supported on domain controllers.|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Ensure that each computer is running Windows Server 2012 or later versions.|[Hardware and Software Requirements for Installing SQL Server 2016](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Ensure that each computer is a node in a WSFC.|[Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Ensure that the WSFC contains sufficient nodes to support your availability group configurations.|A cluster node can host one replica for an availability group. The same node cannot host two replicas from the same availability group. The cluster node can participate in multiple availability groups, with one replica from each group. <br /><br /> Ask your database administrators how many cluster nodes are required for to support the availability replicas of the planned availability groups.<br /><br /> [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).|  

  
> [!IMPORTANT]  
>  Also ensure that your environment is correctly configured for connecting to an availability group. For more information, see [Always On Client Connectivity &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-client-connectivity-sql-server.md).  
  
###  <a name="ComputerRecommendations"></a> Recommendations for Computers That Host Availability Replicas (Windows System)  
  
-   **Comparable systems:**  For a given availability group, all the availability replicas should run on comparable systems that can handle identical workloads.  
  
-   **Dedicated network adapters:**  For best performance, use a dedicated network adapter (network interface card) for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)].  
  
-   **Sufficient disk space:**  Every computer on which a server instance hosts an availability replica must possess sufficient disk space for all the databases in the availability group. Keep in mind that as primary databases grow, their corresponding secondary databases grow the same amount.  
  
###  <a name="PermissionsWindows"></a> Permissions (Windows System)  
 To administer a WSFC, the user must be a system administrator on every cluster node.  
  
 For more information about the account for administering the cluster, see [Appendix A: Failover Cluster Requirements](https://technet.microsoft.com/library/dd197454.aspx).  
  
###  <a name="RelatedTasksWindows"></a> Related Tasks (Windows System)  
  
|Task|Link|  
|----------|----------|  
|Set the HostRecordTTL value.|[Change the HostRecordTTL (Using Windows PowerShell)](#ChangeHostRecordTTLps)|  
  
####  <a name="ChangeHostRecordTTLps"></a> Change the HostRecordTTL (Using Windows PowerShell)  
  
1.  Open PowerShell window via **Run as Administrator**.  
  
2.  Import the FailoverClusters module.  
  
3.  Use the **Get-ClusterResource** cmdlet to find the Network Name resource, then use **Set-ClusterParameter** cmdlet to set the **HostRecordTTL** value, as follows:  
  
     Get-ClusterResource "*\<NetworkResourceName>*" | Set-ClusterParameter HostRecordTTL *\<TimeInSeconds>*  
  
     The following PowerShell example sets the HostRecordTTL to 300 seconds for a Network Name resource named `SQL Network Name (SQL35)`.  
  
    ```  
    Import-Module FailoverClusters  
  
    $nameResource = "SQL Network Name (SQL35)"  
    Get-ClusterResource $nameResource | Set-ClusterParameter ClusterParameter HostRecordTTL 300  
    ```  
  
    > [!TIP]  
    >  Every time you open a new PowerShell window, you need to import the **FailoverClusters** module.  
  
##### Related Content (PowerShell)  
  
-   [Clustering and High-Availability](https://blogs.msdn.com/b/clustering/archive/2009/05/23/9636665.aspx) (Failover Clustering and Network Load Balancing Team Blog)  
  
-   [Getting Started with Windows PowerShell on a Failover Cluster](https://technet.microsoft.com/library/ee619762\(WS.10\).aspx)  
  
-   [Cluster resource commands and equivalent Windows PowerShell cmdlets](https://msdn.microsoft.com/library/ee619744.aspx#BKMK_resource)  
  
###  <a name="RelatedContentWS"></a> Related Content (Windows System)  
  
-   [Configure DNS settings in a Multi-Site Failover Cluster](https://technet.microsoft.com/library/dd197562\(WS.10\).aspx)  
  
-   [DNS Registration with Network Name Resource](https://blogs.msdn.com/b/clustering/archive/2009/07/17/9836756.aspx)  
  

##  <a name="ServerInstance"></a> SQL Server Instance Prerequisites and Restrictions  
 Each availability group requires a set of failover partners, known as *availability replicas*, which are hosted by instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. A given server instance can be a *stand-alone instance* or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]*failover cluster instance* (FCI).  
  
 **In This Section:**  
  
-   [Checklist: Prerequisites](#PrerequisitesSI)  
  
-   [Thread Usage by Availability Groups](#ThreadUsage)  
  
-   [Permissions](#PermissionsSI)  
  
-   [Related Tasks](#RelatedTasksSI)  
  
-   [Related Content](#RelatedContentSI)  
  
###  <a name="PrerequisitesSI"></a> Checklist: Prerequisites (Server Instance)  
  
||Prerequisite|Links|  
|-|------------------|-----------|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|The host computer must be a WSFC node. The instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that host availability replicas for a given availability group reside on separate nodes of the cluster. An availability group can temporarily straddle two clusters while being migrated to different cluster. SQL Server 2016 introduces distributed availability groups. In a distributed availability group two availability groups reside on different clusters.|[Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)<br /><br /> [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)<br/> <br/> [Distributed Availability Groups (Always On Availability Groups)](../../../database-engine/availability-groups/windows/distributed-availability-groups-always-on-availability-groups.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|If you want an availability group to work with Kerberos:<br /><br /> All server instances that host an availability replica for the availability group must use the same SQL Server service account.<br /><br /> The domain administrator needs to manually register a Service Principal Name (SPN) with Active Directory on the SQL Server service account for the virtual network name (VNN) of the availability group listener. If the SPN is registered on an account other than the SQL Server service account, authentication will fail.<br /><br /> <br /><br /> <b>\*\* Important \*\*</b> If you change the SQL Server service account, the domain administrator will need to manually re-register the SPN.|[Register a Service Principal Name for Kerberos Connections](../../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md)<br /><br /> **Brief explanation:**<br /><br /> Kerberos and SPNs enforce mutual authentication. The SPN maps to the Windows account that starts the SQL Server services. If the SPN is not registered correctly or if it fails, the Windows security layer cannot determine the account associated with the SPN, and Kerberos authentication cannot be used.<br /><br /> <br /><br /> Note: NTLM does not have this requirement.|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|If you plan to use a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance (FCI) to host an availability replica, ensure that you understand the FCI restrictions and that the FCI requirements are met.|[Prerequisites and Requirements on Using a SQL Server Failover Cluster Instance (FCI) to Host an Availability Replica](#FciArLimitations) (later in this article)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Each server instance must be running the same version of SQL Server to participate in an Always On Availability Group.|Editions and supported features for [SQL 2014](https://docs.microsoft.com/sql/getting-started/features-supported-by-the-editions-of-sql-server-2014?view=sql-server-2014), [SQL 2016](https://docs.microsoft.com/sql/sql-server/editions-and-components-of-sql-server-2016?view=sql-server-2016), [SQL 2017](https://docs.microsoft.com/sql/sql-server/editions-and-components-of-sql-server-2017?view=sql-server-2017).|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|All the server instances that host availability replicas for an availability group must use the same [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] collation.|[Set or Change the Server Collation](../../../relational-databases/collations/set-or-change-the-server-collation.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Enable the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature on each server instance that will host an availability replica for any availability group. On a given computer, you can enable as many server instances for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] as your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation supports.|[Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)<br /><br /> <br /><br /> <b>\*\* Important \*\*</b> If you destroy and re-create a WSFC, you must disable and re-enable the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature on each server instance that was enabled for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] on the original cluster.|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Each server instance requires a database mirroring endpoint. Note that this endpoint is shared by all the availability replicas and database mirroring partners and witnesses on the server instance.<br /><br /> If a server instance that you select to host an availability replica is running under a domain user account and does not yet have a database mirroring endpoint, the [New Availability Group Wizard](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md) (or [Add Replica to Availability Group Wizard](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)) can create the endpoint and grant CONNECT permission to the server instance service account. However, if the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service is running as a built-in account, such as Local System, Local Service, or Network Service, or a nondomain account, you must use certificates for endpoint authentication, and the wizard will be unable to create a database mirroring endpoint on the server instance. In this case, we recommend that you create the database mirroring endpoints manually before you launch the wizard.<br /><br /> <br /><br /> <b>\*\* Security Note \*\*</b> Transport security for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] is the same as for database mirroring.|[The Database Mirroring Endpoint &#40;SQL Server&#41;](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)<br /><br /> [Transport Security for Database Mirroring and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|If any databases that use FILESTREAM will be added to an availability group, ensure that FILESTREAM is enabled on every server instance that will host an availability replica for the availability group.|[Enable and Configure FILESTREAM](../../../relational-databases/blob/enable-and-configure-filestream.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|If any contained databases will be added to an availability group, ensure that the **contained database authentication** server option is set to **1** on every server instance that will host an availability replica for the availability group.|[contained database authentication Server Configuration Option](../../../database-engine/configure-windows/contained-database-authentication-server-configuration-option.md)<br /><br /> [Server Configuration Options &#40;SQL Server&#41;](../../../database-engine/configure-windows/server-configuration-options-sql-server.md)|  
  
###  <a name="ThreadUsage"></a> Thread Usage by Availability Groups  
 [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] has the following requirements for worker threads:  
  
-   On an idle instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] uses 0 threads.  
  
-   The maximum number of threads used by availability groups is the configured setting for the maximum number of server threads ('**max worker threads**') minus 40.  
  
-   The availability replicas hosted on a given server instance share a single thread pool.  
  
     Threads are shared on an on-demand basis, as follows:  
  
    -   Typically, there are 3-10 shared threads, but this number can increase depending on the primary replica workload.  
  
    -   If a given thread is idle for a while, it is released back into the general [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] thread pool. Normally, an inactive thread is released after ~15 seconds of inactivity. However, depending on the last activity, an idle thread might be retained longer.  

    -   A SQL Server instance uses up to 100 threads for parallel redo for secondary replicas. Each database uses up to one-half of the total number of CPU cores, but not more than 16 threads per database. If the total number of required threads for a single instance exceeds 100, SQL Server uses a single redo thread for every remaining database. Serial Redo threads are released after ~15 seconds of inactivity. 
    
    > [!NOTE]
    > Databases are chosen to go single-threaded based on their ascending database ID. As such, the database creation order should be considered for SQL Server instances that host more availability group databases than available worker threads. For example, on a system with 32 or more CPU cores, the first six databases (ordered by database ID) in an availability group or groups will use parallel redo mode, and all subsequent databases will use single redo mode.
  
-   In addition, availability groups use unshared threads, as follows:  
  
    -   Each primary replica uses 1 Log Capture thread for each primary database. In addition, it uses 1 Log Send thread for each secondary database. Log send threads are released after ~15 seconds of inactivity.    
  
    -   A backup on a secondary replica holds a thread on the primary replica for the duration of the backup operation.  
  
 For more information, see [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](https://blogs.msdn.com/b/psssql/archive/2012/05/17/Always%20On-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases.aspx) (a CSS [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Engineers Blog).  
  
###  <a name="PermissionsSI"></a> Permissions (Server Instance)  
  
|Task|Required Permissions|  
|----------|--------------------------|  
|Creating the database mirroring endpoint|Requires CREATE ENDPOINT permission, or membership in the **sysadmin** fixed server role.  Also requires CONTROL ON ENDPOINT permission. For more information, see [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../../t-sql/statements/grant-endpoint-permissions-transact-sql.md).|  
|Enabling [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]|Requires membership in the **Administrator** group on the local computer and full control on the WSFC.|  
  
###  <a name="RelatedTasksSI"></a> Related Tasks (Server Instance)  
  
|Task|Article|  
|----------|-----------|  
|Determining whether database mirroring endpoint exists|[sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)|  
|Creating the database mirroring endpoint (if it does not yet exist)|[Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)<br /><br /> [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)<br /><br /> [Create a Database Mirroring Endpoint for Always On Availability Groups &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md)|  
|Enabling Availability Groups|[Enable and Disable Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md)|  
  
###  <a name="RelatedContentSI"></a> Related Content (Server Instance)  
  
-   [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](https://blogs.msdn.com/b/psssql/archive/2012/05/17/Always%20On-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases.aspx)  
  
##  <a name="NetworkConnect"></a> Network Connectivity Recommendations  
 We strongly recommend that you use the same network links for communications between WSFC nodes and communications between availability replicas.  Using separate network links can cause unexpected behaviors if some of links fail (even intermittently).  
  
 For example, for an availability group to support automatic failover, the secondary replica that is the automatic-failover partner must be in the SYNCHRONIZED state. If the network link to this secondary replica fails (even intermittently), the replica enters the UNSYNCHRONIZED state and cannot begin to resynchronize until the link is restored. If the WSFC requests an automatic failover while the secondary replica is unsynchronized, automatic failover will not occur.  
  
##  <a name="ClientConnSupport"></a> Client Connectivity Support  
 For information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] support for client connectivity, see [Always On Client Connectivity &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-client-connectivity-sql-server.md).  
  
##  <a name="FciArLimitations"></a> Prerequisites and Restrictions for Using a SQL Server Failover Cluster Instance (FCI) to Host an Availability Replica  
 **In This Section:**  
  
-   [Restrictions](#RestrictionsFCI)  
  
-   [Checklist: Prerequisites](#PrerequisitesFCI)  
  
-   [Related Tasks](#RelatedTasksFCIs)  
  
-   [Related Content](#RelatedContentFCIs)  
  
###  <a name="RestrictionsFCI"></a> Restrictions (FCIs)  
  
> [!NOTE]  
> Failover Cluster Instances supports Clustered Shared Volumes (CSV). For more information on CSV, see [Understanding Cluster Shared Volumes in a Failover Cluster](https://technet.microsoft.com/library/dd759255.aspx).  
  
-   **The cluster nodes of an FCI can host only one replica for a given availability group:**  If you add an availability replica on an FCI, the WSFC nodes that are possible FCI owners cannot host another replica for the same availability group.  To avoid possible conflicts, it is recommended to configure possible owners for the failover cluster instance. This will prevent potentially causing a single WSFC from attempting to host two availability replicas for the same availability group.
  
     Furthermore, every other replica must be hosted by an instance of SQL Server 2016 that resides on a different cluster node in the same Windows Server failover cluster. The only exception is that while being migrated to another cluster, an availability group can temporarily straddle two clusters. 

  >[!WARNING]
  > Using the Failover Cluster Manager to move a *failover cluster instance* hosting an availability group to a node that is *already* hosting a replica of the same availability group may result in the loss of the availability group replica, preventing it from being brought online on the target node. A single node of a failover cluster cannot host more than one replica for the same availability group. For more information on  how this occurs, and how to recover, see the blog [Replica unexpectedly dropped in availability group](https://blogs.msdn.microsoft.com/alwaysonpro/2014/02/03/issue-replica-unexpectedly-dropped-in-availability-group/). 

  
-   **FCIs do not support automatic failover by availability groups:**  FCIs do not support automatic failover by availability groups, so any availability replica that is hosted by an FCI can be configured for manual failover only.  
  
-   **Changing FCI network name:**  If you need to change the network name of an FCI that hosts an availability replica, you will need to remove the replica from its availability group and then add the replica back into the availability group. You cannot remove the primary replica, so if you are renaming an FCI that is hosting the primary replica, you should fail over to a secondary replica and then remove the former primary replica and add it back. Note that renaming an FCI might alter the URL of its database mirroring endpoint. When you add the replica ensure that you specify the current endpoint URL.  
  
###  <a name="PrerequisitesFCI"></a> Checklist: Prerequisites (FCIs)  
  
||Prerequisite|Link|  
|-|------------------|----------|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Ensure that each SQL Server failover cluster instance (FCI) possesses the required shared storage as per standard SQL Server failover cluster instance installation.||  
  
###  <a name="RelatedTasksFCIs"></a> Related Tasks (FCIs)  
  
|Task|Article|  
|----------|-----------|  
|Installing a SQL Server Failover Cluster|[Create a New SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)|  
|In-place upgrade of your existing SQL Server Failover Cluster|[Upgrade a SQL Server Failover Cluster Instance &#40;Setup&#41;](../../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance-setup.md)|  
|Maintaining your existing SQL Server Failover Cluster|[Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md)|  
  
###  <a name="RelatedContentFCIs"></a> Related Content (FCIs)  
  
-   [Failover Clustering and Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)  
  
-   [Always On Architecture Guide: Building a High Availability and Disaster Recovery Solution by Using Failover Cluster Instances and Availability Groups](https://technet.microsoft.com/library/jj215886.aspx)  
  
##  <a name="PrerequisitesForAGs"></a> Availability Group Prerequisites and Restrictions  
 **In This Section:**  
  
-   [Restrictions](#RestrictionsAG)  
  
-   [Requirements](#RequirementsAG)  
  
-   [Security](#SecurityAG)  
  
-   [Related Tasks](#RelatedTasksAGs)  
  
###  <a name="RestrictionsAG"></a> Restrictions (Availability Groups)  
  
-   **Availability replicas must be hosted by different nodes of one WSFC:**  For a given availability group, availability replicas must be hosted by server instances running on different nodes of the same WSFC. The only exception is that while being migrated to another cluster, an availability group can temporarily straddle two clusters.  
  
    > [!NOTE]  
    >  Virtual machines on the same physical computer can each host an availability replica for the same availability group because each virtual machine acts as a separate computer.  
  
-   **Unique availability group name:**  Each availability group name must be unique on the WSFC. The maximum length for an availability group name is 128 characters.  
  
-   **Availability replicas:**  Each availability group supports one primary replica and up to eight secondary replicas. All of the replicas can run under asynchronous-commit mode, or up to three of them can run under synchronous-commit mode (one primary replica with two synchronous secondary replicas).  
  
-   **Maximum number of availability groups and availability databases per computer:** The actual number of databases and availability groups you can put on a computer (VM or physical) depends on the hardware and workload, but there is no enforced limit. Microsoft has tested up to 10 AGs and 100 DBs per physical machine, however this is not a binding limit. Depending on the hardware specification on the server and the workload, you can put a higher number of databases and availability groups on an instance of SQL Server. Signs of overloaded systems can include, but are not limited to, worker thread exhaustion, slow response times for availability group system views and DMVs, and/or stalled dispatcher system dumps. Please make sure to thoroughly test your environment with a production-like workload to ensure it can handle peak workload capacity within your application SLAs. When considering SLAs be sure to consider load under failure conditions as well as expected response times.  
  
-   **Do not use the Failover Cluster Manager to manipulate availability groups:**  
  
     For example:  
  
    -   Do not change any availability group properties, such as the possible owners.  
  
    -   Do not use the Failover Cluster Manager to fail over availability groups. You must use [!INCLUDE[tsql](../../../includes/tsql-md.md)] or [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
  
###  <a name="RequirementsAG"></a> Prerequisites (Availability Groups)  
 When creating or reconfiguring an availability group configuration, ensure that you adhere to the following requirements.  
  
||Prerequisite|Description|  
|-|------------------|-----------------|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|If you plan to use a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance (FCI) to host an availability replica, ensure that you understand the FCI restrictions and that the FCI requirements are met.|[Prerequisites and Restrictions for Using a SQL Server Failover Cluster Instance (FCI) to Host an Availability Replica](#FciArLimitations) (earlier in this article)|  
  
###  <a name="SecurityAG"></a> Security (Availability Groups)  
  
-   Security is inherited from the WSFC. Windows Server failover clustering provides two levels of user security at the granularity of entire cluster:  
  
    -   Read-only access  
  
    -   Full control  
  
         [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] need full control, and enabling [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] gives it full control of the cluster (through Service SID).  
  
         You cannot directly add or remove security for a server instance in Cluster Manager. To manage cluster security sessions, use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager or the WMI equivalent from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   Each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] must have permissions to access the registry, cluster, and so forth.  
  
-   We recommend that you use encryption for connections between server instances that host [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] availability replicas.  
  
#### Permissions (Availability Groups)  
  
|Task|Required Permissions|  
|----------|--------------------------|  
|Creating an availability group|Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.|  
|Altering an  availability group|Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.<br /><br /> In addition, joining a database to an availability group requires membership in the **db_owner** fixed database role.|  
|Dropping/deleting an availability group|Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission. To drop an availability group that is not hosted on the local replica location you need CONTROL SERVER permission or CONTROL permission on that Availability Group.|  
  
###  <a name="RelatedTasksAGs"></a> Related Tasks (Availability Groups)  
  
|Task|Article|  
|----------|-----------|  
|Creating an availability group|[Use the Availability Group (New Availability Group Wizard)](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md)<br /><br /> [Create an Availability Group (Transact-SQL)](../../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md)<br /><br /> [Create an Availability Group (SQL Server PowerShell)](../../../database-engine/availability-groups/windows/create-an-availability-group-sql-server-powershell.md)<br /><br /> [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md)|  
|Modifying the number of availability replicas|[Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md)<br /><br /> [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md)<br /><br /> [Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-replica-from-an-availability-group-sql-server.md)|  
|Creating an  availability group listener|[Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)|  
|Dropping an  availability group|[Remove an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-an-availability-group-sql-server.md)|  
  
##  <a name="PrerequisitesForDbs"></a> Availability Database Prerequisites and Restrictions  
 To be eligible to be added to an availability group, a database must meet the following prerequisites and restrictions.  
  
 **In This Section:**  
  
-   [Requirements](#RequirementsDb)  
  
-   [Restrictions](#RestrictionsDb)  
  
-   [Recommendations for Computers That Host Availability Replicas (Windows System](#TDEdbs)  
  
-   [Permissions](#PermissionsDbs)  
  
-   [Related Tasks](#RelatedTasksADb)  
  
###  <a name="RequirementsDb"></a> Checklist: Requirements (Availability Databases)  
 To be eligible to be added to an availability group, a database must:  
  
||Requirements|Link|  
|-|------------------|----------|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Be a user database. System databases cannot belong to an availability group.||  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Reside on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] where you create the availability group and be accessible to the server instance.||  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Be a read-write database. Read-only databases cannot be added to an availability group.|[sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**is_read_only** = 0)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Be a multi-user database.|[sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**user_access** = 0)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Not use AUTO_CLOSE.|[sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**is_auto_close_on** = 0)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Use the full recovery model (also known as, full recovery mode).|[sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**recovery_model** = 1)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Possess at least one full database backup.<br /><br /> Note: After setting a database to full recovery mode, a full backup is required to initiate the full-recovery log chain.|[Create a Full Database Backup &#40;SQL Server&#41;](../../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Not belong to any existing availability group.|[sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**group_database_id** = NULL)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Not be configured for database mirroring.|[sys.database_mirroring](../../../relational-databases/system-catalog-views/sys-database-mirroring-transact-sql.md) (If the database does not participate in mirroring, all columns prefixed with "mirroring_" are NULL.)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Before adding a database that uses FILESTREAM to an availability group, ensure that FILESTREAM is enabled on every server instance that hosts or will host an availability replica for the availability group.|[Enable and Configure FILESTREAM](../../../relational-databases/blob/enable-and-configure-filestream.md)|  
|![Checkbox](../../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Before adding a contained database to an availability group, ensure that the **contained database authentication** server option is set to **1** on every server instance that hosts or will host an availability replica for the availability group.|[contained database authentication Server Configuration Option](../../../database-engine/configure-windows/contained-database-authentication-server-configuration-option.md)<br /><br /> [Server Configuration Options &#40;SQL Server&#41;](../../../database-engine/configure-windows/server-configuration-options-sql-server.md)|  
  
> [!NOTE]  
>  [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] works with any supported database compatibility level.  
  
###  <a name="RestrictionsDb"></a> Restrictions (Availability Databases)  
  
-   If the file path (including the drive letter) of a secondary database differs from the path of the corresponding primary database, the following restrictions apply:  
  
    -   **[!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)]/[!INCLUDE[ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)]:**  The **Full** option is not supported (on the[Select Initial Data Synchronization Page](../../../database-engine/availability-groups/windows/select-initial-data-synchronization-page-always-on-availability-group-wizards.md) page),  
  
    -   **RESTORE WITH MOVE:**  To create the secondary databases, the database files must be RESTORED WITH MOVE on each instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts a secondary replica.  
  
    -   **Impact on add-file operations:**  A later add-file operation on the primary replica might fail on the secondary databases. This failure could cause the secondary databases to be suspended. This, in turn, causes the secondary replicas to enter the NOT SYNCHRONIZING state.  
  
        > [!NOTE]  
        >  For information about responding to a failed ad-file operation, see [Troubleshoot a Failed Add-File Operation &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md).  
  
-   You cannot drop a database that currently belongs to an availability group.  
  
###  <a name="TDEdbs"></a> Follow Up for TDE Protected Databases  
 If you use transparent data encryption (TDE), the certificate or asymmetric key for creating and decrypting other keys must be the same on every server instance that hosts an availability replica for the availability group. For more information, see [Move a TDE Protected Database to Another SQL Server](../../../relational-databases/security/encryption/move-a-tde-protected-database-to-another-sql-server.md).  
  
###  <a name="PermissionsDbs"></a> Permissions (Availability Databases)  
 Requires ALTER permission on the database.  
  
###  <a name="RelatedTasksADb"></a> Related Tasks (Availability Databases)  
  
|Task|Article|  
|----------|-----------|  
|Preparing a secondary database (manually)|[Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)|  
|Joining a secondary database to availability group (manually)|[Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)|  
|Modifying the number of availability databases|[Add a Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/availability-group-add-a-database.md)<br /><br /> [Remove a Secondary Database from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-database-from-an-availability-group-sql-server.md)<br /><br /> [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-primary-database-from-an-availability-group-sql-server.md)|  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
-   [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](https://blogs.msdn.microsoft.com/sqlalwayson/)  
  
-   [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](https://blogs.msdn.com/b/psssql/archive/2012/05/17/Always%20On-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases.aspx)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)   
 [Always On Client Connectivity &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-client-connectivity-sql-server.md)  
  
    
  
--------------------------------------------------  

