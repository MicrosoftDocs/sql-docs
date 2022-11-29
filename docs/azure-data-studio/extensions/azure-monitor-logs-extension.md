---
title: Azure Monitor Logs extension for Azure Data Studio
description: This article describes how you can connect and query Log Analytics Workspace IDs with Azure Data Studio.
author: markingmyname
ms.author: maghan
ms.reviewer: jukoesma
ms.date: 11/03/2021
ms.service: azure-data-studio
ms.topic: conceptual
---

# Azure Monitor Logs extension for Azure Data Studio (Preview)

The Azure Monitor Logs extension for [Azure Data Studio](../what-is-azure-data-studio.md) enables you to connect and query a [Log Analytics workspace](/azure/azure-monitor/logs/quick-create-workspace).

This extension is currently in preview.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required:

- [Azure Data Studio installed](../download-azure-data-studio.md).
- [Log Analytics workspace](/azure/azure-monitor/logs/data-platform-logs#log-analytics-workspaces).

## Install the Azure Monitor Logs extension

To install the Azure Monitor Logs extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Type in *Monitor logs* in the search bar.

3. Select the **Azure Monitor Logs** extension and view its details.

4. Select **Install**.

:::image type="content" source="media/azure-monitor-logs-extension/azure-monitor-logs-extension-icon.png" alt-text="Kusto extension":::

## How to connect to a Log Analytics workspace

### Find your Log Analytics workspace

Find your **Log Analytics Workspace** in the [Azure portal](https://ms.portal.azure.com/#home), then find the Workspace ID.

:::image type="content" source="media/azure-monitor-logs-extension/azure-monitor-logs-extension-workspace-id.png" alt-text="Azure Monitor Logs Workspace ID":::

### Connection details

To set up an Azure Monitor Log workspace, follow the steps below.

1. Select **New connection** from the **Connections** pane.

2. Fill in the **Connection Details** information.
    1. For **Connection type**, select *Azure Monitor Logs*.
    2. For **Workspace ID**, enter in your Log Analytics Workspace ID.
    3. For **Authentication type**, use the default - *Azure Active Directory - Universal with MFA account*.
    4. For **Account**, use your account information.
    5. For **Database**, select the same Workspace ID.
    6. For **Server group**, use *Default*.
        1. You can use this field to organize your servers in a specific group.
    7. For **Name (optional)**, leave blank.
        1. You can use this field to give your server an alias.

    :::image type="content" source="media/azure-monitor-logs-extension/azure-monitor-logs-extension-connection-details.png" alt-text="Connection details":::

## How to query a Log Analytics Workspace in Azure Data Studio

Now that you have set up a connection to your Log analytics workspace, you can query the workspace using Kusto (KQL).

To create a new query tab, you can either select **File > New Query**, use *Ctrl + N*, or right-click the database and select **New Query**.

Once you have your new query tab open, then enter your Kusto query.

There are two main tables in Azure Log Analytics (Azure Monitor Logs) workspace that capture Azure SQL events for an Azure SQL database:

1. [AzureDiagnostics](/azure/azure-monitor/reference/tables/azurediagnostics#azure-diagnostics-mode)
2. [AzureMetric](/azure/azure-monitor/reference/tables/azuremetrics#resource-types)

To execute the samples below, you must be logged in to an [Azure SQL database](/azure/azure-sql/database/single-database-create-quickstart?tabs=azure-portal).

Here are some samples of KQL queries:

```kusto
AzureDiagnostics
| summarize count() by OperationName
```

```kusto
AzureDiagnostics
| where LogicalServerName_s == "<servername>"
| summarize count() by Category
```

> [!Note]
> Allow about 15 minutes before the log results appear.

For more information about writing Azure Monitor Logs, visit [Azure Monitor documentation](/azure/azure-monitor/)

## Next steps

- [Create and run a notebook with Azure Monitor Logs](../notebooks/notebooks-azure-monitor-logs.md)
- [Create diagnostic settings to send platform logs and metrics to different destinations](/azure/azure-monitor/essentials/diagnostic-settings)
- [SQL to Kusto cheat sheet](/azure/data-explorer/kusto/query/sqlcheatsheet)
- [What is Azure Monitor Logs?](/azure/azure-monitor/logs/data-platform-logs)
- [Using SandDance visualizations](https://microsoft.github.io/SandDance/)
