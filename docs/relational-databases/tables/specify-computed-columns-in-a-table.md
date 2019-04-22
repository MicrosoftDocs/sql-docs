---
title: "Specify Computed Columns in a Table | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "computed columns, define"
ms.assetid: 731a4576-09c1-47f0-a8f6-edd0b55679f4
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specify Computed Columns in a Table
[!INCLUDE[tsql-appliesto-ss-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss-asdb-xxxx-xxx-md.md)]

  A computed column is a virtual column that is not physically stored in the table, unless the column is marked PERSISTED. A computed column expression can use data from other columns to calculate a value for the column to which it belongs. You can specify an expression for a computed column in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Limitations)  
  
     [Security](#Security)  
  
-   **To specify a computed column, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Limitations"></a> Limitations and Restrictions  
  
-   A computed column cannot be used as a DEFAULT or FOREIGN KEY constraint definition or with a NOT NULL constraint definition. However, if the computed column value is defined by a deterministic expression and the data type of the result is allowed in index columns, a computed column can be used as a key column in an index or as part of any PRIMARY KEY or UNIQUE constraint. For example, if the table has integer columns a and b, the computed column a + b may be indexed, but computed column a + DATEPART(dd, GETDATE()) cannot be indexed, because the value might change in subsequent invocations.  
  
-   A computed column cannot be the target of an INSERT or UPDATE statement.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
###  <a name="NewColumn"></a> To add a new computed column  
  
1.  In **Object Explorer**, expand the table for which you want to add the new computed column. Right-click **Columns** and select **New Column**.  
  
2.  Enter the column name and accept the default data type (**nchar**(10)). The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines the data type of the computed column by applying the rules of data type precedence to the expressions specified in the formula. For example, if the formula references a column of type **money** and a column of type **int**, the computed column will be of type **money** because that data type has the higher precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
3.  In the **Column Properties** tab, expand the **Computed Column Specification** property.  
  
4.  In the **(Formula)** child property, enter the expression for this column in the grid cell to the right. For example, in a `SalesTotal` column, the formula you enter might be `SubTotal+TaxAmt+Freight`, which adds the value in these columns for each row in the table.  
  
    > [!IMPORTANT]  
    >  When a formula combines two expressions of different data types, the rules for data type precedence specify that the data type with the lower precedence is converted to the data type with the higher precedence. If the conversion is not a supported implicit conversion, the error "`Error validating the formula for column column_name.`" is returned. Use the CAST or CONVERT function to resolve the data type conflict. For example, if a column of type **nvarchar** is combined with a column of type **int**, the integer type must be converted to **nvarchar** as shown in this formula `('Prod'+CONVERT(nvarchar(23),ProductID))`. For more information, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
5.  Indicate whether the data is persisted by choosing **Yes** or **No** from the drop-down for the **Is Persisted** child property.  
  
6.  On the **File** menu, click **Save**_table name_.  
  
#### To add a computed column definition to an existing column  
  
1.  In **Object Explorer**, right-click the table with the column for which you want to change and expand the **Columns** folder.  
  
2.  Right-click the column for which you want to specify a computed column formula and click **Delete**. Click **OK**.  
  
3.  Add a new column and specify the computed column formula by following the previous procedure to add a new computed column.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To add a computed column when creating a table  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and then click **Execute**. The example creates a table with a computed column that multiplies the value in the `QtyAvailable` column times the value in the `UnitPrice` column.  
  
    ```  
    CREATE TABLE dbo.Products   
    (  
        ProductID int IDENTITY (1,1) NOT NULL  
      , QtyAvailable smallint  
      , UnitPrice money  
      , InventoryValue AS QtyAvailable * UnitPrice  
    );  
  
    -- Insert values into the table.  
    INSERT INTO dbo.Products (QtyAvailable, UnitPrice)  
    VALUES (25, 2.00), (10, 1.5);  
  
    -- Display the rows in the table.  
    SELECT ProductID, QtyAvailable, UnitPrice, InventoryValue  
    FROM dbo.Products;  
  
    ```  
  
#### To add a new computed column to an existing table  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and then click **Execute**. The following example adds a new column to the table created in the previous example.  
  
    ```  
    ALTER TABLE dbo.Products ADD RetailValue AS (QtyAvailable * UnitPrice * 1.35);  
  
    ```  
  
#### To change an existing column to a computed column  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  To change an existing column to a computed column you must drop and re-create the computed column. Copy and paste the following example into the query window and then click **Execute**. The following example modifies the column added in the previous example.  
  
    ```  
    ALTER TABLE dbo.Products DROP COLUMN RetailValue;  
    GO  
    ALTER TABLE dbo.Products ADD RetailValue AS (QtyAvailable * UnitPrice * 1.5);  
  
    ```  
  
     For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  
###  <a name="TsqlExample"></a>  
