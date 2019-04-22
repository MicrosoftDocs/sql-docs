---
title: "CONCAT_WS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/25/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: conceptual
f1_keywords: 
  - "CONCAT_WS"
  - "CONCAT_WS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CONCAT_WS function"
ms.assetid: f1375fd7-a2fd-48bf-922a-4f778f0deb1f
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CONCAT_WS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2017-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2017-asdb-asdw-xxx-md.md)]

This function returns a string resulting from the concatenation, or joining, of two or more string values in an end-to-end manner. It separates those concatenated string values with the delimiter specified in the first function argument. (`CONCAT_WS` indicates *concatenate with separator*.)

##  Syntax   
```sql
CONCAT_WS ( separator, argument1, argument2 [, argumentN]... )
```

## Arguments   
separator  
An expression of any character type (`char`, `nchar`, `nvarchar`, or `varchar`).

argument1, argument2, argument*N*  
An expression of any type.

## Return types
A string value whose length and type depend on the input.

## Remarks   
`CONCAT_WS` takes a variable number of string arguments and concatenates (or joins) them into a single string. It separates those concatenated string values with the delimiter specified in the first function argument. `CONCAT_WS` requires a separator argument and a minimum of two other string value arguments; otherwise, `CONCAT_WS` will raise an error. `CONCAT_WS` implicitly converts all arguments to string types before concatenation. 

The implicit conversion to strings follows the existing rules for data type conversions. See [CONCAT (Transact-SQL)](../../t-sql/functions/concat-transact-sql.md) for more information about behavior and data type conversions.

### Treatment of NULL values

`CONCAT_WS` ignores the `SET CONCAT_NULL_YIELDS_NULL {ON|OFF}` setting.

If `CONCAT_WS` receives arguments with all NULL values, it will return an empty string of type varchar(1).

`CONCAT_WS` ignores null values during concatenation, and does not add the separator between null values. Therefore, `CONCAT_WS` can cleanly handle concatenation of strings that might have "blank" values - for example, a second address field. See example B for more information.

If a scenario involves null values separated by a delimiter, consider the `ISNULL` function. See example C for more information.

## Examples   

### A.  Concatenating values with separator
This example concatenates three columns from the sys.databases table, separating the values with a  `-`.   

```sql
SELECT CONCAT_WS( ' - ', database_id, recovery_model_desc, containment_desc) AS DatabaseInfo
FROM sys.databases;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]   

|DatabaseInfo |  
|---------|
|1 - SIMPLE - NONE |
|2 - SIMPLE - NONE |
|3 - FULL - NONE |
|4 - SIMPLE - NONE |


### B.  Skipping NULL values
This example ignores `NULL` values in the arguments list.

```sql
SELECT CONCAT_WS(',','1 Microsoft Way', NULL, NULL, 'Redmond', 'WA', 98052) AS Address;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]   

```sql
Address
------------   
1 Microsoft Way,Redmond,WA,98052
```

### C.  Generating CSV file from table
This example uses a comma `,` as the separator value, and adds the carriage return character `char(13)` in the column separated values format of the result set.

```sql
SELECT 
STRING_AGG(CONCAT_WS( ',', database_id, recovery_model_desc, containment_desc), char(13)) AS DatabaseInfo
FROM sys.databases
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]   

```sql
DatabaseInfo
------------   
1,SIMPLE,NONE
2,SIMPLE,NONE
3,FULL,NONE 
4,SIMPLE,NONE 
```

CONCAT_WS ignores NULL values in the columns. Wrap a nullable column with the `ISNULL` function, and provide a default value. See this example for more:

```sql
SELECT 
STRING_AGG(CONCAT_WS( ',', database_id, ISNULL(recovery_model_desc,''), ISNULL(containment_desc,'N/A')), char(13)) AS DatabaseInfo
FROM sys.databases;
```

## See also
 [CONCAT &#40;Transact-SQL&#41;](../../t-sql/functions/concat-transact-sql.md)  
 [FORMATMESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/formatmessage-transact-sql.md)  
 [QUOTENAME &#40;Transact-SQL&#41;](../../t-sql/functions/quotename-transact-sql.md)  
 [REPLACE &#40;Transact-SQL&#41;](../../t-sql/functions/replace-transact-sql.md)  
 [REVERSE &#40;Transact-SQL&#41;](../../t-sql/functions/reverse-transact-sql.md)  
 [STRING_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/string-agg-transact-sql.md)  
 [STRING_ESCAPE &#40;Transact-SQL&#41;](../../t-sql/functions/string-escape-transact-sql.md)  
 [STUFF &#40;Transact-SQL&#41;](../../t-sql/functions/stuff-transact-sql.md)  
 [TRANSLATE &#40;Transact-SQL&#41;](../../t-sql/functions/translate-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  

