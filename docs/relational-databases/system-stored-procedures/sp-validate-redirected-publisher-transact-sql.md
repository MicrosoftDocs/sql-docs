---
title: "sp_validate_redirected_publisher (Transact-SQL)"
description: Verifies that the current host for the publishing database is capable of supporting replication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/14/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_validate_redirected_publisher"
  - "sp_validate_redirected_publisher_TSQL"
helpviewer_keywords:
  - "sp_validate_redirected_publisher"
dev_langs:
  - "TSQL"
---
# sp_validate_redirected_publisher (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Verifies that the current host for the publishing database is capable of supporting replication. Must be run from a distribution database. This procedure is called by `sp_get_redirected_publisher`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_validate_redirected_publisher
    [ @original_publisher = ] N'original_publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @redirected_publisher = ] N'redirected_publisher' OUTPUT
    , [ @multi_subnet_failover = ] multi_subnet_failover
[ ; ]
```

## Arguments

#### [ @original_publisher = ] N'*original_publisher*'

The name of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that originally published the database. *@original_publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the database being published. *@publisher_db* is **sysname**, with no default.

#### [ @redirected_publisher = ] N'*redirected_publisher*' OUTPUT

The target of redirection specified when `sp_redirect_publisher` was called for the publisher/database pair. *@redirected_publisher* is an OUTPUT parameter of type **sysname**.

#### [ @multi_subnet_failover = ] *multi_subnet_failover*

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 10 and later versions.

Used to pass information for the creation of the dynamic linked server. If `0`, the dynamic linked server isn't created with the `MultiSubnetFailover` parameter. If `1`, the dynamic linked server is created with the `MultiSubnetFailover` parameter as `1`. *@multi_subnet_failover* is **bit**, with a default of `0`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

If no entry exists for the publisher and the publishing database, `sp_validate_redirected_publisher` returns null in the output parameter *@redirected_publisher*. If an entry exists, it's returned in the output parameter in both success and failure cases.

If the validation succeeds, `sp_validate_redirected_publisher` returns a success indication.

If the validation fails, errors are raised describing the failure.

## Permissions

Caller must either be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role for the distribution database, or a member of a publication access list for a defined publication associated with the publisher database.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [sp_get_redirected_publisher (Transact-SQL)](sp-get-redirected-publisher-transact-sql.md)
- [sp_redirect_publisher (Transact-SQL)](sp-redirect-publisher-transact-sql.md)
- [sp_validate_replica_hosts_as_publishers (Transact-SQL)](sp-validate-replica-hosts-as-publishers-transact-sql.md)
