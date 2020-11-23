---
description: "String Functions (Transact-SQL)"
title: "String Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/15/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "functions [SQL Server], strings"
  - "strings [SQL Server], functions"
  - "string functions"
  - "strings [SQL Server]"
ms.assetid: 6940a83d-5374-4af3-bb27-5d89c8af83ac
author: julieMSFT
ms.author: jrasnick
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# String Functions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The following scalar functions perform an operation on a string input value and return a string or numeric value:  

:::row:::
    :::column:::
        [ASCII](../../t-sql/functions/ascii-transact-sql.md)
    :::column-end:::
    :::column:::
        [CHAR](../../t-sql/functions/char-transact-sql.md)
    :::column-end:::
    :::column:::
        [CHARINDEX](../../t-sql/functions/charindex-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [CONCAT](../../t-sql/functions/concat-transact-sql.md)
    :::column-end:::
    :::column:::
        [CONCAT_WS](../../t-sql/functions/concat-ws-transact-sql.md)
    :::column-end:::
    :::column:::
        [DIFFERENCE](../../t-sql/functions/difference-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [FORMAT](../../t-sql/functions/format-transact-sql.md)
    :::column-end:::
    :::column:::
        [LEFT](../../t-sql/functions/left-transact-sql.md)
    :::column-end:::
    :::column:::
        [LEN](../../t-sql/functions/len-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [LOWER](../../t-sql/functions/lower-transact-sql.md)
    :::column-end:::
    :::column:::
        [LTRIM](../../t-sql/functions/ltrim-transact-sql.md)
    :::column-end:::
    :::column:::
        [NCHAR](../../t-sql/functions/nchar-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [PATINDEX](../../t-sql/functions/patindex-transact-sql.md)
    :::column-end:::
    :::column:::
        [QUOTENAME](../../t-sql/functions/quotename-transact-sql.md)
    :::column-end:::
    :::column:::
        [REPLACE](../../t-sql/functions/replace-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [REPLICATE](../../t-sql/functions/replicate-transact-sql.md)
    :::column-end:::
    :::column:::
        [REVERSE](../../t-sql/functions/reverse-transact-sql.md) 
    :::column-end:::
    :::column:::
        [RIGHT](../../t-sql/functions/right-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [RTRIM](../../t-sql/functions/rtrim-transact-sql.md)
    :::column-end:::
    :::column:::
        [SOUNDEX](../../t-sql/functions/soundex-transact-sql.md) 
    :::column-end:::
    :::column:::
        [SPACE](../../t-sql/functions/space-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [STR](../../t-sql/functions/str-transact-sql.md)
    :::column-end:::
    :::column:::
        [STRING_AGG](../../t-sql/functions/string-agg-transact-sql.md)
    :::column-end:::
    :::column:::
        [STRING_ESCAPE](../../t-sql/functions/string-escape-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [STRING_SPLIT](../../t-sql/functions/string-split-transact-sql.md)
    :::column-end:::
    :::column:::
        [STUFF](../../t-sql/functions/stuff-transact-sql.md)
    :::column-end:::
    :::column:::
        [SUBSTRING](../../t-sql/functions/substring-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [TRANSLATE](../../t-sql/functions/translate-transact-sql.md)
    :::column-end:::
    :::column:::
        [TRIM](../../t-sql/functions/trim-transact-sql.md)
    :::column-end:::
    :::column:::
        [UNICODE](../../t-sql/functions/unicode-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [UPPER](../../t-sql/functions/upper-transact-sql.md) 
    :::column-end:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

  
 All built-in string functions except `FORMAT` are deterministic. This means they return the same value any time they are called with a specific set of input values. For more information about function determinism, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
 When string functions are passed arguments that are not string values, the input type is implicitly converted to a text data type. For more information, see [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md).  
  
## See Also  
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)  
  
  

