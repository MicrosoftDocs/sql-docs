---
title: Debug stored procedures
description: Learn how to use the Transact-SQL debugger to interactively debug a stored procedure. See how to display the SQL call stack, local variables, and parameters.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan
ms.date: 08/28/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords:
  - "SQL.DATA.TOOLS.EXECUTESTOREDPROCEDURE.DIALOG"
---

# Debug stored procedures

The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger allows you to interactively debug stored procedures by displaying the SQL call stack, local variables, and parameters for the SQL stored procedure. The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger supports viewing and modifying local variables and parameters, viewing global variables.  It also provides the ability to control and manage breakpoints when debugging your [!INCLUDE[tsql](../../includes/tsql-md.md)] script.  
  
This example shows how to create and debug a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure by stepping into it.  
  
> [!NOTE]
> [!INCLUDE[tsql](../../includes/tsql-md.md)] debugging isn't available for Azure SQL Database or Azure SQL Managed Instance.
  
## To debug stored procedures  
  
1. In the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window, connect to an instance of the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)]. Select a database in which you can create an example stored procedure.
  
1. Paste the following code in the Query Editor.  
  
    ```sql  
    CREATE TABLE [dbo].[Product] ([Id] INT, [Name] NVARCHAR(128))

    CREATE PROCEDURE [dbo].[AddProduct]  
    @id INT,  
    @name NVARCHAR(128)  
    AS  
    BEGIN
        INSERT INTO [dbo].[Product] ([Id], [Name]) VALUES (@id, @name) 
        SELECT [Name] FROM [dbo].[Product] WHERE [Id] = @id
        DECLARE @nextid INT
        SET @nextid = @id + 1
        INSERT INTO [dbo].[Product] ([Id], [Name]) VALUES (@id, @name) 
        SELECT [Name] FROM [dbo].[Product] WHERE [Id] = @nextid
    END
    ```  
  
1. Press F5 to run the [!INCLUDE[tsql](../../includes/tsql-md.md)] code.  
  
1. In SQL Server Object Explorer, right-click on the same [!INCLUDE [ssDE](../../includes/ssde-md.md)] and select **New Query...**.  Ensure you are connected to the same database in which you created the stored procedure.
  
1. Paste in the following code to the query window.  
  
    ```sql
    EXEC [dbo].[AddProduct] 50, N'T-SQL Debugger Test';  
    GO  
    ```  
  
1. Click the left window margin to add a breakpoint to the `EXEC` statement.  
  
1. Press the drop-down arrow on the green arrow button in the Transact-SQL editor toolbar and select **Execute with Debugger** to execute the query with debugging on.  
  
1. Alternately, you can start debugging from the **SQL** menu.  Select **SQL** -> **Execute With Debugger**.
  
1. Make sure that the **Locals** window is opened. If not, click the **Debug** menu, select **Windows** and **Local**.  
  
1. Press F11 to step into the query. Notice that the parameters of the store procedure and their respective values show up in the **Locals** window. Alternatively, hover your mouse over the `@name` parameter in the `INSERT` clause to see the **T-SQL Debugger Test** value being assigned to it.  
  
1. Select **T-SQL Debugger Test** in the textbox. Type **Validate Change** and press ENTER to change the `name` variable's value while debugging. You can also change its value in the **Locals** window. Notice that the value of the parameter is red, indicating a change.  
  
1. Press F10 to step over the remaining code.  
  
1. When debugging is complete, query the **Product** table to view its contents.

    ```sql
    SELECT * FROM [dbo].[Products];  
    GO
    ```  

1. In the results window, notice that new rows exist in the table.  

## Related content

- [Transact-SQL Debugger](./transact-sql-debugger.md)
- [Run the Transact-SQL Debugger](./run-transact-sql-debugger.md)
- [Step Through Transact-SQL Code](./step-through-transact-sql-code.md)
- [Transact-SQL Debugger Information](./transact-sql-debugger-information.md)