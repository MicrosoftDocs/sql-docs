---
title: "sp_notify_operator (Transact-SQL)"
description: sp_notify_operator sends an e-mail message to an operator using Database Mail.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_notify_operator_TSQL"
  - "sp_notify_operator"
helpviewer_keywords:
  - "sp_notify_operator"
dev_langs:
  - "TSQL"
---
# sp_notify_operator (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sends an e-mail message to an operator using Database Mail.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_notify_operator
    [ [ @profile_name = ] N'profile_name' ]
    [ , [ @id = ] id ]
    [ , [ @name = ] N'name' ]
    [ , [ @subject = ] N'subject' ]
    [ , [ @body = ] N'body' ]
    [ , [ @file_attachments = ] N'file_attachments' ]
    [ , [ @mail_database = ] N'mail_database' ]
[ ; ]
```

## Arguments

#### [ @profile_name = ] N'*profile_name*'

The name of the Database Mail profile to use to send the message. *@profile_name* is **sysname**, with a default of `NULL`. If *@profile_name* isn't specified, the default Database Mail profile is used.

#### [ @id = ] *id*

The identifier for the operator to send the message to. *@id* is **int**, with a default of `NULL`.

One of *@id* or *@name* must be specified.

#### [ @name = ] N'*name*'

The name of the operator to send the message to. *@name* is **sysname**, with a default of `NULL`.

One of *@id* or *@name* must be specified.

An e-mail address must be defined for the operator before they can receive messages.

#### [ @subject = ] N'*subject*'

The subject for the e-mail message. *@subject* is **nvarchar(256)**, with a default of `NULL`.

#### [ @body = ] N'*body*'

The body of the e-mail message. *@body* is **nvarchar(max)**, with a default of `NULL`.

#### [ @file_attachments = ] N'*file_attachments*'

The name of a file to attach to the e-mail message. *@file_attachments* is **nvarchar(512)**, with a default of `NULL`.

#### [ @mail_database = ] N'*mail_database*'

Specifies the name of the mail host database. *@mail_database* is **sysname**, with a default of `msdb`. If no *@mail_database* is specified, the `msdb` database is used by default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Sends the message specified to the e-mail address of the operator specified. If the operator has no e-mail address configured, `sp_notify_operator` returns an error.

Database Mail and a mail host database must be configured before a notification can be sent to an operator.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

## Examples

The following example sends a notification e-mail to the operator `François Ajenstat` using the `AdventureWorks Administrator` Database Mail profile. The subject of the e-mail is `Test Notification`. The e-mail message contains the sentence, `This is a test of notification via e-mail`.

```sql
USE msdb;
GO

EXEC dbo.sp_notify_operator
    @profile_name = N'AdventureWorks Administrator',
    @name = N'François Ajenstat',
    @subject = N'Test Notification',
    @body = N'This is a test of notification via e-mail';
GO
```

## Related content

- [SQL Server Agent stored procedures (Transact-SQL)](sql-server-agent-stored-procedures-transact-sql.md)
- [sp_add_operator (Transact-SQL)](sp-add-operator-transact-sql.md)
- [sp_help_operator (Transact-SQL)](sp-help-operator-transact-sql.md)
- [sp_delete_operator (Transact-SQL)](sp-delete-operator-transact-sql.md)
