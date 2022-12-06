---
title: Manage SQL Server databases
description: Manage databases in Azure from an instance of Azure Arc-enabled SQL Server. Use to inventory databases, and view properties of databases centrally, as Arc-enabled resources.
author: ntakru
ms.author: nikitatakru
ms.reviewer: mikeray
ms.date: 11/03/2022
ms.topic: conceptual
ms.custom:
ms.service: sql
---

# Manage SQL Server databases - Azure Arc

You can manage SQL Server databases in Azure.

## Prerequisites

Before you begin, verify that the SQL Server instance that hosts the databases:

* Is hosted on a physical or virtual machine running Windows operating system.
* Is [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], or [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].
* Is connected to Azure Arc. See [Connect your SQL Server to Azure Arc](connect.md).
* Is connected to the internet directly or through a proxy server.

## Inventory databases

1. Locate the Azure Arc-enabled SQL Server instance in Azure portal
1. **Select** the SQL Server resource.
1. Under **Data management**, select **Databases**.

   :::image type="content" source="media/view-databases/databases.png" alt-text="Screenshot of Azure portal, SQL Server databases - Azure Arc.":::

Azure portal shows **SQL Server databases - Azure Arc**. Use this area to view the databases that belong to the instance.

## View database properties

To view database properties for a specific database, select the database on the portal.

After you create, modify, or delete a database, changes are visible in Azure portal within an hour.

:::image type="content" source="media/view-databases/database-properties.png" alt-text="Screenshot of Azure portal, SQL Server database properties.":::

## Next steps

* [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)

* [Configure on-demand SQL assessment for your SQL Server instance](assess.md)
