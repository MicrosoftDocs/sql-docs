---
title: "SOUNDEX (Transact-SQL)"
description: "SOUNDEX (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SOUNDEX"
  - "SOUNDEX_TSQL"
helpviewer_keywords:
  - "SOUNDEX function"
  - "comparing string data"
  - "strings [SQL Server], similarity"
  - "strings [SQL Server], comparing"
  - "SOUNDEX values"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SOUNDEX (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a four-character (SOUNDEX) code to evaluate the similarity of two strings.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
SOUNDEX ( character_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *character_expression*  
 Is an alphanumeric [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column.  
  
## Return Types  
 **varchar**  
  
## Remarks  
 SOUNDEX converts an alphanumeric string to a four-character code that is based on how the string sounds when spoken in English. The first character of the code is the first character of *character_expression*, converted to upper case. The second through fourth characters of the code are numbers that represent the letters in the expression. The letters A, E, I, O, U, H, W, and Y are ignored unless they are the first letter of the string. Zeroes are added at the end if necessary to produce a four-character code. For more information about the SOUNDEX code, see [The Soundex Indexing System](https://www.archives.gov/research/census/soundex.html).  
  
 SOUNDEX codes from different strings can be compared to see how similar the strings sound when spoken. The DIFFERENCE function performs a SOUNDEX on two strings, and returns an integer that represents how similar the SOUNDEX codes are for those strings.  
  
 SOUNDEX is collation sensitive. String functions can be nested.  
  
## SOUNDEX Compatibility  
 In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the SOUNDEX function applied a subset of the SOUNDEX rules. Under database compatibility level 110 or higher, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applies a more complete set of the rules.  
  
 After upgrading to compatibility level 110 or higher, you may need to rebuild the indexes, heaps, or CHECK constraints that use the SOUNDEX function.  
  
-   A heap that contains a persisted computed column defined with SOUNDEX cannot be queried until the heap is rebuilt by running the statement `ALTER TABLE <table> REBUILD`.  
  
-   CHECK constraints defined with SOUNDEX are disabled upon upgrade. To enable the constraint, run the statement `ALTER TABLE <table> WITH CHECK CHECK CONSTRAINT ALL`.  
  
-   Indexes (including indexed views) that contain a persisted computed column defined with SOUNDEX cannot be queried until the index is rebuilt by running the statement `ALTER INDEX ALL ON <object> REBUILD`.  
  
## Examples  
 The following example shows the SOUNDEX function and the related DIFFERENCE function. In the first example, the standard `SOUNDEX` values are returned for all consonants. Returning the `SOUNDEX` for `Smith` and `Smythe` returns the same SOUNDEX result because all vowels, the letter `y`, doubled letters, and the letter `h`, are not included.  
  
```sql
-- Using SOUNDEX  
SELECT SOUNDEX ('Smith'), SOUNDEX ('Smythe');  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)] Valid for a Latin1_General collation.  
  
```  
S530  S530    
```  
  
 The `DIFFERENCE` function compares the difference of the `SOUNDEX` pattern results. The following example shows two strings that differ only in vowels. The difference returned is `4`, the lowest possible difference.  
  
```sql
-- Using DIFFERENCE  
SELECT DIFFERENCE('Smithers', 'Smythers');  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)] Valid for a Latin1_General collation.  
  
```  
4             
```  
  
 In the following example, the strings differ in consonants; therefore, the difference returned is `2`, the greater difference.  
  
```sql
SELECT DIFFERENCE('Anothers', 'Brothers');  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)] Valid for a Latin1_General collation.  
  
```  
2             
```  
  
## See Also  
 [DIFFERENCE &#40;Transact-SQL&#41;](../../t-sql/functions/difference-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)  
  
  

