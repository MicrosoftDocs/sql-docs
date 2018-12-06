---
title: "GetDescendant (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "7/22/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "GetDescendant_TSQL"
  - "GetDescendant"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GetDescendant [Database Engine]"
ms.assetid: f5f39596-033e-4243-acbc-caa188b45b03
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# GetDescendant (Database Engine)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns a child node of the parent.
  
## Syntax  
  
```sql
-- Transact-SQL syntax  
parent.GetDescendant ( child1 , child2 )   
```  
  
```sql
-- CLR syntax  
SqlHierarchyId GetDescendant ( SqlHierarchyId child1 , SqlHierarchyId child2 )   
```  
  
## Arguments  
*child1*  
NULL or the **hierarchyid** of a child of the current node.
  
*child2*  
NULL or the **hierarchyid** of a child of the current node.
  
## Return Types  
**SQL Server return type:hierarchyid**
  
**CLR return type:SqlHierarchyId**
  
## Remarks  
Returns one child node that is a descendant of the parent.
-   If parent is NULL, returns NULL.  
-   If parent is not NULL, and both child1 and child2 are NULL, returns a child of parent.  
-   If parent and child1 are not NULL, and child2 is NULL, returns a child of parent greater than child1.  
-   If parent and child2 are not NULL and child1 is NULL, returns a child of parent less than child2.  
-   If parent, child1, and child2 are not NULL, returns a child of parent greater than child1 and less than child2.  
-   If child1 is not NULL and not a child of parent, an exception is raised.  
-   If child2 is not NULL and not a child of parent, an exception is raised.  
-   If child1 >= child2, an exception is raised.  
  
GetDescendant is deterministic. Therefore, if GetDescendant is called with the same inputs, it will always produce the same output. However, the exact identity of the child produced can vary depending upon its relationship to the other nodes, as demonstrated in example C.
  
## Examples  
  
### A. Inserting a row as the least descendant node  
A new employee is hired, reporting to an existing employee at node `/3/1/`. Execute the following code to insert the new row by using the GetDescendant method without arguments to specify the new rows node as `/3/1/1/`:
  
```sql
DECLARE @Manager hierarchyid;   
SET @Manager = CAST('/3/1/' AS hierarchyid);  
  
INSERT HumanResources.EmployeeDemo (OrgNode, LoginID, Title, HireDate)  
VALUES  
(@Manager.GetDescendant(NULL, NULL),  
'adventure-works\FirstNewEmployee', 'Application Intern', '3/11/07') ;  
```  
  
### B. Inserting a row as a greater descendant node  
Another new employee is hired, reporting to the same manager as in example A. Execute the following code to insert the new row by using the GetDescendant method using the child 1 argument to specify that the node of the new row will follow the node in example A, becoming `/3/1/2/`:
  
```sql
DECLARE @Manager hierarchyid, @Child1 hierarchyid;  
  
SET @Manager = CAST('/3/1/' AS hierarchyid);  
SET @Child1 = CAST('/3/1/1/' AS hierarchyid);  
  
INSERT HumanResources.EmployeeDemo (OrgNode, LoginID, Title, HireDate)  
VALUES  
(@Manager.GetDescendant(@Child1, NULL),  
'adventure-works\SecondNewEmployee', 'Application Intern', '3/11/07') ;  
```  
  
### C. Inserting a row between two existing nodes  
A third employee is hired, reporting to the same manager as in example A. This example inserts the new row to a node greater than the `FirstNewEmployee` in example A, and less than the `SecondNewEmployee` in example B. Execute the following code by using the GetDescendant method. Use both the child1 argument and the child2 argument to specify that the node of the new row will become node `/3/1/1.1/`:
  
```sql
DECLARE @Manager hierarchyid, @Child1 hierarchyid, @Child2 hierarchyid;  
  
SET @Manager = CAST('/3/1/' AS hierarchyid);  
SET @Child1 = CAST('/3/1/1/' AS hierarchyid);  
SET @Child2 = CAST('/3/1/2/' AS hierarchyid);  
  
INSERT HumanResources.EmployeeDemo (OrgNode, LoginID, Title, HireDate)  
VALUES  
(@Manager.GetDescendant(@Child1, @Child2),  
'adventure-works\ThirdNewEmployee', 'Application Intern', '3/11/07') ;  
  
```  
  
After completing examples A, B, and C, the nodes added to the table will be peers with the following **hierarchyid** values:
  
`/3/1/1/`
  
`/3/1/1.1/`
  
`/3/1/2/`
  
Node `/3/1/1.1/` is greater than node `/3/1/1/` but at the same level in the hierarchy.
  
### D. Scalar examples  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports arbitrary insertions and deletions of any **hierarchyid** nodes. By using GetDescendant(), it is always possible to generate a node between any two **hierarchyid** nodes. Execute the following code to generate sample nodes using `GetDescendant`:
  
```sql
DECLARE @h hierarchyid = hierarchyid::GetRoot();  
DECLARE @c hierarchyid = @h.GetDescendant(NULL, NULL);  
SELECT @c.ToString();  
DECLARE @c2 hierarchyid = @h.GetDescendant(@c, NULL);  
SELECT @c2.ToString();  
SET @c2 = @h.GetDescendant(@c, @c2);  
SELECT @c2.ToString();  
SET @c = @h.GetDescendant(@c, @c2);  
SELECT @c.ToString();  
SET @c2 = @h.GetDescendant(@c, @c2);  
SELECT @c2.ToString();  
```  
  
### E. CLR example  
The following code snippet calls the `GetDescendant()` method:
  
```sql
SqlHierarchyId parent, child1, child2;  
parent = SqlHierarchyId.GetRoot();  
child1 = parent.GetDescendant(SqlHierarchyId.Null, SqlHierarchyId.Null);  
child2 = parent.GetDescendant(child1, SqlHierarchyId.Null);  
Console.Write(parent.GetDescendant(child1, child2).ToString());  
```  
  
## See also
[hierarchyid Data Type Method Reference](https://msdn.microsoft.com/library/01a050f5-7580-4d5f-807c-7f11423cbb06)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
  
  
