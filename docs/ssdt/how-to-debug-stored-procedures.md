---
title: Debug Stored Procedures
description: Learn how to use the Transact-SQL debugger to interactively debug a stored procedure. See how to display the SQL call stack, local variables, and parameters.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords: 
  - "SQL.DATA.TOOLS.EXECUTESTOREDPROCEDURE.DIALOG"
ms.assetid: e3c8707f-0f6b-4265-8a5a-81f079330b52
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: Debug Stored Procedures

The Transact\-SQL debugger allows you to interactively debug stored procedures by displaying the SQL call stack, local variables, and parameters for the SQL stored procedure. As with debugging in other programming languages, you can view and modify local variables and parameters, view global variables, as well as control and manage breakpoints while debugging your Transact\-SQL script.  
  
This example shows how to create and debug a Transact\-SQL stored procedure by stepping into it.  
  
> [!WARNING]  
> The following procedure uses entities created in procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  

> [!NOTE]
> Transact\-SQL debugging isn't available for Azure SQL Database or Azure SQL Managed Instance.
  
### To debug stored procedures  
  
1.  In **Solution Explorer**, right-click the **TradeDev** project and select **Add**, then **Stored Procedure**. Name this new stored procedure **AddProduct** and click **Add**.  
  
2.  Paste the following code to the store procedure.  
  
    ```  
    CREATE PROCEDURE [dbo].[AddProduct]  
    @id int,  
    @name nvarchar(128)  
    AS  
    INSERT INTO [dbo].[Product] (Id, Name) VALUES (@id, @name)  
    ```  
  
3.  Press F5 to build and deploy the project.  
  
4.  In SQL Server Object Explorer, under the **Local** node, right-click **TradeDev** database and select **New Query**.  
  
5.  Paste in the following code to the query window.  
  
    ```  
    EXEC [dbo].[AddProduct] 50, N'Contoso';  
    GO  
    ```  
  
6.  Click the left window margin to add a breakpoint to the `EXEC` statement.  
  
7.  Press the drop-down arrow on the green arrow button in the Transact\-SQL editor toolbar and select **Execute with Debugger** to execute the query with debugging on.  
  
8.  Alternately, you can start debugging from SQL Server Object Explorer. Right-click the **AddProduct** stored procedure (located under **Local** -> **TradeDev** database -> **Programmability** -> **Stored Procedures**). Select **Debug Procedure...**. If the object requires parameters, the **Debug Procedure** dialog box appears, with a table containing a row for each parameter. Each row in the table contains a column for the name of the parameter, and one for the value of that parameter. Enter values for each parameter, and click OK.  
  
9. Make sure that the **Locals** window is opened. If not, click the **Debug** menu, select **Windows** and **Local**.  
  
10. Press F11 to step into the query. Notice that the parameters of the store procedure and their respective values show up in the **Locals** window. Alternatively, hover your mouse over the `@name` parameter in the `INSERT` clause, and you will see the **Contoso** value being assigned to it.  
  
11. Click **Contoso** in the textbox. Type **Fabrikam** and press ENTER to change the `name` variable's value while debugging. You can also change its value in the **Locals** window. Notice that the value of the parameter is now displayed in red, indicating that it has changed.  
  
12. Press F10 to step over the remaining code.  
  
13. In SQL Server Object Explorer, refresh the **TradeDev** database node in order to see the new contents in the data view of the **Product** table.  
  
14. In SQL Server Object Explorer, under the **Local** node, find the **Product** table of the **TradeDev** database.  
  
15. Right-click **Product** table and select **View Data**. Notice that the new row has been added to the table.  
  
