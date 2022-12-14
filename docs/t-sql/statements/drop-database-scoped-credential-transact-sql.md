---
title: "DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)"
description: DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "02/27/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP DATABASE SCOPED CREDENTIAL"
  - "DROP_DATABASE_SCOPED_CREDENTIAL_TSQL"
helpviewer_keywords:
  - "DROP DATABASE SCOPED CREDENTIAL statement"
  - "credential [SQL Server], DROP DATABASE SCOPED CREDENTIAL statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest"
---
# DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)
[!INCLUDE [asdb-asdbmi-asa](../../includes/applies-to-version/asdb-asdbmi-asa.md)]

  Removes a database scoped credential from the server.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
DROP DATABASE SCOPED CREDENTIAL credential_name  
```  
  
## Arguments  
 *credential_name*  
 Is the name of the database scoped credential to remove from the server.  
  
## Remarks  
 To drop the secret associated with a database scoped credential without dropping the database scoped credential itself, use [ALTER CREDENTIAL](../../t-sql/statements/alter-credential-transact-sql.md).  
  
 Information about database scoped credentials is visible in the [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md) catalog view.  
  
## Permissions  
 Requires `ALTER` permission on the credential.  
  
## Examples  
 The following example removes the database scoped credential called `SalesAccess`.  
  
```sql  
DROP DATABASE SCOPED CREDENTIAL SalesAccess;  
GO  
```  
  
## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)   
 [ALTER DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-credential-transact-sql.md)   
 [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)  
  
  
