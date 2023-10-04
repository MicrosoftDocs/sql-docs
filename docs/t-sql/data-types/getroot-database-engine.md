---
title: "GetRoot (Database Engine)"
description: "GetRoot (Database Engine)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/22/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "GetRoot"
  - "GetRoot_TSQL"
helpviewer_keywords:
  - "GetRoot [Database Engine]"
dev_langs:
  - "TSQL"
---

# GetRoot (Database Engine)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the root of the hierarchy tree. GetRoot() is a static method.
  
## Syntax  
  
```syntaxsql
-- Transact-SQL syntax  
hierarchyid::GetRoot ( )   
```  
  
```syntaxsql
-- CLR syntax  
static SqlHierarchyId GetRoot ( )   
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types  
**SQL Server return type:hierarchyid**
  
**CLR return type:SqlHierarchyId**
  
## Remarks  
Used to determine the root node in a hierarchy tree.
  
## Examples  
  
### A. Transact-SQL example  
The following example returns the root of the hierarchy tree:
  
```sql
SELECT OrgNode.ToString() AS Text_OrgNode, *  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode = hierarchyid::GetRoot()  
```  
  
### B. CLR example  
The following code snippet calls the GetRoot() method:
  
```sql
SqlHierarchyId.GetRoot()  
```  
  
## See also
[hierarchyid Data Type Method Reference](./hierarchyid-data-type-method-reference.md)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
  
