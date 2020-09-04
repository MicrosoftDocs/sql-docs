---
title: Kusto extension with Azure Data Studio
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

:::image type="content" source="media/kusto-extension/kusto-extension-icon.png" alt-text="Kusto extension":::

## Prerequisites

You need The following prerequisites:

- [Azure Data Studio installed](../download-azure-data-studio.md).
- [Azure Data Explorer cluster](https://docs.microsoft.com/azure/data-explorer/create-cluster-database-portal).

## Install the Kusto extension

To install the Kusto extension in Azure Data Studio, follow the steps below.

1. Open the extensions manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Type in *Kusto* in te search bar.

3. Select the **Kusto (KQL)** extension and view its details.

4. Select **Install**.

5. Select **Reload** to enable the extension. (This is only required the first time you install an extension).

## Extension settings

To change the settings for the Kusto extension, follow the steps below.

1. Open the extension manager in Azure Data Studio. You can either select the extensions icon or select **Extensions** in the View menu.

2. Find the **Kusto (KQL)** extension.

3. Select the **Manage** icon.

4. Select the **Extension Settings** icon.

The extensions settings look like this:

:::image type="content" source="media/kusto-extension/kusto-extension-settings.png" alt-text="Machine Learning extension settings":::

## How to connect to an Azure Data Explorer cluster

To connect to an Azure Data Explorer cluster, follow the steps below.

1. Select **New connection** from the **Connections** pane.

2. Fill in the **Connection Details** information.
    a. For **Connection type**, select *Kusto*.
    b. For **Cluster**, enter in your Azure Data Explorer cluster


## Next steps

- [SQL to Kusto cheat sheet](https://docs.microsoft.com/azure/data-explorer/kusto/query/sqlcheatsheet)
- [Kqlmagic](../notebooks-kqlmagic.md)