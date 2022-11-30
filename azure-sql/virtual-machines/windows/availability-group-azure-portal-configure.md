---
title: Configure a multiple-subnet availability group (preview) in the Azure portal
description: "Use the Azure portal to create SQL Server VMs in various subnets, a Windows failover cluster, an availability group, and an availability group listener."
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma, randolphwest
ms.date: 11/16/2022
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
tags: azure-resource-manager
---
# Use the Azure portal to configure a multiple-subnet availability group (preview) for SQL Server on Azure VMs

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!TIP]
> Eliminate the need for an Azure load balancer for your Always On availability group by creating your SQL Server virtual machines (VMs) in multiple subnets within the same Azure virtual network.

This article describes how to use the [Azure portal](https://portal.azure.com) to configure an availability group for SQL Server on Azure VMs within multiple subnets by creating:

- New virtual machines with SQL Server.
- A Windows failover cluster.
- An availability group.
- A listener. 

This deployment method supports SQL Server 2016 and later on Windows Server 2016 and later.

Deploying a multiple-subnet availability group through the portal provides an easy end-to-end experience for users. It configures the virtual machines by following the [best practices for high availability and disaster recovery (HADR)](hadr-cluster-best-practices.md).

The ability to create your availability group through the Azure portal is currently in preview.

Although this article uses the Azure portal to configure the availability group environment, you can also do so [manually](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md).

> [!NOTE]  
> It's possible to lift and shift your availability group solution to SQL Server on Azure VMs by using Azure Migrate. To learn more, see [Migrate an availability group](../../migration-guides/virtual-machines/sql-server-availability-group-to-sql-on-azure-vm.md).

## Prerequisites

To configure an Always On availability group by using the Azure portal, you must have the following prerequisites:

- An [Azure subscription](https://azure.microsoft.com/free/)

- A resource group

- A virtual network

- A domain controller VM in the same virtual network

- The following account permissions:

  - A domain admin user account that has **Create Computer Object** permissions in the domain. This user will create the cluster and availability group, and will install SQL Server. 
  
    For example, a domain admin account (account@domain.com) typically has sufficient permission. This account should also be part of the local administrator group on each VM to create the cluster.

  - A domain SQL Server service account to control SQL Server. This should be the same account for every SQL Server VM that you want to add to the availability group.

## <a id="select"></a> Choose an Azure Marketplace image

Use the Azure Marketplace to choose one of several preconfigured images from the virtual machine gallery:

1. In the Azure portal, on the left menu, select **Azure SQL**. If **Azure SQL** isn't in the list, select **All services**, type **Azure SQL** in the search box, and select the result.  

1. Select **+ Create** to open the **Select SQL deployment option** page.  

1. Under **SQL Virtual Machines**, select the **High availability** checkbox, and then select the **Image** drop-down. Enter the version of SQL Server that you're interested in (such as **2019**), and then choose a SQL Server image (such as **Free SQL Server License: SQL 2019 Developer on Windows Server 2019**). 

   After you select the **High availability** checkbox, the portal displays the supported SQL Server versions, starting with SQL Server 2016. 

   :::image type="content" source="./media/availability-group-az-portal-configure/select-sql-server-image.png" alt-text="Screenshot of the Azure portal that shows the page for selecting a SQL Server deployment option, with high availability selected. ":::

1. Select **Create**.

## Choose basic settings

On the **Basics** tab, select the subscription and resource group. Also, provide details for the SQL Server instances that you're creating for your availability group.

1. Choose the subscription and resource group from the dropdown lists that contain your domain controller and where you intend to deploy your availability group.

   :::image type="content" source="./media/availability-group-az-portal-configure/basics-subscription.png" alt-text="Screenshot of the Azure portal that shows boxes for specifying subscription and resource group.":::

1. Use the slider to select the number of virtual machines that you want to create for the availability group. The minimum is **2**, and the maximum is **9**. The virtual machine names are pre-populated, but you can edit them by selecting **Edit names**.

   :::image type="content" source="./media/availability-group-az-portal-configure/edit-vm-names.png" alt-text="Screenshot of the Azure portal that shows a slider for selecting the number of virtual machines, along with the option for editing names.":::

1. For **Region**, select a region. All VMs will be deployed to the same region.

1. For **Availability**, select either **Availability Zone** or **Availability Set**. For more information about availability options, see [Availability](/azure/virtual-machines/availability).

1. For **Security type**, select either **Standard**, or [Trusted launch](/azure/virtual-machines/trusted-launch). 

1. The **Image** area displays the chosen SQL Server VM image. Select **Configure VM generation** to choose the VM generation.

1. Select **See all sizes** for the size of the virtual machines. All created VMs will be the same size. For production workloads, see the recommended machine sizes and configuration in [Performance best practices for SQL Server on Azure VMs](./performance-guidelines-best-practices-vm-size.md).

1. Under **Virtual machine administrator account**, provide a username and password. The password must be at least 12 characters and meet the [defined complexity requirements](/azure/virtual-machines/windows/faq#what-are-the-password-requirements-when-creating-a-vm-). This account will be the administrator of the VM.  

1. Under **Licensing**, you have the option to enable [Azure Hybrid Benefit](/azure/virtual-machines/windows/hybrid-use-benefit-licensing) to use your own Windows Server license and save on licensing costs.

1. **SQL Server License**: If you're a Software Assurance customer, you can use the [Azure Hybrid Benefit](/azure/virtual-machines/windows/hybrid-use-benefit-licensing) to bring your own SQL Server license and save on resources. Select **Yes** to enable the Azure Hybrid Benefit, and then confirm that you have Software Assurance by selecting the checkbox. This option is grayed out if you selected one of the free SQL Server images, such as the developer edition. 

   :::image type="content" source="./media/availability-group-az-portal-configure/sql-server-license.png" alt-text="Screenshot of the Azure portal, basics tab of the Create Always On availability group for SQL Server on Azure Virtual Machines page, showing SQL Server License section.":::

1. Select **Next: Networking**.

## Choose network settings

On the **Networking** tab, configure your network options.

1. Select the **virtual network** from the dropdown. The list is pre-populated based on the previously chosen region and resource group on the **Basics** tab. The selected virtual network should contain the domain controller VM in it.

1. Under **NIC network security group**, select basic security group. Choosing the basic option allows you to select inbound ports for the SQL Server VM.  

1. Configure **Public inbound ports**, if needed, by selecting **Allow selected ports**, then use the drop-down to select the allowed common ports.

    :::image type="content" source="./media/availability-group-az-portal-configure/networking-basic.png" alt-text="Screenshot of the Azure portal, networking tab of the Create Always On availability group for SQL Server on Azure Virtual Machines page, showing NIC settings.":::

1. Each virtual machine you create has to be in its own subnet. Under **Create subnets**, select **Manage subnet configuration** to open the **Subnets** page for the virtual network and either create a subnet (**+Subnet**) for each virtual machine or validate that a subnet is available for each virtual machine you intend to create for the availability group. Once done, use the **X** to close the subnet management window, and navigate back to the availability group deployment page.

    :::image type="content" source="./media/availability-group-az-portal-configure/subnet-management.png" alt-text="Screenshot of the Azure portal, subnet management page for your virtual network. ":::

1. Choose a **Public IP SKU** type.  All machines will use this Public IP type.  

1. Use the drop-down to assign the subnet, Public IP, and Listener IP to each VM you're creating. If you're using a Windows Server 2016 image, you'll also need to assign the Cluster IP. When assigning a subnet to a virtual machine, the listener and cluster IP pre-populate with available IP addresses - place the cursor in the field if you want to edit the IP address.  Select **Create new** if you need to create a new IP address. 

    :::image type="content" source="./media/availability-group-az-portal-configure/assign-subnet-to-virtual-machine.png" alt-text="Screenshot of the Azure portal, subnet configuration page.":::

1. You can also choose to delete the newly created public IP address and NIC when you delete the VM. Check the box if you want your resources deleted with the VM. 

1. Select **Next: WSFC and Credentials**.

## Choose failover cluster settings

On the **WSFC and Credentials** tab, provide account information to configure and manage the Windows Server Failover Cluster and SQL Server. All the accounts need to already be present in the Active Directory of the domain controller virtual machine for the deployment to work. This deployment process doesn't create any accounts and will fail if you provide an invalid account. For more information about the required permissions, review [Configure cluster accounts in Active Directory](/windows-server/failover-clustering/configure-ad-accounts).

1. Under **Windows Server Failover Cluster details**, provide the name you want to use for the failover cluster.  

1. Select the **storage account** from the drop-down that you want to use for the cloud witness, or, if one doesn't exist, select **Create a new storage account**.

1. Under **Windows Active Directory domain details**, provide the following:

   - The **Domain join user name and password**: this account creates the Windows Server Failover Cluster name in Active Directory and joins the VMs to the domain. **This account must have Create Computer Objects permissions**.

   - **Domain FQDN (fully-qualified domain name)**: your domain, such as contoso.com.

   - If you're using the active directory Ou path, provide it in the **Ou path** field.

    :::image type="content" source="./media/availability-group-az-portal-configure/windows-ad-domain.png" alt-text="Screenshot of the Azure portal, WSFC and Credentials tab of the Create Always On availability group for SQL Server on Azure Virtual Machines page, showing Windows Active Directory Domain details.":::

1. Under SQL Server details, provide the domain-joined account you want to use to manage SQL Server on the VMs. You can choose to use the same user that created the cluster and joined the VMs to the domain by choosing **Same as domain join account** or you can select **Custom** and provide different account details to use with the SQL Server service account.  

    :::image type="content" source="./media/availability-group-az-portal-configure/sql-server-account-credentials.png" alt-text="Screenshot of the Azure portal, WSFC and Credentials tab of the Create Always On availability group for SQL Server on Azure Virtual Machines page, showing SQL Server service account information.":::

1. Select **Next: Disks**.

## Choose disk settings

On the **Disks** tab, configure your disk options for both the virtual machines and the SQL Server storage configuration.

1. Under **OS disk type**, select the type of disk you want for your OS from the drop-down. Premium is recommended for production systems but isn't available for a Basic VM. To use a Premium SSD, change the virtual machine size.

1. Select the **Encryption type** for the disks.

1. Under **Storage Configuration**, select **Change configuration** to open the **Configure storage** page and specify storage requirements. You can choose to leave the values at default, or you can manually change the storage topology to suit your IOPS needs. For more information, see [storage configuration](storage-configuration.md).

    :::image type="content" source="./media/availability-group-az-portal-configure/change-sql-server-disk-configuration.png " alt-text="Screenshot of the Azure portal, Disks tab of the Create Always On availability group for SQL Server on Azure Virtual Machines page, showing the Storage configuration with Change configuration highlighted.":::

1. Under **Data storage**, choose the location for your data drive, the disk type, and the number of disks. You can also select the checkbox to store your system databases on your data drive instead of the local C:\ drive.

    :::image type="content" source="./media/create-sql-vm-portal/storage-configuration-data-storage.png" alt-text="Screenshot of the Azure portal, storage configuration page, showing data storage configuration settings.":::

1. Under **Log storage**, you can choose to use the same drive as the data drive for your transaction log files, or you can choose to use a separate drive from the drop-down. You can also choose the name of the drive, the disk type, and the number of disks.

    :::image type="content" source="./media/create-sql-vm-portal/storage-configuration-log-storage.png " alt-text="Screenshot of the Azure portal, storage configuration page, showing log storage configuration settings.":::

1. Configure your tempdb database settings under **Tempdb storage**, such as the location of the database files, as well as the number of files, initial size, and autogrowth size in MB. Currently, the max number of tempdb files. Currently, during deployment, the max number of tempdb files is 8, but more files can be added after the SQL Server VM is deployed.

    :::image type="content" source="./media/create-sql-vm-portal/storage-configuration-tempdb-storage.png" alt-text="Screenshot of the Azure portal, storage configuration page, showing tempdb storage configuration settings.":::

1. Select **OK** to save your storage configuration settings.

1. Select **Next: SQL Server settings**.

## Choose SQL Server settings

On the **SQL Server settings** tab, configure specific settings and optimizations for SQL Server and the availability group.

1. Under **Availability group details** provide the following:

   1. The name of the availability group and the listener.

   1. Select the role, either Primary or Secondary, for each virtual machine to be created.

   1. Choose the availability group settings that best suit your business needs.  

    :::image type="content" source="./media/availability-group-az-portal-configure/availability-group-settings.png" alt-text="Screenshot of the Azure portal availability group deployment page, SQL Server settings tab, showing availability group details section. ":::

1. Under **Security & Networking**, choose **SQL connectivity** to access the SQL Server instance on the VMs. For more information about connectivity options, see [Connectivity](create-sql-vm-portal.md#connectivity).

1. If you require SQL Server Authentication, select **Enable** under **SQL Server Authentication** and provide the login name and password. This will be used across all the VMs being deployed. For more information about authentication options, see [Authentication](create-sql-vm-portal.md#authentication).

1. Select **Enable** if you need to use **Azure Key Vault integration** to store security secrets for encryption. Fill in the requested information once choosing **Enable**. See [Azure Key Vault Integration](create-sql-vm-portal.md#azure-key-vault-integration) for more information.

1. Select **Change SQL instance settings** to modify SQL Server configuration options, such as the server collation, max degree of parallelism (MAXDOP), SQL Server min and max memory limits, and whether you want to optimize for ad-hoc workloads.

    :::image type="content" source="./media/create-sql-vm-portal/sql-instance-settings.png" alt-text="Screenshot of the Azure portal availability group deployment page, showing SQL instance settings, with Change SQL instance settings highlighted.":::

1. You have the option to enable **Machine Learning Services**, if it suits your business needs.

1. Select **Review + Create**.

On the **Review + Create** tab:

- Review the summary.

- Select **Create** to create the SQL Servers, failover cluster, availability group and listener.

- If needed, you can **Download a template for automation**.

You can monitor the deployment from the Azure portal. The **Notifications** button at the top of the screen shows the basic status of the deployment.

Once the deployment completes, you can browse to the [SQL virtual machines resource](manage-sql-vm-portal.md) in the portal and,  under **Settings** select **High Availability** to monitor the health of the availability group. Select the arrow next to the name of your availability group to see a list of all replicas:

:::image type="content" source="./media/availability-group-az-portal-configure/unhealthy-availability-group.png" alt-text="Screenshot of the Azure portal, High Availability tab of the SQL VM resource, showing the health of the availability group, which is currently not healthy." lightbox="./media/availability-group-az-portal-configure/unhealthy-availability-group.png":::

> [!NOTE]
> Your **Synchronization health** on the **High Availability** page of the Azure portal will show as **Not healthy** until you add databases to your availability group.

## Configure a firewall

This deployment creates a firewall rule for the listener on port 5022, but it doesn't configure a firewall rule for the SQL Server VM port 1433. Once the virtual machines are created, you can configure any firewall rules. For more information, see [configure the firewall](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md#configure-the-firewall).

## Add a database to the availability group

Add your databases to your availability group after deployment completes. The below steps use SQL Server Management Studio (SSMS) but you can use [Transact-SQL or PowerShell](/sql/database-engine/availability-groups/windows/availability-group-add-a-database) as well.

To add databases to your availability group by using SQL Server Management Studio (SSMS), follow these steps:

1. Connect to one of your SQL Server VMs by using your preferred method, such as Remote Desktop Connection (RDP), with a domain account that is a member of the sysadmin fixed server role on all of the SQL Servers.

1. Open SQL Server Management Studio (SSMS).

1. Connect to your SQL Server instance.  

1. Expand **Always On High Availability** in **Object Explorer**.

1. Expand **Availability Groups**, right-click your availability group and choose to **Add database...**.

    :::image type="content" source="media/availability-group-az-portal-configure/add-database.png" alt-text="Screenshot of SSMS UI, with the name of the availability group selected, and Add Database highlighted from the selection menu.":::

1. Follow the prompts to select the database(s) you want to add to your availability group.  

1. Select **OK** to save your settings and add your database to the availability group.  

1. After the database is added, refresh **Object Explorer** to confirm the status of your database as `synchronized`.

After databases are added, you can check the status of your availability group in the Azure portal:

:::image type="content" source="media/availability-group-az-portal-configure/healthy-availability-group.png" alt-text="Screenshot of the Azure portal, High Availability  tab of the SQL VM resource, showing the health of the availability group, which is currently healthy.":::

## Modify the availability group

Once your availability group is deployed through the portal, all changes to the availability group need to be done manually. If you want to [remove a replica](/sql/database-engine/availability-groups/windows/remove-a-secondary-replica-from-an-availability-group-sql-server), you can do so through SQL Server Management Studio (SSMS) or Transact-SQL, and then delete the VM through the Azure portal. If you want to add a replica, you'll have to deploy the virtual machine manually to the resource group, join it to the domain, and [add the replica](/sql/database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server) as you normally would in a traditional on-premises environment. 

## Remove a cluster

Remove all of the SQL Server VMs from the cluster to destroy it, and then remove the cluster metadata from the SQL IaaS Agent extension. You can do so by using the latest version of the [Azure CLI](/cli/azure/install-azure-cli) or PowerShell.

# [Azure CLI](#tab/azure-cli)

First, remove all of the SQL Server VMs from the cluster. This will physically remove the nodes from the cluster, and destroy the cluster:

```azurecli-interactive
# Remove the VM from the cluster metadata
# example: az sql vm remove-from-group --name SQLVM2 --resource-group SQLVM-RG

az sql vm remove-from-group --name <VM1 name>  --resource-group <resource group name>
az sql vm remove-from-group --name <VM2 name>  --resource-group <resource group name>
```

If these are the only VMs in the cluster, then the cluster will be destroyed. If there are any other VMs in the cluster apart from the SQL Server VMs that were removed, the other VMs won't be removed and the cluster won't be destroyed.

Next, remove the cluster metadata from the SQL IaaS Agent extension:

```azurecli-interactive
# Remove the cluster from the SQL VM RP metadata
# example: az sql vm group delete --name Cluster --resource-group SQLVM-RG

az sql vm group delete --name <cluster name> --resource-group <resource group name>
```

# [PowerShell](#tab/azure-powershell)

First, remove all of the SQL Server VMs from the cluster. This will physically remove the nodes from the cluster, and destroy the cluster:

```powershell-interactive
# Remove the SQL VM from the cluster
# example: $sqlvm = Get-AzSqlVM -Name SQLVM3 -ResourceGroupName SQLVM-RG
#  $sqlvm. SqlVirtualMachineGroup = ""
#  Update-AzSqlVM -ResourceId $sqlvm -SqlVM $sqlvm

$sqlvm = Get-AzSqlVM -Name <VM Name> -ResourceGroupName <Resource Group Name>
   $sqlvm. SqlVirtualMachineGroup = ""
   
   Update-AzSqlVM -ResourceId $sqlvm -SqlVM $sqlvm
```

If these are the only VMs in the cluster, then the cluster will be destroyed. If there are any other VMs in the cluster apart from the SQL Server VMs that were removed, the other VMs won't be removed and the cluster won't be destroyed.

Next, remove the cluster metadata from the SQL IaaS Agent extension:

```powershell-interactive
# Remove the cluster metadata
# example: Remove-AzSqlVMGroup -ResourceGroupName "SQLVM-RG" -Name "Cluster"

Remove-AzSqlVMGroup -ResourceGroupName "<resource group name>" -Name "<cluster name>"
```

---

## Troubleshoot

If you run into issues, you can check the deployment history, and review the common errors as well as their resolutions.

### Check deployment history

Changes to the cluster and availability group via the portal are done through deployments. Deployment history can provide greater detail if there are issues with creating, or onboarding the cluster, or with creating the availability group.

To view the logs for the deployment, and check the deployment history, follow these steps:

1. Sign into the [Azure portal](https://portal.azure.com).
1. Navigate to your resource group.
1. Select **Deployments** under **Settings**.
1. Select the deployment of interest to learn more about the deployment.

   :::image type="content" source="media/availability-group-az-portal-configure/failed-deployment.png" alt-text="Screenshot of the Azure portal, Deployments page, with a failed availability group deployment highlighted. Select the deployment you're interested in learning more about." :::

If the deployment fails and you want to redeploy using the portal experience, then manual clean-up of the resources, including deleting VMs, removing entries in Active Directory and/or DNS will be necessary, as the UX portal deployment isn't idempotent. However, if you've deployed the using the template for automation, then clean-up of resources won't be necessary as the template is idempotent.

## Next steps

After the availability group is deployed, consider optimizing the [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md).

- [Windows Server Failover Cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Always On availability groups overview](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
