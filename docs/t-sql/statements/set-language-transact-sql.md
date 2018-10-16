---
title: "SET LANGUAGE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/05/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SET_LANGUAGE_TSQL"
  - "SET LANGUAGE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "LANGUAGE option"
  - "languages [SQL Server], setting language"
  - "SET LANGUAGE statement"
  - "options [SQL Server], date"
  - "default languages"
ms.assetid: 0ec0e5cf-e115-4be9-a0db-e65837d6fa45
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET LANGUAGE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-asdw-xxx-md.md)]

  Specifies the language environment for the session. The session language determines the **datetime** formats and system messages.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SET LANGUAGE { [ N ] 'language' | @language_var }   
```  
  
## Arguments  
 [**N**]**'**_language_**'** | **@**_language\_var_  
 Is the name of the language as stored in [sys.syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md). This argument can be either Unicode or DBCS converted to Unicode. To specify a language in Unicode, use **N'**_language_**'**. If specified as a variable, the variable must be **sysname**.  
  
## Remarks  
 The setting of SET LANGUAGE is set at execute or run time and not at parse time.  
  
 SET LANGUAGE implicitly sets the setting of [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md).  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example sets the default language to `Italian`, displays the month name, and then switches back to `us_english` and displays the month name again.  
  
```  
DECLARE @Today DATETIME;  
SET @Today = '12/5/2007';  
  
SET LANGUAGE Italian;  
SELECT DATENAME(month, @Today) AS 'Month Name';  
  
SET LANGUAGE us_english;  
SELECT DATENAME(month, @Today) AS 'Month Name' ;  
GO  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md)   
 [sp_helplanguage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helplanguage-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  
