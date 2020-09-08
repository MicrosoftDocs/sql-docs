---
title: Kusto extension for Azure Data Studio
description: This article describes how you can connect and query Azure Data Explorer clusters with Azure Data Studio.
ms.topic: conceptual
ms.prod: azure-data-studio
ms.technology: azure-data-studio
author: markingmyname
ms.author: maghan
ms.reviewer: jukoesma
ms.custom: ""
ms.date: 09/22/2020
---

# Kusto extension for Azure Data Studio (Preview)

The Kusto extension for [Azure Data Studio](../what-is.md) enables you to connect and query to [Azure Data Explorer](https://docs.microsoft.com/azure/data-explorer/data-explorer-overview) clusters. This extension is currently in preview.

## Prerequisites

If you don't have an Azure subscription, create a free Azure account before you begin.

You also need The following prerequisites:

- [Azure Data Studio installed](../download-azure-data-studio.md).
- [An Azure Data Explorer cluster and database](https://docs.microsoft.com/azure/data-explorer/create-cluster-database-portal).

## Install the Kusto extension

To install the Kusto extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Type in *Kusto* in te search bar.

3. Select the **Kusto (KQL)** extension and view its details.

4. Select **Install**.

:::image type="content" source="media/kusto-extension/kusto-extension-icon.png" alt-text="Kusto extension":::

## Extension settings

To change the settings for the Kusto extension, follow the steps below.

1. Open the extension manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Find the **Kusto (KQL)** extension.

3. Select the **Manage** icon.

4. Select the **Extension Settings** icon.

The extensions settings look like this:

:::image type="content" source="media/kusto-extension/kusto-extension-settings.png" alt-text="Machine Learning extension settings":::

## How to connect to an Azure Data Explorer cluster

### Find your Azure Data Explorer cluster

Find your Azure Data Explorer cluster in the [Azure portal](https://ms.portal.azure.com/#home), then find the URI for the cluster.

:::image type="content" source="media/kusto-extension/kusto-extension-adx-cluster-uri-03.png" alt-text="URI":::

However, you can get started immediately using the *help.kusto.windows.net* cluster.

For this article, we are using the help.kusto.windows.net cluster for examples.

### Connection details

To set up the Azure Data Explorer cluster you connect to, follow the steps below.

1. Select **New connection** from the **Connections** pane.

2. Fill in the **Connection Details** information.
    1. For **Connection type**, select *Kusto*.
    2. For **Cluster**, enter in your Azure Data Explorer cluster.

        > [!Note]
        > When entering the cluster name, don't include the *https://* prefix or a trailing */*.

    3. For **Authentication Type**, use the default - *Azure Active Directory - Universal with MFA account*.
    4. For **Account**, use your account information.
    5. For **Database**, use *Default*.
    6. For **Server Group**, use *Default*.
        1. You can use this field to organize your servers in a specific group.
    7. For **Name (optional)**, leave blank.
        1. You can use this field to give your server an alias.

    :::image type="content" source="media/kusto-extension/kusto-extension-connection-details.png" alt-text="Connection details":::

## How to query a Kusto database in Azure Data Studio

Now that you have set up a connection to your Azure Data Explorer cluster, you can query your database(s) using Kusto (KQL).

To create a new query tab, you can either select **File > New Query**, use *Ctrl + N*, or ri the database and select **New Query**.

Once you have your new query tab open, then enter your kusto query.

Here are some samples of Kusto queries:

```kusto
StormEvents
| limit 1000
```

```kusto
StormEvents
| where EventType == "Waterspout"
```

For more information about writing Kusto queries, visit [Write queries for Azure Data Explorer](https://docs.microsoft.com/en-us/azure/data-explorer/write-queries#overview-of-the-query-language)

## Next steps

- [Kqlmagic](../notebooks-kqlmagic.md)
- [SQL to Kusto cheat sheet](https://docs.microsoft.com/azure/data-explorer/kusto/query/sqlcheatsheet)
- [What is Azure Data Explorer?](https://docs.microsoft.com/azure/data-explorer/data-explorer-overview)