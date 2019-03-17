---
title: "MSSQLSERVER_4186 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "4186 (Database Engine error)"
ms.assetid: 1ae88554-f291-45bc-a186-6f41d9cd0fca
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_4186
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|4186|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|Column '%ls.%.*ls' cannot be referenced in the OUTPUT clause because the column definition contains a subquery or references a function that performs user or system data access. A function is assumed by default to perform data access if it is not schemabound. Consider removing the subquery or function from the column definition or removing the column from the OUTPUT clause.|  
  
## Explanation  
 To prevent nondeterministic behavior, the OUTPUT clause cannot reference a column from a view or inline table-valued function when that column is defined by one of the following methods:  
  
-   A subquery.  
  
-   A user-defined function that performs user or system data access, or is assumed to perform such access.  
  
-   A computed column that contains a user-defined function that performs user or system data access in its definition.  
  
### Examples  
 **View Column Defined by a Subquery**  
  
 The following example creates a view that uses a subquery in the select list to define the column `State`. An UPDATE statement then references the `State` column in the OUTPUT clause and fails because ob the subquery in the select list.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE VIEW dbo.V1  
AS  
    SELECT City,  
-- subquery to return the State name  
           (SELECT Name FROM Person.StateProvince AS sp   
            WHERE sp.StateProvinceID = a.StateProvinceID) AS State  
    FROM Person.Address AS a;  
GO  
--Reference the State column in the OUTPUT clause of an UPDATE statement  
UPDATE dbo.V1   
SET City = City + 'Test'   
OUTPUT deleted.City, deleted.State, inserted.City, inserted.State  
WHERE State = 'Texas';  
GO  
```  
  
 **View Column Defined by a Function**  
  
 The following example creates a view that uses the data accessing, scalar function `dbo.ufnGetStock` in the select list to define the column `CurrentInventory`. An UPDATE statement then references the `CurrentInventory` column in the OUTPUT clause .  
  
```  
USE AdventureWorks2012;  
GO  
CREATE VIEW Production.ReorderLevels  
AS  
    SELECT ProductID, ProductModelID, ReorderPoint,  
           dbo.ufnGetStock(ProductID) AS CurrentInventory  
    FROM Production.Product;  
GO  
  
UPDATE Production.ReorderLevels  
SET ReorderPoint += CurrentInventory  
OUTPUT deleted.ReorderPoint, deleted.CurrentInventory,  
       inserted.ReorderPoint, inserted.CurrentInventory  
WHERE ProductModelID BETWEEN 75 and 80;  
```  
  
## User Action  
 Error 4186 can be corrected in one of the following ways:  
  
-   Use joins instead of subqueries to define the column in the view or function. For example, you can rewrite the view `dbo.V1` as follows.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    CREATE VIEW dbo.V1  
    AS  
        SELECT City, sp.Name AS State  
        FROM Person.Address AS a   
        JOIN Person.StateProvince AS sp   
        ON sp.StateProvinceID = a.StateProvinceID;  
    ```  
  
-   Examine the definition of the user-defined function. If the function does not perform user or system data access, alter the function to include the WITH SCHEMABINDING clause.  
  
-   Remove the column from the OUTPUT clause.  
  
## See Also  
 [OUTPUT Clause &#40;Transact-SQL&#41;](/sql/t-sql/queries/output-clause-transact-sql)  
  
  
