---
title: "sp_adddistpublisher (Transact-SQL)"
description: "Configures a Publisher to use a specified distribution database."
author: mashamsft
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_adddistpublisher"
  - "sp_adddistpublisher_TSQL"
helpviewer_keywords:
  - "sp_adddistpublisher"
dev_langs:
  - "TSQL"
---
# sp_adddistpublisher (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Configures a Publisher to use a specified distribution database. This stored procedure is executed at the Distributor on any database. The stored procedures [sp_adddistributor](sp-adddistributor-transact-sql.md) and [sp_adddistributiondb](sp-adddistributiondb-transact-sql.md) must have been run prior to using this stored procedure.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_adddistpublisher
    [ @publisher = ] N'publisher'
    , [ @distribution_db = ] N'distribution_db'
    [ , [ @security_mode = ] security_mode ]
    [ , [ @login = ] N'login' ]
    [ , [ @password = ] N'password' ]
    [ , [ @working_directory = ] N'working_directory' ]
    [ , [ @trusted = ] N'trusted' ]
    [ , [ @encrypted_password = ] encrypted_password ]
    [ , [ @thirdparty_flag = ] thirdparty_flag ]
    [ , [ @publisher_type = ] N'publisher_type' ]
    [ , [ @storage_connection_string = ] N'storage_connection_string' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The Publisher name. *@publisher* is **sysname**, with no default.

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15"
[!INCLUDE [custom-port](includes/custom-port.md)]
::: moniker-end

#### [ @distribution_db = ] N'*distribution_db*'

The name of the distribution database. *@distribution_db* is **sysname**, with no default. This parameter is used by replication agents to connect to the Publisher.

#### [ @security_mode = ] *security_mode*

The implemented security mode. This parameter is only used by replication agents to connect to the Publisher for queued updating subscriptions or with a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@security_mode* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` | Replication agents at the Distributor use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication to connect to the Publisher. |
| `1` (default) | Replication agents at the Distributor use Windows Authentication to connect to the Publisher. |

#### [ @login = ] N'*login*'

The login. This parameter is required if *security_mode* is `0`. *@login* is **sysname**, with a default of `NULL`. This parameter is used by replication agents to connect to the Publisher.

#### [ @password = ] N'*password*'

The password. *@password* is **sysname**, with a default of `NULL`. This parameter is used by replication agents to connect to the Publisher.

> [!IMPORTANT]  
> Don't use a blank password. Use a strong password.

#### [ @working_directory = ] N'*working_directory*'

The name of the working directory used to store data and schema files for the publication. *@working_directory* is **nvarchar(255)**, and defaults to the `ReplData` folder for this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For example, `C:\Program Files\Microsoft SQL Server\MSSQL\MSSQL.1\ReplData`. The name should be specified in UNC format.

For Azure SQL Database, use `\\<storage_account>.file.core.windows.net\<share>`.

#### [ @trusted = ] N'*trusted*'

*@trusted* is deprecated, and is provided for backward compatibility only. *@trusted* is **nvarchar(5)**, with a default of `false`. Setting this parameter to anything but `false` results in an error.

#### [ @encrypted_password = ] *encrypted_password*

Setting this parameter is no longer supported. *@encrypted_password* is **bit**, with a default of `0`. Setting this parameter to `1` results in an error.

#### [ @thirdparty_flag = ] *thirdparty_flag*

Specifies when the Publisher is [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. *@thirdparty_flag* is **bit**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `0` (default) | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| `1` | Database other than [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |

#### [ @publisher_type = ] N'*publisher_type*'

Specifies the Publisher type when the Publisher isn't [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. *@publisher_type* is **sysname**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `MSSQLSERVER` (default) | Specifies a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. |
| `ORACLE` | Specifies a standard Oracle Publisher. |
| `ORACLE GATEWAY` | Specifies an Oracle Gateway Publisher. |

For more information about the differences between an Oracle Publisher and an Oracle Gateway Publisher, see [Configure an Oracle Publisher](../replication/non-sql/configure-an-oracle-publisher.md).

#### [ @storage_connection_string = ] N'*storage_connection_string*'

Required for Azure SQL Database. *@storage_connection_string* is **nvarchar(255)**, with a default of `NULL`. Use the access key from the Azure portal, under **Storage > Settings**.

[!INCLUDE [Azure SQL Database link](../../includes/azure-sql-db-repl-for-more-information.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_adddistpublisher` is used by snapshot replication, transactional replication, and merge replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-adddistpublisher-tran_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_adddistpublisher`.

## Related content

- [Configure Publishing and Distribution](../replication/configure-publishing-and-distribution.md)
- [sp_changedistpublisher (Transact-SQL)](sp-changedistpublisher-transact-sql.md)
- [sp_dropdistpublisher (Transact-SQL)](sp-dropdistpublisher-transact-sql.md)
- [sp_helpdistpublisher (Transact-SQL)](sp-helpdistpublisher-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Configure Distribution](../replication/configure-distribution.md)
