---
title: "ALTER SERVER AUDIT (Transact-SQL)"
description: ALTER SERVER AUDIT alters a server audit object using the SQL Server Audit feature.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: randolphwest
ms.date: 11/25/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_SERVER_AUDIT_TSQL"
  - "ALTER SERVER AUDIT"
helpviewer_keywords:
  - "server audit [SQL Server]"
  - "audits [SQL Server], specification"
  - "ALTER SERVER AUDIT statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017"
---
# ALTER SERVER AUDIT (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Alters a server audit object using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Audit feature. For more information, see [SQL Server Audit (Database Engine)](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ALTER SERVER AUDIT audit_name
{
    [ TO { { FILE ( <file_options> [ , ...n ] ) } | APPLICATION_LOG | SECURITY_LOG } | URL ]
    [ WITH ( <audit_options> [ , ...n ] ) ]
    [ WHERE <predicate_expression> ]
}
| REMOVE WHERE
| MODIFY NAME = new_audit_name
[ ; ]

<file_options>::=
{
      FILEPATH = 'os_file_path'
    | MAXSIZE = { max_size { MB | GB | TB } | UNLIMITED }
    | MAX_ROLLOVER_FILES = { integer | UNLIMITED }
    | MAX_FILES = integer
    | RESERVE_DISK_SPACE = { ON | OFF }
}

<audit_options>::=
{
      QUEUE_DELAY = integer
    | ON_FAILURE = { CONTINUE | SHUTDOWN | FAIL_OPERATION }
    | STATE = = { ON | OFF }
}

<predicate_expression>::=
{
    [ NOT ] <predicate_factor>
    [ { AND | OR } [ NOT ] { <predicate_factor> } ]
    [ , ...n ]
}

<predicate_factor>::=
    event_field_name { = | < > | != | > | >= | < | <= } { number | 'string' }
```

## Arguments

#### TO { FILE | APPLICATION_LOG | SECURITY | URL }

Determines the location of the audit target. The options are a binary file, the Windows application log, or the Windows security log.

> [!IMPORTANT]  
> In Azure SQL Managed Instance, SQL Audit works at the server level and stores `.xel` files in Azure Blob Storage.

#### FILEPATH = '*os_file_path*'

The path of the audit trail. The file name is generated based on the audit name and audit GUID.

#### MAXSIZE = *max_size*

Specifies the maximum size to which the audit file can grow. The *max_size* value must be an integer followed by `MB`, `GB`, `TB`, or `UNLIMITED`. The minimum size that you can specify for *max_size* is 2 `MB` and the maximum is 2,147,483,647 `TB`. When `UNLIMITED` is specified, the file grows until the disk is full. Specifying a value lower than 2 MB raises the `MSG_MAXSIZE_TOO_SMALL` error. The default value is `UNLIMITED`.

#### MAX_ROLLOVER_FILES = *integer* | UNLIMITED

Specifies the maximum number of files to retain in the file system. When the setting of `MAX_ROLLOVER_FILES = 0`, no limit is imposed on the number of rollover files created. The default value is `0`. The maximum number of files that can be specified is 2,147,483,647.

#### MAX_FILES = *integer*

Specifies the maximum number of audit files that can be created. Doesn't roll over to the first file when the limit is reached. When the `MAX_FILES` limit is reached, any action that causes more audit events to be generated fails with an error.

#### RESERVE_DISK_SPACE = { ON | OFF }

This option preallocates the file on the disk to the `MAXSIZE` value. Only applies if `MAXSIZE` isn't equal to `UNLIMITED`. The default value is `OFF`.

#### QUEUE_DELAY = *integer*

Determines the time in milliseconds that can elapse before audit actions are forced to be processed. A value of 0 indicates synchronous delivery. The minimum settable query delay value is `1000` (1 second), which is the default. The maximum is 2,147,483,647 (2,147,483.647 seconds or 24 days, 20 hours, 31 minutes, 23.647 seconds). If you specify an invalid number, you receive the error `MSG_INVALID_QUEUE_DELAY`.

#### ON_FAILURE = { CONTINUE | SHUTDOWN | FAIL_OPERATION }

Indicates whether the instance writing to the target should fail, continue, or stop if [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't write to the audit log.

#### CONTINUE

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] operations continue. Audit records aren't retained. The audit continues to attempt to log events and resumes if the failure condition is resolved. Selecting the continue option can allow unaudited activity, which could violate your security policies. Use this option, when continuing operation of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] is more important than maintaining a complete audit.

#### SHUTDOWN

Forces the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to shut down, if [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] fails to write data to the audit target for any reason. The login executing the `ALTER` statement must have the `SHUTDOWN` permission within [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The shutdown behavior persists even if the `SHUTDOWN` permission is later revoked from the executing login. If the user doesn't have this permission, then the statement fails and the audit isn't modified. Use the option when an audit failure could compromise the security or integrity of the system. For more information, see [SHUTDOWN](../language-elements/shutdown-transact-sql.md).

#### FAIL_OPERATION

Database actions fail if they cause audited events. Actions, which don't cause audited events can continue, but no audited events can occur. The audit continues to attempt to log events and resumes if the failure condition is resolved. Use this option when maintaining a complete audit is more important than full access to the [!INCLUDE [ssde-md](../../includes/ssde-md.md)].

#### STATE = { ON | OFF }

Enables or disables the audit from collecting records. Changing the state of a running audit (from `ON` to `OFF`) creates an audit entry that the audit was stopped, the principal that stopped the audit, and the time the audit was stopped.

#### MODIFY NAME = *new_audit_name*

Changes the name of the audit. Can't be used with any other option.

#### predicate_expression

Specifies the predicate expression used to determine if an event should be processed or not. Predicate expressions are limited to a length of 3,000 characters, which limits string arguments.

#### event_field_name

The name of the event field that identifies the predicate source. Audit fields are described in [sys.fn_get_audit_file](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md). All fields can be audited except `file_name` and `audit_file_offset`.

#### number

Any numeric type including **decimal**. Limitations are the lack of available physical memory or a number that is too large to be represented as a 64-bit integer.

#### '*string*'

Either an ANSI or Unicode string as required by the predicate compare. No implicit string type conversion is performed for the predicate compare functions. Passing the wrong type results in an error

## Remarks

You must specify at least one of the `TO`, `WITH`, or `MODIFY NAME` clauses when you call `ALTER AUDIT`.

You must set the state of an audit to the OFF option in order to make changes to an audit. If `ALTER AUDIT` is run when an audit is enabled with any options other than `STATE = OFF`, you receive a `MSG_NEED_AUDIT_DISABLED` error message.

You can add, alter, and remove audit specifications without stopping an audit.

You can't change an audit's GUID after the audit has been created.

`ALTER SERVER AUDIT` statement can't be used inside a user transaction.

## Permissions

To create, alter, or drop a server audit principal, you must have ALTER ANY SERVER AUDIT or the CONTROL SERVER permission.

## Examples

### A. Change a server audit name

The following example changes the name of the server audit `HIPAA_Audit` to `HIPAA_Audit_Old`.

```sql
USE master;
GO

ALTER SERVER AUDIT HIPAA_Audit
WITH (STATE = OFF);
GO

ALTER SERVER AUDIT HIPAA_Audit
MODIFY NAME = HIPAA_Audit_Old;
GO

ALTER SERVER AUDIT HIPAA_Audit_Old
WITH (STATE = ON);
GO
```

### B. Change a server audit target

The following example changes the server audit called `HIPAA_Audit` to a file target.

```sql
USE master;
GO

ALTER SERVER AUDIT HIPAA_Audit
WITH (STATE = OFF);
GO

ALTER SERVER AUDIT HIPAA_Audit TO FILE (
    FILEPATH = '\\SQLPROD_1\Audit\',
    MAXSIZE = 1000 MB,
    RESERVE_DISK_SPACE = OFF
)
WITH (
    QUEUE_DELAY = 1000,
    ON_FAILURE = CONTINUE
);
GO

ALTER SERVER AUDIT HIPAA_Audit
WITH (STATE = ON);
GO
```

### C. Change a server audit WHERE clause

The following example modifies the where clause created in example C of [CREATE SERVER AUDIT](create-server-audit-transact-sql.md). The new WHERE clause filters for the user-defined event if of 27.

```sql
ALTER SERVER AUDIT [FilterForSensitiveData]
WITH (STATE = OFF);
GO

ALTER SERVER AUDIT [FilterForSensitiveData]
    WHERE user_defined_event_id = 27;
GO

ALTER SERVER AUDIT [FilterForSensitiveData]
WITH (STATE = ON);
GO
```

### D. Remove a WHERE clause

The following example removes a `WHERE` clause predicate expression.

```sql
ALTER SERVER AUDIT [FilterForSensitiveData]
WITH (STATE = OFF);
GO

ALTER SERVER AUDIT [FilterForSensitiveData]
REMOVE WHERE;
GO

ALTER SERVER AUDIT [FilterForSensitiveData]
WITH (STATE = ON);
GO
```

### E. Rename a server audit

The following example changes the server audit name from `FilterForSensitiveData` to `AuditDataAccess`.

```sql
ALTER SERVER AUDIT [FilterForSensitiveData]
WITH (STATE = OFF);
GO

ALTER SERVER AUDIT [FilterForSensitiveData]
MODIFY NAME = AuditDataAccess;
GO

ALTER SERVER AUDIT [AuditDataAccess]
WITH (STATE = ON);
GO
```

## Transact-SQL reference

- [DROP SERVER AUDIT (Transact-SQL)](drop-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION (Transact-SQL)](create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)](alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION (Transact-SQL)](drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)](alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)](drop-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](alter-authorization-transact-sql.md)

## Related content

- [sys.fn_get_audit_file (Transact-SQL)](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.server_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)
- [sys.server_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)
- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)
