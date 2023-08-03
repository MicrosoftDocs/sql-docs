---
title: "Delete User-defined Functions"
description: "Delete User-defined Functions"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/28/2022"
ms.service: sql
ms.topic: conceptual
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Delete user-defined functions

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

You can delete (drop) user-defined functions in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].

## <a id="Restrictions"></a> Limitations and restrictions

- You won't be able to delete the function if there are Transact-SQL functions or views in the database that reference this function and were created by using SCHEMABINDING, or if there are computed columns, CHECK constraints, or DEFAULT constraints that reference the function.

- You won't be able to delete the function if there are computed columns that reference this function and have been indexed.

## Permissions

Requires ALTER permission on the schema to which the function belongs, or CONTROL permission on the function.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. Select the plus sign next to the database that contains the function you wish to modify.

1. Select the plus sign next to the **Programmability** folder.

1. Select the plus sign next to the folder that contains the function you wish to modify:

   - Table-valued Function
   - Scalar-valued Function
   - Aggregate Function

1. Right-click the function you want to delete and select **Delete**.

1. In the **Delete Object** dialog box, select **OK**.

   Select **Show Dependencies** in the **Delete Object** dialog box to open the *function_name* **Dependencies** dialog box. This will show all of the objects that depend on the function and all of the objects on which the function depends.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   The following code sample creates a user-defined function:

   ```sql
   -- creates function called "Sales.ufn_SalesByStore"
   USE AdventureWorks2012;
   GO
   CREATE FUNCTION Sales.ufn_SalesByStore (@storeid int)
   RETURNS TABLE
   AS
   RETURN
   (
       SELECT P.ProductID, P.Name, SUM(SD.LineTotal) AS 'Total'
       FROM Production.Product AS P
       JOIN Sales.SalesOrderDetail AS SD ON SD.ProductID = P.ProductID
       JOIN Sales.SalesOrderHeader AS SH ON SH.SalesOrderID = SD.SalesOrderID
       JOIN Sales.Customer AS C ON SH.CustomerID = C.CustomerID
       WHERE C.StoreID = @storeid
       GROUP BY P.ProductID, P.Name
   );
   GO
   ```

   The following code sample deletes the user-defined function created in the previous example.

   ```sql
   USE AdventureWorks2012;
   GO
   -- determines if function exists in database
   IF OBJECT_ID (N'Sales.fn_SalesByStore', N'IF') IS NOT NULL
   -- deletes function
       DROP FUNCTION Sales.fn_SalesByStore;
   GO
   ```

## See also

- [DROP FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-function-transact-sql.md)
