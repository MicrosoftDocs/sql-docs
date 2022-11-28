---
title: Configure license-free standby replica (preview)
description: Learn how to save on license costs with a standby Azure SQL Managed Instance replica. 
author: MladjoA
ms.author: mlandzic
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: high-availability
ms.topic: how-to
ms.custom: 
---

# Configure a license-free standby replica for Azure SQL Managed Instance (preview)

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

In this article, you learn how to configure a standby replica for Azure SQL Managed Instance to save on licensing costs.

The feature to designate your instance as standby is currently in preview.

If you use your secondary SQL Managed Instance deployment as a standby for disaster recovery and it doesn't have any read-workloads or applications connected to it, you can save on licensing costs by designating the replica as standby. When a secondary instance is designated as a standby instance, Microsoft provides you with many vCores that matches the number of vCores licensed to the primary instance at no extra charge under the failover rights benefit provided by the [product licensing terms](https://www.microsoft.com/Licensing/product-licensing/sql-server). You're still billed for the compute and storage the secondary instance uses.

Auto-failover groups for a SQL Managed Instance deployment support only one replica. The replica must be either a readable replica or be designated as a standby replica.

## Cost breakdown

For replicas that are designated as standby, Microsoft doesn't charge you SQL Server licensing costs for the vCores the secondary standby replica uses. The instance is billed for the entire hour, even if the state is changed in the middle of the hour.

The benefit translates differently between customers who use the pay-as-you-go model and customers who use the [Azure Hybrid Benefit](../azure-hybrid-benefit.md) model. Pay-as-you-go customers see the vCores discounted on their invoice. For customers who use the Azure Hybrid Benefit for the standby replica, the number of vCores that the secondary replica uses are returned to their licensing pool.

For example, as a pay-as-you-go customer, if you have 16 vCores assigned to the secondary instance, you'll see a discount for 16 vCores on your invoice when you designate your secondary instance as standby only. If you have 16 Azure Hybrid Benefit licenses and you deploy two managed instances that have 8 vCores each to a failover group, after you designate the secondary instance as standby, 8 vCores are returned to your license pool for you to use with other Azure SQL deployments.

## Functional capabilities

The following table describes the functional capabilities of a standby secondary managed instance:

|Functionality  |Description  |
|---------|---------|
|Limited read-workloads     | After you designate your instance as standby, you can run only a limited number of read-workloads on the secondary instance, such as Dynamic Management Views, backups, and Database Console Commands commands.      |
|Planned failover | All planned failover scenarios, including recovery drills, relocating databases to different regions, and returning databases to the primary, are supported by the standby replica. When the secondary switches to the primary, it can serve read and write queries. The original primary and new secondary becomes the standby replica and shouldn't be used for read workloads.     |
|Unplanned failover | During an unplanned failover, after the secondary switches to the primary role, it can serve read and write queries. After the outage is mitigated and the original primary reconnects, it becomes the new secondary standby replica and shouldn't be used for read workloads.   |
|Backup and restore| The backup and restore behavior in a standby replica and a readable secondary managed instance are the same.         |
|Monitoring     | All monitoring operations that are supported by a readable secondary replica are supported by the standby replica.         |
|Recovery Point Object (RPO) and Recovery Time Objective (RTO) | The standby replica provides the same RPO and RTO as a readable secondary replica.          |
|Removing a failover group  | If the failover group is removed via a method like using the [Remove-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/remove-azsqldatabaseinstancefailovergroup) cmdlet, the standby replica becomes a read/write standalone instance. The licensing model returns to what it was before it was designated for standby (either Azure Hybrid Benefit or pay-as-you-go).  |

The secondary instance must be used only for disaster recovery. No production applications can be connected to the replica. The following lists the only activities that are permitted on the standby replica:

- Run backups
- Perform maintenance operations, such as checkDB
- Connect monitoring applications
- Run disaster recovery drills

## Configure standby replica

You can designate your secondary instance as standby when you create your auto-failover group, or update the configuration for an existing auto-failover group by using the Azure portal.

### New failover group

You can designate your secondary instance as a standby replica when you create a new failover group by using the Azure portal.

When you're creating your failover group in the Azure portal, set **Failover rights** to *On*, and then select the **I confirm that I will use the secondary instance as a standby replica** checkbox. Select **Create** to create your failover group.

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/new-failover-group.png" alt-text="Screenshot of the Azure portal, creating a new failover group page, with the Failover rights option highlighted. ":::

For instructions to create a new failover group, review the [how to guide](auto-failover-group-configure-sql-mi.md) or the detailed [tutorial](failover-group-add-instance-tutorial.md).

### Existing failover group

To update the failover rights for an existing failover group by using the Azure portal:

1. In the [Azure portal](https://portal.azure.com), go to your SQL Managed Instance resource.
1. In the left menu under **Data management**, select **Failover groups**.
1. In the command bar, select **Edit configurations**.

   :::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/update-failover-group-configuration.png" alt-text="Screenshot of the Azure portal, Failover groups page, selecting Edit Configurations. ":::

1. In **Edit configuration** for your failover group, set **Failover rights** to **On** and select the **I confirm that I will use the secondary instance as a standby replica** option.

   :::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/update-failover-rights-existing-instance.png" alt-text="Screenshot of the Azure portal, edit failover group configuration, with failover rights highlighted." :::

1. Select **Apply** to save your new settings and close the configuration pane.

Alternatively, you can enable failover rights in **Compute + storage** for your *secondary* managed instance. To learn more, see [View licensing rights](#view-licensing-rights).

## View licensing rights

In the Azure portal, you can check the licensing for your secondary managed instance in two locations.

In **Failover groups**, be sure that **Failover rights status** is **ON** and that the license model for the secondary instance is **Failover rights currently activated**.

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/view-failover-group-settings.png" alt-text="Screenshot of the Azure portal, Failover groups page, with failover rights, and license model highlighted." lightbox="media/auto-failover-group-standby-replica-how-to-configure/view-failover-group-settings.png":::

The default license model indicates the licensing model the instance will revert to if the failover group fails over and the current secondary instance becomes the new primary instance. You might incur charges upon failover, depending on the default license model.

In **Compute + storage** for your *secondary managed instance*, confirm that the **Failover rights** license is activated. Under **Cost summary**, view the failover discount you're currently receiving for that instance.

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/compute-storage.png" alt-text="Screenshot of the Azure portal, Compute and storage page, with failover rights highlighted." lightbox="media/auto-failover-group-standby-replica-how-to-configure/compute-storage.png":::

If you don't have failover rights activated and you qualify for the benefit, you also see the following recommendation in **Overview** for either instance. To activate the benefit, select the recommendation to go to **Edit configurations**:

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/failover-rights-notification.png" alt-text="Screenshot of the Azure portal, SQL Managed Instance overview pane, and recommendations showing failover rights aren't used." :::

## Next steps

- For a detailed tutorial, see [Add a SQL Managed Instance to a failover group](failover-group-add-instance-tutorial.md).
- For a sample script, see [Use PowerShell to create an auto-failover group on a SQL Managed Instance](scripts/add-to-failover-group-powershell.md).
- For a business continuity overview and scenarios, see [Business continuity overview](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md).
- To learn about automated backups, see [SQL Database automated backups](../database/automated-backups-overview.md).
- To learn about using automated backups for recovery, see [Restore a database from the service-initiated backups](../database/recovery-using-backups.md).
