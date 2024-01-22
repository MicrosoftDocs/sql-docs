---
title: "sp_redirect_publisher (Transact-SQL)"
description: "sp_redirect_publisher specifies a redirected publisher for an existing publisher and/or database pair."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_redirect_publisher_TSQL"
  - "sp_redirect_publisher"
helpviewer_keywords:
  - "sp_redirect_publisher"
dev_langs:
  - "TSQL"
---
# sp_redirect_publisher (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies a redirected publisher for an existing publisher/database pair. If the publisher database belongs to an Always On availability group (AG), the redirected publisher is the AG listener name associated with the AG.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_redirect_publisher
    [ @original_publisher = ] N'original_publisher'
    , [ @publisher_db = ] N'publisher_db'
    [ , [ @redirected_publisher = ] N'redirected_publisher' ]
[ ; ]
```

## Arguments

#### [ @original_publisher = ] N'*original_publisher*'

The name of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that originally published the database. *@original_publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the database being published. *@publisher_db* is **sysname**, with no default.

#### [ @redirected_publisher = ] N'*redirected_publisher*'

The AG listener name associated with the AG that will be the new publisher. *@redirected_publisher* is **sysname**, with a default of `NULL`. When the AG listener is configured to use a non-default port, specify the port number along with listener name, such as `ListenerName,51433`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_redirect_publisher` is used to allow a replication publisher to be redirected to the current primary of an AG by associating the publisher/database pair with an AG's listener. Execute `sp_redirect_publisher` after the AG listener is configured for the AG that contains the published database.

If the publication database at the original publisher is removed from an AG at the primary replica, execute `sp_redirect_publisher` without specifying a value for the *@redirected_publisher* parameter to remove the redirection for the publisher/database pair. For more information about redirecting the publisher, see [Manage a replicated Publisher database as part of an Always On availability group](../../database-engine/availability-groups/windows/maintaining-an-always-on-publication-database-sql-server.md).

## Permissions

Caller must either be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role for the distribution database, or a member of a publication access list for a defined publication associated with the publisher database.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [sp_validate_redirected_publisher (Transact-SQL)](sp-validate-redirected-publisher-transact-sql.md)
- [sp_get_redirected_publisher (Transact-SQL)](sp-get-redirected-publisher-transact-sql.md)
- [sp_validate_replica_hosts_as_publishers (Transact-SQL)](sp-validate-replica-hosts-as-publishers-transact-sql.md)
