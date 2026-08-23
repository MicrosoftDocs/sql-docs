---
title: "Backslash (Line Continuation) (Transact-SQL)"
description: "Backslash (Line Continuation) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "07/25/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2024
  - ignite-2025
f1_keywords:
  - "\\_TSQL"
  - "\\"
helpviewer_keywords:
  - "backwhack"
  - "backslash"
  - "escape character"
  - "hack character"
  - "\\ (backslash)"
  - "backslant"
  - "bash"
  - "reverse slant"
  - "slosh"
  - "reversed virgule"
  - "line continuation character"
  - "reverse solidus"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Backslash (Line Continuation) (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

`\`  breaks a long string constant, character or binary, into two or more lines for readability.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
<first section of string> \  
<continued section of string>  
```  
  
## Arguments
 \<first section of string>  
 Is the start of a string.  
  
 \<continued section of string>  
 Is the continuation of a string.  
  
## Remarks  
This command returns the first and continued sections of the string as one string, without the backslash. The new line after the backslash must either be a line feed character (U+000A) or a combination of carriage return (U+000D) and line feed (U+000A) in that order. 

## Examples  

### A. Splitting a character string  

The following example uses a backslash and a carriage return to split a character string into two lines.  
  
```sql  
SELECT 'abc\  
def' AS [ColumnResult];  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 ColumnResult  
 ------------  
 abcdef
 ```    

### B. Splitting a binary string  

The following example uses a backslash and a carriage return to split a binary string into two lines.  

```sql  
SELECT 0xabc\
def AS [ColumnResult];  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 ColumnResult  
 ------------  
 0xABCDEF
 ```    

## Related content

- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [What are the SQL database functions?](../functions/functions.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [/ (Division) (Transact-SQL)](divide-transact-sql.md)
- [/= (Division assignment) (Transact-SQL)](divide-equals-transact-sql.md)
- [Compound operators (Transact-SQL)](compound-operators-transact-sql.md)
