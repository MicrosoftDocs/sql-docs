---
title: "MSSQLSERVER_125"
description: "MSSQLSERVER_125"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "125"
helpviewer_keywords:
  - "125 (Database Engine error)"
---
# MSSQLSERVER_125
 [!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|125|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|Case expressions may only be nested to level %d.|  
  
## Explanation  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows for only 10 levels of nesting in CASE expressions.  
  
## User Action  
Reduce the level of CASE statements to 10 or less.  
  
## Related content

- [CASE (Transact-SQL)](../../t-sql/language-elements/case-transact-sql.md)
