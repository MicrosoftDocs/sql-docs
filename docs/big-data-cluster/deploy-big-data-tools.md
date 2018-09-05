---
title: Install big data tools for SQL Server vNext | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/06/2018
ms.topic: conceptual
ms.prod: sql
---

# Install big data tools for SQL Server vNext

This article describes how to install big data tools for SQL Server vNext.

## Install [!INCLUDE [SQL Operations Studio](../includes/name-sos-short.md)]

1. Download the version of [!INCLUDE [SQL Operations Studio](../includes/name-sos-short.md)] for your OS from [https://github.com/Microsoft/sqlservervnext/releases/tag/v0.1.8](https://github.com/Microsoft/sqlservervnext/releases/tag/v0.1.8).

1. Unzip the file and run the application. We recommend using 7zip on Windows.

## Install the SQL Server scale-out data management extension

1. Download the extension from [https://github.com/Microsoft/sqlservervnext/releases/tag/v0.1.8](https://github.com/Microsoft/sqlservervnext/releases/tag/v0.1.8).

1. In [!INCLUDE [SQL Operations Studio](../includes/name-sos-short.md)] press F1 to open the action bar and type "Extensions: Install from VSIX", and then press enter to select this action.

1. Choose the VSIX file you just downloaded and click **Install**.

1. You will be prompted to reload [!INCLUDE [SQL Operations Studio](../includes/name-sos-short.md)] – click the **Reload** button as requested.

## Connect to the cluster

In [!INCLUDE [SQL Operations Studio](../includes/name-sos-short.md)], press F1 -> New Connection. You can now connect to your SQL Server (Example: **\<IP Address\>,31433**). Connect to the **high_value_data** database.

![Connect to cluster](./media/deploy-big-data-tools/connect-to-cluster.png)

Click Connect, and the **Server Dashboard** will appear.   You are now connected and can run queries and interact with the cluster.

## Next steps

To explore different ways to interact with your cluster, see [Quickstart: Get started with big data on SQL Server vNext](quickstart-big-data-cluster-get-started.md).
