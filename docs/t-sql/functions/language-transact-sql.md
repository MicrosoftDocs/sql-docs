---
title: "@@LANGUAGE (Transact-SQL)"
description: "@@LANGUAGE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "09/18/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "@@LANGUAGE_TSQL"
  - "@@LANGUAGE"
helpviewer_keywords:
  - "languages [SQL Server], current in use"
  - "@@LANGUAGE function"
  - "current language in use"
  - "names [SQL Server], language in use"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# &#x40;&#x40;LANGUAGE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

  Returns the name of the language currently being used.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
@@LANGUAGE  
```  
  
## Return Types
 **nvarchar**  
  
## Remarks  
 To view information about language settings, including valid official language names, run **sp_helplanguage** without a parameter specified.  
  
## Examples  
 The following example returns the language for the current session.  
  
```sql  
SELECT @@LANGUAGE AS 'Language Name';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Language Name                   
------------------------------  
us_english                      
```  
  
## Related content

- [SET LANGUAGE (Transact-SQL)](../statements/set-language-transact-sql.md)
- [sys.sp_helplanguage (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-helplanguage-transact-sql.md)
