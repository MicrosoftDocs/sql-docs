---
title: "T-SQL Tutorial: Create & query database objects| Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: conceptual
ms.assetid: 9fb8656b-0e4e-4ada-b404-4db4d3eea995
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Lesson 1: Create and query database objects
[!INCLUDE[tsql-appliesto-ss2008-all-md](../includes/tsql-appliesto-ss2008-all-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

This lesson shows you how to create a database, create a table in the database, and then access and change the data in the table. Because this lesson is an introduction to using [!INCLUDE[tsql](../includes/tsql-md.md)], it does not use or describe the many options that are available for these statements.  
  
[!INCLUDE[tsql](../includes/tsql-md.md)] statements can be written and submitted to the [!INCLUDE[ssDE](../includes/ssde-md.md)] in the following ways:  
  
-   By using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. This tutorial assumes that you are using [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], but you can also use [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] Express, which is available as a free download from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?linkid=67359).  
  
-   By using the [sqlcmd utility](../tools/sqlcmd-utility.md).  
  
-   By connecting from an application that you create.  
  
The code executes on the [!INCLUDE[ssDE](../includes/ssde-md.md)] in the same way and with the same permissions, regardless of how you submit the code statements.  
  
To run [!INCLUDE[tsql](../includes/tsql-md.md)] statements in [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], open [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] and connect to an instance of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)].  

## Prerequisites
To complete this tutorial, you need SQL Server Management Studio and access to a SQL Server instance. 

- Install [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

If you don't have access to a SQL Server instance, select your platform from the following links. If you choose SQL Authentication, use your SQL Server login credentials.
- **Windows**: [Download SQL Server 2017 Developer Edition](https://www.microsoft.com/sql-server/sql-server-downloads).
- **macOS**: [Download SQL Server 2017 on Docker](https://docs.microsoft.com/sql/linux/quickstart-install-connect-docker).

## Create a database
Like many [!INCLUDE[tsql](../includes/tsql-md.md)] statements, the CREATE DATABASE statement has a required parameter: the name of the database. CREATE DATABASE also has many optional parameters, such as the disk location where you want to put the database files. When you execute CREATE DATABASE without the optional parameters, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses default values for many of these parameters. This tutorial uses very few of the optional syntax parameters.   

1.  In a Query Editor window, type but do not execute the following code:  
  
    ```sql  
    CREATE DATABASE TestData  
    GO  
    ```  
  
2.  Use the pointer to select the words `CREATE DATABASE`, and then press **F1**. The CREATE DATABASE topic in SQL Server Books Online should open. You can use this technique to find the complete syntax for CREATE DATABASE and for the other statements that are used in this tutorial.  
  
3.  In Query Editor, press **F5** to execute the statement and create a database named `TestData`.  
  
When you create a database, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] makes a copy of the **model** database, and renames the copy to the database name. This operation should only take several seconds, unless you specify a large initial size of the database as an optional parameter.  
  
> [!NOTE]  
> The keyword GO separates statements when more than one statement is submitted in a single batch. GO is optional when the batch contains only one statement.  

## Create a Table
[!INCLUDE[tsql-appliesto-ss2008-all-md](../includes/tsql-appliesto-ss2008-all-md.md)]

To create a table, you must provide a name for the table, and the names and data types of each column in the table. It is also a good practice to indicate whether null values are allowed in each column. To create a table, you must have the `CREATE TABLE` permission, and the `ALTER SCHEMA` permission on the schema that will contain the table. The [`db_ddladmin`](../relational-databases/security/authentication-access/database-level-roles.md) fixed database role has these permissions.  
  
Most tables have a primary key, made up of one or more columns of the table. A primary key is always unique. The [!INCLUDE[ssDE](../includes/ssde-md.md)] will enforce the restriction that any primary key value cannot be repeated in the table.  
  
For a list of data types and links for a description of each, see [Data Types &#40;Transact-SQL&#41;](../t-sql/data-types/data-types-transact-sql.md).  
  
> [!NOTE]  
> The [!INCLUDE[ssDE](../includes/ssde-md.md)] can be installed as case sensitive or non-case sensitive. If the [!INCLUDE[ssDE](../includes/ssde-md.md)] is installed as case sensitive, object names must always have the same case. For example, a table named OrderData is a different table from a table named ORDERDATA. If the [!INCLUDE[ssDE](../includes/ssde-md.md)] is installed as non-case sensitive, those two table names are considered to be the same table, and that name can only be used one time.  
  
  
### Switch the Query Editor connection to the TestData database  
In a Query Editor window, type and execute the following code to change your connection to the `TestData` database.  
  
  ```sql  
  USE TestData  
  GO  
  ```  
  
### Create the table
In a Query Editor window, type and execute the following code to create a simple table named `Products`. The columns in the table are named `ProductID`, `ProductName`, `Price`, and `ProductDescription`. The `ProductID` column is the primary key of the table. `int`, `varchar(25)`, `money`, and `text` are all data types. Only the `Price` and `ProductionDescription` columns can have no data when a row is inserted or changed. This statement contains an optional element (`dbo.`) called a schema. The schema is the database object that owns the table. If you are an administrator, `dbo` is the default schema. `dbo` stands for database owner.  
  
  ```sql  
  CREATE TABLE dbo.Products  
     (ProductID int PRIMARY KEY NOT NULL,  
     ProductName varchar(25) NOT NULL,  
     Price money NULL,  
     ProductDescription text NULL)  
  GO  
 ```  

## Insert and update data in a table
Now that you have created the **Products** table, you are ready to insert data into the table by using the INSERT statement. After the data is inserted, you will change the content of a row by using an UPDATE statement. You will use the WHERE clause of the UPDATE statement to restrict the update to a single row. The four statements will enter the following data.  
  
|ProductID|ProductName|Price|ProductDescription|  
|-------------|---------------|---------|----------------------|  
|1|Clamp|12.48|Workbench clamp|  
|50|Screwdriver|3.17|Flat head|  
|75|Tire Bar||Tool for changing tires.|  
|3000|3mm Bracket|.52||  
  
The basic syntax is: INSERT, table name, column list, VALUES, and then a list of the values to be inserted. The two hyphens in front of a line indicate that the line is a comment and the text will be ignored by the compiler. In this case, the comment describes a permissible variation of the syntax.  
  
### Insert data into a table  
  
1.  Execute the following statement to insert a row into the `Products` table that was created in the previous task. This is the basic syntax.  
  
   ```sql 
   -- Standard syntax  
   INSERT dbo.Products (ProductID, ProductName, Price, ProductDescription)  
       VALUES (1, 'Clamp', 12.48, 'Workbench clamp')  
   GO   
   ```  
  
2.  The following statement shows how you can change the order in which the parameters are provided by switching the placement of the `ProductID` and `ProductName` in both the field list (in parentheses) and in the values list.  
  
   ```sql  
   -- Changing the order of the columns  
   INSERT dbo.Products (ProductName, ProductID, Price, ProductDescription)  
       VALUES ('Screwdriver', 50, 3.17, 'Flat head')  
   GO    
   ```  
  
3.  The following statement demonstrates that the names of the columns are optional, as long as the values are listed in the correct order. This syntax is common but is not recommended because it might be harder for others to understand your code. `NULL` is specified for the `Price` column because the price for this product is not yet known.  
  
   ```sql  
   -- Skipping the column list, but keeping the values in order  
   INSERT dbo.Products  
       VALUES (75, 'Tire Bar', NULL, 'Tool for changing tires.')  
   GO  
  ```  
  
4.  The schema name is optional as long as you are accessing and changing a table in your default schema. Because the `ProductDescription` column allows null values and no value is being provided, the `ProductDescription` column name and value can be dropped from the statement completely.  
  
   ```sql  
   -- Dropping the optional dbo and dropping the ProductDescription column  
   INSERT Products (ProductID, ProductName, Price)  
       VALUES (3000, '3mm Bracket', .52)  
   GO  
   ```  
  
### Update the products table  
Type and execute the following `UPDATE` statement to change the `ProductName` of the second product from `Screwdriver`, to `Flat Head Screwdriver`.  
  
  ```sql  
  UPDATE dbo.Products  
      SET ProductName = 'Flat Head Screwdriver'  
      WHERE ProductID = 50  
  GO  
  ```  

## Read data from a table
Use the SELECT statement to read the data in a table. The SELECT statement is one of the most important [!INCLUDE[tsql](../includes/tsql-md.md)] statements, and there are many variations in the syntax. For this tutorial, you will work with five simple versions.  
  
### Read the data in a table  
  
1.  Type and execute the following statements to read the data in the `Products` table.  
  
  ```sql 
  -- The basic syntax for reading data from a single table  
  SELECT ProductID, ProductName, Price, ProductDescription  
      FROM dbo.Products  
  GO  
  ```  
  
2.  You can use an asterisk to select all the columns in the table. This is often used in ad hoc queries. You should provide the column list in you permanent code so that the statement will return the predicted columns, even if a new column is added to the table later.  
  
  ```sql  
  -- Returns all columns in the table  
  -- Does not use the optional schema, dbo  
  SELECT * FROM Products  
  GO   
  ```  
  
3.  You can omit columns that you do not want to return. The columns will be returned in the order that they are listed.  
  
  ```sql  
  -- Returns only two of the columns from the table  
  SELECT ProductName, Price  
      FROM dbo.Products  
  GO    
  ```  
  
4.  Use a `WHERE` clause to limit the rows that are returned to the user.  
  
  ``` sql 
  -- Returns only two of the records in the table  
  SELECT ProductID, ProductName, Price, ProductDescription  
      FROM dbo.Products  
      WHERE ProductID < 60  
  GO    
  ```  
  
5.  You can work with the values in the columns as they are returned. The following example performs a mathematical operation on the `Price` column. Columns that have been changed in this way will not have a name unless you provide one by using the `AS` keyword.  
  
  ```sql  
  -- Returns ProductName and the Price including a 7% tax  
  -- Provides the name CustomerPays for the calculated column  
  SELECT ProductName, Price * 1.07 AS CustomerPays  
      FROM dbo.Products  
  GO  
  ```  
  
### Useful functions in a SELECT Statement  
For information about some functions that you can use to work with data in SELECT statements, see the following topics:  
  
|||  
|-|-|  
|[String Functions &#40;Transact-SQL&#41;](../t-sql/functions/string-functions-transact-sql.md)|[Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md)|  
|[Mathematical Functions &#40;Transact-SQL&#41;](../t-sql/functions/mathematical-functions-transact-sql.md)|[Text and Image Functions &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/b9c70488-1bf5-4068-a003-e548ccbc5199)|  

## Create views and stored procedures
A view is a stored SELECT statement, and a stored procedure is one or more [!INCLUDE[tsql](../includes/tsql-md.md)] statements that execute as a batch.  
  
Views are queried like tables and do not accept parameters. Stored procedures are more complex than views. Stored procedures can have both input and output parameters and can contain statements to control the flow of the code, such as IF and WHILE statements. It is good programming practice to use stored procedures for all repetitive actions in the database.  
  
For this example, you will use CREATE VIEW to create a view that selects only two of the columns in the **Products** table. Then, you will use CREATE PROCEDURE to create a stored procedure that accepts a price parameter and returns only those products that cost less than the specified parameter value.  
  
### Create a view  
  
Execute the following statement to create a very simple view that executes a select statement, and returns the names and prices of our products to the user.  
  
  ```sql  
  CREATE VIEW vw_Names  
     AS  
     SELECT ProductName, Price FROM Products;  
  GO    
  ```  
  
### Test the view  
  
Views are treated just like tables. Use a `SELECT` statement to access a view.  
  
  ```sql  
  SELECT * FROM vw_Names;  
  GO   
  ```  
  
### Create a stored procedure  
  
The following statement creates a stored procedure name `pr_Names`, accepts an input parameter named `@VarPrice` of data type `money`. The stored procedure prints the statement `Products less than` concatenated with the input parameter that is changed from the `money` data type into a `varchar(10)` character data type. Then, the procedure executes a `SELECT` statement on the view, passing the input parameter as part of the `WHERE` clause. This returns all products that cost less than the input parameter value.  
  
  ```sql  
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
  
To test the stored procedure, type and execute the following statement. The procedure should return the names of the two products entered into the `Products` table in Lesson 1 with a price that is less than `10.00`.  
  
  ```sql  
  EXECUTE pr_Names 10.00;  
  GO  
  ```  

## Next steps
The next article teaches you how to configure permissions on database objects. The objects created in lesson 1 will also be used in lesson 2. 

Go to the next article to learn more:
> [!div class="nextstepaction"]
> [Next steps](../t-sql/lesson-2-configuring-permissions-on-database-objects.md)
  
  
  
