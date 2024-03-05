---
title: "sp_audit_write (Transact-SQL)"
description: sp_audit_write adds a user-defined audit event to USER_DEFINED_AUDIT_GROUP.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_audit_write"
  - "sp_audit_write_TSQL"
helpviewer_keywords:
  - "sp_audit_write"
dev_langs:
  - "TSQL"
---
# sp_audit_write (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a user-defined audit event to `USER_DEFINED_AUDIT_GROUP`. If `USER_DEFINED_AUDIT_GROUP` isn't enabled, `sp_audit_write` is ignored.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_audit_write [ @user_defined_event_id = ] user_defined_event_id
    [ , [ @succeeded = ] succeeded ]
    [ , [ @user_defined_information = ] N'user_defined_information' ]
[ ; ]
```

## Arguments

#### [ @user_defined_event_id = ] *user_defined_event_id*

A parameter defined by the user and recorded in the `user_defined_event_id` column of the audit log. *@user_defined_event_id* is **smallint**.

#### [ @succeeded = ] *succeeded*

A parameter passed by user to indicate whether the event was successful or not. This value appears in the `succeeded` column of the audit log. *@succeeded* is **bit**.

#### [ @user_defined_information = ] N'*user_defined_information*'

The text defined by the user and recorded in the new `user_defined_event_id` column of the audit log. *@user_defined_information* is **nvarchar(4000)**.

## Return code values

`0` (success) or `1` (failure).

Failures are caused by incorrect input parameters, or failure to write to the target audit log.

## Remarks

When the `USER_DEFINED_AUDIT_GROUP` is added to either a server audit specification or a database audit specification, the event triggered by `sp_audit_write` is included in the audit log.

## Permissions

Requires membership in the **public** database role.

## Examples

### A. Create a user-defined audit event with informational text

The following example creates an audit event with a `@user_defined_event_id` value of `27`, the `@succeeded` value of `0`, and includes optional informational text.

```sql
EXEC sp_audit_write @user_defined_event_id = 27,
    @succeeded = 0,
    @user_defined_information = N'Access to a monitored object.';
```

### B. Create a user-defined audit event without informational text

The following example creates an audit event with a `@user_defined_event_id` value of `27`, the `@succeeded` value of `0`, and doesn't include optional informational text or the optional parameter names.

```sql
EXEC sp_audit_write 27, 0;
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sys.server_principals (Transact-SQL)](../system-catalog-views/sys-server-principals-transact-sql.md)
- [sp_addrole (Transact-SQL)](sp-addrole-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [sp_dropuser (Transact-SQL)](sp-dropuser-transact-sql.md)
- [sp_grantdbaccess (Transact-SQL)](sp-grantdbaccess-transact-sql.md)
- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
