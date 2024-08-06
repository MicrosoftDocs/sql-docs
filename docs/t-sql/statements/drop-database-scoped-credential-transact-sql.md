---
title: "DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)"
description: DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 08/06/2024
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
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=aps-pdw-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Removes a database scoped credential from the server.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax
  
```syntaxsql
DROP DATABASE SCOPED CREDENTIAL credential_name  
```  
  
## Arguments

#### *credential_name*
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
  
## Related content

- [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [ALTER DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/alter-database-scoped-credential-transact-sql.md)
- [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-credential-transact-sql.md)
- [sys.credentials (Transact-SQL)](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)
