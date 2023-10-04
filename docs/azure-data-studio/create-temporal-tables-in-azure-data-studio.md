---
title: Creating system-versioned tables in Azure Data Studio
description: How to use the Table Designer to create a system-versioned table
author: tdoshin
ms.author: timioshin
ms.reviewer: maghan
ms.date: 09/24/2022
ms.service: azure-data-studio
ms.topic: tutorial
---

# System-Versioned Tables

[!INCLUDE [sql-asdb-asdbmi](../includes/applies-to-version/sql-asdb-asdbmi.md)]

system-versioned tables, also known as temporal tables, can also be configured directly on Azure Data Studio. If you're new to system versioning, check out the [temporal tables on SQL Server documentation.](../relational-databases/tables/creating-a-system-versioned-temporal-table.md) system-versioning tables must have the period columns defined.

## Creating a system-versioned Table

1. Create a table called "Department" with the column properties as seen below. To do this, right-click on the Tables folder in the Connections pane and select "New Table". Next, in the Table Properties pane, select the "System Versioning Enabled" check box. You can decide to rename this history table or leave the default name as is.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-create-a-system-versioned-table-column-properties.png" alt-text="Screenshot of Table Designer showing how to create a system-versioned-table.":::

2. The period columns, ***ValidFrom*** and ***ValidTo*** are enabled by default on system-versioned tables. The script pane will now be updated to show the T-SQL version of this system-versioned table.

    :::image type="content" source="media/table-designer-azure-data-studio/table-designer-system-versioning-row-start.png" alt-text="Screenshot of Table Designer showing how to define period columns.":::

> [!NOTE]
> system-versioned tables require a primary key.

## Next steps

- [Download Azure Data Studio](./download-azure-data-studio.md)