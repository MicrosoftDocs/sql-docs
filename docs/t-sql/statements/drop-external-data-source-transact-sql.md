---
title: "DROP EXTERNAL DATA SOURCE (Transact-SQL)"
description: DROP EXTERNAL DATA SOURCE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP EXTERNAL DATA SOURCE (Transact-SQL)
[!INCLUDE [sqlserver2016-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdbmi-asa-pdw.md)]

  Removes a PolyBase external data source.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Drop an external data source  
DROP EXTERNAL DATA SOURCE external_data_source_name  
[;]  
```  
  
## Arguments  
 *external_data_source_name*  
 The name of the external data source to drop.  
  
## Metadata  
 To view a list of external data sources use the sys.external_data_sources system view.  
  
```sql  
SELECT * FROM sys.external_data_sources;  
```  
  
## Permissions  
 Requires ALTER ANY EXTERNAL DATA SOURCE.  
  
## Locking  
 Takes a shared lock on the external data source object.  
  
## General Remarks  
 Dropping an external data source does not remove the external data.  
  
## Examples  
  
### A. Using basic syntax  
  
```sql  
DROP EXTERNAL DATA SOURCE mydatasource;  
```  
  
## See Also  
 [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)  
  
  

