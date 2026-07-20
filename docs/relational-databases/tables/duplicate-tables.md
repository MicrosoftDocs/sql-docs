---
title: "Duplicate Tables"
description: "Create a duplicate copy of a table in the SQL Database Engine."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/07/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "copying tables"
  - "tables [SQL Server], duplicating"
  - "duplicating tables"
  - "duplicating table structures"
  - "table copying [SQL Server]"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Duplicate tables

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

You can duplicate an existing table in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] by creating a new table and then copying column information from an existing table.  

These steps described duplicate only the structure of a table, not the row data.  

<a id="Permissions"></a>

<a id="Security"></a>

## Permissions

Requires `CREATE TABLE` permission in the destination database.  

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

<a id="to-duplicate-a-table"></a>

#### Duplicate a table

1. Make sure you are connected to the database in which you want to create the table and that the database is selected in **Object Explorer**.  

1. In **Object Explorer**, right-click **Tables** and select **New** and then **Table...**.  

1. In **Object Explorer** right-click the table you want to copy, and select **Design**. The existing table opens in a separate tab.

1. Select the columns in the existing table and, from the **Edit** menu, select **Copy**, or `Ctrl+C` to copy the column information to your clipboard.

1. Switch back to the new table tab and select the first column of the first row.  

1. From the **Edit** menu, select **Paste** or `Ctrl+V` to paste.

1. From the **File** menu, select **Save** _table name_, or `Ctrl+S` to save.

1. In the **Choose Name** dialog box, type a name for the new table. Select **OK**. The table will be created and visible in the **Object Explorer**.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

<a id="to-duplicate-a-table-in-query-editor"></a>

#### Duplicate a table in Query Editor

1. Make sure you are connected to the database in which you want to create the table and that the database is selected in **Object Explorer**.  

1. Right-click the table you wish to duplicate, point to **Script Table as**, then point to **CREATE to**, and then select **New Query Editor Window**.  

1. Change the name of the table.  

1. Remove any columns that are not needed in the new table.  

1. Select **Execute** to create the new table. The table will be created and visible in the **Object Explorer**.

## Related content

- [Copy Columns from One Table to Another (Database Engine)](copy-columns-from-one-table-to-another-database-engine.md)
- [Script objects in SQL Server Management Studio](/ssms/tutorials/scripting-ssms)
