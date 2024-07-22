---
title: "Modify a stored procedure"
description: Learn how to modify a stored procedure in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/19/2024
ms.service: sql
ms.subservice: stored-procedures
ms.topic: conceptual
helpviewer_keywords:
  - "modifying stored procedures"
  - "editing stored procedures"
  - "stored procedures [SQL Server], modifying"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Modify a stored procedure
[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to modify a stored procedure in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].
  
## <a id="Restrictions"></a> Limitations

 [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedures cannot be modified to be CLR stored procedures and vice versa.  
  
 If the previous procedure definition was created using `WITH ENCRYPTION` or `WITH RECOMPILE`, these options are enabled only if they are included in the `ALTER PROCEDURE` statement.  
  
## <a id="Security"></a> Permissions
 Requires ALTER PROCEDURE permission on the procedure.  
    
## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

To modify a procedure in SQL Server Management Studio:
  
1. In Object Explorer, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then expand that instance.  

1. Expand **Databases**, expand the database in which the procedure belongs, and then expand **Programmability**.  
  
1. Expand **Stored Procedures**, right-click the procedure to modify, and then select **Modify**.  
  
1. Modify the text of the stored procedure.  
  
1. To test the syntax, on the **Query** menu, select **Parse**.  
  
1. To save the modifications to the procedure definition, on the **Query** menu, select **Execute**.  
  
1. To save the updated procedure definition as a [!INCLUDE [tsql](../../includes/tsql-md.md)] script, on the **File** menu, select **Save As**. Accept the file name or replace it with a new name, and then select **Save**.  

> [!IMPORTANT]  
> Validate all user input. Do not concatenate user input before you validate it. Never execute a command constructed from unvalidated user input. Unvalidated user input makes your database vulnerable a type of exploit called a *SQL injection attack*. For more information, see [SQL injection](../security/sql-injection.md).
  
## <a id="TsqlProcedure"></a> Use Transact-SQL
 
To modify a procedure using T-SQL commands:
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
1. Expand **Databases**, expand the database in which the procedure belongs. Or, from the tool bar, select the database from the list of available databases. For this example, select the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
1. On the **File** menu, select **New Query**.  
  
1. Copy and paste the following example into the query editor. The example creates the `Purchasing.uspVendorAllInfo` procedure, which returns the names of all the vendors in the [!INCLUDE [ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] database, the products they supply, their credit ratings, and their availability.  
  
    ```sql  
    CREATE PROCEDURE Purchasing.uspVendorAllInfo  
    WITH EXECUTE AS CALLER  
    AS  
        SET NOCOUNT ON;  
        SELECT v.Name AS Vendor, p.Name AS 'Product name',   
          v.CreditRating AS 'Rating',   
          v.ActiveFlag AS Availability  
        FROM Purchasing.Vendor v   
        INNER JOIN Purchasing.ProductVendor pv  
          ON v.BusinessEntityID = pv.BusinessEntityID   
        INNER JOIN Production.Product p  
          ON pv.ProductID = p.ProductID   
        ORDER BY v.Name ASC;  
    GO   
    ```  
      
1. On the **File** menu, select **New Query**.  
  
1. Copy and paste the following example into the query editor. The example modifies the `uspVendorAllInfo` procedure. The `EXECUTE AS CALLER` clause is removed and the body of the procedure is modified to return only those vendors that supply the specified product. The `LEFT` and `CASE` functions customize the appearance of the result set.  


      > [!IMPORTANT]  
      > Dropping and recreating an existing stored procedure removes permissions that have been explicitly granted to the stored procedure. Use `ALTER` to modify the existing stored procedure instead.
  
    ```sql  
    ALTER PROCEDURE Purchasing.uspVendorAllInfo  
        @Product varchar(25)   
    AS  
        SET NOCOUNT ON;  
        SELECT LEFT(v.Name, 25) AS Vendor, LEFT(p.Name, 25) AS 'Product name',   
        'Rating' = CASE v.CreditRating   
            WHEN 1 THEN 'Superior'  
            WHEN 2 THEN 'Excellent'  
            WHEN 3 THEN 'Above average'  
            WHEN 4 THEN 'Average'  
            WHEN 5 THEN 'Below average'  
            ELSE 'No rating'  
            END  
        , Availability = CASE v.ActiveFlag  
            WHEN 1 THEN 'Yes'  
            ELSE 'No'  
            END  
        FROM Purchasing.Vendor AS v   
        INNER JOIN Purchasing.ProductVendor AS pv  
          ON v.BusinessEntityID = pv.BusinessEntityID   
        INNER JOIN Production.Product AS p   
          ON pv.ProductID = p.ProductID   
        WHERE p.Name LIKE @Product  
        ORDER BY v.Name ASC;  
    GO  
    ```  
  
1. To save the modifications to the procedure definition, on the **Query** menu, select **Execute**.  
  
1. To save the updated procedure definition as a [!INCLUDE [tsql](../../includes/tsql-md.md)] script, on the **File** menu, select **Save As**. Accept the file name or replace it with a new name, and then select **Save**.  
  
1. To run the modified stored procedure, execute the following example.  
  
    ```sql  
    EXEC Purchasing.uspVendorAllInfo N'LL Crankarm';  
    GO
    ```  
  
## Related content

- [ALTER PROCEDURE (Transact-SQL)](../../t-sql/statements/alter-procedure-transact-sql.md)
- [SQL Injection](../security/sql-injection.md)
