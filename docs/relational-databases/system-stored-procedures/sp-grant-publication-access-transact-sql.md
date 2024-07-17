---
title: "sp_grant_publication_access (Transact-SQL)"
description: sp_grant_publication_access adds a login to the access list of the publication.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_grant_publication_access_TSQL"
  - "sp_grant_publication_access"
helpviewer_keywords:
  - "sp_grant_publication_access"
dev_langs:
  - "TSQL"
---
# sp_grant_publication_access (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a login to the access list of the publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_grant_publication_access
    [ @publication = ] N'publication'
    , [ @login = ] N'login'
    [ , [ @reserved = ] N'reserved' ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to access. *@publication* is **sysname**, with no default.

#### [ @login = ] N'*login*'

The login ID. *@login* is **sysname**, with no default.

#### [ @reserved = ] N'*reserved*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @publisher = ] N'*publisher*'

The name of the publisher. *@publisher* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_grant_publication_access` is used in snapshot, transactional, and merge replication.

This stored procedure can be called repeatedly.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_grant_publication_access`.

## Related content

- [sp_help_publication_access (Transact-SQL)](sp-help-publication-access-transact-sql.md)
- [sp_revoke_publication_access (Transact-SQL)](sp-revoke-publication-access-transact-sql.md)
- [Secure the Publisher](../replication/security/secure-the-publisher.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
