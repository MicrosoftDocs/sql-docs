---
title: Install big data tools for SQL Server 2019 preview | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/02/2018
ms.topic: conceptual
ms.prod: sql
---

# Install big data tools for SQL Server 2019 preview

This article describes how to install Azure Data Studio and the SQL Server 2019 extension (preview). This includes preview support for [SQL Server 2019 big data clusters](big-data-cluster-overview.md), an integrated [notebook experience](notebooks-guidance.md), and a PolyBase [Create External Table wizard](../relational-databases/polybase/data-virtualization.md?toc=%2fsql%2fbig-data-cluster%2ftoc.json).

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Install the big data tools

1. [Download and install the latest version of Azure Data Studio](../azure-data-studio/download.md).

1. Then [install the SQL Server 2019 extension (preview)](../azure-data-studio/sql-server-2019-extension.md).

## Connect to the cluster

In [!INCLUDE [Azure Data Studio](../includes/name-sos-short.md)], press **F1** -> **New Connection**. You can now connect to your SQL Server (for example: **\<IP Address\>,31433**). Connect to the **high_value_data** database.

![Connect to cluster](./media/deploy-big-data-tools/connect-to-cluster.png)

Click Connect, and the **Server Dashboard** will appear.   You are now connected and can run queries and interact with the cluster.

## Next steps

To run notebooks in Azure Data Studio, see [How to use notebooks in SQL Server 2019 preview](notebooks-guidance.md).
