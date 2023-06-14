---
author: MashaMSFT
ms.author: mathoma
ms.date: 03/29/2023
ms.service: virtual-machines
ms.topic: include
---
High availability and disaster recovery (HADR) features, such as the [Always On availability group](../virtual-machines/windows/availability-group-overview.md) and the [failover cluster instance](../virtual-machines/windows/failover-cluster-instance-overview.md) rely on underlying [Windows Server Failover Cluster](../virtual-machines/windows/hadr-windows-server-failover-cluster-overview.md) technology. Review the best practices for modifying your HADR settings to better support the cloud environment.

For your Windows cluster, consider these best practices:

* Deploy your SQL Server VMs to multiple subnets whenever possible to avoid the dependency on an Azure Load Balancer or a distributed network name (DNN) to route traffic to your HADR solution. 
* Change the cluster to less aggressive parameters to avoid unexpected outages from transient network failures or Azure platform maintenance. To learn more, see [heartbeat and threshold settings](../virtual-machines/windows/hadr-cluster-best-practices.md#heartbeat-and-threshold). For Windows Server 2012 and later, use the following recommended values: 
   - **SameSubnetDelay**:  1 second
   - **SameSubnetThreshold**: 40 heartbeats
   - **CrossSubnetDelay**: 1 second
   - **CrossSubnetThreshold**:  40 heartbeats
* Place your VMs in an availability set or different availability zones.  To learn more, see [VM availability settings](../virtual-machines/windows/hadr-cluster-best-practices.md#vm-availability-settings). 
* Use a single NIC per cluster node. 
* Configure cluster [quorum voting](../virtual-machines/windows/hadr-cluster-best-practices.md#quorum-voting) to use 3 or more odd number of votes. Don't assign votes to DR regions. 
* Carefully monitor [resource limits](../virtual-machines/windows/hadr-cluster-best-practices.md#resource-limits) to avoid unexpected restarts or failovers due to resource constraints.
   - Ensure your OS, drivers, and SQL Server are at the latest builds. 
   - Optimize performance for SQL Server on Azure VMs. Review the other sections in this article to learn more. 
   - Reduce or spread out workload to avoid resource limits. 
   - Move to a VM or disk that his higher limits to avoid constraints. 

For your SQL Server availability group or failover cluster instance, consider these best practices: 

* If you're experiencing frequent unexpected failures, follow the performance best practices outlined in the rest of this article. 
* If optimizing SQL Server VM performance doesn't resolve your unexpected failovers, consider [relaxing the monitoring](../virtual-machines/windows/hadr-cluster-best-practices.md#relaxed-monitoring) for the availability group or failover cluster instance. However, doing so may not address the underlying source of the issue and could mask symptoms by reducing the likelihood of failure. You may still need to investigate and address the underlying root cause. For Windows Server 2012 or higher, use the following recommended values: 
   - **Lease timeout**: Use this equation to calculate the maximum lease time-out value:   
   `Lease timeout < (2 * SameSubnetThreshold * SameSubnetDelay)`.   
   Start with 40 seconds. If you're using the relaxed `SameSubnetThreshold` and `SameSubnetDelay` values recommended previously, don't exceed 80 seconds for the lease timeout value.   
   - **Max failures in a specified period**: Set this value to 6. 
* When using the virtual network name (VNN) and an Azure Load Balancer to connect to your HADR solution, specify `MultiSubnetFailover = true` in the connection string, even if your cluster only spans one subnet. 
   - If the client doesn't support `MultiSubnetFailover = True` you may need to set `RegisterAllProvidersIP = 0` and `HostRecordTTL = 300` to cache client credentials for shorter durations. However, doing so may cause additional queries to the DNS server. 
- To connect to your HADR solution using the distributed network name (DNN), consider the following:
   - You must use a client driver that supports `MultiSubnetFailover = True`, and this parameter must be in the connection string. 
   - Use a unique DNN port in the connection string when connecting to the DNN listener for an availability group. 
- Use a database mirroring connection string for a basic availability group to bypass the need for a load balancer or DNN. 
- Validate the sector size of your VHDs before deploying your high availability solution to avoid having misaligned I/Os. See [KB3009974](https://support.microsoft.com/topic/kb3009974-fix-slow-synchronization-when-disks-have-different-sector-sizes-for-primary-and-secondary-replica-log-files-in-sql-server-ag-and-logshipping-environments-ed181bf3-ce80-b6d0-f268-34135711043c) to learn more. 
- If the SQL Server database engine, Always On availability group listener, or failover cluster instance health probe are configured to use a port between 49,152 and 65,536 (the [default dynamic port range for TCP/IP](/windows/client-management/troubleshoot-tcpip-port-exhaust#default-dynamic-port-range-for-tcpip)), add an exclusion for each port. Doing so prevents other systems from being dynamically assigned the same port. The following example creates an exclusion for port 59999:   
`netsh int ipv4 add excludedportrange tcp startport=59999 numberofports=1 store=persistent`

