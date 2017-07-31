---
title: "CONCAT_WS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "CONCAT_WS"
  - "CONCAT_WS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CONCAT_WS function"
ms.assetid: f1375fd7-a2fd-48bf-922a-4f778f0deb1f
caps.latest.revision: 5
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CONCAT_WS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssvNxt-asdb-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-asdb-xxxx-xxx.md)]

Concatenates a variable number of arguments with a delimiter specified in the 1st argument. (`CONCAT_WS` indicates *concatenate with separator*.)

##  Syntax   
```sql
CONCAT_WS ( separator, argument1, argument1 [, argumentN]… ) 
```

## Arguments   
separator  
Is an expression of any character type (`nvarchar`, `varchar`, `nchar`, or `char`).

argument1, argument2, argument*N*  
Is an expression of any type.

## Return types
String. The length and type depend on the input.

## Remarks   
`CONCAT_WS` takes a variable number of arguments and concatenates them into a single string using the first argument as separator. It requires a separator and a minimum of two arguments; otherwise, an error is raised. All arguments are implicitly converted to string types and are then concatenated. 

The implicit conversion to strings follows the existing rules for data type conversions. For more information about behavior and data type conversions, see [CONCAT (Transact-SQL)](../../t-sql/functions/concat-transact-sql.md).

### Treatment of NULL values

`CONCAT_WS` ignores the `SET CONCAT_NULL_YIELDS_NULL {ON|OFF}` setting.

If all the arguments are null, an empty string of type `varchar(1)` is returned. 

Null values are ignored during concatenation, and does not add the separator. This facilitates the common scenario of concatenating strings which often have blank values, such as a second address field. See example B.

If your scenario requires null values to be included with a separator, see example C using the `ISNULL` function.

## Examples   

### A.  Concatenating values with separator
The following example concatenates three columns from the sys.databases table, separating the values with a  `- `.   

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
The following example ignores `NULL` values in the arguments list.

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
The following example uses a comma as the separator and adds the carriage return character to result in the column separated values format.

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

CONCAT_WS will ignore NULL values in the columns. If some of the columns are nullable, wrap it with `ISNULL` function and provide default value like in the following example:

```sql
SELECT 
STRING_AGG(CONCAT_WS( ',', database_id, ISNULL(recovery_model_desc,''), ISNULL(containment_desc,'N/A')), char(13)) AS DatabaseInfo
FROM sys.databases;
```

## See also
[String Functions (Transact-SQL)](../../t-sql/functions/string-functions-transact-sql.md)  
[CONCAT (Transact-SQL)](../../t-sql/functions/concat-transact-sql.md)      

