---
title: HADR configuration best practices
description: "Learn about the supported cluster configurations when you configure high availability and disaster recovery (HADR) for SQL Server on Azure Virtual Machines, such as supported quorums or connection routing options."
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: mathoma, randolphwest
ms.date: 01/23/2026
ms.service: azure-vm-sql-server
ms.subservice: hadr
ms.topic: best-practice
editor: monicar
ai-usage: ai-assisted
tags: azure-service-management
---
# HADR configuration best practices (SQL Server on Azure VMs)

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

A [Windows Server Failover Cluster](hadr-windows-server-failover-cluster-overview.md) is used for high availability and disaster recovery (HADR) with SQL Server on Azure Virtual Machines (VMs).

This article provides cluster configuration best practices for both [failover cluster instances (FCIs)](failover-cluster-instance-overview.md) and [availability groups](availability-group-overview.md) when you use them with SQL Server on Azure VMs.

To learn more, see the other articles in this series: [Checklist](performance-guidelines-best-practices-checklist.md), [VM size](performance-guidelines-best-practices-vm-size.md), [Storage](performance-guidelines-best-practices-storage.md), [Security](security-considerations-best-practices.md), [HADR configuration](hadr-cluster-best-practices.md), [Collect baseline](performance-guidelines-best-practices-collect-baseline.md).

## Checklist

Review the following checklist for a brief overview of the HADR best practices that the rest of the article covers in greater detail.

[!INCLUDE [HADR best practices](../../includes/virtual-machines-best-practices-hadr.md)]

To compare the HADR checklist with the other best practices, see the comprehensive [Performance best practices checklist](performance-guidelines-best-practices-checklist.md).

## VM availability settings

To reduce the effect of downtime, consider the following VM best availability settings:

- Use proximity placement groups together with accelerated networking for lowest latency (optional, recommended for production).
- Place virtual machine cluster nodes in separate availability zones to protect from datacenter-level failures or in a single availability set for lower-latency redundancy within the same datacenter (required for production SLA).
- Use premium-managed OS and data disks for VMs in an availability set (recommended for production).
- Configure each application tier into separate availability sets (required for application-level redundancy).

## Quorum

Although a two-node cluster functions without a [quorum resource](/windows-server/storage/storage-spaces/understand-quorum), customers are strictly required to use a quorum resource to have production Microsoft support. Cluster validation doesn't pass any cluster without a quorum resource.

Technically, a three-node cluster can survive a single node loss (down to two nodes) without a quorum resource, but after the cluster is down to two nodes, if there's another node loss or communication failure, then there's a risk that the clustered resources will go offline to prevent a split-brain scenario. Configuring a quorum resource allows the cluster to continue online with only one node online.

The disk witness is the most resilient quorum option, but to use a disk witness on a SQL Server on Azure VM, you must use an Azure Shared Disk, which imposes some limitations to the high availability solution. As such, use a disk witness when you're configuring your failover cluster instance with Azure Shared Disks, otherwise use a cloud witness whenever possible.

The following table lists the quorum options available for SQL Server on Azure VMs:

| | [Cloud witness](/windows-server/failover-clustering/deploy-cloud-witness) | [Disk witness](/windows-server/failover-clustering/manage-cluster-quorum#configure-the-cluster-quorum) | [File share witness](/windows-server/failover-clustering/manage-cluster-quorum#configure-the-cluster-quorum) |
| --- | --- | --- | --- |
| **Supported OS** | Windows Server 2016+ | All | All |

- The **cloud witness** (Windows Server 2016+, cluster administrator required) is ideal for deployments in multiple sites, multiple zones, and multiple regions. Use a cloud witness whenever possible, unless you're using a shared-storage cluster solution.
- The **disk witness** (all Windows Server versions, cluster administrator required) is the most resilient quorum option and is preferred for any cluster that uses Azure Shared Disks (or any shared-disk solution like shared SCSI, iSCSI, or fiber channel SAN).  A Clustered Shared Volume can't be used as a disk witness.
- The **fileshare witness** (all Windows Server versions, cluster administrator required) is suitable for when the disk witness and cloud witness are unavailable options.

To get started, see [Configure cluster quorum](hadr-cluster-quorum-configure-how-to.md).

## Quorum Voting

It's possible to change the quorum vote of a node participating in a Windows Server Failover Cluster (cluster administrator required, optional for advanced configurations). 

When modifying the node vote settings, follow these guidelines:

| Quorum voting guidelines |
| --- |
| Start with each node having no vote by default. Each node should only have a vote with explicit justification. |
| Enable votes for cluster nodes that host the primary replica of an availability group, or the preferred owners of a failover cluster instance. |
| Enable votes for automatic failover owners. Each node that might host a primary replica or FCI as a result of an automatic failover should have a vote. |
| If an availability group has more than one secondary replica, only enable votes for the replicas that have automatic failover. |
| Disable votes for nodes that are in secondary disaster recovery sites. Nodes in secondary sites shouldn't contribute to the decision of taking a cluster offline if there's nothing wrong with the primary site. |
| Have an odd number of votes, with three quorum votes minimum. Add a [quorum witness](hadr-cluster-quorum-configure-how-to.md) for an additional vote if necessary in a two-node cluster. |
| Reassess vote assignments post-failover. You don't want to fail over into a cluster configuration that doesn't support a healthy quorum. |

You can adjust quorum voting using Failover Cluster Manager or PowerShell: 

```powershell
Import-Module FailoverClusters  
  
$node = "AlwaysOnSrv1"  
(Get-ClusterNode $node).NodeWeight = 0  
  
$cluster = (Get-ClusterNode $node).Cluster  
$nodes = Get-ClusterNode -Cluster $cluster  
  
$nodes | Format-Table -property NodeName, State, NodeWeight  
```

You can also use cluster.exe in an elevated command prompt:

```
cluster.exe Cluster001 node AlwaysOnSrv1 /prop NodeWeight=0  
```

## Connectivity

To match the on-premises experience for connecting to your availability group listener or failover cluster instance, deploy your SQL Server VMs to multiple subnets within the same virtual network. Having multiple subnets negates the need for the extra dependency on an Azure Load Balancer, or a distributed network name (DNN) to route your traffic to your listener.

To simplify your HADR solution, deploy your SQL Server VMs to multiple subnets whenever possible.  To learn more, see [Multi-subnet AG](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md), and [Multi-subnet FCI](failover-cluster-instance-prepare-vm.md#subnets).

If your SQL Server VMs are in a single subnet, it's possible to configure either a virtual network name (VNN) and an Azure Load Balancer, or a distributed network name (DNN) for both failover cluster instances and availability group listeners.

The distributed network name (DNN) is the recommended connectivity option, when available:

- The end-to-end solution is more robust since you no longer have to maintain the load balancer resource.
- Eliminating the load balancer probes minimizes failover duration.
- The DNN simplifies provisioning and management of the failover cluster instance or availability group listener with SQL Server on Azure VMs.

Consider the following limitations:

- The client driver must support the `MultiSubnetFailover=true` parameter.
- The DNN feature is available starting with [SQL Server 2016 SP3](https://support.microsoft.com/topic/kb5003279-sql-server-2016-service-pack-3-release-information-46ab9543-5cf9-464d-bd63-796279591c31), [SQL Server 2017 CU25](https://support.microsoft.com/topic/kb5003830-cumulative-update-25-for-sql-server-2017-357b80dc-43b5-447c-b544-7503eee189e9), and [SQL Server 2019 CU8](https://support.microsoft.com/topic/cumulative-update-8-for-sql-server-2019-ed7f79d9-a3f0-a5c2-0bef-d0b7961d2d72) on Windows Server 2016 and later.

To learn more, see the [Windows Server Failover Cluster overview](hadr-windows-server-failover-cluster-overview.md#virtual-network-name-vnn).

To configure connectivity, see the following articles:

- Availability group: [Configure DNN](availability-group-distributed-network-name-dnn-listener-configure.md), [Configure VNN](availability-group-vnn-azure-load-balancer-configure.md)
- Failover cluster instance: [Configure DNN](failover-cluster-instance-distributed-network-name-dnn-configure.md), [Configure VNN](failover-cluster-instance-vnn-azure-load-balancer-configure.md).

Most SQL Server features work transparently with FCI and availability groups when using the DNN, but there are certain features that might require special consideration. See [FCI and DNN interoperability](failover-cluster-instance-dnn-interoperability.md) and [AG and DNN interoperability](availability-group-dnn-interoperability.md) to learn more.

> [!TIP]  
> Set the `MultiSubnetFailover=true` parameter in the connection string even for HADR solutions that span a single subnet to support future spanning of subnets without needing to update connection strings.

## Heartbeat and threshold

Change the cluster heartbeat and threshold settings to relaxed settings. The default heartbeat and threshold cluster settings are designed for highly tuned on-premises networks and don't consider the possibility of increased latency in a cloud environment. The heartbeat network is maintained with UDP 3343, which is traditionally far less reliable than TCP and more prone to incomplete conversations.

Therefore, when running cluster nodes for SQL Server on Azure VM high availability solutions, change the cluster settings to a more relaxed monitoring state to avoid transient failures due to the increased possibility of network latency or failure, Azure maintenance, or hitting resource bottlenecks.

The delay and threshold settings have a cumulative effect to total health detection. For example, setting *CrossSubnetDelay* to send a heartbeat every 2 seconds and setting the *CrossSubnetThreshold* to 10 missed heartbeats before taking recovery means the cluster can have a total network tolerance of 20 seconds before recovery action is taken. In general, continuing to send frequent heartbeats but having greater thresholds is preferred.

To ensure recovery during legitimate outages while providing greater tolerance for transient issues, relax your delay and threshold settings to the recommended values detailed in the following table:

| Setting | Windows Server 2012 or later | Windows Server 2008 R2 |
| :--- | :--- | :--- |
| SameSubnetDelay | 1 second | 2 second |
| SameSubnetThreshold | 40 heartbeats | 10 heartbeats (max) |
| CrossSubnetDelay | 1 second | 2 second |
| CrossSubnetThreshold | 40 heartbeats | 20 heartbeats (max) |

Use PowerShell to change your cluster parameters: 

# [Windows Server 2012-2019](#tab/windows2012)

```powershell
(get-cluster).SameSubnetThreshold = 40
(get-cluster).CrossSubnetThreshold = 40
```

Expected output: Commands complete successfully with no error messages.

Verify your changes:

```powershell
get-cluster | fl *subnet*
```

Expected output:
```output
CrossSubnetThreshold  : 40
SameSubnetThreshold   : 40
```

# [Windows Server 2008/R2](#tab/windows2008)

```powershell
(get-cluster).SameSubnetThreshold = 10  # 10 heartbeats
(get-cluster).CrossSubnetThreshold = 20  # 20 heartbeats
(get-cluster).SameSubnetDelay = 2000  # 2000 milliseconds (2 seconds)
(get-cluster).CrossSubnetDelay = 2000  # 2000 milliseconds (2 seconds)
```

Expected output: Commands complete successfully with no error messages.

Verify your changes:

```powershell
get-cluster | fl *subnet*
```

Expected output:
```output
CrossSubnetDelay      : 2000
CrossSubnetThreshold  : 20
SameSubnetDelay       : 2000
SameSubnetThreshold   : 10
```

---

Consider the following:

- This change is immediate, restarting the cluster or any resources isn't required.
- Same subnet values shouldn't be greater than cross subnet values.
- SameSubnetThreshold <= CrossSubnetThreshold
- SameSubnetDelay <= CrossSubnetDelay

Choose relaxed values based on how much down time is tolerable and how long before a corrective action should occur depending on your application,  business needs, and your environment. If you're not able to exceed the default Windows Server 2019 values, then at least try to match them, if possible:

For reference, the following table details the default values:

| Setting | Windows Server 2019 | Windows Server 2016 | Windows Server 2008 - 2012 R2 |
| :--- | :--- | --- | :--- |
| SameSubnetDelay | 1 second | 1 second | 1 second |
| SameSubnetThreshold | 20 heartbeats | 10 heartbeats | 5 heartbeats |
| CrossSubnetDelay | 1 second | 1 second | 1 second |
| CrossSubnetThreshold | 20 heartbeats | 10 heartbeats | 5 heartbeats |

To learn more, see [Tuning Failover Cluster Network Thresholds](/windows-server/troubleshoot/iaas-sql-failover-cluster).

## Relaxed monitoring

If tuning your cluster heartbeat and threshold settings as recommended is insufficient tolerance and you're still seeing failovers due to transient issues rather than true outages, you can configure your AG or FCI monitoring to be more relaxed. In some scenarios, it might be beneficial to temporarily relax the monitoring for a period of time given the level of activity. For example, you might want to relax the monitoring when you're doing IO intensive workloads such as database backups, index maintenance, DBCC CHECKDB, etc. Once the activity is complete, set your monitoring to less relaxed values.

> [!WARNING]  
> Changing these settings might mask an underlying problem, and should be used as a temporary solution to reduce, rather than eliminate, the likelihood of failure. Underlying issues should still be investigated and addressed.

Start by increasing the following parameters from their default values for relaxed monitoring, and adjust as necessary:

| Parameter | Default value | Relaxed Value | Description |
| --- | --- | --- | --- |
| **Healthcheck timeout** | 30000 milliseconds (30 seconds) | 60000 milliseconds (60 seconds) | Determines health of the primary replica or node. The cluster resource DLL `sp_server_diagnostics` returns results at an interval that equals 1/3 of the health-check timeout threshold. If `sp_server_diagnostics` is slow or isn't returning information, the resource DLL waits for the full interval of the health-check timeout threshold before determining that the resource is unresponsive, and initiating an automatic failover, if configured to do so. |
| **Failure-Condition Level** | 3 | 2 | Conditions that trigger an automatic failover. There are five failure-condition levels, which range from the least restrictive (level one) to the most restrictive (level five) |

Use Transact-SQL (T-SQL, **sysadmin** or **ALTER AVAILABILITY GROUP** permission required) to modify the health check and failure conditions for both AGs and FCIs.

For availability groups:

```sql
ALTER AVAILABILITY GROUP AG1 SET (HEALTH_CHECK_TIMEOUT = 60000);  -- 60000 milliseconds (60 seconds)
ALTER AVAILABILITY GROUP AG1 SET (FAILURE_CONDITION_LEVEL = 2);
```

Expected output: Commands complete successfully. 

For failover cluster instances (**sysadmin** or **ALTER SETTINGS** permission required):

```sql
ALTER SERVER CONFIGURATION SET FAILOVER CLUSTER PROPERTY HealthCheckTimeout = 60000;  -- 60000 milliseconds (60 seconds)
ALTER SERVER CONFIGURATION SET FAILOVER CLUSTER PROPERTY FailureConditionLevel = 2;
```

Expected output: Commands complete successfully. 

Specific to **availability groups**, start with the following recommended parameters, and adjust as necessary:

| Parameter | Default value | Relaxed Value | Description |
| --- | --- | --- | --- |
| **Lease timeout** | 20000 milliseconds (20 seconds) | 40000 milliseconds (40 seconds) | Prevents split-brain. |
| **Session timeout** | 10000 milliseconds (10 seconds) | 20000 milliseconds (20 seconds) | Checks communication issues between replicas. The session-timeout period is a replica property that controls how long (in seconds) that an availability replica waits for a ping response from a connected replica before considering the connection to have failed. By default, a replica waits 10 seconds for a ping response. This replica property applies to only the connection between a given secondary replica and the primary replica of the availability group. |
| **Max failures in specified period** | 2 | 6 | Used to avoid indefinite movement of a clustered resource within multiple node failures. Too low of a value can lead to the availability group being in a failed state. Increase the value to prevent short disruptions from performance issues as too low a value can lead to the AG being in a failed state. |

Before making any changes, consider the following:
- Don't lower any timeout values below their default values.
- Use this equation to calculate the maximum lease time-out value:
`Lease timeout < (2 * SameSubnetThreshold * SameSubnetDelay)`.  
  Start with 40 seconds (40000 milliseconds). If you're using the relaxed `SameSubnetThreshold` and `SameSubnetDelay` values recommended previously, don't exceed 80 seconds (80000 milliseconds) for the lease timeout value.
- For synchronous-commit replicas, changing session-timeout to a high value can increase HADR_sync_commit waits.

**Lease timeout**

Use the **Failover Cluster Manager** (cluster administrator required, optional for tuning) to modify the **lease timeout** settings for your availability group. See the SQL Server [availability group lease health check](/sql/database-engine/availability-groups/windows/availability-group-lease-healthcheck-timeout#lease-timeout) documentation for detailed steps.

**Session timeout**

Use Transact-SQL (T-SQL, **sysadmin** or **ALTER AVAILABILITY GROUP** permission required, optional for tuning) to modify the **session timeout** for an availability group:

```sql
ALTER AVAILABILITY GROUP AG1
MODIFY REPLICA ON 'INSTANCE01' WITH (SESSION_TIMEOUT = 20);  -- 20 seconds
```

Expected output: Command completes successfully. 

**Max failures in specified period**

Use the Failover Cluster Manager (cluster administrator required, optional for tuning) to modify the **Max failures in specified period** value:

1. Select **Roles** in the navigation pane.
1. Under **Roles**, right-click the clustered resource and choose **Properties**.
1. Select the **Failover** tab, and increase the **Max failures in specified period** value as desired.

## Resource limits

VM or disk limits could result in a resource bottleneck that affects the health of the cluster and impedes the health check.

### Identify resource bottlenecks

- Use [I/O Analysis](storage-performance-analysis.md) in the Azure portal to identify disk performance issues that can cause a failover.
- Monitor [Storage IO Utilization metrics](/azure/virtual-machines/disks-metrics#storage-io-utilization-metrics) to understand VM or disk level latency.
- Ensure your OS, drivers, and SQL Server are at the latest builds.

### Optimize SQL Server environment

- Optimize your SQL Server on Azure VM environment as described in the [performance guidelines](performance-guidelines-best-practices-checklist.md) for SQL Server on Azure Virtual Machines.
- Reduce or spread out the workload to reduce utilization without exceeding resource limits.

### Tune SQL Server workload

When there's an opportunity to improve SQL Server workload performance:

- Add or optimize indexes to improve query efficiency.
- Update statistics if needed and if possible, with Full scan.
- Use features like resource governor (starting with SQL Server 2014, enterprise only) to limit resource utilization during specific workloads, such as backups or index maintenance.

### Scale VM or disk resources

Move to a VM or disk that has higher limits to meet or exceed the demands of your workload.

## Networking

For guidance on deploying your SQL Server VMs to multiple subnets to avoid dependency on an Azure Load Balancer or distributed network name (DNN), see the [Connectivity](#connectivity) section.

Use a single NIC per server (cluster node). Azure networking has physical redundancy, which makes additional NICs unnecessary on an Azure virtual machine guest cluster. The cluster validation report warns you that the nodes are reachable only on a single network. You can ignore this warning on Azure virtual machine guest failover clusters.

Bandwidth limits for a particular VM are shared across NICs and adding an additional NIC doesn't improve availability group performance for SQL Server on Azure VMs. As such, there's no need to add a second NIC.

The non-RFC-compliant DHCP service in Azure can cause the creation of certain failover cluster configurations to fail. This failure happens because the cluster network name is assigned a duplicate IP address, such as the same IP address as one of the cluster nodes. This is an issue when you use availability groups, which depend on the Windows Server Failover Clustering feature.

Consider the scenario when a two-node cluster is created and brought online:

1. The cluster comes online, and then NODE1 requests a dynamically assigned IP address for the cluster network name.
1. The DHCP service doesn't give any IP address other than NODE1's own IP address, because the DHCP service recognizes that the request comes from NODE1 itself.
1. Windows detects that a duplicate address is assigned both to NODE1 and to the failover cluster's network name, and the default cluster group fails to come online.
1. The default cluster group moves to NODE2. NODE2 treats NODE1's IP address as the cluster IP address and brings the default cluster group online.
1. When NODE2 tries to establish connectivity with NODE1, packets directed at NODE1 never leave NODE2 because it resolves NODE1's IP address to itself. NODE2 can't establish connectivity with NODE1, and then loses quorum and shuts down the cluster.
1. NODE1 can send packets to NODE2, but NODE2 can't reply. NODE1 loses quorum and shuts down the cluster.

You can avoid this scenario by assigning an unused static IP address to the cluster network name in order to bring the cluster network name online and add the IP address to [Azure Load Balancer](availability-group-load-balancer-portal-configure.md).

## Configure probe port

When using an Azure Load Balancer to support a virtual network name (VNN) resource (single subnet deployments only, cluster administrator required), you must configure the cluster to reply to the health probe requests for your [Always On availability group listener](availability-group-vnn-azure-load-balancer-configure.md#configure-a-health-probe) or your [failover cluster instance](failover-cluster-instance-vnn-azure-load-balancer-configure.md#configure-a-health-probe). If the health probe fails to get a response from a backend instance, then **no new connections are sent to that backend instance until the health probe succeeds again**.

## Configure port exclusion 

If you're using a virtual network name (VNN) resource with an Azure Load Balancer (single subnet deployments, Windows Server 2012+, cluster administrator required), when using a health probe port between 49,152 and 65,536 (the [default dynamic port range for TCP/IP](/windows/client-management/troubleshoot-tcpip-port-exhaust#default-dynamic-port-range-for-tcpip)), you must configure port exclusion for your [Always On availability group listener](availability-group-vnn-azure-load-balancer-configure.md#configure-port-exclusion), or your [failover cluster instance](failover-cluster-instance-vnn-azure-load-balancer-configure.md#configure-port-exclusion).

Configuring port exclusion prevents events such as **Event ID: 1069 with status 10048**, which can be caused by an internal process taking the port defined as the probe port. 

The following example shows Event ID 1069 with status 10048 in the cluster logs:

```output
Cluster resource '<IP name in AG role>' of type 'IP Address' in cluster role '<AG Name>' failed.
An Event ID: 1069 with status 10048 can be identified from cluster logs with events like:
Resource IP Address 10.0.1.0 called SetResourceStatusEx: checkpoint 5. Old state OnlinePending, new state OnlinePending, AppSpErrorCode 0, Flags 0, nores=false
IP Address <IP Address 10.0.1.0>: IpaOnlineThread: **Listening on probe port 59999** failed with status **10048**
```

Status [**10048**](/windows/win32/winsock/windows-sockets-error-codes-2) happens if an application attempts to bind a socket to an **IP address/port that has already been used** for an existing socket.


## Known issues

Review the resolutions for some commonly known issues and errors.

### Resource contention causes failover

Exhausting I/O or CPU capacity for the VM can cause your availability group to fail over. Identifying the contention that happens right before the failover is the most reliable way to identify what is causing automatic failover.

#### Identify I/O bottlenecks

Use [I/O Analysis](storage-performance-analysis.md) in the Azure portal to identify disk performance issues that can cause a failover.

#### Monitor VM storage metrics

[Monitor Azure Virtual Machines](/azure/virtual-machines/monitor-vm) to look at the [Storage IO Utilization metrics](/azure/virtual-machines/disks-metrics#storage-io-utilization-metrics) to understand VM or disk level latency.

Follow these steps to review the **Azure VM Overall IO Exhaustion event** (Azure Contributor or Reader role required):

1. Navigate to your *Virtual Machine* in the [Azure portal](https://portal.azure.com) - not the [SQL virtual machines](manage-sql-vm-portal.md).
1. Select **Metrics** under **Monitoring** to open the **Metrics** page.
1. Select **Local time** to specify the time range you're interested in, and the time zone, either local to the VM, or UTC/GMT.

   :::image type="content" source="media/hadr-cluster-best-practices/specify-time-for-metrics.png" alt-text="Screenshot of the Metrics page in the Azure portal, selecting changing the time frame of the graph.":::

1. Select **Add metric** to add the following two metrics to see the graph:
   - **VM Cached Bandwidth Consumed Percentage**
   - **VM Uncached Bandwidth Consumed Percentage**

:::image type="content" source="./media/hadr-cluster-best-practices/hadr-metrics-cached-uncached.png" alt-text="Screenshot of the Metrics page in the Azure portal.":::

### Azure VM HostEvents causes failover

An Azure VM HostEvent can cause your availability group to fail over.

#### Check Azure Monitor Activity log

The [Azure Monitor activity log](/azure/azure-monitor/essentials/activity-log) provides insight into subscription-level events, including when a resource is modified or a virtual machine is started. You can view the activity log in the Azure portal, or retrieve entries with PowerShell and the Azure CLI.

To check the Azure Monitor activity log (Azure Reader role required), follow these steps:

1. Navigate to your Virtual Machine in Azure portal
1. Select **Activity Log** on the Virtual Machine pane
1. Select **Timespan** and then choose the time frame when your availability group failed over. Select **Apply**.

   :::image type="content" source="./media/hadr-cluster-best-practices/activity-log.png" alt-text="Screenshot of the Azure portal, showing the Activity log. ":::

#### Check Resource Health

If Azure has further information about the root cause of a platform-initiated unavailability, that information might be posted on the [Azure VM - Resource Health overview](/azure/service-health/resource-health-overview#root-cause-information) page up to 72 hours after the initial unavailability. This information is only available for virtual machines at this time.

To check Resource Health, follow these steps:

1. Navigate to your Virtual Machine in Azure portal
1. Select **Resource Health** under the **Health** pane.

:::image type="content" source="./media/hadr-cluster-best-practices/resource-health.png" alt-text="Screenshot of the Resource Health page in the Azure portal.":::

You can also configure alerts based on health events from this page.

### Cluster node removed from membership

If the [Windows Cluster heartbeat and threshold settings](#heartbeat-and-threshold) are too aggressive for your environment, you might experience cluster membership issues.

#### Symptoms

You might see the following message in the system event log frequently:

```output
Error 1135
Cluster node 'Node1' was removed from the active failover cluster membership.
The Cluster service on this node may have stopped. This could also be due to the node having
lost communication with other active nodes in the failover cluster. Run the Validate a
Configuration Wizard to check your network configuration. If the condition persists, check
for hardware or software errors related to the network adapters on this node. Also check for
failures in any other network components to which the node is connected such as hubs, switches, or bridges.
```

For more information, review [Troubleshooting cluster issue with Event ID 1135](/windows-server/troubleshoot/troubleshooting-cluster-event-id-1135).

### Lease expiration errors

If monitoring is too aggressive for your environment, you might see frequent availability group or FCI restarts, failures, or failovers.

#### Symptoms

For availability groups, you might see the following messages in the SQL Server error log:

```output
Error 19407: The lease between availability group 'PRODAG' and the Windows Server Failover Cluster has expired.
A connectivity issue occurred between the instance of SQL Server and the Windows Server Failover Cluster.
To determine whether the availability group is failing over correctly, check the corresponding availability group
resource in the Windows Server Failover Cluster
```

```output
Error 19419: The renewal of the lease between availability group '%.*ls' and the Windows Server Failover Cluster
failed because the existing lease is no longer valid.
```

### Resolution

Adjust your monitoring settings as described in the [Relaxed monitoring](#relaxed-monitoring) section.

### Connection timeout errors

If the **session timeout** is too aggressive for your availability group environment, you might experience connection failures.

#### Symptoms

You might see the following messages frequently:

```output
Error 35201: A connection timeout has occurred while attempting to establish a connection to availability
replica 'replicaname' with ID [availability_group_id]. Either a networking or firewall issue exists,
or the endpoint address provided for the replica is not the database mirroring endpoint of the host server instance.
```

```output
Error 35206
A connection timeout has occurred on a previously established connection to availability
replica 'replicaname' with ID [availability_group_id]. Either a networking or a firewall issue
exists, or the availability replica has transitioned to the resolving role.
```

#### Resolution

Adjust your session timeout as described in the [Relaxed monitoring](#relaxed-monitoring) section.

### Group not failing over

If the **Maximum Failures in the Specified Period** value is too low and you're experiencing intermittent failures due to transient issues, your availability group could end in a failed state.

#### Symptoms

```output
Not failing over group <Resource name>, failoverCount 3, failoverThresholdSetting <Number>, computedFailoverThreshold 2.
```

#### Resolution

Adjust your monitoring, session timeout, and increase the **Maximum Failures in the Specified Period** value to tolerate more transient failures.

### Event 1196 - Network name resource failed DNS registration

#### Resolution

- Check the NIC settings for each of your cluster nodes to make sure there are no external DNS records present.
- Ensure the A record for your cluster exists on your internal DNS servers. If not, create a new A Record manually in DNS Server for the Cluster Access Control object and check the Allow any authenticated users to update DNS Records with the same owner name.
- Take the Resource "Cluster Name" with IP Resource offline and fix it.

### Event 157 - Disk has been surprised removed

#### Causes

This can happen in the following scenarios:

- The Storage Spaces property `AutomaticClusteringEnabled` is set to `True` for an AG environment.
- Running a Validation Report with Storage option can trigger the disk reset or surprise removed event.
- Storage system [Throttling](/azure/virtual-machines/windows/disk-performance-windows#storage-io-utilization-metrics) can trigger the disk surprise remove event.

#### Resolution

Change the Storage Spaces property `AutomaticClusteringEnabled` to `False`.

### Event 1206 - Cluster network name resource can't be brought online

#### Cause

The computer object associated with the resource couldn't be updated in the domain.

#### Resolution

Make sure you have [appropriate permissions on domain](https://techcommunity.microsoft.com/t5/sql-server-support/error-during-installation-of-an-sql-server-failover-cluster/ba-p/317873).

### Windows Clustering errors

#### Cluster connectivity issues

You might encounter issues while setting up a Windows Server failover cluster or its connectivity if you don't have [Cluster Service Ports open for communication](/troubleshoot/windows-server/networking/service-overview-and-network-port-requirements#cluster-service).

#### Missing Windows Cluster IP on Windows Server 2019

If you are on Windows Server 2019 and you don't see a Windows Cluster IP, you have configured [Distributed Network Name](failover-cluster-instance-distributed-network-name-dnn-configure.md), which is only supported on SQL Server 2019. If you have previous versions of SQL Server, you can remove and [Recreate the Cluster using Network Name](availability-group-manually-configure-tutorial.md#create-the-cluster).

#### Additional resources

Review other Windows Failover [Failover Clustering system log events](/windows-server/failover-clustering/system-events) and their solutions.

## Related content

For detailed guidance on each optimization area:

- **[Quick checklist](performance-guidelines-best-practices-checklist.md)** - Review the full best practices checklist
- **[VM size](performance-guidelines-best-practices-vm-size.md)** - Choose the right VM series and configuration
- **[Storage](performance-guidelines-best-practices-storage.md)** - Optimize disk configuration and performance
- **[Security](security-considerations-best-practices.md)** - Implement security best practices
- **[Collect baseline](performance-guidelines-best-practices-collect-baseline.md)** - Establish performance baselines
- **[Updating SQL Server](servicing-updates-guidelines.md)** - Keep SQL Server up to date

For more information about HADR:

- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Failover cluster instances with SQL Server on Azure VMs](failover-cluster-instance-overview.md)
- [Failover cluster instance overview](/sql/sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server)
