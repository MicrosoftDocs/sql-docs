---
title: In-place change of SQL Server edition
description: Learn how to change the edition of your SQL Server virtual machine in Azure to downgrade to reduce cost or upgrade to enable more features.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 03/29/2023
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: how-to
tags: azure-resource-manager
---

# In-place change of SQL Server edition - SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article describes how to change the edition of SQL Server on a Windows virtual machine in Azure. 

The edition of SQL Server is determined by the product key, and is specified during the installation process using the installation media. The edition dictates what [features](/sql/sql-server/editions-and-components-of-sql-server-2017) are available in the SQL Server product. You can change the SQL Server edition with the installation media and either downgrade to reduce cost or upgrade to enable more features.

Once the edition of SQL Server has been changed internally to the SQL Server VM, you must then update the edition property of SQL Server in the Azure portal for billing purposes. 

## Prerequisites

To do an in-place change of the edition of SQL Server, you need the following: 

- An [Azure subscription](https://azure.microsoft.com/free/).
- A [SQL Server VM on Windows](./create-sql-vm-portal.md) registered with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md).
- Setup media with the **desired edition** of SQL Server. Customers who have [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) can obtain their installation media from the [Volume Licensing Center](https://www.microsoft.com/Licensing/servicecenter/default.aspx). Customers who don't have Software Assurance can deploy an Azure Marketplace SQL Server VM image with the desired edition of SQL Server and then copy the setup media (typically located in `C:\SQLServerFull`) from it to their target SQL Server VM. 

## Delete the extension 

Before you modify the edition of SQL Server, be sure to [delete the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md#delete-the-extension) from the SQL Server VM. You can do so with the Azure portal, PowerShell, or the Azure CLI. 

To delete the extension from your SQL Server VM with Azure PowerShell, use the following sample command:

```powershell-interactive
Remove-AzSqlVM -ResourceGroupName <resource_group_name> -Name <SQL VM resource name>
```

## Upgrade an edition

> [!WARNING]
> Upgrading the edition of SQL Server will restart the service for SQL Server, along with any associated services, such as Analysis Services and R Services. 

To upgrade the edition of SQL Server, obtain the SQL Server setup media for the desired edition of SQL Server, and then do the following:

1. Open Setup.exe from the SQL Server installation media. 
1. Go to **Maintenance** and choose the **Edition Upgrade** option. 

   ![Selection for upgrading the edition of SQL Server](./media/change-sql-server-edition/edition-upgrade.png)

1. Select **Next** until you reach the **Ready to upgrade edition** page, and then select **Upgrade**. The setup window might stop responding for a few minutes while the change is taking effect. A **Complete** page confirms that your edition upgrade is finished. 
1. After the SQL Server edition is upgraded, modify the edition property of the SQL Server virtual machine in the Azure portal. This updates the metadata and billing associated with this VM.

After you change the edition of SQL Server, register your SQL Server VM with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md) again so that you can use the Azure portal to view the edition of SQL Server. Then be sure to [Change the edition of SQL Server in the Azure portal](#change-edition-property-for-billing). 

## Downgrade an edition

To downgrade the edition of SQL Server, you need to completely uninstall SQL Server, and reinstall it again with the desired edition setup media. You can get the setup media by deploying a SQL Server VM from the marketplace image with your desired edition, and then copying the setup media to the target SQL Server VM, or using the [Volume Licensing Center](https://www.microsoft.com/Licensing/servicecenter/default.aspx) if you have software assurance. 

> [!WARNING]
> Uninstalling SQL Server might incur additional downtime. 

You can downgrade the edition of SQL Server by following these steps:

1. Back up all databases, including the system databases. 
1. Move system databases (master, model, and msdb) to a new location. 
1. Completely uninstall SQL Server and all associated services. 
1. Restart the virtual machine. 
1. Install SQL Server by using the media with the desired edition of SQL Server.
1. Install the latest service packs and cumulative updates.  
1. Replace the new system databases that were created during installation with the system databases that you previously moved to a different location. 
1. After the SQL Server edition is downgraded, modify the edition property of the SQL Server virtual machine in the Azure portal. This updates the metadata and billing associated with this VM. 

After you change the edition of SQL Server, register your SQL Server VM with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md) again so that you can use the Azure portal to view the edition of SQL Server. Then be sure to [Change the edition of SQL Server in the Azure portal](#change-edition-property-for-billing). 

## Register with the extension

After you've successfully changed the edition of SQL Server, you must register your SQL Server VM with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md#register-with-extension) again to manage it from the Azure portal. 

Register a SQL Server VM with Azure PowerShell:

```powershell-interactive
# Get the existing Compute VM
$vm = Get-AzVM -Name <vm_name> -ResourceGroupName <resource_group_name>

New-AzSqlVM -Name $vm.Name -ResourceGroupName $vm.ResourceGroupName -Location $vm.Location `
-LicenseType <license_type>
```

## Change edition property for billing

Once you've modified the edition of SQL Server using the installation media, and you've registered your SQL Server VM with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md#register-with-extension), you can then use the Azure portal or the Azure CLI to modify the edition property of the SQL Server VM for billing purposes. 

### [Portal](#tab/azure-portal)

To change the edition property of the SQL Server VM for billing purposes by using the Azure portal, follow these steps: 

1. Sign in to the [Azure portal](https://portal.azure.com). 
1. Go to your SQL Server virtual machine resource. 
1. Under **Settings**, select **Configure**. Then select your desired edition of SQL Server from the drop-down list under **Edition**. 

   ![Change edition metadata](./media/change-sql-server-edition/edition-change-in-portal.png)

1. Review the warning that says you must change the SQL Server edition first, and that the edition property must match the SQL Server edition. 
1. Select **Apply** to apply your edition metadata changes. 

### [Azure CLI](#tab/azure-cli)

To change the edition property of the SQL Server VM for billing purposes by using the Azure CLI, run this sample command: 

```azure-cli
az sql vm update -n <vm name> -g <resource group> --image-sku <edition> 
```

The `image-sku` parameter accepts the following editions: Developer, Express, Standard, Enterprise, Web. When using Web, Express, and Developer, the license-type must be pay-as-you-go (PAYG). 


### [PowerShell](#tab/azure-powershell)

To change the edition property of the SQL Server VM for billing purposes by using PowerShell, run this sample command: 

```azure-powershell
Update-AzSqlVM -ResourceGroupName <resource group> -Name <vm name> -Sku <edition> 
```

The `Sku` parameter accepts the following editions: Developer, Express, Standard, Enterprise, Web. When using Web, Express, and Developer, the license-type must be pay-as-you-go (PAYG). 

---

## Remarks

- The edition property for the SQL Server VM must match the edition of the SQL Server instance installed for all SQL Server virtual machines, including both pay-as-you-go and bring-your-own-license types of licenses.
- If you drop your SQL Server VM resource, you'll go back to the hard-coded edition setting of the image.
- The ability to change the edition is a feature of the SQL IaaS Agent extension. Deploying an Azure Marketplace image through the Azure portal automatically registers a SQL Server VM with the SQL IaaS Agent extension. However, customers who are self-installing SQL Server need to manually [register their SQL Server VM](sql-agent-extension-manually-register-single-vm.md).
- Adding a SQL Server VM to an availability set requires re-creating the VM. Any VMs added to an availability set go back to the default edition, and the edition needs to be modified again.

## Related content

- [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [SQL Server Licensing Resources and Documents](https://www.microsoft.com/licensing/docs/view/SQL-Server)
- [Pricing guidance for SQL Server on Windows VMs](pricing-guidance.md)
- [What's new for SQL Server on Azure VMs](doc-changes-updates-release-notes-whats-new.md)
