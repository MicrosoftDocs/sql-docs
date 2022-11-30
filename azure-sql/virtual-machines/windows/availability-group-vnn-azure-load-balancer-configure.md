---
title: Configure an Azure load balancer for an AG VNN listener
description: Learn to configure an Azure load balancer to route traffic to the virtual network name (VNN) listener for your availability group with SQL Server on Azure VMs for high availability and disaster recovery (HADR).
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma
ms.date: 11/10/2021
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
tags: azure-resource-manager
---
# Configure an Azure load balancer for an AG VNN listener
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!TIP]
> Eliminate the need for an Azure load balancer for your Always On availability group (AG) by creating your SQL Server virtual machines (VMs) in [multiple subnets](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md) within the same Azure virtual network.

On Azure virtual machines, clusters use a load balancer to hold an IP address that needs to be on one cluster node at a time. In this solution, the load balancer holds the IP address for the virtual network name (VNN) listener for the Always On availability group when the SQL Server VMs are in a single subnet. 

This article teaches you to configure a load balancer by using the Azure Load Balancer service. The load balancer will route traffic to your [availability group listener](availability-group-overview.md) with SQL Server on Azure VMs for high availability and disaster recovery (HADR). 

For an alternative connectivity option for customers who are on SQL Server 2019 CU8 and later, consider a [distributed network name (DNN) listener](availability-group-vnn-azure-load-balancer-configure.md) instead. A DNN listener offers simplified configuration and improved failover.  

## Prerequisites

Before you complete the steps in this article, you should already have:

- Decided that Azure Load Balancer is the appropriate [connectivity option for your availability group](hadr-windows-server-failover-cluster-overview.md#virtual-network-name-vnn).
- Configured your [availability group listener](availability-group-overview.md).
- Installed the latest version of [PowerShell](/powershell/scripting/install/installing-powershell-core-on-windows). 

## Create a load balancer

You can create either of these types of load balancers:

- **Internal**: An internal load balancer can be accessed only from private resources that are internal to the network. When you configure an internal load balancer and its rules, use the same IP address as the availability group listener for the front-end IP address. 
- **External**: An external load balancer can route traffic from the public to internal resources. When you configure an external load balancer, you can't use the same IP address as the availability group listener because the listener IP address can't be a public IP address. 

  To use an external load balancer, logically allocate an IP address in the same subnet as the availability group that doesn't conflict with any other IP address. Use this address as the front-end IP address for the load-balancing rules. 

To create the load balancer:

1. In the [Azure portal](https://portal.azure.com), go to the resource group that contains the virtual machines.

1. Select **Add**. Search Azure Marketplace for **load balancer**. Select **Load Balancer**.

1. Select **Create**.

1. In **Create load balancer**, on the **Basics** tab, set up the load balancer by using the following values:

   - **Subscription**: Your Azure subscription.
   - **Resource group**: The resource group that contains your virtual machines.
   - **Name**: A name that identifies the load balancer.
   - **Region**: The Azure location that contains your virtual machines.
   - **SKU**: **Standard**.
   - **Type**: Either **Public** or **Internal**. An internal load balancer can be accessed from within the virtual network. Most Azure applications can use an internal load balancer. If your application needs access to SQL Server directly over the internet, use a public load balancer.
   - **Tier**: **Regional**.

   :::image type="content" source="media/availability-group-manually-configure-tutorial-single-subnet/84-create-load-balancer.png" alt-text="Screenshot of the Azure portal that shows the page for basic information about a load balancer.":::

1. Select **Next: Frontend IP configuration**.

1. Select **Add a frontend IP configuration**.

   :::image type="content" source="media/availability-group-manually-configure-tutorial-single-subnet/add-fe-ip-config.png" alt-text="Screenshot of the Azure portal that shows the button for adding a front-end IP configuration.":::

1. Set up the front-end IP address by using the following values:

   - **Name**: A name that identifies the front-end IP configuration.
   - **Virtual network**: The same network as the virtual machines.
   - **Subnet**: The same subnet as the virtual machines.
   - **IP address assignment**: **Static**.
   - **Private IP address**: The IP address that you assigned to the clustered network resource.
   - **Availability zone**: An optional availability zone to deploy your IP address to.

   :::image type="content" source="media/availability-group-manually-configure-tutorial-single-subnet/add-fe-ip-config-details.png" alt-text="Screenshot of the Azure portal that shows the page for configuring a front-end IP address.":::

1. Select **Add** to create the front-end IP address.

1. Select **Review + Create** to create the load balancer.

## Configure a back-end pool

1. Return to the Azure resource group that contains the virtual machines and locate the new load balancer. You might need to refresh the view on the resource group. Select the load balancer.

1. Select **Backend pools**, and then select **+Add**.

1. For **Name**, provide a name for the back-end pool.

1. For **Backend Pool Configuration**, select **NIC**.

1. Select **Add** to associate the back-end pool with the availability set that contains the VMs.

1. Under **Virtual machine**, choose the virtual machines that will participate as cluster nodes. Be sure to include all virtual machines that will host the failover cluster instance. 

   Add only the primary IP address of each VM. Don't add any secondary IP addresses.

1. Select **Add** to add the virtual machines to the back-end pool.

1. Select **Save** to create the back-end pool.

## Configure a health probe

1. On the pane for the load balancer, select **Health probes**.

1. On the **Add health probe** pane, <span id="probe"> </span> set the following parameters:

   - **Name**: A name for the health probe.
   - **Protocol**: **TCP**.
   - **Port**: The port that you created in the firewall for the health probe [when preparing the VM](failover-cluster-instance-prepare-vm.md#uninstall-sql-server-1). In this article, the example uses TCP port **59999**.
   - **Interval**: **5 Seconds**.

1. Select **Add**.

## Set load-balancing rules

1. On the pane for the load balancer, select **Load-balancing rules**.
1. Select **Add**.
1. Set these parameters:

   - **Name**: A name for the load-balancing rule.
   - **Frontend IP address**: The IP address that you set when you configured the front end.
   - **Backend pool**: The back-end pool that contains the virtual machines targeted for the load balancer.
   - **HA Ports**: Enables load balancing on all ports for TCP and UDP protocols.
   - **Protocol**: **TCP**.
   - **Port**: The SQL Server TCP port. The default is **1433**.
   - **Backend port**: The same port as the **Port** value when you enable **Floating IP (direct server return)**.
   - **Health probe**: The health probe that you configured earlier.
   - **Session persistence**: **None**.
   - **Idle timeout (minutes)**: **4**.
   - **Floating IP (direct server return)**: Enabled.

1. Select **Save**.

## Configure a cluster probe

Set the cluster probe's port parameter in PowerShell.

# [Private load balancer](#tab/ilb)

To set the cluster probe's port parameter, update the variables in the following script with values from your environment. Remove the angle brackets (`<` and `>`) from the script.

```powershell
$ClusterNetworkName = "<Cluster Network Name>"
$IPResourceName = "<AG Listener IP Address Resource Name>" 
$ILBIP = "<n.n.n.n>" 
[int]$ProbePort = <nnnnn>

Import-Module FailoverClusters

Get-ClusterResource $IPResourceName | Set-ClusterParameter -Multiple @{"Address"="$ILBIP";"ProbePort"=$ProbePort;"SubnetMask"="255.255.255.255";"Network"="$ClusterNetworkName";"EnableDhcp"=0}
```

The following table describes the values that you need to update:

|**Variable**|**Value**|
|---------|---------|
|`ClusterNetworkName`| The name of the Windows Server failover cluster for the network. In **Failover Cluster Manager** > **Networks**, right-click the network and select **Properties**. The correct value is under **Name** on the **General** tab.|
|`IPResourceName`|The resource name for the IP address of the AG listener. In **Failover Cluster Manager** > **Roles**, under the availability group role, under **Server Name**, right-click the IP address resource and select **Properties**. The correct value is under **Name** on the **General** tab.|
|`ILBIP`|The IP address of the internal load balancer. This address is configured in the Azure portal as the front-end address of the internal load balancer. This is the same IP address as the availability group listener. You can find it in **Failover Cluster Manager**, on the same properties page where you located the value for `IPResourceName`.|
|`ProbePort`|The probe port that you configured in the health probe of the load balancer. Any unused TCP port is valid.|
|`SubnetMask`| The subnet mask for the cluster parameter. It must be the TCP/IP broadcast address: `255.255.255.255`.| 

After you set the cluster probe, you can see all the cluster parameters in PowerShell. Run this script:

```powershell
Get-ClusterResource $IPResourceName | Get-ClusterParameter
```

# [Public load balancer](#tab/elb)

To set the cluster probe's port parameter, update the variables in the following script with values from your environment. Remove the angle brackets (`<` and `>`) from the script.

```powershell
$ClusterNetworkName = "<Cluster Network Name>"
$IPResourceName = "<AG Listener IP Address Resource Name>" 
$ELBIP = "<n.n.n.n>" 
[int]$ProbePort = <nnnnn>

Import-Module FailoverClusters

Get-ClusterResource $IPResourceName | Set-ClusterParameter -Multiple @{"Address"="$ELBIP";"ProbePort"=$ProbePort;"SubnetMask"="255.255.255.255";"Network"="$ClusterNetworkName";"EnableDhcp"=0}
```

The following table describes the values that you need to update:

|**Variable**|**Value**|
|---------|---------|
|`ClusterNetworkName`| The name of the Windows Server failover cluster for the network. In **Failover Cluster Manager** > **Networks**, right-click the network and select **Properties**. The correct value is under **Name** on the **General** tab.|
|`IPResourceName`|The resource name for the IP address of the AG listener. In **Failover Cluster Manager** > **Roles**, under the availability group role, under **Server Name**, right-click the IP address resource and select **Properties**. The correct value is under **Name** on the **General** tab.|
|`ELBIP`|The IP address of the external load balancer. This address is configured in the Azure portal as the front-end address of the external load balancer. It's used to connect to the public load balancer from external resources.|
|`ProbePort`|The probe port that you configured in the health probe of the load balancer. Any unused TCP port is valid.|
|`SubnetMask`| The subnet mask for the cluster parameter. It must be the TCP/IP broadcast address: `255.255.255.255`.| 

After you set the cluster probe, you can see all the cluster parameters in PowerShell. Run this script:

```powershell
Get-ClusterResource $IPResourceName | Get-ClusterParameter
```

> [!NOTE]
> Because there is no private IP address for the external load balancer, users can't directly use the VNN DNS name as it resolves the IP address within the subnet. Use the public IP address of the public load balancer, or configure another DNS mapping on the DNS server. 

---

## Modify the connection string 

For clients that support it, add `MultiSubnetFailover=True` to the connection string. Although the `MultiSubnetFailover` connection option isn't required, it provides the benefit of a faster subnet failover. This is because the client driver tries to open a TCP socket for each IP address in parallel. The client driver waits for the first IP address to respond with success. After the successful response, the client driver uses that IP address for the connection.

If your client doesn't support the `MultiSubnetFailover` parameter, you can modify the `RegisterAllProvidersIP` and `HostRecordTTL` settings to prevent connectivity delays after failover. 

Use PowerShell to modify the `RegisterAllProvidersIp` and `HostRecordTTL` settings: 

```powershell
Get-ClusterResource yourListenerName | Set-ClusterParameter RegisterAllProvidersIP 0  
Get-ClusterResource yourListenerName|Set-ClusterParameter HostRecordTTL 300 
```

To learn more, see the SQL Server [listener connection timeout](/troubleshoot/sql/availability-groups/listener-connection-times-out) documentation. 

> [!TIP]
> - Set the `MultiSubnetFailover parameter` to `true` in the connection string, even for HADR solutions that span a single subnet. This setting supports future spanning of subnets without the need to update connection strings.  
> - By default, clients cache cluster DNS records for 20 minutes. By reducing `HostRecordTTL`, you reduce the time to live (TTL) for the cached record. Legacy clients can then reconnect more quickly. As such, reducing the `HostRecordTTL` setting might increase traffic to the DNS servers.

## Test failover

Test failover of the clustered resource to validate cluster functionality:

1. Open [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms) and connect to your availability group listener. 
1. In **Object Explorer**, expand **Always On Availability Group**. 
1. Right-click the availability group and select **Failover**. 
1. Follow the wizard prompts to fail over the availability group to a secondary replica. 

Failover succeeds when the replicas switch roles and are both synchronized. 

## Test connectivity

To test connectivity, sign in to another virtual machine in the same virtual network. Open SQL Server Management Studio and connect to the availability group listener.

> [!NOTE]
> If you need to, you can [download SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms).

## Next steps

After the VNN is created, consider optimizing the [cluster settings for SQL Server VMs](hadr-cluster-best-practices.md). 

To learn more, see:

- [Windows Server failover cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Overview of Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)



