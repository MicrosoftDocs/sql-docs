---
title: "How to: Create New Database Objects Using Queries | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: ac983ac7-f9c4-495d-8a99-e1ba370fb271
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Create New Database Objects Using Queries
If you prefer to use scripts to create or edit views, stored procedures, functions, triggers, or user-defined-types, you can use the Transact\-SQL Editor. The Transact\-SQL Editor provides IntelliSense and other language support. For more information, see [Use Transact-SQL Editor to Edit and Execute Scripts](../ssdt/use-transact-sql-editor-to-edit-and-execute-scripts.md).  
  
The Transact\-SQL Editor is invoked when you use the **View Code** contextual menu to open a database entity in a connected database or a project. It is also automatically opened when you use the **New Query** contextual menu from the SQL Server Object Explorer, or add a new script object to a database project. If you are not connected to a database but want to execute a query against it, you can also use the **New Query Connection** dialog box by selecting **Transact-SQL Editor** menu from the **SQL** menu to connect to a database and launch the Transact\-SQL Editor.  
  
> [!WARNING]  
> The following procedures use entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
### To create a new table using a Transact\-SQL query  
  
1.  Right-click the **Trade** database node and select **New Query**.  
  
2.  In the script pane, paste in this code:  
  
    ```  
  
    CREATE TABLE [dbo].[Fruits] (  
        [Id]         INT NOT NULL,  
        [Perishable] BIT DEFAULT ((1)) NULL,  
        PRIMARY KEY CLUSTERED ([Id] ASC),  
        FOREIGN KEY ([Id]) REFERENCES [dbo].[Products] ([Id])   
    );  
    ```  
  
3.  Click the **Execute Query** button in the Transact\-SQL Editor toolbar to run this query.  
  
4.  Right-click the **Trade** database in **SQL Server Object Explorer** and select **Refresh**. Notice that new **Fruits** table has been added to the database.  
  
### To create a new function  
  
1.  Replace the code in the current Transact\-SQL Editor with the following:  
  
    ```  
  
    CREATE FUNCTION [dbo].GetProductsBySupplier  
    (  
    @SupplierId int  
    )  
    RETURNS @returntable TABLE   
    (  
    [Id] int NOT NULL,   
    [Name] NVARCHAR (128) NOT NULL,  
    [Shelflife] INT NOT NULL,  
    [SupplierId] INT NOT NULL,  
    [CustomerId] INT NOT NULL  
    )  
    AS  
    BEGIN  
    INSERT @returntable  
    SELECT *  from Products p  
    where p.SupplierId = @SupplierId  
    RETURN   
    END  
    ```  
  
    This function will return all rows in the `Products` table whose `SupplierId` equals to the specified parameter. Click the **Execute Query** button in the Transact\-SQL Editor toolbar to run this query.  
  
2.  In SQL Server Object Explorer, under the **Trade** node, expand the **Programmability** and **Functions** nodes. You can find the new function you just created under **Table-valued Functions**.  
  
### To create a new view  
  
1.  Replace the code in the current Transact\-SQL Editor with the following. Then click the **Execute Query** button above the editor to run this query.  
  
    ```  
    CREATE VIEW [dbo].PerishableFruits   
    AS SELECT p.Id, p.Name FROM dbo.Products p  
    join dbo.Fruits f on f.Id = p.Id  
    where f.Perishable = 1  
    ```  
  
2.  In SQL Server Object Explorer, under the **Trade** node, expand the **View** node to locate the new view you just created.  
  
## See Also  
[Manage Tables, Relationships, and Fix Errors](../ssdt/manage-tables-relationships-and-fix-errors.md)  
[Use Transact-SQL Editor to Edit and Execute Scripts](../ssdt/use-transact-sql-editor-to-edit-and-execute-scripts.md)  
  
