---
title: Enable, Disable, and Delete Breakpoints
description: Learn how to use the Breakpoints window to view, delete, disable, and enable breakpoints.
titleSuffix: T-SQL debugger
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: 357b5874-273f-43a9-8e30-83872bdea5dc
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 12/04/2019
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Enable, Disable, and Delete Breakpoints

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

To view and manage all the open breakpoints, you can use the **Breakpoints** window. Use the window to view breakpoint information and to take actions such as deleting, disabling, or enabling breakpoints.

[!INCLUDE[ssms-old-versions](../../includes/ssms-old-versions.md)]
  
## The Breakpoints Window  
 The **Breakpoints** window lists information such as which line of code the breakpoint is located on. In the **Breakpoints** window, you can also delete, disable, and enable breakpoints. For more information about the **Breakpoints** window, see [Breakpoints Window](./transact-sql-debugger-breakpoints-window.md)  
  
 Disabling a breakpoint prevents it from pausing execution, but leaves the definition in place in case you want to enable the breakpoint later. Deleting a breakpoint removes it permanently. You must toggle a new breakpoint to pause execution on the statement.  
  
## To Open the Breakpoints Window  
 **To open the Breakpoints window**  
  
 You can open the **Breakpoints** window in one of the following ways:  
  
-   On the **Debug** menu, click **Windows**, and then click **Breakpoints**.  
  
-   On the **Debug** toolbar, click the **Breakpoints** button.  
  
-   Press CTRL+ALT+B.  
  
## To Disable a Single Breakpoint  
 **To disable a single breakpoint**  
  
 You can disable a single breakpoint in one of the following ways:  
  
-   In the Query Editor window, right-click the breakpoint, and then click **Disable Breakpoint**.  
  
-   In the Breakpoints window, clear the check box to the left of the breakpoint.  
  
## To Disable All Breakpoints  
 **To disable all breakpoints**  
  
 You can disable all breakpoints in one of the following ways:  
  
-   On the **Debug** menu, click **Disable All Breakpoints**.  
  
-   On the toolbar of the **Breakpoints** window, click the **Disable All Breakpoints** button.  
  
## To Enable a Single Breakpoint  
 **To enable a single breakpoint**  
  
 You can enable a single breakpoint in one of the following ways:  
  
-   In the Query Editor window, right-click the breakpoint, and then click **Enable Breakpoint**.  
  
-   In the Breakpoints window, click the box to the left of the breakpoint.  
  
## To Enable All Breakpoints  
 **To enable all breakpoints**  
  
 You can enable all breakpoints in one of the following ways:  
  
-   On the **Debug** menu, click **Enable All Breakpoints**.  
  
-   On the toolbar of the **Breakpoints** window, click the **Enable All Breakpoints** button.  
  
## To Delete a Single Breakpoint  
 **To delete a single breakpoint**  
  
 You can delete a single breakpoint in one of the following ways:  
  
-   In the Query Editor window, right-click the breakpoint, and then click **Delete Breakpoint**.  
  
-   In the Breakpoints window, right-click the breakpoint, and then click **Delete** on the shortcut menu.  
  
-   In the Breakpoints window, select the breakpoint, and then press DELETE.  
  
## To Delete All Breakpoints  
 **To delete all breakpoints**  
  
 You can delete all breakpoints in one of the following ways:  
  
-   On the **Debug** menu, click **Delete All Breakpoints**.  
  
-   On the toolbar of the **Breakpoints** window, click the **Delete All Breakpoints** button.  
  
## See Also  
 [Toggle a Breakpoint](./toggle-a-breakpoint.md)  
  
