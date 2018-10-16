--<snippetAlterIndex1>    
USE AdventureWorks2012;
GO
ALTER INDEX PK_Employee_BusinessEntityID ON HumanResources.Employee
REBUILD;
GO
--</snippetAlterIndex1> 
  
--<snippetAlterIndex2>   
USE AdventureWorks2012;
GO
ALTER INDEX ALL ON Production.Product
REBUILD WITH (FILLFACTOR = 80, SORT_IN_TEMPDB = ON,
              STATISTICS_NORECOMPUTE = ON);
GO
--</snippetAlterIndex2>   

--<snippetAlterIndex3>        
USE AdventureWorks2012;
GO
ALTER INDEX PK_ProductPhoto_ProductPhotoID ON Production.ProductPhoto
REORGANIZE ;
GO
--</snippetAlterIndex3>   

--<snippetAlterIndex4>        
USE AdventureWorks2012;
GO
ALTER INDEX AK_SalesOrderHeader_SalesOrderNumber ON
    Sales.SalesOrderHeader
SET (
    STATISTICS_NORECOMPUTE = ON,
    IGNORE_DUP_KEY = ON,
    ALLOW_PAGE_LOCKS = ON
    ) ;
GO
--</snippetAlterIndex4> 
  
--<snippetAlterIndex5>         
USE AdventureWorks2012;
GO
ALTER INDEX IX_Employee_OrganizationNode ON HumanResources.Employee
DISABLE ;
GO
--</snippetAlterIndex5>   

--<snippetAlterIndex6>      
USE AdventureWorks2012;
GO
ALTER INDEX PK_Department_DepartmentID ON HumanResources.Department
DISABLE ;
GO
--</snippetAlterIndex6>   

--<snippetAlterIndex7>       
USE AdventureWorks2012;
GO
ALTER INDEX PK_Department_DepartmentID ON HumanResources.Department
REBUILD ;
GO
--</snippetAlterIndex7>   

--<snippetAlterIndex8>
ALTER TABLE HumanResources.EmployeeDepartmentHistory
CHECK CONSTRAINT FK_EmployeeDepartmentHistory_Department_DepartmentID;
GO
--</snippetAlterIndex8>   
