---
title: "@@LANGID (Transact-SQL)"
description: "@@LANGID (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "@@LANGID"
  - "@@LANGID_TSQL"
helpviewer_keywords:
  - "languages [SQL Server], current in use"
  - "@@LANGID function"
  - "current language in use"
  - "ID for language in use"
  - "local language IDs [SQL Server]"
dev_langs:
  - "TSQL"
---
# &#x40;&#x40;LANGID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the local language identifier (ID) of the language that is currently being used.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
@@LANGID  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 **smallint**  
  
## Remarks  
 To view information about language settings, including language ID numbers, run **sp_helplanguage** without a parameter specified.  
  
## Examples  
 The following example sets the language for the current session to `Italian`, and then uses `@@LANGID` to return the ID for Italian.  
  
```sql  
SET LANGUAGE 'Italian'  
SELECT @@LANGID AS 'Language ID'  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Changed language setting to Italiano.  
Language ID  
-----------  
6            
```  
  
## See Also  
 [Configuration Functions &#40;Transact-SQL&#41;](../../t-sql/functions/configuration-functions-transact-sql.md)   
 [SET LANGUAGE &#40;Transact-SQL&#41;](../../t-sql/statements/set-language-transact-sql.md)   
 [sp_helplanguage &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helplanguage-transact-sql.md)  
  
  
