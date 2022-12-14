---
title: "GetLevel (Database Engine)"
description: "GetLevel (Database Engine)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/22/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "GetLevel"
  - "GetLevel_TSQL"
helpviewer_keywords:
  - "GetLevel [Database Engine]"
dev_langs:
  - "TSQL"
---

# GetLevel (Database Engine)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns an integer that represents the depth of the node *this* in the tree.
  
## Syntax  
  
```syntaxsql
-- Transact-SQL syntax  
node.GetLevel ( )   
```  
  
```syntaxsql
-- CLR syntax  
SqlInt16 GetLevel ( )   
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types  
**SQL Server return type:smallint**
  
**CLR return type:SqlInt16**
  
## Remarks  
Used to determine the level of one or more nodes or to filter the nodes to members of a specified level. The root of the hierarchy is level 0.
  
GetLevel is useful for breadth-first search indexes. For more information, see [Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md).
  
## Examples  
  
### A. Returning the hierarchy level as a column  
The following example returns a text representation of the **hierarchyid**, and then the hierarchy level as the **EmpLevel** column for all rows in the table:
  
```sql
SELECT OrgNode.ToString() AS Text_OrgNode,   
OrgNode.GetLevel() AS EmpLevel, *  
FROM HumanResources.EmployeeDemo;  
```  
  
### B. Returning all members of a hierarchy level  
The following example returns all rows in the table at the hierarchy level 2:
  
```sql
SELECT OrgNode.ToString() AS Text_OrgNode,   
OrgNode.GetLevel() AS EmpLevel, *  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode.GetLevel() = 2;  
```  
  
### C. Returning the root of the hierarchy  
The following example returns the root of the hierarchy level:
  
```sql
SELECT OrgNode.ToString() AS Text_OrgNode,   
OrgNode.GetLevel() AS EmpLevel, *  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode.GetLevel() = 0;  
```  
  
### D. CLR example  
The following code snippet calls the GetLevel() method:
  
```sql
this.GetLevel()  
```  
  
## See also
[hierarchyid Data Type Method Reference](./hierarchyid-data-type-method-reference.md)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
