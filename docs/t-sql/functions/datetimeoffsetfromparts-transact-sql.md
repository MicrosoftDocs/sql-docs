---
title: "DATETIMEOFFSETFROMPARTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DATETIMEOFFSETFROMPARTS_TSQL"
  - "DATETIMEOFFSETFROMPARTS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DATETIMEOFFSETFROMPARTS function"
ms.assetid: 463da1f4-b4b6-45a3-9a95-ea1f99575542
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DATETIMEOFFSETFROMPARTS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all-md](../../includes/tsql-appliesto-ss2012-all-md.md)]

Returns a **datetimeoffset** value for the specified date and time arguments. The returned value has a precision specified by the precision argument, and an offset as specified by the offset arguments.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DATETIMEOFFSETFROMPARTS ( year, month, day, hour, minute, seconds, fractions, hour_offset, minute_offset, precision )  
```  
  
## Arguments  

*year*  
An integer expression that specifies a year.  
  
*month*  
An integer expression that specifies a month.  
  
*day*  
An integer expression that specifies a day.  
  
*hour*  
An integer expression that specifies the hours.  
  
*minute*  
An integer expression that specifies the minutes.  
  
*seconds*  
An integer expression that specifies the seconds.  
  
*fractions*  
An integer expression that specifies a fractional seconds value.  
  
*hour_offset*  
An integer expression that specifies the hour portion of the time zone offset.  
  
*minute_offset*  
An integer expression that specifies the minute portion of the time zone offset.  
  
*precision*  
An integer literal value that specifies the precision of the **datetimeoffset** value that `DATETIMEOFFSETFROMPARTS` will return.  
  
## Return types
**datetimeoffset(** *precision* **)**  
  
## Remarks  

`DATETIMEOFFSETFROMPARTS` returns a fully initialized **datetimeoffset** data type. The offset arguments represent the time zone offset. For omitted offset arguments, `DATETIMEOFFSETFROMPARTS` assumes a time zone offset of `00:00` - in other words, no time zone offset. For specified offset arguments, `DATETIMEOFFSETFROMPARTS` expects values for both arguments, and both values positive or negative. If *minute_offset* has a value and *hour_offset* has no value, `DATETIMEOFFSETFROMPARTS` will raise an error. `DATETIMEOFFSETFROMPARTS` will raise an error if the other arguments have invalid values. If at least one required arguments has a `NULL` value, then `DATETIMEOFFSETFROMPARTS` will return `NULL`. However, if the *precision* argument has a `NULL` value, then `DATETIMEOFFSETFROMPARTS` will raise an error.  
  
The *fractions* argument depends on the precision argument. For example, for a precision value of 7, each fraction represents 100 nanoseconds; for a precision of 3, each fraction represents a millisecond. For a precision value of zero, the value of fractions must also be zero; otherwise, `DATETIMEOFFSETFROMPARTS` will raise an error.  
  
This function supports remoting to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] servers and above. It will not support remoting to servers that have a version below [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].
  
## Examples  
  
### A. An example without fractions of a second  
  
```sql
SELECT DATETIMEOFFSETFROMPARTS ( 2010, 12, 31, 14, 23, 23, 0, 12, 0, 7 ) AS Result;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
Result  
----------------------------------
2010-12-31 14:23:23.0000000 +12:00  
  
(1 row(s) affected)  
```  
  
### B. Example with fractions of a second  

This example shows the use of the *fractions* and *precision* parameters:  

1. When *fractions* has a value of 5, and *precision* has a value of 1, the value of *fractions* represents 5/10 of a second.  

2. When *fractions* has a value of 50, and *precision* has a value of 2, the value of *fractions* represents 50/100 of a second.  

3. When *fractions* has a value of 500, and *precision* has a value of 3, then the value of *fractions* represents 500/1000 of a second.  
  
```sql
SELECT DATETIMEOFFSETFROMPARTS ( 2011, 8, 15, 14, 30, 00, 5, 12, 30, 1 );  
SELECT DATETIMEOFFSETFROMPARTS ( 2011, 8, 15, 14, 30, 00, 50, 12, 30, 2 );  
SELECT DATETIMEOFFSETFROMPARTS ( 2011, 8, 15, 14, 30, 00, 500, 12, 30, 3 );  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
----------------------------------  
2011-08-15 14:30:00.5 +12:30  
  
(1 row(s) affected)  
  
----------------------------------  
2011-08-15 14:30:00.50 +12:30  
  
(1 row(s) affected)  
  
----------------------------------  
2011-08-15 14:30:00.500 +12:30  
  
(1 row(s) affected)  
```  
  
## See also
[datetimeoffset &#40;Transact-SQL&#41;](../../t-sql/data-types/datetimeoffset-transact-sql.md)  
[AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md)
  
  


