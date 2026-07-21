---
title: "DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)"
description: DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 05/14/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "DROP DATABASE SCOPED CREDENTIAL"
  - "DROP_DATABASE_SCOPED_CREDENTIAL_TSQL"
helpviewer_keywords:
  - "DROP DATABASE SCOPED CREDENTIAL statement"
  - "credential [SQL Server], DROP DATABASE SCOPED CREDENTIAL statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=aps-pdw-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

  Removes a database scoped credential from the server.  

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
DROP DATABASE SCOPED CREDENTIAL credential_name  
```  

## Arguments

#### *credential_name*

The name of the database scoped credential to remove from the server.

## Remarks

 To drop the secret associated with a database scoped credential without dropping the database scoped credential itself, use [ALTER CREDENTIAL](alter-credential-transact-sql.md).

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
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](create-database-scoped-credential-transact-sql.md)
- [ALTER DATABASE SCOPED CREDENTIAL (Transact-SQL)](alter-database-scoped-credential-transact-sql.md)
- [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](create-credential-transact-sql.md)
- [sys.credentials (Transact-SQL)](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)
