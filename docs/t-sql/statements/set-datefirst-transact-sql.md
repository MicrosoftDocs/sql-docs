---
title: "SET DATEFIRST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SET DATEFIRST"
  - "SET_DATEFIRST_TSQL"
  - "DATEFIRST_TSQL"
  - "DATEFIRST"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "first day of week [SQL Server]"
  - "day of week [SQL Server]"
  - "SET DATEFIRST option [SQL Server]"
  - "DATEFIRST option [SQL Server]"
  - "weekdays [SQL Server]"
  - "options [SQL Server], date"
ms.assetid: 6b0d0e52-8ac1-4f88-b091-f98d6fb8574a
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET DATEFIRST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Sets the first day of the week to a number from 1 through 7.  
  
 For an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
SET DATEFIRST { number | @number_var }   
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
SET DATEFIRST 7 ;  
```  
  
## Arguments  
 *number* | **@**_number_var_  
 Is an integer that indicates the first day of the week. It can be one of the following values.  
  
|Value|First day of the week is|  
|-----------|------------------------------|  
|**1**|Monday|  
|**2**|Tuesday|  
|**3**|Wednesday|  
|**4**|Thursday|  
|**5**|Friday|  
|**6**|Saturday|  
|**7** (default, U.S. English)|Sunday|  
  
## Remarks  
 To see the current setting of SET DATEFIRST, use the [@@DATEFIRST](../../t-sql/functions/datefirst-transact-sql.md) function.  
  
 The setting of SET DATEFIRST is set at execute or run time and not at parse time.  
  
 Specifying SET DATEFIRST has no effect on DATEDIFF. DATEDIFF always uses Sunday as the first day of the week to ensure the function is deterministic.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example displays the day of the week for a date value and shows the effects of changing the `DATEFIRST` setting.  
  
```  
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
  
## See Also  
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  

