---
title: Create an FCI with Azure Elastic SAN (Preview)
description: "Use Azure Elastic SAN to create a failover cluster instance (FCI) with SQL Server on Azure Virtual Machines."
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: mathoma
ms.date: 08/05/2024
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
ms.custom:
  - na
  - devx-track-azurepowershell
editor: monicar
tags: azure-service-management
---

# Create an FCI with Azure Elastic SAN (Preview) - SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article explains how to create a failover cluster instance (FCI) by using an Azure Elastic SAN volume with SQL Server on Azure Virtual Machines (VMs).

To learn more, see an overview of [FCI with SQL Server on Azure VMs](failover-cluster-instance-overview.md) and [cluster best practices](hadr-cluster-best-practices.md).

> [!NOTE]
> Configuring your failover cluster instance with an Azure Elastic SAN is currently in preview for SQL Server on Azure VMs. 

## Prerequisites

Before you complete the instructions in this article, you should already have:

- An Azure subscription. Get started with a [free Azure account](https://azure.microsoft.com/free/).
- [Two or more prepared Azure Windows virtual machines](failover-cluster-instance-prepare-vm.md) in the same availability zone. Since all VMs part of the FCI have to be in the same availability zone, the VM availability is only 99.9%.
- An account that has permissions to create objects on both Azure virtual machines and in Active Directory.

[!INCLUDE[tip-for-multi-subnet-ag](../../includes/virtual-machines-fci-multi-subnet.md)]

## Create Azure Elastic SAN

Follow the instructions to [Create an Azure Elastic SAN](/azure/storage/elastic-san/elastic-san-create). 

Your Elastic SAN should be: 

- In the same resource group as your SQL Server on Azure VM. 
- Configured for zone redundancy. 
- In the same availability zone as the primary SQL Server VM. 

## Connect Elastic SAN volumes to the VMs

Follow the instructions to [Connect Elastic SAN volumes](/azure/storage/elastic-san/elastic-san-connect-windows) to both SQL Server VMs. 

Use [Disk Management](/windows-server/storage/disk-management/overview-of-disk-management) to format your Elastic SAN volume and bring it online. 


## Create Windows Failover Cluster

The steps to create your Windows Server Failover Cluster differ between single subnet and multi-subnet environments. To create your cluster, follow the steps in the tutorial for either a [multi-subnet scenario](availability-group-manually-configure-tutorial-multi-subnet.md#add-failover-cluster-feature) or a [single subnet scenario](availability-group-manually-configure-tutorial-single-subnet.md#create-the-cluster). Though these tutorials create an availability group, the steps to create the cluster are the same for a failover cluster instance. 

## Configure quorum

Since the disk witness is the most resilient quorum option, it's recommended to configure a disk witness as the quorum solution. Cloud witness isn't currently supported with Azure Elastic SAN. 

If you have an even number of votes in the cluster, configure the [quorum solution](hadr-cluster-quorum-configure-how-to.md) that best suits your business needs. For more information, see [Quorum with SQL Server VMs](hadr-windows-server-failover-cluster-overview.md#quorum).

## Validate cluster

Validate the cluster on one of the virtual machines by using the Failover Cluster Manager UI or PowerShell.

Before you validate the cluster, take the Elastic SAN volume offline by following these steps: 

1. Under **Server Manager**, select **Tools**, and then select **Failover Cluster Manager**.
1. Under the cluster, select **Disks** under **Storage**. 
1. Right-click the Elastic SAN disk, and then select **Take Offline**:

   :::image type="content" source="media/failover-cluster-instance-azure-elastic-san-manually-configure/take-cluster-disk-offline.png" alt-text="Screenshot of Failover Cluster Manager, with the disk selected and take offline highlighted. ":::

1. Select **Yes** on the **Offline Cluster Shared Volume** dialog box to confirm that you're sure, and want to take the disk offline. 

To validate the cluster by using the UI, follow these steps:

1. Right-click the cluster in **Failover Cluster Manager**, select **Validate Cluster** to open the **Validate a Configuration Wizard**.
1. On the **Validate a Configuration Wizard**, select **Next**.
1. On the **Select Servers or a Cluster** page, enter the names of both virtual machines.
1. On the **Testing options** page, select **Run all tests (recommended)** and then select **Next**.
1. On the **Confirmation** page, select **Next**.  The **Validate a Configuration** wizard runs the validation tests.

To validate the cluster by using PowerShell, run the following script from an administrator PowerShell session on one of the virtual machines:

```powershell
Test-Cluster –Node ("<node1>","<node2>") –Include "Cluster Configuration", "Inventory", "Network", "Storage", "System Configuration"
```

After your cluster has been validated, use the **Disks** page for your cluster in **Failover Cluster Manager** to bring your Elastic SAN volume back online.

## Test cluster failover

Test the failover of your cluster. In **Failover Cluster Manager**, right-click your cluster, select **More Actions** > **Move Core Cluster Resource** > **Select node**, and then select the other node of the cluster. Move the core cluster resource to every node of the cluster, and then move it back to the primary node. Ensure you can successfully move the cluster to each node before installing SQL Server.

:::image type="content" source="media/failover-cluster-instance-premium-file-share-manually-configure/test-cluster-failover.png" alt-text="Screenshot of testing cluster failover by moving the core resource to the other nodes.":::


## Create SQL Server FCI

After you've configured the failover cluster and all cluster components, including storage, you can create the SQL Server FCI.

### Create first node in the SQL FCI

To create the first node in the SQL Server FCI, follow these steps:

1. Connect to the first virtual machine by using Remote Desktop Protocol (RDP) or Bastion.

1. In **Failover Cluster Manager**, make sure that all core cluster resources are on the first virtual machine. If necessary, move the disks to that virtual machine.

1. Locate the installation media. If the virtual machine uses one of the Azure Marketplace images, the media is located at `C:\SQLServer_<version number>_Full`.

1. Select **Setup**.

1. In **SQL Server Installation Center**, select **Installation**.

1. Select **New SQL Server failover cluster installation**. Follow the instructions in the wizard to install the SQL Server FCI.

1. On the **Cluster Disk Selection** page, select the Azure Elastic SAN volume. 

1. On the **Cluster Network Configuration** page, the IP you provide varies depending on if your SQL Server VMs were deployed to a single subnet, or multiple subnets.

   1. For a **single subnet environment**, provide the IP address that you plan to add to the [Azure Load Balancer](failover-cluster-instance-vnn-azure-load-balancer-configure.md)
   1. For a **multi-subnet environment**, provide the secondary IP address in the subnet of the _first_ SQL Server VM that you previously designated as the [IP address of the failover cluster instance network name](failover-cluster-instance-prepare-vm.md#assign-secondary-ip-addresses):

   :::image type="content" source="./media/failover-cluster-instance-azure-shared-disk-manually-configure/sql-install-cluster-network-secondary-ip-vm-1.png" alt-text="Screenshot that provides the secondary IP address in the subnet of the first SQL Server VM.":::

1. On the **Database Engine Configuration** page, ensure the database directories are on the Azure Elastic SAN volume.

1. After you complete the instructions in the wizard, setup installs the SQL Server FCI on the first node.

### Add additional nodes the SQL FCI

To add an additional node to the SQL Server FCI, follow these steps:

1. After FCI installation succeeds on the first node, connect to the second node by using RDP or Bastion.

1. Open the **SQL Server Installation Center**, and then select **Installation**.

1. Select **Add node to a SQL Server failover cluster**. Follow the instructions in the wizard to install SQL Server and add the node to the FCI.

1. For a multi-subnet scenario, in **Cluster Network Configuration**, enter the secondary IP address in the subnet of the _second_ SQL Server VM subnet that you previously designated as the [IP address of the failover cluster instance network name](failover-cluster-instance-prepare-vm.md#assign-secondary-ip-addresses)

    :::image type="content" source="./media/failover-cluster-instance-azure-shared-disk-manually-configure/sql-install-cluster-network-secondary-ip-vm-2.png" alt-text="Screenshot that enters the secondary IP address in the subnet of the second SQL Server VM.":::

    After selecting **Next** in **Cluster Network Configuration**, setup shows a dialog box indicating that SQL Server Setup detected multiple subnets as in the example image.  Select **Yes** to confirm.

    :::image type="content" source="./media/failover-cluster-instance-azure-shared-disk-manually-configure/sql-install-multi-subnet-confirmation.png" alt-text="Screenshot that shows the multi-subnet confirmation. ":::

1. After you complete the instructions in the wizard, setup adds the second SQL Server FCI node.

1. Repeat these steps on any other SQL Server VMs you want to participate in the SQL Server failover cluster instance.

>[!NOTE]
> Azure Marketplace gallery images come with SQL Server Management Studio installed. If you didn't use a marketplace image [Download SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).

## Register with SQL IaaS Agent extension

To manage your SQL Server VM from the portal, register it with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md). Note that only [limited functionality](sql-server-iaas-agent-extension-automate-management.md#feature-benefits) will be available to SQL Server VMs that have failover clustered instances of SQL Server (FCIs).

If your SQL Server VM has already been registered with the SQL IaaS Agent extension and you've enabled any features that require the agent, you'll need to [unregister](sql-agent-extension-manually-register-single-vm.md#unregister-from-extension) the SQL Server VM from the extension and register it again after your FCI is installed.

Register a SQL Server VM with PowerShell (-LicenseType can be `PAYG` or `AHUB`):

```powershell-interactive
# Get the existing compute VM
$vm = Get-AzVM -Name <vm_name> -ResourceGroupName <resource_group_name>

# Register SQL VM with SQL IaaS Agent extension
New-AzSqlVM -Name $vm.Name -ResourceGroupName $vm.ResourceGroupName -Location $vm.Location `
   -LicenseType <license_type>
```

## Configure connectivity

If you deployed your SQL Server VMs in multiple subnets, skip this step. If you deployed your SQL Server VMs to a single subnet, then you'll need to configure an additional component to route traffic to your FCI. You can configure a virtual network name (VNN) with an Azure Load Balancer, or a distributed network name for a failover cluster instance. [Review the differences between the two](hadr-windows-server-failover-cluster-overview.md#virtual-network-name-vnn) and then deploy either a [distributed network name](failover-cluster-instance-distributed-network-name-dnn-configure.md) or a [virtual network name and Azure Load Balancer](failover-cluster-instance-vnn-azure-load-balancer-configure.md) for your failover cluster instance.  

## Limitations

- Azure virtual machines support Microsoft Distributed Transaction Coordinator (MSDTC) on Windows Server 2019 with storage on CSVs and a [standard load balancer](/azure/load-balancer/load-balancer-overview). MSDTC is not supported on Windows Server 2016 and earlier.
- SQL Server FCIs registered with the SQL IaaS Agent extension don't support features that require the agent, such as automated backup, patching, Microsoft Entra authentication and advanced portal management. See the [table of benefits](sql-server-iaas-agent-extension-automate-management.md#feature-benefits) for more information.

## Next steps

If Azure shared disks are not the appropriate FCI storage solution for you, consider creating your FCI using [premium file shares](failover-cluster-instance-premium-file-share-manually-configure.md) or [Storage Spaces Direct](failover-cluster-instance-storage-spaces-direct-manually-configure.md) instead.

To learn more, see:

- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Failover cluster instances with SQL Server on Azure VMs](failover-cluster-instance-overview.md)
- [Failover cluster instance overview](/sql/sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server)
- [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md)
