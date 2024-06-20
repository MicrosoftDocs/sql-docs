---
title: "Tutorial: Prerequisites for a single-subnet availability group"
description: "This tutorial shows how to configure the prerequisites for creating a SQL Server Always On availability group on Azure virtual machines in a single subnet."
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma
ms.date: 06/18/2024
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
editor: monicar
tags: azure-service-management
---

# Tutorial: Prerequisites for single-subnet availability groups - SQL Server on Azure VMs

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

[!INCLUDE[tip-for-multi-subnet-ag](../../includes/virtual-machines-ag-listener-multi-subnet.md)]

This tutorial shows how to complete the prerequisites for creating a [SQL Server Always On availability group on Azure virtual machines](availability-group-manually-configure-tutorial-single-subnet.md) within a single subnet. When you've completed the prerequisites, you'll have a domain controller, two SQL Server VMs, and a witness server in a single resource group.

This article manually configures the availability group environment. It's also possible to automate the steps by using the [Azure portal](availability-group-azure-portal-configure.md), [PowerShell or the Azure CLI](availability-group-az-commandline-configure.md), or [Azure quickstart templates](availability-group-quickstart-template-configure.md).

**Time estimate**: It might take a couple of hours to complete the prerequisites. You'll spend much of this time creating virtual machines.

The following diagram illustrates what you build in the tutorial.

:::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/00-end-state-sample-no-elb.png" alt-text="Diagram of the setup of an availability group.":::

>[!NOTE]
> It's now possible to lift and shift your availability group solution to SQL Server on Azure VMs by using Azure Migrate. To learn more, see [Migrate an availability group](../../migration-guides/virtual-machines/sql-server-availability-group-to-sql-on-azure-vm.md).

## Review availability group documentation

This tutorial assumes that you have a basic understanding of SQL Server Always On availability groups. If you're not familiar with this technology, see [Overview of Always On availability groups (SQL Server)](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server).

## Create an Azure account

You need an Azure account. You can [open a free Azure account](https://signup.azure.com/signup?offer=ms-azr-0044p&appId=102&ref=azureplat-generic) or [activate Visual Studio subscriber benefits](/visualstudio/subscriptions/subscriber-benefits).

## Create a resource group

To create the resource group in the Azure portal, follow these steps:

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Select **+ Create a resource**.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-multi-subnet/01-portal-plus.png" alt-text="Screenshot of the Azure portal that shows the button for creating a resource.":::

1. Search for **resource group** in the **Marketplace** search box, and then choose the **Resource group** tile from Microsoft. Select **Create**.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-multi-subnet/01-resource-group-search.png" alt-text="Screenshot of the Azure portal that shows selecting a resource group from the marketplace.":::

1. On the **Create a resource group** page, fill out the values to create the resource group:
   1. Choose the appropriate Azure subscription from the dropdown list.
   1. Provide a name for your resource group, such as **SQL-HA-RG**.
   1. Choose a region from the dropdown list, such as **West US 2**. Be sure to deploy all subsequent resources to this location.
   1. Select **Review + create** to review your resource parameters, and then select **Create** to create your resource group.  

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-multi-subnet/01-resource-group-create-complete.png" alt-text="Screenshot that shows filling out values to create a resource group in the Azure portal.":::

## Create the network and subnet

The next step is to create the network and subnet in the Azure resource group.

The solution in this tutorial uses one virtual network and one subnet. The [virtual network overview](/azure/virtual-network/virtual-networks-overview) provides more information about networks in Azure.

To create the virtual network in the Azure portal, follow these steps:

1. Go to your resource group in the [Azure portal](https://portal.azure.com) and select **+ Create**.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-multi-subnet/02-create-resource-rg.png" alt-text="Screenshot of the Azure portal that shows the button for creating a virtual network for a resource group.":::

1. Search for **virtual network** in the **Marketplace** search box, and then choose the **Virtual network** tile from Microsoft. Select **Create**.  
1. On the **Create virtual network** page, enter the following information on the **Basics** tab:
   1. Under **Project details**, for **Subscription**, choose the appropriate Azure subscription. For **Resource group**, select the resource group that you created previously, such as **SQL-HA-RG**.
   1. Under **Instance details**, provide a name for your virtual network, such as **autoHAVNET**. In the dropdown list, choose the same region that you chose for your resource group.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/03-create-vnet-basics.png" alt-text="Screenshot of the Azure portal that shows providing basic details for a creating a virtual network." lightbox="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/03-create-vnet-basics.png":::

1. On the **IP addresses** tab, select the ellipsis (**...**) next to **+ Add a subnet**. Select **Delete address space** to remove the existing address space, if you need a different address range.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/04-delete-address-space.png" alt-text="Screenshot of the Azure portal that shows selections for deleting an address space.":::

1. Select **Add an IP address space** to open the pane to create the address space that you need. This tutorial uses the address space of 192.168.0.0/16 (**192.168.0.0** for **Starting address** and **/16** for **Address space size**). Select **Add** to create the address space.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/05-add-address-space.png" alt-text="Screenshot of the Azure portal that shows selections for creating an address space." lightbox="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/05-add-address-space.png" :::

1. Select **+ Add a subnet**, and then:
    1. Provide a value for **Subnet name**, such as **admin**.
    1. Provide a unique subnet address range within the virtual network address space.
       
       For example, if your address range is 192.168.0.0/16, enter **192.168.15.0** for **Starting address** and **/24** for **Subnet size**.
    1. Select **Add** to add your new subnet.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/06-configure-virtual-network.png" alt-text="Screenshot of the Azure portal that shows selections for adding a subnet." lightbox="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/06-configure-virtual-network.png":::

1. Select **Review + Create**.

Azure returns you to the portal dashboard and notifies you when the new network is created.

## Create availability sets

Before you create virtual machines, you need to create availability sets. Availability sets reduce the downtime for planned or unplanned maintenance events. 

An Azure availability set is a logical group of resources that Azure places on these physical domains:

- **Fault domain**: Ensures that the members of the availability set have separate power and network resources. 
- **Update domain**: Ensures that members of the availability set aren't brought down for maintenance at the same time. 

For more information, see [Manage the availability of virtual machines](/azure/virtual-machines/availability?toc=%2fazure%2fvirtual-machines%2fwindows%2ftoc.json).

You need two availability sets. One is for the domain controllers. The second is for the SQL Server VMs.

To create an availability set:

1. Go to the resource group and select **Add**. 
1. Filter the results by typing **availability set**. Select **Availability Set** in the results.
1. Select **Create**.

Configure two availability sets according to the parameters in the following table:

| Field | Domain controller availability set | SQL Server availability set |
| --- | --- | --- |
| **Name** |**adavailabilityset** |**sqlavailabilityset** |
| **Resource group** |**SQL-HA-RG** |**SQL-HA-RG** |
| **Fault domains** |**3** |**3** |
| **Update domains** |**5** |**3** |

After you create the availability sets, return to the resource group in the Azure portal.

## Create domain controllers

After you've created the network, subnet, and availability sets, you're ready to create and configure domain controllers.

### Create virtual machines for the domain controllers

Now, create two virtual machines. Name them **ad-primary-dc** and **ad-secondary-dc**. Use the following steps for each VM:

1. Return to the **SQL-HA-RG** resource group.
2. Select **Add**.
3. Type **Windows Server 2016 Datacenter**, and then select **Windows Server 2016 Datacenter**.
4. In **Windows Server 2016 Datacenter**, verify that the deployment model is **Resource Manager**, and then select **Create**.

> [!NOTE]
> The **ad-secondary-dc** virtual machine is optional, to provide high availability for Active Directory Domain Services.
>

The following table shows the settings for these two machines:

| **Field** | Value |
| --- | --- |
| **Name** |First domain controller: **ad-primary-dc**</br></br>Second domain controller: **ad-secondary-dc** |
| **VM disk type** |**SSD** |
| **User name** |**DomainAdmin** |
| **Password** |**Contoso!0000** |
| **Subscription** |Your subscription |
| **Resource group** |**SQL-HA-RG** |
| **Location** |Your location |
| **Size** |**DS1_V2** |
| **Storage** | **Use managed disks** - **Yes** |
| **Virtual network** |**autoHAVNET** |
| **Subnet** |**admin** |
| **Public IP address** |Same name as the VM |
| **Network security group** |Same name as the VM |
| **Availability set** |**adavailabilityset** </br></br>**Fault domains**: **3** </br></br>**Update domains**: **5**|
| **Diagnostics** |Enabled |
| **Diagnostics storage account** |Automatically created |

>[!IMPORTANT]
>You can place a VM in an availability set only when you create it. You can't change the availability set after a VM is created. See [Manage the availability of virtual machines](/azure/virtual-machines/availability).

### Configure the primary domain controller

In the following steps, configure the **ad-primary-dc** machine as a domain controller for **corp.contoso.com**:

1. In the portal, open the **SQL-HA-RG** resource group and select the **ad-primary-dc** machine. On **ad-primary-dc**, select **Connect** to open a Remote Desktop Protocol (RDP) file for remote desktop access.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/20-connect-rdp.png" alt-text="Screenshot of the Azure portal that shows selections for connecting to a virtual machine.":::

1. Sign in with your configured administrator account (**\DomainAdmin**) and password (**Contoso!0000**).
1. By default, the **Server Manager** dashboard should be displayed. Select the **Add roles and features** link on the dashboard.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/22-add-features.png" alt-text="Screenshot of the link to add roles and features on the Server Manager dashboard.":::

1. Select **Next** until you get to the **Server Roles** section.
1. Select the **Active Directory Domain Services** and **DNS Server** roles. When you're prompted, add any features that these roles require.

   > [!NOTE]
   > Windows warns you that there is no static IP address. If you're testing the configuration, select **Continue**. For production scenarios, set the IP address to static in the Azure portal, or [use PowerShell to set the static IP address of the domain controller machine](/previous-versions/azure/virtual-network/virtual-networks-reserved-private-ip).
   >

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/23-add-roles.png" alt-text="Screenshot that shows selecting server roles in the wizard for adding roles and features.":::

1. Select **Next** until you reach the **Confirmation** section. Select the **Restart the destination server automatically if required** checkbox.
1. Select **Install**.
1. After installation of the features finishes, return to the **Server Manager** dashboard.
1. Select the new **AD DS** option on the left pane.
1. Select the **More** link on the yellow warning bar.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/24-ad-ds-more.png" alt-text="Screenshot of a message about configuring a DNS server on the Server Manager dashboard.":::

1. In the **Action** column of the **All Server Task Details** dialog, select **Promote this server to a domain controller**.
1. In the **Active Directory Domain Services Configuration Wizard**, use the following values:

    | **Page** | Setting |
    | --- | --- |
    | **Deployment Configuration** |**Add a new forest**<br/><br/>**Root domain name** = **corp.contoso.com** |
    | **Domain Controller Options** |**DSRM Password** = **Contoso!0000**<br/><br/>**Confirm Password** = **Contoso!0000** |

1. Select **Next** to go through the other pages in the wizard. On the **Prerequisites Check** page, verify that the following message appears: "All prerequisite checks passed successfully." You can review any applicable warning messages, but it's possible to continue with the installation.
1. Select **Install**. The **ad-primary-dc** virtual machine automatically restarts.

### Note the IP address of the primary domain controller

Use the primary domain controller for DNS. Note the primary domain controller's private IP address.

One way to get the primary domain controller's IP address is through the Azure portal:

1. Open the resource group.

1. Select the primary domain controller.

1. On the primary domain controller, select **Network interfaces**.

:::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/25-primary-dc-ip.png" alt-text="Screenshot of a private IP address shown on the Azure portal.":::

### Configure the virtual network DNS

After you create the first domain controller and enable DNS on the first server, configure the virtual network to use this server for DNS:

1. In the Azure portal, select the virtual network.

1. Under **Settings**, select **DNS Server**.

1. Select **Custom**, and enter the private IP address of the primary domain controller.

1. Select **Save**.

### Configure the secondary domain controller

After the primary domain controller restarts, you can use the following steps to configure the secondary domain controller. This optional procedure is for high availability.

#### Set preferred DNS server address

The preferred DNS server address [should not be updated](/azure/virtual-network/virtual-networks-name-resolution-for-vms-and-role-instances#specify-dns-servers) directly within a VM, it should be edited from the [Azure portal, or Powershell, or Azure CLI](/azure/virtual-network/virtual-network-network-interface?tabs=network-interface-portal#change-dns-servers). The steps below are to make the change inside of the Azure portal:

1. Sign-in to the [Azure portal](https://portal.azure.com).

1. In the search box at the top of the portal, enter **Network interface**. Select **Network interfaces** in the search results.

1. Select the network interface for the second domain controller that you want to view or change settings for from the list.

1. In **Settings**, select **DNS servers**.

1. Select either:
   
    - **Inherit from virtual network**: Choose this option to inherit the DNS server setting defined for the virtual network the network interface is assigned to. This would automatically inherit the primary domain controller as the DNS server.
   
    - **Custom**: You can configure your own DNS server to resolve names across multiple virtual networks. Enter the IP address of the server you want to use as a DNS server. The DNS server address you specify is assigned only to this network interface and overrides any DNS setting for the virtual network the network interface is assigned to. If you select custom, then input the IP address of the primary domain controller, such as `192.168.15.4`.

1. Select **Save**. If using a **Custom** DNS Server, return to the virtual machine in the Azure portal and restart the VM. Once the virtual machine has restarted, you can join the VM to the domain.

### Join the domain

Next, join the **corp.contoso.com** domain. To do so, follow these steps:

1. Remotely connect to the virtual machine using the **BUILTIN\DomainAdmin** account. This account is the same one used when [creating the domain controller virtual machines](availability-group-manually-configure-prerequisites-tutorial-single-subnet.md?#create-virtual-machines-for-the-domain-controllers).
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

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-multi-subnet/09-add-features.png" alt-text="Server Manager - Add roles":::

1. Select **Next** until you get to the **Server Roles** section.
1. Select the **Active Directory Domain Services** and **DNS Server** roles. When you're prompted, add any additional features that are required by these roles.
1. After the features finish installing, return to the **Server Manager** dashboard.
1. Select the new **AD DS** option on the left-hand pane.
1. Select the **More** link on the yellow warning bar.
1. In the **Action** column of the **All Server Task Details** dialog, select **Promote this server to a domain controller**.
1. Under **Deployment Configuration**, select **Add a domain controller to an existing domain**.
1. Click **Select**.
1. Connect by using the administrator account (**CORP.CONTOSO.COM\domainadmin**) and password (**Contoso!0000**).
1. In **Select a domain from the forest**, choose your domain and then select **OK**.
1. In **Domain Controller Options**, use the default values and set a DSRM password.

    >[!NOTE]
    >The **DNS Options** page might warn you that a delegation for this DNS server can't be created. You can ignore this warning in non-production environments.
    >

1. Select **Next** until the dialog reaches the **Prerequisites** check. Then select **Install**.

After the server finishes the configuration changes, restart the server.

### Add the private IP address of the secondary domain controller to the VPN DNS server

In the Azure portal, under **Virtual network**, change the DNS server to include the IP address of the secondary domain controller. This setting allows the DNS service redundancy.

### <a name="DomainAccounts"></a> Configure the domain accounts

Next, configure two accounts in total in Active Directory, one installation account and then a service account for both SQL Server VMs. For example, use the values in the following table for the accounts:

|Account  | VM  |Full domain name  |Description   |
|---------|---------|---------|---------|
|Install    |Both| Corp\Install        |Log into either VM with this account to configure the cluster and availability group. |
|SQLSvc     |Both (sqlserver-0 and sqlserver-1) |Corp\SQLSvc | Use this account for the SQL Server service and SQL Agent Service account on the both SQL Server VMs. |

Use the following steps to create each account:

1. Sign in to the **ad-primary-dc** machine.
1. In Server Manager, select **Tools**, and then select **Active Directory Administrative Center**.
1. Select **corp (local)** from the left pane.
1. On the **Tasks** pane, select **New**, and then select **User**.

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/29-ad-dc-new-user.png" alt-text="Screenshot that shows selections for adding a user in the Active Directory Administrative Center." lightbox="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/29-ad-dc-new-user.png" :::

   >[!TIP]
   >Set a complex password for each account. For non-production environments, set the user account to never expire.

1. Select **OK** to create the user.

### Grant the required permissions to the installation account

1. In **Active Directory Administrative Center**, select **corp (local)** on the left pane. On the **Tasks** pane, select **Properties**.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/31-ad-dc-properties.png" alt-text="Screenshot that shows selecting user properties in the Active Directory Administrative Center." lightbox="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/31-ad-dc-properties.png" :::

1. Select **Extensions**, and then select the **Advanced** button on the **Security** tab.
1. In the **Advanced Security Settings for corp** dialog, select **Add**.
1. Choose **Select a principal**, search for **CORP\Install**, and then select **OK**.
1. Select the **Read all properties** checkbox.

1. Select the **Create Computer objects** checkbox.

     :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/33-add-permissions.png" alt-text="Screenshot of the interface for corp user permissions." lightbox="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/33-add-permissions.png" :::

1. Select **OK**, and then select **OK** again. Close the **corp** properties window.

Now that you've finished configuring Active Directory and the user objects, you can create additional VMs that you'll join to the domain.

## Create SQL Server VMs

The solution in this tutorial requires you to create three virtual machines: two with SQL Server instances and one that functions as a witness. 

Windows Server 2016 can use a [cloud witness](/windows-server/failover-clustering/deploy-cloud-witness). But for consistency with previous operating systems, this article uses a virtual machine for a witness.  

Before you proceed, consider the following design decisions:

* **Storage: Azure managed disks**

   For the virtual machine storage, use Azure managed disks. We recommend managed disks for SQL Server virtual machines. Managed disks handle storage behind the scenes. In addition, when virtual machines with managed disks are in the same availability set, Azure distributes the storage resources to provide appropriate redundancy. 
   
   For more information, see [Introduction to Azure managed disks](/azure/virtual-machines/managed-disks-overview). For specifics about managed disks in an availability set, see [Availability options for Azure virtual machines](/azure/virtual-machines/availability).

* **Network: Private IP addresses in production**

   For the virtual machines, this tutorial uses public IP addresses. A public IP address enables remote connection directly to a virtual machine over the internet and makes configuration steps easier. In production environments, we recommend only private IP addresses to reduce the vulnerability footprint of the SQL Server instance's VM resource.

* **Network: Number of NICs per server**

   Use a single network interface card (NIC) per server (cluster node) and a single subnet. Azure networking has physical redundancy, which makes additional NICs and subnets unnecessary on an Azure VM guest cluster. 
   
   The cluster validation report will warn you that the nodes are reachable only on a single network. You can ignore this warning on Azure VM guest failover clusters.

### Create and configure the VMs

1. Go back to the **SQL-HA-RG** resource group, and then select **Add**.
1. Search for the appropriate gallery item, select **Virtual Machine**, and then select **From Gallery**. 
1. Use the information in the following table to finish creating the three VMs:

   | Page | VM1 | VM2 | VM3 |
   | --- | --- | --- | --- |
   | Select the appropriate gallery item |**Windows Server 2016 Datacenter** |**SQL Server 2016 SP1 Enterprise on Windows Server 2016** |**SQL Server 2016 SP1 Enterprise on Windows Server 2016** |
   | Virtual machine configuration: **Basics** |**Name** = **cluster-fsw**<br/><br/>**User Name** = **DomainAdmin**<br/><br/>**Password** = **Contoso!0000**<br/><br/>**Subscription** = Your subscription<br/><br/>**Resource group** = **SQL-HA-RG**<br/><br/>**Location** = Your Azure location |**Name** = **sqlserver-0**<br/><br/>**User Name** = **DomainAdmin**<br/><br/>**Password** = **Contoso!0000**<br/><br/>**Subscription** = Your subscription<br/><br/>**Resource group** = **SQL-HA-RG**<br/><br/>**Location** = Your Azure location |**Name** = **sqlserver-1**<br/><br/>**User Name** = **DomainAdmin**<br/><br/>**Password** = **Contoso!0000**<br/><br/>**Subscription** = Your subscription<br/><br/>**Resource group** = **SQL-HA-RG**<br/><br/>**Location** = Your Azure location |
   | Virtual machine configuration: **Size** |**SIZE** = **DS1\_V2** (1 vCPU, 3.5 GB) |**SIZE** = **DS2\_V2** (2 vCPUs, 7 GB)</br></br>The size must support SSD storage (premium disk support). |**SIZE** = **DS2\_V2** (2 vCPUs, 7 GB) |
   | Virtual machine configuration: **Settings** |**Storage** = **Use managed disks**<br/><br/>**Virtual network** = **autoHAVNET**<br/><br/>**Subnet** = **admin (192.168.15.0/24)**<br/><br/>**Public IP address** = Automatically generated<br/><br/>**Network security group** = **None**<br/><br/>**Monitoring Diagnostics** = Enabled<br/><br/>**Diagnostics storage account** = **Use an automatically generated storage account**<br/><br/>**Availability set** = **sqlAvailabilitySet**<br/><br/> |**Storage** = **Use managed disks**<br/><br/>**Virtual network** = **autoHAVNET**<br/><br/>**Subnet** = **admin (192.168.15.0/24)**<br/><br/>**Public IP address** = Automatically generated<br/><br/>**Network security group** = **None**<br/><br/>**Monitoring Diagnostics** = Enabled<br/><br/>**Diagnostics storage account** = **Use an automatically generated storage account**<br/><br/>**Availability set** = **sqlAvailabilitySet**<br/><br/> |**Storage** = **Use managed disks**<br/><br/>**Virtual network** = **autoHAVNET**<br/><br/>**Subnet** = **admin (192.168.15.0/24)**<br/><br/>**Public IP address** = Automatically generated<br/><br/>**Network security group** = **None**<br/><br/>**Monitoring Diagnostics** = Enabled<br/><br/>**Diagnostics storage account** = **Use an automatically generated storage account**<br/><br/>**Availability set** = **sqlAvailabilitySet**<br/><br/> |
   | Virtual machine configuration: **SQL Server settings** |Not applicable |**SQL connectivity** = **Private (within virtual network)**<br/><br/>**Port** = **1433**<br/><br/>**SQL Authentication** = Disabled<br/><br/>**Storage configuration** = **General**<br/><br/>**Automated patching** = **Sunday at 2:00**<br/><br/>**Automated backup** = Disabled</br><br/>**Azure Key Vault integration** = Disabled |**SQL connectivity** = **Private (within virtual network)**<br/><br/>**Port** = **1433**<br/><br/>**SQL Authentication** = Disabled<br/><br/>**Storage configuration** = **General**<br/><br/>**Automated patching** = **Sunday at 2:00**<br/><br/>**Automated backup** = Disabled</br><br/>**Azure Key Vault integration** = Disabled |

> [!NOTE]
> The machine sizes suggested here are meant for testing availability groups in Azure virtual machines. For the best performance on production workloads, see the recommendations for SQL Server machine sizes and configuration in [Performance best practices for SQL Server in Azure virtual machines](./performance-guidelines-best-practices-checklist.md?toc=%2fazure%2fvirtual-machines%2fwindows%2ftoc.json).

After the three VMs are fully provisioned, you need to join them to the **corp.contoso.com** domain and grant **CORP\Install** administrative rights to the machines.

### <a name="joinDomain"></a>Join the servers to the domain

Complete the following steps for both the SQL Server VMs and the file share witness server:

1. Remotely connect to the virtual machine with **BUILTIN\DomainAdmin**.
1. In Server Manager, select **Local Server**.
1. Select the **WORKGROUP** link.
1. In the **Computer Name** section, select **Change**.
1. Select the **Domain** checkbox, and enter **corp.contoso.com** in the text box. Select **OK**.
1. In the **Windows Security** popup dialog, specify the credentials for the default domain administrator account (**CORP\DomainAdmin**) and the password (**Contoso!0000**).
1. When you see the "Welcome to the corp.contoso.com domain" message, select **OK**.
1. Select **Close**, and then select **Restart Now** in the popup dialog.

## Add accounts

Add the installation account as an administrator on each VM, grant permission to the installation account and local accounts within SQL Server, and update the SQL Server service account.

### Add the CORP\Install user as an administrator on each cluster VM

After each virtual machine restarts as a member of the domain, add **CORP\Install** as a member of the local administrators group:

1. Wait until the VM is restarted, and then open the RDP file again from the primary domain controller. Sign in to **sqlserver-0** by using the **CORP\DomainAdmin** account.

   >[!TIP]
   >Be sure to sign in with the domain administrator account. In the previous steps, you were using the **BUILTIN** administrator account. Now that the server is in the domain, use the domain account. In your RDP session, specify *DOMAIN*\\*username*.

1. In Server Manager, select **Tools**, and then select **Computer Management**.
1. In the **Computer Management** window, expand **Local Users and Groups**, and then select **Groups**.
1. Double-click the **Administrators** group.
1. In the **Administrators Properties** dialog, select the **Add** button.
1. Enter the user **CORP\Install**, and then select **OK**.
1. Select **OK** to close the **Administrator Properties** dialog.
1. Repeat the previous steps on **sqlserver-1** and **cluster-fsw**.

### Create a sign-in on each SQL Server VM for the installation account

Use the installation account (**CORP\install**) to configure the availability group. This account needs to be a member of the **sysadmin** fixed server role on each SQL Server VM. 

The following steps create a sign-in for the installation account. Complete them on both SQL Server VMs.

1. Connect to the server through RDP by using the **\<MachineName\>\DomainAdmin** account.

1. Open SQL Server Management Studio and connect to the local instance of SQL Server.

1. In **Object Explorer**, select **Security**.

1. Right-click **Logins**. Select **New Login**.

1. In **Login - New**, select **Search**.

1. Select **Locations**.

1. Enter the network credentials for the domain administrator. Use the installation account (**CORP\install**).

1. Set the sign-in to be a member of the **sysadmin** fixed server role.

1. Select **OK**.

### Configure system account permissions

To create an account for the system and grant appropriate permissions, complete the following steps on each SQL Server instance:

1. Create an account for `[NT AUTHORITY\SYSTEM]` by using the following script:

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

1. Open **SQL Server Configuration Manager**.
1. Right-click the SQL Server service, and then select **Properties**.
1. Set the account and password.

For SQL Server availability groups, each SQL Server VM needs to run as a domain account.

## Add failover clustering

To add failover clustering features, complete the following steps on both SQL Server VMs:

1. Connect to the SQL Server virtual machine through RDP by using the **CORP\install** account. Open the **Server Manager** dashboard.
1. Select the **Add roles and features** link on the dashboard.

    :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/22-add-features.png" alt-text="Screenshot of the link for adding roles and features on the Server Manager dashboard.":::

1. Select **Next** until you get to the **Server Features** section.
1. In **Features**, select **Failover Clustering**.
1. Add any required features.
1. Select **Install**.

> [!NOTE]
> You can now automate this task, along with actually joining the SQL Server VMs to the failover cluster, by using the [Azure CLI](./availability-group-az-commandline-configure.md) and [Azure quickstart templates](availability-group-quickstart-template-configure.md).

### Tune network thresholds for a failover cluster

When you're running Windows failover cluster nodes in Azure VMs with SQL Server availability groups, change the cluster setting to a more relaxed monitoring state. This change will make the cluster more stable and reliable. For details, see [IaaS with SQL Server: Tuning failover cluster network thresholds](/windows-server/troubleshoot/iaas-sql-failover-cluster).

## <a name="endpoint-firewall"></a> Configure the firewall on each SQL Server VM

The solution requires the following TCP ports to be open in the firewall:

- **SQL Server VM**: Port 1433 for a default instance of SQL Server.
- **Azure load balancer probe**: Any available port. Examples frequently use 59999.
- **Load balancer IP address health probe for cluster core**: Any available port. Examples frequently use 58888.
- **Database mirroring endpoint**: Any available port. Examples frequently use 5022.

The firewall ports need to be open on both SQL Server VMs. The method of opening the ports depends on the firewall solution that you use. The following steps show how to open the ports in Windows Firewall:

1. On the first SQL Server **Start** screen, open **Windows Firewall with Advanced Security**.
1. On the left pane, select **Inbound Rules**. On the right pane, select **New Rule**.
1. For **Rule Type**, select **Port**.
1. For the port, specify **TCP** and enter the appropriate port numbers. The following screenshot shows an example:

   :::image type="content" source="./media/availability-group-manually-configure-prerequisites-tutorial-single-subnet/35-tcp-ports.png" alt-text="Screenshot of the New Inbound Rule Wizard for a firewall, showing specific local ports.":::

1. Select **Next**.
1. On the **Action** page, keep **Allow the connection** selected, and then select **Next**.
1. On the **Profile** page, accept the default settings, and then select **Next**.
1. On the **Name** page, specify a rule name (such as **Azure LB Probe**) in the **Name** box, and then select **Finish**.

## Next steps

Now that you've configured the prerequisites, get started with [configuring your availability group](availability-group-manually-configure-tutorial-single-subnet.md).

To learn more, see:

- [Windows Server failover cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Overview of Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)
