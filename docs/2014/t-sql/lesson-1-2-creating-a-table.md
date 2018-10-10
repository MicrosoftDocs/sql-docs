---
title: "Creating a Table (Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "creating tables"
ms.assetid: 653f2dd3-36a2-4bd5-8703-71a57d244661
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Creating a Table (Tutorial)
  To create a table, you must provide a name for the table, and the names and data types of each column in the table. It is also a good practice to indicate whether null values are allowed in each column.  
  
 Most tables have a primary key, made up of one or more columns of the table. A primary key is always unique. The [!INCLUDE[ssDE](../includes/ssde-md.md)] will enforce the restriction that any primary key value cannot be repeated in the table.  
  
 For a list of data types and links for a description of each, see [Data Types &#40;Transact-SQL&#41;](/sql/t-sql/data-types/data-types-transact-sql).  
  
> [!NOTE]  
>  The [!INCLUDE[ssDE](../includes/ssde-md.md)] can be installed as case sensitive or non-case sensitive. If the [!INCLUDE[ssDE](../includes/ssde-md.md)] is installed as case sensitive, object names must always have the same case. For example, a table named OrderData is a different table from a table named ORDERDATA. If the [!INCLUDE[ssDE](../includes/ssde-md.md)] is installed as non-case sensitive, those two table names are considered to be the same table, and that name can only be used one time.  
  
### To create a database to contain the new table  
  
-   Enter the following code into a Query Editor window.  
  
    ```  
    USE master;  
    GO  
  
    --Delete the TestData database if it exists.  
    IF EXISTS(SELECT * from sys.databases WHERE name='TestData')  
    BEGIN  
        DROP DATABASE TestData;  
    END  
  
    --Create a new database called TestData.  
    CREATE DATABASE TestData;  
    Press the F5 key to execute the code and create the database.  
    ```  
  
### Switch the Query Editor connection to the TestData database  
  
-   In a Query Editor window, type and execute the following code to change your connection to the `TestData` database.  
  
    ```  
    USE TestData  
    GO  
    ```  
  
### To create a table  
  
-   In a Query Editor window, type and execute the following code to create a simple table named `Products`. The columns in the table are named `ProductID`, `ProductName`, `Price`, and `ProductDescription`. The `ProductID` column is the primary key of the table. `int`, `varchar(25)`, `money`, and `text` are all data types. Only the `Price` and `ProductionDescription` columns can have no data when a row is inserted or changed. This statement contains an optional element (`dbo.`) called a schema. The schema is the database object that owns the table. If you are an administrator, `dbo` is the default schema. `dbo` stands for database owner.  
  
    ```  
    CREATE TABLE dbo.Products  
       (ProductID int PRIMARY KEY NOT NULL,  
        ProductName varchar(25) NOT NULL,  
        Price money NULL,  
        ProductDescription text NULL)  
    GO  
    ```  
  
## Next Task in Lesson  
 [Inserting and Updating Data in a Table &#40;Tutorial&#41;](../t-sql/lesson-1-3-inserting-and-updating-data-in-a-table.md)  
  
## See Also  
 [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql)  
  
  
