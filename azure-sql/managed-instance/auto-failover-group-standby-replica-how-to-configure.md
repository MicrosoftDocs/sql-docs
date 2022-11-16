---
title: Configure license-free standby replica (Preview)
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

# Configure license-free standby replica for Azure SQL Managed Instance (Preview)
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to configure a standby replica for Azure SQL Managed Instance to save on licensing costs. 

The ability to designate your instance as standby is currently in preview. 

## Overview

If your secondary SQL Managed Instance is used as a standby for disaster recovery (DR) and doesn't have any read-workloads or applications connected to it, you can save on licensing costs by designating the replica as **Standby**. When a secondary instance is designated as a standby instance, Microsoft provides you with a number of vCores that matches the number of vCores licensed to the primary instance, at no extra charge under the failover rights benefit provided by the [product licensing terms](https://www.microsoft.com/Licensing/product-licensing/sql-server). You're still billed for the compute and storage used by the secondary instance. 

Auto-failover groups for SQL Managed Instance support only one replica - the replica must either be a readable replica, or designated as a standby replica. 


## Cost breakdown

For replicas designated as standby, Microsoft does not charge you SQL Server licensing costs for the vCores used by the secondary standby replica. The instance is billed for the entire hour, even if the state is changed in the middle of the hour. 

The benefit translates differently between customers using the pay-as-you-go model vs. customers using the [Azure Hybrid Benefit (AHB)](../azure-hybrid-benefit.md). Pay-as-you-go customers see the vCores discounted from their invoice, while customers using the AHB for the standby replica have an equal number of vCores as the secondary replica uses returned to their licensing pool. 

For example, as a pay-as-you go customer, if you have 16 vCores assigned to the secondary instance, you'll see a discount for 16 vCores from your invoice when you designate your secondary instance as standby only. Alternatively, if you have 16 AHB licenses, and you deploy two managed instances to a failover group with 8 vCores each, once you designate the secondary instance for standby, you'll get 8 vCores returned your license pool to use with other Azure SQL deployments. 

## Functional capabilities 

The following table describes the functional capabilities of a standby secondary managed instance:


|Functionality  |Description  |
|---------|---------|
|Limited read-workloads     | Once you designate your instance as standby, you're able to run only a limited number read-workloads on the secondary instance, such as DMVs, backups, and DBCC commands.      |
|Planned failover | All planned failover scenarios including recovery drills, relocating databases to different regions, and returning databases to the primary are supported by the standby replica. When the secondary switches to the primary, it can then serve read and write queries, while the old primary / new secondary becomes the standby replica and shouldn't be used for read workloads.     |
|Unplanned failover | During an unplanned failover, once the secondary switches to the primary role, it can serve both read and write queries. After the outage is mitigated and the old primary reconnects, it becomes the new secondary standby replica, and shouldn't be used for read workloads.   |
|Backup / restore| There's no difference in backup and restore behavior between a standby replica and a readable secondary managed instance.         |
|Monitoring     | All monitoring operations that are supported by a readable secondary replica are supported by the standby replica.         |
|RPO & RTO| The standby replica provides the same RPO and RTO as a readable secondary replica.          |
|Removing failover group  | If the failover group is removed (using something like [Remove-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/remove-azsqldatabaseinstancefailovergroup)), the standby replica becomes a read-write standalone instance, and the licensing model returns to what it was before it was designated for standby (either AHB or PAYG).  |

The secondary instance must be used as DR-only, with no production applications connected to the replica. The following lists the only activities that are permitted on the standby replica: 

- Run backups
- Perform maintenance operations, such as checkDB
- Connect monitoring applications
- Run DR drills


## Configure standby replica 

You can designate your secondary instance as standby when you create your auto-failover group, or update the configuration for an existing auto-failover group by using the Azure portal. 

### New failover group

You can designate your secondary instance as a standby replica when you create a new failover group by using the Azure portal. 

When you're creating your failover group in the Azure portal, set **Failover rights** to *On*, and then check the box next to *I confirm that I will use the secondary instance as a standby replica.*. Select **Create** to create your failover group. 

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/new-failover-group.png" alt-text="Screenshot of the Azure portal, creating a new failover group page, with the Failover rights option highlighted. ":::

For instructions to create a new failover group, review the [how to guide](auto-failover-group-configure-sql-mi.md) or the detailed [tutorial](failover-group-add-instance-tutorial.md). 

### Existing failover group 

You can update the failover rights for an existing failover group by using the Azure portal. 

To update the failover rights, follow these steps: 

1. Go to your SQL Managed Instance in the [Azure portal](https://portal.azure.com). 
1. Select **Failover groups** under **Data management**. 
1. Select **Edit configurations** from the navigation bar to open the **Edit configuration** page for your failover group. 

   :::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/update-failover-group-configuration.png" alt-text="Screenshot of the Azure portal, Failover groups page, selecting Edit Configurations. ":::

1. Set **Failover rights** to *On*, and check the box next to *I confirm that I will use the secondary instance as a standby replica.* 

   :::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/update-failover-rights-existing-instance.png" alt-text="Screenshot of the Azure portal, edit failover group configuration, with failover rights highlighted." :::

1. Select **Apply** to save your new settings and close the configuration pane. 


Alternatively, you can enable Failover rights directly from the **Compute + storage** page for your *secondary* managed instance. Review the [View licensing rights](#view-licensing-rights) section to learn more. 

## View licensing rights

There are two places to check the licensing for your secondary managed instance in the Azure portal. 

From the **Failover groups** page, the **Failover rights** status should be *ON* and the license model for the secondary instance should display **Failover rights currently activated**: 

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/view-failover-group-settings.png" alt-text="Screenshot of the Azure portal, Failover groups page, with failover rights, and license model highlighted." lightbox="media/auto-failover-group-standby-replica-how-to-configure/view-failover-group-settings.png":::

The default license model indicates the licensing model the instance will go back to using if the failover group fails over and the current secondary instance becomes the new primary instance. You may incur charges upon failover, depending on the default license model. 

From the **Compute + storage** page of your *secondary managed instance*, confirm that the **Failover rights** license is activated, and view the failover discount you're currently receiving for that instance under **Cost summary**: 

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/compute-storage.png" alt-text="Screenshot of the Azure portal, Compute and storage page, with failover rights highlighted." lightbox="media/auto-failover-group-standby-replica-how-to-configure/compute-storage.png":::

If you don't have Failover rights activated, and you qualify for the benefit, you'll also see the following recommendation on the **Overview** blade for either instance. Selecting it will take you to the **Edit configurations** page so you can activate the benefit: 

:::image type="content" source="media/auto-failover-group-standby-replica-how-to-configure/failover-rights-notification.png" alt-text="Screenshot of the Azure portal, SQL MI overview page, recommendations showing Failover rights are not used." :::


## Next steps

- For a detailed tutorial, see [Add a SQL Managed Instance to a failover group](failover-group-add-instance-tutorial.md).
- For a sample script, see: [Use PowerShell to create an auto-failover group on a SQL Managed Instance](scripts/add-to-failover-group-powershell.md).
- For a business continuity overview and scenarios, see [Business continuity overview](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md).
- To learn about automated backups, see [SQL Database automated backups](../database/automated-backups-overview.md).
- To learn about using automated backups for recovery, see [Restore a database from the service-initiated backups](../database/recovery-using-backups.md).
