---
title: "SESSIONPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SESSIONPROPERTY"
  - "SESSIONPROPERTY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SET statement, SESSIONPROPERTY function"
  - "SESSIONPROPERTY function"
  - "sessions [SQL Server], SET options settings"
ms.assetid: 1f3730b4-1495-4d3a-af43-e57952812df9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# SESSIONPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the SET options settings of a session.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SESSIONPROPERTY (option)  
```  
  
## Arguments  
 *option*  
 Is the current option setting for this session. *option* can be any of the following values.  
  
|Option|Description|  
|------------|-----------------|  
|ANSI_NULLS|Specifies whether the ISO behavior of equals (=) and not equal to (<>) against null values is applied.<br /><br /> 1 = ON<br /><br /> 0 = OFF|  
|ANSI_PADDING|Controls the way the column stores values shorter than the defined size of the column, and the way the column stores values that have trailing blanks in character and binary data.<br /><br /> 1 = ON<br /><br /> 0 = OFF|  
|ANSI_WARNINGS|Specifies whether the ISO standard behavior of raising error messages or warnings for certain conditions, including divide-by-zero and arithmetic overflow, is applied.<br /><br /> 1 = ON<br /><br /> 0 = OFF|  
|ARITHABORT|Determines whether a query is ended when an overflow or a divide-by-zero error occurs during query execution.<br /><br /> 1 = ON<br /><br /> 0 = OFF|  
|CONCAT_NULL_YIELDS_ NULL|Controls whether concatenation results are treated as null or empty string values.<br /><br /> 1 = ON<br /><br /> 0 = OFF|  
|NUMERIC_ROUNDABORT|Specifies whether error messages and warnings are generated when rounding in an expression causes a loss of precision.<br /><br /> 1 = ON<br /><br /> 0 = OFF|  
|QUOTED_IDENTIFIER|Specifies whether ISO rules about how to use quotation marks to delimit identifiers and literal strings are to be followed.<br /><br /> 1 = ON<br /><br /> 0 = OFF|  
|\<Any other string>|NULL = Input is not valid.|  
  
## Return Types  
 **sql_variant**  
  
## Remarks  
 SET options are figured by combining server-level, database-level, and user-specified options.  
  
## Examples  
 The following example returns the setting for the `CONCAT_NULL_YIELDS_NULL` option.  
  
```  
SELECT   SESSIONPROPERTY ('CONCAT_NULL_YIELDS_NULL')  
```  
  
## See Also  
 [sql_variant &#40;Transact-SQL&#41;](../../t-sql/data-types/sql-variant-transact-sql.md)   
 [SET ANSI_NULLS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-nulls-transact-sql.md)   
 [SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md)   
 [SET ANSI_WARNINGS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-warnings-transact-sql.md)   
 [SET ARITHABORT &#40;Transact-SQL&#41;](../../t-sql/statements/set-arithabort-transact-sql.md)   
 [SET CONCAT_NULL_YIELDS_NULL &#40;Transact-SQL&#41;](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md)   
 [SET NUMERIC_ROUNDABORT &#40;Transact-SQL&#41;](../../t-sql/statements/set-numeric-roundabort-transact-sql.md)   
 [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md)  
  
  
