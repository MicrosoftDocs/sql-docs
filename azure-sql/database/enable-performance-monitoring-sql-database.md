---
title: Enable Performance Monitoring (Preview)
titleSuffix: Azure SQL Database
description: Learn how to enable, verify, and disable performance monitoring for a database in Azure SQL Database, and how to view and query the collected data.
author: lcwright
ms.author: lancewright
ms.reviewer: wiassaf
ms.date: 09/28/2026
ms.service: azure-sql-database
ms.subservice: monitoring
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
ai-usage: ai-assisted
monikerRange: "=azuresql || =azuresql-db"
---

# Enable performance monitoring for Azure SQL Database (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains how to enable, verify, and disable performance monitoring for a database in Azure SQL Database, and how to view and query the data that it collects.

Performance monitoring provides a Microsoft-managed monitoring experience for your databases. After you enable monitoring on a database, Azure collects performance data from that database and makes it available for analysis. You don't have to deploy or maintain monitoring agents, data stores, or other monitoring infrastructure.

The collected data provides visibility into resource utilization, database activity, storage performance, active sessions, and wait statistics. Use this data to establish a performance baseline, identify bottlenecks, investigate the cause of performance issues, and find where tuning can improve performance. You can view the data in dashboards, or query it directly with Kusto Query Language (KQL) through an Azure Data Explorer query endpoint.

> [!NOTE]
> Performance monitoring for Azure SQL Database is currently in preview. Availability, prerequisites, and supported configurations might change before general availability. For more information, see [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

## How performance monitoring works

You enable performance monitoring for a database by setting the `MS_EnablePerformanceMonitoringPreview` database-scoped [extended property](/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql) to `true` in that database. Azure detects the property and starts collecting performance data for the database.

Configure monitoring per database, not per [logical server](logical-servers.md). Setting the property in one database doesn't enable monitoring for other databases on the same logical server.

## Supported configurations

During the preview, performance monitoring supports the following Azure SQL Database configurations:

| Configuration | Supported |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------- |
| Single databases in the [DTU-based purchasing model](service-tiers-dtu.md)              | Yes       |
| Single databases in the [vCore-based purchasing model](service-tiers-sql-database-vcore.md), including the General Purpose, Business Critical, and [Hyperscale](service-tier-hyperscale.md) service tiers            | Yes       |
| Single databases in the [serverless compute tier](serverless-tier-overview.md)          | Yes       |
| Databases in an [elastic pool](elastic-pool-overview.md)    | No        |
| Secondary replicas, including [geo-replicas](active-geo-replication-overview.md), [named replicas](service-tier-hyperscale-replicas.md#named-replica), and [read scale-out](read-scale-out.md) replicas | No        |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- Performance monitoring supports single databases in the [DTU-based purchasing model](service-tiers-dtu.md).
- Performance monitoring supports single databases in the [vCore-based purchasing model](service-tiers-sql-database-vcore.md), including the General Purpose, Business Critical, and [Hyperscale](service-tier-hyperscale.md) service tiers.
- Performance monitoring supports single databases in the [serverless compute tier](serverless-tier-overview.md).
- Performance monitoring doesn't support databases in an [elastic pool](elastic-pool-overview.md).
- Performance monitoring doesn't support secondary replicas, including [geo-replicas](active-geo-replication-overview.md), [named replicas](service-tier-hyperscale-replicas.md#named-replica), and [read scale-out](read-scale-out.md) replicas.

## Prerequisites

Before you enable performance monitoring, ensure that you have the following items:

- A single database in Azure SQL Database in a [supported configuration](#supported-configurations). If you don't have one, see [Quickstart: Create a single database](single-database-create-quickstart.md). You can't monitor the `master` database and other system databases.
- Permission to add or update a database-level extended property in the database, such as membership in the [db\_owner](/sql/relational-databases/security/authentication-access/database-level-roles) fixed database role.
- A client tool that can run Transact-SQL (T-SQL) queries against the database, such as [SQL Server Management Studio (SSMS)](/ssms/install/install), the [MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code), or the [query editor in the Azure portal](query-editor.md).

To view or query the collected data, you also need the [access prerequisites](#access-prerequisites) described later in this article.

## Enable performance monitoring collection

> [!IMPORTANT]
> 
> Run the script in each user database that you want to monitor. Don't run it in `master` or another system database. Adding the property to a system database doesn't enable data collection for user databases.

1. Connect to the user database that you want to monitor. Ensure the database context is the user database, not `master`.
1. Run the following script. The script adds the extended property if it doesn't exist, or sets its value to `true` if it already exists.

   ```sql
   IF EXISTS (
       SELECT 1
       FROM sys.extended_properties
       WHERE class = 0
         AND name = N'MS_EnablePerformanceMonitoringPreview'
   )
   BEGIN
       EXEC sys.sp_updateextendedproperty
           @name = N'MS_EnablePerformanceMonitoringPreview',
           @value = N'true';
   END
   ELSE
   BEGIN
       EXEC sys.sp_addextendedproperty
           @name = N'MS_EnablePerformanceMonitoringPreview',
           @value = N'true';
   END;
   GO
   ```

1. Repeat these steps for each database that you want to monitor.

Performance data is usually available to query within 10 minutes after you enable monitoring.

## Verify performance monitoring collection

To check whether monitoring is enabled for a database, run the following query in that database:

```sql
SELECT name,
       CONVERT(NVARCHAR(128), value) AS value
FROM sys.extended_properties
WHERE class = 0
      AND name = N'MS_EnablePerformanceMonitoringPreview';
GO
```

The query returns one of the following results:

| Result  | Description   |
| ------- | ----------------------------------------- |
| `true` | Monitoring is enabled for the database.   |
| `false` | Monitoring is disabled for the database.  |
| No rows | Monitoring isn't set up for the database. |

A value of `true` means that the database is set up for monitoring. To confirm that data is arriving, [query the performance data](#query-performance-data-with-azure-data-explorer) for the database.

## Disable performance monitoring collection

To stop collecting new performance data for a database, run the following script in that database. The script keeps the extended property and sets its value to `false`.

```sql
IF EXISTS (
    SELECT 1
    FROM sys.extended_properties
    WHERE class = 0
      AND name = N'MS_EnablePerformanceMonitoringPreview'
)
BEGIN
    EXEC sys.sp_updateextendedproperty
        @name = N'MS_EnablePerformanceMonitoringPreview',
        @value = N'false';
END
ELSE
BEGIN
    EXEC sys.sp_addextendedproperty
        @name = N'MS_EnablePerformanceMonitoringPreview',
        @value = N'false';
END;
GO
```

To confirm the change, run the [verification query](#verify-performance-monitoring-collection) again and check that the value is `false`.

Disabling monitoring stops new data collection. Data that was already collected isn't deleted.

## View and query performance data

Enabling performance monitoring starts data collection. To view or query the collected data, you need the access prerequisites in this section. All dashboards and query tools read from the same telemetry endpoint, which enforces [Azure role-based access control (Azure RBAC)](/azure/role-based-access-control/overview).

### Access prerequisites

- Your account must be a member of the [Reader](/azure/role-based-access-control/built-in-roles/general#reader) role, or a role with higher privileges, on the subscription that contains the databases you want to query.
- The `Microsoft.AzureArcData` resource provider must be [registered](/azure/azure-resource-manager/management/resource-providers-and-types#register-resource-provider) on each subscription that contains databases you want to query. The resource provider is required only to query the collected data. It isn't required for data collection.

  To register the resource provider, run the following Azure CLI command:

  ```azurecli
  az provider register --namespace Microsoft.AzureArcData
  ```

  To check the registration state, run the following command:

  ```azurecli
  az provider show --namespace Microsoft.AzureArcData --query "registrationState" --output tsv
  ```

  Registration is complete when the command returns `Registered`.

### View performance data in Fabric Database Hub

[Fabric Database Hub](/fabric/database/hub/) provides built-in dashboards that show the performance of your databases in one place. Use the dashboards to review resource utilization, database activity, active sessions, and wait statistics across your database estate, and to drill into an individual database to investigate a performance issue. Fabric Database Hub reads from the same telemetry endpoint described in this article, so the same [access prerequisites](#access-prerequisites) apply.

You can also create a [Real-Time Dashboard](/fabric/real-time-intelligence/dashboard-real-time-create) in Microsoft Fabric that uses the telemetry endpoint as its data source.

### Query performance data with Azure Data Explorer

You can connect directly to the telemetry endpoint and query the performance data by using [KQL](/kusto/query/). Use this option for ad hoc analysis, to build your own queries, or to integrate the data with other tools. For the schema, rules for correct results, and ready-to-run queries, see [Query performance monitoring telemetry](query-performance-monitoring-telemetry.md).

> [!NOTE]
> Use the [Azure Data Explorer web UI](/azure/data-explorer/web-ui-query-overview). The Kusto.Explorer desktop client isn't currently supported.

To connect to the telemetry endpoint:

1. Go to the [Azure Data Explorer web UI](https://dataexplorer.azure.com/).
1. In the **Connections** pane, select **Add**, and then select **Connection**.
1. For **Connection URI**, enter `https://adx.centralus.arcdataservices.com/kusto/`.

   > [!NOTE]
   > Use this connection URI for all databases, regardless of the Azure region where the database is located.

1. Optionally, enter a display name for the connection, and then select **Add**. If prompted, add the URI as a trusted source.
1. Expand the connection, and then select the `ArcSqlTelemetry` database.
1. Select a table, and then use the query window to write and run KQL queries against your performance data.

## Collected datasets

Performance monitoring collects data for Azure SQL Database in the following tables in the `ArcSqlTelemetry` database. For the columns in each table, see [Performance monitoring data schema](query-performance-monitoring-telemetry.md#schema).

| Table      | Data collected                |
| -------------------------------------- | ----------------------------- |
| `SqlServerActiveSessions` | Active sessions               |
| `SqlServerCPUUtilization` | CPU utilization               |
| `SqlServerStorageIO` | Data and log storage I/O      |
| `SqlServerDatabaseStorageUtilization` | Database storage utilization  |
| `SqlServerWaitStats` | Wait statistics               |
| `SqlServerDatabaseProperties` | Database properties           |
| `SqlServerMemoryUtilization` | Memory utilization            |
| `SqlServerPerformanceCountersCommon` | Common performance counters   |
| `SqlServerPerformanceCountersDetailed` | Detailed performance counters |

## Troubleshoot performance monitoring

The following table describes common issues and how to resolve them.

| Issue | Cause | Resolution  |
| ------------------------------------------------------------------------------------------------ | -------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- |
| The verification query returns no rows.  | The extended property wasn't added, or the query ran in a different database.                | Confirm that you're connected to the user database, and then run the enable script.                   |
| The enable script succeeded, but no data appears for the database.   | The script ran in `master` instead of the user database, or data is still being processed.   | Run the verification query in the user database. If the value is `true`, wait at least 10 minutes, and then query the data again. |
| The property is set to `true`, but no data appears for the database. | The database is in an elastic pool, or it's a secondary replica. | Confirm that the database is in a [supported configuration](#supported-configurations).               |
| The enable script fails with a permission error.                     | Your account doesn't have permission to add a database-level extended property.              | Connect with an account that's a member of the **db_owner** role in the user database.               |
| Some databases on a logical server show data and others don't.       | Monitoring is configured per database.                           | Run the enable script in each database that you want to monitor.          |
| Azure Data Explorer can't connect to the endpoint, or queries return no data for your databases. | Your account doesn't have sufficient permissions, or the resource provider isn't registered. | Confirm that your account has the **Reader** role or higher on the subscription, and that `Microsoft.AzureArcData` is registered. |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- If the verification query returns no rows, the extended property wasn't added or the query ran in a different database. Confirm that you're connected to the user database, and then run the enable script.
- If the enable script succeeds but no data appears for the database, the script might have run in `master` instead of the user database, or the data might still be processing. Run the verification query in the user database. If the value is `true`, wait at least 10 minutes, and then query the data again.
- If the property is set to `true` but no data appears for the database, the database might be in an elastic pool or might be a secondary replica. Confirm that the database is in a [supported configuration](#supported-configurations).
- If the enable script fails with a permission error, your account doesn't have permission to add a database-level extended property. Connect with an account that's a member of the **db_owner** role in the user database.
- If some databases on a logical server show data and others don't, monitoring is configured per database. Run the enable script in each database that you want to monitor.
- If Azure Data Explorer can't connect to the endpoint or queries return no data for your databases, your account might not have sufficient permissions or the resource provider might not be registered. Confirm that your account has the **Reader** role or higher on the subscription and that `Microsoft.AzureArcData` is registered.

## Platform support

The `MS_EnablePerformanceMonitoringPreview` database-scoped extended property doesn't enable performance monitoring in SQL Server, Azure SQL Managed Instance, SQL database in Fabric, or Fabric Data Warehouse.

## Related content

- [Query performance monitoring telemetry (preview)](query-performance-monitoring-telemetry.md)
- [Enable performance monitoring for SQL Server on Azure VMs (preview)](../virtual-machines/windows/enable-performance-monitoring-sql-vm.md)
- [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](monitor-tune-overview.md)
- [Monitor Azure SQL Database performance using dynamic management views](monitoring-with-dmvs.md)
- [Kusto Query Language overview](/kusto/query/)
- [sp\_addextendedproperty (Transact-SQL)](/sql/relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql)
- [sys.extended\_properties (Transact-SQL)](/sql/relational-databases/system-catalog-views/extended-properties-catalog-views-sys-extended-properties)
