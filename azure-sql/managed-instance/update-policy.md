---
title: Update policy
titleSuffix: Azure SQL Managed Instance
description: Use the update policy setting in Azure SQL Managed Instance to control your database compatibility with SQL Server 2022, or receive the latest updates to the database engine. 
author: MladjoA
ms.author: mlandzic
ms.reviewer: mathoma
ms.date: 05/21/2024
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - azure-sql-split
monikerRange: "=azuresql||=azuresql-mi"
---
# Update policy in Azure SQL Managed Instance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes the update policy for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md), and how to modify it. The update policy is an instance setting that controls access to the latest SQL Database Engine features in Azure. 

Azure SQL Managed Instance offers the following two update policies: 

- SQL Server 2022 update policy: The instance can only use SQL Database Engine features available in SQL Server 2022 as the internal database format is aligned with SQL Server 2022.
- Always-up-to-date update policy: The instance has access to all SQL Database Engine features as soon as they're available. The internal database format is no longer aligned with the latest version of SQL Server, and instead evolves with each newly introduced feature.

> [!IMPORTANT]
> Regardless of the configured update policy, all instances will continue receiving updates and features that _don't_ require changes to the SQL Database Engine, such as the following features:  [zone redundancy](high-availability-sla.md#zone-redundant-availability), [instance stop and start](instance-stop-start-how-to.md), and [fast provisioning](management-operations-overview.md?#fast-provisioning). 

## SQL Server 2022 update policy

The SQL Server 2022 update policy aligns your database format with SQL Server 2022. 

When using the SQL Server 2022 update policy, consider the following:

- The SQL Server 2022 update policy is the default update policy for all existing and newly deployed instances. 
- Your internal database format remains aligned with SQL Server 2022. 
- You receive all the latest updates available for SQL Server 2022. 
- You can [restore your database](restore-database-to-sql-server.md) to SQL Server 2022 from Azure SQL Managed Instance. 
- You can configure a [link](managed-instance-link-disaster-recovery.md) for real-time data replication, bidrectional failover, and disaster recovery between SQL Server 2022 and Azure SQL Managed Instance. 
- You may not have access to some of the latest SQL Database Engine features and benefits available to Azure SQL Managed Instance with the Always-up-to-date update policy. 
- The SQL Server 2022 update policy will be available until the [end of mainstream support of SQL Server 2022](/lifecycle/products/sql-server-2022).

## Always-up-to-date update policy 

The Always-up-to-date update policy configures your instance to receive all the latest features and updates available to Azure SQL Managed Instance. 

When using the Always-up-to-date update policy, consider the following:

- You're able to use all the new features and benefits available to Azure SQL Managed Instance. 
- The Always-up-to-date update policy is only available to instances within subnets that have enabled the [November 2022 feature wave](november-2022-feature-wave-enroll.md). 
- Once enabled, you can't go back to the SQL Server 2022 update policy.
- You lose some of the benefits provided by database format alignment with SQL Server 2022, such as the ability to restore your database to SQL Server 2022, and bidrectional failover between your instance and SQL Server 2022 with the [link](managed-instance-link-disaster-recovery.md) feature. 

## Feature comparison

The following columns list all the features that are only available to instances with the designated update policy:

:::row:::
    :::column:::
    **SQL Server 2022 update policy**
    - [Restore database to SQL Server 2022](restore-database-to-sql-server.md)
    - [Link with bidirectional failover and disaster recovery](managed-instance-link-disaster-recovery.md)
    :::column-end:::
    :::column:::
    **Always-up-to-date update policy**
    - There are currently no features that are exclusively available to Azure SQL Managed Instance configured with the Always-up-to-date update policy. 
    :::column-end:::
:::row-end:::

The following features are impacted by the configured update policy: 

- [Automated backups](automated-backups-overview.md) and [copy-only backups](/sql/relational-databases/backup-restore/copy-only-backups-sql-server): Database backups taken from instances configured with the SQL Server 2022 update policy can be restored to instances configured with either the SQL Server 2022 or Always-up-to-date update policy. Database backups taken from instances configured with the Always-up-to-date update policy can only be restored to instances also configured with the Always-up-to-date update policy. 
- [Managed Instance link](managed-instance-link-feature-overview.md#limitations): Establishing a link from SQL Managed Instance to SQL Server 2022, or failing back from SQL Server 2022 to SQL Managed Instance is only available to instances with the SQL Server 2022 update policy. 
- [Database copy and move](database-copy-move-how-to.md#limitations): A database from an instance configured with the Always-up-to-date update policy can't be copied or moved to an instance configured with the SQL Server 2022 update policy. 
- [Failover groups](failover-group-configure-sql-mi.md#change-update-policy): Instances in a failover group must have matching update policies. 

## Existing instances

For an existing instance, you can enable the Always-up-to-date update policy in the Azure portal. 

> [!CAUTION]
> The SQL Server 2022 update policy is enabled by default for all existing and new instances. Once the update policy is changed to **Always-up-to-date**, the internal database format is upgraded permanently. You can't change the update policy back to **SQL Server 2022** and can no longer use the features and benefits that require the SQL Server 2022 update policy. 

To change the update policy, follow these steps:

1. Go to your SQL managed instance resource in the [Azure portal](https://portal.azure.com). 
1. Select **Maintenance and updates** under **Settings**. 
1. Select the bubble to enable the **Always up-to-date** update policy: 

   :::image type="content" source="media/update-policy/update-policy-existing-instance.md.png" alt-text="Screenshot of the SQL Managed Instance page in the Azure portal, with update policy selected.":::

   1. Select **Yes** on the **Confirm update policy change** popup to save your changes. Once the Always-up-to-date update policy is enabled, the **SQL Server 2022** update policy is no longer available

## New instances

Although the SQL Server 2022 update policy is enabled by default, you can choose the **Always-up-to-date** policy when you create your instance in the Azure portal. 

To do so, follow these steps: 
1. Go to the **Azure SQL** page in the [Azure portal](https://portal.azure.com) and select **+ Create** to open the **Select SQL deployment option**. 
1. In the **SQL managed instances** tile, select *Single instance* from the dropdown, and then select **Create** to open the **Create Azure SQL Managed Instance** pane. 
1. Fill out details for your instance. On the **Additional settings** tab, under **SQL engine updates**, choose the **Always-up-to-date** policy: 

   :::image type="content" source="media/update-policy/update-policy-new-instance.png" alt-text="Screenshot of the Create Azure SQL Managed Instance page of the Azure portal with update policy selected.":::

1. You can confirm the designated policy under **Update policy** on the **Review + create** tab. 


## Related content

- [SQL Managed Instance automated backups](automated-backups-overview.md)
- [Long-term retention](../database/long-term-retention-overview.md)
- To learn about faster recovery options, see [Failover groups](failover-group-sql-mi.md).
