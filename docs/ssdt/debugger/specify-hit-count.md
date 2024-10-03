---
title: Specify a hit count
titleSuffix: T-SQL debugger
description: Learn how to set a hit count for a breakpoint, so that the debugger only breaks when the hit count is reached.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan, randolphwest
ms.date: 08/29/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Specify a hit count

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A breakpoint hit count is a counter that the [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger increments each time the breakpoint is reached. If the specified hit count is reached and any specified breakpoint condition is satisfied, the debugger performs the action specified for the breakpoint.

## Hit count considerations

By default, execution breaks every time a breakpoint is hit. You can choose between the following options:

- Break always (the default)
- Break when the hit count equals a specified value
- Break when the hit count equals a multiple of a specified value
- Break when the hit count is greater than or equal to a specified value

Breakpoint hit counts are incremented within the scope of a debugging session. All hit counts are set to zero at the start of each debugging session.

To track the number of times a breakpoint is hit, without breaking execution, specify a hit count with a high value so the breakpoint never breaks.

The default action for a breakpoint is to break execution when both the hit count and breakpoint condition are satisfied. For information about specifying other actions, see [Specify a breakpoint action](specify-breakpoint-action.md).

### Specify a hit count

1. In the editor window, right-click the breakpoint glyph, and then select **Conditions...** on the shortcut menu.

   -or-

   In the **Breakpoints** window, right-click the breakpoint glyph, and then select **Settings** on the shortcut menu.

1. Select the **Conditions** option, and select **Hit count** from the dropdown list.

1. Select the break option from the dropdown list: **=**, **Is a multiple of**, or **>=**.

1. Enter an integer to specify the hit count in the text box.

1. Select the behavior for when the hit count is reached.

   | Option | Behavior |
   | --- | --- |
   | **Actions** | For more information, see [Specify a breakpoint action](specify-breakpoint-action.md). |
   | **Disable breakpoint once hit** | Disables the breakpoint when the hit count is reached. |
   | **Only enable when the following breakpoint is hit** | Select a defined breakpoint. |

1. Select **Close** to implement the changes.

### View or reset the current hit count

1. In the editor window, right-click the breakpoint glyph, and then select **Conditions...** on the shortcut menu.

   -or-

   In the **Breakpoints** window, right-click the breakpoint glyph, and then select **Settings** on the shortcut menu.

1. Select the **X** to the right of the integer specified for the hit count to remove the configuration and set the current hit count to zero.

1. Select **Close** to implement the change.

## Related content

- [Specify a breakpoint condition](specify-breakpoint-condition.md)
- [Specify a breakpoint filter](specify-breakpoint-filter.md)
- [Specify a breakpoint action](specify-breakpoint-action.md)
