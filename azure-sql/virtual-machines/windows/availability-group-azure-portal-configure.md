---
title: Configure a multi-subnet availability group in the Azure portal
description: "Use the Azure portal to create SQL Server VMs in multiple subnets, a Windows failover cluster, an availability group, and an availability group listener for SQL Server on Azure VMs."
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: mathoma, randolphwest
ms.date: 06/18/2024
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
ms.custom: devx-track-azurepowershell, devx-track-azurecli
tags: azure-resource-manager
---
# Use the Azure portal to configure a multiple-subnet availability group for SQL Server on Azure VMs

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

[!INCLUDE[tip-for-multi-subnet-ag](../../includes/virtual-machines-ag-listener-multi-subnet.md)]

This article describes how to use the [Azure portal](https://portal.azure.com) to configure an availability group for SQL Server on Azure VMs in multiple subnets by creating:

- New virtual machines with SQL Server.
- A Windows failover cluster.
- An availability group.
- A listener.

> [!NOTE]
> This deployment method supports SQL Server 2016 and later on Windows Server 2016 and later.

Deploying a multiple-subnet availability group through the portal provides an easy end-to-end experience for users. It configures the virtual machines by following the [best practices for high availability and disaster recovery (HADR)](hadr-cluster-best-practices.md).

Although this article uses the Azure portal to configure the availability group environment, you can also do so [manually](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md).

> [!NOTE]  
> It's possible to lift and shift your availability group solution to SQL Server on Azure VMs by using Azure Migrate. To learn more, see [Migrate an availability group](../../migration-guides/virtual-machines/sql-server-availability-group-to-sql-on-azure-vm.md).

## Prerequisites

To configure an Always On availability group by using the Azure portal, you must have the following prerequisites:

- An [Azure subscription](https://azure.microsoft.com/free/)

- A resource group

- A virtual network with [custom DNS server IP address configured](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md?#configure-virtual-network-dns)

- A domain controller VM in the same virtual network

- The following [account permissions](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md?#configure-domain-accounts):

  - A domain user [account that has **Create Computer Object** permissions](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md?#grant-installation-account-permissions) in the domain. This user will create the cluster and availability group, and will install SQL Server.
  
    For example, a domain user account (`account@domain.com`) typically has sufficient permission. This account should also be part of the local administrator group on each VM to create the cluster.

  - A domain SQL Server service account to control SQL Server. This should be the same account for every SQL Server VM that you want to add to the availability group.


> [!NOTE]
> This tutorial assumes that both the DNS Server and domain controller are on the same virtual machine. If the DNS Server is configured on a different VM than the domain controller VM, latency when syncing objects could result in automated deployment issues.

## <a id="select"></a> Choose an Azure Marketplace image

Use Azure Marketplace to choose one of several preconfigured images from the gallery. There are two entry points to start an availability group deployment in the Azure portal: 

The first option is to search the Azure Marketplace for "SQL Server High Availability" and find the **SQL Server with High Availability** tile:

   :::image type="content" source="./media/availability-group-az-portal-configure/sql-server-ha-tile.png" alt-text="Screenshot of the Azure portal that shows the marketplace tile for SQL Server with High Availability.":::

Select **Create** on the tile to continue. 

The second option is to go to **Azure SQL** in the Azure portal. If **Azure SQL** isn't already in your menu list, select **All services**, type **Azure SQL** in the search box, and select the result.  

1. Select **+ Create** to open the **Select SQL deployment option** pane.  

1. Under **SQL virtual machines**, select the **High availability** checkbox. In the **Image** box, type the version of SQL Server that you're interested in (such as **2019**), and then choose a SQL Server image (such as **Free SQL Server License: SQL 2019 Developer on Windows Server 2019**). 

   After you select the **High availability** checkbox, the portal displays the supported SQL Server versions, starting with SQL Server 2016. 

   :::image type="content" source="./media/availability-group-az-portal-configure/select-sql-server-image.png" alt-text="Screenshot of the Azure portal that shows the page for selecting a SQL Server deployment option, with high availability selected. ":::

1. Select **Create**.

## Choose basic settings

On the **Basics** tab, select the subscription and resource group. Also, provide details for the SQL Server instances that you're creating for your availability group.

1. From the dropdown lists, choose the subscription and resource group that contains your domain controller and where you intend to deploy your availability group.

   :::image type="content" source="./media/availability-group-az-portal-configure/basics-subscription.png" alt-text="Screenshot of the Azure portal that shows boxes for specifying subscription and resource group.":::

1. Use the slider to select the number of virtual machines that you want to create for the availability group. The minimum is **2**, and the maximum is **9**. The virtual machine names are pre-populated, but you can edit them by selecting **Edit names**.

   :::image type="content" source="./media/availability-group-az-portal-configure/edit-vm-names.png" alt-text="Screenshot of the Azure portal that shows a slider for selecting the number of virtual machines, along with the option for editing names.":::

1. For **Region**, select a region. All VMs will be deployed to the same region.

1. For **Availability options**, select either **Availability Zone** or **Availability Set**. For more information about availability options, see [Availability](/azure/virtual-machines/availability). When choosing **Availability Zone**, each VM will be assigned to a zone in the region, but this can be customized later under the **Networking** options.

1. For **Security type**, select either **Standard** or [Trusted launch](/azure/virtual-machines/trusted-launch). 

1. In the **Image** list, select the image with the version of SQL Server and the operating system you want. Use the dropdown to change the image to deploy. Select **Configure VM generation** to choose the VM generation. If you selected **Trusted launch** for **Security type**, then the VM generation must be **Gen 2**. 

1. Select **See all sizes** for the size of the virtual machines. All created VMs will be the same size. For production workloads, see the recommended machine sizes and configuration in [Performance best practices for SQL Server on Azure VMs](./performance-guidelines-best-practices-vm-size.md).

1. Under **Virtual machine administrator account**, provide a username and password. The password must be at least 12 characters and meet the [defined complexity requirements](/azure/virtual-machines/windows/faq#what-are-the-password-requirements-when-creating-a-vm-). This account will be the administrator of the VM.  

1. Under **Licensing**, you have the option to enable [Azure Hybrid Benefit](/azure/virtual-machines/windows/hybrid-use-benefit-licensing) to bring your existing Windows Server license to Azure. This option is available only if you're a Software Assurance customer.

    Select **Yes** if you want to enable Azure Hybrid Benefit, and then confirm that you have Software Assurance by selecting the checkbox. This option is unavailable if you selected one of the free SQL Server images, such as the Developer edition. 

1. Under **SQL Server License**, you have the option to enable [Azure Hybrid Benefit](/azure/virtual-machines/windows/hybrid-use-benefit-licensing) to bring your own SQL Server license and save on licensing cost. This option is available only if you're a Software Assurance customer.

   Select **Yes** if you want to enable Azure Hybrid Benefit, and then confirm that you have Software Assurance by selecting the checkbox. This option is unavailable if you selected one of the free SQL Server images, such as the developer edition. 

   :::image type="content" source="./media/availability-group-az-portal-configure/sql-server-license.png" alt-text="Screenshot of the Azure portal that shows information about SQL Server licenses and Azure Hybrid Benefit.":::

1. Select **Next: Networking**.

## Choose network settings

On the **Networking** tab, configure your network options:

1. Select the virtual network from the dropdown list. The list is pre-populated based on the region and resource group that you previously chose on the **Basics** tab. The selected virtual network should contain the domain controller VM.

    > [!CAUTION]
    > If the domain controller does not exist in the selected virtual network, the deployment will fail.

1. Under **NIC network security group**, select either a basic security group or the advanced security group. Choosing the basic option allows you to select inbound ports for the SQL Server VM. Selecting the advanced option allows you to choose an existing network security group, or create a new one.

1. Configure **Public inbound ports**, if needed, by selecting **Allow selected ports**. Then use the dropdown list to select the allowed common ports.

1. Choose a **Public IP SKU** type. All machines will use this public IP type.  

1. Each virtual machine that you create has to be in its own subnet. 

   Under **Create subnets**, select **Manage subnet configuration** to open the **Subnets** pane for the virtual network. Then, either create a subnet (**+Subnet**) for each virtual machine or validate that a subnet is available for each virtual machine that you want to create for the availability group. 

   When you're done, use the **X** to close the subnet management pane and go back to the page for availability group deployment.

    :::image type="content" source="./media/availability-group-az-portal-configure/subnet-management.png" alt-text="Screenshot of the Azure portal that shows the subnet management pane for a virtual network.":::

1. Use the dropdown lists to assign the subnet, public IP address, and listener IP address to each VM that you're creating. If you're using a Windows Server 2016 image, you also need to assign the cluster IP address. If you selected **Availability Zone** under the **Basics** tab as your availability option, then each VM has been assigned to a different zone. If needed, reassign each VM to a zone, but note that the VMs cannot be placed in the same availability zone. 

   When you're assigning a subnet to a virtual machine, the listener and cluster boxes are pre-populated with available IP addresses. Place your cursor in the box if you want to edit the IP address. Select **Create new** if you need to create a new IP address. 

    :::image type="content" source="./media/availability-group-az-portal-configure/assign-subnet-to-virtual-machine.png" alt-text="Screenshot of the Azure portal that shows the page for configuring subnets and IP addresses.":::

1. If you want to delete the newly created public IP address and NIC when you delete the VM, select the checkbox. 

1. Select **Next: WSFC and Credentials**.

## Choose failover cluster settings

On the **WSFC and Credentials** tab, provide account information to configure and manage the Windows Server failover cluster and SQL Server. 

> [!CAUTION]
>For the deployment to work, all the accounts need to already be present in Active Directory for the domain controller VM. This deployment process doesn't create any accounts and will fail if you provide an invalid account. For more information about the required permissions, review [Configure cluster accounts in Active Directory](/windows-server/failover-clustering/configure-ad-accounts).

1. Under **Windows Server Failover Cluster details**, provide the name that you want to use for the failover cluster.  

1. From the dropdown list, select the storage account that you want to use for the cloud witness. If one doesn't exist, select **Create a new storage account**.

1. Under **Windows Active Directory Domain details**:

   - For **Domain join user name** and **Domain join password**, enter the credentials for the account that creates the Windows Server failover cluster name in Active Directory and joins the VMs to the domain. *This account must have Create Computer Objects permissions*.

   - For **Domain FQDN**, enter a fully qualified domain name, such as **contoso.com**.

    :::image type="content" source="./media/availability-group-az-portal-configure/windows-ad-domain.png" alt-text="Screenshot of the Azure portal that shows Windows Active Directory domain details.":::

1. Under **SQL Server details**, provide the details of the account for the SQL Server service. There are several options available to choose from:

    a. Use the same domain-joined account that creates the cluster and joins the VMs to the domain by choosing **Same as domain join account**. This is the same account as above.

    b. By selecting [Group Managed Service Account](/azure/active-directory-domain-services/create-gmsa) (GMSA) we will install all required tools and services on the VMs being created, including installing the ADDS tool, joining the VMs to the GMSA Security group, and installing the GMSA service. In order for the GMSA deployment to succeed, the domain-joined user must have, at least, write permission on the GMSA AD group.

    c. Or you can select **Custom** and provide different account details to use with the SQL Server service account.  

    :::image type="content" source="./media/availability-group-az-portal-configure/sql-server-account-credentials.png" alt-text="Screenshot of the Azure portal that shows information about a SQL Server service account.":::

1. Select **Next: Disks**.

## Choose disk settings

On the **Disks** tab, configure your disk options for both the virtual machines and the SQL Server storage configuration:

1. Under **OS disk type**, select the type of disk that you want for your operating system. We recommend Premium for production systems, but it isn't available for a Basic VM. To use a Premium SSD, change the virtual machine size.

1. Select an **Encryption type** value for the disks.

1. Under **Storage configuration**, select **Change configuration** to open the **Configure storage** pane and specify storage requirements. You can choose to leave the default values, or you can manually change the storage topology to suit your needs for input/output operations per second (IOPS). For more information, see [Configure storage for SQL Server VMs](storage-configuration.md).

    :::image type="content" source="./media/availability-group-az-portal-configure/change-sql-server-disk-configuration.png " alt-text="Screenshot of the Azure portal that shows the current storage configuration and the button for changing the configuration.":::

1. Under **Data storage**, choose the location for your data drive, the disk type, and the number of disks. You can also select the checkbox to store your system databases on your data drive instead of the local C drive.

    :::image type="content" source="./media/create-sql-vm-portal/storage-configuration-data-storage.png" alt-text="Screenshot of the Azure portal that shows configuration settings for data storage.":::

1. Under **Log storage**, you can choose to use the same drive as the data drive for your transaction log files, or you can select a separate drive from the dropdown list. You can also choose the name of the drive, the disk type, and the number of disks.

    :::image type="content" source="./media/create-sql-vm-portal/storage-configuration-log-storage.png " alt-text="Screenshot of the Azure portal that shows configuration settings for log storage.":::

1. Under **TempDb storage**, configure your `tempdb` database settings. Choices include the location of the database files, the number of files, initial size, and autogrowth size in megabytes.

    Currently, during deployment, the maximum number of `tempdb` files is eight. But you can add more files after the SQL Server VM is deployed.

    :::image type="content" source="./media/create-sql-vm-portal/storage-configuration-tempdb-storage.png" alt-text="Screenshot of the Azure portal that shows configuration settings for tempdb storage.":::

1. Select **OK** to save your storage configuration settings.

1. Select **Next: SQL Server settings**.

## Choose SQL Server settings

On the **SQL Server settings** tab, configure specific settings and optimizations for SQL Server and the availability group:

1. Under **Availability group details**:

   1. Provide the name of the availability group and the listener.

   1. Select the role, either **Primary** or **Secondary**, for each virtual machine to be created.

   1. Choose the availability group settings that best suit your business needs.  

   :::image type="content" source="./media/availability-group-az-portal-configure/availability-group-settings.png" alt-text="Screenshot of the Azure portal that shows availability group details. ":::

1. Under **Security & Networking**, select **SQL connectivity** to access the SQL Server instance on the VMs. For more information about connectivity options, see [Connectivity](create-sql-vm-portal.md#connectivity).

1. If you require SQL Server authentication, select **Enable** under **SQL Server Authentication** and provide the login name and password. These credentials will be used across all the VMs that you're deploying. For more information about authentication options, see [Authentication](create-sql-vm-portal.md#authentication).

1. For **Azure Key Vault integration**, select **Enable** if you want to use Azure Key Vault to store security secrets for encryption. Then, fill in the requested information. To learn more, see [Azure Key Vault integration](create-sql-vm-portal.md#azure-key-vault-integration).

1. Select **Change SQL instance settings** to modify SQL Server configuration options. These options include server collation, maximum degree of parallelism (MAXDOP), minimum and maximum memory, and whether you want to optimize for ad hoc workloads.

    :::image type="content" source="./media/create-sql-vm-portal/sql-instance-settings.png" alt-text="Screenshot of the Azure portal that shows SQL Server instance settings and the button for changing them.":::

## Choose Prerequisites Validation

In order for the deployment to be successful, there are [several prerequisite](availability-group-azure-portal-configure.md#prerequisites) that are required to be in place. To make it easier to validate that all permissions and requirements are correct, use the PowerShell prerequisite script that is available for download on this tab. 

The script will be pre-populated with the values provided in the previous steps. Run the PowerShell script as a domain user on the Domain Controller virtual machine or on a domain joined Windows Server VM. 

Once the script has been executed and the prerequisites have been validated, then select the confirmation checkbox. 

:::image type="content" source="./media/availability-group-az-portal-configure/prerequisites-validation.png" alt-text="Screenshot of the Azure portal that shows the prerequisites validation tab.":::

1. Select **Review + Create**.

1. On the **Review + Create** tab, review the summary. Then select **Create** to create the SQL Servers, failover cluster, availability group, and listener.

   If needed, you can select **Download a template for automation**.

You can monitor the deployment from the Azure portal. The **Notifications** button at the top of the screen shows the basic status of the deployment.

After the deployment finishes, you can browse to the [SQL virtual machines resource](manage-sql-vm-portal.md) in the portal. Under **Settings**, select **High Availability** to monitor the health of the availability group. Select the arrow next to the name of your availability group to see a list of all replicas.

:::image type="content" source="./media/availability-group-az-portal-configure/unhealthy-availability-group.png" alt-text="Screenshot of the Azure portal that shows the health of an availability group, which is currently not healthy." lightbox="./media/availability-group-az-portal-configure/unhealthy-availability-group.png":::

> [!NOTE]
> **Synchronization Health** on the **High Availability** page of the Azure portal shows **Not Healthy** until you add databases to your availability group.

## Add databases to the availability group

Add databases to your availability group after deployment finishes. The following steps use SQL Server Management Studio, but you can also use [Transact-SQL or PowerShell](/sql/database-engine/availability-groups/windows/availability-group-add-a-database).

1. Connect to one of your SQL Server VMs by using your preferred method, such as a remote desktop connection (RDP). Use a domain account that's a member of the **sysadmin** fixed server role on all of the SQL Server instances.

1. Open SQL Server Management Studio.

1. Connect to your SQL Server instance.  

1. In **Object Explorer**, expand **Always On High Availability**.

1. Expand **Availability Groups**, right-click your availability group, and then select **Add Database**.

    :::image type="content" source="media/availability-group-az-portal-configure/add-database.png" alt-text="Screenshot of SQL Server Management Studio that shows selections for adding a database to an availability group.":::

1. Follow the prompts to select the database that you want to add to your availability group.  

1. Select **OK** to save your settings and add the database.  

1. Refresh **Object Explorer** to confirm the status of your database as `synchronized`.

After you add databases, you can check your availability group in the Azure portal and confirm that the status is **Healthy**.

:::image type="content" source="media/availability-group-az-portal-configure/healthy-availability-group.png" alt-text="Screenshot of the Azure portal that shows the health of the availability group, which is currently healthy.":::

## Modify the availability group

After you deploy your availability group through the portal, all changes to the availability group need to be done manually. If you want to [remove a replica](/sql/database-engine/availability-groups/windows/remove-a-secondary-replica-from-an-availability-group-sql-server), you can do so through SQL Server Management Studio or Transact-SQL, and then delete the VM through the Azure portal. If you want to add a replica, you have to deploy the virtual machine manually to the resource group, join it to the domain, and [add the replica](/sql/database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server) as you normally would in a traditional on-premises environment. 

## Remove a cluster

You can remove a cluster by using the latest version of the [Azure CLI](/cli/azure/install-azure-cli) or PowerShell.

# [Azure CLI](#tab/azure-cli)

First, remove all of the SQL Server VMs from the cluster:

```azurecli-interactive
# Remove the VM from the cluster metadata
# example: az sql vm remove-from-group --name SQLVM2 --resource-group SQLVM-RG

az sql vm remove-from-group --name <VM1 name>  --resource-group <resource group name>
az sql vm remove-from-group --name <VM2 name>  --resource-group <resource group name>
```

If the SQL Server VMs that you removed were the only VMs in the cluster, then the cluster will be destroyed. If any other VMs remain in the cluster, those VMs won't be removed and the cluster won't be destroyed.

Next, remove the cluster metadata from the SQL IaaS Agent extension:

```azurecli-interactive
# Remove the cluster from the SQL VM RP metadata
# example: az sql vm group delete --name Cluster --resource-group SQLVM-RG

az sql vm group delete --name <cluster name> --resource-group <resource group name>
```

# [PowerShell](#tab/azure-powershell)

First, remove all of the SQL Server VMs from the cluster:

```powershell-interactive
# Remove the SQL VM from the cluster
# example: $sqlvm = Get-AzSqlVM -Name SQLVM3 -ResourceGroupName SQLVM-RG
#  $sqlvm. SqlVirtualMachineGroup = ""
#  Update-AzSqlVM -ResourceId $sqlvm -SqlVM $sqlvm

$sqlvm = Get-AzSqlVM -Name <VM Name> -ResourceGroupName <Resource Group Name>
   $sqlvm. SqlVirtualMachineGroup = ""
   
   Update-AzSqlVM -ResourceId $sqlvm -SqlVM $sqlvm
```

If the SQL Server VMs that you removed were the only VMs in the cluster, then the cluster will be destroyed. If any other VMs remain in the cluster, those VMs won't be removed and the cluster won't be destroyed.

Next, remove the cluster metadata from the SQL IaaS Agent extension:

```powershell-interactive
# Remove the cluster metadata
# example: Remove-AzSqlVMGroup -ResourceGroupName "SQLVM-RG" -Name "Cluster"

Remove-AzSqlVMGroup -ResourceGroupName "<resource group name>" -Name "<cluster name>"
```

---

## Troubleshoot

If you run into problems, you can check the deployment history and then review common errors and their resolutions.

Changes to the cluster and availability group via the portal happen through deployments. Deployment history can provide more detail if there are problems with creating or onboarding the cluster, or with creating the availability group.

To view the logs for the deployment and check the deployment history:

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Go to your resource group.
1. Under **Settings**, select **Deployments**.
1. Select the deployment of interest to learn more about it.

   :::image type="content" source="media/availability-group-az-portal-configure/failed-deployment.png" alt-text="Screenshot of the Azure portal that shows a failed availability group deployment in a list of deployments." :::

If the deployment fails and you want to redeploy by using the portal, you need to manually cleanup the resources because deployment through the portal isn't idempotent (repeatable). These clean-up tasks include deleting VMs and removing entries in Active Directory and/or DNS. However, if you use the Azure portal to create a template to deploy your availability group, and then use the template for automation, clean-up of resources isn't necessary because the template is idempotent.

## Next steps

After the availability group is deployed, consider optimizing the [HADR settings for SQL Server on Azure VMs](hadr-cluster-best-practices.md).

To learn more, see:

- [Windows Server failover cluster with SQL Server on Azure VMs](hadr-windows-server-failover-cluster-overview.md)
- [Always On availability groups with SQL Server on Azure VMs](availability-group-overview.md)
- [Overview of Always On availability groups](/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server)
