---
title: "GetLevel (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "GetLevel"
  - "GetLevel_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GetLevel [Database Engine]"
ms.assetid: 81577d7e-8ff6-4e73-b7f4-94c03d4921e7
caps.latest.revision: 17
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# GetLevel (Database Engine)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns an integer that represents the depth of the node *this* in the tree.  
  
## Syntax  
  
```  
  
-- Transact-SQL syntax  
node.GetLevel ( )   
```  
  
```  
  
-- CLR syntax  
SqlInt16 GetLevel ( )   
```  
  
## Return Types  
 **SQL Server return type:smallint**  
  
 **CLR return type:SqlInt16**  
  
## Remarks  
 Used to determine the level of one or more nodes or to filter the nodes to members of a specified level. The root of the hierarchy is level 0.  
  
 GetLevel is very useful for breadth-first search indexes. For more information, see [Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md).  
  
## Examples  
  
### A. Returning the hierarchy level as a column  
 The following example returns a text representation of the **hierarchyid**, and then the hierarchy level as the **EmpLevel** column for all rows in the table:  
  
```  
SELECT OrgNode.ToString() AS Text_OrgNode,   
OrgNode.GetLevel() AS EmpLevel, *  
FROM HumanResources.EmployeeDemo;  
```  
  
### B. Returning all members of a hierarchy level  
 The following example returns all rows in the table at the hierarchy level 2:  
  
```  
SELECT OrgNode.ToString() AS Text_OrgNode,   
OrgNode.GetLevel() AS EmpLevel, *  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode.GetLevel() = 2;  
  
```  
  
### C. Returning the root of the hierarchy  
 The following example returns the root of the hierarchy level:  
  
```  
SELECT OrgNode.ToString() AS Text_OrgNode,   
OrgNode.GetLevel() AS EmpLevel, *  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode.GetLevel() = 0;  
  
```  
  
### D. CLR example  
 The following code snippet calls the GetLevel() method:  
  
```  
this.GetLevel()  
  
```  
  
## See Also  
 [hierarchyid Data Type Method Reference](http://msdn.microsoft.com/library/01a050f5-7580-4d5f-807c-7f11423cbb06)   
 [Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)   
 [hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)  
  
  