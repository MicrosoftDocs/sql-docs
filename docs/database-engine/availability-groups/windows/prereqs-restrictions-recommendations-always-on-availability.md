---
title: "Availability group: Prerequisites, restrictions, and recommendations"
description: "A description of the prerequisites, restrictions, and recommendations for deploying an Always On availability group to SQL Server."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 10/24/2023
ms.service: sql
ms.subservice: availability-groups
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
---
# Prerequisites, restrictions, and recommendations for Always On availability groups

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes considerations for deploying [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)], including prerequisites, restrictions, and recommendations for host computers, Windows Server failover clusters (WSFC), server instances, and availability groups. For each of these components security considerations and required permissions, if any, are indicated.

> [!IMPORTANT]  
> Before you deploy [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)], we strongly recommend that you read every section of this topic.

## <a id="DotNetHotfixes"></a> .NET hotfixes that support availability groups

Depending on the [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] components and features you'll use with [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)], you might need to install additional .NET hotfixes identified in the following table. The hotfixes can be installed in any order.

| Dependent Feature | Hotfix | Link |
| --- | --- | --- |
| [!INCLUDE [ssRSnoversion](../../../includes/ssrsnoversion-md.md)] | Hotfix for .NET 3.5 SP1 adds support to SQL Client for Always On features of Read-intent, readonly, and multisubnetfailover. The hotfix needs to be installed on each [!INCLUDE [ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report server. | KB 2654347: [Hotfix for .NET 3.5 SP1 to add support for Always On features](https://support.microsoft.com/en-us/topic/an-update-introduces-support-for-the-alwayson-features-in-sql-server-2012-or-a-later-version-to-the-net-framework-3-5-sp1-d72a8aeb-523b-f41e-4384-e3cf646fe732) |

### <a id="SystemRequirements"></a> Checklist: Requirements (Windows system)

To support the [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] feature, ensure that every computer that is to participate in one or more availability groups meets the following fundamental requirements:

| Requirement | Link |
| --- | --- |
| Ensure that the system isn't a domain controller. | Availability groups aren't supported on domain controllers. |
| Ensure that each computer is running on a supported Windows Server version | Hardware and software requirements for:  <br /> - [SQL Server 2022](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022.md)  <br /> - [SQL Server 2019](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019.md) <br /> - [SQL Server 2017](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md) <br /> - [SQL Server 2016](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  |
| Ensure that each computer is a node in a WSFC. | [Windows Server Failover Clustering with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md) |
| Ensure that the WSFC contains sufficient nodes to support your availability group configurations. | A cluster node can host one replica for an availability group. The same node can't host two replicas from the same availability group. The cluster node can participate in multiple availability groups, with one replica from each group.<br /><br />Ask your database administrators how many cluster nodes are required for to support the availability replicas of the planned availability groups.<br /><br />[What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md). |

> [!IMPORTANT]  
> Also ensure that your environment is correctly configured for connecting to an availability group. For more information, see [Driver and client connectivity support for availability groups](always-on-client-connectivity-sql-server.md).

## <a id="ComputerRecommendations"></a> Recommendations for computers that host availability replicas (Windows system)

- **Comparable systems:** For a given availability group, all the availability replicas should run on comparable systems that can handle identical workloads.

- **Dedicated network adapters:** For best performance, use a dedicated network adapter (network interface card) for [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)].

- **Sufficient disk space:** Every computer on which a server instance hosts an availability replica must possess sufficient disk space for all the databases in the availability group. Keep in mind that as primary databases grow, their corresponding secondary databases grow the same amount.

- **Identical disk layout:** Every computer on which a server instance hosts an availability replica should have an identical disk layout (with exact disk drive letters and sizes) to ensure file paths for database files (mdf,ldf) are mirrored, preventing complications during seeding and synchronization.  Review [Restrictions (availability databases)](#RestrictionsDb) for disk layouts that differ. 

### <a id="PermissionsWindows"></a> Permissions (Windows system)

To administer a WSFC, the user must be a system administrator on every cluster node.

For more information about the account for administering the cluster, see [Appendix A: Failover Cluster Requirements](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/dd197454(v=ws.10)).

### <a id="RelatedTasksWindows"></a> Related tasks (Windows system)

| Task | Link |
| --- | --- |
| Set the HostRecordTTL value. | [Change the HostRecordTTL (Using Windows PowerShell)](#ChangeHostRecordTTLps) |

#### <a id="ChangeHostRecordTTLps"></a> Change the HostRecordTTL (using PowerShell)

1. Open PowerShell window via **Run as Administrator**.

1. Import the FailoverClusters module.

1. Use the **Get-ClusterResource** cmdlet to find the Network Name resource, then use **Set-ClusterParameter** cmdlet to set the **HostRecordTTL** value, as follows:

   Get-ClusterResource "*\<NetworkResourceName>*" | Set-ClusterParameter HostRecordTTL *\<TimeInSeconds>*

   The following PowerShell example sets the HostRecordTTL to 300 seconds for a Network Name resource named `SQL Network Name (SQL35)`.

   ```powershell
   Import-Module FailoverClusters

   $nameResource = "SQL Network Name (SQL35)"
   Get-ClusterResource $nameResource | Set-ClusterParameter HostRecordTTL 300
   ```

   > [!TIP]  
   > Every time you open a new PowerShell window, you need to import the **FailoverClusters** module.

#### Related content (PowerShell)

- [Clustering and High-Availability](https://techcommunity.microsoft.com/t5/failover-clustering/bg-p/FailoverClustering) (Failover Clustering and Network Load Balancing Team Blog)

- [Getting Started with Windows PowerShell on a Failover Cluster](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ee619762(v=ws.10))

- [Cluster resource commands and equivalent Windows PowerShell cmdlets](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ee619744(v=ws.10)#BKMK_resource)

### <a id="RelatedContentWS"></a> Related content (Windows system)

- [Configure DNS settings in a Multi-Site Failover Cluster](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/dd197562(v=ws.10))

- [DNS Registration with Network Name Resource](https://techcommunity.microsoft.com/t5/failover-clustering/dns-registration-with-the-network-name-resource/ba-p/371482)

## <a id="ServerInstance"></a> SQL Server instance prerequisites and restrictions

Each availability group requires a set of failover partners, known as *availability replicas*, which are hosted by instances of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. A given server instance can be a *stand-alone instance* or a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] *failover cluster instance* (FCI).

**In this section:**

- [Checklist: Prerequisites](#PrerequisitesSI)
- [Thread Usage by availability groups](#ThreadUsage)
- [Permissions](#PermissionsSI)
- [Related Tasks](#RelatedTasksSI)
- [Related Content](#RelatedContentSI)

### <a id="PrerequisitesSI"></a> Checklist: Prerequisites (server instance)

| Prerequisite | Links |
| --- | --- |
| The host computer must be a WSFC node. The instances of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that host availability replicas for a given availability group reside on separate nodes of the cluster. An availability group can temporarily straddle two clusters while being migrated to different cluster. [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] introduced distributed availability groups. In a distributed availability group, two availability groups reside on different clusters. | [Windows Server Failover Clustering with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md)<br /><br />[Failover Clustering and Always On Availability Groups (SQL Server)](failover-clustering-and-always-on-availability-groups-sql-server.md)<br /><br />[Distributed availability groups](distributed-availability-groups.md) |
| If you want an availability group to work with Kerberos:<br /><br />All server instances that host an availability replica for the availability group must use the same SQL Server service account.<br /><br />The domain administrator needs to manually register a Service Principal Name (SPN) with Active Directory on the SQL Server service account for the virtual network name (VNN) of the availability group listener. If the SPN is registered on an account other than the SQL Server service account, authentication fails.<br /><br />To use Kerberos authentication for the communication between availability group (AG) endpoints, manually register SPNs for the database mirroring endpoints used by the AG.<br /><br />**Important:** If you change the SQL Server service account, the domain administrator needs to manually re-register the SPN. | [Register a Service Principal Name for Kerberos connections](../../configure-windows/register-a-service-principal-name-for-kerberos-connections.md)<br /><br />**Note:**<br /><br />Kerberos and SPNs enforce mutual authentication. The SPN maps to the Windows account that starts the SQL Server services. If the SPN isn't registered correctly or if it fails, the Windows security layer can't determine the account associated with the SPN, and Kerberos authentication can't be used.<br /><br />Note: NTLM doesn't have this requirement. |
| If you plan to use a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance (FCI) to host an availability replica, ensure that you understand the FCI restrictions and that the FCI requirements are met. | [Prerequisites and Requirements on Using a SQL Server Failover Cluster Instance (FCI) to Host an Availability Replica](#FciArLimitations) (later in this article) |
| Each server instance must be running the same version of SQL Server to participate in an availability group. | For more information, see the list of editions and supported features at the end of this section. |
| All the server instances that host availability replicas for an availability group must use the same [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] collation. | [Set or change the server collation](../../../relational-databases/collations/set-or-change-the-server-collation.md) |
| Enable the [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] feature on each server instance that will host an availability replica for any availability group. On a given computer, you can enable as many server instances for [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] as your [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] installation supports. | [Enable or disable Always On availability group feature](enable-and-disable-always-on-availability-groups-sql-server.md)<br /><br />**Important:** If you destroy and re-create a WSFC, you must disable and re-enable the [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] feature on each server instance that was enabled for [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] on the original cluster. |
| Each server instance requires a database mirroring endpoint. This endpoint is shared by all the availability replicas and database mirroring partners and witnesses on the server instance.<br /><br />If a server instance that you select to host an availability replica is running under a domain user account and doesn't yet have a database mirroring endpoint, the [Use the Availability Group Wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md) (or [Add a replica to your Always On Availability group using the Availability Group Wizard in SQL Server Management](use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)) can create the endpoint and grant CONNECT permission to the server instance service account. However, if the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service is running as a built-in account, such as Local System, Local Service, or Network Service, or a nondomain account, you must use certificates for endpoint authentication, and the wizard is unable to create a database mirroring endpoint on the server instance. In this case, we recommend that you create the database mirroring endpoints manually before you launch the wizard.<br /><br />**Security note:** Transport security for [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] is the same as for database mirroring. | [The Database Mirroring Endpoint (SQL Server)](../../database-mirroring/the-database-mirroring-endpoint-sql-server.md)<br /><br />[Transport Security - Database Mirroring - Always On Availability](../../database-mirroring/transport-security-database-mirroring-always-on-availability.md) |
| If any databases that use FILESTREAM are added to an availability group, ensure that FILESTREAM is enabled on every server instance that will host an availability replica for the availability group. | [Enable and configure FILESTREAM](../../../relational-databases/blob/enable-and-configure-filestream.md) |
| If any contained databases are added to an availability group, ensure that the **contained database authentication** (server configuration option) is set to `1` on every server instance that hosts an availability replica for the availability group. | [contained database authentication Server Configuration Option](../../configure-windows/contained-database-authentication-server-configuration-option.md)<br /><br />[Server configuration options (SQL Server)](../../configure-windows/server-configuration-options-sql-server.md) |

[!INCLUDE [editions-supported-features-windows](../../../includes/editions-supported-features-windows.md)]

### <a id="ThreadUsage"></a> Thread usage by availability groups

[!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] has the following requirements for worker threads:

- On an idle instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] uses 0 threads.

- The maximum number of threads used by availability groups is the configured setting for the maximum number of server threads ('**max worker threads**') minus 40.

- The availability replicas hosted on a given server instance share a single thread pool in [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and previous versions.

  Threads are shared on an on-demand basis, as follows:

  - Typically, there are 3-10 shared threads, but this number can increase depending on the primary replica workload.

  - If a given thread is idle for a while, it's released back into the general [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] thread pool. Normally, an inactive thread is released after ~15 seconds of inactivity. However, depending on the last activity, an idle thread might be retained longer.

  - A SQL Server instance uses up to 100 threads for parallel redo for secondary replicas. Each database uses up to one-half of the total number of CPU cores, but not more than 16 threads per database. If the total number of required threads for a single instance exceeds 100, SQL Server uses a single redo thread for every remaining database. Serial redo threads are released after approximately 15 seconds of inactivity.

- In addition, availability groups use unshared threads, as follows:

  - Each primary replica uses 1 Log Capture thread for each primary database. In addition, it uses 1 Log Send thread for each secondary database. Log send threads are released after ~15 seconds of inactivity.

  - A backup on a secondary replica holds a thread on the primary replica for the duration of the backup operation.

- [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] introduced the parallel redo thread pool, which is an instance-level thread pool shared with all databases having redo work. With this pool, the same set of threads can process the log records for different databases at the same time (in parallel). In [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and previous versions, the number of available threads for redo is limited to 100.

- [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] introduced parallel redo for memory optimized availability group databases. In [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] and [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)], disk-based tables don't use parallel redo if a database in an availability group is also memory optimized.

For more information, see [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](/archive/blogs/psssql/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases) (a CSS [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Engineers Blog).

### <a id="PermissionsSI"></a> Permissions (server instance)

| Task | Required Permissions |
| --- | --- |
| Creating the database mirroring endpoint | Requires CREATE ENDPOINT permission, or membership in the **sysadmin** fixed server role. Also requires CONTROL ON ENDPOINT permission. For more information, see [GRANT Endpoint Permissions (Transact-SQL)](../../../t-sql/statements/grant-endpoint-permissions-transact-sql.md). |
| Enabling [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] | Requires membership in the **Administrator** group on the local computer and full control on the WSFC. |

### <a id="RelatedTasksSI"></a> Related tasks (server instance)

| Task | Article |
| --- | --- |
| Determining whether database mirroring endpoint exists | [sys.database_mirroring_endpoints (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md) |
| Creating the database mirroring endpoint (if it doesn't yet exist) | [Create a Database Mirroring Endpoint for Windows Authentication (Transact-SQL)](../../../database-engine/database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)<br /><br />[Use Certificates for a Database Mirroring Endpoint (Transact-SQL)](../../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)<br /><br />[Create a database mirroring endpoint for an availability group using PowerShell](database-mirroring-always-on-availability-groups-powershell.md) |
| Enabling availability groups | [Enable or disable Always On availability group feature](enable-and-disable-always-on-availability-groups-sql-server.md) |

### <a id="RelatedContentSI"></a> Related content (server instance)

- [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](/archive/blogs/psssql/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases)

## <a id="NetworkConnect"></a> Network connectivity recommendations

We strongly recommend that you use the same network links for communications between WSFC nodes and communications between availability replicas.  Using separate network links can cause unexpected behaviors if some of links fail (even intermittently).

For example, for an availability group to support automatic failover, the secondary replica that is the automatic-failover partner must be in the SYNCHRONIZED state. If the network link to this secondary replica fails (even intermittently), the replica enters the UNSYNCHRONIZED state and can't begin to resynchronize until the link is restored. If the WSFC requests an automatic failover while the secondary replica is unsynchronized, automatic failover doesn't occur.

## <a id="ClientConnSupport"></a> Client connectivity support

For information about [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] support for client connectivity, see [Driver and client connectivity support for availability groups](always-on-client-connectivity-sql-server.md).

## <a id="FciArLimitations"></a> Prerequisites and restrictions for using a SQL Server failover cluster instance (FCI) to host an availability replica

**In this section:**

- [Restrictions](#RestrictionsFCI)
- [Checklist: Prerequisites](#PrerequisitesFCI)
- [Related Tasks](#RelatedTasksFCIs)
- [Related Content](#RelatedContentFCIs)

### <a id="RestrictionsFCI"></a> Restrictions (FCIs)

> [!NOTE]  
> Failover cluster instances (FCIs) support *clustered shared volumes* (CSV). For more information on CSV, see [Understanding Cluster Shared Volumes in a Failover Cluster](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/dd759255(v=ws.11)).

- **The cluster nodes of an FCI can host only one replica for a given availability group:** If you add an availability replica on an FCI, the WSFC nodes that are possible FCI owners can't host another replica for the same availability group.  To avoid possible conflicts, it's recommended to configure possible owners for the failover cluster instance. This prevents potentially causing a single WSFC from attempting to host two availability replicas for the same availability group.

  Furthermore, every other replica must be hosted by an instance of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] that resides on a different cluster node in the same Windows Server failover cluster. The only exception is that while being migrated to another cluster, an availability group can temporarily straddle two clusters.

  > [!WARNING]  
  > Using the Failover Cluster Manager to move an FCI hosting an availability group to a node that is *already* hosting a replica of the same availability group might result in the loss of the availability group replica, preventing it from being brought online on the target node. A single node of a failover cluster can't host more than one replica for the same availability group. For more information on  how this occurs, and how to recover, see the blog [Replica unexpectedly dropped in availability group](/archive/blogs/alwaysonpro/issue-replica-unexpectedly-dropped-in-availability-group).

- **FCIs don't support automatic failover by availability groups:** FCIs don't support automatic failover by availability groups, so any availability replica that is hosted by an FCI can be configured for manual failover only.

- **Changing FCI network name:** If you need to change the network name of an FCI that hosts an availability replica, you'll need to remove the replica from its availability group and then add the replica back into the availability group. You can't remove the primary replica, so if you're renaming an FCI that is hosting the primary replica, you should fail over to a secondary replica and then remove the former primary replica and add it back. Renaming an FCI might alter the URL of its database mirroring endpoint. When you add the replica ensure that you specify the current endpoint URL.

### <a id="PrerequisitesFCI"></a> Checklist: Prerequisites (FCIs)

| Prerequisite | Link |
| --- | --- |
| Ensure that each SQL Server failover cluster instance (FCI) possesses the required shared storage as per standard SQL Server failover cluster instance installation. | |

### <a id="RelatedTasksFCIs"></a> Related tasks (FCIs)

| Task | Article |
| --- | --- |
| Installing a SQL Server FCI | [Create a New Always On Failover Cluster Instance (Setup)](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md) |
| In-place upgrade of your existing SQL Server FCI | [Upgrade a failover cluster instance](../../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md) |
| Maintaining your existing SQL Server FCI | [Add or remove nodes in a failover cluster instance (Setup)](../../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md) |

### <a id="RelatedContentFCIs"></a> Related content (FCIs)

- [Failover Clustering and Always On Availability Groups (SQL Server)](failover-clustering-and-always-on-availability-groups-sql-server.md)

- [Always On Architecture Guide: Building a High Availability and Disaster Recovery Solution by Using Failover Cluster Instances and availability groups](/previous-versions/sql/sql-server-2012/jj215886(v=msdn.10))

## <a id="PrerequisitesForAGs"></a> Availability group prerequisites and restrictions

**In this section:**

- [Restrictions](#RestrictionsAG)
- [Requirements](#RequirementsAG)
- [Security](#SecurityAG)
- [Related Tasks](#RelatedTasksAGs)

### <a id="RestrictionsAG"></a> Restrictions (availability groups)

- **Availability replicas must be hosted by different nodes of one WSFC:** For a given availability group, availability replicas must be hosted by server instances running on different nodes of the same WSFC. The only exception is that while being migrated to another cluster, an availability group can temporarily straddle two clusters.

  > [!NOTE]  
  > Virtual machines on the same physical computer can each host an availability replica for the same availability group because each virtual machine acts as a separate computer.

- **Unique availability group name:** Each availability group name must be unique on the WSFC. The maximum length for an availability group name is 128 characters.

- **Availability replicas:** Each availability group supports one primary replica and up to eight secondary replicas. All of the replicas can run under asynchronous-commit mode, or up to five of them can run under synchronous-commit mode (one primary replica with two synchronous secondary replicas). Each replica must have a unique server name in both Windows and SQL Server. The server names between Windows and SQL Server must match.

- **Maximum number of availability groups and availability databases per computer:** The actual number of databases and availability groups you can put on a computer (VM or physical) depends on the hardware and workload, but there's no enforced limit. Microsoft has tested up to 10 AGs and 100 DBs per physical machine, however this isn't a binding limit. Depending on the hardware specification on the server and the workload, you can put a higher number of databases and availability groups on an instance of SQL Server. Signs of overloaded systems can include, but aren't limited to, worker thread exhaustion, slow response times for availability group system views and DMVs, and/or stalled dispatcher system dumps. Please make sure to thoroughly test your environment with a production-like workload to ensure it can handle peak workload capacity within your application SLAs. When considering SLAs be sure to consider load under failure conditions as well as expected response times.

- **Don't use the Failover Cluster Manager to manipulate availability groups**. The state of a SQL Server FCI is shared between SQL Server and the Windows Server Failover Cluster (WSFC), with SQL Server keeping more detailed state information about the instances than the cluster cares about. The management model is that SQL Server must drive the transactions, and is responsible for keeping the cluster's view of the state in sync with SQL Server's view of state. If the state of the cluster is changed outside of SQL Server it's possible for the state to get out of sync between WSFC and SQL Server, which might lead to unpredictable behavior.

  For example:

  - Don't change any availability group properties, such as the possible owners.

  - Don't use the Failover Cluster Manager to fail over availability groups. You must use [!INCLUDE [tsql](../../../includes/tsql-md.md)] or [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].

- **Don't add resources or alter dependencies associated to availability group role**. We don't recommend placing any additional resources (including user or third-party) into the availability group role or altering the role dependencies as these changes can negatively impact failover performance.

### <a id="RequirementsAG"></a> Prerequisites (availability groups)

When creating or reconfiguring an availability group configuration, ensure that you adhere to the following requirements.

| Prerequisite | Description |
| --- | --- |
| If you plan to use a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance (FCI) to host an availability replica, ensure that you understand the FCI restrictions and that the FCI requirements are met. | [Prerequisites and restrictions for using a SQL Server failover cluster instance (FCI) to host an availability replica](#FciArLimitations) (earlier in this article) |

### <a id="SecurityAG"></a> Security (availability groups)

- Security is inherited from the WSFC. Windows Server failover clustering provides two levels of user security at the granularity of entire cluster:

  - Read-only access

  - Full control

    [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] need full control, and enabling [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] on an instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] gives it full control of the cluster (through Service SID).

    You can't directly add or remove security for a server instance in Cluster Manager. To manage cluster security sessions, use the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager or the WMI equivalent from [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

- Each instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] must have permissions to access the registry, cluster, and so forth.

- We recommend that you use encryption for connections between server instances that host [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] availability replicas.

#### Permissions (availability groups)

| Task | Required Permissions |
| --- | --- |
| Creating an availability group | Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission. |
| Altering an availability group | Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.<br /><br />In addition, joining a database to an availability group requires membership in the **db_owner** fixed database role. |
| Dropping/deleting an availability group | Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission. To drop an availability group that isn't hosted on the local replica location you need CONTROL SERVER permission or CONTROL permission on that availability group. |

### <a id="RelatedTasksAGs"></a> Related tasks (availability groups)

| Task | Article |
| --- | --- |
| Creating an availability group | [Use the Availability Group Wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md)<br /><br />[Create an Always On availability group using Transact-SQL (T-SQL)](create-an-availability-group-transact-sql.md)<br /><br />[Create an Always On availability group using PowerShell](create-an-availability-group-sql-server-powershell.md)<br /><br />[Specify Endpoint URL - Adding or Modifying Availability Replica](specify-endpoint-url-adding-or-modifying-availability-replica.md) |
| Modifying the number of availability replicas | [Add a secondary replica to an Always On Availability Group](add-a-secondary-replica-to-an-availability-group-sql-server.md)<br /><br />[Join a secondary replica to an Always On availability group](join-a-secondary-replica-to-an-availability-group-sql-server.md)<br /><br />[Remove a Secondary Replica from an Availability Group (SQL Server)](remove-a-secondary-replica-from-an-availability-group-sql-server.md) |
| Creating an availability group listener | [Configure a listener for an Always On availability group](create-or-configure-an-availability-group-listener-sql-server.md) |
| Dropping an availability group | [Remove an availability group (SQL Server)](remove-an-availability-group-sql-server.md) |

## <a id="PrerequisitesForDbs"></a> Availability database prerequisites and restrictions

To be eligible to be added to an availability group, a database must meet the following prerequisites and restrictions.

**In this section:**

- [Requirements](#RequirementsDb)
- [Restrictions](#RestrictionsDb)
- [Recommendations for computers that host availability replicas (Windows system](#TDEdbs)
- [Permissions](#PermissionsDbs)
- [Related Tasks](#RelatedTasksADb)

### <a id="RequirementsDb"></a> Checklist: Requirements (availability databases)

To be eligible to be added to an availability group, a database must:

| Requirements | Link |
| --- | --- |
| Be a user database. System databases can't belong to an availability group. | |
| Reside on the instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] where you create the availability group and be accessible to the server instance. | |
| Be a read-write database. Read-only databases can't be added to an availability group. | [sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**is_read_only** = 0) |
| Be a multi-user database. | [sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**user_access** = 0) |
| Not use AUTO_CLOSE. | [sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**is_auto_close_on** = 0) |
| Use the full recovery model. | [sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**recovery_model** = 1) |
| Possess at least one full database backup.<br /><br />Note: After setting a database to full recovery model, a full backup is required to initiate the full-recovery log chain. | [Create a Full Database Backup](../../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md) |
| Not belong to any existing availability group. | [sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) (**group_database_id** = NULL) |
| Not be configured for database mirroring. | [sys.database_mirroring](../../../relational-databases/system-catalog-views/sys-database-mirroring-transact-sql.md) (If the database doesn't participate in mirroring, all columns prefixed with "mirroring_" are NULL.) |
| Before adding a database that uses FILESTREAM to an availability group, ensure that FILESTREAM is enabled on every server instance that hosts or will host an availability replica for the availability group. | [Enable and configure FILESTREAM](../../../relational-databases/blob/enable-and-configure-filestream.md) |
| Before adding a contained database to an availability group, ensure that the **contained database authentication** server option is set to **1** on every server instance that hosts or will host an availability replica for the availability group. | [contained database authentication Server Configuration Option](../../configure-windows/contained-database-authentication-server-configuration-option.md)<br /><br />[Server configuration options (SQL Server)](../../configure-windows/server-configuration-options-sql-server.md) |

> [!NOTE]  
> [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] works with any supported database compatibility level.

### <a id="RestrictionsDb"></a> Restrictions (availability databases)

- If the file path (including the drive letter) of a secondary database differs from the path of the corresponding primary database, the following restrictions apply:

  - **[!INCLUDE [ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)]/[!INCLUDE [ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)]:** The **Full** option isn't supported (on the [Select Initial Data Synchronization Page (Always On Availability Group Wizards)](select-initial-data-synchronization-page-always-on-availability-group-wizards.md) page),

  - **RESTORE WITH MOVE:** To create the secondary databases, the database files must be RESTORED WITH MOVE on each instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts a secondary replica.

  - **Impact on add-file operations:** A later add-file operation on the primary replica might fail on the secondary databases. This failure could cause the secondary databases to be suspended. This, in turn, causes the secondary replicas to enter the NOT SYNCHRONIZING state.

    > [!NOTE]  
    > For information about responding to a failed ad-file operation, see [Troubleshoot a Failed Add-File Operation (Always On Availability Groups)](troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md).

- You can't drop a database that currently belongs to an availability group.

### <a id="TDEdbs"></a> Follow up for TDE protected databases

If you use transparent data encryption (TDE), the certificate or asymmetric key for creating and decrypting other keys must be the same on every server instance that hosts an availability replica for the availability group. For more information, see [Move a TDE Protected Database to Another SQL Server](../../../relational-databases/security/encryption/move-a-tde-protected-database-to-another-sql-server.md).

### <a id="PermissionsDbs"></a> Permissions (availability databases)

Requires ALTER permission on the database.

### <a id="RelatedTasksADb"></a> Related tasks (availability databases)

| Task | Article |
| --- | --- |
| Preparing a secondary database (manually) | [Prepare a secondary database for an Always On availability group](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md) |
| Joining a secondary database to availability group (manually) | [Join a secondary database to an Always On availability group](join-a-secondary-database-to-an-availability-group-sql-server.md) |
| Modifying the number of availability databases | [Add a Database to an Always On availability group](availability-group-add-a-database.md)<br /><br />[Remove a Secondary Database from an Availability Group (SQL Server)](remove-a-secondary-database-from-an-availability-group-sql-server.md)<br /><br />[Remove a primary database from an Always On availability group](remove-a-primary-database-from-an-availability-group-sql-server.md) |

## Related content

- [Microsoft SQL Server Always On Solutions Guide for High Availability and Disaster Recovery](/previous-versions/sql/sql-server-2012/hh781257(v=msdn.10))
- [SQL Server Always On Team Blog: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)
- [Always On - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](/archive/blogs/psssql/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases)
- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Failover Clustering and Always On Availability Groups (SQL Server)](failover-clustering-and-always-on-availability-groups-sql-server.md)
- [Driver and client connectivity support for availability groups](always-on-client-connectivity-sql-server.md)
