---
title: "GetRoot (Database Engine)"
description: "GetRoot (Database Engine)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/22/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "GetRoot"
  - "GetRoot_TSQL"
helpviewer_keywords:
  - "GetRoot [Database Engine]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# GetRoot (Database Engine)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

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
  
## Related content

- [hierarchyid data type method reference](hierarchyid-data-type-method-reference.md)
- [Hierarchical data (SQL Server)](../../relational-databases/hierarchical-data-sql-server.md)
