---
title: "sp_validate_replica_hosts_as_publishers (T-SQL)"
description: Describes the sp_validate_replica_hosts_as_publishers stored procedure that allows all secondary replicas to be validated.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_validate_replica_hosts_as_publishers_TSQL"
  - "sp_validate_replica_hosts_as_publishers"
helpviewer_keywords:
  - "sp_validate_replica_hosts_as_publishers"
dev_langs:
  - "TSQL"
---
# sp_validate_replica_hosts_as_publishers (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

`sp_validate_replica_hosts_as_publishers` is an extension of `sp_validate_redirected_publisher` that allows all secondary replicas to be validated, rather than just the current primary replica. `sp_validate_replica_hosts_as_publisher` validates an entire Always On replication topology. `sp_validate_replica_hosts_as_publishers` must be executed directly on the distributor by using a remote desktop session to avoid a double-hop security error (21892).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_validate_replica_hosts_as_publishers
    [ @original_publisher = ] N'original_publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @redirected_publisher = ] N'redirected_publisher' OUTPUT
[ ; ]
```

## Arguments

#### [ @original_publisher = ] N'*original_publisher*'

The name of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that originally published the database. *@original_publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the database being published. *@publisher_db* is **sysname**, with no default.

#### [ @redirected_publisher = ] N'*redirected_publisher*' OUTPUT

The target of redirection when `sp_redirect_publisher` was called for the original publisher/published database pair. *@redirected_publisher* is an OUTPUT parameter of type **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

If no entry exists for the publisher and the publishing database, `sp_validate_redirected_publisher` returns null for the output parameter *@redirected_publisher*. Otherwise, the associated redirected publisher is returned, both on success and failure.

If the validation succeeds, `sp_validate_redirected_publisher` returns a success indication.

If the validation fails, appropriate errors are raised. `sp_validate_redirected_publisher` makes a best effort to raise all issues and not just the first encountered.

`sp_validate_replica_hosts_as_publishers` fails with the following error when validating secondary replica hosts that don't allow read access, or require read intent to be specified.

> Msg 21899, Level 11, State 1, Procedure `sp_hadr_verify_subscribers_at_publisher`, Line 109
>  
> The query at the redirected publisher 'MyReplicaHostName' to determine whether there were sysserver entries for the subscribers of the original publisher 'MyOriginalPublisher' failed with error '976', error message 'Error 976, Level 14, State 1, Message: The target database, 'MyPublishedDB', is participating in an availability group and is currently not accessible for queries. Either data movement is suspended or the availability replica isn't enabled for read access. To allow read-only access to this and other databases in the availability group, enable read access to one or more secondary availability replicas in the group. For more information, see the ALTER AVAILABILITY GROUP statement in SQL Server Books Online.

## Permissions

Caller must either be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role for the distribution database, or a member of a publication access list for a defined publication associated with the publisher database.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [sp_get_redirected_publisher (Transact-SQL)](sp-get-redirected-publisher-transact-sql.md)
- [sp_redirect_publisher (Transact-SQL)](sp-redirect-publisher-transact-sql.md)
- [sp_validate_redirected_publisher (Transact-SQL)](sp-validate-redirected-publisher-transact-sql.md)
