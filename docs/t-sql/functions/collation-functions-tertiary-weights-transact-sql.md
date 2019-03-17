---
title: "TERTIARY_WEIGHTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "TERTIARY_WEIGHTS_TSQL"
  - "TERTIARY_WEIGHTS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "weights [SQL Server]"
  - "SQL tertiary collations"
  - "TERTIARY_WEIGHTS function"
ms.assetid: 7e1f5350-260b-4c61-8c84-69bb1a214f1f
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Collation Functions - TERTIARY_WEIGHTS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

For each character in a non-Unicode string expression - defined with a SQL tertiary collation - this function returns a binary string of weights.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
TERTIARY_WEIGHTS( non_Unicode_character_string_expression )  
```  
  
## Arguments  
*non_Unicode_character_string_expression*  
A string [expression](../../t-sql/language-elements/expressions-transact-sql.md) of type **char**, **varchar**, or **varchar(max)** defined on a tertiary SQL collation. For a list of these collations, see Remarks.
  
## Return types
`TERTIARY_WEIGHTS` returns **varbinary** when *non_Unicode_character_string_expression* is **char** or **varchar**, and it returns **varbinary(max)** when the *non_Unicode_character_string_expression* has a **varchar(max)** data type.
  
## Remarks  
`TERTIARY_WEIGHTS` returns NULL when a SQL tertiary collection does not define the *non_Unicode_character_string_expression*. This table shows the SQL tertiary collations:
  
|Sort order ID|SQL collation|  
|---|---|
|33|SQL_Latin1_General_Pref_CP437_CI_AS|  
|34|SQL_Latin1_General_CP437_CI_AI|  
|43|SQL_Latin1_General_Pref_CP850_CI_AS|  
|44|SQL_Latin1_General_CP850_CI_AI|  
|49|SQL_1xCompat_CP850_CI_AS|  
|53|SQL_Latin1_General_Pref_CP1_CI_AS|  
|54|SQL_Latin1_General_CP1_CI_AI|  
|56|SQL_AltDiction_Pref_CP850_CI_AS|  
|57|SQL_AltDiction_CP850_CI_AI|  
|58|SQL_Scandinavian_Pref_CP850_CI_AS|  
|82|SQL_Latin1_General_CP1250_CI_AS|  
|84|SQL_Czech_CP1250_CI_AS|  
|86|SQL_Hungarian_CP1250_CI_AS|  
|88|SQL_Polish_CP1250_CI_AS|  
|90|SQL_Romanian_CP1250_CI_AS|  
|92|SQL_Croatian_CP1250_CI_AS|  
|94|SQL_Slovak_CP1250_CI_AS|  
|96|SQL_Slovenian_CP1250_CI_AS|  
|106|SQL_Latin1_General_CP1251_CI_AS|  
|108|SQL_Ukrainian_CP1251_CI_AS|  
|113|SQL_Latin1_General_CP1253_CS_AS|  
|114|SQL_Latin1_General_CP1253_CI_AS|  
|130|SQL_Latin1_General_CP1254_CI_AS|  
|146|SQL_Latin1_General_CP1256_CI_AS|  
|154|SQL_Latin1_General_CP1257_CI_AS|  
|156|SQL_Estonian_CP1257_CI_AS|  
|158|SQL_Latvian_CP1257_CI_AS|  
|160|SQL_Lithuanian_CP1257_CI_AS|  
|183|SQL_Danish_Pref_CP1_CI_AS|  
|184|SQL_SwedishPhone_Pref_CP1_CI_AS|  
|185|SQL_SwedishStd_Pref_CP1_CI_AS|  
|186|SQL_Icelandic_Pref_CP1_CI_AS|  
  
Use `TERTIARY_WEIGHTS` for the definition of a computed column defined on the values of a **char**, **varchar**, or **varchar(max)** column. Index definition on both the computed column and the **char**, **varchar**, or **varchar(max)** column can improve performance when the ORDER BY clause of a query specifies that **char**, **varchar**, or **varchar(max)** column.
  
## Examples  
This example creates a computed column in a table that applies the `TERTIARY_WEIGHTS` function to the values of a `char` column:
  
```sql
CREATE TABLE TertColTable  
(Col1 char(15) COLLATE SQL_Latin1_General_Pref_CP437_CI_AS,  
Col2 AS TERTIARY_WEIGHTS(Col1));  
GO   
```  
  
## See also
[ORDER BY Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-order-by-clause-transact-sql.md)
  
  
