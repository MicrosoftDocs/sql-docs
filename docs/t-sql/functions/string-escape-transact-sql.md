---
title: "STRING_ESCAPE (Transact-SQL)"
description: "The STRING_ESCAPE Transact-SQL function escapes special characters in texts and returns text with escaped characters."
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/09/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "STRING_ESCAPE"
  - "STRING_ESCAPE_TSQL"
helpviewer_keywords:
  - "STRING_ESCAPE function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb"
---
# STRING_ESCAPE (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

Escapes special characters in texts and returns text with escaped characters. `STRING_ESCAPE` is a deterministic function first introduced in SQL Server 2016. 

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
STRING_ESCAPE( text , type )  
```  

## Arguments

#### *text*

A **nvarchar** [expression](../language-elements/expressions-transact-sql.md) representing the object that should be escaped.

#### *type*

 Escaping rules that will be applied. Currently the value supported is `'json'`.  

## Return types

 **nvarchar(max)** text with escaped special and control characters. 

 Currently `STRING_ESCAPE` can only escape JSON special characters shown in the following tables.  

|Special character|Encoded sequence|  
|-----------------------|----------------------|  
| `Quotation mark (")` |\\"|  
| `Reverse solidus (\\)` | \\\\ |  
| `Solidus (/)` |\\/|  
| `Backspace` |\b|  
| `Form feed` |\f|  
| `New line` |\n|  
| `Carriage return` |\r|  
| `Horizontal tab` |\t|  

|Control character|Encoded sequence|  
|-----------------------|----------------------|  
| `CHAR(0)` |\u0000|  
| `CHAR(1)` |\u0001|  
| `...` |...|  
| `CHAR(31)` |\u001f|  

## Remarks

## Examples

### A. Escape text according to the JSON formatting rules

 The following query escapes special characters using JSON rules and returns escaped text.  

```sql
SELECT STRING_ESCAPE('\   /  
\\    "     ', 'json') AS escapedText;  
```  

 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

```output
escapedText  
-------------------------------------------------------------  
\\\t\/\n\\\\\t\"\t
```  

### B. Format JSON object

 The following query creates JSON text from number and string variables, and escapes any special JSON character in variables.  

```sql
SET @json = FORMATMESSAGE('{ "id": %d,"name": "%s", "surname": "%s" }',
    17, STRING_ESCAPE(@name,'json'), STRING_ESCAPE(@surname,'json') );  
```  

## Related content

- [CONCAT (Transact-SQL)](concat-transact-sql.md)
- [CONCAT_WS (Transact-SQL)](concat-ws-transact-sql.md)
- [FORMATMESSAGE (Transact-SQL)](formatmessage-transact-sql.md)
- [QUOTENAME (Transact-SQL)](quotename-transact-sql.md)
- [REPLACE (Transact-SQL)](replace-transact-sql.md)
- [REVERSE (Transact-SQL)](reverse-transact-sql.md)
- [STRING_AGG (Transact-SQL)](string-agg-transact-sql.md)
- [STUFF (Transact-SQL)](stuff-transact-sql.md)
- [TRANSLATE (Transact-SQL)](translate-transact-sql.md)
