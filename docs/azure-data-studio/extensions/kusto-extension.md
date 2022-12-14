---
title: Kusto (KQL) extension for Azure Data Studio
description: This article describes how you can connect and query Azure Data Explorer clusters with Azure Data Studio.
author: markingmyname
ms.author: maghan
ms.reviewer: jukoesma
ms.date: 10/29/2020
ms.service: azure-data-studio
ms.topic: conceptual
---

# Kusto (KQL) extension for Azure Data Studio (Preview)

The Kusto (KQL) extension for [Azure Data Studio](../what-is-azure-data-studio.md) enables you to connect and query to [Azure Data Explorer](/azure/data-explorer/data-explorer-overview) clusters.

Users can write and run KQL queries and author notebooks with the [Kusto kernel](../notebooks/notebooks-kusto-kernel.md) complete with IntelliSense.

By enabling the native Kusto (KQL) experience in Azure Data Studio, data engineers, data scientists, and data analysts can quickly observe trends and anomalies against massive amounts of data stored in Azure Data Explorer.

This extension is currently in preview.

## Prerequisites

If you don't have an Azure subscription, create a [free Azure account](https://azure.microsoft.com/free/) before you begin.

The following prerequisites are also required:

- [Azure Data Studio installed](../download-azure-data-studio.md).
- [An Azure Data Explorer cluster and database](/azure/data-explorer/create-cluster-database-portal).

## Install the Kusto (KQL) extension

To install the Kusto (KQL) extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Type in *Kusto* in the search bar.

3. Select the **Kusto (KQL)** extension and view its details.

4. Select **Install**.

:::image type="content" source="media/kusto-extension/kusto-extension-icon.png" alt-text="Kusto extension":::

## How to connect to an Azure Data Explorer cluster

### Find your Azure Data Explorer cluster

Find your Azure Data Explorer cluster in the [Azure portal](https://ms.portal.azure.com/#home), then find the URI for the cluster.

:::image type="content" source="media/kusto-extension/kusto-extension-adx-cluster-uri.png" alt-text="URI":::

However, you can get started immediately using the *help.kusto.windows.net* cluster.

For this article, we're using data from the help.kusto.windows.net cluster for samples.

### Connection details

To set up an Azure Data Explorer cluster to connect to, follow the steps below.

1. Select **New connection** from the **Connections** pane.

2. Fill in the **Connection Details** information.
    1. For **Connection type**, select *Kusto*.
    2. For **Cluster**, enter in your Azure Data Explorer cluster.

        > [!Note]
        > When entering the cluster name, don't include the `https://` prefix or a trailing `/`.

    3. For **Authentication Type**, use the default - *Azure Active Directory - Universal with MFA account*.
    4. For **Account**, use your account information.
    5. For **Database**, use *Default*.
    6. For **Server Group**, use *Default*.
        1. You can use this field to organize your servers in a specific group.
    7. For **Name (optional)**, leave blank.
        1. You can use this field to give your server an alias.

    :::image type="content" source="media/kusto-extension/kusto-extension-connection-details.png" alt-text="Connection details":::

## How to query an Azure Data Explorer database in Azure Data Studio

Now that you have set up a connection to your Azure Data Explorer cluster, you can query your database(s) using Kusto (KQL).

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

2. Find the **Kusto (KQL)** extension.

3. Select the **Manage** icon.

4. Select the **Extension Settings** icon.

The extensions settings look like this:

:::image type="content" source="media/kusto-extension/kusto-extension-settings.png" alt-text="Kusto (KQL) extension settings":::

## SandDance visualization

The [SandDance extension](sanddance-extension.md) with the Kusto (KQL) extension in Azure Data Studio bring rich interactive visualization together. From the KQL query result set, select the **Visualizer** button to launch [SandDance](https://microsoft.github.io/SandDance/).

:::image type="content" source="media/kusto-extension/kusto-extension-sanddance-demo.gif" alt-text="SandDance visualization":::

## Known issues

| Details | Workaround |
|---------|------------|
| [In Kusto notebook, Changing a database connection on a saved alias connection is stuck after an error in code cell execution](https://github.com/microsoft/azuredatastudio/issues/12384) | Close and reopen the Notebook, then connect to the right cluster with the database |
| [In Kusto Notebook, changing a database connection on a non-saved alias connection doesn't work](https://github.com/microsoft/azuredatastudio/issues/12843) |Create a new connection from Connection Viewlet and save it with an alias. Then create a new notebook and connect to the newly saved connection) |
| [In Kusto Notebook, the database dropdown isn't populated when creating a new ADX connection](https://github.com/microsoft/azuredatastudio/issues/12666) | Create a new connection from Connection Viewlet and save it with an alias. Then create a new notebook and connect to the newly saved connection) |

You can file a [feature request](https://github.com/microsoft/azuredatastudio/issues/new?assignees=&labels=&template=feature_request.md&title=) to provide feedback to the product team.  
You can file a [bug](https://github.com/microsoft/azuredatastudio/issues/new?assignees=&labels=&template=bug_report.md&title=) to provide feedback to the product team.

## Next steps

- [Create and run a Kusto notebook](../notebooks/notebooks-kusto-kernel.md)
- [Kqlmagic notebook in Azure Data Studio](../notebooks/notebooks-kqlmagic.md)
- [SQL to Kusto cheat sheet](/azure/data-explorer/kusto/query/sqlcheatsheet)
- [What is Azure Data Explorer?](/azure/data-explorer/data-explorer-overview)
- [Using SandDance visualizations](https://microsoft.github.io/SandDance/)
