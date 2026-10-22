---
title: Specify a Breakpoint Filter
titleSuffix: T-SQL debugger
description: Learn how to implement a breakpoint filter to limit the breakpoint to acting only when debugging is on specified computers, operating system processes, and threads.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# Specify a breakpoint filter

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A breakpoint filter limits the breakpoint to acting only on specified computers, operating system processes, and threads. Breakpoint filters are typically used when debugging parallel applications.

## Filter considerations

Breakpoint filters aren't typically used with the [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger because [!INCLUDE [tsql](../../includes/tsql-md.md)] scripts and stored procedures aren't parallel applications.

### Specify breakpoint filter

1. In the editor window, right-click the breakpoint glyph, and then select **Conditions...** on the shortcut menu.

   -or-

   In the **Breakpoints** window, right-click the breakpoint glyph, and then select **Settings** on the shortcut menu.

1. In the **Breakpoint Settings** dialog box, select the **Conditions** options and select **Filter** from the dropdown list.

1. Use the **Filter** box to specify computers by name, or operating system processes and threads by either name or ID number:

   - **MachineName** is the computer running the instance of the Database Engine.

   - **ProcessID**, and **ProcessName** are the operating system process running the instance of the Database Engine.

   - **ThreadID** and **ThreadName** are the operating system thread running the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch, procedure, or function in the instance of the Database Engine.

1. Select **Close** to implement the changes.

## Related content

- [Specify a breakpoint condition](specify-breakpoint-condition.md)
- [Specify a hit count](specify-hit-count.md)
- [Specify a breakpoint action](specify-breakpoint-action.md)
