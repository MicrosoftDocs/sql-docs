---
title: "GetRoot (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "7/22/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "GetRoot"
  - "GetRoot_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GetRoot [Database Engine]"
ms.assetid: 240b70f1-eeda-44ab-b4bb-9e4af80fa7c0
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# GetRoot (Database Engine)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the root of the hierarchy tree. GetRoot() is a static method.
  
## Syntax  
  
```sql
-- Transact-SQL syntax  
hierarchyid::GetRoot ( )   
```  
  
```sql
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
  
## See also
[hierarchyid Data Type Method Reference](https://msdn.microsoft.com/library/01a050f5-7580-4d5f-807c-7f11423cbb06)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
  
  
