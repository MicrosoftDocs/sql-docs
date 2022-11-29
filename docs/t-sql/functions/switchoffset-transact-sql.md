---
title: "SWITCHOFFSET (Transact-SQL)"
description: "SWITCHOFFSET (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "12/02/2015"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SWITCHTZ"
  - "SWITCHTZ_TSQL"
helpviewer_keywords:
  - "dates [SQL Server], functions"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "SWITCHOFFSET function [SQL Server]"
  - "time [SQL Server], functions"
  - "date and time [SQL Server], SWITCHOFFSET"
  - "time zones [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SWITCHOFFSET (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a **datetimeoffset** value that is changed from the stored time zone offset to a specified new time zone offset.  
  
 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SWITCHOFFSET ( datetimeoffset_expression, timezoneoffset_expression )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *datetimeoffset_expression*  
 Is an expression that can be resolved to a **datetimeoffset(n)** value.  
  
 *timezoneoffset_expression*  
 Is an expression in the format [+|-]TZH:TZM or a signed integer (of minutes) that represents the time zone offset, and is assumed to be daylight-saving aware and adjusted.  
  
## Return Type  
 **datetimeoffset** with the fractional precision of the *datetimeoffset_expression* argument.  
  
## Remarks  
 Use SWITCHOFFSET to select a **datetimeoffset** value into a time zone offset that is different from the time zone offset that was originally stored. SWITCHOFFSET does not update the stored *time_zone* value.  
  
 SWITCHOFFSET can be used to update a **datetimeoffset** column.  
  
 Using SWITCHOFFSET with the function GETDATE() can cause the query to run slowly. This is because the query optimizer is unable to obtain accurate cardinality estimates for the datetime value. To resolve this problem, use the OPTION (RECOMPILE) query hint to force the query optimizer to recompile a query plan the next time the same query is executed. The optimizer will then have accurate cardinality estimates and will produce a more efficient query plan. For more information about the RECOMPILE query hint, see [Query Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql-query.md).  
  
```sql
DECLARE @dt datetimeoffset = switchoffset (CONVERT(datetimeoffset, GETDATE()), '-04:00');   
SELECT * FROM t    
WHERE c1 > @dt OPTION (RECOMPILE);  
```  
  
## Examples  
 The following example uses `SWITCHOFFSET` to display a different time zone offset than the value stored in the database.  
  
```sql  
CREATE TABLE dbo.test   
    (  
    ColDatetimeoffset datetimeoffset  
    );  
GO  
INSERT INTO dbo.test   
VALUES ('1998-09-20 7:45:50.71345 -5:00');  
GO  
SELECT SWITCHOFFSET (ColDatetimeoffset, '-08:00')   
FROM dbo.test;  
GO  
--Returns: 1998-09-20 04:45:50.7134500 -08:00  
SELECT ColDatetimeoffset  
FROM dbo.test;  
--Returns: 1998-09-20 07:45:50.7134500 -05:00  
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md)  
  
  


