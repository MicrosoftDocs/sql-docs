---
title: "How to: Use Microsoft SQL Server 2012 Objects in Your Project | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 9baf122f-cf22-4860-98db-ef782cd972fc
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Use Microsoft SQL Server 2012 Objects in Your Project
In this example, you will add a sequence object to a database project targeting Microsoft SQL Server 2012.  
  
Sequences are introduced in Microsoft SQL Server 2012. A sequence is a user-defined schema-bound object that generates a sequence of numeric values according to the specification with which the sequence was created. The sequence of numeric values is generated in an ascending or descending order at a defined interval and may cycle (repeat) as requested.  For more information on sequence objects, see [Sequence Numbers](htttp://msdn.microsoft.com/library/ff878058(SQL.110).aspx). For information on what's new in Microsoft SQL Server 2012, see [What's New in SQL Server 2012](https://msdn.microsoft.com/library/bb500435(SQL.110).aspx).  
  
> [!WARNING]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To add a new Sequence object to your project  
  
1.  Right-click the **TradeDev** database project in **Solution Explorer**, select **Add**, then **New Item**.  
  
2.  Click **Programmability** on the left pane, and select **Sequence**. Click **Add** to add the new object to the project.  
  
3.  Replace the default code with the following.  
  
    ```  
    CREATE SEQUENCE [dbo].[Seq1]  
    AS INT  
    START WITH 1  
    INCREMENT BY 1  
    MAXVALUE 1000  
    NO CYCLE  
    CACHE 10  
    ```  
  
4.  If your project's target platform is not set to Microsoft SQL Server 2012, the **Error List** will show a syntax error for the `CREATE SEQUENCE` statement. To correct this, follow the [How to: Change Target Platform and Publish a Database Project](../ssdt/how-to-change-target-platform-and-publish-a-database-project.md) topic to change the target platform accordingly.  
  
5.  Follow the [How to: Change Target Platform and Publish a Database Project](../ssdt/how-to-change-target-platform-and-publish-a-database-project.md) topic to publish the project to a database in your connected Microsoft SQL Server 2012 server.  
  
### To use the new Sequence object  
  
1.  In SQL Server Object Explorer, right-click database you have published to in the previous procedure, and select **New Query**.  
  
2.  Paste the following code to the query window.  
  
    ```  
    DECLARE @counter INT  
    SET @counter=0  
    WHILE @counter<10  
    BEGIN  
        SET @counter = @counter +1  
         INSERT dbo.Products (Id, Name, CustomerId) VALUES (NEXT VALUE FOR dbo.Seq1, 'ProductItem'+cast(@counter as varchar), 1)  
    END   
    GO  
    ```  
  
3.  Press the **Execute Query** button.  
  
4.  In **SQL Server Object Explorer**, navigate to the **Products** table in the database. Right-click and select **View Data** to examine the newly added rows.  
  
