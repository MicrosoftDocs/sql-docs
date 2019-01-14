---
title: "CONCAT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CONCAT"
  - "CONCAT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CONCAT function"
ms.assetid: fce5a8d4-283b-4c47-95e5-4946402550d5
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CONCAT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all-md](../../includes/tsql-appliesto-ss2012-all-md.md)]

> [!div class="nextstepaction"]
> [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

This function returns a string resulting from the concatenation, or joining, of two or more string values in an end-to-end manner. (To add a separating value during concatenation, see [CONCAT_WS](../../t-sql/functions/concat-ws-transact-sql.md).)
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CONCAT ( string_value1, string_value2 [, string_valueN ] )  
```  
  
## Arguments  
*string_value*  
A string value to concatenate to the other values. The `CONCAT` function requires at least two *string_value* arguments, and no more than 254 *string_value* arguments.
  
## Return types  
*string_value*  
A string value whose length and type depend on the input.
  
## Remarks  
`CONCAT` takes a variable number of string arguments and concatenates (or joins) them into a single string. It requires a minimum of two input values; otherwise, `CONCAT` will raise an error. `CONCAT` implicitly converts all arguments to string types before concatenation. `CONCAT` implicitly converts null values to empty strings. If `CONCAT` receives arguments with all **NULL** values, it will return an empty string of type **varchar**(1). The implicit conversion to strings follows the existing rules for data type conversions. See [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md) for more information about data type conversions.
  
The return type depends on the type of the arguments. This table illustrates the mapping:
  
|Input type|Output type and length|  
|---|---|
|1. Any argument of<br><br />a SQL-CLR system type<br><br />a SQL-CLR UDT<br><br />or<br><br />`nvarchar(max)`|**nvarchar(max)**|  
|2. Otherwise, any argument of type<br><br />**varbinary(max)**<br><br />or<br><br />**varchar(max)**|**varchar(max)**, unless one of the parameters is an **nvarchar** of any length. In this case, `CONCAT` returns a result of type **nvarchar(max)**.|  
|3. Otherwise, any argument of type **nvarchar** of at most 4000 characters<br><br />( **nvarchar**(<= 4000) )|**nvarchar**(<= 4000)|  
|4. In all other cases|**varchar**(<= 8000) (a **varchar** of at most 8000 characters) unless one of the parameters is an nvarchar of any length. In that case, `CONCAT` returns a result of type **nvarchar(max)**.|  
  
When `CONCAT` receives **nvarchar** input arguments of length <= 4000 characters, or **varchar** input arguments of length <= 8000 characters, implicit conversions can affect the length of the result. Other data types have different lengths when implicitly converted to strings. For example, an **int** (14) has a string length of 12, while a **float** has a length of 32. Therefore, a concatenation of two integers returns a result with a length of no less than 24.
  
If none of the input arguments has a supported large object (LOB) type, then the return type truncates to 8000 characters in length, regardless of the return type. This truncation preserves space and supports plan generation efficiency.
  
The CONCAT function can be executed remotely on a linked server of version [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and above. For older linked servers, the CONCAT operation will happen locally, after the linked server returns the non-concatenated values.
  
## Examples  
  
### A. Using CONCAT  
  
```sql
SELECT CONCAT ( 'Happy ', 'Birthday ', 11, '/', '25' ) AS Result;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
Result  
-------------------------  
Happy Birthday 11/25  
  
(1 row(s) affected)  
```  
  
### B. Using CONCAT with NULL values  
  
```sql
CREATE TABLE #temp (  
    emp_name nvarchar(200) NOT NULL,  
    emp_middlename nvarchar(200) NULL,  
    emp_lastname nvarchar(200) NOT NULL  
);  
INSERT INTO #temp VALUES( 'Name', NULL, 'Lastname' );  
SELECT CONCAT( emp_name, emp_middlename, emp_lastname ) AS Result  
FROM #temp;  
```  

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
Result  
------------------  
NameLastname  
  
(1 row(s) affected)  
```  
  
## See also
 [CONCAT_WS &#40;Transact-SQL&#41;](../../t-sql/functions/concat-ws-transact-sql.md)   
 [FORMATMESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/formatmessage-transact-sql.md)  
 [QUOTENAME &#40;Transact-SQL&#41;](../../t-sql/functions/quotename-transact-sql.md)  
 [REPLACE &#40;Transact-SQL&#41;](../../t-sql/functions/replace-transact-sql.md)  
 [REVERSE &#40;Transact-SQL&#41;](../../t-sql/functions/reverse-transact-sql.md)  
 [STRING_AGG &#40;Transact-SQL&#41;](../../t-sql/functions/string-agg-transact-sql.md)  
 [STRING_ESCAPE &#40;Transact-SQL&#41;](../../t-sql/functions/string-escape-transact-sql.md)  
 [STUFF &#40;Transact-SQL&#41;](../../t-sql/functions/stuff-transact-sql.md)  
 [TRANSLATE &#40;Transact-SQL&#41;](../../t-sql/functions/translate-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  


