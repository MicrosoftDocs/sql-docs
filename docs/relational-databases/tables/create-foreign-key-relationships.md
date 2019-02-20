---
title: "Create Foreign Key Relationships | Microsoft Docs"
ms.custom: ""
ms.date: "07/25/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "relationships [SQL Server], creating"
ms.assetid: 867a54b8-5be4-46e6-9702-49ae6dabf67c
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Foreign Key Relationships
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

  This topic describes how to create foreign key relationships in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You create a relationship between two tables when you want to associate rows of one table with rows of another.    
     
##  <a name="BeforeYouBegin"></a> Before You Begin! Limits and Restrictions
 
-   A foreign key constraint does not have to be linked only to a primary key constraint in another table; it can also be defined to reference the columns of a UNIQUE constraint in another table.    
    
-   When a value other than NULL is entered into the column of a FOREIGN KEY constraint, the value must exist in the referenced column; otherwise, a foreign key violation error message is returned. To make sure that all values of a composite foreign key constraint are verified, specify NOT NULL on all the participating columns.    
    
-   FOREIGN KEY constraints can reference only tables within the same database on the same server. Cross-database referential integrity must be implemented through triggers. For more information, see [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md).    
    
-   FOREIGN KEY constraints can reference another column in the same table. This is referred to as a self-reference.    
    
-   A FOREIGN KEY constraint specified at the column level can list only one reference column. This column must have the same data type as the column on which the constraint is defined.    
    
-   A FOREIGN KEY constraint specified at the table level must have the same number of reference columns as the number of columns in the constraint column list. The data type of each reference column must also be the same as the corresponding column in the column list.    
    
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not have a predefined limit on either the number of FOREIGN KEY constraints a table can contain that reference other tables, or the number of FOREIGN KEY constraints that are owned by other tables that reference a specific table. Nevertheless, the actual number of FOREIGN KEY constraints that can be used is limited by the hardware configuration and by the design of the database and application.  A table can reference a maximum of 253 other tables and columns as foreign keys (outgoing references). [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] increases the limit for the number of other table and columns that can reference columns in a single table (incoming references), from 253 to 10,000.  (Requires at least 130 compatibility level.) The increase has the following restrictions:    
    
    -   Greater than 253 foreign key references are supported for DELETE and UPDATE DML operations. MERGE operations are not supported.    
    
    -   A table with a foreign key reference to itself is still limited to 253 foreign key references.    
    
    -   Greater than 253 foreign key references are not currently available for columnstore indexes, memory-optimized tables, or Stretch Database.    
    
-   FOREIGN KEY constraints are not enforced on temporary tables.    
    
-   If a foreign key is defined on a CLR user-defined type column, the implementation of the type must support binary ordering. For more information, see [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md).    
    
-   A column of type **varchar(max)** can participate in a FOREIGN KEY constraint only if the primary key it references is also defined as type **varchar(max)**.    
    

    
##   Permissions    
 Creating a new table with a foreign key requires CREATE TABLE permission in the database and ALTER permission on the schema in which the table is being created.    
    
 Creating a foreign key in an existing table requires ALTER permission on the table.    
       
    
## Create a foreign key relationship in Table Designer 
####  Using SQL Server Management Studio    
    
1.  In Object Explorer, right-click the table that will be on the foreign-key side of the relationship and click **Design**.    
    
     The table opens in **Table Designer**.    
    
2.  From the **Table Designer** menu, click **Relationships**.    
    
3.  In the **Foreign-key Relationships** dialog box, click **Add**.    
    
     The relationship appears in the **Selected Relationship** list with a system-provided name in the format FK_\<*tablename*>_\<*tablename*>, where *tablename* is the name of the foreign key table.    
    
4.  Click the relationship in the **Selected Relationship** list.    
    
5.  Click **Tables and Columns Specification** in the grid to the right and click the ellipses (**...**) to the right of the property.    
    
6.  In the **Tables and Columns** dialog box, in the **Primary Key** drop-down list, choose the table that will be on the primary-key side of the relationship.    
    
7.  In the grid beneath, choose the columns contributing to the table's primary key. In the adjacent grid cell to the left of each column, choose the corresponding foreign-key column of the foreign-key table.    
    
     **Table Designer** suggests a name for the relationship. To change this name, edit the contents of the **Relationship Name** text box.    
    
8.  Choose **OK** to create the relationship.    
       
## Create a foreign key in a new table  
####  Using Transact-SQL   
    
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].    
    
2.  On the Standard bar, click **New Query**.    
    
3.  Copy and paste the following example into the query window and click **Execute**. The example creates a table and defines a foreign key constraint on the column `TempID` that references the column `SalesReasonID` in the `Sales.SalesReason` table. The ON DELETE CASCADE and ON UPDATE CASCADE clauses are used to ensure that changes made to `Sales.SalesReason` table are automatically propagated to the `Sales.TempSalesReason` table.    
    
    ```    
    USE AdventureWorks2012;    
    GO    
    CREATE TABLE Sales.TempSalesReason (TempID int NOT NULL, Name nvarchar(50),     
    CONSTRAINT PK_TempSales PRIMARY KEY NONCLUSTERED (TempID),     
    CONSTRAINT FK_TempSales_SalesReason FOREIGN KEY (TempID)     
        REFERENCES Sales.SalesReason (SalesReasonID)     
        ON DELETE CASCADE    
        ON UPDATE CASCADE    
    );
    GO    
    
    ```    
    
## Create a foreign key in an existing table 
#### Using Transact-SQL   
    
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].    
    
2.  On the Standard bar, click **New Query**.    
    
3.  Copy and paste the following example into the query window and click **Execute**. The example creates a foreign key on the column `TempID` and references the column `SalesReasonID` in the `Sales.SalesReason` table.    
    
    ```    
    USE AdventureWorks2012;    
    GO    
    ALTER TABLE Sales.TempSalesReason     
    ADD CONSTRAINT FK_TempSales_SalesReason FOREIGN KEY (TempID)     
        REFERENCES Sales.SalesReason (SalesReasonID)     
        ON DELETE CASCADE    
        ON UPDATE CASCADE    
    ;    
    GO    
    
    ```    
    
     For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md), [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md), and [table_constraint &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-table-constraint-transact-sql.md).    
    
  
