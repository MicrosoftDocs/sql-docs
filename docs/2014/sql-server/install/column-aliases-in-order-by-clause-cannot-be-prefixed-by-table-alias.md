---
title: "Column aliases in ORDER BY clause cannot be prefixed by table alias | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "aliases [SQL Server], columns"
ms.assetid: fee7328f-6e8d-4005-930b-56fb6f17e0b2
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Column aliases in ORDER BY clause cannot be prefixed by table alias
  In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later, column aliases in the ORDER BY clause cannot be prefixed by the table alias.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 For example, the following query executes in [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], but returns an error in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]:  
  
```  
USE AdventureWorks2012;  
GO  
SELECT FirstName AS f, LastName AS l  
FROM Person.Contact p  
ORDER BY p.l  
```  
  
 The [!INCLUDE[ssDEversion10](../../includes/ssdeversion10-md.md)] does not match `p.l` in the `ORDER BY` clause to a valid column in the table.  
  
### Exception  
 If the prefixed column alias that is specified in the ORDER BY clause is a valid column name in the specified table, the query executes without error; in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the semantics of the statement might be different. For example, the column alias (`id`) specified in the following statement is a valid column name in the `sysobjects` table. In [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], when the statement executes, the `CAST` operation is performed after the result set is sorted. This means the `name` column is used in the sort operation. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], the `CAST` operation occurs before the sort operation. This means the `id` column in the table is used in the sort operation and returns the result set in an unexpected order.  
  
```  
SELECT CAST (o.name AS char(128)) AS id  
FROM sysobjects AS o  
ORDER BY o.id;  
```  
  
## Corrective Action  
 Modify queries that use column aliases prefixed by table aliases in the ORDER BY clause in either of the following ways:  
  
-   Do not prefix the column alias in the ORDER BY clause, if possible.  
  
-   Replace the column alias with the column name.  
  
 For example, both of the following queries execute without error in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]:  
  
```  
USE AdventureWorks2012;  
GO  
SELECT FirstName AS f, LastName AS l  
FROM Person.Contact p  
ORDER BY l  
  
USE AdventureWorks2012;  
GO  
SELECT FirstName AS f, LastName AS l  
FROM Person.Contact p  
ORDER BY p.LastName  
```  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
