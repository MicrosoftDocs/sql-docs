---
title: Automated Patching
description: This article explains the Automated Patching feature for SQL Server on Azure VMs.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma, randolphwest
ms.date: 07/31/2023
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: article
ms.custom: devx-track-azurepowershell
tags: azure-resource-manager
---
# Automated Patching for SQL Server on Azure virtual machines

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

Automated Patching establishes a maintenance window for an Azure virtual machine running SQL Server. Automated Updates can only be installed during this maintenance window. For SQL Server, this restriction ensures that system updates and any associated restarts occur at the best possible time for the database.

> [!IMPORTANT]  
> Only Windows and SQL Server updates marked as **Important** or **Critical** are installed. Other SQL Server updates, such as service packs and cumulative updates that are not marked as **Important** or **Critical**, must be installed manually.

## Prerequisites

To use Automated Patching, you need the following prerequisites:

- Automated Patching relies on the SQL Server IaaS Agent Extension. Current SQL virtual machine gallery images add this extension by default. For more information, review [SQL Server IaaS Agent Extension](sql-server-iaas-agent-extension-automate-management.md).
- [Install the latest Azure PowerShell commands](/powershell/azure/) if you plan to configure Automated Patching by using PowerShell.

Automated Patching is supported starting with SQL Server 2012 on Windows Server 2012.

Additionally, consider the following:

- There are also several other ways to enable automatic patching of Azure VMs, such as [Update Management](/azure/automation/update-management/overview) or [Automatic VM guest patching](/azure/virtual-machines/automatic-vm-guest-patching). Choose only one option to automatically update your VM as overlapping tools may lead to failed updates.
- If you want to receive [ESU updates](/sql/sql-server/end-of-support/sql-server-extended-security-updates) without using the automated patching feature, you can use the built-in Windows Update channel.
- For SQL Server VMs in different availability zones that participate in an Always On availability group, configure the automated patching schedule so that availability replicas in different availability zones aren't patched at the same time.

## Settings

The following table describes the options that can be configured for Automated Patching. The actual configuration steps vary depending on whether you use the Azure portal or Azure Windows PowerShell commands.

| Setting | Possible values | Description |
| --- | --- | --- |
| **Automated Patching** | Enable/Disable (Disabled) | Enables or disables Automated Patching for an Azure virtual machine. |
| **Maintenance schedule** | Everyday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday | The schedule for downloading and installing Windows, SQL Server, and Microsoft updates for your virtual machine. |
| **Maintenance start hour** | 0-24 | The local start time to update the virtual machine. |
| **Maintenance window duration** | 30-180 | The number of minutes permitted to complete the download and installation of updates. |
| **Patch Category** | Important | The category of Windows updates to download and install. |

## Configure in the Azure portal

You can use the Azure portal to configure Automated Patching during provisioning or for existing VMs.

### New VMs

Use the Azure portal to configure Automated Patching when you create a new SQL Server virtual machine in the Resource Manager deployment model.

On the **SQL Server settings** tab, select **Change configuration** under **Automated patching**. The following Azure portal screenshot shows the **SQL Automated Patching** blade.

:::image type="content" source="./media/automated-patching/azure-sql-arm-patching.png" alt-text="Screenshot of SQL Automated Patching in the Azure portal.":::

For more information, see [Provision a SQL Server virtual machine on Azure](create-sql-vm-portal.md).

### Exist VMs

For existing SQL Server virtual machines, open your [SQL virtual machines resource](manage-sql-vm-portal.md#access-the-resource) and select **Patching** under **Settings**.

:::image type="content" source="./media/automated-patching/azure-sql-rm-patching-existing-vms.png" alt-text="Screenshot of SQL Automatic Patching for existing VMs.":::

When you're finished, select the **OK** button on the bottom of the **SQL Server configuration** blade to save your changes.

If you're enabling Automated Patching for the first time, Azure configures the SQL Server IaaS Agent in the background. During this time, the Azure portal might not show that Automated Patching is configured. Wait several minutes for the agent to be installed and configured. After that the Azure portal reflects the new settings.

## Configure with PowerShell

After provisioning your SQL VM, use PowerShell to configure Automated Patching.

In the following example, PowerShell is used to configure Automated Patching on an existing SQL Server VM. The **New-AzVMSqlServerAutoPatchingConfig** command configures a new maintenance window for automatic updates.

```azurepowershell
$vmname = "vmname"
$resourcegroupname = "resourcegroupname"
$aps = New-AzVMSqlServerAutoPatchingConfig -Enable -DayOfWeek "Thursday" -MaintenanceWindowStartingHour 11 -MaintenanceWindowDuration 120  -PatchCategory "Important"
s
Set-AzVMSqlServerExtension -AutoPatchingSettings $aps -VMName $vmname -ResourceGroupName $resourcegroupname
```

Based on this example, the following table describes the practical effect on the target Azure VM:

| Parameter | Effect |
| --- | --- |
| **DayOfWeek** | Patches installed every Thursday. |
| **MaintenanceWindowStartingHour** | Begin updates at 11:00am. |
| **MaintenanceWindowsDuration** | Patches must be installed within 120 minutes. Based on the start time, they must complete by 1:00pm. |
| **PatchCategory** | The only possible setting for this parameter is **Important**. This installs Windows update marked Important; it doesn't install any SQL Server updates that aren't included in this category. |

It could take several minutes to install and configure the SQL Server IaaS Agent.

To disable Automated Patching, run the same script without the **-Enable** parameter to the **New-AzVMSqlServerAutoPatchingConfig**. The absence of the **-Enable** parameter signals the command to disable the feature.

## Next steps

For information about other available automation tasks, see [SQL Server IaaS Agent Extension](sql-server-iaas-agent-extension-automate-management.md).

For more information about running SQL Server on Azure VMs, see [SQL Server on Azure virtual machines overview](sql-server-on-azure-vm-iaas-what-is-overview.md).
