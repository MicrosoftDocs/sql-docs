---
title: Configure license-free standby replica
description: Learn how to save on licensing costs by using a standby Azure SQL Database replica.
author: rajeshsetlem
ms.author: rsetlem
ms.reviewer: mathoma, randolphwest
ms.date: 05/21/2024
ms.service: sql-database
ms.subservice: high-availability
ms.topic: how-to
ms.custom:
  - ignite-2023
---

# Configure a license-free standby replica for Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](standby-replica-how-to-configure.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/failover-group-standby-replica-how-to-configure.md?view=azuresql-mi&preserve-view=true)

This article describes how you can save on licensing costs by designating your secondary disaster recovery (DR) database for standby when using Azure SQL Database.

## Overview

When a secondary database replica is used _only_ for disaster recovery, and doesn't have any workloads running on it, or applications connecting to it, you can save on licensing costs by designating the database as a *standby replica*. When a secondary database is designated for standby, Microsoft provides you with the number of vCores licensed to the primary database at no extra charge under the failover rights benefit in the [product licensing terms](https://www.microsoft.com/Licensing/product-licensing/sql-server). You're still billed for the compute and storage that the secondary database uses.

You can designate a replica for standby when you configure a new [active geo-replication](active-geo-replication-overview.md) replication, or you can convert an existing replica to be standby.

While active geo-replication supports adding four secondary replicas, you can only designate one secondary database replica for standby. Failover groups support one secondary database replica per primary database, and it can be either readable, or standby.

During planned or unplanned failover, the standby replica becomes the new primary and starts to incur regular vCore licensing costs while the original primary becomes the new standby secondary, and stops incurring vCore licensing costs.

## Cost benefit

When you designate a database replica as standby, Microsoft doesn't charge you SQL Server licensing costs for the vCores used by the standby replica. However, because the database is billed for the entire hour, you might still be charged licensing costs for the entire hour if the state change is made in the middle of the hour.

The benefit translates differently between customers who use the pay-as-you-go model and customers who use the [Azure Hybrid Benefit](../azure-hybrid-benefit.md) model. For a pay-as-you-go customer, the vCores are discounted on their invoice. For a customer who uses the Azure Hybrid Benefit for the standby replica, the number of vCores that the secondary replica uses are returned to their licensing pool.

For example, as a pay-as-you-go customer, if you have 16 vCores assigned to the secondary database, a discount for 16 vCores appears on your invoice if you designate your secondary database as standby only.

In another example, if you have 16 Azure Hybrid Benefit licenses and you deploy one database that has 16 vCores, after you designate the secondary database for standby, 16 vCores are returned to your license pool for you to use with other Azure SQL deployments.

## Functional capabilities

The following table describes the functional capabilities of a standby secondary database replica:

| Functionality | Description |
| --- | --- |
| Limited read workloads | After you designate your database as standby, you can run only a limited number of read workloads on the secondary database, such as Dynamic Management Views (DMVs), backups, and Database Console Commands (DBCC) queries. |
| Planned failover | All planned failover scenarios, including recovery drills, relocating databases to different regions, and returning databases to the primary, are supported by the standby replica. When the secondary switches to the primary, it can serve read and write queries. The new secondary (the original primary) becomes the standby replica and shouldn't be used for read workloads. |
| Unplanned failover | During an unplanned failover, after the secondary switches to the primary role, it can serve read and write queries. After the outage is mitigated and the original primary reconnects, it becomes the new secondary standby replica and shouldn't be used for read workloads. |
| Back up and restore | The backup and restore behavior in a standby replica and a readable secondary database replica are the same. |
| Monitoring | All monitoring operations that are supported by a readable secondary replica are supported by the standby replica. |

The standby database replica must _only_ be used for disaster recovery. The following lists the only activities that are permitted on the standby database:

- Perform maintenance operations, such as checkDB
- Connect monitoring applications
- Run disaster recovery drills

## Limitations

The following table lists the supported and unsupported deployment models:

| Deployment model | Compute tier | Service tier | Standby replica supported | Hardware |
| -- | -- | -- | -- | -- |
| Single database | Provisioned | General Purpose | Yes | Standard-series (Gen5), FSv2-Series, DC-Series |
| Single database | Provisioned | Business Critical | Yes | Standard-series (Gen5), DC-Series |
| Single database | Provisioned | Hyperscale | N/A | N/A |
| Single database | Serverless | All | No | N/A |
| Elastic pool | All | All | No | N/A |

Using a standby database has the following limitations:

- Only one secondary database replica can be designated for standby.
- The serverless compute tier isn't supported. Standby replica can't be enabled if the primary or secondary database is in the serverless compute tier.
- The DTU purchasing model isn't supported. You can enable a standby replica for databases using the vCore purchasing model only.
- The Hyperscale service tier isn't supported. Only databases in the General Purpose and Business Critical service tiers can be designated for standby.
- When using a failover group, standby rights are assigned at the database level, not the failover group level, and must be assigned separately for each database within the failover group.
- Designating a secondary replica for standby isn't supported when the replica is a secondary replica of a secondary replica (a process known is chaining).

## Prerequisites

- An Azure subscription. [!INCLUDE [quickstarts-free-trial-note](../includes/quickstarts-free-trial-note.md)]
- A primary provisioned vCore Azure SQL Database in the General Purpose or Business Critical service tier running on [supported](#limitations) hardware. Review the [Quickstart](single-database-create-quickstart.md) to get started.

## Configure new replica for standby

You can designate a replica for standby when you configure a new active geo-replication relationship by using the Azure portal, PowerShell, the Azure CLI, or the REST API.

### [Azure portal](#tab/azure-portal)

To create a new active geo-replication relationship and designate your secondary database for standby in the Azure portal, follow these steps:

1. Go to your **SQL database** resource in the [Azure portal](https://portal.azure.com).
1. Choose **Replicas** under **Data management** from the resource menu, and then select **+ Create replica** to open the **Create SQL Database - Geo Replica** page.

   :::image type="content" source="media/standby-replica-how-to-configure/add-replica.png" alt-text="Screenshot of the Replicas page for the SQL database in the Azure portal.":::

1. On the **Create SQL Database - Geo Replica** page, select **Standby replica** for **Replica type** under **Replica configuration**. Check the box to confirm you'll use the replica for standby.

   :::image type="content" source="media/standby-replica-how-to-configure/create-standby-replica.png" alt-text="Screenshot of the Create geo replica page with standby replica highlighted in the Azure portal.":::

1. Provide a new or existing server for the new standby database and then use **Review + create** to do a final validation of your database and server details.
1. Use **Create** to confirm your settings and create your new standby database replica.

> [!NOTE]  
> You can also designate your databases for standby when [you create a failover group](failover-group-configure-sql-db.md#create-failover-group), or [add databases to an existing failover group](failover-group-configure-sql-db.md#) in the Azure portal.

### [PowerShell](#tab/azure-powershell)

To create a new active geo-replication relationship and designate your secondary database for standby with PowerShell, use [New-AzSqlDatabaseSecondary](/powershell/module/az.sql/new-azsqldatabasesecondary) and specify `SecondaryType` as `Standby`, such as the following sample script:

```
$StandbyReplica = @{
   ResourceGroupName = "<PrimaryResourceGroup>"
   ServerName = "<PrimaryServerName>"
   DatabaseName = "<PrimaryDatabaseName>"
   PartnerResourceGroup = "<SecondaryResourceGroup>"
   PartnerServerName = "<SecondaryServerName>"
   PartnerDatabaseName = "<SecondaryDatabaseName>"
   SecondaryType = Standby
}

New-AzSqlDatabaseSecondary @StandByReplica
```

### [Azure CLI](#tab/azure-cli)

To create a new active geo-replication relationship and designate your secondary database for standby with the Azure CLI, use [az sql db replica create](/cli/azure/sql/db/replica#az-sql-db-replica-create) and specify `secondary-type` as `Standby`, such as the following sample script:

```azurecli-interactive
az sql db replica create --resource-group  <PrimaryResourceGroup> --server <PrimaryServerName> \
   --name <PrimaryDatabaseName> --partner-resource-group <SecondaryResourceGroup> \
   --partner-server <SecondaryServerName> --secondary-type Standby
```

### [REST API](#tab/rest-api)

To create a new active geo-replication relationship and designate your secondary database for standby, use the [Replication Links - Create or Update](/rest/api/sql/replication-links/create-or-update) REST API command, specifying `ReplicationLinkType` as `STANDBY`.

---

## Convert existing replica

You can use the Azure portal or the [Replication Links - Update](/rest/api/sql/replication-links/update) REST API command to convert an existing replica from a regular geo-replica to a standby replica, or a standby replica to a regular geo-replica.

To convert an existing replica in the Azure portal, follow these steps:

1. Go to your SQL database resource in the [Azure portal](https://portal.azure.com).
1. Select **Replicas** under **Data management**.
1. Select the ellipses (...) for the replica, and then:
    1. To convert a regular replica to a standby replica, choose **Convert to Standby**. Check the box next to **I confirm...** on the **Convert to Standby replica** popup window, and then select **Yes** to save your change and convert your replica.
    1. To convert a standby replica to a regular geo-replica, choose **Convert to Geo**. Check the box next to **I confirm...** on the **Convert to Geo replica** popup window, and then select **Yes** to save your changes and convert your replica.

To convert an existing replica by using the REST API [Replication Links - Update](/rest/api/sql/replication-links/update) command, designate the `linkType` as `STANDBY` for a standby replica or `GEO` to convert an existing standby replica back to a regular geo-replica.

## View licensing rights

You can view the licensing rights for an existing database by using the Azure portal, PowerShell, the Azure CLI, or REST API.

### [Azure portal](#tab/azure-portal)

To check licensing rights for an existing database by using the Azure portal, follow these steps:

1. Go to your **SQL database** in the [Azure portal](https://portal.azure.com).
1. On the **Overview** page, check **Replica Type** under **Essentials**. A value of `Standby` indicates that your database is a standby replica, and you aren't charged for SQL licensing costs for this database:

   :::image type="content" source="media/standby-replica-how-to-configure/view-replica-rights.png" alt-text="Screenshot of the Overview page for SQL database in the Azure portal with replica type highlighted." lightbox="media/standby-replica-how-to-configure/view-replica-rights.png":::

### [PowerShell](#tab/azure-powershell)

To check licensing rights for an existing database with PowerShell, use [Get-AzSqlDatabaseReplicationLink](/powershell/module/az.sql/get-azsqldatabasereplicationlink).

A value of `NonReadableSecondary` for **Role** indicates your database is a standby replica, and you aren't charged SQL licensing costs for this database.

### [Azure CLI](#tab/azure-cli)

To check licensing rights for an existing database with the Azure CLI, use [az sql db replica list-links](/cli/azure/sql/db/replica#az-sql-db-replica-list-links).

A value of `NonReadableSecondary` for **Role** indicates your database is a standby replica, and you aren't charged SQL licensing costs for this database.

### [REST API](#tab/rest-api)

To check licensing rights for an existing database with the REST API, use the [Replication Links - Get](/rest/api/sql/replication-links/get) command.

---

## Remove standby replica

After a database is designated as standby, you can't just remove the standby property. To remove a standby replica, you must stop replication to end the active geo-replication relationship. After replication stops, your database becomes a standalone, and you'll start incurring licensing costs.

You can stop geo-replication by using the Azure portal, PowerShell, the Azure CLI, or REST API.

### [Azure portal](#tab/azure-portal)

To remove a standby replica by ending geo-replication in the Azure portal, follow these steps:

1. Go to your **SQL database** in the [Azure portal](https://portal.azure.com).
1. Select **Replicas** under **Data management**.
1. Select the ellipses (...) for the **Standby replica** and then select **Stop replication** from the pop-up menu. This stops replication so your secondary database is now standalone rather than designated for standby, and incurring licensing costs.

### [PowerShell](#tab/azure-powershell)

To remove a standby replica with PowerShell, use [Remove-AzSqlDatabaseSecondary](/powershell/module/az.sql/remove-azsqldatabasesecondary) to end replication so your database becomes standalone rather than designated for standby, and starts incurring licensing costs.

### [Azure CLI](#tab/azure-cli)

To remove a standby replica with the Azure CLI, use [az sql db replica delete-link](/cli/azure/sql/db/replica#az-sql-db-replica-delete-link) to end replication so your database becomes standalone rather than designated for standby, and starts incurring licensing costs.

### [REST API](#tab/rest-api)

To remove a standby replica with the REST API, use the [Replication Links - Delete](/rest/api/sql/replication-links/delete) command.

---

## Frequently asked questions (FAQ)

-  What are the pricing implications?

   Secondary database replicas are charged for SQL licensing, compute, and storage for data and backups. When you designate a database replica for standby, you aren't charged for the licensing costs for the vCores used by the secondary replica, but you're still charged for compute and storage.

-  What are the approximate savings with a standby replica?

   Without included licensing costs, a standby replica can save between 35 to 40 percent compared to a regular fully readable secondary replica, though savings vary by region. For accurate pricing, use the [Azure Pricing Calculator](https://azure.microsoft.com/pricing/calculator/) and choose **Standby replica** in the **Disaster Recovery dropdown list.

- How many vCores will be license-free for the standby replica?

   The same number of vCores as the primary database uses. Configuring the secondary replica with the same number of vCores as the primary database is recommended for optimal geo-replication performance.

-  Do I need to have a SQL Server license with active [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) to use a standby replica?

   No. Since the standby replica doesn't incur licensing costs, you don't need an active SQL Server license with active Software Assurance.

-  How can I use the standby replica?

   Standby replicas are intended for disaster recovery (DR) purposes only, and can't have any active read workloads on it. The only acceptable workloads are for monitoring, maintenance such as running Dynamic Management Views (DMVs), and CheckDB.

-  Can I update my existing readable secondary replica to a standby replica to save on costs?

   Yes, in the Azure portal, on the **Replicas** pane. Select the ellipses (...) and then select the option to **Convert** your replica.

-  Can I enable the Azure Hybrid Benefit for the standby replica?

   Designating a replica for standby replaces the discount from the Azure Hybrid Benefit, so you can't modify the licensing model for the replica by using the Azure portal. However, if you want the standby replica to use the Azure Hybrid Benefit upon failover, you can use the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) PowerShell or [az sql db update](/cli/azure/sql/db#az-sql-db-update) Azure CLI command to update the license type to `BasePrice` (Azure Hybrid Benefit) for the standby replica to use when the standby replica becomes primary after failover.

-  What happens to the standby replica status during failover?

   During planned or unplanned failover, the standby replica becomes the new primary incurring regular licensing costs while the original primary becomes the new standby secondary, and stops incurring vCore licensings costs. However, since the instance is billed for the entire hour, you might still be charged licensing costs for the new secondary for the entire hour if the state change happens in the middle of the hour. If the original primary (which becomes the standby after failover) was using the Azure Hybrid Benefit, the standby licensing discount overrides the Azure Hybrid Benefit used by the database.

-  What if I scale up the primary or secondary to a higher vCore size?

   When scaling up, it's a best practice to scale up the secondary first, and then the primary. Although the secondary replica will have a higher number of vCores than the primary during the transition period, the standby replica benefits still apply. Try to minimize the transition period as much as possible.

-  What if I scale down the primary or secondary to a lower vCore size?

   When scaling down, it's a best practice to scale down the primary first, and then the secondary. Although the secondary replica will have a higher number of vCores than the primary during the transition period, the standby replica benefits still apply. Try to minimize the transition period as much as possible.

-  What happens if I remove the geo-replication relationship between the primary and standby replica?

   After geo-replication is removed, the standby database becomes a regular standalone database, and starts incurring licensing costs.

-  Can I get [reserved capacity](reserved-capacity-overview.md) benefits for the standby replica?

   Yes. Reserved capacity pricing is fully compatible with the standby replica.

- Can I designate a replica for standby when I'm creating a new failover group, or adding databases to it?

   Yes, but only when creating a new failover group, or adding databases to an existing failover group in the Azure portal. PowerShell and the Azure CLI aren't currently available.

## Related content

- [failover group](failover-group-configure-sql-db.md)
- [active geo-replication](active-geo-replication-overview.md)
- [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- [standby](standby-replica-how-to-configure.md)
