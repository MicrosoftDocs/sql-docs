---
title: Configure a SQL Server Always On availability group across different regions
description: This article explains how to configure a SQL Server Always On availability group on Azure virtual machines with a replica in a different region.
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma, randolphwest
ms.date: 09/01/2022
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
ms.custom: seo-lt-2019
editor: monicar
tags: azure-service-management
---

# Configure a SQL Server Always On availability group across different Azure regions

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This tutorial explains how to configure a SQL Server Always On availability group replica on Azure virtual machines in a remote Azure location. Use this configuration to support disaster recovery.

> [!NOTE]  
> You can use the same steps in this article to extend your on-premises availability group to Azure.

This article applies to Azure Virtual Machines in Resource Manager mode.

The following image shows a common deployment of an availability group on Azure virtual machines:

:::image type="content" source="./media/availability-group-manually-configure-multiple-regions/00-availability-group-basic.png" alt-text="Diagram that shows the Azure load balancer and the Availability set with a Windows Server Failover Cluster and Always On Availability Group":::

In this deployment, all virtual machines are in one Azure region. The availability group replicas can have synchronous commit with automatic failover on SQL-1 and SQL-2. To build this architecture, see [Availability Group template or tutorial](availability-group-overview.md).

This architecture is vulnerable to downtime if the Azure region becomes inaccessible. To overcome this vulnerability, add a replica in a different Azure region. The following diagram shows how the new architecture would look:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/00-availability-group-basic-dr.png" alt-text="Availability Group DR":::

The preceding diagram shows a new virtual machine called SQL-3. SQL-3 is in a different Azure region. SQL-3 is added to the Windows Server Failover Cluster. SQL-3 can host an availability group replica. Finally, notice that the Azure region for SQL-3 has a new Azure load balancer.

>[!NOTE]
> An Azure availability set is required when more than one virtual machine is in the same region. If only one virtual machine is in the region, then the availability set is not required. You can only place a virtual machine in an availability set at creation time. If the virtual machine is already in an availability set, you can add a virtual machine for an additional replica later.

In this architecture, the replica in the remote region is normally configured with asynchronous commit availability mode and manual failover mode.

When availability group replicas are on Azure virtual machines in different Azure regions, then you can connect the Virtual Networks using the recommended [Virtual Network Peering](/azure/virtual-network/virtual-network-peering-overview) or [Site to Site VPN Gateway](/azure/vpn-gateway/vpn-gateway-about-vpngateways)

>[!IMPORTANT]
>This architecture incurs outbound data charges for data replicated between Azure regions. See [Bandwidth Pricing](https://azure.microsoft.com/pricing/details/bandwidth/).  

This article builds on the [tutorial to manually deploy an availability group in a single subnet in a single region](/azure-sql//virtual-machines//windows/availability-group-manually-configure-prerequisites-tutorial-single-subnet.md). When mentioning to local region in this article, it refers to the virtual machines and availability group already configured in a single region, the remote region is the new infrastructure being added.

To create a replica in a remote data center, perform the following steps:

## Create the network and subnet

Before creating a virtual network and subnet in a new region, decide the address space, subnet starting address, Cluster IP, and AG Listener IP addresses to be used for the remote region. The table below lists the details for the local (current) region and what will be set up in the new remote region for easy reference.

| Type | Local | Remote Region
| ----- | ----- | ----------
| Address Space | 192.168.0.0/16 | 10.36.0.0/16
| Subnet Network | 192.168.15.0/24 | 10.36.1.0/24
| Cluster IP | 192.168.15.200 | 10.36.1.200
| AG Listener IP | 192.168.15.201 | 10.36.1.201

To create a [virtual network and subnet in the new region](/azure/virtual-network/manage-virtual-network#create-a-virtual-network) in the Azure portal, follow these steps:

1. Go to your resource group in the [Azure portal](https://portal.azure.com) and select **+ Create**.
1. Search for **virtual network** in the **Marketplace** search box and choose the **virtual network** tile from Microsoft. Select **Create** on the **Virtual network** page.  
1. On the **Create virtual network** page, enter the following information on the **Basics** tab:
    1. Under **Project details**, choose the appropriate Azure **Subscription**, and the **Resource group** you created previously, such as **SQL-HA-RG**.
    1. Under **Instance details**, provide a name for your virtual network, such as **remote_HAVNET**, and choose a new remote region.

     :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/multi-region-create-vnet-basics.png" alt-text="Screenshot of creating virtual network in remote region.":::

1. On the **IP addresses** tab, click the "..." next to **+ Add a subnet** and select **Delete address space** to remove the existing address space, if you need a different address range.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/04-delete-address-space.png" alt-text="Delete the existing address space":::

1. Select **Add an IP address space** to open the blade to create the address space you need. For this tutorial, the address space of the remote region is **10.36.0.0/16** is being used.  Select **Add**.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/multi-region-add-address-space.png" alt-text="Add the address space":::

1. Select **+ Add a subnet**
   1. Provide a value for the **Subnet name**, such as **Admin**
   1. Provide a unique subnet address range within the virtual network address space.
      - For example, if your address range is *10.36.0.0/16*, enter the IP address range `10.36.1.0/24` for the **Admin** subnet.
   1. Select **Add** to add your new subnet.

     :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/multi-region-configure-virtual-network.png" alt-text="Add the address space":::

## Connect the Virtual Networks in the two Azure Regions

After you have create the new virtual network and subnet, you're ready to connect the two regions so they can communicate with each other. There are two methods to do this:

- [Virtual Network Peering - Connect virtual networks with virtual network peering using the Azure portal](/azure/virtual-network/tutorial-connect-virtual-networks-portal) (Recommended)

- [Site to Site VPN Gateway - Configure a VNet-to-VNet connection using the Azure portal](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal).

   >[!NOTE]
   >In some cases, you may have to use PowerShell to create the VNet-to-VNet connection. For example, if you use different Azure accounts you cannot configure the connection in the portal. In this case see, [Configure a VNet-to-VNet connection using the Azure portal](/azure/vpn-gateway/vpn-gateway-vnet-vnet-rm-ps).

For this tutorial, Virtual Network Peering will be used.

1. In the search box at the top of the Azure portal, look for *autoHAVNET*. When **autoHAVNET** appears in the search results, select it.

1. Under **Settings**, select **Peerings**, and then select **+ Add**, as shown in the following picture:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/add-peering.png" alt-text="Screenshot to add the virtual network peering.":::

1. Enter or select the following information, accept the defaults for the remaining settings, and then select **Add**.

    | Setting | Value |
    | --- | --- |
    | **This virtual network** | |
    | Peering link name | Enter *autoHAVNET-remote_HAVNET* for the name of the peering from **autoHAVNET** to the remote virtual network. |
    | **Remote virtual network** | |
    | Peering link name | Enter *remote_HAVNET-autoHAVNET* for the name of the peering from the remote virtual network to **autoHAVNET**. |
    | Subscription | Select your subscription of the remote virtual network. |
    | Virtual network  | Select **remote_HAVNET** for the name of the remote virtual network. The remote virtual network can be in the same region of **autoHAVNET** or in a different region. |

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/peering-settings-bidirectional.png" alt-text="Screenshot of the peering settings.":::

   In the **Peerings** page, the **Peering status** is **Connected**, as shown in the following picture:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/peering-status.png" alt-text="Screenshot of the connected status of the peering.":::

   If you don't see a **Connected** status, select the **Refresh** button.

## Create Domain Controller

A domain controller in the new region is necessary to provide authentication is the primary site is not available. To create the [domain controller in the new region](/windows-server/identity/ad-ds/introduction-to-active-directory-domain-services-ad-ds-virtualization-level-100), follow these steps:

1. Return to the **SQL-HA-RG** resource group.
1. Select **+ Create**.
1. Type **Windows Server 2016 Datacenter**.
1. Select **Windows Server 2016 Datacenter**. In **Windows Server 2016 Datacenter**, verify that the deployment model is **Resource Manager**, and then select **Create**.

The following table shows the settings for these two machines:

| **Field** | Value |
| --- | --- |
| **Name** |Remote domain controller: *ad-remote-dc*.|
| **VM disk type** |SSD |
| **User name** |DomainAdmin |
| **Password** |Contoso!0000 |
| **Subscription** |*Your subscription* |
| **Resource group** |SQL-HA-RG |
| **Location** |*Your location* |
| **Size** |DS1_V2 |
| **Storage** | **Use managed disks** - **Yes** |
| **Virtual network** |remote_HAVNET |
| **Subnet** |admin |
| **Public IP address** |*Same name as the VM* |
| **Network security group** |*Same name as the VM* |
| **Diagnostics** |Enabled |
| **Diagnostics storage account** |*Automatically created* |

Azure creates the virtual machines.

After the virtual machines are created, configure the domain controller.

### Configure the domain controller

In the following steps, configure the **ad-remote-dc** machine as a domain controller for corp.contoso.com. In the portal, open the **SQL-HA-RG** resource group and select the **ad-remote-dc** machine. On **ad-remote-dc**, select **Connect** to open an RDP file for remote desktop access.

1. Sign in to the VM by using your configured administrator account (**BUILTIN\DomainAdmin**) and password (**Contoso!0000**).

1. Change the preferred DNS server address to the [address of the primary domain controller](availability-group-manually-configure-prerequisites-tutorial-single-subnet.md?#note-the-ip-address-of-the-primary-domain-controller).

1. In **Network and Sharing Center**, select the network interface.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/26-networkinterface.png" alt-text="Network interface":::

1. Select **Properties**.
1. Select **Internet Protocol Version 4 (TCP/IPv4)** and then select **Properties**.
1. Select **Use the following DNS server addresses** and then specify the address of the primary domain controller in **Preferred DNS server**.
1. Select **OK**, and then **Close** to commit the changes. You are now able to join the VM to **corp.contoso.com**.

   >[!IMPORTANT]
   >If you lose the connection to your remote desktop after changing the DNS setting, go to the Azure portal and restart the virtual machine.

1. From the remote desktop to the secondary domain controller, open **Server Manager Dashboard**.
1. Select the **Add roles and features** link on the dashboard.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/22-add-features.png" alt-text="Select the Add roles and features link on the dashboard.":::

1. Select **Next** until you get to the **Server Roles** section.
1. Select the **Active Directory Domain Services** and **DNS Server** roles. When you're prompted, add any additional features that are required by these roles.
1. After the features finish installing, return to the **Server Manager** dashboard.
1. Select the new **AD DS** option on the left-hand pane.
1. Select the **More** link on the yellow warning bar.
1. In the **Action** column of the **All Server Task Details** dialog, select **Promote this server to a domain controller**.
1. Under **Deployment Configuration**, select **Add a domain controller to an existing domain**.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/28-deploymentconfig.png" alt-text="Deployment configuration":::

1. Click **Select**.
1. Connect by using the administrator account (**CORP.CONTOSO.COM\domainadmin**) and password (**Contoso!0000**).
1. In **Select a domain from the forest**, choose your domain and then select **OK**.
1. In **Domain Controller Options**, use the default values and set a DSRM password.

    >[!NOTE]
    >The **DNS Options** page might warn you that a delegation for this DNS server can't be created. You can ignore this warning in non-production environments.
    >

1. Select **Next** until the dialog reaches the **Prerequisites** check. Then select **Install**.

After the server finishes the configuration changes, restart the server.

## Create SQL Server VM

After the domain controller restarts, the next step is to [Create a SQL Server virtual machine in the new region](create-sql-vm-portal.md).

Before you proceed consider the following design decisions.

* **Storage - Azure Managed Disks**

   For the virtual machine storage, use Azure Managed Disks. Microsoft recommends Managed Disks for SQL Server virtual machines. Managed Disks handles storage behind the scenes. In addition, when virtual machines with Managed Disks are in the same availability set, Azure distributes the storage resources to provide appropriate redundancy. For more information, see [Azure Managed Disks Overview](/azure/virtual-machines/managed-disks-overview). For specifics about managed disks in an availability set, see [Use Managed Disks for VMs in an availability set](/azure/virtual-machines/availability).

* **Network - Private IP addresses in production**

   For the virtual machines, this tutorial uses public IP addresses. A public IP address enables remote connection directly to the virtual machine over the internet and makes configuration steps easier. In production environments, Microsoft recommends only private IP addresses in order to reduce the vulnerability footprint of the SQL Server instance VM resource.

* **Network - Recommend a single NIC per server**

Use a single NIC per server (cluster node) and a single subnet. Azure networking has physical redundancy, which makes additional NICs and subnets unnecessary on an Azure virtual machine guest cluster. The cluster validation report will warn you that the nodes are reachable only on a single network. You can ignore this warning on Azure virtual machine guest failover clusters.

### Create and configure the SQL Server VM

To create the SQL Server VM, go back to the **SQL-HA-RG** resource group, and then select **Add**. Search for the appropriate gallery item, select **Virtual Machine**, and then select **From Gallery**. Use the information in the following table to help you create the VMs:

| Page | VM1 |
| --- | --- |
| Select the appropriate gallery item |**SQL Server 2016 SP1 Enterprise on Windows Server 2016** |
| Virtual machine configuration **Basics** |**Name** = sqlserver-2<br/>**User Name** = DomainAdmin<br/>**Password** = Contoso!0000<br/>**Subscription** = Your subscription<br/>**Resource group** = SQL-HA-RG<br/>**Location** = Your Remote Region |
| Virtual machine configuration **Size** |**SIZE** = DS2\_V2 (2 vCPUs, 7 GB)</br>The size must support SSD storage (Premium disk support.) |
| Virtual machine configuration **Settings** |**Storage**: Use managed disks.<br/>**Virtual network** = remote-autoHAVNET<br/>**Subnet** = admin(10.36.1.0/24)<br/>**Public IP address** automatically generated.<br/>**Network security group** = None<br/>**Monitoring Diagnostics** = Enabled<br/>**Diagnostics storage account** = Use an automatically generated storage account<br/> |
| Virtual machine configuration **SQL Server settings** |**SQL connectivity** = Private (within Virtual Network)<br/>**Port** = 1433<br/>**SQL Authentication** = Disable<br/>**Storage configuration** = General<br/>**Automated patching** = Sunday at 2:00<br/>**Automated backup** = Disabled</br>**Azure Key Vault integration** = Disabled |

<br/>

> [!NOTE]
> The machine size suggested here is meant for testing availability groups in Azure Virtual Machines. For the best performance on production workloads, see the recommendations for SQL Server machine sizes and configuration in [Performance best practices for SQL Server in Azure Virtual Machines](./performance-guidelines-best-practices-checklist.md?toc=%2fazure%2fvirtual-machines%2fwindows%2ftoc.json).
>

After the VM is fully provisioned, you need to join it to the **corp.contoso.com** domain and grant CORP\Install administrative rights to the machines.

### <a name="joinDomain"></a>Join the servers to the domain

You're now able to join the VM to **corp.contoso.com**. Do the following steps for the SQL Server VM:

1. Remotely connect to the virtual machine with **BUILTIN\DomainAdmin**.
1. In **Server Manager**, select **Local Server**.
1. Select the **WORKGROUP** link.
1. In the **Computer Name** section, select **Change**.
1. Select the **Domain** check box and type **corp.contoso.com** in the text box. Select **OK**.
1. In the **Windows Security** popup dialog, specify the credentials for the default domain administrator account (**CORP\DomainAdmin**) and the password (**Contoso!0000**).
1. When you see the "Welcome to the corp.contoso.com domain" message, select **OK**.
1. Select **Close**, and then select **Restart Now** in the popup dialog.

## Add accounts

Add the installation account as an administrator on the SQL Server VM, grant permission to the installation account and local accounts within SQL Server, and update the SQL Server service account.

### Add the Corp\Install user as an administrator on each cluster VM

After the SQL Server virtual machine restarts as a member of the domain, add **CORP\Install** as a member of the local administrators group.

1. Wait until the VM is restarted, then launch the RDP file again from the primary domain controller to sign in to **sqlserver-2** by using the **CORP\DomainAdmin** account.

   >[!TIP]
   >Make sure that you sign in with the domain administrator account. In the previous steps, you were using the BUILT IN administrator account. Now that the server is in the domain, use the domain account. In your RDP session, specify *DOMAIN*\\*username*.
   >

1. In **Server Manager**, select **Tools**, and then select **Computer Management**.
1. In the **Computer Management** window, expand **Local Users and Groups**, and then select **Groups**.
1. Double-click the **Administrators** group.
1. In the **Administrators Properties** dialog, select the **Add** button.
1. Enter the user **CORP\Install**, and then select **OK**.
1. Select **OK** to close the **Administrator Properties** dialog.

### Create a sign-in on each SQL Server VM for the installation account

Use the installation account (CORP\install) to configure the availability group. This account needs to be a member of the **sysadmin** fixed server role on each SQL Server VM. The following steps create a sign-in for the installation account:

1. Connect to the server through the Remote Desktop Protocol (RDP) by using the *\<MachineName\>\DomainAdmin* account.

1. Open SQL Server Management Studio and connect to the local instance of SQL Server.

1. In **Object Explorer**, select **Security**.

1. Right-click **Logins**. Select **New Login**.

1. In **Login - New**, select **Search**.

1. Select **Locations**.

1. Enter the domain administrator network credentials.

1. Use the installation account (CORP\install).

1. Set the sign-in to be a member of the **sysadmin** fixed server role.

1. Select **OK**.

Repeat the preceding steps on the other SQL Server VM.

### Configure system account permissions

To create an account for the system account and grant appropriate permissions, complete the following steps on each SQL Server instance:

1. Create an account for `[NT AUTHORITY\SYSTEM]` on each SQL Server instance. The following script creates this account:

   ```sql
   USE [master]
   GO
   CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master]
   GO 
   ```

1. Grant the following permissions to `[NT AUTHORITY\SYSTEM]` on each SQL Server instance:

   - `ALTER ANY AVAILABILITY GROUP`
   - `CONNECT SQL`
   - `VIEW SERVER STATE`

   The following script grants these permissions:

   ```sql
   GRANT ALTER ANY AVAILABILITY GROUP TO [NT AUTHORITY\SYSTEM]
   GO
   GRANT CONNECT SQL TO [NT AUTHORITY\SYSTEM]
   GO
   GRANT VIEW SERVER STATE TO [NT AUTHORITY\SYSTEM]
   GO 
   ```

### <a name="setServiceAccount"></a>Set the SQL Server service accounts

On each SQL Server VM, set the SQL Server service account. Use the accounts that you created when you configured the domain accounts.

1. Open **SQL Server Configuration Manager**.
1. Right-click the SQL Server service, and then select **Properties**.
1. Set the account and password.
1. Repeat these steps on the other SQL Server VM.  

For SQL Server availability groups, each SQL Server VM needs to run as a domain account.

## Create an Azure load balancer

1. [Create an Azure load balancer in the network on the new region](availability-group-manually-configure-tutorial-single-subnet.md#configure-internal-load-balancer).

   This load balancer must:

   - Be in the same network and subnet as the new virtual machine.
   - Have a static IP address for the availability group listener.
   - Include a backend pool consisting of only the virtual machines in the same region as the load balancer.
   - Use a TCP port probe specific to the IP address.
   - Have a load balancing rule specific to the SQL Server in the same region.  
   - Be a Standard Load Balancer if the virtual machines in the backend pool are not part of either a single availability set or virtual machine scale set. For additional information review [Azure Load Balancer Standard overview](/azure/load-balancer/load-balancer-overview).
   - Be a Standard Load Balancer if the two virtual networks in two different regions are peered over global VNet peering. For more information, see [Azure Virtual Network frequently asked questions (FAQ)](/azure/virtual-network/virtual-networks-faq#what-are-the-constraints-related-to-global-vnet-peering-and-load-balancers).

## Add Failover Clustering

1. [Add Failover Clustering feature to the new SQL Server](availability-group-manually-configure-prerequisites-tutorial-single-subnet.md#add-failover-clustering-features-to-both-sql-server-vms).

1. [Join the new SQL Server to the domain](availability-group-manually-configure-prerequisites-tutorial-single-subnet.md#joinDomain).

1. [Set the new SQL Server service account to use a domain account](availability-group-manually-configure-prerequisites-tutorial-single-subnet.md#setServiceAccount).

1. [Add the new SQL Server to the Windows Server Failover Cluster](availability-group-manually-configure-tutorial-single-subnet.md#addNode).

1. Add an IP address resource to the cluster.

   > [!NOTE]
   > On Windows Server 2019, the cluster creates a **Distributed Server Name** instead of the **Cluster Network Name**. If you're using Windows Server 2019, skip to Step 12. You can create a cluster network name using [PowerShell](failover-cluster-instance-storage-spaces-direct-manually-configure.md#create-windows-failover-cluster). Review the blog [Failover Cluster: Cluster Network Object](https://blogs.windows.com/windowsexperience/2018/08/14/announcing-windows-server-2019-insider-preview-build-17733/#W0YAxO8BfwBRbkzG.97) for more information.  

   You can create the IP address resource in Failover Cluster Manager. Select the name of the cluster, then right-click the cluster name under **Cluster Core Resources** and select **Properties**: 

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/cluster-name-properties.png" alt-text="Screenshot that shows the Failover Cluster Manager with a cluster name Server Name and Properties selected.":::

   On the **Properties** dialog box, select **Add** under **IP Address**, and then add the IP address of the cluster name from the remote network region. Select **OK** on the **IP Address** dialog box, and then select **OK** again on the **Cluster Properties** dialog box to save the new IP address. 

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/add-cluster-ip-address.png" alt-text="Add cluster IP":::


1. Add the IP address as a dependency for the core cluster name.

   Open the cluster properties once more and select the **Dependencies** tab. Configure an OR dependency for the two IP addresses: 

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/cluster-ip-dependencies.png" alt-text="Cluster properties":::

1. Add an IP address resource to the availability group role in the cluster. 

   Right-click the availability group role in Failover Cluster Manager, choose **Add Resource**, **More Resources**, and select **IP Address**.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/20-add-ip-resource.png" alt-text="Create IP Address":::

   Configure this IP address as follows:

   - Use the network from the remote data center.
   - Assign the IP address from the new Azure load balancer. 

1. Add the IP address resource as a dependency for the listener client access point (network name) cluster.

   The following screenshot shows a properly configured IP address cluster resource:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/50-configure-dependency-multiple-ip.png" alt-text="Availability Group":::

   >[!IMPORTANT]
   >The cluster resource group includes both IP addresses. Both IP addresses are dependencies for the listener client access point. Use the **OR** operator in the cluster dependency configuration.

1. [Set the cluster parameters in PowerShell](availability-group-manually-configure-tutorial-single-subnet.md#setparam).

   Run the PowerShell script with the cluster network name, IP address, and probe port that you configured on the load balancer in the new region.

   ```powershell
   $ClusterNetworkName = "<MyClusterNetworkName>" # The cluster name for the network in the new region (Use Get-ClusterNetwork on Windows Server 2012 of higher to find the name).
   $IPResourceName = "<IPResourceName>" # The cluster name for the new IP Address resource.
   $ILBIP = "<n.n.n.n>" # The IP Address of the Internal Load Balancer (ILB) in the new region. This is the static IP address for the load balancer you configured in the Azure portal.
   [int]$ProbePort = <nnnnn> # The probe port you set on the ILB.

   Import-Module FailoverClusters

   Get-ClusterResource $IPResourceName | Set-ClusterParameter -Multiple @{"Address"="$ILBIP";"ProbePort"=$ProbePort;"SubnetMask"="255.255.255.255";"Network"="$ClusterNetworkName";"EnableDhcp"=0}
   ```

1. On the new SQL Server in SQL Server Configuration Manager, [enable Always On Availability Groups](/sql/database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server).

1. [Open firewall ports on the new SQL Server](availability-group-manually-configure-prerequisites-tutorial-single-subnet.md#endpoint-firewall). The port numbers you need to open depend on your environment. Open ports for the mirroring endpoint and Azure load balancer health probe.
1. On the new SQL Server in SQL Server Management Studio, [configure system account permissions](availability-group-manually-configure-prerequisites-tutorial-single-subnet.md#configure-system-account-permissions).

1. [Add a replica to the availability group on the new SQL Server](/sql/database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio). For a replica in a remote Azure region, set it for asynchronous replication with manual failover.  

## Set connection for multiple subnets

The replica in the remote data center is part of the availability group but it is in a different subnet. If this replica becomes the primary replica, application connection time-outs may occur. This behavior is the same as an on-premises availability group in a multi-subnet deployment. To allow connections from client applications, either update the client connection or configure name resolution caching on the cluster network name resource.

Preferably, update the cluster configuration to set `RegisterAllProvidersIP=1` and the client connection strings to set `MultiSubnetFailover=Yes`. See [Connecting With MultiSubnetFailover](/sql/relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery#Anchor_0).

If you cannot modify the connection strings, you can configure name resolution caching. See [Time-out error and you cannot connect to a SQL Server 2012 Always On availability group listener in a multi-subnet environment](https://support.microsoft.com/help/2792139/time-out-error-and-you-cannot-connect-to-a-sql-server-2012-alwayson-av).

## Fail over to remote region

To test listener connectivity to the remote region, you can fail over the replica to the remote region. While the replica is asynchronous, failover is vulnerable to potential data loss. To fail over without data loss, change the availability mode to synchronous and set the failover mode to automatic. Use the following steps:

1. In **Object Explorer**, connect to the instance of SQL Server that hosts the primary replica.
1. Under **Always On Availability Groups**, **Availability Groups**, right-click your availability group and select **Properties**.
1. On the **General** page, under **Availability Replicas**, set the secondary replica in the DR site to use **Synchronous Commit** availability mode and **Automatic** failover mode.
1. If you have a secondary replica in same site as your primary replica for high availability, set this replica to **Asynchronous Commit** and **Manual**.
1. Select OK.
1. In **Object Explorer**, right-click the availability group, and select **Show Dashboard**.
1. On the dashboard, verify that the replica on the DR site is synchronized.
1. In **Object Explorer**, right-click the availability group, and select **Failover...**. SQL Server Management Studios opens a wizard to fail over SQL Server.  
1. Select **Next**, and select the SQL Server instance in the DR site. Select **Next** again.
1. Connect to the SQL Server instance in the DR site and select **Next**.
1. On the **Summary** page, verify the settings and select **Finish**.

After testing connectivity, move the primary replica back to your primary data center and set the availability mode back to their normal operating settings. The following table shows the normal operational settings for the architecture described in this document:

| Location | Server Instance | Role | Availability Mode | Failover Mode
| ----- | ----- | ----- | ----- | -----
| Primary data center | SQL-1 | Primary | Synchronous | Automatic
| Primary data center | SQL-2 | Secondary | Synchronous | Automatic
| Secondary or remote data center | SQL-3 | Secondary | Asynchronous | Manual


### More information about planned and forced manual failover

For more information, see the following topics:

- [Perform a Planned Manual Failover of an Availability Group (SQL Server)](/sql/database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server)
- [Perform a Forced Manual Failover of an Availability Group (SQL Server)](/sql/database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server)

## Next steps

To learn more, see:

- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Always On availability groups overview](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)
