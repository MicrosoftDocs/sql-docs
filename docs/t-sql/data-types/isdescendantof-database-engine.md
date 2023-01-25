---
title: "IsDescendantOf (Database Engine)"
description: "IsDescendantOf (Database Engine)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/22/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "IsDescendant_TSQL"
  - "IsDescendant"
helpviewer_keywords:
  - "IsDescendantOf [Database Engine]"
dev_langs:
  - "TSQL"
---
# IsDescendantOf (Database Engine)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns true if *this* is a descendant of parent.
  
## Syntax  
  
```syntaxsql
-- Transact-SQL syntax  
child. IsDescendantOf ( parent )  
```  
  
```syntaxsql
-- CLR syntax  
SqlHierarchyId IsDescendantOf (SqlHierarchyId parent )  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*parent*  
The **hierarchyid** node for which the IsDescendantOf test should be performed.
  
## Return Types  
**SQL Server return type:bit**
  
**CLR return type:SqlBoolean**
  
## Remarks  
Returns true for all the nodes in the sub-tree rooted at parent, and false for all other nodes.
  
Parent is considered its own descendant.
  
## Examples  
  
### A. Using IsDescendantOf in a WHERE clause  
The following example returns a manager and the employees that report to the manager:
  
```sql
DECLARE @Manager hierarchyid  
SELECT @Manager = OrgNode FROM HumanResources.EmployeeDemo  
  WHERE LoginID = 'adventure-works\dylan0'  
  
SELECT * FROM HumanResources.EmployeeDemo  
WHERE OrgNode.IsDescendantOf(@Manager) = 1  
```  
  
### B. Using IsDescendantOf to evaluate a relationship  
The following code declares and populates three variables. It then evaluates the hierarchical relationship and returns one of two printed results based on the comparison:
  
```sql
DECLARE @Manager hierarchyid, @Employee hierarchyid, @LoginID nvarchar(256)  
SELECT @Manager = OrgNode FROM HumanResources.EmployeeDemo  
WHERE LoginID = 'adventure-works\terri0' ;  
  
SELECT @Employee = OrgNode, @LoginID = LoginID FROM HumanResources.EmployeeDemo  
  WHERE LoginID = 'adventure-works\rob0'  
  
IF @Employee.IsDescendantOf(@Manager) = 1  
   BEGIN  
    PRINT 'LoginID ' + @LoginID + ' is a subordinate of the selected Manager.'  
   END  
ELSE  
   BEGIN  
    PRINT 'LoginID ' + @LoginID + ' is not a subordinate of the selected Manager.' ;  
   END  
```  
  
### C. Calling a common language runtime method  
The following code snippet calls the `IsDescendantOf()` method.
  
```sql
this.IsDescendantOf(Parent)  
```  
  
## See also
[hierarchyid Data Type Method Reference](./hierarchyid-data-type-method-reference.md)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
  
