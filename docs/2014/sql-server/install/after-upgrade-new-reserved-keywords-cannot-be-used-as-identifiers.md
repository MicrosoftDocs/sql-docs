---
title: "After upgrade, new reserved keywords cannot be used as identifiers | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "keywords [SQL Server], after upgrade"
  - "keywords [SQL Server], reserved"
  - "keywords [SQL Server]"
ms.assetid: cb242081-54f8-4273-a8ef-52f3751c25ef
author: mashamsft
ms.author: mathoma
manager: craigg
---
# After upgrade, new reserved keywords cannot be used as identifiers
  Upgrade Advisor detected the use of words that are reserved keywords. A reserved keyword cannot be used as an identifier or object name unless the name is delimited.  
  
## Component  
 Database Engine  
  
## Description  
 At compatibility level 90 or lower, the following words are not reserved keywords and can be used as identifiers or object names in [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts. At compatibility level 100, these words are fully reserved keywords and should not be used as identifiers or object names.  
  
-   EXTERNAL  
  
-   MERGE  
  
-   PIVOT  
  
-   REVERT  
  
-   STOPLIST  
  
-   TABLESAMPLE  
  
-   UNPIVOT  
  
## Corrective Action  
 We recommend that you rename the object. If that cannot be done before upgrading, use one of the following methods until the name can be changed:  
  
-   Retain the database compatibility level setting of 90 or lower.  
  
-   Refer to the object by using delimited identifiers. For example, the statement `CREATE TABLE [MERGE] ([MERGE] int);` uses brackets to delimit the object name MERGE.  
  
## External Resources  
 [Reserved Keywords &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reserved-keywords-transact-sql)  
  
 [MERGE &#40;Transact-SQL&#41;](/sql/t-sql/statements/merge-transact-sql)  
  
 [Delimited Identifiers (Database Engine)](https://go.microsoft.com/fwlink/?LinkId=112509)  
  
 [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level)  
  
  
