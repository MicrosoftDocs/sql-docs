---
title: Create a database watcher to monitor Azure SQL
titleSuffix: Azure SQL Database & SQL Managed Instance
description: Learn how to create a new database watcher to monitor an Azure SQL database, elastic pool, or SQL managed instance using Microsoft Entra authentication and private connectivity.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf
ms.date: 04/08/2024
ms.service: sql-db-mi
ms.subservice: monitoring
ms.topic: quickstart
ms.custom:
  - subject-monitoring
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---

# Quickstart: Create a database watcher to monitor Azure SQL (preview)

This article shows you how to create a new database watcher to monitor an Azure SQL database, elastic pool, or SQL managed instance. For an introduction to database watcher, see [Monitor Azure SQL workloads with database watcher](database-watcher-overview.md). For a detailed description of database watcher setup and configuration, see [Create and configure a database watcher](database-watcher-manage.md).

Follow the steps to create a database watcher and start monitoring your Azure SQL resources in minutes. The watcher you create uses Microsoft Entra authentication and private connectivity to the monitoring targets.

> [!NOTE]
> Database watcher is currently in preview. Preview features are released with limited capabilities, but are made available on a *preview* basis so customers can get early access and provide feedback. Preview features are subject to separate [supplemental preview terms](https://go.microsoft.com/fwlink/?linkid=2240967), and aren't subject to SLAs. Support is provided as best effort in certain cases. However, Microsoft Support is eager to get your feedback on the preview functionality, and might provide best effort support in certain cases. Preview features might have limited or restricted functionality, and might be available only in selected geographic areas.

## Prerequisites

This quickstart requires the [prerequisites for creating and configuring a database watcher](database-watcher-manage.md#prerequisites).

## Create a watcher

1. In Azure portal, in the resource menu, select **All services**.

    :::image type="content" source="media/database-watcher-quickstart/all-services.png" alt-text="Screenshot of the main menu of Azure portal with the all services menu item highlighted." lightbox="media/database-watcher-quickstart/all-services.png":::

1. Select **Monitor** as the category. Under **Monitoring tools**, select **Database watchers**. In the **Database watchers** view, select **Create**.

    :::image type="content" source="media/database-watcher-quickstart/database-watchers-create.png" alt-text="Screenshot of the database watchers view with the Create button highlighted." lightbox="media/database-watcher-quickstart/database-watchers-create.png":::

1. On the **Basics** tab, select the subscription and resource group for the watcher, enter the name of the watcher, and select an Azure region where you want to create the watcher.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-create-basics.png" alt-text="Screenshot of the Basics page for a new database watcher being created, with the subscription, resource group, watcher name, and watcher region filled in." lightbox="media/database-watcher-quickstart/database-watcher-create-basics.png":::

1. Select **Review + create**, review the watcher and data store configuration. Select **Create**.

    This step creates a database watcher, an Azure Data Explorer cluster, and a database on the cluster. Collected monitoring data is stored in this database.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-review-create.png" alt-text="Screenshot of the Review and create page for a new database watcher being created, with the watcher and data store details shown for review." lightbox="media/database-watcher-quickstart/database-watcher-review-create.png":::

## Add a SQL target and grant access

Once the deployment completes, select **Go to resource** to open the new database watcher in Azure portal.

1. Select **SQL Targets**, **Add**. Use the dropdowns to select the Azure SQL resource that you want to monitor, and select **Add**. You can select an Azure SQL database, an elastic pool, or a SQL managed instance. In this example, an Azure SQL database is selected.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-add-target.png" alt-text="Screenshot of the add target page of a database watcher, showing an Azure SQL database ready to be added as a target." lightbox="media/database-watcher-quickstart/database-watcher-add-target.png":::

1. In the **Grant access** card, select a Microsoft Entra authentication link matching your target type. Copy the T-SQL script that grants the watcher limited, specific access to collect monitoring data from the selected target.

    The following screenshot shows the T-SQL script for Azure SQL databases and elastic pools.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-grant-access-sql-database.png" alt-text="Screenshot of the targets page of a database watcher, showing the T-SQL script that grants access to a database watcher on a SQL database or an elastic pool, and a button that copies it to the clipboard." lightbox="media/database-watcher-quickstart/database-watcher-grant-access-sql-database.png":::

    The following screenshot shows the T-SQL script for Azure SQL managed instances.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-grant-access-sql-managed-instance.png" alt-text="Screenshot of the targets page of a database watcher, showing the T-SQL script that grants access to a database watcher on a SQL managed instance, and a button that copies it to the clipboard." lightbox="media/database-watcher-quickstart/database-watcher-grant-access-sql-managed-instance.png":::

1. Open a new query window in SQL Server Management Studio or Azure Data Studio. Connect to the `master` database on the Azure SQL logical server or SQL managed instance you selected. Paste the T-SQL script, and select **Execute**.

    The following screenshot shows the T-SQL script for Azure SQL databases and elastic pools.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-grant-access-t-sql-sql-database.png" alt-text="Screenshot of a SQL Server Management Studio query window that is connected to the master database on an Azure SQL logical server, showing a successfully executed T-SQL script that grants access to a database watcher." lightbox="media/database-watcher-quickstart/database-watcher-grant-access-t-sql-sql-database.png":::

    The following screenshot shows the T-SQL script for Azure SQL managed instances.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-grant-access-t-sql-sql-managed-instance.png" alt-text="Screenshot of a SQL Server Management Studio query window that is connected to the master database on an Azure SQL managed instance, showing a successfully executed T-SQL script that grants access to a database watcher." lightbox="media/database-watcher-quickstart/database-watcher-grant-access-t-sql-sql-managed-instance.png":::

## Create and approve a managed private endpoint

1. On the **Managed private endpoints** page of the watcher, select **Add**.
1. Enter the private endpoint name, select the subscription for your Azure SQL target, the resource type, and the same logical server or SQL managed instance that you selected when adding the target.
1. For resource type, use `Microsoft.Sql/servers` for a logical server, and `Microsoft.Sql/managedInstances` for a SQL managed instance.
1. For target sub-resource, use `sqlServer` for a logical server, and `managedInstance` for a SQL managed instance.
1. Select **Create**.

    In this example, a private endpoint for an Azure SQL logical server named `watcher-example` is ready to be created. Creating a private endpoint might take one or two minutes.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-add-managed-private-endpoint.png" alt-text="Screenshot of the managed private endpoints page of a database watcher, showing a ready-to-be-created private endpoint for an Azure SQL logical server." lightbox="media/database-watcher-quickstart/database-watcher-add-managed-private-endpoint.png":::

1. In the Azure portal, search for **Private endpoints**. Under **Pending connections**, find the private endpoint you created, and select **Approve**.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-private-access-approve.png" alt-text="Screenshot of the pending connections page in the private link center, showing a ready-to-be-approved database watcher private endpoint." lightbox="media/database-watcher-quickstart/database-watcher-private-access-approve.png":::

## Start monitoring

1. On the watcher **Overview** page, select **Start**.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-start.png" alt-text="Screenshot of the overview page of a database watcher that is stopped, showing the enabled Start button." lightbox="media/database-watcher-quickstart/database-watcher-start.png":::

1. The watcher status changes from **Stopped** to **Running**. It takes a few minutes for data collection to start. 

1. Select **Dashboards**. Refresh the dashboard if the resources do not appear yet.

1. Once the SQL target appears on the corresponding tile, select the tile, and select the link to the database, elastic pool, or SQL managed instance to open its monitoring dashboard. You can also select the link on the tile to open the estate dashboard that shows all monitored Azure SQL resources.

    :::image type="content" source="media/database-watcher-quickstart/database-watcher-dashboard.png" alt-text="Screenshot of the dashboards page of a database watcher, showing a monitored target and a link to open the monitoring dashboard for this target." lightbox="media/database-watcher-quickstart/database-watcher-dashboard.png":::

For a more detailed description of database watcher setup and configuration, see [Create and configure a database watcher](database-watcher-manage.md). You'll learn how to use a database on an existing Azure Data Explorer cluster, on a free Azure Data Explorer cluster, or in Real-Time Analytics in Microsoft Fabric, how to use SQL authentication, how to manage watcher access to SQL targets and the data store, and how to scale and manage your Azure Data Explorer cluster over time.

## Related content

- [Monitor Azure SQL workloads with database watcher (preview)](database-watcher-overview.md)
- [Create and configure a database watcher (preview)](database-watcher-manage.md)
- [Database watcher data collection and datasets (preview)](database-watcher-data.md)
- [Analyze database watcher monitoring data (preview)](database-watcher-analyze.md)
- [Database watcher FAQ](database-watcher-faq.yml)
