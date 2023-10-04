---
title: Configure license-free standby replica
description: Learn how to save on licensing costs by using a standby Azure SQL Managed Instance replica.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma
ms.date: 07/30/2023
ms.service: sql-managed-instance
ms.subservice: high-availability
ms.topic: how-to
---

# Configure a license-free standby replica for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes how you can save on licensing costs by designating your read-only secondary managed instance for stand-by when using Azure SQL Managed Instance.

## Overview

If you use a secondary Azure SQL Managed Instance deployment as a standby for disaster recovery and the secondary instance doesn't have any read workloads or applications connected to it, you can save on licensing costs by designating the replica as a *standby instance*.

When a secondary instance is designated as a standby instance, Microsoft provides you with the number of vCores that are licensed to the primary instance at no extra charge under the failover rights benefit in the [product licensing terms](https://www.microsoft.com/Licensing/product-licensing/sql-server). You're still billed for the compute and storage that the secondary instance uses.

Auto-failover groups for a SQL Managed Instance deployment support only one replica. The replica must be either a readable replica or be designated as a standby replica.


## Cost benefit

If you designate a managed instance replica as standby, Microsoft doesn't charge you SQL Server licensing costs for the vCores that the secondary standby replica uses. However, because the instance is billed for the entire hour, you might still be charged licensing costs for the entire hour if the state change is made in the middle of the hour. 

The benefit translates differently between customers who use the pay-as-you-go model and customers who use the [Azure Hybrid Benefit](../azure-hybrid-benefit.md) model. For a pay-as-you-go customer, the vCores are discounted on their invoice. For a customer who uses the Azure Hybrid Benefit for the standby replica, the number of vCores that the secondary replica uses are returned to their licensing pool.

For example, as a pay-as-you-go customer, if you have 16 vCores assigned to the secondary instance, a discount for 16 vCores appears on your invoice if you designate your secondary instance as standby only.

In another example, if you have 16 Azure Hybrid Benefit licenses and you deploy two managed instances that have 8 vCores each to a failover group, after you designate the secondary instance as standby, 8 vCores are returned to your license pool for you to use with other Azure SQL deployments.

## Functional capabilities

The following table describes the functional capabilities of a standby secondary managed instance:

|Functionality  |Description  |
|---------|---------|
|Limited read workloads     | After you designate your instance as standby, you can run only a limited number of read workloads on the secondary instance, such as Dynamic Management Views (DMVs), backups, and Database Console Commands (DBCC) queries.      |
|Planned failover | All planned failover scenarios, including recovery drills, relocating databases to different regions, and returning databases to the primary, are supported by the standby replica. When the secondary switches to the primary, it can serve read and write queries. The new secondary (the original primary) becomes the standby replica and shouldn't be used for read workloads.     |
|Unplanned failover | During an unplanned failover, after the secondary switches to the primary role, it can serve read and write queries. After the outage is mitigated and the original primary reconnects, it becomes the new secondary standby replica and shouldn't be used for read workloads.   |
|Backup and restore| The backup and restore behavior in a standby replica and a readable secondary managed instance are the same.         |
|Monitoring     | All monitoring operations that are supported by a readable secondary replica are supported by the standby replica.         |
|RPO and RTO | The standby replica provides the same Recovery Point Object (RPO) and Recovery Time Objective (RTO) as a readable secondary replica.          |
|Removing a failover group  | If the failover group is removed via a method like using the [Remove-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/remove-azsqldatabaseinstancefailovergroup) cmdlet, the standby replica becomes a read/write standalone instance. The licensing model returns to what it was before it was designated as standby (either Azure Hybrid Benefit or pay-as-you-go).  |

The secondary instance must be used only for disaster recovery. No production applications can be connected to the replica. The following lists the only activities that are permitted on the standby replica:

- Run backups
- Perform maintenance operations, such as checkDB
- Connect monitoring applications
- Run disaster recovery drills

## Configure a standby replica

You have two options to designate your secondary managed instance as standby:

- Designate it as standby when you create your auto-failover group.
- Update the configuration of an existing auto-failover group.

### New failover group

You can designate your secondary instance as a standby replica when you create a new failover group by using the Azure portal, Azure PowerShell, and the Azure CLI. 

### [Azure portal](#tab/azure-portal)

When you create a new failover group in the Azure portal, for **Failover rights**, select **On**. Check the box next to **I confirm that I will use the secondary instance as a standby replica**. Select **Create** to create your failover group.

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/new-failover-group.png" alt-text="Screenshot that shows creating a new failover group in the Azure portal, with the Failover rights option highlighted. ":::

For more information, see [Configure an auto-failover group](auto-failover-group-configure-sql-mi.md) or [Tutorial: Add SQL Managed Instance to a failover group](failover-group-add-instance-tutorial.md).

### [Azure PowerShell](#tab/azure-powershell)

You can use Azure PowerShell to create a new failover group, and designate your secondary instance as a standby replica. 

When you create a new failover group by using Azure PowerShell, use the:   
[New-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/new-azsqldatabaseinstancefailovergroup) command, and specify `standby` for the `SecondaryType` parameter. 

### [Azure CLI](#tab/azure-cli)

You can use the Azure CLI to create a new failover group, and designate your secondary instance as a standby replica. 

When you create a new failover group by using the Azure CLI, use the:   
[az sql instance-failover-group create](/cli/azure/sql/instance-failover-group#az-sql-instance-failover-group-create) command, and specify `standby` for the `--secondary-type` parameter. 

---

### Existing failover group

You can use the Azure portal, Azure PowerShell, and the Azure CLI to update failover rights for an existing failover group. 

### [Azure portal](#tab/azure-portal)

To update the failover rights for an existing failover group by using the Azure portal, follow these steps: 

1. In the [Azure portal](https://portal.azure.com), go to your SQL Managed Instance resource.
1. In the left menu under **Data management**, select **Failover groups**.
1. In the command bar, select **Edit Configurations**.

   :::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/update-failover-group-configuration.png" alt-text="Screenshot that shows the Failover groups pane in the portal and Edit Configurations highlighted. ":::

1. In **Edit configurations** for your failover group, for **Failover rights**, select **On**. Select the **I confirm that I will use the secondary instance as a standby replica** checkbox.

   :::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/update-failover-rights-existing-instance.png" alt-text="Screenshot that shows the Failover groups pane in the portal and Failover rights highlighted." :::

1. Select **Apply** to save your new settings and close the configuration pane.

Alternatively, you can enable failover rights in **Compute + storage** for your *secondary* managed instance. To learn more, review [View licensing rights](#view-licensing-rights).

### [Azure PowerShell](#tab/azure-powershell)

You can use Azure PowerShell to update an existing failover group, and designate your secondary instance as a standby replica. 

When you update an existing failover group by using Azure PowerShell, use the:   
[Set-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/set-azsqldatabaseinstancefailovergroup) command, and specify `standby` for the `SecondaryType` parameter. 

### [Azure CLI](#tab/azure-cli)

You can use the Azure CLI to update an existing failover group, and designate your secondary instance as a standby replica. 

When you update an existing failover group by using the Azure CLI, use the:  
[az sql instance-failover-group update](/cli/azure/sql/instance-failover-group#az-sql-instance-failover-group-update) command, and specify `standby` for the `--secondary-type` parameter. 

---


## View licensing rights

You can check the licensing rights for an existing failover group by using the Azure portal, Azure PowerShell or the Azure CLI. 

### [Azure portal](#tab/azure-portal)

In the Azure portal, you can check the licensing for your secondary managed instance in two locations:

- **Failover groups** for your *primary managed instance*.
- **Compute + storage** for your *secondary managed instance*.

In **Failover groups**, be sure that **Failover rights status** is set to **ON** and that the license model for the secondary instance is **Failover rights currently activated**.

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/view-failover-group-settings.png" alt-text="Screenshot that shows the Failover groups page, with failover rights on and the license model highlighted." lightbox="media/auto-failover-group-standby-replica-how-to-configure/view-failover-group-settings.png":::

The default license model indicates the licensing model the instance reverts to if the failover group fails over and the current secondary instance becomes the new primary instance. You might incur charges upon failover, depending on the default license model.

In **Compute + storage** for your *secondary managed instance*, confirm that the **Failover rights** license is activated. Under **Cost summary**, view the failover discount you're currently receiving for that instance.

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/compute-storage.png" alt-text="Screenshot that shows the Compute and storage page, with failover rights highlighted." lightbox="media/auto-failover-group-standby-replica-how-to-configure/compute-storage.png":::

If failover rights aren't activated and you qualify for the benefit, you also see the following recommendation in **Overview** for either instance. To activate the benefit, select the recommendation to go to **Edit Configurations**.

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/failover-rights-notification.png" alt-text="Screenshot that shows the SQL Managed Instance overview pane, and recommendations showing failover rights aren't used." :::

### [Azure PowerShell](#tab/azure-powershell)

Use the Azure PowerShell [Get-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/get-azsqldatabaseinstancefailovergroup) command to determine failover rights for an existing failover group. 

A value of `Standby` for the `SecondaryType` parameter indicates failover rights are activated, and you're currently not paying licensing costs for this secondary standby instance. 

### [Azure CLI](#tab/azure-cli)

Use the Azure CLI [az sql instance-failover-group show](/cli/azure/sql/instance-failover-group#az-sql-instance-failover-group-show) command to determine failover rights for an existing failover group. 

A value of `Standby` for the `secondaryType` parameter indicates failover rights are activated, and you're currently not paying licensing costs for this secondary standby instance. 

---

## Next steps

- For a detailed tutorial, see [Add SQL Managed Instance to a failover group](failover-group-add-instance-tutorial.md).
- For a sample script, see [Use PowerShell to create an auto-failover group in SQL Managed Instance](scripts/add-to-failover-group-powershell.md).
- For a business continuity overview and scenarios, see [Business continuity with Azure SQL Database and Azure SQL Managed Instance](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md).
- To learn about automated backups, see [SQL Database automated backups](../database/automated-backups-overview.md).
- To learn about using automated backups for recovery, see [Restore a database from service-initiated backups](../database/recovery-using-backups.md).
