---
title: "Creating Views and Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "creating views and stored procedures"
ms.assetid: 53a0426d-07d8-4b7c-aa21-22632753bad8
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Creating Views and Stored Procedures
  Now that Mary can access the **TestData** database, you may want to create some database objects, such as a view and a stored procedure, and then grant Mary access to them. A view is a stored SELECT statement, and a stored procedure is one or more [!INCLUDE[tsql](../includes/tsql-md.md)] statements that execute as a batch.  
  
 Views are queried like tables and do not accept parameters. Stored procedures are more complex than views. Stored procedures can have both input and output parameters and can contain statements to control the flow of the code, such as IF and WHILE statements. It is good programming practice to use stored procedures for all repetitive actions in the database.  
  
 For this example, you will use CREATE VIEW to create a view that selects only two of the columns in the **Products** table. Then, you will use CREATE PROCEDURE to create a stored procedure that accepts a price parameter and returns only those products that cost less than the specified parameter value.  
  
### To create a view  
  
1.  Execute the following statement to create a very simple view that executes a select statement, and returns the names and prices of our products to the user.  
  
    ```  
    CREATE VIEW vw_Names  
       AS  
       SELECT ProductName, Price FROM Products;  
    GO  
  
    ```  
  
### Test the view  
  
1.  Views are treated just like tables. Use a `SELECT` statement to access a view.  
  
    ```  
    SELECT * FROM vw_Names;  
    GO  
  
    ```  
  
### To create a stored procedure  
  
1.  The following statement creates a stored procedure name `pr_Names`, accepts an input parameter named `@VarPrice` of data type `money`. The stored procedure prints the statement `Products less than` concatenated with the input parameter that is changed from the `money` data type into a `varchar(10)` character data type. Then, the procedure executes a `SELECT` statement on the view, passing the input parameter as part of the `WHERE` clause. This returns all products that cost less than the input parameter value.  
  
    ```  
    CREATE PROCEDURE pr_Names @VarPrice money  
       AS  
       BEGIN  
          -- The print statement returns text to the user  
          PRINT 'Products less than ' + CAST(@VarPrice AS varchar(10));  
          -- A second statement starts here  
          SELECT ProductName, Price FROM vw_Names  
                WHERE Price < @varPrice;  
       END  
    GO  
  
    ```  
  
### Test the stored procedure  
  
1.  To test the stored procedure, type and execute the following statement. The procedure should return the names of the two products entered into the `Products` table in Lesson 1 with a price that is less than `10.00`.  
  
    ```  
    EXECUTE pr_Names 10.00;  
    GO  
  
    ```  
  
## Next Task in Lesson  
 [Granting Access to a Database Object](lesson-2-4-granting-access-to-a-database-object.md)  
  
## See Also  
 [CREATE VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-view-transact-sql)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-procedure-transact-sql)  
  
  
