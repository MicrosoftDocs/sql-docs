---
title: Azure Monitor Logs extension for Azure Data Studio
description: This article describes how you can connect and query Log Analytics Workspace IDs with Azure Data Studio.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: jukoesma
ms.custom: 
ms.date: 08/18/2021
---

# Azure Monitor Logs extension for Azure Data Studio (Preview)

The Azure Monitor Logs extension for [Azure Data Studio](../what-is-azure-data-studio.md) enables you to connect and query a [Log Analytics workspace](/azure/azure-monitor/essentials/quick-collect-activity-log-portal) clusters.

Users can write and run KQL queries and author notebooks with the Log Analytics kernel complete with IntelliSense.

By enabling the native Azure Monitor Logs experience in Azure Data Studio, data engineers, data scientists, and data analysts can quickly observe trends and anomalies against massive amounts of data stored in and Azure Data Explorer  cluster with a Log Analytics Workspace.

This extension is currently in preview.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required:

- [Azure Data Studio installed](../download-azure-data-studio.md).
- [An Azure Monitor Logs Workspace](/azure/azure-sql/database/metrics-diagnostic-telemetry-logging-streaming-export-configure?tabs=azure-portal#stream-into-sql-analytics).

## Install the Azure Monitor Logs extension

To install the Azure Monitor Logs extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Type in *Monitor logs* in the search bar.

3. Select the **Azure Monitor Logs** extension and view its details.

4. Select **Install**.

:::image type="content" source="media/azure-monitor-logs-extension/azure-monitor-logs-extension-icon.png" alt-text="Kusto extension":::

## How to connect to a Azure Monitor Log workspace

### Find your Azure Monitor Log workspace

Find your **Log Analytics Workspace** in the [Azure portal](https://ms.portal.azure.com/#home), then find the Workspace ID for the Workspace.

:::image type="content" source="media/azure-monitor-logs-extension/azure-monitor-logs-extension-workspace-id.png" alt-text="Azure Monitor Logs Workspace ID":::

### Connection details

To set up an Azure Monitor Log workspace, follow the steps below.

1. Select **New connection** from the **Connections** pane.

2. Fill in the **Connection Details** information.
    1. For **Connection type**, select *Azure Monitor Logs*.
    2. For **Workspace ID**, enter in your Log Analytics Workspace ID.
    3. For **Authentication Type**, use the default - *Azure Active Directory - Universal with MFA account*.
    4. For **Account**, use your account information.
    5. For **Database**, use the same Workspace ID from te Workspace ID section.
    6. For **Server Group**, use *Default*.
        1. You can use this field to organize your servers in a specific group.
    7. For **Name (optional)**, leave blank.
        1. You can use this field to give your server an alias.

    :::image type="content" source="media/azure-monitor-logs-extension/azure-monitor-logs-extension-connection-details.png" alt-text="Connection details":::

## How to query an Azure Data Explorer database in Azure Data Studio

Now that you have set up a connection to your Log Analytics Workspace ID, you can query your database(s) using Azure Monitor Logs.

To create a new query tab, you can either select **File > New Query**, use *Ctrl + N*, or right-click the database and select **New Query**.

Once you have your new query tab open, then enter your Kusto query.

Here are some samples of KQL queries:

```kusto
StormEvents
| limit 1000
```

```kusto
StormEvents
| where EventType == "Waterspout"
```

For more information about writing KQL queries, visit [Write queries for Azure Data Explorer](/azure/data-explorer/write-queries#overview-of-the-query-language)

## View extension settings

To change the settings for the Kusto extension, follow the steps below.

1. Open the extension manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Find the **Azure Monitor Logs** extension.

3. Select the **Manage** icon.

4. Select the **Extension Settings** icon.

The extensions settings look like this:

:::image type="content" source="media/azure-monitor-logs-extension/azure-monitor-logs-extension-settings.png" alt-text="Azure Monitor Logs extension settings":::

## Known issues


## Next steps

- [Create and run a Kusto notebook](../notebooks/notebooks-kusto-kernel.md)
- [Kqlmagic notebook in Azure Data Studio](../notebooks/notebooks-kqlmagic.md)
- [SQL to Kusto cheat sheet](/azure/data-explorer/kusto/query/sqlcheatsheet)
- [What is Azure Data Explorer?](/azure/data-explorer/data-explorer-overview)
- [Using SandDance visualizations](https://sanddance.js.org/)
