---
title: "DROP CREDENTIAL (Transact-SQL)"
description: DROP CREDENTIAL removes a credential from the server.
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 10/20/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP CREDENTIAL"
  - "DROP_CREDENTIAL_TSQL"
helpviewer_keywords:
  - "removing credentials"
  - "DROP CREDENTIAL statement"
  - "credentials [SQL Server], DROP CREDENTIAL statement"
  - "authentication [SQL Server], credentials"
  - "deleting credentials"
  - "dropping credentials"
dev_langs:
  - TSQL
---
# DROP CREDENTIAL (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Removes a credential from the server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DROP CREDENTIAL credential_name
```

## Arguments

#### *credential_name*

The name of the credential to remove from the server.

## Remarks

To drop the secret associated with a credential without dropping the credential itself, use [ALTER CREDENTIAL](alter-credential-transact-sql.md).

Information about credentials is visible in the `sys.credentials` catalog view.

Proxies are associated with a credential. Deleting a credential that is used by a proxy leaves the associated proxy in an unusable state. When dropping a credential used by a proxy, delete the proxy (by using [sp_delete_proxy](../../relational-databases/system-stored-procedures/sp-delete-proxy-transact-sql.md)) and recreate the associated proxy (by using [sp_add_proxy](../../relational-databases/system-stored-procedures/sp-add-proxy-transact-sql.md)).

## Permissions

Requires `ALTER ANY CREDENTIAL` permission. If dropping a system credential, requires `CONTROL SERVER` permission.

## Examples

The following example removes the credential called `Saddles`.

```sql
DROP CREDENTIAL Saddles;
GO
```

## Related content

- [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [CREATE CREDENTIAL (Transact-SQL)](create-credential-transact-sql.md)
- [ALTER CREDENTIAL (Transact-SQL)](alter-credential-transact-sql.md)
- [DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)](drop-database-scoped-credential-transact-sql.md)
- [sys.credentials (Transact-SQL)](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)
