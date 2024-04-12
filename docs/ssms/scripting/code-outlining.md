---
title: "Code outlining"
description: Learn how to use the code outlining feature in the SQL Server Management Studio query editors to selectively hide code.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/11/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "Query Editor [SQL Server Management Studio], outlining code"
  - "Query Editor [SQL Server Management Studio], hiding code"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Code outlining

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can use the outlining feature in the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] query editors to selectively hide code when you edit queries. This enables you to more easily view the code you're working on, especially in large query files.

## Outlining overview

By default, all code is visible when you open a query editor window. Regions of the code can be collapsed to hide it from view. A vertical line on the left edge of the editor window uses a square with a minus sign (`-`) to identify the start of each collapsible code region. When you select a minus sign, the text of the code region is replaced with a box that contains three periods (`...`), and the minus sign changes to a plus sign (`+`). When you select a plus sign, the collapsed code appears and the plus sign changes to a minus sign. When you move the pointer over a box that has three periods, a tooltip appears that shows the code in the collapsed section.

## Enable or disable code outlining

You can manage this setting by navigating to **Tools** > **Options**, expanding the **Text Editor** section, and selecting **IntelliSense**. To disable code outlining, clear the **Outline statements** checkbox. Code outlining is enabled by default.

## System outline regions

Each SQL Server Management Studio editor generates a set of default, system-defined outline regions.

The MDX and DMX code editors create outline regions for each multiline statement. This is the only level of outlining that these editors support.

### Analysis Services XMLA query editor regions

The [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] XMLA Query Editor generates an outline region for each multiline XML attribute. The editor nests the outline regions for nested tags. For example, the XMLA Editor creates three outline regions for the following document.

:::image type="content" source="../../ssms/scripting/media/editoutlinexmlfull.gif" alt-text="Screenshot of XML code showing outlining.":::

When you select the minus sign on the `<InnerTag>` line, just the `InnerTag` is collapsed, as shown in the following illustration.

:::image type="content" source="../../ssms/scripting/media/editoutlinexmlinnercol.gif" alt-text="Screenshot of XML code with inner node hidden.":::

When you move the pointer over the box that has the three periods (`...`), the code in the collapsed region appears in a tooltip, as shown in the following illustration.

:::image type="content" source="../../ssms/scripting/media/editoutlinexmlmouse.gif" alt-text="Screenshot of XML code with tooltip showing hidden code.":::

When you select the minus sign on the `<MiddleTag>` line, both the `MiddleTag` and `InnerTag` are collapsed, as shown in the following illustration.

:::image type="content" source="../../ssms/scripting/media/editoutlinexmlmiddlecol.gif" alt-text="Screenshot of XML code with inner and middle tags hidden.":::

When you select the minus sign on the `<OuterTag>` line, all three lines are collapsed, as shown in the following illustration.

:::image type="content" source="../../ssms/scripting/media/editoutlinexmloutercol.gif" alt-text="Screenshot of XML code showing all three tags hidden.":::

### Database Engine query editor regions

The [!INCLUDE [ssDE](../../includes/ssde-md.md)] Query Editor generates outline regions for each element in the following hierarchy:

1. Batches. The first batch is the code from the start of the file to either the first `GO` command or the end of the file when there are no `GO` commands. After the first `GO`, there's one batch from each `GO` command to either the next `GO` command or the end of the file.

1. Blocks delimited by the following keywords:

   - `BEGIN` - `END`
   - `BEGIN TRY` - `END TRY`
   - `BEGIN CATCH` - `END CATCH`

1. Multiline statements.

For example, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] Query Editor creates three outline regions for the following query:

```sql
CREATE PROCEDURE Sales.SampleProc --Outline region 1
AS
BEGIN --Outline region 2
    SELECT GETDATE() AS TimeOfQuery;

    SELECT * --Outline region 3
    FROM sys.transmission_queue;

    SELECT @@VERSION;
END;
GO
```

You can select the minus sign on the `SELECT *` line to collapse just that `SELECT` statement. To collapse the whole `BEGIN - END` block, select the minus sign on the `BEGIN` line. To collapse the whole batch to the `GO` command, select the minus sign on the `CREATE PROCEDURE` line. You can't collapse the `SELECT GETDATE()` or `SELECT @@VERSION` lines individually because they are single line statements and don't get outlining regions.
