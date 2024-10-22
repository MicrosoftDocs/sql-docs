---
title: In-place change of SQL Server version
description: Learn how to change the version of your SQL Server virtual machine in Azure.
author: suresh-kandoth
ms.author: sureshka
ms.reviewer: sqlblt, daleche, mathoma, maghan
ms.date: 02/01/2024
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
tags: azure-resource-manager
---

# In-place change of SQL Server version - SQL Server on Azure VMs

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article describes how to change the version of Microsoft SQL Server on a Windows virtual machine (VM) in Microsoft Azure.

## Plan for a version upgrade

Consider the following prerequisites before upgrading your version of SQL Server:

1. Decide what version of SQL Server you want to upgrade to:

   - What's new in [SQL Server 2022](/sql/sql-server/what-s-new-in-sql-server-2022)
   - What's new in [SQL Server 2019](/sql/sql-server/what-s-new-in-sql-server-ver15)
   - What's new in [SQL Server 2017](/sql/sql-server/what-s-new-in-sql-server-2017)

1. We recommend that you check the [compatibility certification](/sql/database-engine/install-windows/compatibility-certification) for the version that you change to so that you can use the database compatibility modes to minimize the upgrade's effect.
1. You can review the following articles to help ensure a successful outcome:

   - [Video: Modernizing SQL Server | Pam Lahoud & Pedro Lopes | 20 Years of PASS](https://www.youtube.com/watch?v=5RPkuQHcxxs&feature=youtu.be)
   - [Database Experimentation Assistant for AB testing](/sql/dea/database-experimentation-assistant-overview)
   - [Upgrading Databases by using the Query Tuning Assistant](/sql/relational-databases/performance/upgrade-dbcompat-using-qta)
   - [Change the Database Compatibility Level and use the Query Store](/sql/database-engine/install-windows/change-the-database-compatibility-mode-and-use-the-query-store)

## Prerequisites

To do an in-place upgrade of SQL Server, you need the following:

- SQL Server installation media. Customers who have [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) can obtain their installation media from the [Volume Licensing Center](https://www.microsoft.com/Licensing/servicecenter/default.aspx). Customers who don't have Software Assurance can deploy an Azure Marketplace SQL Server VM image with the desired version of SQL Server and then copy the setup media (typically located in `C:\SQLServerFull`) from it to their target SQL Server VM.
- Version upgrades should follow the [support upgrade paths](/sql/database-engine/install-windows/supported-version-and-edition-upgrades-version-15).

## Delete the extension 

Before you modify the edition of SQL Server, be sure to [delete the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md#delete-the-extension) from the SQL Server VM. You can do so with the Azure portal, PowerShell or the Azure CLI. 

To delete the extension from your SQL Server VM with Azure PowerShell, use the following sample command:

```powershell-interactive
Remove-AzSqlVM -ResourceGroupName <resource_group_name> -Name <SQL VM resource name>
```

## Upgrade SQL Version

> [!WARNING]  
> Upgrading the version of SQL Server will restart the service for SQL Server in addition to any associated services, such as Analysis Services and R Services.

To upgrade the version of SQL Server, obtain the SQL Server setup media for the later version that would [support the upgrade path](/sql/database-engine/install-windows/supported-version-and-edition-upgrades-version-15) of SQL Server, and do the following steps:

1. Back up the databases, including the system (except `tempdb`) and user databases, before you start the process. You can also create an application-consistent VM-level backup by using Azure Backup Services.
1. Start Setup.exe from the SQL Server installation media.
1. The Installation Wizard starts the SQL Server Installation Center. To upgrade an existing instance of SQL Server, select **Installation** on the navigation pane, then select **Upgrade from an earlier version of SQL Server**.

   :::image type="content" source="media/change-sql-server-version/upgrade.png" alt-text="Selection for upgrading the version of SQL Server." lightbox="media/change-sql-server-version/upgrade.png":::

1. On the **Product Key** page, select an option to indicate whether you're upgrading to a free edition of SQL Server or you have a PID key for a production version of the product. For more information, see [Editions and supported features of SQL Server 2019 (15.x)](/sql/sql-server/editions-and-components-of-sql-server-version-15) and [Supported version and edition Upgrades (SQL Server 2016)](/sql/database-engine/install-windows/supported-version-and-edition-upgrades).
1. Select **Next** until you reach the **Ready to upgrade** page, and then select **Upgrade**. The setup window might stop responding for several minutes while the change is taking effect. A **Complete** page confirms that your upgrade is completed. For a step-by-step procedure to upgrade, see [the complete procedure](/sql/database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup#procedure).

   :::image type="content" source="media/change-sql-server-version/complete-page.png" alt-text="Complete page.":::

If you have changed the SQL Server edition in addition to changing the version, also update the edition, and refer to the **Verify Version and Edition in Portal** section to change the SQL VM instance.

   :::image type="content" source="media/change-sql-server-version/change-portal.png" alt-text="Change version metadata." lightbox="media/change-sql-server-version/change-portal.png":::

## Downgrade the version of SQL Server

To downgrade the version of SQL Server, you have to completely uninstall SQL Server, and reinstall it again by using the desired version. This is similar to a fresh installation of SQL Server because you can't restore the earlier database from a later version to the newly installed earlier version. The databases have to be re-created from scratch. If you also changed the edition of SQL Server during the upgrade, change the **Edition** property of the SQL Server VM in the Azure portal to the new edition value. This updates the metadata and billing that is associated with this VM.

> [!WARNING]  
> An in-place downgrade of SQL Server is not supported.

You can downgrade the version of SQL Server by following these steps:

1. Back up all databases, including system (except `tempdb`) and user databases.
1. Export all the necessary server-level objects (such as server triggers, roles, logins, linked servers, jobs, credentials, and certificates).
1. If you don't have scripts to re-create your user databases on the earlier version, you must script out all objects and export all data by using BCP.exe, SSIS, or DACPAC.

   Ensure you select the correct options when you script such items as the target version, dependent objects, and advanced options.

   :::image type="content" source="media/change-sql-server-version/scripting-options.png" alt-text="Scripting options." lightbox="media/change-sql-server-version/scripting-options.png":::

1. Completely uninstall SQL Server and all associated services.
1. Restart the VM.
1. Install SQL Server by using the media for the desired version of the program.
1. Install the latest service packs and cumulative updates.
1. Import all the necessary server-level objects (that were exported in Step 3).
1. Re-create all the necessary user databases from scratch (by using created scripts or the files from Step 4).

## Register with the extension

After you've successfully changed the edition of SQL Server, you must register your SQL Server VM with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md#register-with-extension) again to manage it from the Azure portal. 

Register a SQL Server VM with Azure PowerShell:

```powershell-interactive
# Get the existing Compute VM
$vm = Get-AzVM -Name <vm_name> -ResourceGroupName <resource_group_name>

New-AzSqlVM -Name $vm.Name -ResourceGroupName $vm.ResourceGroupName -Location $vm.Location `
-LicenseType <license_type>
```

## Verify the version and edition in the portal

After you change the version of SQL Server, register your SQL Server VM with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md) again so that you can use the Azure portal to view the version of SQL Server. The listed version number should now reflect your SQL Server installation's newly upgraded version and edition.

:::image type="content" source="media/change-sql-server-version/verify-portal.png" alt-text="Verify version." lightbox="media/change-sql-server-version/verify-portal.png":::

## Remarks

- We recommend that you initiate backups/update statistics/rebuild indexes/check consistency after the upgrade is finished. You can also check the individual database compatibility levels to ensure they reflect your desired level.
- After SQL Server is updated on the VM, make sure that the **Edition** property of SQL Server in the Azure portal matches the installed edition number for billing.
- The ability to [change the edition](change-sql-server-edition.md#change-edition-property-for-billing) is a feature of the SQL IaaS Agent extension. Deploying an Azure Marketplace image through the Azure portal automatically registers a SQL Server VM with the extension. However, customers who self-install SQL Server have to manually [register their SQL Server VM](sql-agent-extension-manually-register-single-vm.md).
- If you drop your SQL Server VM resource, the hard-coded edition setting of the image is restored.

## Related content

- [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Windows VMs](pricing-guidance.md)
- [What's new for SQL Server on Azure VMs](doc-changes-updates-release-notes-whats-new.md)
