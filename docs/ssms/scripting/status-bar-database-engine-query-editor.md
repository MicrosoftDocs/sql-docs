---
title: Status Bar (Database Engine Query Editor)
description: Learn how to color code the status bar of a Database Engine Query Editor window to indicate which instance of the DataBase engine the window is connected to.
titleSuffix: T-SQL debugger
ms.service: sql
ms.subservice: ssms
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: e7f2d6f4-bb94-4cf5-a096-c34397e679af
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 12/04/2019
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Status Bar (Database Engine Query Editor)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The status bar of [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor windows can be color coded to indicate which instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] each window is connected to.

1. **Before you begin:**  [Status Bar Colors](#StatusBarColors)  

2. **To set a server status color in:**  [Object Explorer](#SetOEServerColor), [Registered Server](#SetRegServerColor)  

3. **To use a status color:**  [Open Query Editor Using a Server Color](#OpenServerColor), [Open a Query Editor Specifying a Status Color](#OpenSpecColor)  

[!INCLUDE[ssms-old-versions](../../includes/ssms-old-versions.md)]

##  <a name="StatusBarColors"></a> Status Bar Colors

You can associate a status bar color with a specific server node in either **Object Explorer** or **Registered Servers**. The colors can only be specified for server nodes connected to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], not server nodes for other SQL Server technologies. You can also specify a custom status bar color each time you connect a new [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. You can then open a query editor window using either the status color defined for the server node, or specify a unique color for that editor window.  

Setting a custom status bar color for a server node in Object Explorer must be done when making the connection. To change the color associated with an existing server node, you must disconnect and then reconnect specifying the new color.  

##  <a name="SetOEServerColor"></a> Set the Status Color for a Server in Object Explorer

**To set a server status color in Object Explorer**  
  
1.  In **Object Explorer**, select the **Connect** button and then select **Database Engine...**.  
  
2.  On the **Connect to Server** dialog, select **Options >>**.  
  
3.  Select the **Use custom color** check box.  
  
4.  To select the color, select the **Select...** button.  
  
5.  Select either a basic or custom color, then select OK.  
  
6.  Fill in the rest of the connection information, and then select the **Connect** button.  
  
##  <a name="SetRegServerColor"></a> Set the Status Color For a Registered Server  
 **To set a server color For a Registered Server**  
  
1.  In **Registered Servers**, right click the server node and then select **Properties...**.  
  
2.  On the **Edit Server Registration Properties** dialog, select the **Connection Properties** tab.  
  
3.  Select the **Use custom color** check box.  
  
4.  To select the color, select the **Select...** button.  
  
5.  Select either a basic or custom color, then select OK.  
  
6.  Select the **Save** button on the **Edit Server Registration Properties** dialog.  
  
##  <a name="OpenServerColor"></a> Open An Editor Using a Server Color  
 **To open an editor window using a server color**  
  
-   Right-click a server node in either **Object Explorer** or **Registered Servers**, and select **New Query**.  
  
-   Alternatively, highlight a server node, and then select the **New Query** toolbar button.  
  
-   The status bar in the editor window will use the color defined for the associated server.  
  
##  <a name="OpenSpecColor"></a> Open an Editor Specifying a Status Color  
 **To open an editor window specifying a status color**  
  
-   Open the **File** menu, select **New**, and then select **Database Engine Query**.  
  
-   On the **Connect to Server** dialog, select **Options >>**.  
  
-   Select the **Use custom color** check box.  
  
-   To select the color, select the **Select...** button.  
  
-   Select either a basic or custom color, then select OK.  
  
-   Fill in the rest of the connection information, and then select the **Connect** button.  
  
## See Also  
 [Query and Text Editors &#40;SQL Server Management Studio&#41;](../f1-help/database-engine-query-editor-sql-server-management-studio.md)  
  
