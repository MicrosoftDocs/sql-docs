---
title: "IS_OBJECTSIGNED (Transact-SQL)"
description: "IS_OBJECTSIGNED (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "IS_OBJECTSIGNED"
  - "IS_OBJECTSIGNED_TSQL"
helpviewer_keywords:
  - "IS_OBJECTSIGNED function"
dev_langs:
  - "TSQL"
---
# IS_OBJECTSIGNED (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Indicates whether an object is signed by a specified certificate or asymmetric key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
IS_OBJECTSIGNED (   
'OBJECT', @object_id, @class, @thumbprint  
  )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 **'OBJECT'**  
 The type of securable class.  
  
 *\@object_id*  
 The object_id of the object being tested. *\@object_id* is type **int**.  
  
 *\@class*  
 The class of the object:  
  
-   'certificate'  
  
-   'asymmetric key'  
  
 *\@class* is **sysname**.  
  
 *\@thumbprint*  
 The SHA thumbprint of the object. *\@thumbprint* is type **varbinary(32)**.  
  
## Returned Types  
 **int**  
  
## Remarks  
 IS_OBJECTSIGNED returns the following values.  
  
|Return value|Description|  
|------------------|-----------------|  
|NULL|The object is not signed, or the object is not valid.|  
|0|The object is signed, but the signature is not valid.|  
|1|The object is signed.|  
  
## Permissions  
 Requires VIEW DEFINITION on the certificate or asymmetric key.  
  
## Examples  
  
### A. Displaying extended properties on a database  
 The following example tests if the spt_fallback_db table in the **master** database is signed by the schema signing certificate.  
  
```sql  
USE master;  
-- Declare a variable to hold a thumbprint and an object name  
DECLARE @thumbprint varbinary(20), @objectname sysname;  
  
-- Populate the thumbprint variable with the thumbprint of   
-- the master database schema signing certificate  
SELECT @thumbprint = thumbprint   
FROM sys.certificates   
WHERE name LIKE '%SchemaSigningCertificate%';  
  
-- Populate the object name variable with a table name in master  
SELECT @objectname = 'spt_fallback_db';  
  
-- Query to see if the table is signed by the thumbprint  
SELECT @objectname AS [object name],  
IS_OBJECTSIGNED(  
'OBJECT', OBJECT_ID(@objectname), 'certificate', @thumbprint  
) AS [Is the object signed?] ;  
```  
  
## See Also  
 [sys.fn_check_object_signatures &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-check-object-signatures-transact-sql.md)  
  
  
