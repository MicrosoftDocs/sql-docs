---
title: "UNICODE (Transact-SQL)"
description: "UNICODE (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "UNICODE"
  - "UNICODE_TSQL"
helpviewer_keywords:
  - "first character of input expression [SQL Server]"
  - "UNICODE function"
  - "Unicode [SQL Server], UNICODE function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# UNICODE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the integer value, as defined by the Unicode standard, for the first character of the input expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
UNICODE ( 'ncharacter_expression' )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
**'** *ncharacter_expression* **'**  
Is an **nchar** or **nvarchar** expression.  
  
## Return Types  
**int**  
  
## Remarks  
In versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the UNICODE function returns a UCS-2 codepoint in the range 000000 through 00FFFF which is capable of representing the 65,535 characters in the Unicode Basic Multilingual Plane (BMP). Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], when using [Supplementary Character (SC)](../../relational-databases/collations/collation-and-unicode-support.md#Supplementary_Characters) enabled collations, UNICODE returns a UTF-16 codepoint in the range 000000 through 10FFFF. For more information on Unicode support in the [!INCLUDE[ssde_md](../../includes/ssde_md.md)], see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md#Unicode_Defn). 
  
## Examples  
  
### A. Using UNICODE and the NCHAR function  
 The following example uses the `UNICODE` and `NCHAR` functions to print the UNICODE value of the first character of the string `Åkergatan 24`, and to print the actual first character, `Å`.  
  
```sql  
DECLARE @nstring NCHAR(12);  
SET @nstring = N'Åkergatan 24';  
SELECT UNICODE(@nstring), NCHAR(UNICODE(@nstring));  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
----------- -   
197         Å  
```  
  
### B. Using SUBSTRING, UNICODE, and CONVERT  
 The following example uses the `SUBSTRING`, `UNICODE`, and `CONVERT` functions to print the character number, the Unicode character, and the UNICODE value of each of the characters in the string `Åkergatan 24`.  
  
```sql  
-- The @position variable holds the position of the character currently  
-- being processed. The @nstring variable is the Unicode character   
-- string to process.  
DECLARE @position INT, @nstring NCHAR(12);  
-- Initialize the current position variable to the first character in   
-- the string.  
SET @position = 1;  
-- Initialize the character string variable to the string to process.   
-- Notice that there is an N before the start of the string, which   
-- indicates that the data following the N is Unicode data.  
SET @nstring = N'Åkergatan 24';  
-- Print the character number of the position of the string you are at,   
-- the actual Unicode character you are processing, and the UNICODE   
-- value for this particular character.  
PRINT 'Character #' + ' ' + 'Unicode Character' + ' ' + 'UNICODE Value';  
WHILE @position <= LEN(@nstring)  
-- While these are still characters in the character string,  

BEGIN;  
   SELECT @position AS [position],   
      SUBSTRING(@nstring, @position, 1) AS [character],  
      UNICODE(SUBSTRING(@nstring, @position, 1)) AS [code_point];  
   SET @position = @position + 1;  
END; 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Character # Unicode Character UNICODE Value  
  
----------- ----------------- -----------   
1           Å                 197           
  
----------- ----------------- -----------   
2           k                 107           
  
----------- ----------------- -----------   
3           e                 101           
  
----------- ----------------- -----------   
4           r                 114           
  
----------- ----------------- -----------   
5           g                 103           
  
----------- ----------------- -----------   
6           a                 97            
  
----------- ----------------- -----------   
7           t                 116           
  
----------- ----------------- -----------   
8           a                 97            
  
----------- ----------------- -----------   
9           n                 110           
  
----------- ----------------- -----------   
10                            32            
  
----------- ----------------- -----------   
11          2                 50            
  
----------- ----------------- -----------   
12          4                 52  
```  
  
## See Also  
 [ASCII &#40;Transact-SQL&#41;](../../t-sql/functions/ascii-transact-sql.md)  
 [CHAR &#40;Transact-SQL&#41;](../../t-sql/functions/char-transact-sql.md)  
 [NCHAR &#40;Transact-SQL&#41;](../../t-sql/functions/nchar-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)  
  
  

