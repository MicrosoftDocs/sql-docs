---
title: "sp_get_redirected_publisher (Transact-SQL)"
description: Used by replication agents to query a distributor to determine whether the original publisher was redirected.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_get_redirected_publisher_TSQL"
  - "sp_get_redirected_publisher"
dev_langs:
  - "TSQL"
---
# sp_get_redirected_publisher (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Used by replication agents to query a distributor to determine whether the original publisher was redirected.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_get_redirected_publisher
    [ @original_publisher = ] N'original_publisher'
    , [ @publisher_db = ] N'publisher_db'
    [ , [ @bypass_publisher_validation = ] bypass_publisher_validation ]
    [ , [ @multi_subnet_failover = ] multi_subnet_failover ]
[ ; ]
```

## Arguments

#### [ @original_publisher = ] N'*original_publisher*'

The name of the instance of SQL Server that originally published the database. *@original_publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the database being published. *@publisher_db* is **sysname**, with no default.

#### [ @bypass_publisher_validation = ] *bypass_publisher_validation*

Used to bypass validation of the redirected publisher. If `0`, validation is performed. If `1`, validation isn't performed. *@bypass_publisher_validation* is **bit**, with a default of `0`.

#### [ @multi_subnet_failover = ] *multi_subnet_failover*

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 10 and later versions

Used to pass information for the creation of the dynamic linked server. If `0`, the dynamic linked server isn't created with the `MultiSubnetFailover` parameter. If `1`, the dynamic linked server is created with the `MultiSubnetFailover` parameter as `1`. *@multi_subnet_failover* is **bit**, with a default of `0`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `redirected_publisher` | **sysname** | The name of the publisher after redirection. |
| `error_number` | **int** | The error number of the validation error. |
| `error_severity` | **int** | The severity of the validation error. |
| `error_message` | **nvarchar(4000)** | The text of the validation error message. |

## Remarks

`redirected_publisher` returns the current publisher name. Returns `NULL` if the publisher and publishing databases aren't redirected using `sp_redirect_publisher`.

If validation isn't requested or if no entry exists for the publisher and the publishing database, `error_number` and `error_severity` return `0` and `error_message` returns `NULL`.

If validation is requested, the validation stored procedure [sp_validate_redirected_publisher](sp-validate-redirected-publisher-transact-sql.md) is called to verify that the target of the redirection is a suitable host for the publishing database. If the validation succeeds, `sp_get_redirected_publisher` returns the redirected publisher name, `0` for the `error_number` and `error_severity` columns, and `NULL` in the `error_message` column.

If validation is requested and fails, the redirected publisher name is returned along with error information.

## Permissions

Caller must either be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role for the distribution database, or a member of a publication access list for a defined publication associated with the publisher database.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [sp_validate_redirected_publisher (Transact-SQL)](sp-validate-redirected-publisher-transact-sql.md)
- [sp_redirect_publisher (Transact-SQL)](sp-redirect-publisher-transact-sql.md)
- [sp_validate_replica_hosts_as_publishers (Transact-SQL)](sp-validate-replica-hosts-as-publishers-transact-sql.md)
