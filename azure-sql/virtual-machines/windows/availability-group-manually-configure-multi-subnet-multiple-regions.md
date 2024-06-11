---
title: "Tutorial: Configure a multi-subnet availability group across regions"
description: "This tutorial explains how to configure a multi-subnet Always On availability group for SQL Server on Azure Virtual Machines with a replica in a different region from the primary replica."
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma, randolphwest
ms.date: 06/18/2024
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: tutorial
editor: monicar
tags: azure-service-management
---

# Configure a multi-subnet availability group across Azure regions - SQL Server on Azure VMs

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This tutorial explains how to configure an Always On availability group replica for SQL Server on Azure Virtual Machines (VMs) in an Azure region that is remote to the primary replica. You can use this configuration for disaster recovery (DR).

You can also use the steps in this article to extend an existing on-premises availability group to Azure.

This tutorial builds on the [tutorial to manually deploy an availability group in multiple subnets in a single region](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md). Mentions of the local region in this article refer to the virtual machines and availability group already configured in the first region. The remote region is the new infrastructure that's being added in this tutorial.

## Overview

The following image shows a common deployment of an availability group on Azure virtual machines:

:::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-multi-subnet/multi-subnet-availability-group-diagram.png" alt-text="Diagram showing the resources that are deployed in this tutorial. ":::

In the deployment shown in the diagram, all virtual machines are in one Azure region. The availability group replicas can have synchronous commit with automatic failover on SQL-VM-1 and SQL-VM-2. To build this architecture, see the [availability group template or tutorial](availability-group-overview.md).

This architecture is vulnerable to downtime if the Azure region becomes inaccessible. To overcome this vulnerability, add a replica in a different Azure region. The following diagram shows how the new architecture looks:

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-availability-group-basic-dr.png" alt-text="Diagram of a disaster recovery scenario for an availability group.":::

The diagram shows a new virtual machine called SQL-VM-3. SQL-VM-3 is in a different Azure region. It's added to the Windows Server failover cluster and can host an availability group replica. In this architecture, the replica in the remote region is normally configured with asynchronous commit availability mode and manual failover mode.

> [!NOTE]
> An Azure availability set is required when more than one virtual machine is in the same region. If only one virtual machine is in the region, the availability set is not required.
>
> You can place a virtual machine in an availability set only at creation time. If the virtual machine is already in an availability set, you can add a virtual machine for an additional replica later.

When availability group replicas are on Azure virtual machines in different Azure regions, you can connect the virtual networks by using [virtual network peering](/azure/virtual-network/virtual-network-peering-overview) or a [site-to-site VPN gateway](/azure/vpn-gateway/vpn-gateway-about-vpngateways).

> [!IMPORTANT]
> This architecture incurs outbound data charges for data replicated between Azure regions. See [Bandwidth pricing](https://azure.microsoft.com/pricing/details/bandwidth/).  

## Create the network and subnet

Before you create a [virtual network and subnet in a new region](/azure/virtual-network/manage-virtual-network#create-a-virtual-network), decide on the address space, subnet network, cluster IP, and availability group listener IP addresses that you'll use for the remote region.

The following table lists details for the local (current) region and what will be set up in the new remote region.

| Type | Local | Remote region
| ----- | ----- | ----------
| Address space | 10.38.0.0/16 | 10.19.0.0/16
| DC Subnet network | 10.38.0.0/24 | 10.19.0.0/24
| SQL Subnet 1 network | 10.38.1.0/24 | 10.19.1.0/24
| SQL Subnet 2 network | 10.38.2.0/24 | n/a
| Cluster IP 1 | 10.38.1.10 | 10.19.1.10
| Cluster IP 2 | 10.38.2.10 | n/a
| Availability group listener IP 1 | 10.38.1.11 | 10.19.1.11
| Availability group listener IP 1 | 10.38.2.11 | n/a



To create a virtual network and subnet in the new region in the Azure portal:

1. Go to your resource group in the [Azure portal](https://portal.azure.com) and select **+ Create**.
1. Search for **virtual network** in the **Marketplace** search box, and then select the **virtual network** tile from Microsoft. 
1. On the **Create virtual network** page, select **Create**. Then enter the following information on the **Basics** tab:
    1. Under **Project details**, for **Subscription**, select the appropriate Azure subscription. For **Resource group**, select the resource group that you created previously, such as **SQL-HA-RG**.
    1. Under **Instance details**, provide a name for your virtual network, such as **remote_HAVNET**. Then choose a new remote region.

    :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/multi-region-create-vnet-basics.png" alt-text="Screenshot of the Azure portal that shows selections for creating a virtual network in a remote region.":::

1. On the **IP addresses** tab, select the ellipsis (**...**) next to **+ Add a subnet**. Select **Delete address space** to remove the existing address space, if you need a different address range.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/04-delete-address-space.png" alt-text="Screenshot of the Azure portal that shows selections for deleting the existing address space in a virtual network." lightbox="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/04-delete-address-space.png":::

1. Select **Add an IP address space** to open the pane to create the address space that you need. This tutorial uses the address space of the remote region: 10.19.0.0/16. Select **Add**.

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-add-address-space.png" alt-text="Screenshot of the Azure portal that shows selections for adding an address space for a virtual network." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-add-address-space.png":::

1. Add subnets for the domain controller and the SQL Server.
   1. Select **+ Add a subnet**
   1. Provide a value for the **Subnet name**, such as **DC-Subnet**.
   1. Provide a unique subnet address range within the virtual network address space.
      
      For example, if your address range is **10.19.0.0/16**, enter these values for the **DC-Subnet** subnet: **10.19.1.0** for **Starting address** and **/24** for **Subnet size**.
   1. Select **Add** to add your new subnet.
   1. Repeat the process for the **SQL-subnet1**. When complete, you should have a subnet for the domain controller in the remote region and a subnet for each SQL Server in the remote region. For example, in this tutorial, the remote region virtual network contains:

     :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-configure-virtual-network.png" alt-text="Screenshot of the Azure portal that shows selections for adding a subnet to a virtual network." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-configure-virtual-network.png":::

1. Select **Review + create** to create the virtual network.

### Configure virtual network DNS

After you create the virtual network, configure it to use the DNS server from the local or primary domain controller.

To configure your virtual network for DNS, follow these steps:

1. Go to your resource group in the [Azure portal](https://portal.azure.com), and select your virtual network, such as **remote-HAVNET**. 
1. Select **DNS servers** under the **Settings** pane and then select **Custom**. 
1. Enter the private IP address you identified previously in the **IP Address** field, such as `10.38.0.4`.
1. Select **Save**.

## Connect the virtual networks in the two Azure regions

After you create the new virtual network and subnet, you're ready to connect the two regions so they can communicate with each other. There are two methods to do this:

- [Connect virtual networks with virtual network peering by using the Azure portal](/azure/virtual-network/tutorial-connect-virtual-networks-portal) (recommended)

  In some cases, you might have to use PowerShell to create the connection between virtual networks. For example, if you use different Azure accounts, you can't configure the connection in the portal. In this case, review [Configure a network-to-network connection by using the Azure portal](/azure/vpn-gateway/vpn-gateway-vnet-vnet-rm-ps).

- [Configure a site-to-site VPN gateway connection by using the Azure portal](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal)

This tutorial uses virtual network peering. To configure virtual network peering: 

1. In the search box at the top of the Azure portal, type **autoHAVNET**, which is the virtual network in your local region. When **autoHAVNET** appears in the search results, select it.

1. Under **Settings**, select **Peerings**, and then select **+ Add**.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/add-peering.png" alt-text="Screenshot of the Azure portal that shows selections for adding a virtual network peering.":::

1. Enter or select the following information, accept the defaults for the remaining settings, and then select **Add**.

    | Setting | Value |
    | --- | --- |
    | **This virtual network** | |
    | **Peering link name** | Enter **autoHAVNET-remote_HAVNET** for the name of the peering from **autoHAVNET** to the remote virtual network. |
    | **Remote virtual network** | |
    | **Peering link name** | Enter **remote_HAVNET-autoHAVNET** for the name of the peering from the remote virtual network to **autoHAVNET**. |
    | **Subscription** | Select your subscription for the remote virtual network. |
    | **Virtual network**  | Select **remote_HAVNET** for the name of the remote virtual network. The remote virtual network can be in the same region of **autoHAVNET** or in a different region. |

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/peering-settings-bidirectional.png" alt-text="Screenshot of the Azure portal that shows peering settings." lightbox="./media/availability-group-manually-configure-multiple-regions/peering-settings-bidirectional.png" :::

1. On the **Peerings** page, **Peering status** is **Connected**.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/peering-status.png" alt-text="Screenshot of the Azure portal that shows a Connected status for virtual network peering." lightbox="./media/availability-group-manually-configure-multiple-regions/peering-status.png":::

   If you don't see a **Connected** status, select the **Refresh** button.

## Create a domain controller

A [domain controller in the new region](/windows-server/identity/ad-ds/introduction-to-active-directory-domain-services-ad-ds-virtualization-level-100) is necessary to provide authentication if the primary site isn't available. To create the domain controller in the new region:

1. Return to the **SQL-HA-RG** resource group.
1. Select **+ Create**.
1. Type **Windows Server 2016 Datacenter**, and then select the **Windows Server 2016 Datacenter** result.
1. In **Windows Server 2016 Datacenter**, verify that the deployment model is **Resource Manager**, and then select **Create**.

The following table shows the settings for the two machines:

| Setting | Value |
| --- | --- |
| **Name** |Remote domain controller: **DC-VM-3**|
| **VM disk type** |**SSD** |
| **User name** |**DomainAdmin** |
| **Password** |**Contoso!0000** |
| **Subscription** |Your subscription |
| **Resource group** |**SQL-HA-RG** |
| **Location** |Your location |
| **Size** |**DS1_V2** |
| **Storage** | **Use managed disks**: **Yes** |
| **Virtual network** |**remote_HAVNET** |
| **Subnet** |**DC-subnet** |
| **Public IP address** |Same name as the VM |
| **Network security group** |Same name as the VM |
| **Diagnostics** |Enabled |
| **Diagnostics storage account** |Automatically created |

Azure creates the virtual machine.

### Configure the domain controller

In the following steps, configure the **DC-VM-3** machine as a domain controller for **corp.contoso.com**:

#### Set preferred DNS server address

The preferred DNS server address [shouldn't be updated](/azure/virtual-network/virtual-networks-name-resolution-for-vms-and-role-instances#specify-dns-servers) directly within a VM, it should be edited from the [Azure portal, or PowerShell, or Azure CLI](/azure/virtual-network/virtual-network-network-interface?tabs=network-interface-portal#change-dns-servers). The steps below are to make the change inside of the Azure portal:

1. Sign-in to the [Azure portal](https://portal.azure.com).

1. In the search box at the top of the portal, enter **Network interface**. Select **Network interfaces** in the search results.

1. Select the network interface for the second domain controller that you want to view or change settings for from the list.

1. In **Settings**, select **DNS servers**.

1. Since this domain controller isn't in the same virtual network as the primary domain controller select **Custom** and input the IP address of the local domain controller, such as `10.38.0.4`. The DNS server address you specify is assigned only to this network interface and overrides any DNS setting for the virtual network the network interface is assigned to.

1. Select **Save**.
1. Return to the virtual machine in the Azure portal and restart the VM. Once the virtual machine has restarted, you can join the VM to the domain.

### Join the domain

Next, join the **corp.contoso.com** domain. To do so, follow these steps: 

1. Remotely connect to the virtual machine using the **BUILTIN\DomainAdmin** account.
1. Open **Server Manager**, and select **Local Server**.
1. Select **WORKGROUP**.
1. In the **Computer Name** section, select **Change**.
1. Select the **Domain** checkbox and type **corp.contoso.com** in the text box. Select **OK**.
1. In the **Windows Security** popup dialog, specify the credentials for the default domain administrator account (**CORP\DomainAdmin**) and the password (**Contoso!0000**).
1. When you see the "Welcome to the corp.contoso.com domain" message, select **OK**.
1. Select **Close**, and then select **Restart Now** in the popup dialog.

#### Configure domain controller 

Once your server has joined the domain, you can configure it as the second domain controller. To do so, follow these steps: 

1. If you're not already connected, open an RDP session to your secondary domain controller, and open **Server Manager Dashboard** (which may be open by default).
1. Select the **Add roles and features** link on the dashboard.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-multi-subnet/09-add-features.png" alt-text="Screenshot of the Server Manager - Add roles highlighted.":::

1. Select **Next** until you get to the **Server Roles** section.
1. Select the **Active Directory Domain Services** and **DNS Server** roles. When you're prompted, add any additional features that are required by these roles.
1. After the features finish installing, return to the **Server Manager** dashboard.
1. Select the new **AD DS** option on the left-hand pane.
1. Select the **More** link on the yellow warning bar.
1. In the **Action** column of the **All Server Task Details** dialog, select **Promote this server to a domain controller**.
1. Under **Deployment Configuration**, select **Add a domain controller to an existing domain**.
1. Select **Select**.
1. Connect by using the administrator account (**CORP.CONTOSO.COM\domainadmin**) and password (**Contoso!0000**).
1. In **Select a domain from the forest**, choose your domain and then select **OK**.
1. In **Domain Controller Options**, use the default values and set a DSRM password.

    >[!NOTE]
    >The **DNS Options** page might warn you that a delegation for this DNS server can't be created. You can ignore this warning in non-production environments.
    >

1. Select **Next** until the dialog reaches the **Prerequisites** check. Then select **Install**.

After the server finishes the configuration changes, restart the server.

### Add second DC IP address to DNS

After your remote domain controller is configured, follow the same steps as before to [identify the private IP address of the VM](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md#identify-dns-ip-address), and [add the private IP address as a secondary custom DNS server](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md#configure-virtual-network-dns) in the virtual networks (both the local and remote virtual networks) of your resource group. Adding the secondary DNS server in the Azure portal enables redundancy of the DNS service.

## Create a SQL Server VM

After the domain controller restarts, the next step is to [create a SQL Server virtual machine in the new region](./create-sql-vm-portal.md).

Before you proceed, consider the following design decisions:

**Availability - Availability Zones**   

For the highest level of redundancy, resiliency and availability deploy the VMs within separate Availability Zones. Availability Zones are unique physical locations within an Azure region. Each zone is made up of one or more datacenters with independent power, cooling, and networking. For Azure regions that don't support Availability Zones yet, use Availability Sets instead. Place all the VMs within the same Availability Set. 

**Storage - Azure Managed Disks**   

For the virtual machine storage, use Azure Managed Disks. Microsoft recommends Managed Disks for SQL Server virtual machines as they handle storage behind the scenes. For more information, see [Azure Managed Disks Overview](/azure/virtual-machines/managed-disks-overview). 

**Network - Private IP addresses in production**   

For the virtual machines, this tutorial uses public IP addresses. A public IP address enables remote connection directly to the virtual machine over the internet and makes configuration steps easier. In production environments, Microsoft recommends only private IP addresses in order to reduce the vulnerability footprint of the SQL Server instance VM resource.

**Network - Single NIC per server**   

Use a single NIC per server (cluster node). Azure networking has physical redundancy, which makes additional NICs unnecessary on a failover cluster deployed to an Azure virtual machine. The cluster validation report will warn you that the nodes are reachable only on a single network. You can ignore this warning when your failover cluster is on Azure virtual machines.

### Create and configure the SQL Server VM

To create the SQL Server VM, go back to the **SQL-HA-RG** resource group, and then select **Add**. Search for the appropriate gallery item, select **Virtual Machine**, and then select **From Gallery**. Use the information in the following table to help you create the VMs:

| Page | Setting |
| --- | --- |
| **Select the appropriate gallery item** |**SQL Server 2016 SP1 Enterprise on Windows Server 2016** |
| **Virtual machine configuration**: **Basics** |**Name** = **SQL-VM-3**<br/><br/>**User Name** = **DomainAdmin**<br/><br/>**Password** = **Contoso!0000**<br/><br/>**Subscription** = Your subscription<br/><br/>**Resource group** = **SQL-HA-RG**<br/><br/>**Location** = Your remote region |
| **Virtual machine configuration**: **Size** |**Size** = **DS2\_V2** (2 vCPUs, 7 GB)</br><br/>The size must support SSD storage (premium disk support). |
| **Virtual machine configuration**: **Settings** |**Storage**: **Use managed disks**<br/><br/>**Virtual network** = **remote-HAVNET**<br/><br/>**Subnet** = **SQL-subnet1 (10.19.1.0/24)**<br/><br/>**Public IP address** = Automatically generated<br/><br/>**Network security group** = **None**<br/><br/>**Monitoring Diagnostics** = Enabled<br/><br/>**Diagnostics storage account** = **Use an automatically generated storage account**<br/><br/> |
| **Virtual machine configuration**: **SQL Server settings** |**SQL connectivity** = **Private (within Virtual Network)**<br/><br/>**Port** = **1433**<br/><br/>**SQL Authentication** = Disabled<br/><br/>**Storage configuration** = **General**<br/><br/>**Automated patching** = **Sunday at 2:00**<br/><br/>**Automated backup** = Disabled</br><br/>**Azure Key Vault integration** = Disabled |


> [!NOTE]
> The machine size suggested here is meant for testing availability groups in Azure virtual machines. For the best performance on production workloads, see the recommendations for SQL Server machine sizes and configuration in [Checklist: Best practices for SQL Server on Azure VMs](./performance-guidelines-best-practices-checklist.md?toc=%2fazure%2fvirtual-machines%2fwindows%2ftoc.json).

After the VM is fully provisioned, you need to configure it, join it to the **corp.contoso.com** domain, and grant **CORP\Install** administrative rights to the machines.

## Configure SQL Server VMs

After VM creation completes, configure your SQL Server VMs by adding a secondary IP address to each VM, and joining them to the domain.



### Add secondary IPs to SQL Server VMs

In the multi-subnet environment, assign secondary IP addresses to each SQL Server VM to use for the availability group listener, and for Windows Server 2016 and earlier, assign secondary IP addresses to each SQL Server VM for the cluster IP address as well. Doing this negates the need for an Azure Load Balancer, as is the requirement in a single subnet environment.  

On Windows Server 2016 and earlier, you need to assign an additional secondary IP address to each SQL Server VM to use for the windows cluster IP since the cluster uses the **Cluster Network Name** rather than the default Distributed Network Name (DNN) introduced in Windows Server 2019. With a DNN, the cluster name object (CNO) is automatically registered with the IP addresses for all the nodes of the cluster, eliminating the need for a dedicated windows cluster IP address.

If you're on Windows Server 2016 and prior, follow the steps in this section to assign a secondary IP address to each SQL Server VM for *both* the availability group listener, *and* the cluster.

> [!IMPORTANT]
> If you're on Windows Server 2019 or later, only assign a secondary IP address for the availability group listener, and skip the steps to assign a windows cluster IP, unless you plan to configure your cluster with a virtual network name (VNN), in which case assign both IP addresses to each SQL Server VM as you would for Windows Server 2016.

To assign additional secondary IPs to the VMs, follow these steps:

1. Go to your resource group in the [Azure portal](https://portal.azure.com/) and select the SQL Server VM, **SQL-VM-3**.
1. Select **Networking** in the **Settings** pane, and then select the **Network Interface**.
1. On the **Network Interface** page, select **IP configurations** in the **Settings** pane and then choose **+ Add** to add an additional IP address.
1. On the **Add IP configuration** page, do the following:
   1. Specify the **Name** as the Windows Cluster IP, such as **windows-cluster-ip** for Windows 2016 and earlier. Skip this step if you're on Windows Server 2019 or later.  
   1. Set the **Allocation** to **Static**.
   1. Enter an unused **IP address** in the same subnet (**SQL-subnet-1**) as the SQL Server VM (**SQL-VM-1**), such as `10.19.1.10`.
   1. Leave the **Public IP address** at the default of **Disassociate**.
   1. Select **OK** to finish adding the IP configuration.

1. Select **+ Add** again to configure an additional IP address for the availability group listener (with a name such as **availability-group-listener**), again specifying an unused IP address in **SQL-subnet-1** such as `10.19.1.11`.

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-secondary-ips.png" alt-text="Screenshot of the Azure portal that shows the IP configurations on the network interface." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-secondary-ips.png":::

Now you're ready to join the **corp.contoso.com**.

### <a name="joinDomain"></a>Join the server to the domain

To join the VM to **corp.contoso.com**, use the following steps for the SQL Server VM:

1. Remotely connect to the virtual machine by using **BUILTIN\DomainAdmin**.
1. In **Server Manager**, select **Local Server**.
1. Select the **WORKGROUP** link.
1. In the **Computer Name** section, select **Change**.
1. Select the **Domain** check box, and enter **corp.contoso.com** in the text box. Then select **OK**.
1. In the **Windows Security** pop-up dialog, specify the credentials for the default domain administrator account (**CORP\DomainAdmin**) and the password (**Contoso!0000**).
1. When you see the "Welcome to the corp.contoso.com domain" message, select **OK**.
1. Select **Close**, and then select **Restart Now** in the pop-up dialog.

## Add accounts

The next task is to add the installation account as an administrator on the SQL Server VM, and then grant permission to that account and to local accounts within SQL Server. You can then update the SQL Server service account.

### Add the CORP\Install user as an administrator on each cluster VM

After the SQL Server virtual machine restarts as a member of the domain, add **CORP\Install** as a member of the local administrators group:

1. Wait until the VM is restarted, and then open the RDP file again from the primary domain controller. Sign in to **SQL-VM-3** by using the **CORP\DomainAdmin** account.

   > [!TIP]
   > In earlier steps, you were using the **BUILTIN** administrator account. Now that the server is in the domain, make sure that you sign in with the domain administrator account. In your RDP session, specify *DOMAIN*\\*username*.

1. In **Server Manager**, select **Tools**, and then select **Computer Management**.
1. In the **Computer Management** window, expand **Local Users and Groups**, and then select **Groups**.
1. Double-click the **Administrators** group.
1. In the **Administrator Properties** dialog, select the **Add** button.
1. Enter the user as **CORP\Install**, and then select **OK**.
1. Select **OK** to close the **Administrator Properties** dialog.

### Create a sign-in on each SQL Server VM for the installation account

Use the installation account (**CORP\Install**) to configure the availability group. This account needs to be a member of the **sysadmin** fixed server role on each SQL Server VM. The following steps create a sign-in for the installation account. Complete them on both SQL Server VMs.

1. Connect to the server through RDP by using the *\<MachineName\>\DomainAdmin* account.

1. Open SQL Server Management Studio and connect to the local instance of SQL Server.

1. In **Object Explorer**, select **Security**.

1. Right-click **Logins**. Select **New Login**.

1. In **Login - New**, select **Search**.

1. Select **Locations**.

1. Enter the domain administrator's network credentials. Use the installation account (**CORP\Install**).

1. Set the sign-in to be a member of the **sysadmin** fixed server role.

1. Select **OK**.

### Configure system account permissions

To create a system account and grant appropriate permissions, complete the following steps on each SQL Server instance:

1. Use the following script to create an account for `[NT AUTHORITY\SYSTEM]`:

   ```sql
   USE [master]
   GO
   CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master]
   GO 
   ```

1. Grant the following permissions to `[NT AUTHORITY\SYSTEM]`:

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

On each SQL Server VM, complete the following steps to set the SQL Server service account. Use the accounts that you created when you configured the domain accounts.

1. Open SQL Server Configuration Manager.
1. Right-click the SQL Server service, and then select **Properties**.
1. Set the account and password.

For SQL Server availability groups, each SQL Server VM needs to run as a domain account.

## Add failover clustering to SQL Server VM

To add failover clustering features, complete the following steps on both SQL Server VMs:

1. Connect to the SQL Server virtual machine through RDP by using the **CORP\Install** account. Open the **Server Manager** dashboard.
1. Select the **Add roles and features** link on the dashboard.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/22-add-features.png" alt-text="Screenshot of the Server Manager dashboard that shows the link for adding roles and features. ":::

1. Select **Next** until you get to the **Server Features** section.
1. In **Features**, select **Failover Clustering**.
1. Add any required features.
1. Select **Install**.

>[!NOTE]
> You can now automate this task, along with actually joining the SQL Server VMs to the failover cluster, by using the [Azure CLI](./availability-group-az-commandline-configure.md) and [Azure quickstart templates](availability-group-quickstart-template-configure.md).

### Tune network thresholds for a failover cluster

When you're running Windows failover cluster nodes in Azure VMs with SQL Server availability groups, change the cluster setting to a more relaxed monitoring state. This change will make the cluster more stable and reliable. For details, see [IaaS with SQL Server: Tuning failover cluster network thresholds](/windows-server/troubleshoot/iaas-sql-failover-cluster).

## <a name="endpoint-firewall"></a> Configure the firewall on each SQL Server VM

The availability group feature relies on traffic through the following TCP ports: 

- **SQL Server VM**: Port 1433 for a default instance of SQL Server.
- **Database mirroring endpoint:** Any available port. Examples frequently use 5022.

Open these firewall ports on both SQL Server VMs. The method of opening the ports depends on the firewall solution that you use, and may vary from the Windows Firewall example provided in this section. 

To open these ports on a Windows Firewall, follow these steps: 

1. On the first SQL Server **Start** screen, launch **Windows Firewall with Advanced Security**.
1. On the left pane, select **Inbound Rules**. On the right pane, select **New Rule**.
1. For **Rule Type**, choose **Port**.
1. For the port, specify **TCP** and type the appropriate port numbers. See the following example:

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-multi-subnet/17-firewall-tcp-ports.png" alt-text="Screenshot of configuring a new rule for the Windows firewall.":::

1. Select **Next**.
1. On the **Action** page, select **Allow the connection** , and then select **Next**.
1. On the **Profile** page, accept the default settings, and then select **Next**.
1. On the **Name** page, specify a rule name (such as **SQL Inbound**) in the **Name** text box, and then select **Finish**.

## Add SQL Server to the Windows Server failover cluster

The new SQL Server VM needs to be [added to the Windows Server failover cluster](availability-group-manually-configure-tutorial-single-subnet.md#addNode) that exists in your local region.

To add the SQL Server VM to the cluster:

1. Use RDP to connect to a SQL Server VM in the existing cluster. Use a domain account that's an administrator on both SQL Server VMs and the witness server.
1. On the **Server Manager** dashboard, select **Tools**, and then select **Failover Cluster Manager**.
1. On the left pane, right-click **Failover Cluster Manager**, and then select **Connect to Cluster**.
1. In the **Select Cluster** window, under **Cluster name**, choose **\<Cluster on this server\>**. Then select **OK**.
1. In the browser tree, right-click the cluster and select **Add Node**.
1. In the **Add Node Wizard**, select **Next**. 
1. On the **Select Servers** page, add the name of the new SQL Server instance. Enter the server name in **Enter server name**, select **Add**, and then select **Next**.
1. On the **Validation Warning** page, select **No**. (In a production scenario, you should perform the validation tests). Then, select **Next**.

1. On the **Confirmation** page, if you're using Storage Spaces, clear the **Add all eligible storage to the cluster** checkbox.

   >[!WARNING]
   >If you don't clear **Add all eligible storage to the cluster**, Windows detaches the virtual disks during the clustering process. As a result, they don't appear in Disk Manager or Explorer until the storage is removed from the cluster and reattached via PowerShell.

1. Select **Next**.

1. Select **Finish**.

**Failover Cluster Manager** shows that your cluster has a new node and lists it in the **Nodes** container.

### Add the IP address for the Windows Server failover cluster

> [!NOTE]
> On Windows Server 2019, the cluster creates a *distributed server name* instead of a *cluster network name*. If you're using Windows Server 2019, skip to [Add an IP address for the availability group listener](#add-an-ip-address-for-the-availability-group-listener). You can create a cluster network name by using [PowerShell](failover-cluster-instance-storage-spaces-direct-manually-configure.md#create-windows-failover-cluster). For more information, review the blog post [Failover Cluster: Cluster Network Object](https://blogs.windows.com/windowsexperience/2018/08/14/announcing-windows-server-2019-insider-preview-build-17733/#W0YAxO8BfwBRbkzG.97).  

Next, create the IP address resource and add it to the cluster for the new SQL Server VM:

1. In **Failover Cluster Manager**, select the name of the cluster. Right-click the cluster name under **Cluster Core Resources**, and then select **Properties**:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/cluster-name-properties.png" alt-text="Screenshot of Failover Cluster Manager that shows selections for opening cluster properties." lightbox="./media/availability-group-manually-configure-multiple-regions/cluster-name-properties.png":::

1. In the **Cluster Properties** dialog, select **Add** under **IP Addresses**, and then add the IP address of the cluster name from the remote network region. Select **OK** in the **IP Address** dialog, and then select **OK** in the **Cluster Properties** dialog to save the new IP address.

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/add-cluster-ip-address.png" alt-text="Screenshot that shows the dialogs for creating a cluster IP address." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/add-cluster-ip-address.png":::

1. Add the IP address as a dependency for the cluster core name.

   Open the **Cluster Properties** dialog once more, and select the **Dependencies** tab. Configure an **OR** dependency for the two IP addresses.

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/cluster-ip-dependencies.png" alt-text="Screenshot of the Cluster Properties dialog that shows selections for adding a dependency." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/cluster-ip-dependencies.png":::

### Add an IP address for the availability group listener

The IP address for the listener in the remote region needs to be added to the cluster. To add the IP address:

1. In **Failover Cluster Manager**, right-click the availability group role. Point to **Add Resource**, point to **More Resources**, and then select **IP Address**.

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-add-ip-resource.png" alt-text="Screenshot of Failover Cluster Manager that shows selections for adding an IP address as a resource." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-add-ip-resource.png":::

1. To configure this IP address, right-click the resource under **Other Resources**, and then select **Properties**.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/configure-listener-ip-cluster.png" alt-text="Screenshot of Failover Cluster Manager that shows selections for opening properties for a resource. ":::

1. For **Name**, enter a name for the new resource. For **Network**, select the network from the remote datacenter. Select **Static IP Address**, and then in the **Address** box, assign the static IP address that you previously selected for the listener, in this tutorial is it **10.19.1.11**.

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-assign-listener-ip-cluster.png" alt-text="Screenshot of the dialog for IP address properties, showing assignment of the listener IP in the cluster." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-assign-listener-ip-cluster.png":::

1. Select **Apply**, and then select **OK**.

1. Add the IP address resource as a dependency for the listener client access point (network name) cluster.

   Right-click the listener client access point, and then select **Properties**. Browse to the **Dependencies** tab and add the new IP address resource to the listener client access point. The following screenshot shows a properly configured IP address cluster resource:

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-configure-dependency-multiple-ip.png" alt-text="Screenshot of Failover Cluster Manager that shows configured IP addresses for an availability group." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-configure-dependency-multiple-ip.png" :::

   > [!IMPORTANT]
   > The cluster resource group includes both IP addresses. Both IP addresses are dependencies for the listener client access point. Use the **OR** operator in the cluster dependency configuration.


## Enable availability groups

Next, enable the Always On availability groups feature. Complete these steps on the new SQL Server VM:

1. From the **Start** screen, open **SQL Server Configuration Manager**.
1. In the browser tree, select **SQL Server Services**. Right-click the **SQL Server (MSSQLSERVER)** service, and then select **Properties**.
1. Select the **AlwaysOn High Availability** tab, and then select **Enable AlwaysOn Availability Groups**.

   :::image type="content" source="./media/availability-group-manually-configure-tutorial-single-subnet/54-enable-Always-On.png" alt-text="Screenshot of selections for enabling Always On availability groups in SQL Server properties.":::

1. Select **Apply**. Select **OK** in the pop-up dialog.

1. Restart the SQL Server service.

## Add a replica to the availability group

After SQL Server has restarted on the newly created virtual machine, you can add it as a [replica to the availability group](/sql/database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio):

1. Open a remote desktop session to the primary SQL Server instance in the availability group, and then open SQL Server Management Studio (SSMS).
1. In **Object Explorer** in SSMS, open **Always On High Availability** > **Availability Groups**. Right-click your availability group name, and then select **Add Replica**.
1. Connect to the existing replica, and then select **Next**.
1. Select **Add Replica** and connect to the new SQL Server VM.

   >[!IMPORTANT]
   > A replica in a remote Azure region should be set to asynchronous replication with manual failover.

1. On the **Select Initial Data Synchronization** page, select **Full** and specify a shared network location. For the location, use the [backup share that you created](availability-group-manually-configure-tutorial-single-subnet.md#backupshare). In the example, it was **\\\\<First SQL Server\>\Backup\\**. Then select **Next**.

   >[!NOTE]
   >Full synchronization takes a full backup of the database on the first instance of SQL Server and restores it to the second instance. For large databases, we don't recommend full synchronization because it might take a long time. 
   >
   > You can reduce this time by manually backing up the database and restoring it with `NO RECOVERY`. If the database is already restored with `NO RECOVERY` on the second SQL Server instance before you configure the availability group, select **Join only**. If you want to take the backup after you configure the availability group, select **Skip initial data synchronization**.
   >

1. On the **Validation** page, select **Next**. This page should look similar to the following image:

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-subnet-multi-region-ag-validation.png" alt-text="Screenshot of the page that displays results of availability group validation in SSMS.":::

    >[!NOTE]
    >A warning for the listener configuration says you haven't configured an availability group listener. You can ignore this warning because the listener is already set up. 

1. On the **Summary** page, select **Finish**, and then wait while the wizard configures the new availability group. On the **Progress** page, you can select **More details** to view the detailed progress. 

   After the wizard finishes the configuration, inspect the **Results** page to verify that the availability group is successfully created.
1. Select **Close** to close the wizard.

### Check the availability group

In **Object Explorer**, expand **Always On High Availability**, and then expand **Availability Groups**. Right-click the availability group and select **Show Dashboard**.

Your availability group dashboard should look similar to the following screenshot, now with another replica:

:::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/ag-health-dashboard.png" alt-text="Screenshot of the availability group dashboard in SSMS." lightbox="./media/availability-group-manually-configure-multi-subnet-multiple-regions/ag-health-dashboard.png" :::

The dashboard shows the replicas, the failover mode of each replica, and the synchronization state.

### Check the availability group listener

1. In **Object Explorer**, expand **Always On High Availability**, expand **Availability Groups**, and then expand **Availability Group Listener**.
1. Right-click the listener name and select **Properties**. All IP addresses should now appear for the listener (one in each region).

   :::image type="content" source="./media/availability-group-manually-configure-multi-subnet-multiple-regions/multi-region-listener.png" alt-text="Screenshot of the Availability Group Listener Properties window in SSMS, showing both IP addresses being used for the listener.":::

## Set the connection for multiple subnets

The replica in the remote datacenter is part of the availability group, but it's in a different subnet. If this replica becomes the primary replica, application connection time-outs might occur. This behavior is the same as an on-premises availability group in a multiple-subnet deployment. To allow connections from client applications, either update the client connection or configure name resolution caching on the cluster network name resource.

Preferably, update the cluster configuration to set `RegisterAllProvidersIP=1` and the client connection strings to set `MultiSubnetFailover=Yes`. See [Connecting with MultiSubnetFailover](/sql/relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery#Anchor_0).

If you can't modify the connection strings, you can configure name resolution caching. See [Timeout occurs when you connect to an Always On listener in a multi-subnet environment](https://support.microsoft.com/help/2792139/time-out-error-and-you-cannot-connect-to-a-sql-server-2012-alwayson-av).

## Fail over to the remote region

To test listener connectivity to the remote region, you can fail the replica over to the remote region. While the replica is asynchronous, failover is vulnerable to potential data loss. To fail over without data loss, change the availability mode to synchronous and set the failover mode to automatic. Use the following steps:

1. In **Object Explorer**, connect to the instance of SQL Server that hosts the primary replica.
1. Under **Always On Availability Groups**, right-click your availability group and select **Properties**.
1. On the **General** page, under **Availability Replicas**, set the secondary replica on the disaster recovery (DR) site to use **Synchronous Commit** availability mode and **Automatic** failover mode.

   If you have a secondary replica in same site as your primary replica for high availability, set this replica to **Asynchronous Commit** and **Manual**.
1. Select **OK**.
1. In **Object Explorer**, right-click the availability group and select **Show Dashboard**.
1. On the dashboard, verify that the replica on the DR site is synchronized.
1. In **Object Explorer**, right-click the availability group and select **Failover**. SQL Server Management Studio opens a wizard to fail over SQL Server.  
1. Select **Next**, and select the SQL Server instance on the DR site. Select **Next** again.
1. Connect to the SQL Server instance on the DR site, and then select **Next**.
1. On the **Summary** page, verify the settings and select **Finish**.

After you test connectivity, move the primary replica back to your primary datacenter and set the availability mode back to its normal operating settings. The following table shows the normal operating settings for the architecture described in this article:

| Location | Server instance | Role | Availability mode | Failover mode
| ----- | ----- | ----- | ----- | -----
| Primary datacenter | SQL-VM-1 | Primary | Synchronous | Automatic
| Primary datacenter | SQL-VM-2 | Secondary | Synchronous | Automatic
| Secondary or remote datacenter | SQL-VM-3 | Secondary | Asynchronous | Manual

For more information about planned and forced manual failover, see the following articles:

- [Perform a planned manual failover of an availability group (SQL Server)](/sql/database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server)
- [Perform a forced manual failover of an availability group (SQL Server)](/sql/database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server)

## Next steps

To learn more, see:

- [Windows Server failover cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Overview of Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)
