---
title: Update Policy
titleSuffix: Azure SQL Managed Instance
description: Use the update policy setting in Azure SQL Managed Instance to control your database compatibility with SQL Server 2022, SQL Server 2025, or receive the latest updates to the Database Engine.
author: MladjoA
ms.author: mlandzic
ms.reviewer: mathoma
ms.date: 2/27/2026
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - sfi-image-nochange
monikerRange: "=azuresql || =azuresql-mi"
---
# Update policy in Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes the update policy for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md), and how to modify it. The update policy is an instance setting that controls access to the latest SQL engine features in Azure. 

Azure SQL Managed Instance offers the following three update policies: 

- **SQL Server 2025** update policy: The instance can only use SQL engine features available in SQL Server 2025 because the internal database format is aligned with SQL Server 2025.
- **SQL Server 2022** update policy: The instance can only use SQL engine features available in SQL Server 2022 because the internal database format is aligned with SQL Server 2022.
- **Always-up-to-date** update policy: The instance has access to all SQL engine features as soon as they're available in Azure. The internal database format no longer aligns with the latest version of SQL Server, and instead evolves with each newly introduced feature.

> [!IMPORTANT]
> - Regardless of the configured update policy, all instances continue receiving updates and features that *don't* require changes to the SQL engine, such as the following features:  [zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability), and [instance stop and start](instance-stop-start-how-to.md).
> - The **SQL Server 2022** update policy is the default update policy for all existing and newly deployed instances.

## SQL Server 2025 update policy

> [!NOTE]
> Changing the update policy from **SQL Server 2025** to **Always-up-to-date** is currently and temporarily disabled.

The **SQL Server 2025** update policy aligns your database format with [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)].

When you use the SQL Server 2025 update policy, consider the following points:

- Your internal database format stays aligned with [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)].
- You receive all the latest updates available for [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)].
- You can [restore your database](restore-database-to-sql-server.md) to [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)] from Azure SQL Managed Instance.
- You can configure a [link](managed-instance-link-disaster-recovery.md) for real-time data replication, bidirectional failover, and disaster recovery between [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)] and Azure SQL Managed Instance.
- You might not have access to some of the latest SQL engine features and benefits available to Azure SQL Managed Instance with the **Always-up-to-date** update policy.
- The **SQL Server 2025** update policy is available until the end of mainstream support of [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)], at which point, the update policy for instances with the **SQL Server 2025** update policy automatically updates to the update policy that corresponds to the latest major SQL Server release available at that time.

## SQL Server 2022 update policy

The **SQL Server 2022** update policy aligns your database format with SQL Server 2022. 

When you use the SQL Server 2022 update policy, consider the following points:

- The **SQL Server 2022** update policy is the default update policy for all existing and newly deployed instances. 
- Your internal database format stays aligned with SQL Server 2022. 
- You receive all the latest updates available for SQL Server 2022. 
- You can [restore your database](restore-database-to-sql-server.md) to SQL Server 2022 from Azure SQL Managed Instance. 
- You can configure a [link](managed-instance-link-disaster-recovery.md) for real-time data replication, bidirectional failover, and disaster recovery between SQL Server 2022 and Azure SQL Managed Instance. 
- You might not have access to some of the latest SQL engine features and benefits available to Azure SQL Managed Instance with the **Always-up-to-date** update policy. 
- The **SQL Server 2022** update policy is available until the [end of mainstream support of SQL Server 2022](/lifecycle/products/sql-server-2022), at which point, the update policy for instances with the **SQL Server 2022** update policy automatically updates to the update policy that corresponds to the latest major SQL Server release available at that time. 

## Always-up-to-date update policy

The **Always-up-to-date** update policy configures your instance to receive all the latest features and updates available to Azure SQL Managed Instance. 

When you use the **Always-up-to-date** update policy, consider the following points:

- You can use all the new features and benefits available to Azure SQL Managed Instance. 
- Once you enable the **Always-up-to-date** policy, you can't go back to the **SQL Server 2022**, or **SQL Server 2025** update policy for that instance.
- You lose some of the benefits provided by database format alignment with SQL Server 2022 or [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)], such as the ability to restore your database to SQL Server 2022 or [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)], and bidirectional failover between your instance and SQL Server 2022 or [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)] with the [link](managed-instance-link-disaster-recovery.md) feature. 

## Feature comparison

The following table lists all the features that are only available to instances with the designated update policy:

| Update policy | Features |
|---------|---------|
| *Always-up-to-date* update policy | - There are currently no separate features that are only available to instances with the *Always-up-to-date* update policy. <br /> - All features available with the *SQL Server 2025* update policy are also available to instances with the *Always-up-to-date* update policy, other than the ability to restore databases, or configure a link with bidirectional failover, to [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)].   |
| *SQL Server 2025* update policy | - [Restore database to SQL Server 2025](restore-database-to-sql-server.md)<br /> - [Link with bidirectional failover and disaster recovery with SQL Server 2025](managed-instance-link-disaster-recovery.md) <br />- [JSON data type](/sql/t-sql/data-types/json-data-type)<br /> - [JSON_ARRAYAGG](/sql/t-sql/functions/json-arrayagg-transact-sql) and [JSON_OBJECTAGG](/sql/t-sql/functions/json-objectagg-transact-sql) aggregate functions <br /> - [Invoke an HTTPS REST endpoint SP](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql) <br /> - [Azure SQL Managed Instance Mirroring in Fabric](/fabric/database/mirrored-database/azure-sql-managed-instance) <br /> - [Vector functions](/sql/t-sql/functions/vector-functions-transact-sql?view=azuresqlmi-current&preserve-view=true) <br /> - [Vector data type](/sql/t-sql/data-types/vector-data-type?view=azuresqlmi-current&preserve-view=true) <br /> - [Fuzzy string matching](/sql/relational-databases/fuzzy-string-match/overview)  <br /> - [DATEADD (Transact-SQL)](/sql/t-sql/functions/dateadd-transact-sql).  <br /> - [UNISTR (Transact-SQL)](/sql/t-sql/functions/unistr-transact-sql) <br /> - [Regular expression functions](/sql/relational-databases/regular-expressions/overview) <br /> - [\|\| (String concatenation)](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql) <br /> - [\|\|= (Compound assignment)](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql) <br /> - [Degree of parallelism (DOP) feedback](/sql/relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback?view=azuresqlmi-current&preserve-view=true) <br /> - [Optimized locking](/sql/relational-databases/performance/optimized-locking?view=azuresqlmi-current&preserve-view=true) |
| *SQL Server 2022* update policy | - [Restore database to SQL Server 2022](restore-database-to-sql-server.md)  <br /> - [Link with bidirectional failover and disaster recovery with SQL Server 2022](managed-instance-link-disaster-recovery.md) |

The following features are affected by the configured update policy: 

- [Automated backups](automated-backups-overview.md) and [copy-only backups](/sql/relational-databases/backup-restore/copy-only-backups-sql-server): 
   - You can restore database backups taken from instances configured with the **SQL Server 2022** update policy to instances configured with either the **SQL Server 2022** or **Always-up-to-date** update policy. 
   - You can restore database backups taken from instances configured with the **SQL Server 2025** update policy to instances configured with either the **SQL Server 2025** or **Always-up-to-date** update policy.
   - You can only restore database backups taken from instances configured with the **Always-up-to-date** update policy to instances also configured with the **Always-up-to-date** update policy. 
- [Managed Instance link](managed-instance-link-feature-overview.md#limitations): 
   - Only instances with the **SQL Server 2022** update policy can establish a link from SQL Managed Instance to SQL Server 2022 or fail back from SQL Server 2022 to SQL Managed Instance. 
   - Only instances with the **SQL Server 2025** update policy can establish a link from SQL Managed Instance to [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)] or fail back from [!INCLUDE [sssql25-md](../../docs/includes/sssql25-md.md)] to SQL Managed Instance.
- [Database copy and move](database-copy-move-how-to.md#limitations): You can only copy and move databases to instances with matching, or higher version, update policies. Copying or moving a database to an instance with a lower version update policy is not supported. 
- [Failover groups](failover-group-configure-sql-mi.md#change-update-policy): Instances in a failover group must have matching update policies. 

## Which update policy to choose?

Unless you're relying on a specific feature that requires the **SQL Server 2022**, or **SQL Server 2025**, update policy, we recommend using the **Always-up-to-date** update policy. The **Always-up-to-date** update policy provides you with the latest features and benefits available to Azure SQL Managed Instance. While the latest features might not be directly relevant to you, there are often improvements to performance, security, and reliability that can benefit your workload.

If you're using the **SQL Server 2022**, or **SQL Server 2025**, update policy to copy databases from SQL Managed Instance to SQL Server for regulatory compliance, contractual obligations, or other reasons important to your business, you can often accomplish the same goals by using other features like database export/import, or transactional replication, or services like Azure Data Factory. Using one of these alternative methods allows you to use the **Always-up-to-date** update policy with SQL Managed Instance while still meeting your business requirements.

If you're not yet sure what requirements your solution needs, then take your time and start with the **SQL Server 2022**, or **SQL Server 2025** update policy. You can always switch to the **Always-up-to-date** update policy later.

You can also use different update policies for different environments. For example, use the **Always-up-to-date** update policy in your development environment to take advantage of the latest features, while using the **SQL Server 2022** update policy in your production environment to ensure compatibility with SQL Server 2022 for failover scenarios.

## Existing instances

For an existing instance, you can enable the **Always-up-to-date** update policy by using the Azure portal, PowerShell, the Azure CLI, or REST API. 

> [!CAUTION]
> - The **SQL Server 2022** update policy is enabled by default for all existing and new instances. When you change the update policy to **SQL Server 2025**, or **Always-up-to-date**, the internal database format is upgraded permanently. You can't change the update policy back to **SQL Server 2022** and you can no longer use the features and benefits that require the **SQL Server 2022** update policy.
> - Changing the update policy from **SQL Server 2025** to **Always-up-to-date** is currently and temporarily disabled.

### [Azure portal](#tab/azure-portal)

To change the update policy for an existing instance in the Azure portal, follow these steps:

1. Go to your **SQL managed instance** resource in the [Azure portal](https://portal.azure.com). 
1. Select **Maintenance and updates** under **Settings**. 
1. Select the bubble to enable the **Always up-to-date** update policy: 

   :::image type="content" source="media/update-policy/update-policy-existing-instance.md.png" alt-text="Screenshot of the SQL Managed Instance page in the Azure portal, with update policy selected.":::

1. Select **Yes** on the **Confirm update policy change** popup to save your changes. Once the **Always-up-to-date** update policy is enabled, the **SQL Server 2022** update policy is no longer available.

### [PowerShell](#tab/powershell)

Set `DatabaseFormat` when you update an existing SQL managed instance with the [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance#example-9-update-an-existing-instance-with-database-format-and-pricing-model) PowerShell command to configure your instance to use the desired update policy. 

To configure the update policy, set `DatabaseFormat` to one of the following values:
- `SqlServer2025` to enable the **SQL Server 2025** update policy.
- `AlwaysUpToDate` to enable the **Always-up-to-date** update policy.

### [Azure CLI](#tab/azure-cli)

Set `--database-format` when you update an existing SQL managed instance with the [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update:~:text=Update%20mi%20database%20format%20and%20pricing%20model) Azure CLI command to configure your instance to use the desired update policy.

To configure the update policy, set `--database-format` to one of the following values:
- `SqlServer2025` to enable the **SQL Server 2025** update policy.
- `AlwaysUpToDate` to enable the **Always-up-to-date** update policy.


### [REST API](#tab/rest-api)

Set `databaseFormat` when you update an existing SQL managed instance with the [Managed Instances - Create Or Update](/rest/api/sql/managed-instances/create-or-update#create-managed-instance-with-all-properties) REST API command to configure your instance to use the desired update policy. 

To configure the update policy, set `databaseFormat` to one of the following values:
- `SqlServer2025` to enable the **SQL Server 2025** update policy.
- `AlwaysUpToDate` to enable the **Always-up-to-date** update policy.

---

## New instances

Although the **SQL Server 2022** update policy is enabled by default, you can choose the **SQL Server 2025** or **Always-up-to-date** policy when you create your instance by using the Azure portal, PowerShell, Azure CLI, or REST API.

> [!IMPORTANT]
> Make sure to add update policy configuration to your deployment templates, so that you don't rely on system defaults that might change over time. 

### [Azure portal](#tab/azure-portal)

To create a new SQL managed instance with the **Always-up-to-date** policy in the Azure portal, follow these steps: 

1. Go to [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).
1. In the pane for **Azure SQL Managed Instance**, select **Show options**.
1. In the **Azure SQL Managed Instance options** window, select **Create SQL Managed Instance**.

   :::image type="content" source="media/update-policy/show-options-create-sql-managed-instance.png" alt-text="Screenshot from the Azure portal of the Azure SQL hub, showing the Show options button and the Create SQL Managed Instance button." lightbox="media/update-policy/show-options-create-sql-managed-instance.png":::

1. On the **Create Azure SQL Managed Instance** page, fill out details for your instance. For complete steps to create a new SQL managed instance, see [Quickstart: Create Azure SQL Managed Instance](instance-create-quickstart.md).
1. On the **Additional settings** tab, under **SQL engine updates**, choose the **Always-up-to-date** policy: 

   :::image type="content" source="media/update-policy/update-policy-new-instance.png" alt-text="Screenshot of the Create Azure SQL Managed Instance page of the Azure portal with update policy selected.":::

1. Confirm the designated policy under **Update policy** on the **Review + create** tab before you create your new instance. 

### [PowerShell](#tab/powershell)

Set `DatabaseFormat` = `AlwaysUpToDate` when you create a new SQL managed instance with the [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance#example-10-create-a-new-instance-with-database-format-and-pricing-model) PowerShell command to create a new instance with the desired update policy.

To configure the update policy, set `DatabaseFormat` to one of the following values:
- `SqlServer2025` to enable the **SQL Server 2025** update policy.
- `AlwaysUpToDate` to enable the **Always-up-to-date** update policy.

### [Azure CLI](#tab/azure-cli)

Set `--database-format` = `AlwaysUpToDate` when you create a new SQL managed instance with the [az sql mi create](/cli/azure/sql/mi#az-sql-mi-create:~:text=Create%20managed%20instance%20with%20database%20format%20and%20pricing%20model) Azure CLI command to create a new instance with the desired update policy.

To configure the update policy, set `--database-format` to one of the following values:
- `SqlServer2025` to enable the **SQL Server 2025** update policy.
- `AlwaysUpToDate` to enable the **Always-up-to-date** update policy.

### [REST API](#tab/rest-api)

Set `databaseFormat` when you create a new SQL managed instance with the [Managed Instances - Create Or Update](/rest/api/sql/managed-instances/create-or-update#create-managed-instance-with-all-properties) REST API command to create a new instance with the desired update policy. 

To configure the update policy, set `DatabaseFormat` to one of the following values:
- `SqlServer2025` to enable the **SQL Server 2025** update policy.
- `AlwaysUpToDate` to enable the **Always-up-to-date** update policy.

---

## Check update policy

You can check the current update policy by using the Azure portal or Transact-SQL (T-SQL). 

To check the current update policy in the Azure portal, go to your **SQL managed instance** resource. Check the **Update policy** field under **Updates and maintenance** in the resource menu. 

You can also use the [serverproperty](/sql/t-sql/functions/serverproperty-transact-sql) T-SQL command: 

```sql
select serverproperty('ProductUpdateType')
```

The following values for `ProductUpdateType` indicate the update policy for the current instance: 
- `CU`: Updates are deployed via cumulative updates (CUs) for the corresponding major SQL Server release (**SQL Server 2022**, or **SQL Server 2025** update policy)
- `Continuous`: New features are brought to Azure SQL Managed Instance as soon as they're available, independent of the SQL Server release cadence (**Always-up-to-date** update policy)

## Related content

- [Automated backups in Azure SQL Managed Instance](automated-backups-overview.md)
- [Long-term retention backups - Azure SQL Database and Azure SQL Managed Instance](../database/long-term-retention-overview.md)
- [Failover groups overview & best practices - Azure SQL Managed Instance](failover-group-sql-mi.md)
