---
title: Notebooks with Kusto Kernel in Azure Data Studio
description: This tutorial shows how you can create and run a Kusto notebook.
author: markingmyname
ms.author: maghan
ms.reviewer: jukoesma
ms.date: 09/22/2020
ms.service: azure-data-studio
ms.topic: how-to
---

# Create and run a Kusto (KQL) notebook (Preview)

This article shows you how to create and run an [Azure Data Studio notebook](./notebooks-guidance.md) using the [Kusto (KQL) extension](../extensions/kusto-extension.md), connecting to an Azure Data Explorer cluster.

With the Kusto (KQL) extension, you can change the kernel option to **Kusto**.

This feature is currently in preview.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

- [An Azure Data Explorer cluster with a database that you can connect to](/azure/data-explorer/create-cluster-database-portal).
- [Azure Data Studio](../download-azure-data-studio.md).
- [Kusto (KQL) extension for Azure Data Studio](../extensions/kusto-extension.md).

## Create a Kusto (KQL) notebook

The following steps show how to create a notebook file in Azure Data Studio:

1. In Azure Data Studio, connect to your Azure Data Explorer cluster.

2. Navigate to the **Connections** pane and under the **Servers** window, right-click the Kusto database and select *New Notebook*. You can also go to **File** > **New Notebook**.

   :::image type="content" source="media/notebooks-kusto-kernel/kusto-new-notebook.png" alt-text="Open notebook":::

3. Select *Kusto* for the **Kernel**. Confirm that the **Attach to** menu is set to the cluster name and database. For this article, we use the help.kusto.windows.net cluster with the Samples database data.

   :::image type="content" source="media/notebooks-kusto-kernel/set-kusto-kernel.png" alt-text="Set Kernel and Attach to":::

You can save the notebook using the **Save** or **Save as...** command from the **File** menu.

To open a notebook, you can use the **Open file...** command in the **File** menu, select **Open file** on the **Welcome** page, or use the **File: Open** command from the command palette.

## Change the connection

To change the Kusto connection for a notebook:

1. Select the **Attach to** menu from the notebook toolbar and then select **Change Connection**.

   :::image type="content" source="media/notebooks-kusto-kernel/kusto-select-attach-to-change-connections.png" alt-text="change connections":::

   > [!Note]
   > Ensure that the database value is populated. Kusto notebooks require to have the database specified.

2. Now you can either select a recent connection server or enter new connection details to connect.

   :::image type="content" source="media/notebooks-kusto-kernel/kusto-change-connection.png" alt-text="Select a different cluster":::

   > [!Note]
   > Specify the cluster name without the `https://`.

## Run a code cell

You can create cells containing KQL queries that you can run in place by selecting the **Run cell** button to the cell's left. The results are shown in the notebook after the cell runs.

For example:

1. Add a new code cell by selecting the **+Code** command in the toolbar.

   :::image type="content" source="media/notebooks-kusto-kernel/kusto-kernel-code.png" alt-text="Kusto kernel code block":::

2. Copy and paste the following example into the cell and select **Run cell**. This example queries the StormEvents data for a specific event type.

   ```kusto
    StormEvents
    | where EventType == "Waterspout"
   ```

   :::image type="content" source="media/notebooks-kusto-kernel/run-kusto-notebook-cell.png" alt-text="Run cell":::

## Save the result or show chart

If you run a script that returns a result, you can save that result in different formats using the toolbar displayed above the result.

- Save As CSV
- Save As Excel
- Save As JSON
- Save As XML
- Show Chart

```kusto
    StormEvents
    | limit 10
```

:::image type="content" source="media/notebooks-kusto-kernel/run-notebook-save-results.png" alt-text="Save result":::

## Known issues

| Details | Workaround |
|---------|------------|
| [Query result only shows column headers](https://github.com/microsoft/azuredatastudio/issues/12565). | N/A |

You can file a [feature request](https://github.com/microsoft/azuredatastudio/issues/new?assignees=&labels=&template=feature_request.md&title=) to provide feedback to the product team.  
You can file a [bug](https://github.com/microsoft/azuredatastudio/issues/new?assignees=&labels=&template=bug_report.md&title=) to provide feedback to the product team.

## Next steps

Learn more about notebooks:

- [Kusto (KQL) extension for Azure Data Studio](../extensions/kusto-extension.md)
- [How to use notebooks in Azure Data Studio](./notebooks-guidance.md)
- [Create and run a Python notebook](./notebooks-python-kernel.md)
- [Create and run a SQL Server notebook](./notebooks-sql-kernel.md)
