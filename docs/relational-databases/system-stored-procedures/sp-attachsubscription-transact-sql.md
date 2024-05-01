---
title: "sp_attachsubscription (Transact-SQL)"
description: sp_attachsubscription attaches an existing subscription database to any Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_attachsubscription"
  - "sp_attachsubscription_TSQL"
helpviewer_keywords:
  - "sp_attachsubscription"
dev_langs:
  - "TSQL"
---
# sp_attachsubscription (Transact-SQL)

[!INCLUDE [sql-asdb](../../includes/applies-to-version/sql-asdb.md)]

Attaches an existing subscription database to any Subscriber. This stored procedure is executed at the new Subscriber on the `master` database.

> [!IMPORTANT]  
> This feature is deprecated and will be removed in a future release. This feature shouldn't be used in new development work. For merge publications that are partitioned using parameterized filters, we recommend using the new features of partitioned snapshots, which simplify the initialization of a large number of subscriptions. For more information, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md). For publications that aren't partitioned, you can initialize a subscription with a backup. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../replication/initialize-a-transactional-subscription-without-a-snapshot.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_attachsubscription
    [ @dbname = ] N'dbname'
    , [ @filename = ] N'filename'
    [ , [ @subscriber_security_mode = ] subscriber_security_mode ]
    [ , [ @subscriber_login = ] N'subscriber_login' ]
    [ , [ @subscriber_password = ] N'subscriber_password' ]
    [ , [ @distributor_security_mode = ] distributor_security_mode ]
    [ , [ @distributor_login = ] N'distributor_login' ]
    [ , [ @distributor_password = ] N'distributor_password' ]
    [ , [ @publisher_security_mode = ] publisher_security_mode ]
    [ , [ @publisher_login = ] N'publisher_login' ]
    [ , [ @publisher_password = ] N'publisher_password' ]
    [ , [ @job_login = ] N'job_login' ]
    [ , [ @job_password = ] N'job_password' ]
    [ , [ @db_master_key_password = ] N'db_master_key_password' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

Specifies the name of the destination subscription database. *@dbname* is **sysname**, with no default.

#### [ @filename = ] N'*filename*'

The name and physical location of the primary data file (`.mdf`). *@filename* is **nvarchar(260)**, with no default.

#### [ @subscriber_security_mode = ] *subscriber_security_mode*

The security mode of the Subscriber to use when connecting to a Subscriber when synchronizing. *@subscriber_security_mode* is **int**, with a default of `NULL`.

> [!NOTE]  
> Windows Authentication must be used. If *@subscriber_security_mode* isn't `1` (Windows Authentication), you receive an error.

#### [ @subscriber_login = ] N'*subscriber_login*'

The Subscriber login name to use when connecting to a Subscriber when synchronizing. *@subscriber_login* is **sysname**, with a default of `NULL`.

> [!NOTE]
> [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] If *@subscriber_security_mode* isn't `1` and *@subscriber_login* is specified, you receive an error.

#### [ @subscriber_password = ] N'*subscriber_password*'

The Subscriber password. *@subscriber_password* is **sysname**, with a default of `NULL`.

> [!NOTE]
> [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] If *@subscriber_security_mode* isn't `1` and *@subscriber_password* is specified, you receive an error.

#### [ @distributor_security_mode = ] *distributor_security_mode*

The security mode to use when connecting to a Distributor when synchronizing. *@distributor_security_mode* is **int**, with a default of `1`.

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication
- `1` specifies Windows authentication

[!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

#### [ @distributor_login = ] N'*distributor_login*'

The Distributor login to use when connecting to a Distributor when synchronizing. *@distributor_login* is **sysname**, with a default of `NULL`. *@distributor_login* is required if *@distributor_security_mode* is set to `0`.

#### [ @distributor_password = ] N'*distributor_password*'

The Distributor password. *@distributor_password* is **sysname**, with a default of `NULL`. *@distributor_password* is required if *@distributor_security_mode* is set to `0`. The value of *@distributor_password* must be shorter than 120 Unicode characters.

> [!IMPORTANT]  
> [!INCLUDE [ssnotestrongpass-md](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @publisher_security_mode = ] *publisher_security_mode*

The security mode to use when connecting to a Publisher when synchronizing. *@publisher_security_mode* is **int**, with a default of `1`.

- If `0`, specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.
- If `1`, specifies Windows Authentication. [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

#### [ @publisher_login = ] N'*publisher_login*'

The login to use when connecting to a Publisher when synchronizing. *@publisher_login* is **sysname**, with a default of `NULL`.

#### [ @publisher_password = ] N'*publisher_password*'

The password used when connecting to the Publisher. *@publisher_password* is **sysname**, with a default of `NULL`. The value of *@publisher_password* must be shorter than 120 Unicode characters.

> [!IMPORTANT]  
> [!INCLUDE [ssnotestrongpass-md](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @job_login = ] N'*job_login*'

The login for the Windows account under which the agent runs. *@job_login* is **nvarchar(257)**, with no default. This Windows account is always used for agent connections to the Distributor.

#### [ @job_password = ] N'*job_password*'

The password for the Windows account under which the agent runs. *@job_password* is **sysname**, with a default of `NULL`. The value of *job_password* must be shorter than 120 Unicode characters.

> [!IMPORTANT]  
> [!INCLUDE [ssnotestrongpass-md](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @db_master_key_password = ] N'*db_master_key_password*'

The password of a user-defined database master key (DMK). *@db_master_key_password* is **nvarchar(524)**, with a default of `NULL`. If *@db_master_key_password* isn't specified, an existing DMK is dropped and recreated.

> [!IMPORTANT]  
> [!INCLUDE [ssnotestrongpass-md](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_attachsubscription` is used in snapshot replication, transactional replication, and merge replication.

A subscription can't be attached to the publication if the publication retention period has expired. If a subscription with an elapsed retention period is specified, an error occurs when the subscription is either attached, or first synchronized. Publications with a publication retention period of `0` (never expire) are ignored.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_attachsubscription`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
