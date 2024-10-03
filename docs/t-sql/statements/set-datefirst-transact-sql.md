---
title: "SET DATEFIRST (Transact-SQL)"
description: SET DATEFIRST (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 04/04/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET DATEFIRST"
  - "SET_DATEFIRST_TSQL"
  - "DATEFIRST_TSQL"
  - "DATEFIRST"
helpviewer_keywords:
  - "first day of week [SQL Server]"
  - "day of week [SQL Server]"
  - "SET DATEFIRST option [SQL Server]"
  - "DATEFIRST option [SQL Server]"
  - "weekdays [SQL Server]"
  - "options [SQL Server], date"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET DATEFIRST (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Sets the first day of the week to a number from 1 through 7. 
  
 For an overview of all [!INCLUDE [tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions (Transact-SQL)](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax
  
#### Syntax for SQL Server and Azure SQL Database  

```syntaxsql 
SET DATEFIRST { number | @number_var }   
```  
  
#### Syntax for Azure Synapse Analytics and Parallel Data Warehouse  

```syntaxsql 
SET DATEFIRST 7 ;  
```  
  

## Arguments

#### *number* | *@number_var*  

 Is an integer that indicates the first day of the week. It can be one of the following values.  
  
|Value|First day of the week is|  
|-----------|------------------------------|  
| `1` |Monday|  
| `2` |Tuesday|  
| `3` |Wednesday|  
| `4` |Thursday|  
| `5` |Friday|  
| `6` |Saturday|  
| `7` (default, U.S. English) |Sunday|  
  
## Remarks

 To see the current setting of SET DATEFIRST, use the [@@DATEFIRST](../../t-sql/functions/datefirst-transact-sql.md) function.  
  
 The setting of SET DATEFIRST is set at execute or run time and not at parse time.  
  
 Specifying SET DATEFIRST has no effect on DATEDIFF. DATEDIFF always uses Sunday as the first day of the week to ensure the function is deterministic.  

 Like all [SET Statements](../../t-sql/statements/set-statements-transact-sql.md), SET DATEFIRST affects the current session.  

## Permissions

 Requires membership in the **public** role.  
  
## Examples

 The following example displays the day of the week for a date value and shows the effects of changing the `DATEFIRST` setting.  
  
```sql
-- SET DATEFIRST to U.S. English default value of 7.  
SET DATEFIRST 7;  
  
SELECT CAST('1999-1-1' AS datetime2) AS SelectDate  
    ,DATEPART(dw, '1999-1-1') AS DayOfWeek;  
-- January 1, 1999 is a Friday. Because the U.S. English default   
-- specifies Sunday as the first day of the week, DATEPART of 1999-1-1  
-- (Friday) yields a value of 6, because Friday is the sixth day of the   
-- week when you start with Sunday as day 1.  
  
SET DATEFIRST 3;  
-- Because Wednesday is now considered the first day of the week,  
-- DATEPART now shows that 1999-1-1 (a Friday) is the third day of the   
-- week. The following DATEPART function should return a value of 3.  
SELECT CAST('1999-1-1' AS datetime2) AS SelectDate  
    ,DATEPART(dw, '1999-1-1') AS DayOfWeek;  
GO  
```  

## Related content

- [SET Statements (Transact-SQL)](../../t-sql/statements/set-statements-transact-sql.md)
