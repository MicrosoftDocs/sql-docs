---
title: "Modify a Stored Procedure"
description: Learn how to modify a stored procedure in SQL Server by using SQL Server Management Studio or Transact-SQL.
ms.service: sql
ms.subservice: stored-procedures
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying stored procedures"
  - "editing stored procedures"
  - "stored procedures [SQL Server], modifying"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: "10/21/2021"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Modify a Stored Procedure
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

<a name="Top"></a> This article describes how to modify a stored procedure in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. 
  
##  <a name="Restrictions"></a> Limitations and restrictions  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures cannot be modified to be CLR stored procedures and vice versa.  
  
 If the previous procedure definition was created using WITH ENCRYPTION or WITH RECOMPILE, these options are enabled only if they are included in the ALTER PROCEDURE statement.  
  
## <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER PROCEDURE permission on the procedure.  
    
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
 
To modify a procedure in SQL Server Management Studio:
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, expand the database in which the procedure belongs, and then expand **Programmability**.  
  
3.  Expand **Stored Procedures**, right-click the procedure to modify, and then select **Modify**.  
  
4.  Modify the text of the stored procedure.  
  
5.  To test the syntax, on the **Query** menu, select **Parse**.  
  
6.  To save the modifications to the procedure definition, on the **Query** menu, select **Execute**.  
  
7.  To save the updated procedure definition as a [!INCLUDE[tsql](../../includes/tsql-md.md)] script, on the **File** menu, select **Save As**. Accept the file name or replace it with a new name, and then select **Save**.  

> [!IMPORTANT]  
>  Validate all user input. Do not concatenate user input before you validate it. Never execute a command constructed from unvalidated user input. For more information, see [SQL Injection](../security/sql-injection.md)
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
 
 To modify a procedure using T-SQL commands:
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, expand the database in which the procedure belongs. Or, from the tool bar, select the database from the list of available databases. For this example, select the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
3.  On the **File** menu, select **New Query**.  
  
4.  Copy and paste the following example into the query editor. The example creates the `uspVendorAllInfo` procedure, which returns the names of all the vendors in the [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] database, the products they supply, their credit ratings, and their availability.  
  
    ```sql  
    IF OBJECT_ID ( 'Purchasing.uspVendorAllInfo', 'P' ) IS NOT NULL   
        DROP PROCEDURE Purchasing.uspVendorAllInfo;  
    GO  
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

      > [!IMPORTANT]  
      >  Dropping and recreating an existing stored procedure removes permissions that have been explicitly granted to the stored procedure. Use ALTER instead.
      
5.  On the **File** menu, select **New Query**.  
  
6.  Copy and paste the following example into the query editor. The example modifies the `uspVendorAllInfo` procedure. The `EXECUTE AS CALLER` clause is removed and the body of the procedure is modified to return only those vendors that supply the specified product. The `LEFT` and `CASE` functions customize the appearance of the result set.  
  
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
  
7.  To save the modifications to the procedure definition, on the **Query** menu, select **Execute**.  
  
8.  To save the updated procedure definition as a [!INCLUDE[tsql](../../includes/tsql-md.md)] script, on the **File** menu, select **Save As**. Accept the file name or replace it with a new name, and then select **Save**.  
  
9. To run the modified stored procedure, execute the following example.  
  
    ```sql  
    EXEC Purchasing.uspVendorAllInfo N'LL Crankarm';  
    GO  
    ```  
  
## Next steps

 - [ALTER PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-procedure-transact-sql.md)  
 - [SQL Injection](../security/sql-injection.md)