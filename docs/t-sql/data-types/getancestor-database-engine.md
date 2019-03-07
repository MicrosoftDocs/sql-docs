---
title: "GetAncestor (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "07/22/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "GetAncestor_TSQL"
  - "GetAncestor"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GetAncestor [Database Engine]"
ms.assetid: b96a986f-d5e4-4034-8013-de7974594ee9
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# GetAncestor (Database Engine)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a **hierarchyid** representing the *n*th ancestor of *this*.
  
## Syntax  
  
```sql
-- Transact-SQL syntax  
child.GetAncestor ( n )   
```  
  
```sql
-- CLR syntax  
SqlHierarchyId GetAncestor ( int n )  
```  
  
## Arguments  
*n*  
An **int**, representing the number of levels to go up in the hierarchy.
  
## Return types
**SQL Server return type:hierarchyid**
  
**CLR return type:SqlHierarchyId**
  
## Remarks  
Used to test whether each node in the output has the current node as an ancestor at the specified level.
  
If a number greater than [GetLevel()](../../t-sql/data-types/getlevel-database-engine.md) is passed, NULL is returned.
  
If a negative number is passed, an exception is raised.
  
## Examples  
  
### A. Finding the child nodes of a parent  
`GetAncestor(1)` returns the employees that have `david0` as their immediate ancestor (their parent). The following example uses `GetAncestor(1)`.
  
```sql
DECLARE @CurrentEmployee hierarchyid  
SELECT @CurrentEmployee = OrgNode FROM HumanResources.EmployeeDemo  
WHERE LoginID = 'adventure-works\david0'  
  
SELECT OrgNode.ToString() AS Text_OrgNode, *  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode.GetAncestor(1) = @CurrentEmployee ;  
```  
  
### B. Returning the grandchildren of a parent  
`GetAncestor(2)` returns the employees that are two levels down in the hierarchy from the current node. These employees are the grandchildren of the current node. The following example uses `GetAncestor(2)`.
  
```sql
DECLARE @CurrentEmployee hierarchyid  
SELECT @CurrentEmployee = OrgNode FROM HumanResources.EmployeeDemo  
WHERE LoginID = 'adventure-works\ken0'  
  
SELECT OrgNode.ToString() AS Text_OrgNode, *  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode.GetAncestor(2) = @CurrentEmployee ;  
```  
  
### C. Returning the current row  
To return the current node by using `GetAncestor(0)`, execute the following code.
  
```sql
DECLARE @CurrentEmployee hierarchyid  
SELECT @CurrentEmployee = OrgNode FROM HumanResources.EmployeeDemo  
WHERE LoginID = 'adventure-works\david0'  
  
SELECT OrgNode.ToString() AS Text_OrgNode, *  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode.GetAncestor(0) = @CurrentEmployee ;  
```  
  
### D. Returning a hierarchy level if a table isn't present  
`GetAncestor` returns the selected level in the hierarchy even if a table isn't present. For example, the following code specifies a current employee and returns the `hierarchyid` of the ancestor of the current employee without reference to a table.
  
```sql
DECLARE @CurrentEmployee hierarchyid ;  
DECLARE @TargetEmployee hierarchyid ;  
SELECT @CurrentEmployee = '/2/3/1.2/5/3/' ;  
SELECT @TargetEmployee = @CurrentEmployee.GetAncestor(2) ;  
SELECT @TargetEmployee.ToString(), @TargetEmployee ;  
```  
  
### E. Calling a common language runtime method  
The following code snippet calls the `GetAncestor()` method.
  
```sql
this.GetAncestor(1)  
```  
  
## See also
[IsDescendantOf &#40;Database Engine&#41;](../../t-sql/data-types/isdescendantof-database-engine.md)  
[hierarchyid Data Type Method Reference](https://msdn.microsoft.com/library/01a050f5-7580-4d5f-807c-7f11423cbb06)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
  