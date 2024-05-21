---
title: "Always On failover cluster instances"
description: Learn how Always On failover cluster instances provide local high availability through redundancy at the server-instance level in SQL Server.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/08/2024"
ms.service: sql
ms.subservice: failover-cluster-instance
ms.topic: conceptual
ms.custom: intro-overview
helpviewer_keywords:
  - "clustering [SQL Server]"
  - "high availability [SQL Server], failover clustering"
  - "virtual servers [SQL Server], about virtual servers"
  - "clusters [SQL Server]"
  - "servers [SQL Server], failover clustering"
  - "resource groups [SQL Server]"
  - "availability [SQL Server]"
  - "failover clustering [SQL Server]"
  - "AlwaysOn [SQL Server], see failover clustering [SQL Server]"
  - "Always On [SQL Server], see failover clustering [SQL Server]"
---
# Always On failover cluster instances (SQL Server)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Always On failover cluster instances use Windows Server Failover Clustering (WSFC) to provide local high availability. A failover cluster instance (FCI) is redundant at the server-instance level. An FCI is a single instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that is installed across Windows Server cluster nodes and, possibly, across multiple subnets. On the network, an FCI appears as an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] running on a single computer, but the FCI provides failover from one WSFC node to another if the current node becomes unavailable.
  
An FCI can use [Always On availability groups](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) to provide remote disaster recovery at the database level. For more information, see [Failover clustering and availability groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md).

> [!NOTE]  
> Windows Server 2016 Datacenter edition introduces support for Storage Spaces Direct (S2D). SQL Server failover cluster instances support S2D for cluster storage resources. For more information, see [Storage Spaces Direct in Windows Server](/windows-server/storage/storage-spaces/storage-spaces-direct-overview).
> 
>Failover cluster instances also support clustered shared volumes (CSV). For more information, see [Understanding cluster shared volumes in a failover cluster](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/dd759255(v=ws.11)). 
   
##  <a name="Benefits"></a> Benefits of a failover cluster instance

When there is hardware or software failure of a server, the applications or clients connecting to the server will experience downtime. Redundant nodes protect the availability of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance when it is an FCI instead of a standalone instance. Only one of the nodes in the FCI owns the WSFC resource group at a time. In case of a failure (hardware failures, operating system failures, application or service failures), or a planned upgrade, the cluster moves the resource group ownership to another WSFC node. This process is transparent to the client or application connecting to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. This minimizes the downtime the application or clients experience during a failure. The following lists some key benefits that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instances provide:  
  
- Protection at the instance level through redundancy.
  
- Automatic failover in the event of a failure (hardware failures, operating system failures, application or service failures).

   > [!IMPORTANT]
   > In an availability group, automatic failover from an FCI to other nodes within the availability group is not supported. This means that FCIs and standalone nodes should not be coupled together within an availability group if automatic failover is an important component your high availability solution. However, this coupling can be made for your *disaster recovery* solution.
  
- Support for a broad array of storage solutions, including WSFC cluster disks (iSCSI, Fiber Channel, and so on) and server message block (SMB) file shares.

- Disaster recovery using a multi-subnet FCI or running an FCI-hosted database inside an availability group. With the new multi-subnet support in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], a multi-subnet FCI no longer requires a virtual LAN, increasing the manageability and security of a multi-subnet FCI.

- Zero reconfiguration of applications and clients during failovers.
  
- Flexible failover policy for granular trigger events for automatic failovers.
  
- Reliable failovers through periodic and detailed health detection using dedicated and persisted connections.
  
- Configurability and predictability in failover time through indirect background checkpoints.
  
- Throttled resource usage during failovers.
  
##  <a name="Recommendations"></a> Recommendations

In a production environment:

- Use static IP addresses in conjunction the virtual IP address of a failover cluster instance.
- Don't use DHCP in a production environment. In the event of down time, if the DHCP IP lease expires, extra time is required to re-register the new DHCP IP address associated with the DNS name.

##  <a name="Overview"></a> Failover cluster instance Overview  
 An FCI runs in a WSFC resource group with one or more WSFC nodes. When the FCI starts up, one of the nodes assumes ownership of the resource group and brings its [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance online. The resources owned by this node include:  
  
- Network name  
  
- IP address  
  
- Shared disks  
  
- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Database Engine service  
  
- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent service  
  
- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Analysis Services service, if installed  
  
- One file share resource, if the FILESTREAM feature is installed  
  
 At any time, only the resource group owner (and no other node in the FCI) is running its respective [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] services in the resource group. When a failover occurs, whether it be an automatic failover or a planned failover, the following sequence of events happens:  
  
1. Unless a hardware or system failure occurs, all dirty pages in the buffer cache are written to disk.

2. All respective [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] services in the resource group are stopped on the active node.

3. The resource group ownership is transferred to another node in the FCI.

4. The new resource group owner starts its [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] services.

5. Client application connection requests are automatically directed to the new active node using the same virtual network name (VNN).

 The FCI is online as long as its underlying WSFC cluster is in good quorum health (the majority of the quorum WSFC nodes are available as automatic failover targets). When the WSFC cluster loses its quorum, whether due to hardware, software, network failure, or improper quorum configuration, the entire WSFC cluster, along with the FCI, is brought offline. Manual intervention is then required in this unplanned failover scenario to reestablish quorum in the remaining available nodes in order to bring the WSFC cluster and FCI back online. For more information, see [WSFC Quorum Modes and Voting Configuration &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-quorum-modes-and-voting-configuration-sql-server.md).

### Predictable failover time

Depending on when your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance last performed a checkpoint operation, there can be a substantial number of dirty pages in the buffer cache. Consequently, failovers last as long as it takes to write the remaining dirty pages to disk, which can lead to long and unpredictable failover time. Beginning with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], the FCI can use indirect checkpoints to throttle the number of dirty pages kept in the buffer cache. While this does consume additional resources under regular workload, it makes the failover time more predictable as well as more configurable. This is very useful when the service-level agreement in your organization specifies the recovery time objective (RTO) for your high availability solution. For more information on indirect checkpoints, see [Indirect Checkpoints](../../../relational-databases/logs/database-checkpoints-sql-server.md#IndirectChkpt).

### Reliable health monitoring and flexible failover policy

 After the FCI starts successfully, the WSFC service monitors both the health of the underlying WSFC cluster, as well as the health of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. Beginning with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], the WSFC service uses a dedicated connection to poll the active [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance for detailed component diagnostics through a system stored procedure. The implication of this is three-fold:  
  
- The dedicated connection to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance makes it possible to reliably poll for component diagnostics all the time, even when the FCI is under heavy load. This makes it possible to distinguish between a system that is under heavy load and a system that actually has failure conditions, thus preventing issues such as false failovers.

- The detailed component diagnostics makes it possible to configure a more flexible failover policy, whereby you can choose what failure conditions trigger failovers and which failure conditions do not.

- The detailed component diagnostics also enables better troubleshooting of automatic failovers retroactively. The diagnostic information is stored to log files, which are collocated with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] error logs. You can load them into the Log File Viewer to inspect the component states leading up to the failover occurrence in order to determine what cause that failover.

 For more information, see [Failover Policy for failover cluster instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)  
  
##  <a name="FCIelements"></a> Elements of a failover cluster instance  
 An FCI consists of a set of physical servers (nodes) that contain similar hardware configuration as well as identical software configuration that includes operating system version and patch level, and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] version, patch level, components, and instance name. Identical software configuration is necessary to ensure that the FCI can be fully functional as it fails over between the nodes.

 WSFC Resource Group  
 A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI runs in a WSFC resource group. Each node in the resource group maintains a synchronized copy of the configuration settings and check-pointed registry keys to ensure full functionality of the FCI after a failover, and only one of the nodes in the cluster owns the resource group at a time (the active node). The WSFC service manages the server cluster, quorum configuration, failover policy, and failover operations, as well as the VNN and virtual IP addresses for the FCI. In case of a failure (hardware failures, operating system failures, application or service failures) or a planned upgrade, the resource group ownership is moved to another node in the FCI. The number of nodes that are supported in a WSFC resource group depends on your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] edition. Also, the same WSFC cluster can run multiple FCIs (multiple resource groups), depending on your hardware capacity, such as CPUs, memory, and number of disks.

 SQL Server Binaries  
 The product binaries are installed locally on each node of the FCI, a process similar to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] stand-alone installations. However, during startup, the services are not started automatically, but managed by WSFC.

 Storage  
 Contrary to the availability group, an FCI must use shared storage between all nodes of the FCI for database and log storage. The shared storage can be in the form of WSFC cluster disks, disks on a SAN, Storage Spaces Direct (S2D), or file shares on an SMB. This way, all nodes in the FCI have the same view of instance data whenever a failover occurs. This does mean, however, that the shared storage has the potential of being the single point of failure, and FCI depends on the underlying storage solution to ensure data protection.

 Network Name  
 The VNN for the FCI provides a unified connection point for the FCI. This allows applications to connect to the VNN without the need to know the currently active node. When a failover occurs, the VNN is registered to the new active node after it starts. This process is transparent to the client or application connecting to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and this minimizes the downtime the application or clients experience during a failure.

 Virtual IPs  
 In the case of a multi-subnet FCI, a virtual IP address is assigned to each subnet in the FCI. During a failover, the VNN on the DNS server is updated to point to the virtual IP address for the respective subnet. Applications and clients can then connect to the FCI using the same VNN after a multi-subnet failover.

##  <a name="ConceptsAndTasks"></a> SQL Server failover concepts and tasks  
  
|**Concepts and Tasks**|**Article**|  
|------------------------|-----------|  
|Describes the failure detection mechanism and the flexible failover policy.|[Failover policy for failover cluster instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)|  
|Describes concepts in FCI administration and maintenance.|[Failover cluster instance administration and maintenance](../../../sql-server/failover-clusters/windows/failover-cluster-instance-administration-and-maintenance.md)|  
|Describes multi-subnet configuration and concepts|[SQL Server multi-subnet clustering &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/sql-server-multi-subnet-clustering-sql-server.md)|  
  
##  <a name="RelatedTopics"></a> Related topics  
  
|**Topic descriptions**|**Article**|  
|----------------------------|---------------|  
|Describes how to install a new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] FCI.|[Create a New SQL Server failover cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)|  
|Describes how to upgrade to a [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] failover cluster.|[Upgrade a SQL Server failover cluster instance](../../../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md)|  
|Describes Windows Failover Clustering concepts and provides links to tasks related to Windows failover clustering.|[Windows Server failover cluster with SQL Server](windows-server-failover-clustering-wsfc-with-sql-server.md)|
|Describes the distinctions in concepts between nodes in an FCI and replicas within an availability group and considerations for using an FCI to host a replica for an availability group.|[Failover clustering and Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md)|  
  