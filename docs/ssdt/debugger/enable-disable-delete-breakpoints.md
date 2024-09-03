---
title: Enable, disable, and delete breakpoints
titleSuffix: T-SQL debugger
description: Learn how to use the Breakpoints window to view, delete, disable, and enable breakpoints.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan
ms.date: 08/28/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Enable, disable, and delete breakpoints

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

To view and manage all the open breakpoints, you can use the **Breakpoints** window. Use the window to view breakpoint information and to take actions such as deleting, disabling, or enabling breakpoints.

## The breakpoints window

 The **Breakpoints** window lists information such as which line of code the breakpoint is located on. In the **Breakpoints** window, you can also delete, disable, and enable breakpoints.
  
 Disabling a breakpoint prevents it from pausing execution, but leaves the definition in place in case you want to enable the breakpoint later. Deleting a breakpoint removes it permanently. You must toggle a new breakpoint to pause execution on the statement.  
  
## Open the breakpoints window

 You can open the **Breakpoints** window in one of the following ways:  
  
- On the **Debug** menu, select **Windows**, and then select **Breakpoints**.  
  
- On the **Debug** toolbar, select the **Breakpoints** button.  
  
- Press CTRL+ALT+B.  
  
## Disable a single breakpoint
  
 You can disable a single breakpoint in one of the following ways:  
  
- In the Query Editor window, hover over the breakpoint, and then select **Disable**.  
  
- In the **Breakpoints** window, clear the check box to the left of the breakpoint.  
  
## Disable all breakpoints
  
 You can disable all breakpoints in one of the following ways:  
  
- On the **Debug** menu, click **Disable All Breakpoints**.  
  
- On the toolbar of the **Breakpoints** window, select the **Disable all breakpoints** button.  
  
## Enable a single breakpoint
  
 You can enable a single breakpoint in one of the following ways:  
  
- In the Query Editor window, hover over the breakpoint, and then select **Enable**.  
  
- In the **Breakpoints** window, select the box to the left of the breakpoint.  
  
## Enable all breakpoints

You can enable all breakpoints in one of the following ways:  
  
- On the **Debug** menu, select **Enable All Breakpoints**.  
  
- On the toolbar of the **Breakpoints** window, select the **Enable all breakpoints** button.  
  
## Delete a single breakpoint

You can delete a single breakpoint in one of the following ways:  
  
- In the Query Editor window, right-click the breakpoint, and then select **Delete Breakpoint**.  
  
- In the Breakpoints window, right-click the breakpoint, and then select **Delete** on the shortcut menu.  
  
- In the Breakpoints window, select the breakpoint, and then select the **Delete selected breakpoints** button.  
  
## Delete all breakpoints  

You can delete all breakpoints in one of the following ways:  
  
- On the **Debug** menu, click **Delete All Breakpoints**.  
  
- On the toolbar of the **Breakpoints** window, select the **Delete all breakpoints** button.  

## Related content

- [Transact-SQL Breakpoints](transact-sql-breakpoints.md)
- [Toggle a Breakpoint](./toggle-breakpoint.md)  
- [Transact-SQL Debugger](./transact-sql-debugger.md)
