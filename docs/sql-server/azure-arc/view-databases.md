---
title: View SQL Server databases
description: View databases in Azure from an instance of Azure Arc-enabled SQL Server. Use to inventory databases, and view properties of databases centrally, as Arc-enabled resources.
author: ntakru
ms.author: nikitatakru
ms.reviewer: mikeray
ms.date: 11/03/2022
ms.topic: conceptual
ms.custom:
ms.service: sql
---

# View SQL Server databases - Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

You can inventory and view SQL Server databases in Azure.

## Prerequisites

Before you begin, verify that the SQL Server instance that hosts the databases:

* Is hosted on a physical or virtual machine running Windows operating system.
* Is [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] or later.
* Is connected to Azure Arc. See [Connect your SQL Server to Azure Arc](connect.md).
* Is connected to the internet directly or through a proxy server.
* Has `NT AUTHORITY\SYSTEM` in the `sysadmin` role.

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

* [Protect Azure Arc-enabled SQL Server with Microsoft Defender for Cloud](configure-advanced-data-security.md)

* [Configure SQL Assessment | Azure Arc-enabled SQL Server](assess.md)
