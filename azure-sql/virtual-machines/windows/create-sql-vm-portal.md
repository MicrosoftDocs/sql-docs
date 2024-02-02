---
title: Provision SQL Server on Azure VM (Azure portal)
description: This detailed guide explains available configuration options when deploying your SQL Server on Azure VM by using the Azure portal.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 10/16/2023
ms.service: virtual-machines-sql
ms.subservice: deployment
ms.topic: how-to
tags: azure-resource-manager
---
# Provision SQL Server on Azure VM (Azure portal)

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides a detailed description of the available configuration options when deploying your SQL Server on Azure Virtual Machines (VMs) by using the Azure portal. For a quick guide, see the [SQL Server VM quickstart](sql-vm-create-portal-quickstart.md) instead. 


## Prerequisites

An Azure subscription. Create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) to get started. 

## <a id="select"></a> Choose Marketplace image

Use the Azure Marketplace to choose one of several pre-configured images from the virtual machine gallery. 

The Developer edition is used in this article because it is a full-featured, free edition of SQL Server for development testing. You pay only for the cost of running the VM. However, you are free to choose any of the images to use in this walkthrough. For a description of available images, see the [SQL Server Windows Virtual Machines overview](sql-server-on-azure-vm-iaas-what-is-overview.md#licensing).

Licensing costs for SQL Server are incorporated into the per-second pricing of the VM you create and varies by edition and cores. However, SQL Server Developer edition is free for development and testing, not production. Also, SQL Express is free for lightweight workloads (less than 1 GB of memory, less than 10 GB of storage). You can also bring-your-own-license (BYOL) and pay only for the VM. Those image names are prefixed with {BYOL}. For more information on these options, see [Pricing guidance for SQL Server Azure VMs](pricing-guidance.md).

To choose an image, follow these steps: 

1. Select **Azure SQL** in the left-hand menu of the Azure portal. If **Azure SQL** is not in the list, select **All services**, then type *Azure SQL* in the search box. You can select the star next to **Azure SQL** to save it as a favorite to pin it to the left-hand navigation. 

1. Select **+ Create** to open the **Select SQL deployment option** page. Select the **Image** dropdown list and then type **2019** in the SQL Server image search box. Choose a SQL Server image, such as **Free SQL Server License: SQL 2019 on Windows Server 2019** from the dropdown list.  Choose **Show details** for additional information about the image. 

   :::image type="content" source="./media/create-sql-vm-portal/select-sql-vm-image-portal.png" alt-text="Screenshot from the Azure portal of the select a SQL VM image deployment option.":::

1. Select **Create**.

[!INCLUDE [appliesto-sqlvm](../../includes/virtual-machines-2008-end-of-support.md)]

## Basic settings

The **Basics** tab allows you to select the subscription, resource group, and instance details. 

Using a new resource group is helpful if you are just testing or learning about SQL Server deployments in Azure. After you finish with your test, delete the resource group to automatically delete the VM and all resources associated with that resource group. For more information about resource groups, see [Azure Resource Manager Overview](/azure/active-directory-b2c/overview).

On the **Basics** tab, provide the following information:

-  Under **Project Details**, make sure the correct subscription is selected.
-  In the **Resource group** section, either select an existing resource group from the list or choose **Create new** to create a new resource group. A resource group is a collection of related resources in Azure (virtual machines, storage accounts, virtual networks, etc.).

  :::image type="content" source="./media/create-sql-vm-portal/basics-project-details.png" alt-text="Screenshot from the Azure portal of the Create a virtual machine page, starting with the Subscription field.":::

-  Under **Instance details**:

    1. Enter a unique **Virtual machine name**.  
    1. Choose a location for your **Region**. 
    1. For the purpose of this guide, leave **Availability options** set to _No infrastructure redundancy required_. To find out more information about availability options, see [Availability](/azure/virtual-machines/availability). 
    1. In the **Image** list, select _Free SQL Server License: SQL Server 2019 Developer on Windows Server 2019_ if it's not already selected.
    1. Choose **Standard** for **Security type**. 
    1. Select **See all sizes** for the **Size** of the virtual machine and search for the **E4ds_v5** offering. This is one of the minimum recommended VM sizes for SQL Server on Azure VMs. If this is for testing purposes, be sure to clean up your resources once you're done with them to prevent any unexpected charges. For production workloads, see the recommended machine sizes and configuration in [Performance best practices for SQL Server in Azure Virtual Machines](./performance-guidelines-best-practices-vm-size.md).

    :::image type="content" source="./media/create-sql-vm-portal/basics-instance-details.png" alt-text="Screenshot from the Azure portal of instance details for a new SQL VM.":::

> [!IMPORTANT]
> The estimated monthly cost displayed on the **Choose a size** window does not include SQL Server licensing costs. This estimate is the cost of the VM alone. For the Express and Developer editions of SQL Server, this estimate is the total estimated cost. For other editions, see the [Windows Virtual Machines pricing page](https://azure.microsoft.com/pricing/details/virtual-machines/windows/) and select your target edition of SQL Server. Also see the [Pricing guidance for SQL Server Azure VMs](pricing-guidance.md) and [Sizes for virtual machines](/azure/virtual-machines/sizes?toc=%2fazure%2fvirtual-machines%2fwindows%2ftoc.json).

- Under **Administrator account**, provide a username and password. The password must be at least 12 characters long and meet the [defined complexity requirements](/azure/virtual-machines/windows/faq#what-are-the-password-requirements-when-creating-a-vm-).

   :::image type="content" source="./media/create-sql-vm-portal/basics-administrator-account.png" alt-text="Screenshot from the Azure portal of the Administrator account fields.":::

- Under **Inbound port rules**, choose **Allow selected ports** and then select **RDP (3389)** from the dropdown list.

   :::image type="content" source="./media/create-sql-vm-portal/basics-inbound-port-rules.png" alt-text="Screenshot from the Azure portal of inbound port rules.":::

You also have the option to enable the [Azure Hybrid Benefit](/azure/virtual-machines/windows/hybrid-use-benefit-licensing) to use your own SQL Server license and save on licensing cost. 


## Disks

On the **Disks** tab, configure your disk options. 

- Under **OS disk type**, select the type of disk you want for your OS from the dropdown list. Premium is recommended for production systems but is not available for a Basic VM. To use a Premium SSD, change the virtual machine size.
- Under **Advanced**, select **Yes** under use **Managed Disks**.

Microsoft recommends Managed Disks for SQL Server. Managed Disks handles storage behind the scenes. In addition, when virtual machines with Managed Disks are in the same availability set, Azure distributes the storage resources to provide appropriate redundancy. For more information, see [Azure Managed Disks Overview](/azure/virtual-machines/managed-disks-overview). For specifics about managed disks in an availability set, see [Use managed disks for VMs in availability set](/azure/virtual-machines/availability).

## Networking

On the **Networking** tab, configure your networking options.

- Create a new **virtual network** or use an existing virtual network for your SQL Server VM. Designate a **Subnet** as well.

- Under **NIC network security group**, select either a basic security group or the advanced security group. Choosing the basic option allows you to select inbound ports for the SQL Server VM which are the same values configured on the **Basic** tab. Selecting the advanced option allows you to choose an existing network security group, or create a new one.

- You can make other changes to network settings, or keep the default values.

## Management

On the **Management** tab, configure monitoring and auto-shutdown.

- Azure enables **Boot diagnostics** by default with the same storage account designated for the VM. On this tab, you can change these settings and enable **OS guest diagnostics**.
- You can also enable **System assigned managed identity** and **auto-shutdown** on this tab.

## SQL Server settings

On the **SQL Server settings** tab, configure specific settings and optimizations for SQL Server. You can configure the following settings for SQL Server:

- [Connectivity](#connectivity)
- [Authentication](#authentication)
- [Azure Key Vault integration](#azure-key-vault-integration)
- [Storage configuration](#storage-configuration)
- [SQL instance settings](#sql-instance-settings)
- [Automated patching](#automated-patching)
- [Automated backup](#automated-backup)
- [Machine Learning Services](#machine-learning-services)

### Connectivity

Under **SQL connectivity**, specify the type of access you want to the SQL Server instance on this VM. For the purposes of this walkthrough, select **Public (internet)** to allow connections to SQL Server from machines or services on the internet. With this option selected, Azure automatically configures the firewall and the network security group to allow traffic on the port selected.

> [!TIP]
> By default, SQL Server listens on a well-known port, **1433**. For increased security, change the port in the previous dialog to listen on a non-default port, such as 1401. If you change the port, you must connect using that port from any client tools, such as SQL Server Management Studio (SSMS).

:::image type="content" source="./media/create-sql-vm-portal/azure-sqlvm-security.png" alt-text="Screenshot from the Azure portal of SQL VM Security.":::

To connect to SQL Server via the internet, you also must enable SQL Server Authentication, which is described in the next section.

If you would prefer to not enable connections to the Database Engine via the internet, choose one of the following options:

- **Local (inside VM only)** to allow connections to SQL Server only from within the VM.
- **Private (within Virtual Network)** to allow connections to SQL Server from machines or services in the same virtual network.

In general, improve security by choosing the most restrictive connectivity that your scenario allows. But all the options are securable through network security group (NSG) rules and SQL/Windows Authentication. You can edit the NSG after the VM is created. For more information, see [Security Considerations for SQL Server in Azure Virtual Machines](security-considerations-best-practices.md).

### Authentication

If you require SQL Server Authentication, select **Enable** under **SQL Authentication** on the **SQL Server settings** tab.

:::image type="content" source="./media/create-sql-vm-portal/azure-sqlvm-authentication.png" alt-text="Screenshot from the Azure portal of the SQL Server authentication options enabled.":::

> [!NOTE]
> If you plan to access SQL Server over the internet (the Public connectivity option), you must enable SQL Authentication here. Public access to the SQL Server requires SQL Authentication.

If you enable SQL Server Authentication, specify a **Login name** and **Password**. This login name is configured as a SQL Server Authentication login and a member of the **sysadmin** fixed server role. For more information about Authentication Modes, see [Choose an Authentication Mode](/sql/relational-databases/security/choose-an-authentication-mode).

If you prefer not to enable SQL Server Authentication, you can use the local Administrator account on the VM to connect to the SQL Server instance.

### Azure Key Vault integration

To store security secrets in Azure for encryption, select **SQL Server settings**, and scroll down to  **Azure key vault integration**. Select **Enable** and fill in the requested information. 

:::image type="content" source="./media/create-sql-vm-portal/azure-sqlvm-akv.png" alt-text="Screenshot from the Azure portal of Azure Key Vault integration.":::

The following table lists the parameters required to configure Azure Key Vault (AKV) Integration.

| PARAMETER | DESCRIPTION | EXAMPLE |
| --- | --- | --- |
| **Key Vault URL** |The location of the key vault. |`https://contosokeyvault.vault.azure.net/` |
| **Principal name** |Microsoft Entra service principal name. This name is also referred to as the Client ID. |`fde2b411-33d5-4e11-af04eb07b669ccf2` |
| **Principal secret** |Microsoft Entra service principal secret. This secret is also referred to as the Client Secret. |`9VTJSQwzlFepD8XODnzy8n2V01Jd8dAjwm/azF1XDKM=` |
| **Credential name** |**Credential name**: AKV Integration creates a credential within SQL Server and allows the VM to access the key vault. Choose a name for this credential. |`mycred1` |

For more information, see [Configure Azure Key Vault Integration for SQL Server on Azure VMs](azure-key-vault-integration-configure.md).

### Storage configuration


On the **SQL Server settings** tab, under **Storage configuration**, select **Change configuration** to open the **Configure storage** page and specify storage requirements. You can choose to leave the values at default, or you can manually change the storage topology to suit your IOPS needs. For more information, see [storage configuration](storage-configuration.md). 

:::image type="content" source="./media/create-sql-vm-portal/sql-vm-storage-configuration-provisioning.png" alt-text="Screenshot that highlights where you can change the storage configuration.":::

Under **Data storage**, choose the location for your data drive, the disk type, and the number of disks. You can also select the checkbox to store your system databases on your data drive instead of the local C:\ drive. 

:::image type="content" source="./media/create-sql-vm-portal/storage-configuration-data-storage.png" alt-text="Screenshot that shows where you can configure the data files storage for your SQL VM.":::

Under **Log storage**, you can choose to use the same drive as the data drive for your transaction log files, or you can choose to use a separate drive from the dropdown list. You can also choose the name of the drive, the disk type, and the number of disks. 

:::image type="content" source="./media/create-sql-vm-portal/storage-configuration-log-storage.png" alt-text="Screenshot that shows where you can configure the transaction log storage for your SQL VM.":::

Configure your `tempdb` database settings under **TempDb storage**, such as the location of the database files, as well as the number of files, initial size, and autogrowth size in MB.

- Currently, during deployment, the max number of `tempdb` files is 8, but more files can be added after the SQL Server VM is deployed.
- If you configure the SQL Server instance `tempdb` on the D: local SSD volume as recommended, the SQL IaaS Agent extension will manage the folder and permissions needed upon re-provisioning. 

:::image type="content" source="./media/create-sql-vm-portal/storage-configuration-tempdb-storage.png" alt-text="Screenshot that shows where you can configure the tempdb storage for your SQL VM.":::

Select **OK** to save your storage configuration settings.

### SQL instance settings

Select **Change SQL instance settings** to modify SQL Server configuration options, such as the server collation, max degree of parallelism (MAXDOP), SQL Server min and max memory limits, and whether you want to enable the **optimize for ad hoc workloads** option. 

:::image type="content" source="./media/create-sql-vm-portal/sql-instance-settings.png" alt-text="Screenshot that shows where you can configure the SQL Server settings for your SQL VM instance.":::

### SQL Server license

If you're a Software Assurance customer, you can use the [Azure Hybrid Benefit](https://azure.microsoft.com/pricing/hybrid-benefit/) to bring your own SQL Server license and save on resources. Select **Yes** to enable the Azure Hybrid Benefit, and then confirm that you have Software Assurance by selecting the checkbox. 

:::image type="content" source="./media/create-sql-vm-portal/azure-sqlvm-license.png" alt-text="Screenshot from the Azure portal of the SQL VM License options.":::

If you chose a free license image, such as the developer edition, the **SQL Server license** option is grayed out. 

### Automated patching

**Automated patching** is enabled by default. [Automated Patching](automated-patching.md) allows Azure to automatically apply SQL Server and operating system security updates. Specify a day of the week, time, and duration for a maintenance window. Azure performs patching in this maintenance window. The maintenance window schedule uses the VM locale. If you do not want Azure to automatically patch SQL Server and the operating system, select **Disable**.  

:::image type="content" source="./media/create-sql-vm-portal/azure-sqlvm-automated-patching.png" alt-text="Screenshot from the Azure portal of SQL VM automated patching.":::

For improved patching management, which also includes Cumulative Updates, try the integrated [Azure Update Manager](../azure-update-manager-sql-vm.md) experience after your SQL Server VM finishes deployment. 

### Automated backup

Enable automatic database backups for all databases under **Automated backup**. Automated backup is disabled by default.

When you enable SQL automated backup, you can configure the following settings:

- Retention period for backups (up to 90 days)
- Storage account, and storage container, to use for backups
- Encryption option and password for backups
- Backup system databases
- Configure backup schedule

To encrypt the backup, select **Enable**. Then specify the **Password**. Azure creates a certificate to encrypt the backups and uses the specified password to protect that certificate. 

Choose **Select Storage Container** to specify the container where you want to store your backups. 

By default the schedule is set automatically, but you can create your own schedule by selecting **Manual**, which allows you to configure the backup frequency, backup time window, and the log backup frequency in minutes.

:::image type="content" source="./media/create-sql-vm-portal/automated-backup.png" alt-text="Screenshot from the Azure portal of SQL VM automated backups.":::

For more information, see [Automated Backup for SQL Server in Azure Virtual Machines](automated-backup-sql-2014.md).


### Machine Learning Services

You have the option to enable [Machine Learning Services](/sql/advanced-analytics/). This option lets you use machine learning with Python and R in SQL Server 2017. Select **Enable** on the **SQL Server Settings** window. Enabling this feature from the Azure portal after the SQL Server VM is deployed will trigger a restart of the SQL Server service. 


## Review + create

On the **Review + create** tab:
1. Review the summary.
1. Select **Create** to create the SQL Server, resource group, and resources specified for this VM.

You can monitor the deployment from the Azure portal. The **Notifications** button at the top of the screen shows basic status of the deployment.

> [!NOTE]
> An example of time for Azure to deploy a SQL Server VM: A test SQL Server VM provisioned to the East US region with default settings takes approximately 12 minutes to complete. You might experience faster or slower deployment times based on your region and selected settings.

## <a id="remotedesktop"></a> Open the VM with Remote Desktop

Use the following steps to connect to the SQL Server virtual machine with Remote Desktop Protocol (RDP):

[!INCLUDE [Connect to SQL Server VM with remote desktop](../../includes/virtual-machines-sql-server-remote-desktop-connect.md)]

After you connect to the SQL Server virtual machine, you can launch SQL Server Management Studio and connect with Windows Authentication using your local administrator credentials. If you enabled SQL Server Authentication, you can also connect with SQL Authentication using the SQL login and password you configured during provisioning.

Access to the machine enables you to directly change machine and SQL Server settings based on your requirements. For example, you could configure the firewall settings or change SQL Server configuration settings.

## <a id="connect"></a> Connect to SQL Server remotely

In this walkthrough, you selected **Public** access for the virtual machine and **SQL Server Authentication**. These settings automatically configured the virtual machine to allow SQL Server connections from any client over the internet (assuming they have the correct SQL login).

The following sections show how to connect over the internet to your SQL Server VM instance.

[!INCLUDE [Connect to SQL Server in a VM Resource Manager](../../includes/virtual-machines-sql-server-connection-steps-resource-manager.md)]

  > [!NOTE]
  > This example uses the common port 1433. However, this value will need to be modified if a different port (such as 1401) was specified during the deployment of the SQL Server VM. 

## Known Issues

### I am unable to change the SQL Binary files installation path

SQL Server images from Azure Marketplace install the SQL Server binaries to the C drive. It is not currently possible to change this during deployment. The only available workaround is to manually uninstall SQL Server from within the VM, then reinstall SQL Server and choose a different location for the binary files during the installation process. 

## Related content

- [SQL Server on Azure Virtual Machines](sql-server-on-azure-vm-iaas-what-is-overview.md) 
- [Frequently asked questions for SQL Server on Azure VMs](frequently-asked-questions-faq.yml).
- [Checklist: Best practices for SQL Server on Azure VMs](performance-guidelines-best-practices-checklist.md)
