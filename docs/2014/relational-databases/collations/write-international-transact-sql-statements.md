---
title: "Write International Transact-SQL Statements | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "writing international statements"
  - "Transact-SQL international considerations"
  - "international considerations [SQL Server], Transact-SQL"
  - "Database Engine international considerations [SQL Server], Transact-SQL"
  - "statements [SQL Server], international"
  - "database international considerations [SQL Server], Transact-SQL"
  - "dates [SQL Server], international considerations"
ms.assetid: f0b10fee-27f7-45fe-aece-ccc3f63bdcdb
author: stevestein
ms.author: sstein
manager: craigg
---
# Write International Transact-SQL Statements
  Databases and database applications that use [!INCLUDE[tsql](../../includes/tsql-md.md)] statements will become more portable from one language to another, or will support multiple languages, if the following guidelines are followed:  
  
-   Replace all uses of the `char`, `varchar`, and `text` data types with `nchar`, `nvarchar`, and `nvarchar(max)`. By doing this, you do not have to consider code page conversion issues. For more information, see [Collation and Unicode Support](collation-and-unicode-support.md).  
  
-   When you perform month and day-of-week comparisons and operations, use the numeric date parts instead of the name strings. Different language settings return different names for the months and weekdays. For example, DATENAME(MONTH,GETDATE()) returns May when the language is set to U.S. English, returns Mai when the language is set to German, and returns mai when the language is set to French. Instead, use a function such as DATEPART that uses the number of the month instead of the name. Use the DATEPART names when you build result sets to be displayed to a user, because the date names are frequently more meaningful than a numeric representation. However, do not code any logic that depends on the displayed names being from a specific language.  
  
-   When you specify dates in comparisons or for input to INSERT or UPDATE statements, use constants that are interpreted the same way for all language settings:  
  
    -   ADO, OLE DB, and ODBC applications should use the ODBC timestamp, date, and time escape clauses of:  
  
         **{ ts'**yyyy**-**_mm_**-**_ddhh_**:**_mm_**:**_ss_[**.**_fff_] **'}** such as: **{ ts'**1998**-**09**-**24 10**:**02**:**20**' }**  
  
         **{ d'** _yyyy_ **-** _mm_ **-** _dd_ **'}** such as: **{ d'**1998**-**09**-**24**'}**  
  
         **{ t'** _hh_ **:** _mm_ **:** _ss_ **'}** such as: **{ t'**10:02:20**'}**  
  
    -   Applications that use other APIs, or [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, stored procedures, and triggers, should use the unseparated numeric strings. For example, *yyyymmdd* as 19980924.  
  
    -   Applications that use other APIs, or [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts, stored procedures, and triggers should use the CONVERT statement with an explicit style parameter for all conversions between the `time`, `date`, `smalldate`, `datetime`, **datetime2**, and `datetimeoffset` data types and character string data types. For example, the following statement is interpreted in the same way for all language or date format connection settings:  
  
        ```  
        SELECT *  
        FROM AdventureWorks2012.Sales.SalesOrderHeader  
        WHERE OrderDate = CONVERT(DATETIME, '20060719', 101)  
        ```  
  
         For more information, see [CAST and CONVERT &#40;Transact-SQL&#41;](/sql/t-sql/functions/cast-and-convert-transact-sql).  
  
  
