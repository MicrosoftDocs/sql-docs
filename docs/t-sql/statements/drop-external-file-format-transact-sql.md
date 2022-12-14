---
title: "DROP EXTERNAL FILE FORMAT (Transact-SQL)"
description: DROP EXTERNAL FILE FORMAT (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP EXTERNAL FILE FORMAT (Transact-SQL)
[!INCLUDE [sqlserver2016-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdbmi-asa-pdw.md)]

  Removes a PolyBase external file format.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Drop an external file format  
DROP EXTERNAL FILE FORMAT external_file_format_name  
[;]  
```  
  
## Arguments  
 *external_file_format_name*  
 The name of the external file format to drop.  
  
## Metadata  
 To view a list of external file formats use the [sys.external_file_formats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-external-file-formats-transact-sql.md) system view.  
  
```sql  
SELECT * FROM sys.external_file_formats;  
```  
  
## Permissions  
 Requires ALTER ANY EXTERNAL FILE FORMAT.  
  
## General Remarks  
 Dropping an external file format does not remove the external data.  
  
## Locking  
 Takes a shared lock on the external file format object.  
  
## Examples  
  
### A. Using basic syntax  
  
```sql  
DROP EXTERNAL FILE FORMAT myfileformat;  
```  
  
## See Also  
 [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)  
  
  

