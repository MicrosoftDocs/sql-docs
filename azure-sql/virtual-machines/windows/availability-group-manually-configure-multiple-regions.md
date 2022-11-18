---
title: "Tutorial: Configure a SQL Server Always On availability group across different regions"
description: "This tutorial explains how to configure a SQL Server Always On availability group on Azure virtual machines with a replica in a different region."
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma, randolphwest
ms.date: 11/02/2022
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: tutorial
ms.custom: seo-lt-2019
editor: monicar
tags: azure-service-management
---

# Configure a SQL Server Always On availability group across different Azure regions

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This tutorial explains how to configure a SQL Server Always On availability group replica on Azure virtual machines in a remote Azure location. Use this configuration to support disaster recovery.

This tutorial builds on the [tutorial to manually deploy an availability group in a single subnet in a single region](availability-group-manually-configure-prerequisites-tutorial-single-subnet.md). Mentions of the local region in this article refers to the virtual machines and availability group already configured in a single region, whereas the remote region is the new infrastructure being added in this tutorial.

> [!NOTE]  
> You can use the same steps in this article to extend your on-premises availability group to Azure.


## Overview

The following image shows a common deployment of an availability group on Azure virtual machines:

:::image type="content" source="./media/availability-group-manually-configure-multiple-regions/00-availability-group-basic.png" alt-text="Diagram that shows the Azure load balancer and the Availability set with a Windows Server Failover Cluster and Always On Availability Group":::

In this deployment, all virtual machines are in one Azure region. The availability group replicas can have synchronous commit with automatic failover on SQL-1 and SQL-2. To build this architecture, see [Availability Group template or tutorial](availability-group-overview.md).

This architecture is vulnerable to downtime if the Azure region becomes inaccessible. To overcome this vulnerability, add a replica in a different Azure region. The following diagram shows how the new architecture would look:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/00-availability-group-basic-dr.png" alt-text="Diagram of an Availability Group disaster recovery scenario.":::

The previous diagram shows a new virtual machine called SQL-3. SQL-3 is in a different Azure region. SQL-3 is added to the Windows Server Failover Cluster. SQL-3 can host an availability group replica. Finally, notice that the Azure region for SQL-3 has a new Azure load balancer. In this architecture, the replica in the remote region is normally configured with asynchronous commit availability mode and manual failover mode.

> [!NOTE]
> An Azure availability set is required when more than one virtual machine is in the same region. If only one virtual machine is in the region, then the availability set is not required. You can only place a virtual machine in an availability set at creation time. If the virtual machine is already in an availability set, you can add a virtual machine for an additional replica later.

When availability group replicas are on Azure virtual machines in different Azure regions, then you can connect the Virtual Networks using the recommended [Virtual Network Peering](/azure/virtual-network/virtual-network-peering-overview) or [Site to Site VPN Gateway](/azure/vpn-gateway/vpn-gateway-about-vpngateways)

> [!IMPORTANT]
> This architecture incurs outbound data charges for data replicated between Azure regions. See [Bandwidth Pricing](https://azure.microsoft.com/pricing/details/bandwidth/).  



## Create the network and subnet

Before creating a virtual network and subnet in a new region, decide the address space, subnet starting address, Cluster IP, and AG listener IP addresses you'll use for the remote region. 

The following table lists details for the local (current) region and what will be set up in the new remote region for easy reference.

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

     :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/multi-region-create-vnet-basics.png" alt-text="Screenshot the Azure portal, Create virtual network page, showing creating virtual network in remote region.":::

1. On the **IP addresses** tab, select the "..." next to **+ Add a subnet** and select **Delete address space** to remove the existing address space, if you need a different address range.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/04-delete-address-space.png" alt-text="Screenshot the Azure portal, Create virtual network page, showing how to delete the existing address space in a virtual network." lightbox="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/04-delete-address-space.png":::

1. Select **Add an IP address space** to open the blade to create the address space you need. For this tutorial, the address space of the remote region is **10.36.0.0/16** is being used.  Select **Add**.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/multi-region-add-address-space.png" alt-text="Screenshot the Azure portal, Add an IP address space page, showing how to add the address space for a virtual network." lightbox="./media/availability-group-manually-configure-multiple-regions/multi-region-add-address-space.png":::

1. Select **+ Add a subnet**
   1. Provide a value for the **Subnet name**, such as **Admin**
   1. Provide a unique subnet address range within the virtual network address space.
      - For example, if your address range is *10.36.0.0/16*, enter the IP address range `10.36.1.0/24` for the **Admin** subnet.
   1. Select **Add** to add your new subnet.

     :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/multi-region-configure-virtual-network.png" alt-text="Screenshot the Azure portal, Add a subnet page, showing how to add the subnet to a virtual network." lightbox="./media/availability-group-manually-configure-multiple-regions/multi-region-configure-virtual-network.png":::

## Connect the Virtual Networks in the two Azure Regions

After you've create the new virtual network and subnet, you're ready to connect the two regions so they can communicate with each other. There are two methods to do this:

- [Virtual Network Peering - Connect virtual networks with virtual network peering using the Azure portal](/azure/virtual-network/tutorial-connect-virtual-networks-portal) (Recommended)

  In some cases, you may have to use PowerShell to create the VNet-to-VNet connection. For example, if you use different Azure accounts you cannot configure the connection in the portal. In this case, review  [Configure a VNet-to-VNet connection using the Azure portal](/azure/vpn-gateway/vpn-gateway-vnet-vnet-rm-ps).

- [Site to Site VPN Gateway - Configure a VNet-to-VNet connection using the Azure portal](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal).

This tutorial uses virtual network peering. 

To configure virtual network peering, follow these steps: 


1. In the search box at the top of the Azure portal, look for *autoHAVNET* the virtual network in your local region. When **autoHAVNET** appears in the search results, select it.

1. Under **Settings**, select **Peerings**, and then select **+ Add**, as shown in the following picture:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/add-peering.png" alt-text="Screenshot of the Azure portal, Peerings page, showing how to add the virtual network peering.":::

1. Enter or select the following information, accept the defaults for the remaining settings, and then select **Add**.

    | Setting | Value |
    | --- | --- |
    | **This virtual network** | |
    | Peering link name | Enter *autoHAVNET-remote_HAVNET* for the name of the peering from **autoHAVNET** to the remote virtual network. |
    | **Remote virtual network** | |
    | Peering link name | Enter *remote_HAVNET-autoHAVNET* for the name of the peering from the remote virtual network to **autoHAVNET**. |
    | Subscription | Select your subscription of the remote virtual network. |
    | Virtual network  | Select **remote_HAVNET** for the name of the remote virtual network. The remote virtual network can be in the same region of **autoHAVNET** or in a different region. |

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/peering-settings-bidirectional.png" alt-text="Screenshot the Azure portal, Peerings page for the virtual network, showing peering settings." lightbox="./media/availability-group-manually-configure-multiple-regions/peering-settings-bidirectional.png" :::

   On the **Peerings** page, the **Peering status** is **Connected**, as shown in the following picture:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/peering-status.png" alt-text="Screenshot of the Azure portal, Peerings page, showing connected status of the virtual network peering." lightbox="./media/availability-group-manually-configure-multiple-regions/peering-status.png":::

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

1. Change the preferred DNS server address to the [address of the primary domain controller](./availability-group-manually-configure-prerequisites-tutorial-single-subnet.md?#note-the-ip-address-of-the-primary-domain-controller).

1. In **Network and Sharing Center**, select the network interface.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/26-network-interface.png" alt-text="Screenshot of the Networking and Sharing center on a VM, with the ethernet2 connection selected..":::

1. Select **Properties**.
1. Select **Internet Protocol Version 4 (TCP/IPv4)** and then select **Properties**.
1. Select **Use the following DNS server addresses** and then specify the address of the primary domain controller in **Preferred DNS server**.
1. Select **OK**, and then **Close** to commit the changes. You are now able to join the VM to **corp.contoso.com**.

   > [!IMPORTANT]
   > If you lose the connection to your remote desktop after changing the DNS setting, go to the Azure portal and restart the virtual machine.

1. From the remote desktop to the secondary domain controller, open **Server Manager Dashboard**.
1. Select the **Add roles and features** link on the dashboard.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/22-add-features.png" alt-text="Screenshot of the Server Manger on a VM, with Select the Add roles and features link on the dashboard highlighted.":::

1. Select **Next** until you get to the **Server Roles** section.
1. Select the **Active Directory Domain Services** and **DNS Server** roles. When you're prompted, add any additional features that are required by these roles.
1. After the features finish installing, return to the **Server Manager** dashboard.
1. Select the new **AD DS** option on the left-hand pane.
1. Select the **More** link on the yellow warning bar.
1. In the **Action** column of the **All Server Task Details** dialog, select **Promote this server to a domain controller**.
1. Under **Deployment Configuration**, select **Add a domain controller to an existing domain**.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/28-deployment-config.png" alt-text="Screenshot of the Active Directory Services Configuration Manager, showing the deployment configuration on a VM.":::

1. Select **Select**.
1. Connect by using the administrator account (**CORP.CONTOSO.COM\domainadmin**) and password (**Contoso!0000**).
1. In **Select a domain from the forest**, choose your domain and then select **OK**.
1. In **Domain Controller Options**, use the default values and set a DSRM password.

    >[!NOTE]
    >The **DNS Options** page might warn you that a delegation for this DNS server can't be created. You can ignore this warning in non-production environments.
    >

1. Select **Next** until the dialog reaches the **Prerequisites** check. Then select **Install**.

After the server finishes the configuration changes, restart the server.

## Create SQL Server VM

After the domain controller restarts, the next step is to [create a SQL Server virtual machine in the new region](./create-sql-vm-portal.md).

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

### <a name="joinDomain"></a>Join the server to the domain

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

   > [!TIP]
   > Make sure that you sign in with the domain administrator account. In the previous steps, you were using the BUILT IN administrator account. Now that the server is in the domain, use the domain account. In your RDP session, specify *DOMAIN*\\*username*.
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

A load balancer is required in the remote region to support the SQL Server availability group. The load balancer holds the IP addresses for the availability group listeners and the Windows Server Failover Cluster. This section summarizes how to create the load balancer in the Azure portal.

The load balancer must:

   - Be in the same network and subnet as the new virtual machine.
   - Have a static IP address for the availability group listener.
   - Include a backend pool consisting of only the virtual machines in the same region as the load balancer.
   - Use a TCP port probe specific to the IP address.
   - Have a load balancing rule specific to the SQL Server in the same region.  
   - Be a Standard Load Balancer if the virtual machines in the backend pool aren't part of either a single availability set or Virtual Machine Scale Set. For additional information review [Azure Load Balancer Standard overview](/azure/load-balancer/load-balancer-overview).
   - Be a Standard Load Balancer if the two virtual networks in two different regions are peered over global VNet peering. For more information, see [Azure Virtual Network frequently asked questions (FAQ)](/azure/virtual-network/virtual-networks-faq#what-are-the-constraints-related-to-global-vnet-peering-and-load-balancers).

The steps to [create the load balancer](availability-group-manually-configure-tutorial-single-subnet.md#configure-internal-load-balancer) are:

1. In the Azure portal, go to the resource group where your SQL Server is and select **+ Add**.
1. Search for **Load Balancer**. Choose the load balancer published by Microsoft.
1. Select **Create**.
1. Configure the following parameters for the load balancer.

   | Setting | Field |
   | --- | --- |
   | **Subscription** |Use the same subscription as the virtual machine. |
   | **Resource Group** |Use the same resource group as the virtual machine. |
   | **Name** |Use a text name for the load balancer, for example **remoteLB**. |
   | **Region** |Use the same region as the virtual machine. |
   | **SKU** |Standard |
   | **Type** |Internal |

   The Azure portal blade should look like this:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/create-load-balancer.png" alt-text="Screenshot of the Azure portal, Create Load Balancer page." lightbox="./media/availability-group-manually-configure-multiple-regions/create-load-balancer.png":::

1. Select **Next: Frontend IP Configuration**

1. Select **Add a frontend IP Configuration**

   :::image type="content" source="./media/availability-group-manually-configure-tutorial-single-subnet/add-fe-ip-config.png" alt-text="Screenshot of the Azure portal, Create Load Balancer page with add a frontend IP configuration highlighted.":::

1. Set up the frontend IP using the following values:

   - **Name**: A name that identifies the frontend IP configuration
   - **Virtual network**: The same network as the virtual machines.
   - **Subnet**: The subnet as the virtual machines.
   - **IP address assignment**: Static.
   - **IP address**: Use an available address from subnet. **Use this address for your availability group listener**. Note that this is different from your cluster IP address.
   - **Availability zone**: Optionally choose and availability zone to deploy your IP to.

   The following image shows the **Add frontend IP Configuration** UI:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/add-fe-ip-config-details.png" alt-text="Screenshot of the Azure portal, Add fronted IP configuration page.":::

1. Select **Add** to create the frontend IP.

1. Choose **Review + Create** to validate the configuration, and then **Create** to create the load balancer and the frontend IP.

To configure the load balancer, you need to create a backend pool, a probe, and set the load balancing rules. Do these in the Azure portal.

### Add a backend pool for the availability group listener

1. In the Azure portal, go to your availability group. You might need to refresh the view to see the newly created load balancer.

1. Select the load balancer, select **Backend pools**, and select **+Add**.

1. Provide a **Name** for the Backend pool.

1. Select **NIC** for Backend Pool Configuration.

1. Select **Add** to associate the backend pool with the newly created SQL Server VM.

1. Under **Virtual machine** choose the virtual machine that will host availability group replica.

1. Select **Add** to add the virtual machine to the backend pool.

1. Select **Save** to create the backend pool.

### Set the probe

1. Select the load balancer, choose **Health probes**, and then select **+Add**.

1. Set the listener health probe as follows:

   | Setting | Description | Example
   | --- | --- |---
   | **Name** | Text | SQLAlwaysOnEndPointProbe |
   | **Protocol** | Choose TCP | TCP |
   | **Port** | Any unused port | 59999 |
   | **Interval**  | The amount of time between probe attempts in seconds |5 |

1. Select **Add** to set the health probe.

### Set the load balancing rules

1. Select the load balancer, choose **Load balancing rules**, and select **+Add**.

1. Set the listener load balancing rules as follows.

   | Setting | Description | Example
   | --- | --- |---
   | **Name** | Text | SQLAlwaysOnEndPointListener |
   | **Frontend IP address** | Choose an address |Use the address that you created when you created the load balancer. |
   | **Backend pool** | Choose the backend pool |Select the backend pool containing the virtual machines targeted for the load balancer. |
   | **Protocol** | Choose TCP |TCP |
   | **Port** | Use the port for the availability group listener | 1433 |
   | **Backend Port** | This field is not used when Floating IP is set for direct server return | 1433 |
   | **Health Probe** |The name you specified for the probe | SQLAlwaysOnEndPointProbe |
   | **Session Persistence** | Drop down list | **None** |
   | **Idle Timeout** | Minutes to keep a TCP connection open | 4 |
   | **Floating IP (direct server return)** | |Enabled |

   > [!WARNING]
   > Direct server return is set during creation. It cannot be changed.
   >

1. Select **Save** to set the listener load balancing rules.

## Add Failover Clustering to SQL Server VM

To add Failover Clustering features, do the following steps on both SQL Server VMs:

1. Connect to the SQL Server virtual machine through the Remote Desktop Protocol (RDP) by using the *CORP\install* account. Open **Server Manager Dashboard**.
1. Select the **Add roles and features** link on the dashboard.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/22-add-features.png" alt-text="Screenshot of the Server manager, showing Add roles and features link highlighted on the dashboard page. ":::

1. Select **Next** until you get to the **Server Features** section.
1. In **Features**, select **Failover Clustering**.
1. Add any additional required features.
1. Select **Install** to add the features.

Repeat the steps on the other SQL Server VM.

  >[!NOTE]
  > This step, along with actually joining the SQL Server VMs to the failover cluster, can now be automated with [Azure SQL VM CLI](./availability-group-az-commandline-configure.md) and [Azure Quickstart Templates](availability-group-quickstart-template-configure.md).
  >

### Tuning Failover Cluster Network Thresholds

When running Windows Failover Cluster nodes in Azure VMs with SQL Server availability groups, change the cluster setting to a more relaxed monitoring state.  This will make the cluster much more stable and reliable.  For details on this, see [IaaS with SQL Server - Tuning Failover Cluster Network Thresholds](/windows-server/troubleshoot/iaas-sql-failover-cluster).

## <a name="endpoint-firewall"></a> Configure the firewall on the SQL Server VM

The solution requires the following TCP ports to be open in the firewall:

- **SQL Server VM**: Port 1433 for a default instance of SQL Server.
- **Azure load balancer probe:** Any available port. Examples frequently use 59999.
- **Cluster core load balancer IP address health probe:** Any available port. Examples frequently use 58888.
- **Database mirroring endpoint:** Any available port. Examples frequently use 5022.

The firewall ports need to be open on the new SQL Server VM.

The method of opening the ports depends on the firewall solution that you use. The next section explains how to open the ports in Windows Firewall. Open the required ports on your SQL Server VM.

### Open a TCP port in the firewall

1. On the SQL Server **Start** screen, launch **Windows Firewall with Advanced Security**.
1. On the left pane, select **Inbound Rules**. On the right pane, select **New Rule**.
1. For **Rule Type**, choose **Port**.
1. For the port, specify **TCP** and type the appropriate port numbers. See the following example:

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/35-tcp-ports.png" alt-text="Screenshot of creating a new Inbound Rule for the SQL firewall.":::

1. Select **Next**.
1. On the **Action** page, keep **Allow the connection** selected, and then select **Next**.
1. On the **Profile** page, accept the default settings, and then select **Next**.
1. On the **Name** page, specify a rule name (such as **Azure LB Probe**) in the **Name** text box, and then select **Finish**.

## Add SQL Server to the Windows Server Failover Cluster

The new SQL Server VM needs to be [added to the Windows Server Failover Cluster](availability-group-manually-configure-tutorial-single-subnet.md#addNode) that exists in your local region.

To add the SQL Server to the cluster, perform the following steps:

1. Use Remote Desktop Protocol (RDP) to connect to a SQL Server in the existing cluster. Use a domain account that is an administrator on both SQL Servers and the witness server.
1. In the **Server Manager** dashboard, select **Tools**, and then select **Failover Cluster Manager**.
1. In the left pane, right-click **Failover Cluster Manager**, and then select **Connect to Cluster**.
1. In the **Select Cluster** window under **Cluster name**, choose **\<Cluster on this server...\>**, select **Ok**.
1. In the browser tree, right-click the cluster and select **Add Node**.
1. In the **Add Node Wizard**, select **Next**. In the **Select Servers** page, add the name of the new SQL Server. Type the server name in **Enter server name** and then select **Add**. When you're done, select **Next**.

1. In the **Validation Warning** page, select **No** (in a production scenario you should perform the validation tests). Then, select **Next**.

1. In the **Confirmation** page if you're using Storage Spaces, clear the checkbox labeled **Add all eligible storage to the cluster.**

   >[!WARNING]
   >If you do not uncheck **Add all eligible storage to the cluster**, Windows detaches the virtual disks during the clustering process. As a result, they don't appear in Disk Manager or Explorer until the storage is removed from the cluster and reattached using PowerShell.
   >

1. Select **Next**.

1. Select **Finish**.

   Failover Cluster Manager shows that your cluster has a new node and lists it in the **Nodes** container.

### Add the Windows Server Failover cluster IP address

   > [!NOTE]
   > On Windows Server 2019, the cluster creates a **Distributed Server Name** instead of the **Cluster Network Name**. If you're using Windows Server 2019, skip to **Add IP Address for the Availability Group Listener**. You can create a cluster network name using [PowerShell](failover-cluster-instance-storage-spaces-direct-manually-configure.md#create-windows-failover-cluster). Review the blog [Failover Cluster: Cluster Network Object](https://blogs.windows.com/windowsexperience/2018/08/14/announcing-windows-server-2019-insider-preview-build-17733/#W0YAxO8BfwBRbkzG.97) for more information.  

Next, add the IP address resource to the cluster for the new SQL Server VM.

1. Create the IP address resource in **Failover Cluster Manager**, select the name of the cluster, then right-click the cluster name under **Cluster Core Resources** and select **Properties**:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/cluster-name-properties.png" alt-text="Screenshot that shows the Failover Cluster Manager with a cluster name Server Name and Properties selected." lightbox="./media/availability-group-manually-configure-multiple-regions/cluster-name-properties.png":::

1. On the **Properties** dialog box, select **Add** under **IP Address**, and then add the IP address of the cluster name from the remote network region. Select **OK** on the **IP Address** dialog box, and then select **OK** again on the **Cluster Properties** dialog box to save the new IP address.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/add-cluster-ip-address.png" alt-text="Screenshot of Cluster name properties, and IP address dialog box, showing how to add the cluster IP." lightbox="./media/availability-group-manually-configure-multiple-regions/add-cluster-ip-address.png":::

1. Add the IP address as a dependency for the core cluster name.

   Open the cluster properties once more and select the **Dependencies** tab. Configure an OR dependency for the two IP addresses:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/cluster-ip-dependencies.png" alt-text="Screenshot of the cluster properties dialog box to add the dependencies.":::

### Add IP Address for the Availability Group Listener

The IP address for the listener in the remote region needs to be added to the cluster. To add the IP Address, follow these steps:

1. Right-click the availability group role in Failover Cluster Manager, choose **Add Resource**, **More Resources**, and select **IP Address**.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/20-add-ip-resource.png" alt-text="Screenshot of the Failover Cluster Manager, selecting IP address on the availability group right-click menu." lightbox="./media/availability-group-manually-configure-multiple-regions/20-add-ip-resource.png":::

1. To configure this IP address, right select the resource under **Other Resources** and select **Properties**.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/configure-listener-ip-cluster.png" alt-text="Screenshot of Failover Cluster Manager, with the right-click menu open, selecting properties. ":::

1. Provide a **Name** for the new resource, select the network from the remote data center, assign the static IP address from the new Azure load balancer.

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/assign-listener-ip-cluster.png" alt-text="Screenshot of the IP address properties dialog box, showing assigning the listener IP in the cluster.":::

1. Select **Apply**, and then **OK**.

1. Add the IP address resource as a dependency for the listener client access point (network name) cluster, by right-clicking on the listener client access point, and choosing **Properties**. Browse to the **Dependencies** tab and add the new IP address resource to the listener client access point. The following screenshot shows a properly configured IP address cluster resource:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/50-configure-dependency-multiple-ip.png" alt-text="Screenshot of Failover Cluster Manager, the Server name properties dialog, showing the configured the IP addresses for the availability group." lightbox="./media/availability-group-manually-configure-multiple-regions/50-configure-dependency-multiple-ip.png" :::

   > [!IMPORTANT]
   > The cluster resource group includes both IP addresses. Both IP addresses are dependencies for the listener client access point. Use the **OR** operator in the cluster dependency configuration.

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

## Enable availability groups

Next, enable the **Always On availability groups** feature. Do these steps on the new SQL Server.

1. From the **Start** screen, launch **SQL Server Configuration Manager**.
1. In the browser tree, select **SQL Server Services**, then right-click the **SQL Server (MSSQLSERVER)** service and select **Properties**.
1. Select the **Always On High Availability** tab, then select **Enable Always On availability groups**, as follows:

   :::image type="content" source="./media/availability-group-manually-configure-tutorial-single-subnet/54-enable-Always-On.png" alt-text="Screenshot of SQL Server properties, AlwaysOn High Availability tab, with the checkbox to enable emphasized.":::

1. Select **Apply**. Select **OK** in the pop-up dialog.

1. Restart the SQL Server service.

## Add Replica to availability group

Once SQL Server has restarted on the newly created virtual machine, it can be added as a [replica to the availability group](/sql/database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio).

1. On remote desktop session to the primary SQL Server in the availability group. In **Object Explorer** in SSMS, open **Always On High Availability**, and **Availability Groups**, right-click on your availability group name, and select **Add Replica**.
1. Connect to the existing replica, and choose **Next**.
1. Select **Add Replica** and connect to the new SQL Server VM.

   >[!IMPORTANT]
   > A replica in a remote Azure region, should be set to asynchronous replication with manual failover.

1. In the **Select Initial Data Synchronization** page, select **Full** and specify a shared network location. For the location, use the [backup share that you created](availability-group-manually-configure-tutorial-single-subnet.md?#backupshare). In the example it was, **\\\\<First SQL Server\>\Backup\\**. Select **Next**.

   >[!NOTE]
   >Full synchronization takes a full backup of the database on the first instance of SQL Server and restores it to the second instance. For large databases, full synchronization is not recommended because it may take a long time. You can reduce this time by manually taking a backup of the database and restoring it with `NO RECOVERY`. If the database is already restored with `NO RECOVERY` on the second SQL Server before configuring the availability group, choose **Join only**. If you want to take the backup after configuring the availability group, choose **Skip initial data synchronization**.
   >

   :::image type="content" source="./media/availability-group-manually-configure-tutorial-single-subnet/70-data-synchronization.png" alt-text="Screenshot of the New Availability Group window in SSMS, Select Data Synchronization page, with Full selected.":::

1. In the **Validation** page, select **Next**. This page should look similar to the following image:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/ag-validation.png" alt-text="Screenshot of the Add Replica to Availability Group window in SSMS, showing the Validation page.":::

    >[!NOTE]
    >There is a warning for the listener configuration because you have not configured an availability group listener. You can ignore this warning because the listener is already setup, it was created after creating the Azure load balancer in the local region.

1. In the **Summary** page, select **Finish**, then wait while the wizard configures the new availability group. In the **Progress** page, you can select **More details** to view the detailed progress. Once the wizard is finished, inspect the **Results** page to verify that the availability group is successfully created.
1. Select **Close** to exit the wizard.

### Check the availability group

1. In **Object Explorer**, expand **Always On High Availability**, and then expand **availability groups**. Right-click the availability group and select **Show Dashboard**.

   :::image type="content" source="./media/availability-group-manually-configure-tutorial-single-subnet/76-show-dashboard.png" alt-text="Screenshot of Object Explorer in SSMS, right-click menu open for the availability group, show dashboard selected.":::

   Your **Always On Dashboard** should look similar to the following screenshot, now with another replica:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/ag-health-dashboard.png" alt-text="Screenshot of SSMS availability group dashboard." lightbox="./media/availability-group-manually-configure-multiple-regions/ag-health-dashboard.png" :::

   You can see the replicas, the failover mode of each replica, and the synchronization state.

### Check the availability group listener

1. In **Object Explorer**, expand **Always On High Availability**, expand **availability groups**, and then expand **Availability Group Listener**. Right-click the listener name and select **Properties**. You should now see both IP addresses for the listener, one in each region:

   :::image type="content" source="./media/availability-group-manually-configure-multiple-regions/multi-region-listener.png" alt-text="Screenshot of the Availability Group Listener Properties window in SSMS, showing both IP addresses being used for the listener.":::

## Set connection for multiple subnets

The replica in the remote data center is part of the availability group but it is in a different subnet. If this replica becomes the primary replica, application connection time-outs may occur. This behavior is the same as an on-premises availability group in a multi-subnet deployment. To allow connections from client applications, either update the client connection or configure name resolution caching on the cluster network name resource.

Preferably, update the cluster configuration to set `RegisterAllProvidersIP=1` and the client connection strings to set `MultiSubnetFailover=Yes`. See [Connecting With MultiSubnetFailover](/sql/relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery#Anchor_0).

If you can't modify the connection strings, you can configure name resolution caching. See [Time-out error and you can't connect to a SQL Server 2012 Always On availability group listener in a multi-subnet environment](https://support.microsoft.com/help/2792139/time-out-error-and-you-cannot-connect-to-a-sql-server-2012-alwayson-av).

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

For more information, see the following articles:

- [Perform a Planned Manual Failover of an Availability Group (SQL Server)](/sql/database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server)
- [Perform a Forced Manual Failover of an Availability Group (SQL Server)](/sql/database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server)

## Next steps

To learn more, see:

- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Always On availability groups overview](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)
