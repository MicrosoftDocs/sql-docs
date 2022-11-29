---
title: Notebooks with Azure Monitor Logs in Azure Data Studio
description: This tutorial shows how you can create and run a notebook with Azure Monitor Logs.
author: markingmyname
ms.author: maghan
ms.reviewer: alayu
ms.date: 11/03/2021
ms.service: azure-data-studio
ms.topic: how-to
---
# Create and run a notebook with Azure Monitor Logs (Preview)

This article shows you how to create and run an [Azure Data Studio notebook](./notebooks-guidance.md) using the [Azure Monitor Log extension](../extensions/azure-monitor-logs-extension.md) to connect to a [Log Analytics workspace](/azure/azure-monitor/logs/log-analytics-overview) to view results for an Azure SQL database.

Once the Azure Monitor Logs extension is installed, you can connect to your Azure Monitor Log workspaces, browse the tables, write/execute KQL queries against workspaces and write/execute Notebooks connected to the Log Analytics kernel.

With the Azure Monitor Log extension, you can change the kernel option to **Log Analytics**.

This feature is currently in preview.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required:

- [Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart?tabs=azure-portal)
- [Azure Data Studio installed](../download-azure-data-studio.md).
- [Log Analytics workspace](/azure/azure-monitor/logs/data-platform-logs#log-analytics-workspaces).
- [Azure Monitor Log extension](../extensions/azure-monitor-logs-extension.md).

## Connect to an Azure Monitor Logs (Log Analytics) workspace

There are two main tables in a Log Analytics (Azure Monitor Logs) workspace that capture Azure SQL events:

In the examples below, this article uses the [AzureDiagnostics](/azure/azure-monitor/reference/tables/azurediagnostics#azure-diagnostics-mode) table and the [AzureMetric](/azure/azure-monitor/reference/tables/azuremetrics#resource-types) table in a Log Analytics workspace, which store some Azure SQL event data. We have previously configured Azure SQL to write its selected events to a Log Analytics workspace. Learn more about how to do this here.

You can connect to a [Log Analytics workspace](/azure/azure-monitor/logs/data-platform-logs#log-analytics-workspaces).

## Create an Azure Monitor Log notebook

The following steps show how to create a notebook file in Azure Data Studio:

1. In Azure Data Studio, connect to your Log Analytics workspace.

2. Navigate to the **Connections** pane and under the **Servers** window, right-click the Log analytics workspace and select *New Notebook*. You can also go to **File** > **New Notebook**.

    :::image type="content" source="media/notebooks-azure-monitor-logs/notebooks-azure-monitor-logs-new-notebook.png" alt-text="Open notebook":::

3. Select *Log Analytics* for the **Kernel**. Confirm that the **Attach to** menu is set to the workspace name.

    :::image type="content" source="media/notebooks-azure-monitor-logs/set-notebooks-azure-monitor-logs-kernel.png" alt-text="Set Kernel and Attach to":::

You can save the notebook using the **Save** or **Save as...** command from the **File** menu.

To open a notebook, you can use the **Open file...** command in the **File** menu, select **Open file** on the **Welcome** page, or use the **File: Open** command from the command palette.

## Change the connection

To change the Azure Monitor Log connection for a notebook:

1. Select the **Attach to** menu from the notebook toolbar and then select **Change Connection**.

   :::image type="content" source="media/notebooks-azure-monitor-logs/notebooks-azure-monitor-logs-select-attach-to-change-connections.png" alt-text="change connections":::

   > [!Note]
   > Ensure that the workspace value is populated. Azure Monitor Log notebooks require to have the Workspace ID specified in the **Server** field.

2. Now, you can either select a recent connection workspace or enter new connection details to connect.

   :::image type="content" source="media/notebooks-azure-monitor-logs/notebooks-azure-monitor-logs-change-connection-cluster.png" alt-text="Select a different cluster":::

## Run a code cell

You can create cells containing KQL queries that you can run in place by selecting the **Run cell** button to the cell's left. The results are shown in the notebook after the cell runs.

For example:

Add a new code cell by selecting the **+Code** command in the toolbar.

   :::image type="content" source="media/notebooks-azure-monitor-logs/notebooks-azure-monitor-logs-kernel-code.png" alt-text="kernel code block":::

## Query log results for an Azure SQL database

You can connect to your Log Analytics workspace(s) to browse tables, write KQL queries against workspaces, and create notebooks connected to the Log Analytics kernel.

> [!Note]
> Allow about 15 minutes before the log results appear.

Some sample queries are listed below.

### AzureMetrics

Here's a sample query to view AzureMetrics results.

```kusto
AzureMetrics
```

   :::image type="content" source="media/notebooks-azure-monitor-logs/azure-metrics-results.png" alt-text="azure metrics code cell results":::

### AzureDiagnostics

Here's a sample query to view AzureDiagnostics results.

The **AzureDiagnostics** table tends to have more details than **AzureMetrics**.

```kusto
AzureDiagnostics
| project-away TenantId, ResourceId, SubscriptionId, _ResourceId, ResourceGroup // hide sensitive info
| take 10
```

   :::image type="content" source="media/notebooks-azure-monitor-logs/azure-diagnostics-results.png" alt-text="azure diagnostics code cell results":::

### Analyze events by Diagnostic Settings

Here's a query to analyze the number of events by Operation Name.

> [!Note]
> Each row in AzureDiagnostic represents an event for a specific Operation or category. Some SQL actions may result in generating multiple events of different types.

```kusto
AzureDiagnostics
| summarize count() by OperationName
```

   :::image type="content" source="media/notebooks-azure-monitor-logs/azure-diagnostics-summarize results-operation-name.png" alt-text="azure diagnostics summarize code cell results":::

You can try some more examples from the [Azure Monitor Logs samples repo](https://github.com/MsSQLGirl/jubilant-data-wizards/blob/main/Simple%20Demo/KQL%20Notebooks/AzureMonitorLogsSample.ipynb).

## Next steps

- [Azure Monitor Log extension](../extensions/azure-monitor-logs-extension.md)
- [Azure Monitor Log samples](https://github.com/MsSQLGirl/jubilant-data-wizards/blob/main/Simple%20Demo/KQL%20Notebooks/AzureMonitorLogsSample.ipynb)
- [Notebook guidance](notebooks-guidance.md)