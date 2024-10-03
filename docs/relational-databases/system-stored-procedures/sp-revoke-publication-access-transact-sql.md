---
title: "sp_revoke_publication_access (Transact-SQL)"
description: sp_revoke_publication_access removes the login from a publications access list.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_revoke_publication_access_TSQL"
  - "sp_revoke_publication_access"
helpviewer_keywords:
  - "sp_revoke_publication_access"
dev_langs:
  - "TSQL"
---
# sp_revoke_publication_access (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the login from a publications access list. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_revoke_publication_access
    [ @publication = ] N'publication'
    , [ @login = ] N'login'
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to access. *@publication* is **sysname**, with no default.

#### [ @login = ] N'*login*'

The login ID. *@login* is **sysname**, with no default.

#### [ @publisher = ] N'*publisher*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_revoke_publication_access` is used in snapshot, transactional, and merge replication.

`sp_revoke_publication_access` can be called repeatedly.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_revoke_publication_access`.

## Related content

- [sp_grant_publication_access (Transact-SQL)](sp-grant-publication-access-transact-sql.md)
- [sp_help_publication_access (Transact-SQL)](sp-help-publication-access-transact-sql.md)
- [Secure the Publisher](../replication/security/secure-the-publisher.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
