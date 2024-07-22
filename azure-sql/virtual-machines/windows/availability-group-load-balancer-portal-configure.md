---
title: Configure a load balancer & availability group listener (Azure portal)
description: Step-by-step instructions for creating a listener for an Always On availability group for SQL Server in Azure virtual machines
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: mathoma, randolphwest
ms.date: 06/18/2024
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
editor: monicar
---
# Configure a load balancer & availability group listener (SQL Server on Azure VMs)

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

[!INCLUDE [tip-for-multi-subnet-ag](../../includes/virtual-machines-ag-listener-multi-subnet.md)]

This article explains how to create a load balancer for a SQL Server Always On availability group in Azure Virtual Machines within a single subnet that are running with Azure Resource Manager. An availability group requires a load balancer when the SQL Server instances are on Azure Virtual Machines. The load balancer stores the IP address for the availability group listener. If an availability group spans multiple regions, each region needs a load balancer.

To complete this task, you need to have a SQL Server Always On availability group deployed in Azure VMs that are running with Resource Manager. Both SQL Server virtual machines must belong to the same availability set. You can use the [Microsoft template](./availability-group-quickstart-template-configure.md) to automatically create the availability group in Resource Manager. This template automatically creates an internal load balancer for you.

If you prefer, you can [manually configure an availability group](availability-group-manually-configure-tutorial-single-subnet.md).

This article requires that your availability groups are already configured.

View related articles:

- [Configure Always On availability groups in Azure VM (GUI)](availability-group-manually-configure-tutorial-single-subnet.md)
- [Configure a VNet-to-VNet connection by using Azure Resource Manager and PowerShell](/azure/vpn-gateway/vpn-gateway-vnet-vnet-rm-ps)

By walking through this article, you create and configure a load balancer in the Azure portal. After the process is complete, you configure the cluster to use the IP address from the load balancer for the availability group listener.

## Create & configure load balancer

In this portion of the task, do the following steps:

1. In the Azure portal, create the load balancer and configure the IP address.
1. Configure the back-end pool.
1. Create the probe.
1. Set the load-balancing rules.

> [!NOTE]  
> If the SQL Server instances are in multiple resource groups and regions, perform each step twice, once in each resource group.
>

> [!IMPORTANT]  
> On September 30, 2025, the Basic SKU for the Azure Load Balancer will be retired. For more information, see the [official announcement](https://azure.microsoft.com/updates/azure-basic-load-balancer-will-be-retired-on-30-september-2025-upgrade-to-standard-load-balancer/). If you're currently using Basic Load Balancer, upgrade to Standard Load Balancer prior to the retirement date. For guidance, review [upgrade load balancer](/azure/load-balancer/load-balancer-basic-upgrade-guidance).

### Step 1: Create the load balancer and configure the IP address

First, create the load balancer.

1. In the Azure portal, open the resource group that contains the SQL Server virtual machines.

1. In the resource group, select **+ Create**.

1. Search for **load balancer**. Choose **Load Balancer** (published by **Microsoft**) in the search results.

1. On the **Load Balancer** pane, select **Create**.

1. Configure the following parameters for the load balancer.

   | Setting | Field |
   | --- | --- |
   | **Subscription** | Use the same subscription as the virtual machine. |
   | **Resource Group** | Use the same resource group as the virtual machine. |
   | **Name** | Use a text name for the load balancer, for example **sqlLB**. |
   | **Region** | Use the same region as the virtual machine. |
   | **SKU** | Standard |
   | **Type** | Internal |

   The Azure portal pane should look like this:

   :::image type="content" source="media/availability-group-manually-configure-tutorial-single-subnet/84-create-load-balancer.png" alt-text="Screenshot of the Azure portal, Create Load Balancer page." lightbox="media/availability-group-manually-configure-tutorial-single-subnet/84-create-load-balancer.png":::

1. Select **Next: Frontend IP Configuration**

1. Select **Add a frontend IP Configuration**

   :::image type="content" source="media/availability-group-manually-configure-tutorial-single-subnet/add-fe-ip-config.png" alt-text="Screenshot of Azure portal, Create load balancer page, showing frontend IP configuration tab." lightbox="media/availability-group-manually-configure-tutorial-single-subnet/add-fe-ip-config.png":::

1. Set up the frontend IP using the following values:

   - **Name**: A name that identifies the frontend IP configuration
   - **Virtual network**: The same network as the virtual machines.
   - **Subnet**: The subnet as the virtual machines.
   - **IP address assignment**: Static.
   - **IP address**: Use an available address from subnet. **Use this address for your availability group listener**. Notice this is different from your cluster IP address.
   - **Availability zone**: Optionally choose and availability zone to deploy your IP to.

   The following image shows the **Add frontend IP Configuration** UI:

   :::image type="content" source="media/availability-group-manually-configure-tutorial-single-subnet/add-fe-ip-config-details.png" alt-text="Screenshot of Azure portal, add frontend IP configuration page.":::

1. Select **Add** to create the frontend IP.

1. Choose **Review + Create** to validate the configuration, and then **Create** to create the load balancer and the frontend IP.

Azure creates the load balancer. The load balancer belongs to a specific network, subnet, resource group, and location. After Azure completes the task, verify the load balancer settings in Azure.

To configure the load balancer, you need to create a backend pool, a probe, and set the load balancing rules. Do these in the Azure portal.

### Step 2: Configure the backend pool

Azure calls the back-end address pool *backend pool*. In this case, the backend pool is the addresses of the two SQL Server instances in your availability group.

1. In the Azure portal, go to your availability group. You might need to refresh the view to see the newly created load balancer.

   :::image type="content" source="media/availability-group-manually-configure-tutorial-single-subnet/86-find-load-balancer.png" alt-text="Screenshot of Azure portal, resource group page, searching for the Load Balancer." lightbox="media/availability-group-manually-configure-tutorial-single-subnet/86-find-load-balancer.png":::

1. Select the load balancer, select **Backend pools**, and select **+Add**.

1. Provide a **Name** for the Backend pool.

1. Select **NIC** for Backend Pool Configuration.

1. Select **Add** to associate the backend pool with the availability set that contains the VMs.

1. Under **Virtual machine** choose the SQL Server virtual machines that will host availability group replicas.

   > [!NOTE]  
   > If both virtual machines are not specified, connections will only succeed to the primary replica.

1. Select **Add** to add the virtual machines to the backend pool.

1. Select **Save** to create the backend pool.

Azure updates the settings for the back-end address pool. Now your availability set has a pool of two SQL Server instances.

### Step 3: Create a probe

The probe defines how Azure verifies which of the SQL Server instances currently owns the availability group listener. Azure probes the service based on the IP address on a port that you define when you create the probe.

1. Select the load balancer, choose **Health probes**, and then select **+Add**.

1. Set the listener health probe as follows:

   | Setting | Description | Example
   | --- | --- |---
   | **Name** | Text | SQLAlwaysOnEndPointProbe |
   | **Protocol** | Choose TCP | TCP |
   | **Port** | Any unused port | 59999 |
   | **Interval** | The amount of time between probe attempts in seconds | 5 |

1. Select **Add** to set the health probe.

> [!NOTE]  
> Make sure that the port you specify is open on the firewall of both SQL Server instances. Both instances require an inbound rule for the TCP port that you use. For more information, see [Add or Edit Firewall Rule](/previous-versions/orphan-topics/ws.11/cc753558(v=ws.11)).
>

Azure creates the probe and then uses it to test which SQL Server instance has the listener for the availability group.

### Step 4: Set the load-balancing rules

The load-balancing rules configure how the load balancer routes traffic to the SQL Server instances. For this load balancer, you enable direct server return because only one of the two SQL Server instances owns the availability group listener resource at a time.

1. Select the load balancer, choose **Load balancing rules**, and select **+Add**.

1. Set the listener load balancing rules as follows.

   | Setting | Description | Example
   | --- | --- |---
   | **Name** | Text | SQLAlwaysOnEndPointListener |
   | **Frontend IP address** | Choose an address | Use the address that you created when you created the load balancer. |
   | **Backend pool** | Choose the backend pool | Select the backend pool containing the virtual machines targeted for the load balancer. |
   | **Protocol** | Choose TCP | TCP |
   | **Port** | Use the port for the availability group listener | 1433 |
   | **Backend Port** | This field isn't used when Floating IP is set for direct server return | 1433 |
   | **Health Probe** | The name you specified for the probe | SQLAlwaysOnEndPointProbe |
   | **Session Persistence** | Dropdown list | **None** |
   | **Idle Timeout** | Minutes to keep a TCP connection open | 4 |
   | **Floating IP (direct server return)** | A flow topology and an IP address mapping scheme | Enabled |

   > [!WARNING]  
   > Direct server return is set during creation. It cannot be changed.
   >

   > [!NOTE]  
   > You might have to scroll down the pane to view all the settings.
   >

1. Select **Save** to set the listener load balancing rules.

Azure configures the load-balancing rule. Now the load balancer is configured to route traffic to the SQL Server instance that hosts the listener for the availability group.

At this point, the resource group has a load balancer that connects to both SQL Server machines. The load balancer also contains an IP address for the SQL Server Always On availability group listener, so that either machine can respond to requests for the availability groups.

> [!NOTE]  
> If your SQL Server instances are in two separate regions, repeat the steps in the other region. Each region requires a load balancer.
>

### Add the cluster core IP address for the Windows Server Failover Cluster (WSFC)

The WSFC IP address also needs to be on the load balancer. If you're using Windows Server 2019, skip this process as the cluster creates a **Distributed Server Name** instead of the **Cluster Network Name**.

1. In the Azure portal, go to the same Azure load balancer. Select **Frontend IP configuration** and select **+Add**. Use the IP Address you configured for the WSFC in the cluster core resources. Set the IP address as static.

1. On the load balancer, select **Health probes**, and then select **+Add**.

1. Set the WSFC cluster core IP address health probe as follows:

   | Setting | Description | Example
   | --- | --- |---
   | **Name** | Text | WSFCEndPointProbe |
   | **Protocol** | Choose TCP | TCP |
   | **Port** | Any unused port | 58888 |
   | **Interval** | The amount of time between probe attempts in seconds | 5 |

1. Select **Add** to set the health probe.

1. Set the load balancing rules. Select **Load balancing rules**, and select **+Add**.

1. Set the cluster core IP address load balancing rules as follows.

   | Setting | Description | Example
   | --- | --- |---
   | **Name** | Text | WSFCEndPoint |
   | **Frontend IP address** | Choose an address | Use the address that you created when you configured the WSFC IP address. This is different from the listener IP address |
   | **Backend pool** | Choose the backend pool | Select the backend pool containing the virtual machines targeted for the load balancer. |
   | **Protocol** | Choose TCP | TCP |
   | **Port** | Use the port for the cluster IP address. This is an available port that isn't used for the listener probe port. | 58888 |
   | **Backend Port** | This field isn't used when Floating IP is set for direct server return | 58888 |
   | **Probe** | The name you specified for the probe | WSFCEndPointProbe |
   | **Session Persistence** | Dropdown list | **None** |
   | **Idle Timeout** | Minutes to keep a TCP connection open | 4 |
   | **Floating IP (direct server return)** | A flow topology and an IP address mapping scheme | Enabled |

   > [!WARNING]  
   > Direct server return is set during creation. It cannot be changed.
   >

1. Select **OK** to set the load balancing rules.

## Configure the cluster to use the load balancer IP address

The next step is to configure the listener on the cluster, and bring the listener online. Do the following steps:

1. Create the availability group listener on the failover cluster.

1. Bring the listener online.

### Step 5: Create the availability group listener on the failover cluster

In this step, you manually create the availability group listener in Failover Cluster Manager and SQL Server Management Studio.

[!INCLUDE [ag-listener-configure](../../includes/virtual-machines-ag-listener-configure.md)]

### Verify the configuration of the listener

If the cluster resources and dependencies are correctly configured, you should be able to view the listener in SQL Server Management Studio. To set the listener port, do the following steps:

1. Start SQL Server Management Studio, and then connect to the primary replica.

1. Go to **Always On High Availability** > **Availability Groups** > **Availability Group Listeners**.

    You should now see the listener name that you created in Failover Cluster Manager.

1. Right-click the listener name, and then select **Properties**.

1. In the **Port** box, specify the port number for the availability group listener by using the $EndpointPort you used earlier (1433 was the default), and then select **OK**.

You now have an availability group in Azure virtual machines running in Resource Manager mode.

## Test the connection to the listener

Test the connection by doing the following steps:

1. Use remote desktop protocol (RDP) to connect to a SQL Server instance that's in the same virtual network, but doesn't own the replica. This server can be the other SQL Server instance in the cluster.

1. Use **sqlcmd** utility to test the connection. For example, the following script establishes a **sqlcmd** connection to the primary replica through the listener with Windows authentication:

    ```console
    sqlcmd -S <listenerName> -E
    ```

The SQLCMD connection automatically connects to the SQL Server instance that hosts the primary replica.

## Create an IP address for an additional availability group

Each availability group uses a separate listener. Each listener has its own IP address. Use the same load balancer to hold the IP address for additional listeners. Add only the primary IP address of the VM to the back-end pool of the load balancer as the [secondary VM IP address doesn't support floating IP](/azure/load-balancer/load-balancer-floating-ip).

To add an IP address to a load balancer with the Azure portal, do the following steps:

1. In the Azure portal, open the resource group that contains the load balancer, and then select the load balancer.

1. Under **Settings**, select **Frontend IP configuration**, and then select **+ Add**.

1. Under **Add frontend IP address**, assign a name for the front end.

1. Verify that the **Virtual network** and the **Subnet** are the same as the SQL Server instances.

1. Set the IP address for the listener.

   > [!TIP]  
   > You can set the IP address to static and type an address that is not currently used in the subnet. Alternatively, you can set the IP address to dynamic and save the new front-end IP pool. When you do so, the Azure portal automatically assigns an available IP address to the pool. You can then reopen the front-end IP pool and change the assignment to static.

1. Save the IP address for the listener by selecting **Add**.

1. Add a health probe selecting **Health probes** under **Settings** and use the following settings:

   |Setting |Value
   |:-----|:----
   |**Name** |A name to identify the probe.
   |**Protocol** |TCP
   |**Port** |An unused TCP port, which must be available on all virtual machines. It can't be used for any other purpose. No two listeners can use the same probe port.
   |**Interval** |The amount of time between probe attempts. Use the default (5).

1. Select **Add** to save the probe.

1. Create a load-balancing rule. Under **Settings**, select **Load balancing rules**, and then select **+ Add**.

1. Configure the new load-balancing rule by using the following settings:

    |Setting |Value
    |:-----|:----
    |**Name** |A name to identify the load-balancing rule.
    |**Frontend IP address** |Select the IP address you created.
    |**Backend pool** |The pool that contains the virtual machines with the SQL Server instances.
    |**Protocol** |TCP
    |**Port** |Use the port that the SQL Server instances are using. A default instance uses port 1433, unless you changed it.
    |**Backend port** |Use the same value as **Port**.
    |**Health probe** |Choose the probe you created.
    |**Session persistence** |None
    |**Idle timeout (minutes)** |Default (4)
    |**Floating IP (direct server return)** | Enabled

### Configure the availability group to use the new IP address

To finish configuring the cluster, repeat the steps that you followed when you made the first availability group. That is, configure the [cluster to use the new IP address](#configure-the-cluster-to-use-the-load-balancer-ip-address).

After you've added an IP address for the listener, configure the additional availability group by doing the following steps:

1. Verify that the probe port for the new IP address is open on both SQL Server virtual machines.

1. [In Cluster Manager, add the client access point](#addcap).

1. [Configure the IP resource for the availability group](#congroup).

   > [!IMPORTANT]  
   > When you create the IP address, use the IP address that you added to the load balancer.

1. [Make the SQL Server availability group resource dependent on the client access point](#dependencyGroup).

1. [Make the client access point resource dependent on the IP address](#listname).

1. [Set the cluster parameters in PowerShell](#setparam).

If you're on the secondary replica VM, and you're unable to connect to the listener, it's possible the probe port was not configured correctly.

You can use the following script to validate the probe port is correctly configured for the availability group:

```powershell
Clear-Host
Get-ClusterResource `
| Where-Object {$_.ResourceType.Name -like "IP Address"} `
| Get-ClusterParameter `
| Where-Object {($_.Name -like "Network") -or ($_.Name -like "Address") -or ($_.Name -like "ProbePort") -or ($_.Name -like "SubnetMask")}
```

## Add load-balancing rule for distributed availability group

If an availability group participates in a distributed availability group, the load balancer needs an additional rule. This rule stores the port used by the distributed availability group listener.

> [!IMPORTANT]  
> This step only applies if the availability group participates in a [distributed availability group](/sql/database-engine/availability-groups/windows/configure-distributed-availability-groups).

1. On each server that participates in the distributed availability group, create an inbound rule on the distributed availability group listener TCP port. In many examples, documentation uses 5022.

1. In the Azure portal, select the load balancer and select **Load balancing rules**, and then select **+Add**.

1. Create the load balancing rule with the following settings:

   |Setting |Value
   |:-----|:----
   |**Name** |A name to identify the load balancing rule for the distributed availability group.
   |**Frontend IP address** |Use the same frontend IP address as the availability group.
   |**Backend pool** |The pool that contains the virtual machines with the SQL Server instances.
   |**Protocol** |TCP
   |**Port** |5022 - The port for the [distributed availability group endpoint listener](/sql/database-engine/availability-groups/windows/configure-distributed-availability-groups).</br> Can be any available port.  
   |**Backend port** | 5022 - Use the same value as **Port**.
   |**Health probe** |Choose the probe you created.
   |**Session persistence** |None
   |**Idle timeout (minutes)** |Default (4)
   |**Floating IP (direct server return)** | Enabled

Repeat these steps for the load balancer on the other availability groups that participate in the distributed availability groups.

If you have an Azure Network Security Group to restrict access, make sure that the allow rules include:
- The backend SQL Server VM IP addresses
- The load balancer floating IP addresses for the AG listener
- The cluster core IP address, if applicable.

## Related content

- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Always On availability groups overview](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)
